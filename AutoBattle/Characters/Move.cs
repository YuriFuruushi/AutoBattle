using System;
using System.Text;
using AutoBattle.Battlefield;

namespace AutoBattle.Characters
{
    public class Move
    {
        private Character character;

        public Move(Character character)
        {
            this.character = character;
        }

        public void To(int newBoxIndex)
        {
            character.battlefield.grids[character.currentBox.index].ocupied = false;
            character.battlefield.grids[newBoxIndex].ocupied = true;
            character.currentBox = character.battlefield.grids[newBoxIndex];
        }

        public string ChaseTarget(Character target)
        {
            int newIndex = character.currentBox.index;

            StringBuilder feedbackMessage = new StringBuilder();

            //Move in X
            if (character.currentBox.index > target.currentBox.index)
            {
                newIndex -= 1;
                feedbackMessage.Append("left/");
            }
            else if (character.currentBox.index < target.currentBox.index)
            {
                newIndex += 1;
                feedbackMessage.Append("right/");
            }

            //Move in Y
            if (character.currentBox.yIndex > target.currentBox.yIndex)
            {
                newIndex -= character.battlefield.yLenght;
                feedbackMessage.Append("up");
            }
            else if (character.currentBox.yIndex < target.currentBox.yIndex)
            {
                newIndex += character.battlefield.yLenght;
                feedbackMessage.Append("down");
            }

            //Feedback Message
            if (feedbackMessage.Length > 0 && feedbackMessage[feedbackMessage.Length - 1] == '/')
                feedbackMessage.Remove(feedbackMessage.Length - 1, 1);

            feedbackMessage.Append(".\n");

            //Move if new Position
            if (newIndex != character.currentBox.index)
            {
                To(newIndex);
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

