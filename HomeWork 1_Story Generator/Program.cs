using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

//Start date 2/1/21

//Lance Royer, Elliot Gong, John Wager, Jack Doyle, Nathan Caron



namespace HomeWork_1_Story_Generator
{

    //This is the main class to ask for a type of story and to output the story to the user
    class Program
    {
        static void Main(string[] args)
        {

            // Reads in data from text files and stores it in arrays
            string[] actors_raw = File.ReadAllLines("../../actors.txt");
            string[] settings_raw = File.ReadAllLines("../../settings.txt");
            string[] conflicts_raw = File.ReadAllLines("../../conflicts.txt");

            // Initializes the lists to store the objects
            List<Actor> actors = new List<Actor>();
            List<Setting> settings = new List<Setting>();
            List<Conflict> conflicts = new List<Conflict>();

            // Adds the actor objects to the actors list
            foreach (string line in actors_raw)
            {
                string[] actorInfo = line.Split('|');
                actors.Add(new Actor(actorInfo[0], actorInfo[1], actorInfo[2]));
            }

            // Adds the setting objects to the settings list
            foreach (string line in settings_raw)
            {
                string[] settingInfo = line.Split('|');
                settings.Add(new Setting(settingInfo[0], settingInfo[1]));
            }

            // Adds the conflict objects to the conflicts list
            foreach (string line in conflicts_raw)
            {
                string[] conflictInfo = line.Split('|');
                conflicts.Add(new Conflict(conflictInfo[0], conflictInfo[1], conflictInfo[2]));
            }


            //vars
            string storyEnd;
            string fullStory = "";
            string repeatString;
            Random random = new Random();
            


            Console.WriteLine("Welcome to the story generator!\n");


            //Start loop 

            //This repeats when the option is chosen to choice multiple stories
            for (int i = 0; i < 1; i++)
            {
                
                // loop exists until input is valid
                while (true)
                {
                    Actor actor1 = actors[random.Next(actors.Count)];
                    // create a list where actor1 is not in it then select a random element
                    Actor actor2 = actors.Where(x => x != actor1)
                        .ElementAt(random.Next(actors.Count - 1));

                    Setting setting = settings[random.Next(settings.Count)];
                    
                    //ask for type of story that is wanted
                    Console.WriteLine("Please choose a type of ending to generate a story: \n'happy' 'tragic' 'romantic' \n'destructive' 'twist' 'any ending'\n\n");

                    Console.Write("Your choice >>  ");
                    string userInput = Console.ReadLine();

                    //putt the input to lower to check for caps errors
                    storyEnd = userInput.ToLower().Trim();
                    
                    Conflict[] validConflicts = conflicts.Where(x => x.EndTag == storyEnd).ToArray();
                    // if storyEnd was not found in conflicts then validConflicts will be empty
                    // and it was not valid unless 'any ending' was given
                    if (storyEnd != "any ending" && validConflicts.Length == 0)
                    {
                        Console.WriteLine("That is not one of the choices, please retry\n");
                        // run the loop again
                        continue;
                    }

                    // find a corresponding conflict
                    Conflict conflict;
                    if (storyEnd == "any ending")
                    {
                        conflict = conflicts[random.Next(conflicts.Count)];
                    }
                    else
                    {
                        conflict = validConflicts[random.Next(validConflicts.Length)];
                    }
                    
                    // assimilate the story
                    fullStory = conflict.GenerateStory(actor1, actor2, setting);
                    break;
                }
            

                Console.WriteLine("\n");


                




                //Print out generated sotry
                Console.WriteLine("Story Idea: ");
                Console.WriteLine(fullStory);


                //loop to make sure a valid answer is inputted
                for (int k = 0; k < 1; k++)
                {
                    Console.WriteLine("\nWould you like another story? Choose 'yes' or 'no' >> ");
                    repeatString = Console.ReadLine();
                    Console.WriteLine("");

                    //to lower to remove caps errors
                    string stringLower = repeatString.ToLower();


                    if (stringLower == "yes")
                    {
                        //restart major loop to restart the code
                        i--;
                    }
                    else if (stringLower == "no")
                    {
                        Console.WriteLine("GoodBye!");
                    }
                    else
                    {
                        Console.WriteLine("That is not a valid repsonse.");
                        //restart this small loop because the answer is not yes or no
                        k--;
                    }
                }//end of loop to make sure they answer yes or no

            }//end of loop to run majority of code how ever manny time the user would like



            //keep console open
            Console.ReadKey();

        }//end of main
    }
}
