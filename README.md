# Mobile Stock Capture Application

A Windows Forms application for managing mobile phone inventory with SQLite database support.

## Project Overview

This application provides a user-friendly interface for stock management operations including:
- Adding new mobile phone records
- Deleting existing records
- Finding and retrieving mobile phone information

## Technology Stack

- **Platform**: Windows Forms (C#/.NET Framework)
- **Database**: SQLite
- **IDE**: Visual Studio

## Project Structure

```
Mobile-Stock-Capture-App/
├── MobileStockApp/
│   ├── Forms/
│   │   └── MainForm.cs
│   ├── Database/
│   │   └── DatabaseManager.cs
│   ├── Models/
│   │   └── MobilePhone.cs
│   ├── App.config
│   └── Program.cs
├── Tests/
│   └── TestCases.md
├── Documentation/
│   └── SoftwareTestingReport.md
└── README.md
```

## Getting Started

1. Clone the repository
2. Open the solution in Visual Studio
3. Restore NuGet packages
4. Build and run the application

## Database Setup

The application uses SQLite. The database is automatically created on first run.

## Features

### Add Record
- Enter MobileCode, Make, and Quantity
- Click "Add" to insert into database
- Displays "Record Added" on success

### Delete Record
- Enter MobileCode in search field
- Click "Delete" to remove record
- Displays "Record Found" or "Record NOT Found"

### Find Record
- Enter MobileCode in search field
- Click "Find" to retrieve record details
- Displays "Record Deleted" on success
