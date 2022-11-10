using System;
using System.Numerics;
using AutoBattle.Entities.Actions;

namespace AutoBattle
{
    public static class Constants
    {
        //MAP
        public const int MapWidth = 7;
        public const int MapHeight = 9;

        //CHARACTER
        public const int CharacterBaseHealth = 100;
        public const int CharacterBaseDamage = 20;

        //CLASS PALADIN
        public const float PaladinHPModifier = 1.2f;
        public const float PaladinClassDamage = 1.2f;
        public const int PaladinAttackRange = 1;
        public const int PaladinMovementSpeed = 1;

        public const string PaladinSkillName = "Magic Spear";
        public const float PaladinSkillDamage = 40;
        public const float PaladinSkillDamageMultiplier = 1.2f;

        //CLASS WARRIOR
        public const float WarriorHPModifier = 2f;
        public const float WarriorClassDamage = 1f;
        public const int WarriorAttackRange = 1;
        public const int WarriorMovementSpeed = 1;

        public const string WarriorSkillName = "Ultra Kick";
        public const float WarriorSkillDamage = 40;
        public const float WarriorSkillDamageMultiplier = 1.2f;

        //CLASS ARCHER
        public const float ArcherHPModifier = .8f;
        public const float ArcherClassDamage = 2f;
        public const int ArcherAttackRange = 2;
        public const int ArcherMovementSpeed = 1;

        public const string ArcherSkillName = "Fire Arrow";
        public const float ArcherSkillDamage = 50;
        public const float ArcherSkillDamageMultiplier = 1.2f;

        //CLASS CLERIC
        public const float ClericHPModifier = 1.2f;
        public const float ClericClassDamage = 1f;
        public const int ClericAttackRange = 2;
        public const int ClericMovementSpeed = 1;

        public const string ClericSkillName = "Light Bean";
        public const float ClericSkillDamage = 60;
        public const float ClericSkillDamageMultiplier = 1.2f;

    }
}

