namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using NUnit.Framework.Internal.Commands;
    using System;
    using System.Globalization;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database database;

        [SetUp]
        public void SetUp()
        {
            Person first = new(1, "F");
            Person second = new(2, "S");
            Person third = new(3, "T");
            Person[] persons = { first, second, third };
            database = new(persons);
        }

        [Test]
        public void DataBaseCreate_ConstructorTest()
        {
            Assert.IsNotNull(database);
        }

        [Test]
        public void DataBaseCount_ShouldReturnCorrectly_PersonsCount()
        {
            int expectedCount = 3;
            Assert.AreEqual(expectedCount, database.Count);
        }
        [Test]
        public void DataBaseConstructor_AddRangeMethod_ShouldAddPersonCorrectly()
        {
            Person fourth = new(4, "Fourth");
            Person fifth = new(5, "Fifth");
            Person[] persons = new Person[] {fourth, fifth};
            database = new(persons);
        }
        [Test]
        public void DataBaseConstructor_AddRangeMethod_ShouldThrowException_WithMoreThan16()
        {
            Person first = new(1, "F");
            Person second = new(2, "S");
            Person third = new(3, "T");
            Person fourth = new(4, "Fo");
            Person fifth = new(5, "Fi");
            Person six = new(6, "Fe");
            Person seven = new(7, "Fia");
            Person eight = new(8, "Foue");
            Person nine = new(9, "Fiftfh");
            Person ten = new(10, "Fourtsdh");
            Person eleven = new(11, "Fiftgsdh");
            Person twelve = new(42, "Foufcrth");
            Person thirdteen = new(35, "Fifsfth");
            Person fourteen = new(455, "Fifeuth");
            Person fiftheen = new(53, "Fifxhth");
            Person sixteen = new(556, "Fifewth");
            Person seventeen = new(5234, "Figjfth");
            Person[] persons = new Person[] 
            {first, second, third, fourth, fifth, six, seven, eight, nine, ten, eleven
            , twelve, thirdteen, fourteen,fiftheen, sixteen, seventeen };

            ArgumentException ex = Assert.Throws<ArgumentException>(()
                => database = new(persons));

            Assert.AreEqual("Provided data length should be in range [0..16]!", ex.Message);
        }

        [Test]
        public void Database_AddMethodShould_AddPersonCorreclty()
        {
            Person person = new(4, "Fourth");
            database.Add(person);
            int expectedCount = 4;

            Assert.AreEqual(expectedCount, database.Count);
        }
        [Test]
        public void Database_AddMethodShould_ThrowExWhen_CountIs16()
        {
            Person first = new(1, "F");
            Person second = new(2, "S");
            Person third = new(3, "T");
            Person fourth = new(4, "Fo");
            Person fifth = new(5, "Fi");
            Person six = new(6, "Fe");
            Person seven = new(7, "Fia");
            Person eight = new(8, "Foue");
            Person nine = new(9, "Fiftfh");
            Person ten = new(10, "Fourtsdh");
            Person eleven = new(11, "Fiftgsdh");
            Person twelve = new(42, "Foufcrth");
            Person thirdteen = new(35, "Fifsfth");
            Person fourteen = new(455, "Fifeuth");
            Person fiftheen = new(53, "Fifxhth");
            Person sixteen = new(556, "Fifewth");

            Person sevenTeen = new(214, "SevenTeen");
            Person[] persons = new Person[]
            {first, second, third, fourth, fifth, six, seven, eight, nine, ten, eleven
            , twelve, thirdteen, fourteen,fiftheen, sixteen };
            database = new(persons);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()
                => database.Add(sevenTeen));

            Assert.AreEqual("Array's capacity must be exactly 16 integers!", ex.Message);
        }

        [Test]
        public void Database_AddMethodShould_ThrowExWhen_NameContainsPersons()
        {
            Person personFourth = new(4, "F");

            //throw new InvalidOperationException("There is already user with this username!");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
                => database.Add(personFourth));

            Assert.AreEqual("There is already user with this username!", exception.Message);
        }
        [Test]
        public void Database_AddMethodShould_ThrowExWhen_ID_ContainsPersons()
        {
            Person personFourth = new(3, "Q");

            //throw new InvalidOperationException("There is already user with this Id!");

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
                => database.Add(personFourth));

            Assert.AreEqual("There is already user with this Id!", exception.Message);
        }

        [Test]
        public void Database_ShouldRemovePeopleCorrectly()
        {
            int expectedCount = 2;
            database.Remove();

            Assert.AreEqual(expectedCount, database.Count);
        }

        [Test]
        public void Database_ShouldThrowException_WhenRemovePerson_AndPersonIs0()
        {
            database.Remove();
            database.Remove();
            database.Remove();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void Database_FindPersonMethod_ShouldThrownExWhen_ParameterIsNull()
        {
            try
            {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(()
                => database.FindByUsername(""));
            }
            catch(ArgumentNullException ex)
            {
                Assert.AreEqual("Username parameter is null!", ex.Message);
            }
        }

        [Test]
        public void Database_ShouldThrowException_WhenPersonDontContain()
        {
            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
            => database.FindByUsername("Johny"));

            Assert.AreEqual("No user is present by this username!", exception.Message);
        }

        [Test]
        public void Database_ShouldFindPerson_ByName()
        {
            string expectedResult = "T";
            string actualResult = database.FindByUsername("T").UserName;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DatabaseFindByUsernameMethodShouldBeCaseSensitive()
        {
            string expectedResult = "t";
            string actualResult = database.FindByUsername("T").UserName;

            Assert.AreNotEqual(expectedResult, actualResult);
        }

        [Test]
        public void Database_ShouldThrowException_IfIDIsUnder0()
        {
            try
            {
                ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(()
                    => database.FindById(-1));
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Id should be a positive number!", ex.Message);
            }
        }
        [Test]
        public void Database_ShouldThrowException_IfPersonsDontContainIDPerson()
        {
            try
            {
                InvalidOperationException exception = Assert.Throws<InvalidOperationException>(()
                    => database.FindById(9));
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No user is present by this ID!", ex.Message);
            }
        }

        [Test]
        public void Database_ShouldFindPersonByID()
        {
            string expectedResult = "T";
            string name = database.FindById(3).UserName;
            Assert.AreEqual(expectedResult, name);
        }

        [Test]
        public void PersonCanGetUserNameCorrectly()
        {
            Person person = database.FindById(3);
            Assert.IsNotNull(person.UserName);
        }
        [Test]
        public void PersonCanSetUserNameCorrectly()
        {
            string expectedResult = "Pesho";
            Person person = new(3, "Pesho");
            Assert.AreEqual(expectedResult, person.UserName);
        }

        [Test]
        public void PersonCanGetID_Correctly()
        {
            Person person = database.FindById(3);
            Assert.IsNotNull(person.Id);
        }
        [Test]
        public void PersonCanSetId_Correctly()
        {
            int expectedResult = 5;
            Person person = new(5, "Pesho");
            Assert.AreEqual(expectedResult, person.Id);
        }
    }
}