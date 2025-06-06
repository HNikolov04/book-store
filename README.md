# 📚 Book Store

A full-stack **Book Store** application developed as a student project at Technical University Sofia, Bulgaria. This project demonstrates the implementation of a modern web application with a **C# API**, **MSSQL database**, and a **clean frontend (HTML, CSS, JavaScript)**.

## 🌟 Features

- **Book Management**: Add, edit, delete, and view books with details like title, descriptions, price, and image
- **Order System**: Process and manage customer book orders
- **Review System**: Allow customers to leave and manage book reviews
- **Admin Panel**: Dedicated management interface for administrators
- **Responsive Design**: Modern and user-friendly interface

## 🛠 Technology Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- MSSQL Database
- Clean Architecture Pattern
- Repository Pattern
- CORS enabled for local development

### Frontend
- HTML5
- CSS3
- Vanilla JavaScript
- Responsive Design

## 🏗 Project Structure

```
book-store/
├── server/               # Backend C# solution
│   └── src/
│       ├── BookStore.Api/            # API endpoints and configuration
│       ├── BookStore.Application/    # Application logic and DTOs
│       ├── BookStore.Domain/         # Domain entities and interfaces
│       └── BookStore.Infrastructure/ # Data access and migrations
└── client/               # Frontend application
    ├── html/             # HTML pages
    ├── css/              # Stylesheets
    ├── js/               # JavaScript files
    └── assets/           # Images and other static files
```

## 🚀 Getting Started

1. Clone the repository
2. Set up the database:
   - Update the connection string in `appsettings.json`
   - Run Entity Framework migrations
3. Start the backend server
4. Open the frontend in a web browser

## 📝 About

This project was developed as part of the curriculum at Technical University Sofia, Bulgaria. It serves as a practical implementation of web development concepts, demonstrating the integration of a C# backend with a clean frontend implementation.