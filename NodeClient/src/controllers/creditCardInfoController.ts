import { Request, Response } from 'express';
import { getCreditCardInfos, setCreditCardInfos } from '../services/creditCardInfoCacheService';
import logger from '../logger';

export const get = (_: Request, res: Response) => {
  logger.info('GET credit card info');
  const creditCards = getCreditCardInfos();
  res.json(creditCards);
};

export const post = (req: Request, res: Response) => {
  logger.info('POST credit card info');
  setCreditCardInfos(req.body);
  res.sendStatus(201);
};
