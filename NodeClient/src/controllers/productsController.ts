import { Request, Response } from 'express';
import logger from '../logger';
import { query } from '../services/dbService';
import { ProductModel } from '../models/ProductModel';

export const get = async (_: Request, res: Response) => {
  logger.info('GET products');
  const products = await query<ProductModel>(
    'SELECT Id as id, Name as name, Price as price FROM Products',
  );
  res.json(products);
};
