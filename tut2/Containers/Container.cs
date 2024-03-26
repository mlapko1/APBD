public abstract class Container
{
    public string SerialNumber { get; private set; }
    public double CargoMass { get; protected set; }
    public double TareWeight { get; private set; }
    public double Height { get; private set; }
    public double Depth { get; private set; }
    public double MaxPayload { get; private set; }

    protected Container(string serialNumber, double tareWeight, double height, double depth, double maxPayload)
    {
        SerialNumber = serialNumber;
        TareWeight = tareWeight;
        Height = height;
        Depth = depth;
        MaxPayload = maxPayload;
    }

    public abstract void LoadCargo(double mass);
    public abstract void EmptyCargo();
}