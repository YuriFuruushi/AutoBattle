using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid battlefield = new Grid(5, 10);
            Character[] AllPlayers = new Character[2];
            int currentTurn = 0;
            Setup();


            void Setup()
            {
                //Create Player Character
                GetPlayerChoice();
                //Create Enemy Character
                CreateCharacter("Enemy", 1);
                StartGame();
            }

            void GetPlayerChoice()
            {
                //asks for the player to choose between for possible classes via console.
                Console.WriteLine("Choose Between One of this Classes:\n");
                Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
                //store the player choice in a variable
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        CreateCharacter("Player", 0, Int32.Parse(choice));
                        break;
                    default:
                        GetPlayerChoice();
                        break;
                }

            }

            void CreateCharacter(string name, int id, int classIndex = -1)
            {
                int charClass = classIndex;

                if (charClass < 1 || charClass > 4)
                    charClass = GetRandomInt(1, 4);


                Character character = new Character(name, id, 100, 20, (CharacterClass)charClass);
                Console.WriteLine($"Player {id} Class Choice: {(CharacterClass)charClass}");
                AllPlayers[id] = (character);
            }

            void StartGame()
            {
                //populates the character variables and targets
                AllPlayers[0].target = AllPlayers[1];
                AllPlayers[1].target = AllPlayers[0];
                AllocatePlayers();
                StartTurn();
            }

            void AllocatePlayers()
            {
                for (int i = 0; i < AllPlayers.Length; i++)
                {
                    Character character = AllPlayers[i];
                    //Allocate players opposite directions
                    AllocateCharacter(character, i * 49);
                }
            }

            void AllocateCharacter(Character character, int locationIndex = -1)
            {
                int spawnLocation = locationIndex;
                if (spawnLocation == -1)
                {
                    spawnLocation = GetRandomInt(0, battlefield.grids.Length);
                    while (battlefield.grids[spawnLocation].ocupied)
                    {
                        spawnLocation = GetRandomInt(0, battlefield.grids.Length);
                    }
                }
                battlefield.grids[spawnLocation].ocupied = true;
                character.currentBox = battlefield.grids[spawnLocation];
            }

            void StartTurn()
            {
                bool reDrawBattlefield = false;
                for (int i = 0; i < AllPlayers.Length; i++)
                {
                    Character character = AllPlayers[i];
                    reDrawBattlefield = character.StartTurn(battlefield);
                }

                if (reDrawBattlefield)
                    battlefield.Draw();

                currentTurn++;
                HandleTurn();
            }

            void HandleTurn()
            {
                //if (PlayerCharacter.health == 0)
                //{
                //    return;
                //}
                //else if (EnemyCharacter.health == 0)
                //{
                //    Console.Write(Environment.NewLine + Environment.NewLine);

                //    // endgame?

                //    Console.Write(Environment.NewLine + Environment.NewLine);

                //    return;
                //}
                //else
                {
                    Console.Write(Environment.NewLine + Environment.NewLine);
                    Console.WriteLine("Click on any key to start the next turn...\n");
                    Console.Write(Environment.NewLine + Environment.NewLine);

                    ConsoleKeyInfo key = Console.ReadKey();
                    StartTurn();
                }
            }





            int GetRandomInt(int min, int max)
            {
                var rand = new Random();
                int index = rand.Next(min, max);
                return index;
            }
        }
    }
}
