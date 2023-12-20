namespace Television.Tests
{
    using System;
    using NUnit.Framework;
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Samsung", device.Brand);
            Assert.AreEqual(1000, device.Price);
            Assert.AreEqual(100, device.ScreenWidth);
            Assert.AreEqual(100, device.ScreenHeigth);
            Assert.AreEqual(0, device.CurrentChannel);
            Assert.AreEqual(13, device.Volume);
            Assert.AreEqual(false, device.IsMuted);
        }
        [Test]
        public void SwitchOnMethodProperly()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Cahnnel 0 - Volume 13 - Sound On", device.SwitchOn());
            Assert.AreEqual(false, device.IsMuted);
        }
        [Test]
        public void ChangeChannelMethodException()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.Throws<ArgumentException>(() => device.ChangeChannel(-1), "Invalid key!");
        }
        [Test]
        public void ChangeChannelMethodProperly()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual(5, device.ChangeChannel(5));
            Assert.AreEqual(5, device.CurrentChannel);
        }
        [Test]
        public void VolumeChangeMethodException()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Volume: 13", device.VolumeChange("Test", 10));
        }
        [Test]
        public void VolumeChangeMethodProperly()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Volume: 23", device.VolumeChange("UP", 10));
        }
        [Test]
        public void VolumeChangeMethodProperly150()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Volume: 100", device.VolumeChange("UP", 150));
        }
        [Test]
        public void VolumeChangeMethodProperlyMinus()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Volume: 3", device.VolumeChange("DOWN", 10));
        }
        [Test]
        public void VolumeChangeMethodProperlyMinus150()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual("Volume: 0", device.VolumeChange("DOWN", 150));
        }
        [Test]
        public void MuteDevice()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual(true, device.MuteDevice());
            device.MuteDevice();
            Assert.AreEqual(false, device.IsMuted);
        }
        [Test]
        public void ToString()
        {
            TelevisionDevice device = new TelevisionDevice("Samsung", 1000, 100, 100);
            Assert.AreEqual(device.ToString(), "TV Device: Samsung, Screen Resolution: 100x100, Price 1000$");
        }
    }
}