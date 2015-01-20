/* Name: Cynthia Liu 
 * Date: Decemeber 27, 2012
 * Purpose: Assignmet 3
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            //Title of the project
            Console.Title = "Hangman";
            //A number of WriteLines that output the title of the game
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("********************");
            Console.WriteLine("    Welcome To");
            Console.WriteLine("* * *         * * *");
            Console.WriteLine("    ~ HANGMAN~");
            Console.WriteLine("********************");
            //Chnages the colour back to normal
            Console.ResetColor();
            //Use the procedure MenuSystem 
            MenuSystem();
        }

        /// <summary>
        /// Asks the user which option they would like and then calls that option's associated subprogram
        /// </summary>
        static void MenuSystem()
        {
            //Store the int variable for userChoice
            int userChoice;
            //Store the bool variable for repeat
            bool repeat;
            //create a do while loop that will do everything below at least once until the bool repeat is false
            do
            {
                //Output for organizational purposes
                Console.WriteLine("--------------------------------------------------------------------------");
                //Output a list of options for the user to choose from
                Console.WriteLine("Would you like to:");
                //Changes the colour of the play game option to red 
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("1. Play Game ");
                //Changes the colour of the play game option to yellow
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2. View Instructions");
                //Changes the colour of the play game option to cyan
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("3. View About/Credit Page");
                //Changes the colour of the play game option to magenta
                Console.ForegroundColor = ConsoleColor.Magenta;  
                Console.WriteLine("4. Exit The Game");
                //Changes the colour back to normal
                Console.ResetColor();
                //Outuput the user prompt to choose an option from the list above
                Console.Write("Choose an option from the list above by inputting the associated number: ");
                //Stores the user input to the variable userChoice
                userChoice = int.Parse(Console.ReadLine());
                //Output for visual effect
                Console.WriteLine("--------------------------------------------------------------------------");
                //If the user inputs 1 or 4, the bool variable repeat will equal false
                if (userChoice == 5)
                {
                    repeat = false;
                }
                //If the user inputs 2 or 5, the bool variable repeat will equal true
                else
                {
                    repeat = true;
                }
                //If userInput is 1, the procedure PlayGame will be called
                if (userChoice == 1)
                {
                    //Store the string variables for user1 and user2 
                    string user1, user2;
                    //Assigns the string returned from the Login function to the variable user1
                    user1 = Login();
                    //Output user prompts
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("User 1 = " + user1);
                    //Output for organizational purposes
                    Console.WriteLine("----------------------");
                    Console.ResetColor();
                    //Assigns the string returned from the Login function to the variable user2
                    user2 = Login();
                    //Output user prompts
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("User 2 = " + user2);
                    //Output for organizational purposes
                    Console.WriteLine("----------------------");
                    Console.ResetColor();
                    PlayGame(user1, user2);
                }
                //If the userInput is 2, the procedure Instructions will be called
                else if (userChoice == 2)
                {
                    Instructions();
                }
                //If the userInput is 3, the procedure Credits will be called
                else if (userChoice == 3)
                {
                    Credits();
                }
                //If the userInput is 4, the procedure Exit will be called
                else if (userChoice == 4)
                {
                    Exit();
                }
                //If the user does not choose any of the numbers above, there will be an output telling the user there was an error
                else
                {
                    Console.WriteLine("Error please pick 1, 2, 3 or 4");
                }
            //Continue the loop until the user chooses option 1 or 4 (userChoice == false)
            } while (repeat == true);
        }

        /// <summary>
        /// Asks the user to input their username and the program stores this username
        /// </summary>
        /// <returns>The username inputed by the user as a string</returns>
        static string Login()
        {
            //Store the string variable for username
            string username;
            //Asks the user for their username
            Console.Write("Please enter a username: ");
            //Stores the player's username to the variable username
            username = Console.ReadLine();
            //Returns the string variable username
            return username;
        }

        /// <summary>
        /// Uses a number of other subprograms to output all the user prompts and calculations needed to play the game
        /// </summary>
        /// <param name="user1">The username entered by a user</param>
        /// <param name="user2">The username entered by the other user</param>
        static void PlayGame(string user1, string user2)
        {
            //Output user prompts
            //Changes the colour to red
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Play Hangman");
            Console.WriteLine("////////////////");
            //Changes the colour back
            Console.ResetColor();

            string player1, player2;
            //Generates random numbers
            Random numberGenerator = new Random();
            //Creates the int variable randomNumber and assigns a value of either 0 or 1 to it
            int randomNumber = numberGenerator.Next(0, 2);
            //If randomNumber is 0, user1 is player1, and user2 is player2
            if (randomNumber == 0)
            {
                player1 = user1;
                player2 = user2;
            }
            //If randomNumber is 1, user2 is player1 and user 1 is player2
            else
            {
                player2 = user1;
                player1 = user2;
            }

            //Outputs information to the users that tells them who is going first and who is going second 
            Console.WriteLine(player1 + " Goes first (creates the word or phrase)");
            Console.WriteLine(player2 + " Goes second (guesses the letters in the word or phrase)");

            //Create the bool variable playAgain
            bool playAgain;
            //Create the bool variable player2Win
            bool player2Win = false;
            //Create the following int variables
            int user1Wins = 0, user2Wins = 0, user1Losses = 0, user2Losses = 0, life = 8;
            //Create a do loop that repeats as long as the bool variable playAgain is true
            
            do
            {
                //Asks player1 to enter the word/phrase they want the other player to guess
                Console.WriteLine(player1 + " please enter the word or phrase you would like your opponent to guess." + "\n" + "Please use letters and refrain from using characters except for ! or ?");
                //Store this word/phrase to the string variable word and change all the letters to lower case and change the colour to white 
                Console.ForegroundColor = ConsoleColor.White;
                string word = Console.ReadLine();
                word = word.ToLower();
                //Change the colour back to normal
                Console.ResetColor();
                //Create an int variable called size which is the length of the variable word
                int size = word.Length;
                //Create a string array 
                string[] wordLetters = new string[size];
                //Create a counting loop that will count from 0 to the length of the array wordLetters by 1
                for (int index = 0; index != wordLetters.Length; index++)
                {
                    //Save each letter in the word to an element in the string array
                    wordLetters[index] = word.Substring(index, 1);
                }

                //Clear the program 
                Console.Clear();

                //Create the following int varibales
                int counter = 0, guesses = 0;
                //Create an array 
                string[] player2Guesses = new string[word.Length + 8];
                //Create a string variable called player2LetterGuess and assign it as empty 
                string player2LetterGuess = "";

                //Create a new string array that will be the same size of the variable size that was passed in from PlayGame
                string[] boardLetters = new string[size];
                //Assign the value of boardLetters as the returned value from the subprogram DetermineBoardLetters
                boardLetters = DetermineBoardLetters(boardLetters, wordLetters, player2LetterGuess);
                //Call the DrawBoard subprogram
                DrawBoard(life, player2Guesses, boardLetters, guesses);
                //Create a do loop that will continue to repeat as long as the variable life does not reach 0
                do
                {
                    //Asks the user if they know what the word is
                    Console.Write("Do you know what the word/phrase is? (please enter yes or no) ");
                    //Save the user's answer to a string variable
                    string player2Answer = Console.ReadLine();
                    //If the user inputs yes, the following will happen:
                    if (player2Answer == "yes")
                    {
                        //Ask the user for the word/phrase that they think it is
                        Console.Write("What is the word/phrase? ");
                        //Save the user's guess to a variable 
                        string player2WordGuess = Console.ReadLine();
                        //If player2's guess is correct, the following will happen
                        if (player2WordGuess == word)
                        {
                            //Output congratulations to the winner
                            Console.WriteLine("Congratulations " + player2 + " You Win!");
                            //If player2 is user2, increase user2Wins by 1
                            if (player2 == user2)
                            {
                                user2Wins++;
                            }
                            //If player2 is user1, increase user1Wins by 1
                            else if (player2 == user1)
                            {
                                user1Wins++;
                            }
                            //Exit the loop 
                            break;
                        }
                        //Else if the guess is not correct:
                        else
                        {
                            //Tell the user that the guess was wrong
                            Console.WriteLine("Nice try, but your guess was wrong.");
                            //Save the guess to the array player2Guesses
                            player2Guesses[counter] = player2WordGuess;
                            //Increase the variable counter by 1
                            counter++;
                            //Decrease the variable life by 1
                            life--;
                            //Increase the variable guesses by 1
                            guesses++;
                            //Assign the value of boardLetters as the returned value from the subprogram DetermineBoardLetters
                            boardLetters = DetermineBoardLetters(boardLetters, wordLetters, player2LetterGuess);
                            //Call the subprogram DrawBoard to create an updated board
                            DrawBoard(life, player2Guesses, boardLetters, guesses);
                        }
                    }
                    //If the user inputs no
                    else if (player2Answer == "no")
                    {
                        //Nothing will happen, the program will continue
                    }
                    //Create a bool variable called repeat and assign it as false
                    bool repeat = false;
                    do
                    {
                        //Asks player2 for the letter they want to guess
                        Console.WriteLine(player2 + " :" + " What letter are you going to guess?");
                        //Save that input to the variable player2LetterGuess
                        player2LetterGuess = Console.ReadLine();

                        //Create a bool called valid letter and assign it the value of the bool returned from CheckLetter1
                        bool validLetter = CheckLetter1(player2LetterGuess, player2Guesses);
                        //If validLetter is false:
                        if (validLetter == false)
                        {
                            //Save that guess to the array player2Guesses
                            player2Guesses[counter] = player2LetterGuess;
                            //Tell the user that their guess was wrong
                            Console.WriteLine("Sorry but you already guessed this letter.");
                            //Decrease the value of the variable counter by 2
                            counter = counter--;
                            //Assign the value of repeat as true
                            repeat = true;

                        }
                        //If validLetter is true:
                        else if (validLetter == true)
                        {
                            //Save that guess to the array player2Guesses
                            player2Guesses[counter] = player2LetterGuess;
                            //Create a bool variable called letterFound and assign it the value of the bool returned from CheckLetter2
                            bool letterFound = CheckLetter2(wordLetters, player2LetterGuess);
                            //If letterFound is true:
                            if (letterFound == true)
                            {
                                //Increase the value of guesses by 1
                                guesses++;
                                //Assign the value of boardLetters as the returned value from the subprogram DetermineBoardLetters
                                boardLetters = DetermineBoardLetters(boardLetters, wordLetters, player2LetterGuess);
                                //Call the subprogram DrawBoard to create an updated board
                                DrawBoard(life, player2Guesses, boardLetters, guesses);
                                //Asign the bool variable repeat as false
                                repeat = false;
                                //Assign the value of player2Win to the value returned by DidPlayer2Win
                                player2Win = DidPlayer2Win(boardLetters, wordLetters);
                                //If player2Win is true, player2 has won; increase either user1Wins or user2Wins by 1
                                if (player2Win == true)
                                {
                                    if (player2 == user2)
                                    {
                                        user2Wins++;
                                        //Output a congratulatory message for the user
                                        Console.WriteLine("Congratulations! You guessed the correct word!");
                                    }
                                    //If player2 is user1, increase user1Wins by 1
                                    else if (player2 == user1)
                                    {
                                        user1Wins++;
                                    }
                                }
                                //If player2Win is false, player2 has not won yet
                                else
                                {
                                    //The program will continue, nothing will happen
                                }
                            }
                            //If letterFound is false:
                            else if (letterFound == false)
                            {
                                //Increase the value of guesses by 1
                                guesses++;
                                //Drecrease the value of life by 1
                                life--;
                                //Assign the value of repeat as false
                                repeat = false;
                                //Assign the value of boardLetters as the returned value from the subprogram DetermineBoardLetters
                                boardLetters = DetermineBoardLetters(boardLetters, wordLetters, player2LetterGuess);
                                //Call the subprogram DrawBoard
                                DrawBoard(life, player2Guesses, boardLetters, guesses);
                            }
                        }
                    //The loop will end when the variable repeat is false
                    } while (repeat == true);
                    if (player2Win == true)
                    {
                        break;
                    }
                    //Increase the value of the variable counter
                    counter++;
                //The loop will end when the varibale life is equal to 0
                } while (life != 0);
                //If the variable life is 0
                if (life == 0)
                {
                    //increase either user1Losses or user2Losses by 1
                    if (player2 == user1)
                    {
                        user1Losses++;
                    }
                    else
                    {
                        user2Losses++;
                    }
                }
                //Output the statistics for each user
                Statistics(user1, user1Wins, user1Losses);
                Statistics(user2, user2Wins, user2Losses);
                //Call the subprogram GameOver and assign the returned value to playAgain
                playAgain = GameOver(word, life);
                //If playAgain is true, the users switch player numbers
                if (playAgain == true)
                {
                    if (user1 == player1)
                    {
                        player1 = user2;
                        player2 = user1;
                    }
                    else
                    {
                        player1 = user1;
                        player2 = user2;
                    }
                }
                //If playAgain is false, the subprogram Exit is called
                else
                {
                    Exit();
                }
            //This loop will start again if playAgain is true
            } while (playAgain == true);
        }

        /// <summary>
        /// Takes the user's letter guess and checks to see if it exists in the wordLetters array and then
        /// creates an updated boardletters array
        /// </summary>
        /// <param name="boardLetters">The most recently updated boardLetters array</param>
        /// <param name="wordLetters">The letters in the word/phrase that player1 entered</param>
        /// <param name="player2LetterGuess">the most recent letter guess that the player has made</param>
        /// <returns>an updated boardLetters array</returns>
        static string[] DetermineBoardLetters(string[] boardLetters, string[] wordLetters, string player2LetterGuess)
        {
            //Create a counting loop that will count from zero by one as long as index does not equal the same size as the length of the variable wordLetters
            for (int index = 0; index != wordLetters.Length; index++)
            {
                //If the element in the array wordLetters is " ", then the corresponding element in the array in boardLetters will have the same value
                if (wordLetters[index] == " ")
                {
                    boardLetters[index] = " ";
                }
                //If the element in the array wordLetters is "?", then the corresponding element in the array in boardLetters will have the same value
                else if (wordLetters[index] == "?")
                {
                    boardLetters[index] = "?";
                }
                //If the element in the array wordLetters is "!", then the corresponding element in the array in boardLetters will have the same value
                else if (wordLetters[index] == "!")
                {
                    boardLetters[index] = "!";
                }
                //If the element in the array wordLetters is "_", then the corresponding element in the array in boardLetters will have the same value
                else if (boardLetters[index] == wordLetters[index])
                {
                    boardLetters[index] = wordLetters[index];
                }
                //Else, the element in the array will be a blank
                else
                {
                    boardLetters[index] = "_";
                }

            }
            //Create a counting loop that will count from zero by one as long as index does not equal the same as the length of the variable wordLetters
            for (int index = 0; index != wordLetters.Length; index++)
            {
                //If the guess made by player2 in PlayGame is found in the wordLetters array, that element in the array boardLetters is chnaged to the variable of player2LetterGuess
                if (player2LetterGuess == wordLetters[index])
                {
                    boardLetters[index] = player2LetterGuess;

                }
                //If the guess is not found, then do nothing
                else
                {
                }
            }
            //Return the updated array 
            return boardLetters;
        }

        /// <summary>
        /// Outputs the game board
        /// </summary>
        /// <param name="life">number of lives the player has left</param>
        /// <param name="player2Guesses">an array of all the guesses the player has ever made</param>
        /// <param name="boardLetters">boardLetters array</param>
        /// <param name="guesses">number of guesses the player has made</param>
        static void DrawBoard(int life, string[] player2Guesses, string[] boardLetters, int guesses)
        {
            //Change the colour to white
            Console.ForegroundColor = ConsoleColor.White;
            //Output the amount of guesses the user has made and the number of lives they have left
            Console.WriteLine("Guesses: " + guesses);
            Console.WriteLine("Lives: " + life);

            //Output this gameboard when the player has all lives left
            if (life == 8)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │     ");
                Console.WriteLine("     │     ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │       ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has lost one life
            else if (life == 7)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │     ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │       ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has lost 2 lives
            else if (life == 6)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │     │ ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │       ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has 5 remaining lives
            else if (life == 5)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│");
                Console.WriteLine("     │   / │ ");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │       ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has 4 remaining lives
            else if (life == 4)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│\\     ");
                Console.WriteLine("     │   / │ \\");
                Console.WriteLine("     │      ");
                Console.WriteLine("     │       ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has 3 remaining lives
            else if (life == 3)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│\\     ");
                Console.WriteLine("     │   / │ \\");
                Console.WriteLine("     │    / ");
                Console.WriteLine("     │   /   ");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has 2 remaining lives
            else if (life == 2)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│\\     ");
                Console.WriteLine("     │   / │ \\");
                Console.WriteLine("     │    / \"");
                Console.WriteLine("     │   /   \"");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player only has 1 remaining life
            else if (life == 1)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│\\     ");
                Console.WriteLine("     │   / │ \\");
                Console.WriteLine("     │    / \"");
                Console.WriteLine("     │  _/   \"");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Output this gameboard when the player has no more lives
            else if (life == 0)
            {
                Console.WriteLine("  _____________   ");
                Console.WriteLine("     │     │");
                Console.WriteLine("     │    ( )");
                Console.WriteLine("     │    /│\\     ");
                Console.WriteLine("     │   / │ \\");
                Console.WriteLine("     │    / \"");
                Console.WriteLine("     │  _/   \"" + "_");
                Console.WriteLine("     │         ");
                Console.WriteLine("##########");
            }
            //Create a counting loop so tht the elements in the boardLetters array will be outputted with a space after each
            for (int index = 0; index != boardLetters.Length; index++)
            {
                Console.Write(boardLetters[index] + " ");
            }
            //Output a user prompt to output the user's previous guesses
            Console.WriteLine("" + "\n");
            Console.WriteLine("Previous Guesses:");
            //Create a counting loop that will output all the elements in the player2Guesses with a space after each
            for (int index = 0; index != player2Guesses.Length; index++)
            {
                Console.Write(player2Guesses[index] + " ");
            }
            Console.WriteLine("");
            //Change the colour back to normal 
            Console.ResetColor();
        }

        static bool CheckLetter1(string player2LetterGuess, string[] player2Guesses)
        {
            //Create a counting loop that will count from zero by one as long as index is smaller than player2Guesses
            for (int index = 0; index < player2Guesses.Length; index++)
            {
                //if player2's guess is found in the array player2Guesses, it means that the user has already made this guess
                if (player2LetterGuess == player2Guesses[index])
                {
                    return false;
                }
                //If player2's guess is not found in the array player2Guesses, it means that the user has not made this guess yet
                else if (player2LetterGuess != player2Guesses[index])
                {
                    return true;
                }
            }
            return true;
        }

        static bool CheckLetter2(string[] wordLetters, string player2LetterGuess)
        {
            bool letterFound = false;
            //Create a counting loop that will count from zero by one as long as the variable index is less than the length of wordLetters
            for (int index = 0; index < wordLetters.Length; index++)
            {
                //If player2's guess is found in the array wordLetters, the player guessed right
                if (player2LetterGuess == wordLetters[index])
                {
                    //Return true
                    letterFound = true;
                }
            }
            return letterFound;
        }

        /// <summary>
        /// Find out if the user wants to play again
        /// </summary>
        /// <param name="word">the word that player2 was trying to guess</param>
        /// <param name="life">the number of lives the player has left </param>
        /// <returns></returns>
        static bool GameOver(string word, int life)
        {
            //If life is 0, the player was not able to guess the word/phrase
            if (life == 0)
            {
                //Outputs message telling the user he/she lost and the correct word
                Console.WriteLine("Sorry but you could not guess the correct word/phrase before your tries ran out");
                Console.WriteLine("The word was: " + word);
                //Asks if the user wants to play again
                Console.WriteLine("Would you like to play again?");
                //Store that answer to the variable answer
                string answer = Console.ReadLine();
                //If answer is yes, return true
                if (answer == "yes")
                {
                    return true;
                }
                //If answer is no, return false
                else 
                {
                    return false;
                }
            }
            //Else, the player has won (they have remaining lives)
            else 
            {
                //Asks if the user wants to play again
                Console.WriteLine("Would you like to play again?");
                //Store that answer to the variable answer
                string answer = Console.ReadLine();
                //If answer is yes, return true 
                if (answer == "yes")
                {
                    return true;
                }
                //If answer is no, return false
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks to see if player2 has won
        /// </summary>
        /// <param name="boardLetters">the array boardLetters</param>
        /// <param name="wordLetters">the array that contains the letters of the word/phrase being guessed</param>
        /// <returns>true or false</returns>
        static bool DidPlayer2Win(string[] boardLetters, string[] wordLetters)
        {
            bool player2win = false;
            //If the arrays boardLetters and wordLetters equal one another, return true
            if (boardLetters != wordLetters)
            {
                player2win = true;
            }
            return player2win;
        }

        /// <summary>
        /// Calculates a player's winning percentage
        /// </summary>
        /// <param name="userWins">The amount of times the player won</param>
        /// <param name="userLosses">The amount of times the player lost</param>
        /// <returns>The calculated winning percentage</returns>
        static double WinningPercentageCalculation(int userWins, int userLosses)
        {
            //Store the double variable for winningPercentage
            double winningPercentage;
            //Calculation for winning percentage saved to the variable winningPercentage
            winningPercentage = (double)userWins / (userWins + userLosses) * 100;
            //Return the double variable winningPercentage
            return winningPercentage;
        }

        /// <summary>
        /// Output statistics for a user after playing the game
        /// </summary>
        /// <param name="user">the username</param>
        /// <param name="userWins">the number of times that user has won</param>
        /// <param name="userLosses">the number of times that user has lost</param>
        static void Statistics(string user, int userWins, int userLosses)
        {
            //Call the WinningPercentageCalculation subprogram and assign the returned value to the double varibale: winningPercentage
            double winningPercentage = WinningPercentageCalculation(userWins, userLosses);
            //Outputs the information for the user 
            Console.WriteLine(user + ":   " + "Wins: " + userWins + " Losses: " + userWins + " Games Played: " + (userLosses + userWins) + " Winning Percentage: " + winningPercentage + "%");
        }

        //Outputs the credits 
        static void Credits()
        {
            //Changes the colour to cyan 
            Console.ForegroundColor = ConsoleColor.Cyan;
            //Outputs the title Credits
            Console.WriteLine("Credits");
            Console.WriteLine("////////////////");
            //Changes the colour back to normal
            Console.ResetColor();
        }

        //Outputs the instructions on how to play the game 
        static void Instructions()
        {
            //Changes the colour to yellow
            Console.ForegroundColor = ConsoleColor.Yellow;
            //Outputs the title Tic-Tac-Toe Help
            Console.WriteLine("Hangman Help");
            Console.WriteLine("////////////////");
            //Changes the colour back to normal 
            Console.ResetColor();
            //Outputs the instructions for the game
            Console.WriteLine("1. Both players choose usernames");
            Console.WriteLine("2. Player 1 (randomly decided by the computer) will enter the word/phrase" + "\n" + "that they want player2 to guess");
            Console.WriteLine("3. Then, player 1 will enter the letters of this word/phrase one by one");
            Console.WriteLine("4. Player 2 will then guess the letters that they believe to be in the word/phrase");
            Console.WriteLine("* To win: player 2 must guess the correct word/letter before their chances run out");
        }

        //Outputs a goodbye to the users and the entire program ends 
        static void Exit()
        {
            //Changes the colour to magenta
            Console.ForegroundColor = ConsoleColor.Magenta;
            //Outputs the title 
            Console.WriteLine("~GOODBYE :) PLAY AGAIN SOON :D");
            Console.WriteLine("//////////////////////////////");
            //Changes the colour back to normal
            Console.ResetColor();
        }
    }
}
