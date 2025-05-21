import express from 'express';
import weatherForecastRoutes from './routes/weatherForecastRoutes';
import creditCardInfoRoutes from './routes/creditCardInfoRoutes';
import productRoutes from './routes/productRoutes';
import logger from './logger';

const app = express();

const port = process.env.PORT || 3000;

app.use(express.json());

app.use('/weatherforecast', weatherForecastRoutes);
app.use('/creditcardinfo', creditCardInfoRoutes);
app.use('/products', productRoutes);

app.listen(port, () => {
  logger.info(`Server running on port ${port}`);
});
