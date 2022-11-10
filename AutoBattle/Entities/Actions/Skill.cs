namespace AutoBattle.Entities.Actions
{
    public struct Skill
    {
        public readonly string Name;
        public readonly float Damage;
        public readonly float DamageMultiplier;

        public Skill(string name, float damage, float damageMultiplier)
        {
            this.Name = name;
            this.Damage = damage;
            this.DamageMultiplier = damageMultiplier;
        }
    }

}
