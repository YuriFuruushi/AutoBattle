using System.Numerics;

namespace AutoBattle.Map
{
    public struct GridBox
    {
        public readonly Vector2 Position;
        public readonly int Index;
        public bool Ocupied;

        public GridBox(Vector2 position, bool ocupied, int index)
        {
            this.Position = position;
            this.Ocupied = ocupied;
            this.Index = index;
        }

    }
}