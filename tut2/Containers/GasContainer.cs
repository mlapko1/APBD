using System;
using tut2;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(double tareWeight, double height, double depth, double maxPayload, double pressure)
        : base(SerialNumberGenerator.GenerateSerialNumber("G"), tareWeight, height, depth, maxPayload)
    {
        Pressure = pressure;
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaxPayload)
        {
            NotifyHazard($"Attempt to overload. Max allowable mass: {MaxPayload}, Attempted load mass: {mass}.");
            throw new OverfillException($"Loading mass exceeds the allowable capacity for container {SerialNumber}.");
        }
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass = MaxPayload * 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Notification for Gas Container {SerialNumber}: {message}");
    }
}