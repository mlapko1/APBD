namespace tut2
{
    public class GasContainer : Container, IHazardNotifier
    {
        public GasContainer(double tareWeight, double height, double depth, double maxPayload, double pressure) : base(SerialNumberGenerator.GenerateSerialNumber("G"), tareWeight, height, depth, maxPayload)
        {
            Pressure = pressure;
        }

        public double Pressure { get; set; }
        public override void LoadCargo(double mass)
        {
            throw new System.NotImplementedException();
        }

        public override void EmptyCargo()
        {
            throw new System.NotImplementedException();
        }

        public void Notify()
        {
            throw new System.NotImplementedException();
        }
    }
}