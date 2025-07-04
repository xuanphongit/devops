# 🔐 Auth Service

Authentication and authorization microservice for the E-Commerce platform built with Clean Architecture principles.

## 🏗️ Architecture

This service follows **Clean Architecture** with clear separation of concerns:

```
Auth.API/           # Presentation Layer (Controllers, HTTP handling)
Auth.Application/   # Application Layer (Use Cases, Business Rules)
Auth.Domain/        # Domain Layer (Entities, Interfaces)
Auth.Infrastructure/# Infrastructure Layer (Database, External Services)
```

### 📁 Project Structure

```
services/auth/
├── Auth.API/                    # Web API (.NET 8)
│   ├── Controllers/            # HTTP controllers
│   ├── Program.cs              # Application startup
│   └── appsettings.json        # Configuration
├── Auth.Application/            # Use cases and business logic
│   ├── DTOs/                   # Data Transfer Objects
│   └── UseCases/               # Application use cases
│       ├── RegisterUser/       # User registration logic
│       └── LoginUser/          # User login logic
├── Auth.Domain/                # Core business entities
│   ├── Entities/               # Domain entities (User)
│   └── Interfaces/             # Repository and service contracts
├── Auth.Infrastructure/        # External concerns
│   ├── Data/                   # Database context
│   ├── Repositories/           # Data access implementations
│   └── Services/               # External service implementations
├── Auth.UnitTests/             # Unit tests (TODO)
├── Dockerfile                  # Container configuration
└── README.md                   # This file
```

## 🚀 Features

- **User Registration**: Create new user accounts
- **User Login**: Authenticate existing users
- **JWT Token Generation**: Secure authentication tokens
- **Password Hashing**: BCrypt secure password storage
- **Email Validation**: Check email availability
- **User Profile**: Get authenticated user information
- **Role-Based Access**: Support for different user roles

## 🛠️ Technology Stack

- **.NET 8**: Latest LTS framework
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for database access
- **PostgreSQL**: Primary database
- **BCrypt**: Password hashing
- **JWT**: Authentication tokens
- **Swagger**: API documentation

## 📋 API Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | ❌ |
| POST | `/api/auth/login` | Login user | ❌ |
| GET | `/api/auth/profile` | Get user profile | ✅ |
| GET | `/api/auth/check-email` | Check email availability | ❌ |
| GET | `/health` | Health check | ❌ |

## 🔧 Setup & Development

### Prerequisites

- .NET 8 SDK
- PostgreSQL
- Docker (optional)

### Local Development

1. **Clone and navigate to the service**:
   ```bash
   cd services/auth
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Update database connection** in `Auth.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Database=ecommerce_auth;Username=your_user;Password=your_password"
     }
   }
   ```

4. **Run database migrations**:
   ```bash
   cd Auth.API
   dotnet ef database update
   ```

5. **Start the service**:
   ```bash
   dotnet run
   ```

6. **Access Swagger UI**: `https://localhost:5001/swagger`

### Docker Development

1. **Build the image**:
   ```bash
   docker build -t auth-service .
   ```

2. **Run the container**:
   ```bash
   docker run -p 5000:80 -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Database=ecommerce_auth;Username=postgres;Password=postgres" auth-service
   ```

## 🧪 Testing

### Unit Tests

```bash
cd Auth.UnitTests
dotnet test
```

### API Testing

Use the provided HTTP file or Swagger UI to test endpoints:

**Register User**:
```json
POST /api/auth/register
{
  "email": "john@example.com",
  "firstName": "John",
  "lastName": "Doe",
  "password": "password123",
  "confirmPassword": "password123"
}
```

**Login User**:
```json
POST /api/auth/login
{
  "email": "john@example.com",
  "password": "password123"
}
```

## 🔒 Security

- **Password Hashing**: BCrypt with salt
- **JWT Tokens**: Secure, stateless authentication
- **Input Validation**: Comprehensive request validation
- **Error Handling**: Secure error messages
- **HTTPS**: TLS encryption in production

## 📊 Monitoring

- **Health Checks**: `/health` endpoint
- **Logging**: Structured logging with Serilog
- **Metrics**: Prometheus metrics (TODO)
- **Tracing**: Distributed tracing (TODO)

## 🚀 Deployment

### Kubernetes

```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: auth-service
spec:
  replicas: 3
  selector:
    matchLabels:
      app: auth-service
  template:
    metadata:
      labels:
        app: auth-service
    spec:
      containers:
      - name: auth-service
        image: auth-service:latest
        ports:
        - containerPort: 80
        env:
        - name: ConnectionStrings__DefaultConnection
          valueFrom:
            secretKeyRef:
              name: auth-secrets
              key: connection-string
```

### Environment Variables

- `ConnectionStrings__DefaultConnection`: Database connection string
- `JwtSettings__SecretKey`: JWT signing key
- `JwtSettings__Issuer`: JWT issuer
- `JwtSettings__Audience`: JWT audience

## 🤝 Contributing

1. Follow Clean Architecture principles
2. Write unit tests for new features
3. Update API documentation
4. Follow the existing code style

## 📝 License

This project is part of the E-Commerce platform and follows the same license terms. 