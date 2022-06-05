using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBDemo
{
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

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collection = this.db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).First();
        }

        // Replace or add record
        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = this.db.GetCollection<T>(table);
            // https://stackoverflow.com/questions/67158337/c-sharp-mongodb-upsert-bsonvalueguid-is-obsolete-use-the-bsonbinarydata-c
            BsonBinaryData binData = new BsonBinaryData(id, GuidRepresentation.Standard);
            var result = collection.ReplaceOne(new BsonDocument("_id", binData), record, new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = this.db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}