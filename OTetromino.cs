using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    internal class OTetromino : Block
    {
        public override int Id => 4;

        private readonly Position[][] tiles = new Position[][]
        {
            new Position[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1) }
        };

        protected override Position StartOffset => new Position(0, 4);

        protected override Position[][] Tiles => tiles;
    }
}
