using System;
using AutoBattle.Battlefield;
using System.Xml.Linq;

namespace AutoBattle.Characters
{
    public class Attack
    {
        private Character character;
        public int attackRange;

        public Attack(Character character)
        {
            this.character = character;
            attackRange = character.classAttributes.attackRange;
        }

        //Apply damage to target
        public float ApplyDamage(Character target)
        {
            float hitDamage = RandomExtensions.GetRandomFloat(0, character.baseDamage * character.classAttributes.classDamage);
            target.TakeDamage(hitDamage);
            return hitDamage;

        }

        // Check in x and y directions if there is any character close enough to be a target.
        public bool TargetInRange()
        {

            for (int i = -attackRange; i <= attackRange; i++)
            {
                int boxX = character.currentBox.xIndex + i;
                if (boxX < 0 || boxX >= character.battlefield.yLenght)
                    continue;

                for (int j = -attackRange; j <= attackRange; j++)
                {
                    int boxY = character.currentBox.yIndex + j;
                    if (boxY < 0 || boxY >= character.battlefield.xLenght)
                        continue;

                    int checkIndex = character.battlefield.yLenght * boxY + boxX;

                    if (character.currentBox.index == checkIndex || checkIndex < 0 || checkIndex >= character.battlefield.grids.Length)
                        continue;

                    if (character.battlefield.grids[checkIndex].ocupied)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

