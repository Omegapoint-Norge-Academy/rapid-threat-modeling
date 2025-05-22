# BlazorClient

This project contains an ASP.NET Blazor Server and Web API that provides a
secure frontend for authenticated users. It fetches data from the database
and receives data from the background worker service.

## Features

- Blazor Server Frontend with Entra ID authentication
  - Displays **Products** fetched directly from the Azure SQL database
  - Displays **Weather Forecasts** pushed by the Worker service
- Credit Card Info API that receives sensitive data from the Worker

## Architecture

- Hosted as an **Azure App Service**
- Uses the shared `Database` project for entity and DbContext access
- Receives data periodically from the **Worker** via HTTP POST
  (credit card info and weather forecasts)

## Endpoints

- `GET /api/creditcardinfo` — Returns cached Credit Card Info
- `POST /api/creditcardinfo` — Receives Credit Card Info from the Worker
- `GET /api/weatherforecast` — Returns cached Weather Forecasts
- `POST /api/weatherforecast` — Receives Weather Forecasts from the Worker

## Security

- Entra ID authentication
