using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using AutoBattle.Entities;

namespace AutoBattle.Map
{
    public class Battlefield
    {
        public readonly GridBox[] grids;
        public readonly Vector2 grid = new Vector2();
        public Character[] allPlayers = new Character[2];

        public Battlefield(Vector2 grid)
        {
            this.grid = grid;
            grids = new GridBox[(int)grid.X * (int)grid.Y];
            for (int i = 0; i < grid.X; i++)
            {
                for (int j = 0; j < grid.Y; j++)
                {

                    GridBox newBox = new GridBox(new Vector2(j, i), false, (int)(grid.Y * i + j));
                    grids[(int)grid.Y * i + j] = newBox;
                    //Console.Write($"{newBox.Index}\n");
                }
            }
        }

        // prints the matrix that indicates the tiles of the battlefield
        public void Draw()
        {
            for (int i = 0; i < grid.X; i++)
            {
                for (int j = 0; j < grid.Y; j++)
                {
                    GridBox currentgrid = grids[(int)grid.Y * i + j];
                    if (grids[(int)grid.Y * i + j].Ocupied)
                    {
                        Console.Write("<X>\t");
                    }
                    else
                    {
                        Console.Write($"[ ]\t");
                    }
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(Environment.NewLine);
        }
    }
}
