namespace AutoBattle.Characters
{
    public struct ClassAttributes
    {
        public CharacterClassEnum characterClass;
        public float hpModifier;
        public float classDamage;
        public int attackRange;
        Skill[] skills;

        public ClassAttributes(CharacterClassEnum characterClass)
        {
            this.characterClass = characterClass;
            skills = new Skill[1];
            switch (characterClass)
            {
                case CharacterClassEnum.Paladin:
                    hpModifier = 1.2f;
                    classDamage = 1.2f;
                    attackRange = 1;
                    skills[0] = new Skill("Magic Spear", 50, 1.2f);
                    break;
                case CharacterClassEnum.Warrior:
                    hpModifier = 2f;
                    classDamage = 1f;
                    attackRange = 1;
                    skills[0] = new Skill("Ultra Kick", 50, 1.2f);
                    break;
                case CharacterClassEnum.Archer:
                    hpModifier = .8f;
                    classDamage = 2f;
                    attackRange = 2;
                    skills[0] = new Skill("Fire Arrow", 50, 1.2f);
                    break;
                case CharacterClassEnum.Cleric:
                    hpModifier = 1f;
                    classDamage = 1f;
                    attackRange = 2;
                    skills[0] = new Skill("Light Bean", 50, 1.2f);
                    break;
                default:
                    hpModifier = 1f;
                    classDamage = 1f;
                    attackRange = 1;
                    skills[0] = new Skill("none", 0, 1f);
                    break;
            }
        }
    }

}
