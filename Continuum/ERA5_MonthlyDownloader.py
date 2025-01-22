# ERA5 downloader

def GetDayJSON(thisMonth, thisYear):

    if thisMonth in {1, 3, 5, 7, 8, 10, 12}:
        dayStr = [f"{i:02}" for i in range(1, 32)]
    elif thisMonth in {4, 6, 9, 11}:
        dayStr = [f"{i:02}" for i in range(1, 31)]
    else:
    # It's February so figure out if it's leap year
        if thisYear % 4 == 0:
            dayStr = [f"{i:02}" for i in range(1, 30)]
        else:
            dayStr = [f"{i:02}" for i in range(1, 29)]
            
    return dayStr

import sys
import os
import cdsapi
import datetime

download_dir = sys.argv[1]
req_year = int(sys.argv[2])
req_month = int(sys.argv[3])

req_datetime = datetime.datetime(req_year, req_month, 1)
print(req_datetime)

min_lat = float(sys.argv[4])
max_lat = float(sys.argv[5])
min_lon = float(sys.argv[6])
max_lon = float(sys.argv[7])

incl10mWS = sys.argv[8].lower() in ('true', '1', 'yes')
incl10mGust = sys.argv[9].lower() in ('true', '1', 'yes')

if not incl10mWS and not incl10mGust:
    varStr = ["100m_u_component_of_wind", "100m_v_component_of_wind", "2m_temperature", "surface_pressure"]
elif incl10mWS and not incl10mGust:
    varStr = ["100m_u_component_of_wind", "100m_v_component_of_wind", "2m_temperature", "surface_pressure", "10m_u_component_of_wind", "10m_v_component_of_wind"]
elif incl10mWS and incl10mGust:
    varStr = ["100m_u_component_of_wind", "100m_v_component_of_wind", "2m_temperature", "surface_pressure", "10m_u_component_of_wind", "10m_v_component_of_wind", "10m_wind_gust_since_previous_post_processing"]
elif not incl10mWS and incl10mGust:
    varStr = ["100m_u_component_of_wind", "100m_v_component_of_wind", "2m_temperature", "surface_pressure", "10m_wind_gust_since_previous_post_processing"]


#output_file = 'ERA5_N' + str(min_lat) + "_to_" + str(max_lat) + "_W" + str(min_lon) + '_to_' + str(max_lat) + '_' + str(req_year)  + str(req_month) + str(req_day) + '.nc'
output_file = 'ERA5_N' + str(min_lat) + "_to_" + str(max_lat) + "_W" + str(min_lon) + '_to_' + str(max_lon) + '_' + str(req_datetime.strftime("%Y_%m")) + '.nc'
# print (output_file)

dayStr = GetDayJSON(req_month, req_year)

c = cdsapi.Client()
c.retrieve('reanalysis-era5-single-levels',
        {
        'product_type': 'reanalysis',
        'data_format': 'netcdf_legacy',
        'variable': varStr,
        'year': req_year,
        'month': req_month,
        'day': dayStr,
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



