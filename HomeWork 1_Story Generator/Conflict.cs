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

        public void ReplaceStrings(Actor actor1, Actor actor2, Setting setting)
        {
            problem = problem.Replace("{actor1Name}", actor1.Name);
            problem = problem.Replace("{actor2Name}", actor2.Name);
            problem = problem.Replace("{actor1Skill}", actor1.Skill);
            problem = problem.Replace("{actor2Skill}", actor2.Skill);
            problem = problem.Replace("{actor1Job}", actor1.Profession);
            problem = problem.Replace("{actor2Job}", actor2.Profession);
            problem = problem.Replace("{location}", setting.Location);
            problem = problem.Replace("{timePeriod}", setting.TimePeriod);
            resolution = resolution.Replace("{actor1Name}", actor1.Name);
            resolution = resolution.Replace("{actor2Name}", actor2.Name);
            resolution = resolution.Replace("{actor1Skill}", actor1.Skill);
            resolution = resolution.Replace("{actor2Skill}", actor2.Skill);
            resolution = resolution.Replace("{actor1Job}", actor1.Profession);
            resolution = resolution.Replace("{actor2Job}", actor2.Profession);
            resolution = resolution.Replace("{location}", setting.Location);
            resolution = resolution.Replace("{timePeriod}", setting.TimePeriod);
        }

        public string GenerateStory()
        {
            // could just be the ToString()
            return problem + " " + resolution;
        }

        public override string ToString()
        {
            return $"<actor1Name> is a <profession1> who is <actor1Skill> from <location> in <time_period>." + problem + ". " + resolution + ".";
        }
    }
}
