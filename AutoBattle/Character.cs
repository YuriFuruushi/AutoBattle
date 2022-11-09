﻿using System;
using System.Collections.Generic;
using System.Text;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Character
    {
        public string name;
        public float currentHealth;
        public float maxHealth;
        private float baseDamage;
        private float damageMultiplier;
        public GridBox currentBox;
        public int playerIndex;
        public Character target;
        public CharacterClass characterClass;
        public bool dead = false;

        public Character(string name, int playerIndex, float maxHealth, float baseDamage, CharacterClass characterClass)
        {
            this.name = name;
            this.playerIndex = playerIndex;
            this.baseDamage = baseDamage;
            this.characterClass = characterClass;
            this.maxHealth = maxHealth;
            currentHealth = maxHealth;
        }


        public bool TakeDamage(float amount)
        {
            currentHealth = Math.Clamp(currentHealth - amount, 0, maxHealth);
            if (currentHealth == 0)
            {
                Die();
                return true;
            }
            return false;
        }

        public void Die()
        {
            dead = true;
        }

        private void Move(Grid battlefield, int newBoxIndex)
        {
            battlefield.grids[currentBox.index].ocupied = false;
            battlefield.grids[newBoxIndex].ocupied = true;
            currentBox = battlefield.grids[newBoxIndex];
        }

        public bool StartTurn(Grid battlefield)
        {
            if (dead)
                return false;

            if (CheckCloseTargets(battlefield))
            {
                Attack(target);


                return false;
            }
            else
            {   // if there is no target close enough, calculates in wich direction this character should move to be closer to a possible target

                int newIndex = currentBox.index;

                StringBuilder feedbackMessage = new StringBuilder($"Player {playerIndex} walked ");

                //Move in X
                if ((currentBox.xIndex % battlefield.xLenght) > (target.currentBox.xIndex % battlefield.xLenght))
                {
                    newIndex -= 1;
                    feedbackMessage.Append("left/");
                }
                else if ((currentBox.xIndex % battlefield.xLenght) < (target.currentBox.xIndex % battlefield.xLenght))
                {
                    newIndex += 1;
                    feedbackMessage.Append("right/");
                }

                //Move in Y
                if (currentBox.yIndex > target.currentBox.yIndex)
                {
                    newIndex -= battlefield.yLenght;
                    feedbackMessage.Append("up");
                }
                else if (currentBox.yIndex < target.currentBox.yIndex)
                {
                    newIndex += battlefield.yLenght;
                    feedbackMessage.Append("down");
                }

                //Feedback Message Adjust
                if (feedbackMessage[feedbackMessage.Length - 1] == '/')
                    feedbackMessage.Remove(feedbackMessage.Length - 1, 1);

                feedbackMessage.Append(".");


                if (newIndex != currentBox.index)
                {
                    Move(battlefield, newIndex);
                    Console.WriteLine(feedbackMessage);
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        // Check in x and y directions if there is any character close enough to be a target.
        bool CheckCloseTargets(Grid battlefield)
        {
            //int attackRange = 1;
            //for (int i = currentBox.xIndex - attackRange; i <= currentBox.xIndex + attackRange; i++)
            //{
            //    if (i < 0 || i > battlefield.xLenght)
            //        continue;

            //    for (int j = currentBox.yIndex - attackRange; j <= currentBox.yIndex + attackRange; j++)
            //    {
            //        if (j < 0 || j > battlefield.yLenght)
            //            continue;

            //        int index = battlefield.yLenght * i + j;


            //        if (index != currentBox.index && index > 0 && index < battlefield.grids.Length)
            //        {
            //            Console.WriteLine($"{index}");
            //            if (battlefield.grids[index].ocupied)
            //            {

            //                return true;
            //            }
            //        }
            //    }
            //}
            return false;
        }

        public void Attack(Character target)
        {
            var rand = new Random();
            int hitDamage = rand.Next(0, (int)baseDamage);
            target.TakeDamage(hitDamage);
            Console.WriteLine($"{name} is attacking {this.target.name} and did {hitDamage} damage. {this.target.name} health is {this.target.currentHealth}.\n");
        }
    }
}
