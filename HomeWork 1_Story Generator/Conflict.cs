using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    class Conflict
    {
        private string problem;
        private string resolution;
        private string endTag;

        public Conflict(string endingType, string conflictFromFile, string rapUp)
        {
            problem = conflictFromFile;
            resolution = rapUp;
            endTag = endingType;
        }

        public string Problem { get { return problem; } }

        public string Resolution { get { return resolution; } }

        public string EndTag { get { return endTag; } }


    }
}
