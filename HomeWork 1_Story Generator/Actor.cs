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
        private string ethnicity;
        public Actor(string name, int age, string ethnicity)
        {
            this.name = name;
            this.age = age;
            this.ethnicity = ethnicity;
        }

        public string Name
        {
            get { return name; }
            set { name = value;}
        }

        public string Ethnicity
        {
            get { return ethnicity; }
            set { ethnicity = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }
    }
}
