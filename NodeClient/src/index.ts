import express from 'express';

const app = express();

const port = process.env.PORT || 3000;

app.get('/weatherforecast', (request, response) => {
  const forecast = [
    { date: '2025-05-22', temperatureC: 20, summary: 'Varmt' },
    { date: '2025-05-23', temperatureC: 15, summary: 'Småkaldt' },
    { date: '2025-05-24', temperatureC: 12, summary: 'Kjølig' },
  ];
  response.json(forecast);
});

app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
