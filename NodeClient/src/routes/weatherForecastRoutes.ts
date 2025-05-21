import { Router } from 'express';
import { get, post } from '../controllers/weatherForecastController';

const weatherForecastRoutes = Router();

weatherForecastRoutes.get('/', get);
weatherForecastRoutes.post('/', post);

export default weatherForecastRoutes;
