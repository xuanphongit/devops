# Week 1 - Architecture Design & Project Setup

## 🎯 Main Objectives
- Design the overall architecture for the e-commerce platform.
- Identify the microservices and their communication methods.
- Choose the right technology and DevOps tools.
- Set up local development and initial repositories.

## ✅ Tasks

### 📌 Architecture Planning
- [ ] Draw the complete system architecture (frontend, backend, ingress, DB, message queue, monitoring).
- [ ] List all required microservices: Auth, Product, Cart, Order, Notification, etc.
- [ ] Choose technologies: .NET 8 (backend), Redis (cache), PostgreSQL (DB), RabbitMQ (message queue), Vue.js (frontend).
- [ ] Define how services communicate (RabbitMQ events).

### 🛠️ Tooling & Stack Selection
- [ ] Choose CI/CD: GitHub Actions + Argo CD.
- [ ] Choose Infrastructure as Code: Terraform (GKE, VPC, Secret Manager).
- [ ] Define GitOps structure using Helm or Kustomize.
- [ ] Choose monitoring stack: Prometheus + Grafana.
- [ ] Choose logging: Fluent Bit + Loki or GCP Logging.
- [ ] Plan backup strategies: DB backup to GCS via CronJobs.
- [ ] Plan security strategies: TLS (cert-manager), RBAC, Secret Manager, NetworkPolicy.

### 🧰 Environment Preparation
- [ ] Create main GitHub repo: `ecommerce-platform`.
- [ ] Set up folder structure: `terraform/`, `services/`, `frontend/`, `charts/`, `gitops/`.
- [ ] Set up local K8s cluster: Minikube or Kind for testing.
- [ ] Install tools: kubectl, helm, terraform, docker, Argo CD CLI, k9s.
- [ ] Draw the architecture diagram using draw.io or Whimsical.

### 🧠 Learning & Research
- [ ] Study microservices design for e-commerce with .NET.
- [ ] Learn GitOps with Argo CD.
- [ ] Review Kubernetes basics: Deployment, Service, Ingress, ConfigMap, Secret.
- [ ] Review Terraform modules for provisioning GKE and related infrastructure.


Folder structure 

ecommerce-platform/
├── terraform/
│   ├── gke/
│   └── network/
├── services/
│   ├── auth/
│   ├── product/
│   ├── cart/
│   └── ...
├── frontend/
│   └── vuejs-app/
├── charts/
│   └── helm/
├── gitops/
│   ├── base/
│   └── apps/
└── .github/
    └── workflows/