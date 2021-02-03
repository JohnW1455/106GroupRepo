using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Start date 2/1/21

//Lance Royer, Elliot Gong, John Wager, Jack Doyle, Nathan Caron



namespace HomeWork_1_Story_Generator
{

    //This is the main class to ask for a type of story and to output the story to the user
    class Program
    {
        static void Main(string[] args)
        {
            //vars
            string storyEnd;
            string fullStory = "";
            string repeatString;
            bool validInput;
            


            Console.WriteLine("Welcome to the story generator!\n");


            //Start loop 

            //This repeats when the option is chosen to choice multiple stories
            for (int i = 0; i < 1; i++)
            {
                

                do //while validInput == false
                {

                    //ask for type of story that is wanted
                    Console.WriteLine("Please choose a type of ending to generate a story: \n'happy' 'tragic' 'romantic' \n'destructive' 'twist' 'any ending'\n\n");

                    Console.WriteLine("Your choice >> ");
                    string userInput = Console.ReadLine();

                    //putt the input to lower to check for caps errors
                    storyEnd = userInput.ToLower().Trim();

                    switch (storyEnd)
                    {
                        case "happy":
                            validInput = true;
                            break;
                        case "tragic":
                            validInput = true;
                            break;
                        case "romantic":
                            validInput = true;
                            break;
                        case "destructive":
                            validInput = true;
                            break;
                        case "twist":
                            validInput = true;
                            break;
                        case "any ending":
                            validInput = true;
                            break;
                        default:
                            Console.WriteLine("That is not one of the choices, please retry\n");
                            validInput = false;
                            break;
                    }
                } while (validInput == false);
            

                Console.WriteLine("\n");


                ///find way to get the generated story




                //Print out generated sotry
                Console.WriteLine("Story Idea: ");
                Console.WriteLine(fullStory);


                //loop to make sure a valid answer is inputted
                for (int k = 0; k < 1; k++)
                {
                    Console.WriteLine("Would you like another story? Choose 'yes' or 'no' >> ");
                    repeatString = Console.ReadLine();

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
