using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoBattle.Map;
using AutoBattle.Entities.Senses;
using AutoBattle.Entities.Actions;

namespace AutoBattle.Entities
{
    public class Character
    {
        public readonly string Name;
        private readonly int playerIndex;

        private readonly float maxHealth;
        private float currentHealth;

        private readonly float baseDamage;
        public GridBox CurrentBox;

        private Character target;
        public readonly ClassAttributes ClassAttributes;
        public bool Dead = false;
        public Battlefield Battlefield;

        private readonly Move move;
        private readonly Attack attack;
        private readonly Sight sight;

        public Character(string name, int playerIndex, float maxHealth, float baseDamage, CharacterClassEnum characterClass)
        {
            this.Name = name;
            this.playerIndex = playerIndex;
            this.baseDamage = baseDamage;
            this.ClassAttributes = new ClassAttributes(characterClass);
            this.maxHealth = maxHealth * ClassAttributes.HpModifier;
            currentHealth = maxHealth;
            move = new Move();
            attack = new Attack();
            sight = new Sight();
        }

        //Allocate Characters in the Battlefield (-1 if random location)
        public void Spawn(Battlefield battlefield, int locationIndex = -1)
        {
            this.Battlefield = battlefield;
            int spawnLocation = locationIndex;
            if (spawnLocation == -1)
            {
                spawnLocation = RandomExtensions.GetRandomInt(0, this.Battlefield.grids.Length);
                while (this.Battlefield.grids[spawnLocation].Ocupied)
                {
                    spawnLocation = RandomExtensions.GetRandomInt(0, this.Battlefield.grids.Length);
                }
            }
            this.Battlefield.grids[spawnLocation].Ocupied = true;
            CurrentBox = this.Battlefield.grids[spawnLocation];
        }

        //Receive Damage
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

        //Kill character
        public void Die()
        {
            Dead = true;
        }

        //Check if there is a close target and attack OR move closer to target
        public string Behavior()
        {
            StringBuilder feedbackMessage = new StringBuilder();

            //Check if Dead
            if (Dead)
            {
                feedbackMessage.Append($"{Name} is Dead.\n");
                return feedbackMessage.ToString();
            }

            target = sight.SearchTarget(this);
            if (target == null)
                return feedbackMessage.ToString();

            //Check if Target in Range
            if (attack.TargetInRange(this, ClassAttributes.AttackRange))
            {
                float damageDealt = 0;
                //Chance of using Skill over Normal
                if (RandomExtensions.GetRandomInt(0, 5) == 0)
                {
                    damageDealt = attack.Skill(ClassAttributes.Skills[0].Damage, ClassAttributes.Skills[0].DamageMultiplier, target);
                    feedbackMessage.Append($"{Name} used skill {ClassAttributes.Skills[0].Name} on {target.Name} ");
                }
                else
                {
                    damageDealt = attack.Normal(baseDamage, ClassAttributes.ClassDamage, target);
                    feedbackMessage.Append($"{Name} is attacking {target.Name} ");
                }

                feedbackMessage.Append($"and did {damageDealt} damage. {target.Name} health is {target.currentHealth}.\n");
                return feedbackMessage.ToString();
            }
            else
            {
                //Move towards target
                string moveResult = move.ChaseTarget(this, target, ClassAttributes.MovementSpeed);

                if (!string.IsNullOrEmpty(moveResult))
                {
                    feedbackMessage.Append($"{Name} walked ");
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
