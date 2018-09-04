using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watermango.Classes
{
    /*
     *---------------------------------------------------------------
     * Plant Class
     *--------------------------------------------------------------- 
     * @description:
     *      Plant class defines a plant by id, time last watered and
     *      whether it was watered or not
     *---------------------------------------------------------------
     */ 
    public class Plant
    {
        private readonly int m_Id;
        private DateTime m_Time;
        public bool isWatered;


        /*
         * ----------------------------------------------------------
         * Constructor method to initialize Plant object
         * -----------------------------------------------------------
         * @params:
         *      id: unique integer value for plant
         *      time: plant time added
         *      isWatered: value to keep track of plant being watered
         * ------------------------------------------------------------
         */ 
        public Plant(int id, DateTime time, bool isWatered)
        {
            m_Id = id;
            m_Time = time;
            this.isWatered = isWatered;
        }

        /*
         * ----------------------------------------------------------
         * Get Plant Id
         * -----------------------------------------------------------
         * @description:
         *      Get the unique value Plant object
         * @returns:
         *      _id: private unique integer value to get plant
         * ------------------------------------------------------------
         */
        public int GetPlantId()
        {
            return m_Id;
        }

        /*
         * ----------------------------------------------------------
         * Sets Last watered method
         * -----------------------------------------------------------
         * @description:
         *      Set the time the plant was last watered
         * @params:
         *      time: DateTime value to store last time watered
         * ------------------------------------------------------------
         */
        public void SetLastWatered(DateTime time)
        {
            m_Time = time;
        }

        /*
         * ----------------------------------------------------------
         * Get Last watered method
         * -----------------------------------------------------------
         * @description:
         *      Get the time the plant was last watered
         * @returns:
         *      _time: DateTime value of last time watered
         * ------------------------------------------------------------
         */
        public DateTime GetLastWatered()
        {
            return m_Time;
        }
    }
}
