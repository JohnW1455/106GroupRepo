using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_1_Story_Generator
{
    /* Author: Elliot Gong
     * Date: 2/1/2021
     * Purpose: Create a class that stimulates an actor.
     * Restriction: Must have at least 3 pieces of info for an "actor".*/
    class Actor
    {
        //These are the 3 fields attributed to each actor object: Name, Skill, Profession
        private string name;
        private string skill;
        private string profession;
        /// <summary>
        /// This is the constructor for the actor class.
        /// </summary>
        /// <param name="name">Defines the actor's name.</param>
        /// <param name="skill">Defines the actor's special skill.</param>
        /// <param name="profession">Defines the actor's profession.</param>
        public Actor(string name, string skill, string profession)
        {
            this.name = name;
            this.skill = skill;
            this.profession = profession;
        }
        /// <summary>
        /// This is the getter/setter property that focuses on the actor's name parameter.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value;}
        }
        /// <summary>
        /// This is the getter/setter property that focuses on the actor's profession parameter.
        /// </summary>
        public string Profession
        {
            get { return profession; }
            set { profession = value; }
        }
        /// <summary>
        /// This is the getter/setter property that focuses on the actor's skill parameter.
        /// </summary>
        public string Skill
        {
            get { return skill; }
            set { skill = value; }
        }
        /// <summary>
        /// This method gives a brief description of the actor.
        /// </summary>
        /// <returns>This method returns a basic string detailing the actor's info.</returns>
        public override string ToString()
        {
            return $"{name}|{skill}|{profession}";
        }
    }
}
