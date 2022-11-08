using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        private string name;
        public float health;
        private float baseDamage;
        private float damageMultiplier;
        public GridBox currentBox;
        private int playerIndex;
        public Character target;
        public CharacterClass characterClass;

        public Character(string name, int playerIndex, float health, float baseDamage, CharacterClass characterClass)
        {
            this.name = name;
            this.playerIndex = playerIndex;
            this.health = health;
            this.baseDamage = baseDamage;
            this.characterClass = characterClass;
        }


        public bool TakeDamage(float amount)
        {
            if ((health -= baseDamage) <= 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            //TODO >> maybe kill him?
        }

        public void WalkTO(bool CanWalk)
        {

        }

        private void Move(Grid battlefield, GridBox newBoxPosition)
        {
            currentBox.ocupied = false;
            battlefield.grids[currentBox.Index] = currentBox;
            currentBox = newBoxPosition;
            currentBox.ocupied = true;
            battlefield.grids[newBoxPosition.Index] = currentBox;
        }

        public void StartTurn(Grid battlefield)
        {
            if (target == null)
            {

            }

            if (CheckCloseTargets(battlefield))
            {
                Attack(target);


                return;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (this.currentBox.xIndex > target.currentBox.xIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.Index - 1]);
                    Console.WriteLine($"Player {playerIndex} walked left\n");
                    return;
                }
                else if (currentBox.xIndex < target.currentBox.xIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.Index + 1]);
                    Console.WriteLine($"Player {playerIndex} walked right\n");
                    return;
                }

                if (this.currentBox.yIndex > target.currentBox.yIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.Index - battlefield.xLenght]);
                    Console.WriteLine($"Player {playerIndex} walked up\n");
                    return;
                }
                else if (this.currentBox.yIndex < target.currentBox.yIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.Index + battlefield.xLenght]);
                    Console.WriteLine($"Player {playerIndex} walked down\n");
                    return;
                }

            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            //bool left = battlefield.grids[currentBox.Index - 1].ocupied;
            //bool right = battlefield.grids[currentBox.Index + 1].ocupied;
            //bool up = battlefield.grids[currentBox.Index - battlefield.xLenght].ocupied;
            //bool down = battlefield.grids[currentBox.Index + battlefield.xLenght].ocupied;

            //if (left & right & up & down)
            //{
            //    return true;
            //}
            return false;
        }

        public void Attack(Character target)
        {
            var rand = new Random();
            target.TakeDamage(rand.Next(0, (int)baseDamage));
            Console.WriteLine($"Player {playerIndex} is attacking the player {this.target.playerIndex} and did {baseDamage} damage\n");
        }
    }
}
