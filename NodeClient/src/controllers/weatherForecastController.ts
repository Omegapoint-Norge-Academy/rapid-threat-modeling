import { Request, Response } from 'express';
import { getWeatherForecasts, setWeatherForecasts } from '../services/weatherForecastCacheService';
import logger from '../logger';

export const get = (_: Request, res: Response) => {
  logger.info('GET weather forecast');
  const weatherForecasts = getWeatherForecasts();
  res.json(weatherForecasts);
};

export const post = (req: Request, res: Response) => {
  logger.info('POST weather forecast');
  setWeatherForecasts(req.body);
  res.sendStatus(201);
};
