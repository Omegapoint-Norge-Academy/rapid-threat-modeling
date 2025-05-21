import { WeatherForecastModel } from '../models/WeatherForecastModel';
import { WeatherForecastSeriesModel } from '../models/WeatherForecastSeriesModel';

let cachedWeatherForecasts: WeatherForecastSeriesModel = {
  weatherForecasts: [],
  timestamp: new Date(),
};

export function setWeatherForecasts(data: WeatherForecastModel[]) {
  cachedWeatherForecasts = {
    weatherForecasts: data,
    timestamp: new Date(),
  };
}

export function getWeatherForecasts(): WeatherForecastSeriesModel {
  return cachedWeatherForecasts;
}
