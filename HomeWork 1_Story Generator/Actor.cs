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
        private int age;
        private string profession;
        public Actor(string name, int age, string profession)
        {
            this.name = name;
            this.age = age;
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

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public override string ToString()
        {
            return $"{name}|{age}|{profession}";
        }
    }
}
