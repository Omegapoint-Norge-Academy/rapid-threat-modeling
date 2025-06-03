import { Router } from 'express';
import { getAll, getProductById } from '../controllers/productsController';

const productRoutes = Router();

productRoutes.get('/', getAll);
productRoutes.get('/:id', getProductById);

export default productRoutes;
