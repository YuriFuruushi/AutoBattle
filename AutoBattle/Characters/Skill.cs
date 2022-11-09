namespace AutoBattle.Characters
{
    public struct Skill
    {
        string name;
        float damage;
        float damageMultiplier;

        public Skill(string name, float damage, float damageMultiplier)
        {
            this.name = name;
            this.damage = damage;
            this.damageMultiplier = damageMultiplier;
        }
    }

}
