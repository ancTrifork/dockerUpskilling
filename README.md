# CS WebAPI Docker Multi-Container Application

A containerized ASP.NET Core Web API with PostgreSQL database, demonstrating multi-container Docker setup with automatic database migrations and CI/CD pipeline.

## Features

- RESTful API with Hello World endpoint
- Request logging to PostgreSQL database
- Automatic database migrations on startup
- Docker Compose orchestration
- GitHub Actions CI/CD pipeline

## Technology Stack

- **Backend**: ASP.NET Core 9.0
- **Database**: PostgreSQL 15
- **Containerization**: Docker & Docker Compose
- **CI/CD**: GitHub Actions
- **ORM**: Entity Framework Core

## Quick Start

### Prerequisites
- Docker Desktop
- Git

### Running the Application

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/cs-webapi-docker.git
   cd cs-webapi-docker
   ```

2. Start the application:
   ```bash
   docker compose up --build
   ```

3. Test the API:
   ```bash
   curl http://localhost:5043/Hello
   curl http://localhost:5043/Hello/Logs
   ```

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Hello` | Returns greeting with request count |
| GET | `/Hello/Logs` | Returns all logged requests |
| GET | `/weatherforecast` | Sample weather data |

## Architecture

```
┌─────────────────┐    ┌─────────────────┐
│   Host Machine  │    │   Docker Host   │
│                 │    │                 │
│  Port 5043 ────────► │  Port 8080      │
└─────────────────┘    │                 │
                       │ ┌─────────────┐ │
                       │ │ my-cs-webapi│ │
                       │ │ (ASP.NET)   │ │
                       │ └─────────────┘ │
                       │        │        │
                       │        │ Network │
                       │        │ Call    │
                       │        ▼        │
                       │ ┌─────────────┐ │
                       │ │my-webapi-db │ │
                       │ │(PostgreSQL) │ │
                       │ └─────────────┘ │
                       └─────────────────┘
```

## Development

### Project Structure

```
├── MyWebApi/                 # Main application
│   ├── Controllers/          # API controllers
│   ├── Data/                 # Database context
│   ├── Models/               # Data models
│   └── Migrations/           # EF Core migrations
├── docker-compose.yml        # Multi-container setup
├── Dockerfile               # Container image definition
└── .github/workflows/       # CI/CD pipelines
```

### Local Development

- The application auto-applies database migrations on startup
- Logs are persisted to `./logs/` directory
- Database data is ephemeral (recreated on container restart)

## CI/CD Pipeline

The project includes GitHub Actions workflows for:
- **Integration Tests**: Automated testing on every push
- **Docker Build**: Container image building and registry push
- **Multi-Environment Deployment**: Dev → Staging → Production

Pipeline triggers on pushes to `main` branch.

## Learning Objectives

This project demonstrates:
- Multi-container Docker applications
- Database integration with Entity Framework Core
- Automatic database migrations in containers
- GitHub Actions CI/CD pipelines
- Container orchestration with Docker Compose
- RESTful API development with ASP.NET Core

## Troubleshooting

### Container Issues
- Ensure Docker Desktop is running
- Try `docker compose down -v` to reset volumes
- Check container logs: `docker logs my-cs-webapi`

### Database Issues
- Migrations apply automatically on startup
- Check database logs: `docker logs my-webapi-db`
- Verify network connectivity between containers

## License

This project is for educational purposes.
