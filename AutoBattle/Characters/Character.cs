using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoBattle.Battlefield;

namespace AutoBattle.Characters
{
    public class Character
    {
        public int playerIndex;
        public string name;

        public float maxHealth;
        public float currentHealth;

        public float baseDamage;
        public float damageMultiplier;
        public GridBox currentBox;

        public Character target;
        public ClassAttributes classAttributes;
        public bool dead = false;
        public Grid battlefield;

        private Move move;
        private Attack attack;

        public Character(string name, int playerIndex, float maxHealth, float baseDamage, CharacterClassEnum characterClass)
        {
            this.name = name;
            this.playerIndex = playerIndex;
            this.baseDamage = baseDamage;
            this.classAttributes = new ClassAttributes(characterClass);
            this.maxHealth = maxHealth * classAttributes.hpModifier;
            currentHealth = maxHealth;
            move = new Move(this);
            attack = new Attack(this);
        }

        public void Spawn(Grid battlefield, int locationIndex = -1)
        {
            this.battlefield = battlefield;
            int spawnLocation = locationIndex;
            if (spawnLocation == -1)
            {
                spawnLocation = RandomExtensions.GetRandomInt(0, this.battlefield.grids.Length);
                while (this.battlefield.grids[spawnLocation].ocupied)
                {
                    spawnLocation = RandomExtensions.GetRandomInt(0, this.battlefield.grids.Length);
                }
            }
            this.battlefield.grids[spawnLocation].ocupied = true;
            currentBox = this.battlefield.grids[spawnLocation];
        }


        public bool TakeDamage(float amount)
        {
            currentHealth = Math.Clamp(currentHealth - amount, 0, maxHealth);
            currentHealth = (float)Math.Round(currentHealth, 1);
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

        public string Behavior()
        {
            StringBuilder feedbackMessage = new StringBuilder();
            if (dead)
            {
                feedbackMessage.Append($"{name} is Dead.\n");
                return feedbackMessage.ToString();
            }

            if (attack.TargetInRange())
            {
                float damageDealt = attack.ApplyDamage(target);
                feedbackMessage.Append($"{name} is attacking {target.name} and did {damageDealt} damage. {target.name} health is {target.currentHealth}.\n");
                return feedbackMessage.ToString();
            }
            else
            {
                feedbackMessage.Append($"{name} walked ");

                string moveResult = move.ChaseTarget(target);

                if (!string.IsNullOrEmpty(moveResult))
                {
                    feedbackMessage.Append(moveResult);
                    return feedbackMessage.ToString();
                }
                else
                {
                    return feedbackMessage.ToString();
                }
            }
        }


    }
}
