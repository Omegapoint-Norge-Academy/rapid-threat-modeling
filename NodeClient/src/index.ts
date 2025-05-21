import express from 'express';
import weatherForecastRoutes from './routes/weatherForecastRoutes';
import creditCardInfoRoutes from './routes/creditCardInfoRoutes';

const app = express();

const port = process.env.PORT || 3000;

app.use(express.json());

app.use('/weatherforecast', weatherForecastRoutes);
app.use('/creditcardinfo', creditCardInfoRoutes);

app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
