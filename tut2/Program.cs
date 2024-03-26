using System;
using System.Collections.Generic;
using System.Linq;

namespace tut2
{
    internal class Program
    {
        private static List<ContainerShip> ships = new List<ContainerShip>();
        private static List<Container> containers = new List<Container>();

        public static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\n--- Container Ship Management System ---");
                Console.WriteLine("1. Add a container ship");
                Console.WriteLine("2. Add a container");
                Console.WriteLine("3. Load a container onto a ship");
                Console.WriteLine("4. Remove a container from a ship");
                Console.WriteLine("5. Display ship information");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        AddContainerShip();
                        break;
                    case "2":
                        AddContainer();
                        break;
                    case "3":
                        LoadContainerOntoShip();
                        break;
                    case "4":
                        RemoveContainerFromShip();
                        break;
                    case "5":
                        DisplayShipInformation();
                        break;
                    case "6":
                        isRunning = false;
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
        

        private static void AddContainerShip()
        {
            Console.WriteLine("Enter maximum speed (knots): ");
            double maxSpeed = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter maximum container count: ");
            int maxContainerCount = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter maximum weight capacity (tons): ");
            double maxWeight = Convert.ToDouble(Console.ReadLine());
            
            ContainerShip newShip = new ContainerShip(maxSpeed, maxContainerCount, maxWeight);
            
            ships.Add(newShip);

            Console.WriteLine("Ship added successfully.");
        }

        private static void AddContainer()
        {
            Console.WriteLine("Enter container type (G for Gas, L for Liquid, C for Refrigerated): ");
            string type = Console.ReadLine().ToUpper();
            Console.WriteLine("Enter tare weight: ");
            double tareWeight = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter height: ");
            double height = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter depth: ");
            double depth = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Enter max payload: ");
            double maxPayload = Convert.ToDouble(Console.ReadLine());
            
            Container newContainer = null;

            switch (type)
            {
                case "G":
                    Console.WriteLine("Enter pressure: ");
                    double pressure = Convert.ToDouble(Console.ReadLine());
                    newContainer = new GasContainer(tareWeight, height, depth, maxPayload, pressure);
                    break;
                case "L":
                    Console.WriteLine("Does it contain hazardous material? (yes/no): ");
                    bool hazardous = Console.ReadLine().Trim().ToLower() == "yes";
                    newContainer = new LiquidContainer(tareWeight, height, depth, maxPayload, hazardous);
                    break;
                case "C":
                    Console.WriteLine("Enter product type (Bananas, Milk, etc.): ");
                    string productTypeStr = Console.ReadLine();
                    RefrigeratedProductType productType;
                    Enum.TryParse(productTypeStr, out productType);
                    Console.WriteLine("Enter temperature: ");
                    double temperature = Convert.ToDouble(Console.ReadLine());
                    newContainer =
                        new RefrigeratedContainer(tareWeight, height, depth, maxPayload, productType, temperature);
                    break;
                default:
                    Console.WriteLine("Invalid container type.");
                    break;
            }
            
            containers.Add(newContainer);

            Console.WriteLine("Container added successfully.");

        }

        private static void LoadContainerOntoShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No ships available to load. Please add a ship first.");
                return;
            }

            if (containers.Count == 0)
            {
                Console.WriteLine("No containers available to load. Please add a container first.");
                return;
            }

            Console.WriteLine("Choose a ship to load the container onto:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Ship with max speed {ships[i].MaxSpeed} knots, can carry {ships[i].MaxContainerCount} containers, max weight {ships[i].MaxWeight} tons.");
            }

            Console.Write("Select a ship by number: ");
            int shipIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (shipIndex < 0 || shipIndex >= ships.Count)
            {
                Console.WriteLine("Invalid ship selection.");
                return;
            }

            Console.WriteLine("Choose a container to load:");
            for (int i = 0; i < containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Container with serial number {containers[i].SerialNumber}, current cargo mass {containers[i].CargoMass} kg.");
            }

            Console.Write("Select a container by number: ");
            int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (containerIndex < 0 || containerIndex >= containers.Count)
            {
                Console.WriteLine("Invalid container selection.");
                return;
            }

            try
            {
                ships[shipIndex].LoadContainer(containers[containerIndex]);
                Console.WriteLine("Container loaded onto ship successfully.");
                containers.RemoveAt(containerIndex);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static void RemoveContainerFromShip()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No ships available.");
                return;
            }

            Console.WriteLine("Select the ship to remove a container from:");
            for (int i = 0; i < ships.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}: Ship with max speed {ships[i].MaxSpeed} knots and {ships[i].Containers.Count} containers.");
            }

            int shipIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (shipIndex < 0 || shipIndex >= ships.Count)
            {
                Console.WriteLine("Invalid ship selection.");
                return;
            }

            if (ships[shipIndex].Containers.Count == 0)
            {
                Console.WriteLine("This ship has no containers to remove.");
                return;
            }

            Console.WriteLine("Select the container to remove:");
            for (int i = 0; i < ships[shipIndex].Containers.Count; i++)
            {
                Console.WriteLine($"{i + 1}: Container {ships[shipIndex].Containers[i].SerialNumber}");
            }

            int containerIndex = Convert.ToInt32(Console.ReadLine()) - 1;

            if (containerIndex < 0 || containerIndex >= ships[shipIndex].Containers.Count)
            {
                Console.WriteLine("Invalid container selection.");
                return;
            }

            Container containerToRemove = ships[shipIndex].Containers[containerIndex];
            if (ships[shipIndex].RemoveContainer(containerToRemove))
            {
                Console.WriteLine("Container removed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove the container.");
            }
        }

        private static void DisplayShipInformation()
        {
            if (ships.Count == 0)
            {
                Console.WriteLine("No ships have been added yet.");
                return;
            }

            foreach (var ship in ships)
            {
                Console.WriteLine($"\nShip Details:");
                Console.WriteLine($"- Maximum Speed: {ship.MaxSpeed} knots");
                Console.WriteLine($"- Maximum Container Count: {ship.MaxContainerCount}");
                Console.WriteLine($"- Maximum Weight Capacity: {ship.MaxWeight} tons");
                Console.WriteLine($"- Current Container Count: {ship.Containers.Count}");

                double totalWeight = ship.Containers.Sum(c => c.CargoMass + c.TareWeight);
                Console.WriteLine($"- Total Current Weight: {totalWeight} tons");

                if (ship.Containers.Count > 0)
                {
                    Console.WriteLine("  Containers on Ship:");
                    foreach (var container in ship.Containers)
                    {
                        Console.WriteLine($"    - Serial Number: {container.SerialNumber}, Cargo Mass: {container.CargoMass} kg, Type: {container.GetType().Name.Replace("Container", "")}");
                    }
                }
                else
                {
                    Console.WriteLine("  No containers on this ship.");
                }
            }
        }
    }
}
