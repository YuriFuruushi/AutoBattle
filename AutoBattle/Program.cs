using System;
using AutoBattle.Characters;
using AutoBattle.Battlefield;
using System.Text;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid battlefield = new Grid(7, 9);
            Character[] allPlayers = new Character[2];
            int currentTurn = 0;
            Setup();


            void Setup()
            {
                //Create Player Character
                GetPlayerChoice();
                //Create Enemy Character
                CreateCharacter("Enemy", 1);
                allPlayers[0].target = allPlayers[1];
                allPlayers[1].target = allPlayers[0];
                //Set players in the Battlefield
                SpawnCharacters();

                Console.WriteLine("The battle field has been created!\n");
                StartGame();
            }

            void SpawnCharacters()
            {
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    allPlayers[i].Spawn(battlefield);
                }
            }

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

            void CreateCharacter(string name, int id, int classIndex = -1)
            {
                int charClass = classIndex;

                if (charClass < 1 || charClass > 4)
                    charClass = RandomExtensions.GetRandomInt(1, 4);


                Character character = new Character(name, id, 100, 20, (CharacterClassEnum)charClass);
                Console.WriteLine($"Player {id} Class Choice: {(CharacterClassEnum)charClass}");
                allPlayers[id] = (character);
            }

            void StartGame()
            {
                //Randomize Players Order
                var rnd = new Random();
                rnd.Shuffle(allPlayers);
                StartTurn();
            }


            void StartTurn()
            {
                StringBuilder feedbackMessage = new StringBuilder();
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    feedbackMessage.Append(allPlayers[i].Behavior());
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

            string EndGameCheck()
            {
                bool endGame = false;
                string winner = "";
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    Character character = allPlayers[i];
                    if (character.dead)
                        endGame = true;
                    else
                        winner = character.name;
                }
                if (endGame)
                    return winner;
                else
                    return string.Empty;
            }
        }
    }
}
