import { Request, Response } from 'express';
import logger from '../logger';
import { query } from '../services/dbService';

export const getAll = async (_: Request, res: Response) => {
  logger.info('GET products');
  const products = await query('SELECT * FROM Products');
  res.json(products);
};

export const getProductById = async (req: Request, res: Response) => {
  logger.info(`GET product with id: ${req.params.id}`);
  const product = await query(`SELECT * FROM Products WHERE id = ${req.params.id}`);
  res.json(product);
};
