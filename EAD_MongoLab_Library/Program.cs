
using EAD_MongoLab_Library;
using MongoDB.Driver;
using MongoDB.Bson;
using DotNetEnv;

// Load environment variables from .env file
Env.Load();

//Establish mongoDB connection
var connectionUri = Environment.GetEnvironmentVariable("MONGODB_CONNECTION_URI") 
    ?? throw new InvalidOperationException("MONGODB_CONNECTION_URI environment variable is not set");

var settings = MongoClientSettings.FromConnectionString(connectionUri);
// Set the ServerApi field of the settings object to set the version of the Stable API on the client
settings.ServerApi = new ServerApi(ServerApiVersion.V1);
// Create a new client and connect to the server
var client = new MongoClient(settings);
// Send a ping to confirm a successful connection
try
{
    var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");

    //Create a book list
    List<BookStore> bookStores = new List<BookStore>
    {
        new BookStore
        {
            BookTitle = "MongoDB Basics",
            ISBN = "8767687689898yu",
            Author = "Tanya",
            Category = "NoSQL DBMS",
            Price = 456
        },
        new BookStore
        {
            BookTitle = "C# Basics",
            ISBN = "27758987689898yu",
            Author = "Tanvi",
            Category = "Programming Languages",
            TotalPages = 376,
            Price = 289
        },
        new BookStore
        {
            BookTitle = "SQL Server Basics",
            ISBN = "117675787689898yu",
            Author = "Tushar",
            Category = "RDBMS",
            TotalPages = 250,
            Price = 478
        },
        new BookStore
        {
            BookTitle = "Entity Framework Basics",
            ISBN = "6779799933389898yu",
            Author = "Somya",
            Category = "ORM tool",
            TotalPages = 175,
            Price = 289
        }
    };

    //Create database "LibraryDB" or retrieve if exists
    var mongoDb = client.GetDatabase("LibraryDB");
    //Create collection "BookStore" or retrieve if exists
    var collection = mongoDb.GetCollection<BookStore>("BookStore");

    //Insert books to the "BookStore" collection with existence check
    int insertedCount = 0;
    int skippedCount = 0;
    
    foreach (BookStore bookStore in bookStores)
    {
        var existingBook = collection.Find(b => b.ISBN == bookStore.ISBN).FirstOrDefault();
        if (existingBook == null)
        {
            collection.InsertOne(bookStore);
            insertedCount++;
            Console.WriteLine($"Inserted: {bookStore.BookTitle}");
        }
        else
        {
            skippedCount++;
            Console.WriteLine($"Skipped (already exists): {bookStore.BookTitle}");
        }
    }
    
    Console.WriteLine($"\nSummary: {insertedCount} books inserted, {skippedCount} books skipped.");
    Console.ReadLine();

    //Query data
    var bookCount = collection.AsQueryable().Where(b => b.TotalPages > 200);
    Console.WriteLine("\nCount of books having more than 200 pages is => " +
        bookCount.Count());
    var book = collection.AsQueryable().Where(b => b.BookTitle.StartsWith("Mongo"));
    Console.WriteLine("\nThe book which title starts with 'Mongo' is => " +
        book.First().BookTitle);
    var cheapestBook = collection.AsQueryable().OrderBy(b => b.Price).First();
    Console.WriteLine("\nCheapest book is => " + cheapestBook.BookTitle);
    var bookWithISBN = collection.AsQueryable().Single(b => b.ISBN == "6779799933389898yu");
    Console.WriteLine("\nBook with ISBN number 6779799933389898yu is => " +
        bookWithISBN.BookTitle);
    Console.ReadLine();

    // mongoDb.DropCollection("BookStore");
    // Console.ReadLine();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}




