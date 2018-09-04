using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermango.Classes
{
    /*
     *--------------------------------------------------------- 
     * Device Class
     *--------------------------------------------------------
     * @description: 
     *          Stores plants in List<Plant> in order to keep
     *          track of plant status'
     *--------------------------------------------------------
     */
    public class Device
    {
        private List<Plant> m_Plants = new List<Plant>();
        private string m_Status = "";
        
        /*
         * Constructor method to initialize Device class
         * and sets status of Device to "READY"
         * 
         */
        public Device()
        {
            m_Status = "READY";
        }

        /*
         *---------------------------------------------------------
         * Add Plant Method
         * --------------------------------------------------------
         * @description:
         *        Adds Plant object to List<Plan>
         * @params: 
         *        plant: Object plant to be added to list
         * --------------------------------------------------------
         */
        public void Add(Plant plant)
        {
           m_Plants.Add(plant);
        }

        /*
         * -------------------------------------------------------
         * Set Device Status Method
         * -------------------------------------------------------
         * @description:
         *      Set the status of the device overall to keep track
         *      if the device is in use or not
         * @params
         *      status: String that sets the status of the device
         * -------------------------------------------------------
         */
        public void SetDeviceStatus(string status)
        {
            m_Status = status;
        }

        /*
        * -------------------------------------------------------
        * Get Device Status Method
        * -------------------------------------------------------
        * @description:
        *      Get the status of the device {"READY", "WATERING"}
        * @returns
        *      _status: private string that returns the status of
        *      device
        * -------------------------------------------------------
        */
        public string GetDeviceStatus()
        {
            return m_Status;
        }

        /*
        * -------------------------------------------------------
        * Get Plant By Id Method
        * -------------------------------------------------------
        * @description:
        *      Finds the plant in List<Plant> by id
        * @params:
        *      id: integer value of id of Plant object
        * @returns
        *      plant: plant object that holds the id and time
        * -------------------------------------------------------
        */
        public Plant GetPlant(int id)
        {
            Plant plant = m_Plants.Find(g => g.GetPlantId() == id);
            return plant;
        }
    }
}
