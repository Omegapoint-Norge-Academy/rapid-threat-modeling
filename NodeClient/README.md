# NodeClient

This directory contains a Node.js Express API server that exposes endpoints for
products, weather forecasts, and credit card data. It is designed to receive
data from the Worker service and the Azure SQL database and serve it via REST
endpoints.

## Features

- Receives **Credit Card Info** and **Weather Forecasts** from the Worker service
- Fetches **Products** directly from the Azure SQL database
- Exposes data through REST API endpoints

## Architecture

- Hosted as an **Azure App Service**
- Uses TypeScript and Express
- Connects directly to the database for fetching products
- Receives POSTs from the Worker every 60 seconds

## Endpoints

- `GET /products` — Returns products from the database
- `POST /weatherforecast` — Receives Weather Forecasts from the Worker
- `GET /weatherforecast` — Returns cached Weather Forecasts
- `POST /creditcardinfo` — Receives Credit Card Info from the Worker
- `GET /creditcardinfo` — Returns cached Credit Card Info

## Security

- Sensitive data is only pushed from the Worker, never queried directly.
