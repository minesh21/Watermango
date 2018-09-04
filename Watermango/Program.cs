using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Watermango.Classes;

namespace Watermango
{
    public class Program
    {
        public enum status { SUCCESS, IN_USE, NOT_FOUND, WAIT};
        public static void Main(string[] args)
        {
            // Create a device Object
            Device device = new Device();
            // Create controller unit Object
            ControllerUnit cu = new ControllerUnit();

            // Add plants to device and set a default id, time and watered
            device.Add(new Plant(1, DateTime.Now, false));
            device.Add(new Plant(2, DateTime.Now, false));
            device.Add(new Plant(3, DateTime.Now, false));
            device.Add(new Plant(4, DateTime.Now, false));
            device.Add(new Plant(5, DateTime.Now, false));

            // Set device for controller unit
            cu.SetDevice(device);
            
            string s = "";
            
            // Reads commands to be executed if invalid command will continue on
            while (s !=  "SHUTDOWN")
            {
                PrintMenu();
                s = Console.ReadLine();
                string[] commands = s.Split(' ');
                CheckCommand(commands, cu);
            }

        }

        /// <summary>
        /// Check which command has been executed:
        /// WATER <id>
        /// STATUS <id>
        /// SHUTDOWN
        /// </summary>
        /// <param name="args">array string that holds arguments</param>
        /// <param name="cu">Controller unit</param>
        private static void CheckCommand(string[] args, ControllerUnit cu)
        {
            // Checking if the arguments do not exceed 2 arguments
           Console.WriteLine("\n");
           if (args.Length > 2)
            {
                Console.WriteLine("Please enter a command from the prompt menu");
            }

           // Using case statements to determine which command entered
           switch (args[0])
            {
                case "WATER":
                    int id;
                    if (args.Length == 2 && int.TryParse(args[1], out id)) {
                        var state = cu.WaterPlant(id);
                        switch (state)
                        {
                            case (int)status.SUCCESS:
                                Console.WriteLine("Started watering plant {0}", id);
                                break;
                            case (int)status.IN_USE:
                                Console.WriteLine("Device is currently in use");
                                break;
                            case (int)status.NOT_FOUND:
                                Console.WriteLine("Cannot find plant {0}", id);
                                break;
                            case (int)status.WAIT:
                                Console.WriteLine("Must wait 30s to water Plant {0}", id);
                                break;
                            default:
                                break;
                        }
                    }
                    Console.WriteLine("\n");
                    break;
                case "STATUS":
                    if (args.Length == 2 && int.TryParse(args[1], out id))
                    {
                       cu.GetPlantStatus(id);
                    }
                    Console.WriteLine("\n");
                    break;
                default:
                    Console.WriteLine("Invalid Command");
                    Console.WriteLine("\n");
                    break;
            }
        }
       
        /// <summary>
        ///  Prints the prompt menu to console
        /// </summary>
        private static void PrintMenu()
        {
            Console.WriteLine("Welcome to Watermango Project v1.0.0");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("The following is a list of commands:");
            Console.WriteLine("------------------------------------");
            Console.WriteLine("1) WATER <id> <- Water Plant with id");
            Console.WriteLine("2) STATUS <id> <- View Status of Plant");
            Console.WriteLine("3) SHUTDOWN");
            Console.WriteLine("------------------------------------");
            Console.Write("Command: ");
        }
    }
}
