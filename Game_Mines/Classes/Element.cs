using System;

namespace Game_Mines.Classes
{
    class Element
    {
        private static Random _random = new Random();
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsBomb { get; set; }
        public bool IsShow { get; set; }
        public bool IsMarkerAsBomb { get; set; }

        public Element()
        {
            var chanse = (double)Options.Bombs / (Options.MapWidthCount * Options.MapHeightCount) * 100;

            var bombChanse = _random.Next(1, 101);
            if (bombChanse < chanse) IsBomb = true;

        }
    }
}
