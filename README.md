# .NET Core MVC + Neon PostgreSQL Setup Guide

This guide walks you through creating a .NET Core MVC web application connected to a Neon PostgreSQL database with full CRUD support.

---

## ✅ Step 1: Create a GitHub Repository

Create a new repository using GitHub or your CLI of choice.

---

## ✅ Step 2: Create a New MVC Project

```bash
dotnet new mvc -n MyMvcApp
cd MyMvcApp
```

---

## ✅ Step 3: Install Required Packages

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

---

## ✅ Step 4: Install EF Core Tools

```bash
dotnet tool install --global dotnet-ef
exec $SHELL  # Reload the shell so dotnet-ef is available
```

---

## ✅ Step 5: Scaffold DbContext and Models from Neon

```bash
dotnet ef dbcontext scaffold "Host=ep-broad-band-a59s3iyq-pooler.us-east-2.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=npg_zO3gcu7JKMpm;SSL Mode=Require;Trust Server Certificate=true" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
```

---

## ✅ Step 6: Add Code Generator and Scaffold Controllers

### Install Code Generator

```bash
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool install -g dotnet-aspnet-codegenerator
```

### Scaffold Controllers with Views

```bash
dotnet aspnet-codegenerator controller -name RegionsController -m Region -dc NeondbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries

dotnet aspnet-codegenerator controller -name CountriesController -m Country -dc NeondbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
```

---

## ✅ Done!

You now have a full ASP.NET Core MVC app connected to Neon PostgreSQL with auto-generated models and controllers for your tables.