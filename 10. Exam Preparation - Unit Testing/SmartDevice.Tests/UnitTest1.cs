namespace SmartDevice.Tests
{
    using NUnit.Framework;
    using System;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConstructorWorkCorrectly()
        {
            Device device = new(1000);

            int expected = 1000;

            Assert.AreEqual(expected, device.MemoryCapacity);
        }
        [Test]
        public void TestTakePhotoMethodWorkCorrectlyReturnTrue()
        {
            Device device = new(1000);

            bool expected = true;

            Assert.AreEqual(expected, device.TakePhoto(100));
        }
        [Test]
        public void TestTakePhotoMethodWorkCorrectlyReturnFalse()
        {
            Device device = new(1000);

            bool expected = false;

            Assert.AreEqual(expected, device.TakePhoto(100_000));
        }
        [Test]
        public void TestInstallAppMethod_WorkCorrectly()
        {
            Device device = new(1000);
            int expected = 1;
            device.InstallApp("TomAndJerry", 100);
            Assert.AreEqual(expected, device.Applications.Count);
        }
        [Test]
        public void TestInstallAppMethod_WorkCorrectly_ReturnCorrectMessage()
        {
            Device device = new(1000);
            string expected = "TomAndJerry is installed successfully. Run application?";
            device.InstallApp("TomAndJerry", 100);
            Assert.AreEqual(expected, device.InstallApp("TomAndJerry", 100));
        }
        [Test]
        public void TestInstallAppMethod_ThrowException()
        {
            Device device = new(100);
            Assert.Throws<InvalidOperationException>(() => device.InstallApp("TomAndJerry", 1000), "Not enough available memory to install the app.");
        }
        [Test]
        public void TestFormatDeviceMethod_WorkCorrectly()
        {
            Device device = new(100);
            int expected = 0;
            device.TakePhoto(10);
            device.FormatDevice();
            Assert.AreEqual(expected, device.Photos);
        }
        [Test]
        public void TestGetDeviceMethod_WorkCorrectly()
        {
            //"Memory Capacity: {this.MemoryCapacity} MB, Available Memory: {this.AvailableMemory} MB");
            //"Photos Count: {this.Photos}");
            //"Applications Installed: {string.Join(", ", this.Applications)}");

            Device device = new(1000);
            device.TakePhoto(100);
            device.InstallApp("TomAndJerry", 100);
            StringBuilder sb = new();
            sb.AppendLine($"Memory Capacity: 1000 MB, Available Memory: 800 MB");
            sb.AppendLine($"Photos Count: 1");
            sb.Append($"Applications Installed: TomAndJerry");
            Assert.AreEqual(sb.ToString(), device.GetDeviceStatus());
        }
    }
}