using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace MongoDBDemo
{
    class Program
    {
        static void Main()
        {
            MongoDBCRUD db = new MongoDBCRUD("AddressBook");
            db.InsertRecord("Users", new PersonModel { FirstName = "Patryk", LastName = "Tomczak" });
            Console.ReadLine();
        }
    }

    public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
    }
}