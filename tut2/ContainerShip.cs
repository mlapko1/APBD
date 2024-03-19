using System.Collections.Generic;

namespace tut2
{
    public class ContainerShip
    {
        public List<Container> Containers { get; private set; }
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; }
    }
}