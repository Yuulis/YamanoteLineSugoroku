import pandas as pd
import numpy as np


class DataGenerator:
    def __init__(self, book_df):
        self.book_df = book_df


    # Get line_cd from line_name.
    def get_line_cd(self, line_name):
        line_df = self.book_df['line20240426']
        line_df.reset_index(inplace=True)
        line_names = line_df['line_name'].values
        line_cd = -1

        if line_name in line_names:
            line_cd = line_df['line_cd'][line_df['line_name'] == line_name].values[0]
        else:    
            assert line_cd != -1, f"'{line_name}' is not found."

        return line_cd


    # Get stations' list from line_cd.
    def get_stations(self, line_cd):
        station_df = self.book_df['station20240426']
        station_df.reset_index(inplace=True)
        stations_list = station_df.loc[station_df['line_cd'] == line_cd]
        stations_list = stations_list[['station_cd', 'station_g_cd', 'station_name']]
        stations_list.reset_index(inplace=True, drop=True)

        return stations_list


    # Set field index for each station in stations' list.
    def set_field_index(self, stations_list):
        field_map = book_df['map_JR']
        field_map = field_map.replace([np.nan], ['-'])
        field_map = field_map.to_numpy().tolist()

        line_stations_df = pd.DataFrame(stations_list)
        line_stations_df['pos_x'] = -1
        line_stations_df['pos_y'] = -1
        
        for station_name in line_stations_df['station_name']:
            index = (-1, -1)
            for y, row in enumerate(field_map):
                for x, cell in enumerate(field_map[y]):
                    if cell == station_name:
                        index = (x, y)
                        break

                if index != (-1, -1):
                    break
        
            assert index != (-1, -1), f"'{station_name}' is not found."
            
            line_stations_df.loc[line_stations_df['station_name'] == station_name, 'pos_x'] = index[0]
            line_stations_df.loc[line_stations_df['station_name'] == station_name, 'pos_y'] = index[1]
        
        return line_stations_df


if __name__ == '__main__':
    # The 1st row and column of 'map_JR' sheet in this Excel file must be empty for the correct reading (in set_field_index).
    path = '.\\Assets\\fieldData\\'
    book_df = pd.read_excel(path + 'railway_data.xlsx', sheet_name=None, index_col=0)

    data_gen = DataGenerator(book_df)
    target_line = 'JR山手線'
    line_stations_df = data_gen.set_field_index(data_gen.get_stations(data_gen.get_line_cd(target_line)))

    print(line_stations_df)

    # Output to csv file.
    line_stations_df.to_csv(path + f'stations_data_{target_line}.csv', encoding="utf-8", index=False)