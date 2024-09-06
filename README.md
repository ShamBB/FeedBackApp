# FeedBackApp

## Overview
A simple MV Feedback App.

## Database
This application uses postgresql as its database management system.

## Installation
To set up the project, clone the repository and restore the dependencies using the following commands:
```bash
- cd projectname
- dotnet restore

### Configuration
- Change the connection string in `appsettings.json`:
json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=Assesment;Username=postgres;Password=your_password;"
  }

### Database Migration
- Run the migration in the Package Manager Console:
update-database
- dotnet-run
```
