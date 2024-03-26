using System;
using System.Collections.Generic;

namespace tut2
{
    public class ContainerShip
    {
        public List<Container> Containers { get; private set; }
        public double MaxSpeed { get; private set; }
        public int MaxContainerCount { get; private set; }
        public double MaxWeight { get; private set; }

        public ContainerShip(double maxSpeed, int maxContainerCount, double maxWeight)
        {
            Containers = new List<Container>();
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeight = maxWeight;
        }

        public void LoadContainer(Container container)
        {
            if (Containers.Count >= MaxContainerCount)
            {
                throw new InvalidOperationException("Cannot load container: ship has reached its maximum container capacity.");
            }
    
            double currentTotalWeight = GetCurrentTotalWeight();
            if ((currentTotalWeight + container.CargoMass + container.TareWeight) > MaxWeight)
            {
                throw new InvalidOperationException("Cannot load container: ship will exceed its maximum weight capacity.");
            }

            Containers.Add(container);
        }
        
        


        public bool RemoveContainer(Container container)
        {
            return Containers.Remove(container);
        }

        private double GetCurrentTotalWeight()
        {
            double totalWeight = 0.0;
            foreach (var cont in Containers)
            {
                totalWeight += cont.CargoMass + cont.TareWeight;
            }
            return totalWeight;
        }
        
    }
}