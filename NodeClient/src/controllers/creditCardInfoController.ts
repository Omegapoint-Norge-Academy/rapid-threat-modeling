import { Request, Response } from 'express';
import { CreditCardInfoModel } from '../models/creditCardInfoModel';

export const get = (_: Request, res: Response) => {
  const creditCards: CreditCardInfoModel[] = [
    {
      id: 1,
      owner: 'Klas Olesen',
      number: '1234234534564567',
      cvc: '123',
      expirationMonth: 12,
      expirationYear: 2025,
    },
  ];
  res.json(creditCards);
};

export const post = (req: Request, res: Response) => {
  res.sendStatus(201);
};
