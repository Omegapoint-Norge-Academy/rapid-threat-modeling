# Rapid Threat Modeling Workshop

This repository contains materials for the Rapid Threat Modeling workshop.
In this workshop you will analyse the provided system architecture to identify
potential security vulnerabilities using rapid threat modeling.

The goal is to understand how data flows to identify weak points. Then, using
this information, look for vulnerabilities the codebase.

## Architecture

This example system consists of three servers and one Azure SQL database.
All three servers read from the database, but only the Worker (.NET)
accesses sensitive data in the database. The Worker then distributes this
confidential data to the two public servers: an ASP.NET web server
(BlazorClient) and an Express web server (NodeClient).

- **BlazorClient (ASP.NET)** hosts both an API and a Blazor
  Server frontend. The latter of which is accessible only to users
  authenticated with Entra ID.
- **NodeClient (Express.js)** serves an API that provides both
  database data and data received from the Worker.
- **Worker (.NET)** loads sensitive data from the database and pushes
  this data periodically to the two client servers through HTTP POST requests.

Clients (both browsers and other servers) can consume the public APIs and the
Blazor frontend.

<img src="architecture.png" width=800 />

## Project details

Additional project details can be found in each project's respective README:

- [BlazorClient](/BlazorClient/README.md)
- [NodeClient](/NodeClient/README.md)
- [Worker](/Worker/README.md)
