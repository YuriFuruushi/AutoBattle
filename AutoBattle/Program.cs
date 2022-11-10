using System;
using AutoBattle.Entities;
using AutoBattle.Map;
using System.Text;
using System.Numerics;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Battlefield battlefield;
            int currentTurn = 0;
            Setup();

            //Create Characters and Battlefield
            void Setup()
            {
                //Create Battlefield
                battlefield = new Battlefield(new Vector2(Constants.MapWidth, Constants.MapHeight));
                //Create Player Character
                GetPlayerChoice();
                //Create Enemy Character
                CreateCharacter("Enemy", 1);
                //Set players in the Battlefield
                SpawnCharacters();
                Console.Write(Environment.NewLine);
                //Draw battlefield for the first time
                battlefield.Draw();
                Console.WriteLine("The battle field has been created!\n");
                StartGame();
            }

            //Allocate Characters in the Battlefield
            void SpawnCharacters()
            {
                for (int i = 0; i < battlefield.allPlayers.Length; i++)
                {
                    battlefield.allPlayers[i].Spawn(battlefield);
                }
            }

            //Get Player Class & Create Character
            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");

                //store the player choice in a variable
                int key = Console.ReadKey().KeyChar - '0';
                Console.Write(Environment.NewLine);

                if (key == 0 || key > 4)
                {
                    GetPlayerChoice();
                }
                else
                {
                    CreateCharacter("Player", 0, key);
                }
            }

            //Create a new Character
            void CreateCharacter(string name, int id, int classIndex = -1)
            {
                int charClass = classIndex;

                if (charClass < 1 || charClass > 4)
                    charClass = RandomExtensions.GetRandomInt(1, 4);


                Character character = new Character(name, id, Constants.CharacterBaseHealth, Constants.CharacterBaseDamage, (CharacterClassEnum)charClass);
                Console.WriteLine($"{name} Class = {(CharacterClassEnum)charClass}");
                battlefield.allPlayers[id] = (character);
            }


            void StartGame()
            {
                //Randomize Players Order
                var rnd = new Random();
                rnd.Shuffle(battlefield.allPlayers);
                StartTurn();
            }


            void StartTurn()
            {
                StringBuilder feedbackMessage = new StringBuilder();
                for (int i = 0; i < battlefield.allPlayers.Length; i++)
                {
                    feedbackMessage.Append(battlefield.allPlayers[i].Behavior());
                }

                string msg = feedbackMessage.ToString();
                Console.WriteLine(msg);

                if (msg.Contains("walked"))
                    battlefield.Draw();

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                //Check End Game
                string winner = EndGameCheck();
                if (!string.IsNullOrEmpty(winner))
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine($"{winner} WON!\n");
                    Console.WriteLine("THE END!\n");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Press any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }

            //If one of the players are dead, end game
            string EndGameCheck()
            {
                bool endGame = false;
                string winner = string.Empty;
                for (int i = 0; i < battlefield.allPlayers.Length; i++)
                {
                    Character character = battlefield.allPlayers[i];
                    if (character.Dead)
                        endGame = true;
                    else
                        winner = character.Name;
                }
                if (endGame)
                    return winner;
                else
                    return string.Empty;
            }
        }
    }
}
