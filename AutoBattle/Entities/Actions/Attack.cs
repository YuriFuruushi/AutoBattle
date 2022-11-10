using System;
using AutoBattle.Map;
using System.Xml.Linq;

namespace AutoBattle.Entities.Actions
{
    public class Attack
    {

        //Apply normal damage to target
        public float Normal(float baseDamage, float classDamage, Character target)
        {
            float hitDamage = RandomExtensions.GetRandomFloat(0, baseDamage * classDamage);
            target.TakeDamage(hitDamage);
            return hitDamage;

        }

        //Apply skill damage to target
        public float Skill(float skillDamage, float skillDamageMultiplier, Character target)
        {
            float hitDamage = RandomExtensions.GetRandomFloat(0, skillDamage * skillDamageMultiplier);
            target.TakeDamage(hitDamage);
            return hitDamage;

        }

        // Check in x and y directions if there is any character close enough to be a target.
        public bool TargetInRange(Character character, int attackRange)
        {
            //Get closest Gridbox in X
            for (int i = -attackRange; i <= attackRange; i++)
            {
                float boxX = character.CurrentBox.Position.X + i;
                //Check if inside the grid
                if (boxX < 0 || boxX >= character.Battlefield.grid.Y)
                    continue;
                //Get closest Gridbox in Y
                for (int j = -attackRange; j <= attackRange; j++)
                {
                    float boxY = character.CurrentBox.Position.Y + j;
                    //Check if inside the grid
                    if (boxY < 0 || boxY >= character.Battlefield.grid.X)
                        continue;

                    int checkIndex = (int)(character.Battlefield.grid.Y * boxY + boxX);

                    //Check if not own grid
                    if (character.CurrentBox.Index == checkIndex || checkIndex < 0 || checkIndex >= character.Battlefield.grids.Length)
                        continue;

                    if (character.Battlefield.grids[checkIndex].Ocupied)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

