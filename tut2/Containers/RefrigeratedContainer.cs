using System;
using tut2;

public class RefrigeratedContainer : Container
{
    public RefrigeratedProductType ProductType { get; private set; }
    public double Temperature { get; private set; }

    public RefrigeratedContainer(double tareWeight, double height, double depth, double maxPayload, RefrigeratedProductType productType, double temperature)
        : base(SerialNumberGenerator.GenerateSerialNumber("C"), tareWeight, height, depth, maxPayload)
    {
        ProductType = productType;
        Temperature = ProductHelper.GetRequiredTemperature(productType);

        if (temperature < Temperature)
        {
            throw new ArgumentOutOfRangeException(nameof(temperature), $"The provided temperature is too low for {productType}. Required: {Temperature}, Provided: {temperature}.");
        }
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaxPayload)
        {
            throw new OverfillException($"Loading mass exceeds the allowable capacity for container {SerialNumber}.");
        }
        CargoMass = mass;
    }

    public override void EmptyCargo()
    {
        CargoMass = 0;
    }
}