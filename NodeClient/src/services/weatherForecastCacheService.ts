import { WeatherForecastModel } from '../models/WeatherForecastModel';
import { WeatherForecastSeriesModel } from '../models/WeatherForecastSeriesModel';

let cachedWeatherForecasts: WeatherForecastSeriesModel = {
  weatherForecasts: [],
  timestamp: new Date(),
};

export const setWeatherForecasts = (data: WeatherForecastModel[]) => {
  cachedWeatherForecasts = {
    weatherForecasts: data,
    timestamp: new Date(),
  };
};

export const getWeatherForecasts = (): WeatherForecastSeriesModel => {
  return cachedWeatherForecasts;
};
