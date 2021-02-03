using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    /* Author: Elliot Gong
     * Date: 2/1/2021
     * Purpose: Create a class that stimulates a settng.
     * Restriction: Must have at least 2 pieces of info for a "setting".*/
    class Setting
    {
        //These are the 3 fields attributed to each setting object.
        private string location;
        private string timePeriod;
        /// <summary>
        /// This is the constructor for the setting class.
        /// </summary>
        /// <param name="location">This defines the setting's location.</param>
        /// <param name="timePeriod">This defines the settting's time period.</param>
        public Setting(string location, string timePeriod)
        {
            this.location = location;
            this.timePeriod = timePeriod;
        }
        /// <summary>
        /// This is the getter/setter property for the setting's location information.
        /// </summary>
        public string Location
        {
            get { return location; }
            set { location = value; }
        }
        /// <summary>
        /// This is the getter/setter property for the setting's time period information.
        /// </summary>
        public string TimePeriod
        {
            get { return timePeriod; }
            set { timePeriod = value; }
        }
        /// <summary>
        /// This method briefly describes the setting and its attributes.
        /// </summary>
        /// <returns>This method returns a string detailing the basic info of the setting object.</returns>
        public override string ToString()
        {
            return $"{location}|{timePeriod}";
        }
    }
}
