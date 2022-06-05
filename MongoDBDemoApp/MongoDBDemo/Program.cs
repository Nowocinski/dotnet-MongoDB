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
            #region Load data
            //var records = db.LoadRecords<PersonModel>("Users");
            //records.ForEach(record => {
            //    Console.WriteLine($"{record.Id} {record.FirstName} {record.LastName}");
            //    if (record.PrimaryAddress != null)
            //    {
            //        Console.WriteLine($"{record.PrimaryAddress.City}");
            //    }
            //    Console.WriteLine();
            //    });
            #endregion
            #region Query by id
            //var oneRecord = db.LoadRecordById<PersonModel>("Users", new Guid("69003fb4-c4fb-4570-b442-55db24963865"));
            //Console.WriteLine($"{oneRecord.Id} {oneRecord.FirstName} {oneRecord.LastName}");
            //if (oneRecord.PrimaryAddress != null)
            //{
            //    Console.WriteLine($"{oneRecord.PrimaryAddress.City}");
            //}
            #endregion
            #region UpsertRecord (Replace or add record)
            //var oneRecord = db.LoadRecordById<PersonModel>("Users", new Guid("69003fb4-c4fb-4570-b442-55db24963865"));
            //oneRecord.DateOfBirth = new DateTime(1982, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            //db.UpsertRecord("Users", oneRecord.Id, oneRecord);
            #endregion
            #region delete
            var oneRecord = db.LoadRecordById<PersonModel>("Users", new Guid("69003fb4-c4fb-4570-b442-55db24963865"));
            db.DeleteRecord<PersonModel>("Users", oneRecord.Id);
            #endregion
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
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
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