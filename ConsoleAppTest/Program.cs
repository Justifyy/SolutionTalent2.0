using HowManyCountModel.DataModel;
using System;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // 
          CreateDynamicPage.CreateHTMLPageFromModelNew<Customer>(new Customer());

       //   TestLog();

           //  TestDB();
            Console.WriteLine("İşlem Tamamlandı.");
            Console.ReadLine();
        }

        public static void TestDB()
        {
            // DatabaseTest.AddNew();
    
            DatabaseTest.FindById(2);
        }

        public static void TestLog()
        {
            NLogTest.LogTest();
        }
    }
}
