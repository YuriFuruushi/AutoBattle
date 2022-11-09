using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    public class Grid
    {
        public GridBox[] grids;
        public int xLenght;
        public int yLenght;

        public Grid(int Lines, int Columns)
        {
            xLenght = Lines;
            yLenght = Columns;
            grids = new GridBox[yLenght * xLenght];
            Console.WriteLine("The battle field has been created\n");
            for (int i = 0; i < Lines; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    GridBox newBox = new GridBox(j, i, false, (yLenght * i + j));
                    grids[yLenght * i + j] = newBox;
                    //Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void Draw()
        {
            for (int i = 0; i < xLenght; i++)
            {
                for (int j = 0; j < yLenght; j++)
                {
                    GridBox currentgrid = grids[yLenght * i + j];
                    if (grids[yLenght * i + j].ocupied)
                    {
                        //if()
                        Console.Write("<X>\t");
                    }
                    else
                    {
                        Console.Write($"[" + currentgrid.index + "]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

    }
}
