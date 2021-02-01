using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    class Setting
    {
        private string location;
        private string timePeriod;

        public Setting(string location, string timePeriod)
        {
            this.location = location;
            this.timePeriod = timePeriod;
        }

        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public string TimePeriod
        {
            get { return timePeriod; }
            set { timePeriod = value; }
        }

        public override string ToString()
        {
            return $"{location}|{timePeriod}";
        }
    }
}
