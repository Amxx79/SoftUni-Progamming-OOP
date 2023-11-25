namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database database;

        [SetUp]
        public void Setup()
        {
            database = new Database(1, 2);
        }


        [Test]
        public void Database_ShouldCreateCorrectly()
        {
            Database dataBase = new(1, 2);
            int expectedResult = 2;
            int actualResult = dataBase.Count;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase(new int[] {1, 2, 3, 4, 5})]
        [TestCase(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16})]
        public void DataBaseShouldAddElementsCorrectly(int[] data)
        {
            Database dataBase = new(data);
            int[] actualResult = dataBase.Fetch();

            Assert.AreEqual(data, actualResult);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17})]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 20, 21, 22, 23})]
        public void DataBaseShould_ThrowException_WhenAddMore_Than16(int[] data)
        {
            InvalidOperationException ex
                = Assert.Throws<InvalidOperationException>(() 
                => database = new Database(data));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void DataBaseShould_CountCorrectly()
        {
            int actualResult = database.Count;
            int expectedResult = 2;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DataBaseShould_AddMethodShould_IncreaseCount()
        {
            int expectedResult = 3;
            database.Add(3);
            Assert.AreEqual(expectedResult, database.Count);
        }

        [TestCase(new int[] {1, 2, 3})]
        public void DataBaseShould_AddNumbersMethod_ElementsCorrectly(int[] data)
        {
            database = new Database();

            foreach (var element in data)
            {
                database.Add(element);
            }

            Assert.AreEqual(data, database.Fetch());
        }

        [Test]
        public void DataBase_ThrowException_WhenAddMore_Than16InAddMethod()
        {
            for (int i = 0; i < 14; i++)
            {
                database.Add(i);
            }

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(1));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void DataBaseShould_DecreaseCountIn_RemoveMethod()
        {
            int expectedResult = 1;
            database.Remove();
            
            Assert.AreEqual(expectedResult, database.Count);
        }

        [Test]
        public void DataBaseShould_RemoveElements_Correclty()
        {
            int[] expectedResult = { };
            database.Remove();
            database.Remove();

            int[] actualResult = database.Fetch();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DataBaseShould_ThrowEx_Correclty() 
        { 
            database = new();

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Remove());

            Assert.AreEqual("The collection is empty!", ex.Message);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7 })]
        public void DataBaseShould_FetchEementsCorrectly(int[] data)
        {
            database = new(data);
            int[] actualResult = database.Fetch();

            Assert.AreEqual(data, actualResult);
        }
    }
}
