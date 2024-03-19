namespace tut2
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        public LiquidContainer(double tareWeight, double height, double depth, double maxPayload, bool isHazardous) : base(SerialNumberGenerator.GenerateSerialNumber("L"), tareWeight, height, depth, maxPayload)
        {
            IsHazardous = isHazardous;
        }

        public bool IsHazardous { get; set; }

        public override void LoadCargo(double mass)
        {
            
        }

        public override void EmptyCargo()
        {
            
        }

        public void Notify()
        {
            // Message
        }
    }
}