using System;
namespace AutoBattle.Characters
{
    public class Sight
    {
        Character character;
        public int sightRange = 1;

        public Sight(Character character)
        {
            this.character = character;
        }

        public Character SearchTarget()
        {

            for (int i = -sightRange; i <= sightRange; i++)
            {
                int boxX = character.currentBox.xIndex + i;
                if (boxX < 0 || boxX >= character.battlefield.yLenght)
                    continue;

                for (int j = -sightRange; j <= sightRange; j++)
                {
                    int boxY = character.currentBox.yIndex + j;
                    if (boxY < 0 || boxY >= character.battlefield.xLenght)
                        continue;

                    int checkIndex = character.battlefield.yLenght * boxY + boxX;

                    if (character.currentBox.index == checkIndex || checkIndex < 0 || checkIndex >= character.battlefield.grids.Length)
                        continue;

                    if (character.battlefield.grids[checkIndex].ocupied)
                    {
                        return null;
                    }
                }
            }

            return null;
        }
    }
}

