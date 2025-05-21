import { Request, Response } from 'express';
import { getCreditCardInfos, setCreditCardInfos } from '../services/creditCardInfoCacheService';

export const get = (_: Request, res: Response) => {
  const creditCards = getCreditCardInfos();
  res.json(creditCards);
};

export const post = (req: Request, res: Response) => {
  setCreditCardInfos(req.body);
  res.sendStatus(201);
};
