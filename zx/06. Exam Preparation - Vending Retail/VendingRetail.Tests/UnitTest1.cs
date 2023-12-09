using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructor()
        {
            CoffeeMat coffeeMat = new(100, 10);
            Assert.AreEqual(coffeeMat.WaterCapacity, 100);
            Assert.AreEqual(coffeeMat.ButtonsCount, 10);
        }
        [Test]
        public void TestFillWaterTankMethodIsFull()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.FillWaterTank();
            Assert.AreEqual(coffeeMat.FillWaterTank(), "Water tank is already full!");
        }
        [Test]
        public void TestFillWaterTankMethod()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            Assert.AreEqual("Water tank is filled with 100ml", coffeeMat.FillWaterTank());
        }
        [Test]
        public void TestExceedLimitOfDrinks()
        {
            CoffeeMat coffeeMat = new(100, 3);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.AddDrink("Sprite", 4.20);
            coffeeMat.AddDrink("Fanta", 2.20);
            Assert.IsFalse(coffeeMat.AddDrink("Derby", 2.20));
        }
        [Test]
        public void TestAddDrinkMethod()
        {
            CoffeeMat coffeeMat = new(100, 10);
            Assert.AreEqual(true, coffeeMat.AddDrink("Coca Cola", 3.20));
        }
        [Test]
        public void TestAddDrinkMethodReturnFalse()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            Assert.AreEqual(false, coffeeMat.AddDrink("Coca Cola", 3.20));
        }
        [Test]
        public void TestBuyDrinkMethodIsOutOfWater()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.BuyDrink("Coca cola");
            Assert.AreEqual("CoffeeMat is out of water!", coffeeMat.BuyDrink("Coca Cola"));
        }
        [Test]
        public void TestBuyDrinkMethodBuyCorrectly()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.FillWaterTank();
            Assert.AreEqual("Your bill is 3.20$", coffeeMat.BuyDrink("Coca Cola"));
        }
        [Test]
        public void TestBuyDrinkMethodHasNoThisProduct()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.FillWaterTank();
            Assert.AreEqual("Water is not available!", coffeeMat.BuyDrink("Water"));
        }
        [Test]
        public void Test_CollectIncomeMethod()
        {
            CoffeeMat coffeeMat = new(100, 10);
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.FillWaterTank();
            coffeeMat.BuyDrink("Coca Cola");
            Assert.AreEqual(3.2000000000000002d, coffeeMat.Income);
            Assert.AreEqual(3.2000000000000002d, coffeeMat.CollectIncome());
            Assert.AreEqual(0.0d, coffeeMat.Income);
        }
        [Test]
        public void Test_CheckWaterConsuming()
        {
            CoffeeMat coffeeMat = new(1000, 10);
            coffeeMat.FillWaterTank();
            coffeeMat.AddDrink("Coca Cola", 3.20);
            coffeeMat.AddDrink("Fanta", 2.20);
            coffeeMat.AddDrink("Waffle", 1.20);
            coffeeMat.AddDrink("Ice Cream", 4);
            coffeeMat.AddDrink("Gumm", 1.10);

            coffeeMat.BuyDrink("Waffle");
            coffeeMat.BuyDrink("Ice Cream");
            coffeeMat.BuyDrink("Fanta");
            coffeeMat.BuyDrink("Coca Cola");
            coffeeMat.BuyDrink("Gumm");
            Assert.AreEqual("Water tank is filled with 400ml", coffeeMat.FillWaterTank());
        }
    }
}