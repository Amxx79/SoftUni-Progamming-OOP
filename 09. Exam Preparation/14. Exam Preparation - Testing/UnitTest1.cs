namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Linq;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructor()
        {
            UniversityLibrary uniLibrary = new();
            Assert.IsNotNull(uniLibrary);
        }
        [Test]
        public void AddTextBookToLibraryMethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            Assert.AreEqual(1, uniLibrary.Catalogue.Count);
            string findBook = uniLibrary.Catalogue[0].Author;
            Assert.AreEqual("Oruel", findBook);
        }
        [Test]
        public void AddTextBookToLibrary_InventoryNumTest_MethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            TextBook book2 = new TextBook("1984", "Oruel", "Horror");
            TextBook book3 = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            uniLibrary.AddTextBookToLibrary(book2);
            uniLibrary.AddTextBookToLibrary(book3);
            Assert.AreEqual(1, book.InventoryNumber);
            Assert.AreEqual(2, book2.InventoryNumber);
            Assert.AreEqual(3, book3.InventoryNumber);
        }
        [Test]
        public void AddTextBookToLibrary_ToStringBook_MethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            TextBook book2 = new TextBook("1984", "Oruel", "Horror");
            TextBook book3 = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Book: 1984 - 2");
            sb.AppendLine($"Category: Horror");
            sb.AppendLine($"Author: Oruel");
            string result = sb.ToString().Trim();
            Assert.AreEqual(result, uniLibrary.AddTextBookToLibrary(book2));
        }
        [Test]
        public void TestCatalogueMethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1980", "Oruel", "Horror");
            TextBook book2 = new TextBook("1981", "Oruel;", "Horror");
            TextBook book3 = new TextBook("1982", "Oruel;;", "Horror");
            TextBook book4 = new TextBook("1983", "Oruel;;;", "Horror");
            TextBook book5 = new TextBook("1984", "Oruel;;;;", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            uniLibrary.AddTextBookToLibrary(book2);
            uniLibrary.AddTextBookToLibrary(book3);
            uniLibrary.AddTextBookToLibrary(book4);
            uniLibrary.AddTextBookToLibrary(book5);
            Assert.AreEqual(5, uniLibrary.Catalogue.Count);
            string findBook = uniLibrary.Catalogue[0].Author;
            Assert.AreEqual("Oruel", findBook);
        }
        [Test]
        public void LoanTextBookMethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            Assert.AreEqual("1984 loaned to Azz.", uniLibrary.LoanTextBook(1, "Azz"));
        }
        [Test]
        public void LoanTextBook_AnotherStudent_MethodTesting()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            Assert.AreEqual("1984 loaned to Azz.", uniLibrary.LoanTextBook(1, "Azz"));
            Assert.AreEqual("1984 loaned to Another.", uniLibrary.LoanTextBook(1, "Another"));
        }
        [Test]
        public void LoanTextBookMethodTesting_CannotLoanThisBook()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            uniLibrary.LoanTextBook(1, "Azz");
            Assert.AreEqual("Azz still hasn't returned 1984!", uniLibrary.LoanTextBook(1, "Azz"));
        }
        [Test]
        public void ReturnTextBookMethodTesting_CannotLoanThisBook()
        {
            UniversityLibrary uniLibrary = new();
            TextBook book = new TextBook("1984", "Oruel", "Horror");
            uniLibrary.AddTextBookToLibrary(book);
            uniLibrary.LoanTextBook(1, "Azz");
            Assert.AreEqual("1984 is returned to the library.", uniLibrary.ReturnTextBook(1));
            Assert.AreEqual(string.Empty, uniLibrary.Catalogue[0].Holder);
        }
    }
}