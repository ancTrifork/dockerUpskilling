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

### Option 1: Using Published Docker Image (Recommended)

**The easiest way to run this application is using the pre-built image from GitHub Container Registry:**

1. **Pull and run the latest image:**
   ```bash
   docker run -p 5043:8080 ghcr.io/anctrifork/dockerupskilling:latest
   ```

2. **Find all available versions:**
   - Visit: https://github.com/anctrifork/dockerUpskilling/pkgs/container/dockerupskilling
   - Available tags: `latest`, `main`, `main-<commit-sha>`

3. **Run a specific version:**
   ```bash
   docker run -p 5043:8080 ghcr.io/anctrifork/dockerupskilling:main-abc123
   ```

4. **Test the API:**
   ```bash
   curl http://localhost:5043/Hello
   curl http://localhost:5043/weatherforecast
   ```

### Option 2: Build from Source

1. Clone the repository:
   ```bash
   git clone https://github.com/ancTrifork/dockerUpskilling.git
   cd dockerUpskilling
   ```

2. Start the application with database:
   ```bash
   cd cs-webapi
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

The project features a complete CI/CD pipeline with GitHub Actions that:

### **Automated Build Pipeline**
```
Code Push → Build Image → Integration Tests → Publish to Registry
```

### **Pipeline Stages**
1. **Build**: Creates Docker image and saves as artifact
2. **Integration Test**: Downloads artifact, tests functionality
3. **Publish**: Pushes tested image to GitHub Container Registry

### **Container Registry**
- **Registry**: GitHub Container Registry (`ghcr.io`)
- **Package URL**: https://github.com/anctrifork/dockerUpskilling/pkgs/container/dockerupskilling
- **Auto-tagging**: `latest`, `main`, `main-<commit-sha>`
- **Quality Gates**: Only tested images are published

### **Workflow Features**
- **Build Once, Use Everywhere**: Single image artifact shared across jobs
- **Quality Assurance**: No broken images reach the registry
- **Version Control**: Multiple tags for different use cases
- **Efficient Caching**: Fast builds using GitHub Actions cache

Pipeline triggers automatically on pushes to `main` branch.

## Learning Objectives

This project demonstrates:
- **Multi-container Docker applications** with service orchestration
- **Database integration** with Entity Framework Core and auto-migrations
- **Advanced CI/CD pipelines** with GitHub Actions
- **Container registry publishing** and image distribution
- **Quality gates** and automated testing in CI/CD
- **Artifact optimization** (build once, test and deploy same image)
- **Professional DevOps practices** used in enterprise environments
- **Docker Compose** for local development and testing
- **RESTful API development** with ASP.NET Core
- **Container networking** and inter-service communication

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


Hello test