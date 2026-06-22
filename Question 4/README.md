# Question 4 — CI/CD / GitHub Actions or Azure Pipelines

Following Question 3, build and publish the SQL Server image to a container registry.

**Target image (GHCR example)**
```
ghcr.io/REPOSITORY_NAME/mssql:2019-latest
```

**Task**
- Complete `cd.yml` to:
  - Build the Docker image.
  - Push it to GHCR.
- You may answer using **GitHub Actions** or **Azure Pipelines** (your choice).

**Deliverables**
- Updated `cd.yml` (or an Azure Pipelines YAML file if you choose that route).
- A short note on how you would secure secrets in a real pipeline.
