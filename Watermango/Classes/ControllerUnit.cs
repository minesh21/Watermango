using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Watermango.Classes
{
    /*
     * --------------------------------------------------------
     * Control Unit Class
     * --------------------------------------------------------
     * @description:
     *      Controls whether a plant gets watered as well as 
     *      checks when plant was last watered, blocks other
     *      other plants from getting watered until it has
     *      finished being watered
     *-------------------------------------------------------
     */ 
    public class ControllerUnit
    {
        // Initialize instance variables
        private const int INTERVAL = 10000;
        private const int WAIT = 30;
        private Stopwatch m_Timer;
        private Device m_Device;
        private Plant m_Plant = null;
        private enum WaitStatus { READY, WAITING, NONE};

        // Constructor initialization class
        public ControllerUnit() { }

        // Constructor to initialize class with device
        public ControllerUnit(Device device)
        {
            m_Device = device;
        }

        /*
         * ----------------------------------------------------
         * Set Device Method
         * ----------------------------------------------------
         * @description:
         *      Set device to control from control unit
         * @params:
         *      device: Device Object to be controlled
         */ 
        public void SetDevice(Device device)
        {
            m_Device = device;
        }

        /*
         * ----------------------------------------------------
         * Water Plant Method
         * ----------------------------------------------------
         * @description:
         *      Controller Unit will determine whether a plant
         *      can be watered or wait if the plant is not in a
         *      ready state, the plan is current being watered
         *      or if the same plant is selected again, must wait
         *      30 seconds to water again
         * @params:
         *      id: id of Plant to be watered
         */
        public int WaterPlant(int id)
        {
            CheckTimer();
            if (m_Device.GetDeviceStatus() == "READY")
            {
                if (m_Device.GetPlant(id) == null) return 2;

                m_Plant = m_Device.GetPlant(id);

                if (DidWaitTime() == (int)WaitStatus.WAITING) return 3;

                m_Device.SetDeviceStatus("WATERING");
                m_Timer = new Stopwatch();
                m_Timer.Start();
                return 0;
            }
            return 1;
        }

        /*
         * ----------------------------------------------------
         * Water Plant Method
         * ----------------------------------------------------
         * @description:
         *      Checks Wether a Plant has been watered is the last
         *      30s, if it has, you can water again otherwise must
         *      wait 30s
         * @returns:
         *      A status defined by enumerated type (WaitStatus)
         *      to check if the plant has been watered 30s or not
         */
        public int DidWaitTime()
        {
            if (!m_Plant.isWatered)
            {
                return (int)WaitStatus.NONE;
            }

            DateTime lastWatered = m_Plant.GetLastWatered();
            double seconds = Math.Abs((lastWatered - DateTime.Now).TotalSeconds);
            TimeSpan time = TimeSpan.FromSeconds(seconds);

            if (m_Plant.isWatered && time.Seconds >= WAIT)
            {
                m_Plant.isWatered = false;
                return (int)WaitStatus.READY;
            }

            return (int)WaitStatus.WAITING;
        }

        /*
         * ------------------------------------------------------------------------
         * Check Timer method
         * -------------------------------------------------------------------------
         * @description:
         *      Checks whether the device has finished watering plant after 10s
         *      If 10s have passed the device is set back to a READY state otherwise
         *      continues watering plant
         * -------------------------------------------------------------------------
         */ 
        public void CheckTimer()
        {
            if (m_Timer != null && m_Timer.IsRunning && m_Timer.ElapsedMilliseconds >= INTERVAL)
            {
                m_Plant.isWatered = true;
                m_Timer.Stop();
                m_Device.SetDeviceStatus("READY");
                m_Plant.SetLastWatered(DateTime.Now);
            }
        }

        /*
         * ------------------------------------------------------------------------
         * Get Plant method
         * -------------------------------------------------------------------------
         * @description:
         *      Gets the last time the plant was watered in hh:mm:ss 
         * @params:
         *      id: integer value to get plant
         * -------------------------------------------------------------------------
         */
        public void GetPlantStatus(int id)
        {
            CheckTimer();
            Plant plant = m_Device.GetPlant(id);
            if (plant != null)
            {
                DateTime lastWatered = plant.GetLastWatered();
                double seconds = Math.Abs((lastWatered - DateTime.Now).TotalSeconds);
                TimeSpan time = TimeSpan.FromSeconds(seconds);
                Console.WriteLine("Plant {0} hasn't been watered for {1} hours {2} minutes and {3} seconds", id, time.Hours, time.Minutes, time.Seconds);
            } else
            {
                Console.WriteLine("Plant {0} not found", id);
            }
        }
    }
}
