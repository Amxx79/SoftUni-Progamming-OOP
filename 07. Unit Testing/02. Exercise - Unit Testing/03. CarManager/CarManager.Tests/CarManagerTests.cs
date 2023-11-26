namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car car;

        [Test]
        public void CarShouldBeCreatedWithZeroFuelAmount()
        {
            car = new("Fiat", "Grande", 3, 33);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [Test]
        public void CarCreate_TestCtor()
        {
            Car car = new("Fiat", "Grande", 9, 60);

            Assert.AreEqual("Fiat", car.Make);
            Assert.AreEqual("Grande", car.Model);
            Assert.AreEqual(9, car.FuelConsumption);
            Assert.AreEqual(60, car.FuelCapacity);
        }

        [Test]
        public void Car_MakeSetter_ThrowExceptionWhenString_IsEmpty() 
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                ()=> car = new("", "Grande", 9, 60));

            Assert.AreEqual("Make cannot be null or empty!", ex.Message);
        }
        [Test]
        public void Car_ModelSetter_ThrowExceptionWhenString_IsEmpty()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => car = new("Fiat", "", 9, 60));

            Assert.AreEqual("Model cannot be null or empty!", ex.Message);
        }
        [Test]
        public void Car_FuelConsumption_Setter_ThrowExceptionWhenString_ValueIsBelow0()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => car = new("Fiat", "Grande", -1, 60));

            Assert.AreEqual("Fuel consumption cannot be zero or negative!", ex.Message);
            //throw new ArgumentException("Fuel consumption cannot be zero or negative!");
        }
        [Test]
        public void Car_FuelAmount_SetterOnRefuel_ThrowExceptionWhenString_ValueIsBelow0()
        {
            Car car = new("Fiat", "Grande", 4, 77);
            Assert.Throws<InvalidOperationException>(()
                => car.Drive(distance: 12), "Fuel amount cannot be negative!");
        }
        [Test]
        public void Car_FuelCapacity_Setter_ThrowExceptionWhenString_ValueIsBelow0()
        {
            Assert.Throws<ArgumentException>(()
                => car = new("Fiat", "Grande", 4, 0)
                , "Fuel capacity cannot be zero or negative!");
        }
        [Test]
        public void Car_RefuelMethod_ThrowExceptionWhenString_ValueIsBelowOrEqual0()
        {
            car = new("Fiat", "Grande", 4, 33);

            Assert.Throws<ArgumentException>(()
                => car.Refuel(0), "Fuel amount cannot be zero or negative!");
        }
        [Test]
        public void Car_IfRefuelMethodRefuelWithMoreThanCapacity_IsSetToCapacityMaximum()
        {
            car = new("Fiat", "Grande", 4, 60);
            int expectedResult = 60;

            car.Refuel(100);
            Assert.AreEqual(expectedResult, car.FuelAmount);
        }
        [Test]
        public void CarRefuelMethod_ShouldIncreaseFuelAmount()
        {
            int expectedResult = 10;
            car = new("Fiat", "Grande", 9, 40);
            car.Refuel(10);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }
        [Test]
        public void CarDriveMethod_ShouldDecreaseFuelAmount()
        {
            double expectedResult = 9.25;
            car = new("Fiat", "Grande", 7.5, 50);
            car.Refuel(10);
            car.Drive(10);
            double actualResult = car.FuelAmount;

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CarDriveMethod_ThrowExceptionIf_NeededFuelIsMoreThanAvaiable()
        {
            car = new("Fiat", "Grande", 4, 60);

            //throw new InvalidOperationException("You don't have enough fuel to drive!");

            Assert.Throws<InvalidOperationException>(()
                => car.Drive(1000), "You don't have enough fuel to drive!");
        }
    }
}