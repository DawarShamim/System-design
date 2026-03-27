using System;
using System.Collections.Generic;
namespace Composite
{
    interface ISmartDevice
    {
        void TurnOn();
        void TurnOff();
    }

    class Device : ISmartDevice
    {
        private string name;

        public Device(string name)
        { this.name = name; }

        public void TurnOn() { Console.WriteLine($"{name} turned ON"); }

        public void TurnOff() { Console.WriteLine($"{name} turned OFF"); }
    }

    class DeviceGroup : ISmartDevice
    {
        private string name;
        private List<ISmartDevice> devices = new List<ISmartDevice>();

        public DeviceGroup(string name) { this.name = name; }

        public void AddDevice(ISmartDevice device) { devices.Add(device); }

        public void TurnOn()
        {
            Console.WriteLine($"Turning ON group: {name}");
            foreach (var device in devices)
            { device.TurnOn(); }
        }

        public void TurnOff()
        {
            Console.WriteLine($"Turning OFF group: {name}");
            foreach (var device in devices)
            { device.TurnOff(); }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            DeviceGroup livingRoom = new DeviceGroup("livingRoom");
            Device TV = new Device("TV");
            Device Cleaner = new Device("Cleaner");
            Device CoffeeMaker = new Device("CoffeeMaker");

            livingRoom.AddDevice(TV);
            livingRoom.AddDevice(Cleaner);
            livingRoom.AddDevice(CoffeeMaker);
            livingRoom.TurnOff();
            TV.TurnOn();
        }
    }
}