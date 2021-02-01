using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    class Actor
    {
        private string name;
        private string skill;
        private string profession;
        public Actor(string name, string skill, string profession)
        {
            this.name = name;
            this.skill = skill;
            this.profession = profession;
        }

        public string Name
        {
            get { return name; }
            set { name = value;}
        }

        public string Profession
        {
            get { return profession; }
            set { profession = value; }
        }

        public string Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        public override string ToString()
        {
            return $"{name}|{skill}|{profession}";
        }
    }
}
