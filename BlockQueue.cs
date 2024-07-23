using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisWPF
{
    internal class BlockQueue
    {
        private readonly Block[] blocks = new Block[]
        {
            new ITetromino(),
            new JTetromino(),
            new LTetromino(),
            new STetromino(),
            new ZTetromino(),
            new TTetromino(),
            new OTetromino()
        };

        private readonly Random random = new Random();

        public Block NextBlock { get; private set; }

        public BlockQueue()
        {
            NextBlock = RandomBlock();
        }


        private Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block GetAndUpdate()
        {
            Block block = NextBlock;

            // Picking next block until its different than current one.
            do
            {
                NextBlock = RandomBlock();
            } while (block.Id == NextBlock.Id);
            return block;
        }
    }
}
