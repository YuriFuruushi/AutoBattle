using System;

namespace AutoBattle.Entities.Senses
{
    public class Sight
    {
        //Get other player other than Self
        public Character SearchTarget(Character character)
        {
            for (int i = 0; i < character.Battlefield.allPlayers.Length; i++)
            {
                if (character == character.Battlefield.allPlayers[i])
                    continue;
                return character.Battlefield.allPlayers[i];
            }
            return null;
        }
    }
}

