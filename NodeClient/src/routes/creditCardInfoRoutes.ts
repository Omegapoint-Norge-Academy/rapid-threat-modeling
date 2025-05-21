import { Router } from 'express';
import { get, post } from '../controllers/creditCardInfoController';

const creditCardInfoRoutes = Router();

creditCardInfoRoutes.get('/', get);
creditCardInfoRoutes.post('/', post);

export default creditCardInfoRoutes;
