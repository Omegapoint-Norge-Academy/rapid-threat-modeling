import { Request, Response } from 'express';
import { WeatherForecastModel } from '../models/WeatherForecastModel';

export const get = (_: Request, res: Response) => {
  const weatherForecasts: WeatherForecastModel[] = [
    { date: new Date('2025-05-22'), temperatureC: 20, summary: 'Varmt' },
    { date: new Date('2025-05-23'), temperatureC: 15, summary: 'Småkaldt' },
    { date: new Date('2025-05-24'), temperatureC: 12, summary: 'Kjølig' },
  ];
  res.json(weatherForecasts);
};

export const post = (req: Request, res: Response) => {
  res.sendStatus(201);
};
