import { Router } from 'express';
import { get } from '../controllers/productsController';

const productRoutes = Router();

productRoutes.get('/', get);

export default productRoutes;
