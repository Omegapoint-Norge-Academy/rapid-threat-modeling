# Worker

This .NET Worker Service handles secure data access from Azure SQL and pushes
results to other services. It operates as a long-running background process and
acts as the only component with access to sensitive data.

## Features

- Fetches **Credit Card Info** from the Azure SQL database
- Periodically generates and pushes **Weather Forecasts**
- Posts data every 60 seconds to:
  - BlazorClient API
  - NodeClient API

## Architecture

- Hosted as an **Azure App Service**.
- Uses the shared `Database` project for entity and DbContext access

## Services

- `CreditCardInfoService.cs` — fetches Credit Card Info from the database
- `WeatherForecastService.cs` — Generates Weather Forecasts
- `TimedWorker.cs` — Periodically sends data to client applications

## Security

- Only this service accesses sensitive data directly
