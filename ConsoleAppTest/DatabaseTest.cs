using ConsoleAppTest.Class;
using OfferKayalarModel.DataModel;
using OnlineTalent.Core.Models;
using OnlineTalent.Database;
using System;

namespace ConsoleAppTest
{
    public class DatabaseTest 
    {
        private static readonly string connStr = "Server=NUMAN-PC;Database=TESTDB;User Id=sa;Password = as;";

        public static void AddNew()
        {
            ProductCategorys entity = new ProductCategorys
            {
               CategoryName="aa",
               CreatedBy=1,
               LevelOrderNo =1,
               CreatedDate= DateTime.Now
            };

            IConnectionFactory connectionFactory = new ConnectionMssql
            {
                ConnectionString = connStr
            };

            Repository<ProductCategorys> repository = new Repository<ProductCategorys>(connectionFactory);

            ReturnOutput returnOutput = new ReturnOutput();
            repository.Add(entity, ref returnOutput);
        }

        public static void Add()
        {
            Urun urun = new Urun {
                Description = "Muz",
                Price = 10M,
                CreateDate = DateTime.Now
            };

            IConnectionFactory connectionFactory = new ConnectionMssql
            {
                ConnectionString = connStr
            };

            Repository<Urun> repository = new Repository<Urun>(connectionFactory);

            ReturnOutput returnOutput = new ReturnOutput();
            repository.Add(urun,ref returnOutput);
        }

        public static void FindById(int id)
        {
                IConnectionFactory connectionFactory = new ConnectionMssql
                {
                    ConnectionString = connStr
                };

                Repository<Urun> repository = new Repository<Urun>(connectionFactory);

                ReturnOutput returnOutput = new ReturnOutput();
                var result = repository.FindById(2, ref returnOutput);

            int cnt = 1;
        }
    }
}
