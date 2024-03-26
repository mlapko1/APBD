using System;
using tut2;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool ContainsHazardousMaterial { get; private set; }

    public LiquidContainer(double tareWeight, double height, double depth, double maxPayload, bool containsHazardousMaterial)
        : base(SerialNumberGenerator.GenerateSerialNumber("L"), tareWeight, height, depth, maxPayload)
    {
        ContainsHazardousMaterial = containsHazardousMaterial;
    }

    public override void LoadCargo(double mass)
    {
        double maxCapacity = ContainsHazardousMaterial ? MaxPayload * 0.5 : MaxPayload * 0.9;
        if (mass > maxCapacity)
        {
            NotifyHazard($"Attempt to overload. Max allowable mass: {maxCapacity}, Attempted load mass: {mass}.");
            throw new OverfillException($"Loading mass exceeds the allowable capacity for container {SerialNumber}.");
        }
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass = 0;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard Notification for Liquid Container {SerialNumber}: {message}");
    }
}