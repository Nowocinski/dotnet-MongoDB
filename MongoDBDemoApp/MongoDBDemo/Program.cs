using MongoDB.Driver;

namespace MongoDBDemo
{
    class Program
    {
        static void Main()
        {
            Console.ReadLine();
        }
    }

    public class MongoDBCRUD
    {
        private IMongoDatabase db;

        public MongoDBCRUD(string database)
        {
            var client = new MongoClient();
            this.db = client.GetDatabase(database);
        }
    }
}