import { Request, Response } from 'express';
import { getWeatherForecasts, setWeatherForecasts } from '../services/weatherForecastCacheService';

export const get = (_: Request, res: Response) => {
  const weatherForecasts = getWeatherForecasts();
  res.json(weatherForecasts);
};

export const post = (req: Request, res: Response) => {
  setWeatherForecasts(req.body);
  res.sendStatus(201);
};
