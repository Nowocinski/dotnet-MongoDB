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
            #region Delete
            var oneRecord = db.LoadRecordById<PersonModel>("Users", new Guid("69003fb4-c4fb-4570-b442-55db24963865"));
            db.DeleteRecord<PersonModel>("Users", oneRecord.Id);
            #endregion
            Console.ReadLine();
        }
    }
}