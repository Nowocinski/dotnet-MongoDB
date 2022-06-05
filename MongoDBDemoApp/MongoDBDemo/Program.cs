using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDBDemo
{
    class Program
    {
        static void Main()
        {
            MongoDBCRUD db = new MongoDBCRUD("AddressBook");
            #region Insert data
            //PersonModel person = new PersonModel
            //{
            //    FirstName = "Joe",
            //    LastName = "Smith",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 Oak Street",
            //        City = "Scranton",
            //        State = "PA",
            //        ZipCode = "18512"
            //    }
            //};
            //db.InsertRecord("Users", person);
            #endregion
            var records = db.LoadRecords<PersonModel>("Users");
            records.ForEach(record => {
                Console.WriteLine($"{record.Id} {record.FirstName} {record.LastName}");
                if (record.PrimaryAddress != null)
                {
                    Console.WriteLine($"{record.PrimaryAddress.City}");
                }
                Console.WriteLine();
                });
            Console.ReadLine();
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }
    }

    public class AddressModel
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class MongoDBCRUD
    {
        private IMongoDatabase db;

        public MongoDBCRUD(string database)
        {
            var client = new MongoClient();
            this.db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = this.db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = this.db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }
    }
}