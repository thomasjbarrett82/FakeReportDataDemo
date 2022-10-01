using Dapper;
using Dapper.Contrib.Extensions;
using FakeReportDataDemo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace FakeReportDataDemo {
    internal class Program {
        internal const string sqlConnString = @"Data Source=10.0.0.12;Initial Catalog=FakeReportDataDemo;User ID=FakeReportDataDemo;Password=FakeReportDataDemo;";

        internal const string mdbConnString = @"mongodb://10.0.0.12:27017";
        internal const string mdbName = "fakeReportDataDemo";
        internal const string mCollection = "transactions";

        static void Main() {
            try {
                using (SqlConnection db = new SqlConnection(sqlConnString)) { 
                    var mdbClient = new MongoClient(mdbConnString);
                    var mdb = mdbClient.GetDatabase(mdbName);
                    var mTransactions = mdb.GetCollection<BsonDocument>(mCollection);

                    db.Open();
                    db.Execute("truncate table dbo.FactTransaction");
                    db.Execute("delete from dbo.DimCustomer");
                    db.Execute("delete from dbo.DimProduct");

                    db.Execute($@"insert into dbo.DimCustomer (Id, Name) values
                        ({Customers.BloomMarketing.Id}, '{Customers.BloomMarketing.Name}'),
                        ({Customers.GourmetSandwiches.Id}, '{Customers.GourmetSandwiches.Name}'),
                        ({Customers.HeartyPancake.Id}, '{Customers.HeartyPancake.Name}'),
                        ({Customers.HouseBrush.Id}, '{Customers.HouseBrush.Name}'),
                        ({Customers.MyVegetarianDinner.Id}, '{Customers.MyVegetarianDinner.Name}'),
                        ({Customers.OfficeTile.Id}, '{Customers.OfficeTile.Name}'),
                        ({Customers.Raven.Id}, '{Customers.Raven.Name}'),
                        ({Customers.TheGlowUp.Id}, '{Customers.TheGlowUp.Name}'),
                        ({Customers.TheLoop.Id}, '{Customers.TheLoop.Name}'),
                        ({Customers.UrbanPhilosophy.Id}, '{Customers.UrbanPhilosophy.Name}')");

                    db.Execute($@"insert into dbo.DimProduct (Id, Name) values
                        ({Products.Accuprint.Id}, '{Products.Accuprint.Name}'),
                        ({Products.BaconSticks.Id}, '{Products.BaconSticks.Name}'),
                        ({Products.BrigadierCoffee.Id}, '{Products.BrigadierCoffee.Name}'),
                        ({Products.CubicleMachine.Id}, '{Products.CubicleMachine.Name}'),
                        ({Products.FeverBlaster.Id}, '{Products.FeverBlaster.Name}'),
                        ({Products.GrooveKleen.Id}, '{Products.GrooveKleen.Name}'),
                        ({Products.Isoflux.Id}, '{Products.Isoflux.Name}'),
                        ({Products.PanSol.Id}, '{Products.PanSol.Name}'),
                        ({Products.RedThunder.Id}, '{Products.RedThunder.Name}'),
                        ({Products.StoreYourOats.Id}, '{Products.StoreYourOats.Name}')");

                    mTransactions.DeleteMany(new BsonDocument());

                    // loop for 1 billion transactions (10 hours)
                    var transactionCount = 0;
                    var transactionInterval = 10000;
                    int customerId;
                    int productId;
                    List<Transaction> transactionList;
                    List<BsonDocument> mTransactionList;
                    var rand = new Random();
                    var startDate = new DateTime(1980, 1, 1);
                    var dateRange = (DateTime.Today - startDate).Days;
                    int qtySold;
                    decimal unitPrice;
                    DataTable table;

                    while (transactionCount < 1000000000) {

                        // create transactions
                        transactionList = new List<Transaction>();
                        for (int i = 0; i < transactionInterval; i++) {
                            customerId = rand.Next(1,10);
                            productId = rand.Next(1, 10);
                            qtySold = rand.Next(1, 100);
                            unitPrice = rand.Next(1, 100);
                            var t = new Transaction { 
                                Id = i + transactionCount,
                                CustomerId = customerId,
                                ProductId = productId,
                                DateTimeSold = startDate.AddDays(rand.Next(dateRange)),
                                QuantitySold = qtySold,
                                PricePerUnit = unitPrice,
                                TotalPrice = qtySold * unitPrice
                            };
                            switch (customerId) {
                                case 1:
                                    t.Customer = Customers.BloomMarketing;
                                    break;
                                case 2:
                                    t.Customer = Customers.HeartyPancake;
                                    break;
                                case 3:
                                    t.Customer = Customers.TheGlowUp;
                                    break;
                                case 4:
                                    t.Customer = Customers.TheLoop;
                                    break;
                                case 5:
                                    t.Customer = Customers.MyVegetarianDinner;
                                    break;
                                case 6:
                                    t.Customer = Customers.GourmetSandwiches;
                                    break;
                                case 7:
                                    t.Customer = Customers.OfficeTile;
                                    break;
                                case 8:
                                    t.Customer = Customers.HouseBrush;
                                    break;
                                case 9:
                                    t.Customer = Customers.Raven;
                                    break;
                                case 10:
                                    t.Customer = Customers.UrbanPhilosophy;
                                    break;
                            }
                            switch (productId) {
                                case 1:
                                    t.Product = Products.Accuprint;
                                    break;
                                case 2:
                                    t.Product = Products.BaconSticks;
                                    break;
                                case 3:
                                    t.Product = Products.BrigadierCoffee;
                                    break;
                                case 4:
                                    t.Product = Products.CubicleMachine;
                                    break;
                                case 5:
                                    t.Product = Products.FeverBlaster;
                                    break;
                                case 6:
                                    t.Product = Products.GrooveKleen;
                                    break;
                                case 7:
                                    t.Product = Products.Isoflux;
                                    break;
                                case 8:
                                    t.Product = Products.PanSol;
                                    break;
                                case 9:
                                    t.Product = Products.RedThunder;
                                    break;
                                case 10:
                                    t.Product = Products.StoreYourOats;
                                    break;
                            }
                            transactionList.Add(t);
                        }

                        // insert into SQL
                        using (var bulkCopy = new SqlBulkCopy(db)) {
                            bulkCopy.BatchSize = transactionInterval;
                            bulkCopy.DestinationTableName = "FactTransaction";

                            table = transactionList.ToDataTable();

                            bulkCopy.ColumnMappings.Add(nameof(Transaction.Id), nameof(Transaction.Id));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.CustomerId), nameof(Transaction.CustomerId));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.ProductId), nameof(Transaction.ProductId));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.DateTimeSold), nameof(Transaction.DateTimeSold));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.QuantitySold), nameof(Transaction.QuantitySold));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.PricePerUnit), nameof(Transaction.PricePerUnit));
                            bulkCopy.ColumnMappings.Add(nameof(Transaction.TotalPrice), nameof(Transaction.TotalPrice));

                            bulkCopy.WriteToServer(table);
                        }

                        // insert into MongoDB
                        mTransactionList = new List<BsonDocument>();
                        mTransactionList.AddRange(transactionList.Select(t => t.ToBsonDocument()));
                        mTransactions.InsertMany(mTransactionList);

                        transactionCount += transactionInterval;
                        Console.WriteLine($"Transactions created: {transactionCount}");
                    }
                }
            }
            catch (Exception e) {
                PrintException(e);
            }

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void PrintException(Exception? ex) {
            if (ex != null) {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }
    }
}