namespace tut2
{
    public class RefrigeratedContainer : Container

    {
        public RefrigeratedContainer(double tareWeight, double height, double depth, double maxPayload, string productType, double temperature) : base(SerialNumberGenerator.GenerateSerialNumber("R"), tareWeight, height, depth, maxPayload)
        {
            ProductType = productType;
            Temperature = temperature;
        }

        public string ProductType { get; set; }
    public double Temperature { get; set; }
    public override void LoadCargo(double mass)
    {
        throw new System.NotImplementedException();
    }

    public override void EmptyCargo()
    {
        throw new System.NotImplementedException();
    }
    }
}