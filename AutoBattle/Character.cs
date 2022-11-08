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
            battlefield.grids[currentBox.index] = currentBox;
            currentBox = newBoxPosition;
            currentBox.ocupied = true;
            battlefield.grids[newBoxPosition.index] = currentBox;
        }

        public bool StartTurn(Grid battlefield)
        {
            if (CheckCloseTargets(battlefield))
            {
                Attack(target);


                return false;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target
                if (this.currentBox.xIndex > target.currentBox.xIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.index - 1]);
                    Console.WriteLine($"Player {playerIndex} walked left\n");
                    return true;
                }
                else if (currentBox.xIndex < target.currentBox.xIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.index + 1]);
                    Console.WriteLine($"Player {playerIndex} walked right\n");
                    return true;
                }

                if (this.currentBox.yIndex > target.currentBox.yIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.index - battlefield.yLenght]);
                    Console.WriteLine($"Player {playerIndex} walked up\n");
                    return true;
                }
                else if (this.currentBox.yIndex < target.currentBox.yIndex)
                {
                    Move(battlefield, battlefield.grids[currentBox.index + battlefield.yLenght]);
                    Console.WriteLine($"Player {playerIndex} walked down\n");
                    return true;
                }
                return false;
            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            bool left = false;
            if (currentBox.index > 1)
                left = battlefield.grids[currentBox.index - 1].ocupied;

            bool right = false;
            if (currentBox.index < battlefield.grids.Length - 1)
                right = battlefield.grids[currentBox.index + 1].ocupied;

            bool up = false;
            if (currentBox.index > battlefield.yLenght)
                up = battlefield.grids[currentBox.index - battlefield.yLenght].ocupied;

            bool down = false;
            if (currentBox.index + battlefield.yLenght < battlefield.grids.Length - 1)
                down = battlefield.grids[currentBox.index + battlefield.yLenght].ocupied;

            if (left || right || up || down)
            {
                return true;
            }
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
