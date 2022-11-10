using AutoBattle.Entities.Actions;

namespace AutoBattle.Entities
{
    public struct ClassAttributes
    {
        public readonly CharacterClassEnum CharacterClass;
        public readonly float HpModifier;
        public readonly float ClassDamage;
        public readonly int AttackRange;
        public readonly int MovementSpeed;
        public readonly Skill[] Skills;

        public ClassAttributes(CharacterClassEnum characterClass)
        {
            this.CharacterClass = characterClass;
            Skills = new Skill[1];
            switch (characterClass)
            {
                case CharacterClassEnum.Paladin:
                    HpModifier = Constants.PaladinHPModifier;
                    ClassDamage = Constants.PaladinClassDamage;
                    AttackRange = Constants.PaladinAttackRange;
                    MovementSpeed = Constants.PaladinMovementSpeed;
                    Skills[0] = new Skill(Constants.PaladinSkillName, Constants.PaladinSkillDamage, Constants.PaladinSkillDamageMultiplier);
                    break;
                case CharacterClassEnum.Warrior:
                    HpModifier = Constants.WarriorHPModifier;
                    ClassDamage = Constants.WarriorClassDamage;
                    AttackRange = Constants.WarriorAttackRange;
                    MovementSpeed = Constants.WarriorMovementSpeed;
                    Skills[0] = new Skill(Constants.WarriorSkillName, Constants.WarriorSkillDamage, Constants.WarriorSkillDamageMultiplier);
                    break;
                case CharacterClassEnum.Archer:
                    HpModifier = Constants.ArcherHPModifier;
                    ClassDamage = Constants.ArcherClassDamage;
                    AttackRange = Constants.ArcherAttackRange;
                    MovementSpeed = Constants.ArcherMovementSpeed;
                    Skills[0] = new Skill(Constants.ArcherSkillName, Constants.ArcherSkillDamage, Constants.ArcherSkillDamageMultiplier);
                    break;
                case CharacterClassEnum.Cleric:
                    HpModifier = Constants.ClericHPModifier;
                    ClassDamage = Constants.ClericClassDamage;
                    AttackRange = Constants.ClericAttackRange;
                    MovementSpeed = Constants.ClericMovementSpeed;
                    Skills[0] = new Skill(Constants.ClericSkillName, Constants.ClericSkillDamage, Constants.ClericSkillDamageMultiplier);
                    break;
                default:
                    HpModifier = 1;
                    ClassDamage = 1;
                    AttackRange = 1;
                    MovementSpeed = 1;
                    Skills[0] = new Skill("none", 0, 1);
                    break;
            }
        }
    }

}
