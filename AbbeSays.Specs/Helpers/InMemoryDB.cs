using System;
using Simple.Data;
using TechTalk.SpecFlow;

namespace AbbeSays.Specs.Helpers
{
    [Binding]
    public class InMemoryDB
    {
        public static dynamic DB { get; private set; }
        private static InMemoryAdapter Adapter { get; set; }

        public const string TEST_CATEGORY_NAME_1 = "Category 1";
        public const string TEST_CATEGORY_NAME_2 = "Category 2";
        public static int Test_UserId1;
        public static int Test_UserId2;
        public static int Test_Category1Id;
        public static int Test_Category2Id;
        public static dynamic Test_Item1;

        private static void InitInMemoryTestDatabase()
        {
            Adapter = CreateAdapter();
            Database.UseMockAdapter(Adapter);
            DB = Database.Open();
            InitDatastructure();
        }

        private static InMemoryAdapter CreateAdapter()
        {
            var adapter = new InMemoryAdapter();

            adapter.SetKeyColumn("Users", "Id");
            adapter.SetAutoIncrementColumn("Users", "Id");

            adapter.SetKeyColumn("Kids", "Id");
            adapter.SetAutoIncrementColumn("Kids", "Id");

            adapter.SetKeyColumn("Quotes", "Id");
            adapter.SetAutoIncrementColumn("Quotes", "Id");

            adapter.ConfigureJoin("Users", "Id", "Kids", "Kids", "ParentId", "Parent");
            adapter.ConfigureJoin("Kids", "Id", "Quotes", "Quotes", "KidId", "Kid");


            return adapter;
        }

        private static void InitDatastructure()
        {
            DB.Users.Insert(Name: "Fake", Email: "Fake", UserName: "Fake");
            DB.Users.DeleteAll();

            DB.Kids.Insert(Name: "Fake", ParentId: -1, BirthDate: DateTime.Now, Bio: "Fake", Picture: null);
            DB.Kids.DeleteAll();


        }

        [BeforeTestRun]
        public static  void BeforeTestRun()
        {
            InitInMemoryTestDatabase();
        }
    }
}
