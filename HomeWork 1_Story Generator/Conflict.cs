using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    /// <summary>
    /// Conflict class takes the data read from a file and organizes it into usable sections
    /// </summary>
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

        public override string ToString()
        {
            return $"<actor1Name> is a <profession1> who is <actor1Skill> from <location> in <time_period>." + problem + ". " + resolution + ".";
        }
    }
}
