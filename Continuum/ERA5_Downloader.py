# ERA5 downloader

import sys
import os
import cdsapi
import datetime

download_dir = sys.argv[1]
req_year = int(sys.argv[2])
req_month = int(sys.argv[3])
req_day = int(sys.argv[4])
req_datetime = datetime.datetime(req_year, req_month, req_day)
print(req_datetime)

min_lat = float(sys.argv[5])
max_lat = float(sys.argv[6])
min_lon = float(sys.argv[7])
max_lon = float(sys.argv[8])

#output_file = 'ERA5_N' + str(min_lat) + "_to_" + str(max_lat) + "_W" + str(min_lon) + '_to_' + str(max_lat) + '_' + str(req_year)  + str(req_month) + str(req_day) + '.nc'
output_file = 'ERA5_N' + str(min_lat) + "_to_" + str(max_lat) + "_W" + str(min_lon) + '_to_' + str(max_lon) + '_' + str(req_datetime.strftime("%Y_%m_%d")) + '.nc'
# print (output_file)

c = cdsapi.Client()
c.retrieve('reanalysis-era5-single-levels',
        {
        'product_type': 'reanalysis',
        'format': 'netcdf',
        'variable': [
            '100m_u_component_of_wind', '100m_v_component_of_wind', '2m_temperature',
            'surface_pressure',
        ],
        'year': req_year,
        'month': req_month,
        'day': req_day,
        'time': [
            '00:00', '01:00', '02:00',
            '03:00', '04:00', '05:00',
            '06:00', '07:00', '08:00',
            '09:00', '10:00', '11:00',
            '12:00', '13:00', '14:00',
            '15:00', '16:00', '17:00',
            '18:00', '19:00', '20:00',
            '21:00', '22:00', '23:00',
        ],
        'area': [min_lat,min_lon,max_lat,max_lon],
    },
    download_dir + "\\" + output_file)
