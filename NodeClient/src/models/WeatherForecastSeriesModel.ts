import { WeatherForecastModel } from './WeatherForecastModel';

export type WeatherForecastSeriesModel = {
  weatherForecasts: WeatherForecastModel[];
  timestamp: Date;
};
