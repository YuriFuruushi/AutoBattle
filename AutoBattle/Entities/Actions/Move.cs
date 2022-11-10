using System;
using System.Text;
using System.Numerics;

namespace AutoBattle.Entities.Actions
{
    public class Move
    {
        //Move to a new GridBox
        public void To(Character character, int newBoxIndex)
        {
            character.Battlefield.grids[character.CurrentBox.Index].Ocupied = false;
            character.Battlefield.grids[newBoxIndex].Ocupied = true;
            character.CurrentBox = character.Battlefield.grids[newBoxIndex];
        }

        //Move towards target position
        public string ChaseTarget(Character character, Character target, int speed)
        {
            //Get the direction of the target
            Vector2 direction = Vector2.Normalize(target.CurrentBox.Position - character.CurrentBox.Position);
            StringBuilder feedbackMessage = new StringBuilder();

            //Check Direction and Normalize Speed
            Vector2 newPos = new Vector2();
            if (direction.X > 0)
            {
                newPos.X = speed;
                feedbackMessage.Append("right/");
            }
            else if (direction.X < 0)
            {
                newPos.X = -speed;
                feedbackMessage.Append("left/");
            }
            if (direction.Y > 0)
            {
                newPos.Y = speed;
                feedbackMessage.Append("down");
            }
            else if (direction.Y < 0)
            {
                newPos.Y = -speed;
                feedbackMessage.Append("up");
            }

            newPos += character.CurrentBox.Position;

            int newIndex = (int)(character.Battlefield.grid.Y * newPos.Y + newPos.X);

            //Feedback Message
            if (feedbackMessage.Length > 0 && feedbackMessage[feedbackMessage.Length - 1] == '/')
                feedbackMessage.Remove(feedbackMessage.Length - 1, 1);

            feedbackMessage.Append(".\n");

            //Move if new Position
            if (newIndex != character.CurrentBox.Index)
            {
                To(character, newIndex);
                //Console.WriteLine(feedbackMessage);
                return feedbackMessage.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

