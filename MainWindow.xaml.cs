using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileEmpty.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileCyan.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileBlue.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileOrange.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileYellow.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileGreen.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TilePurple.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\TileRed.png", UriKind.Absolute)),
        };

        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-Empty.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-I.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-J.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-L.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-O.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-S.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-T.png", UriKind.Absolute)),
            new BitmapImage(new Uri("D:\\Programming\\C#\\TetrisWPF\\TetrisWPF\\Assets\\Block-Z.png", UriKind.Absolute)),
        };

        private readonly Image[,] imageControls;

        private GameState gameState = new GameState();

        public MainWindow()
        {
            InitializeComponent();
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            //250, 500 to visible 25px
            int cellSize = 25;

            for(int row = 0; row < grid.Rows; row++)
            {
                for(int col = 0; col < grid.Columns; col++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };

                    Canvas.SetTop(imageControl, (row - 2) * cellSize);
                    Canvas.SetLeft(imageControl, col * cellSize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[row, col] = imageControl;
                }
            }
            return imageControls;
        }

        private void DrawGrid(GameGrid grid)
        {
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int col = 0; col < grid.Columns; col++)
                {
                    int id = grid[row, col];
                    imageControls[row, col].Source = tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach(Position p in block.TilePositions())
            {
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
        }

        private async Task GameLoop()
        {
            Draw(gameState);

            while (!gameState.GameOver)
            {
                await Task.Delay(500);
                gameState.MoveBlockHorizontal();
                Draw(gameState);
            }

            GameOverMenu.Visibility = Visibility.Visible;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockDiagonal(0, -1);
                    break;
                case Key.Right:
                    gameState.MoveBlockDiagonal(0, 1);
                    break;
                case Key.Down:
                    gameState.MoveBlockHorizontal();
                    break;
                case Key.Up:
                    gameState.RotateBlockClockwise();
                    break;
                case Key.Z:
                    gameState.RotateBlockCounterClockwise();
                    break;
                default:
                    return;
            }

            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}
