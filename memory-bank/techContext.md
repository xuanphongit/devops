# Technical Context: E-Commerce Platform

## 🛠️ Technology Stack

### Backend Services
- **Runtime**: .NET 8 (Latest LTS)
- **Framework**: ASP.NET Core Web API
- **ORM**: Entity Framework Core
- **Validation**: FluentValidation
- **Documentation**: Swagger/OpenAPI
- **Testing**: xUnit, Moq, TestContainers

### Frontend
- **Framework**: Vue.js 3 with Composition API
- **Build Tool**: Vite
- **UI Library**: Vuetify 3 (Material Design)
- **State Management**: Pinia
- **HTTP Client**: Axios
- **Testing**: Vitest, Vue Testing Library

### Data Layer
- **Primary Database**: PostgreSQL 15
- **Cache**: Redis 7
- **Message Queue**: RabbitMQ 3.12
- **Search**: Elasticsearch (future consideration)

### Infrastructure
- **Container Runtime**: Docker
- **Orchestration**: Kubernetes (GKE)
- **Service Mesh**: Istio (future consideration)
- **Ingress**: NGINX Ingress Controller
- **TLS**: cert-manager with Let's Encrypt

### Cloud Platform (GCP)
- **Compute**: Google Kubernetes Engine (GKE)
- **Networking**: VPC, Cloud Load Balancer
- **Storage**: Cloud SQL, Cloud Storage
- **Secrets**: Secret Manager
- **Monitoring**: Cloud Monitoring, Cloud Logging

## 🔧 Development Setup

### Prerequisites
```bash
# Required tools
- Docker Desktop
- kubectl
- helm
- terraform
- .NET 8 SDK
- Node.js 18+
- Git
```

### Local Development Environment
```bash
# Local Kubernetes
- Minikube or Kind for local testing
- Local registry for container images
- Port forwarding for service access

# Database
- PostgreSQL via Docker Compose
- Redis via Docker Compose
- RabbitMQ via Docker Compose
```

### IDE & Tools
- **Primary IDE**: Visual Studio Code
- **Extensions**: C# Dev Kit, Vue Language Features, Kubernetes
- **API Testing**: Postman or Insomnia
- **Database**: pgAdmin, Redis Commander
- **Monitoring**: k9s for Kubernetes management

## 🏗️ Infrastructure as Code

### Terraform Modules
```
terraform/
├── modules/
│   ├── gke/           # GKE cluster configuration
│   ├── network/       # VPC, subnets, firewall rules
│   ├── database/      # Cloud SQL instances
│   └── monitoring/    # Prometheus, Grafana setup
├── environments/
│   ├── dev/          # Development environment
│   ├── staging/      # Staging environment
│   └── prod/         # Production environment
└── terraform.tf     # Provider configuration
```

### Kubernetes Manifests
```
gitops/
├── base/             # Base Kubernetes resources
│   ├── namespace/
│   ├── rbac/
│   └── network-policies/
├── apps/             # Application deployments
│   ├── auth-service/
│   ├── product-service/
│   └── cart-service/
└── infrastructure/   # Infrastructure components
    ├── ingress/
    ├── monitoring/
    └── logging/
```

## 🔄 CI/CD Pipeline

### GitHub Actions Workflows
```
.github/workflows/
├── build-test.yml        # Build and test on PR
├── security-scan.yml     # Security vulnerability scanning
├── deploy-dev.yml        # Deploy to development
├── deploy-staging.yml    # Deploy to staging
├── deploy-prod.yml       # Deploy to production
└── infrastructure.yml    # Terraform deployment
```

### Pipeline Stages
1. **Code Quality**: Linting, formatting, static analysis
2. **Security**: Dependency scanning, container scanning
3. **Testing**: Unit tests, integration tests, e2e tests
4. **Build**: Docker image building and pushing
5. **Deploy**: Helm chart deployment via Argo CD
6. **Verify**: Health checks, smoke tests

## 📏 Technical Constraints

### Performance Requirements
- **API Response Time**: <200ms for 95th percentile
- **Database Query Time**: <100ms for complex queries
- **Page Load Time**: <2 seconds for first contentful paint
- **Throughput**: 1000+ requests per second per service

### Scalability Constraints
- **Horizontal Scaling**: All services must be stateless
- **Database Scaling**: Read replicas for read-heavy operations
- **Cache Strategy**: Redis cluster for high availability
- **Message Queue**: RabbitMQ cluster for reliability

### Security Requirements
- **Authentication**: JWT with 15-minute expiry
- **Authorization**: RBAC with principle of least privilege
- **Data Encryption**: AES-256 for data at rest, TLS 1.3 for transit
- **Compliance**: GDPR, PCI DSS Level 1 readiness

### Operational Constraints
- **Deployment Window**: No deployment restrictions (zero-downtime)
- **Backup Strategy**: Daily automated backups with 30-day retention
- **Disaster Recovery**: RTO <1 hour, RPO <15 minutes
- **Monitoring**: 24/7 alerting with on-call rotation

This technical foundation ensures our platform can scale, perform, and remain secure while providing an excellent developer experience. 