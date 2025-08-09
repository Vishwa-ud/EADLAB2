# EAD MongoDB Library Project

A .NET 9.0 console application demonstrating MongoDB integration using the MongoDB.Driver NuGet package. This project creates a library management system that connects to MongoDB Atlas and performs CRUD operations on a BookStore collection.

## Project Structure

```
EAD_MongoLab_Library/
├── Program.cs              # Main application logic
├── BookStore.cs           # Book model class
├── EAD_MongoLab_Library.csproj
└── README.md
```

## Prerequisites

- .NET 9.0 SDK
- MongoDB Atlas account and cluster
- Visual Studio Code with C# Dev Kit extension
- MongoDB connection string

## Dependencies

- **MongoDB.Driver** (v3.4.2) - Official MongoDB driver for .NET

## Setup Instructions

### 1. Clone/Download the Project
```bash
git clone <repository-url>
cd EADLAB2
```

### 2. Install Dependencies
```bash
dotnet restore
```

### 3. MongoDB Configuration
Update the connection string in `Program.cs`:
```csharp
const string connectionUri = "your-mongodb-atlas-connection-string";
```

## Running the Application

### Build the Project
```bash
dotnet build
```

### Run the Application
```bash
dotnet run
```

### Alternative: Run from Project Directory
```bash
cd EAD_MongoLab_Library
dotnet run
```

## Features

### 1. MongoDB Connection
- Connects to MongoDB Atlas using connection string
- Verifies connection with ping command
- Uses MongoDB Server API v1

### 2. Data Model
The `BookStore` class represents a book with the following properties:
- **ISBN** (string) - Primary key with `[BsonId]` attribute
- **BookTitle** (string) - Title of the book
- **Author** (string) - Author name
- **Category** (string) - Book category
- **Price** (decimal) - Book price
- **TotalPages** (int?) - Optional total pages with `[BsonIgnoreIfNull]`

### 3. Database Operations
- Creates `LibraryDB` database
- Creates `BookStore` collection
- Inserts sample book data
- Performs various queries

### 4. Sample Data
The application inserts 4 sample books:
1. MongoDB Basics by Tanya
2. C# Basics by Tanvi
3. SQL Server Basics by Tushar
4. Entity Framework Basics by Somya

### 5. Query Operations
- Count books with more than 200 pages
- Find books starting with "Mongo"
- Find the cheapest book
- Find book by specific ISBN

## MongoDB Shell Commands

### Connect to Database
```bash
use LibraryDB
```

### View All Documents
```bash
db.BookStore.find()
```

### Pretty Print Results
```bash
db.BookStore.find().pretty()
```

### Count Documents
```bash
db.BookStore.count()
```

### Find Specific Documents
```bash
# Find books by category
db.BookStore.find({"Category": "Programming Languages"})

# Find books with more than 300 pages
db.BookStore.find({"TotalPages": {$gt: 300}})

# Find books by author
db.BookStore.find({"Author": "Tanya"})
```

## Expected Output

When you run the application, you should see:
```
Pinged your deployment. You successfully connected to MongoDB!
Books added, check your new book collection!

Count of books having more than 200 pages is => 2
The book which title starts with 'Mongo' is => MongoDB Basics
Cheapest book is => C# Basics
Book with ISBN number 6779799933389898yu is => Entity Framework Basics
```

## Troubleshooting

### Common Issues

1. **Connection String Error**
   - Verify your MongoDB Atlas connection string
   - Check username and password
   - Ensure IP whitelist includes your current IP

2. **Build Warnings**
   - Nullable reference warnings are normal and don't affect functionality
   - To fix, add `required` modifier to properties or make them nullable

3. **Network Issues**
   - Check firewall settings
   - Verify internet connection
   - Ensure MongoDB Atlas cluster is running

### Debugging Tips

1. **Enable Detailed Logging**
   ```csharp
   // Add this before creating MongoClient
   settings.LoggingSettings = new LoggingSettings(LogLevel.Debug);
   ```

2. **Check Connection**
   ```bash
   # Test connection using MongoDB Compass or Shell
   mongosh "your-connection-string"
   ```

## Additional Commands

### Clean Build
```bash
dotnet clean
dotnet build
```

### Run in Release Mode
```bash
dotnet run --configuration Release
```

### Publish Application
```bash
dotnet publish -c Release -o ./publish
```

## Development Environment

- **IDE**: Visual Studio Code
- **Extensions**: C# Dev Kit, MongoDB for VS Code
- **Framework**: .NET 9.0
- **Database**: MongoDB Atlas

## Next Steps

- Add error handling for specific MongoDB exceptions
- Implement update and delete operations
- Add data validation
- Create a user interface
- Add logging functionality
- Implement async/await patterns for better performance

## License

This project is for educational purposes as part of EAD (Enterprise Application Development) coursework.
