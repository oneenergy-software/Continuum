# Downloads pre-processed ERA5 ARCO data for a specific location and time range, and saves it as a CSV file.
# ERA5 ARCO point downloader

import sys
import os
import calendar
import xarray as xr
import numpy as np
import pandas as pd

print("About to run ERA5 Arco downloader")
# --------------------------------------------------------
# Arguments
# --------------------------------------------------------

download_dir = sys.argv[1]

lat = float(sys.argv[2])
lon = float(sys.argv[3])

start_year = int(sys.argv[4])
start_month = int(sys.argv[5])

end_year = int(sys.argv[6])
end_month = int(sys.argv[7])

incl10mWS = sys.argv[8].lower() in ('true', '1', 'yes')
incl10mGust = sys.argv[9].lower() in ('true', '1', 'yes')

# --------------------------------------------------------
# Snap coordinates to ERA5 grid
# --------------------------------------------------------

GRID_RES = 0.25

lat = round(lat / GRID_RES) * GRID_RES
lon = round(lon / GRID_RES) * GRID_RES

if lon < 0:
    lon += 360

print(f"Using ERA5 node lat={lat}, lon={lon}")

# --------------------------------------------------------
# Variables
# --------------------------------------------------------

vars_to_get = [
    "u100",
    "v100",
    "t2m",
    "sp"
]

if incl10mWS:
    vars_to_get.extend([
        "u10",
        "v10"
    ])

if incl10mGust:
    vars_to_get.append(
        "fg10"
    )

# --------------------------------------------------------
# Time range
# --------------------------------------------------------

start_date = f"{start_year:04d}-{start_month:02d}-01"

last_day = calendar.monthrange(end_year, end_month)[1]

end_date = (
    f"{end_year:04d}-"
    f"{end_month:02d}-"
    f"{last_day:02d}T23:00:00"
)

print(f"Start: {start_date}")
print(f"End:   {end_date}")

# --------------------------------------------------------
# ERA5 ARCO dataset
# --------------------------------------------------------

ERA5_ARCO_URL = "gs://gcp-public-data-arco-era5/co/single-level-reanalysis.zarr-v2"

print("Opening ERA5 ARCO dataset...")

ds = xr.open_zarr(
    ERA5_ARCO_URL,    
    consolidated=True,
    storage_options={"token": "anon"}    
)

print("Dataset opened.")

ds = ds[vars_to_get]

latitudes = ds.latitude.values
longitudes = ds.longitude.values

dist = ((latitudes - lat) ** 2 + (longitudes - lon) ** 2)
idx = np.argmin(dist)

print(f"Nearest index: {idx}", flush=True)
print(f"Dataset lat: {latitudes[idx]}", flush=True)
print(f"Dataset lon: {longitudes[idx]}", flush=True)


print("Selecting point...", flush=True)
ds_point = ds.isel(values=[idx])

print("Selecting time...", flush=True)
ds_point = ds_point.sel(time=slice(start_date, end_date))

# --------------------------------------------------------
# Convert to dataframe
# --------------------------------------------------------

print("Loading subset into memory...", flush=True)

ds_point = ds_point.load()

print("Loading arrays...", flush=True)

data = {"time": ds_point.time.values}

for var in vars_to_get:
    print(f"Loading {var}...", flush=True)
    data[var] = ds_point[var].values.squeeze()

print("Creating dataframe...", flush=True)

df = pd.DataFrame(data)

# --------------------------------------------------------
# Output filename
# --------------------------------------------------------
print("Creating output file...", flush=True)
output_file = (
    f"ERA5_ARCO_"
    f"{lat:.2f}_"
    f"{lon:.2f}_"
    f"{start_year:04d}_{start_month:02d}_"
    f"to_"
    f"{end_year:04d}_{end_month:02d}.csv"
)

output_path = os.path.join(
    download_dir,
    output_file
)

# --------------------------------------------------------
# Save CSV
# --------------------------------------------------------

print("Saving to CSV", flush=True)
df.to_csv(
    output_path,
    index=False
)

print(f"Saved: {output_path}")
