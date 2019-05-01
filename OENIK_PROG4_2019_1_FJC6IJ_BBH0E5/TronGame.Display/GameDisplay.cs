namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TronGame.Model;
    using TronGame.Repository;

    /// <summary>
    /// Makes the textures, and clculates the positons on the screen.
    /// </summary>
    public class GameDisplay : FrameworkElement
    {
        private IGameModel model;
        private ImageBrush player1Brush;
        private ImageBrush player2Brush;
        private ImageBrush obstacleBrush;
        private ImageBrush turboBrush;
        private double width;
        private double height;
        private double tileSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameDisplay"/> class.
        /// </summary>
        /// <param name="model">IGameModel object</param>
        /// <param name="width">Width of field</param>
        /// <param name="height">Height of field</param>
        public GameDisplay(IGameModel model, double width, double height)
        {
            this.model = model;
            this.width = width;
            this.height = height;
            this.tileSize = 20;

            this.player1Brush = this.GetPlayerBrush(@"..\..\Images\player1.png");
            this.player2Brush = this.GetPlayerBrush(@"..\..\Images\player2.png");
            this.obstacleBrush = this.GetObjectBrush(@"..\..\Images\obstacle.png");
            this.turboBrush = this.GetObjectBrush(@"..\..\Images\turbo.png");
        }

        /// <summary>
        /// Collect the object positions
        /// </summary>
        /// <returns>DrawingGroup object with GameObject textures and positions</returns>
        public Drawing GetDrawings()
        {
            DrawingGroup dg = new DrawingGroup();

            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetPlayer1Route());
            dg.Children.Add(this.GetPlayer2Route());
            dg.Children.Add(this.GetTurbos());
            dg.Children.Add(this.GetObstacles());
            dg.Children.Add(this.GetPlayer(this.model.Player1, this.player1Brush));
            dg.Children.Add(this.GetPlayer(this.model.Player2, this.player2Brush));

            return dg;
        }

        private ImageBrush GetPlayerBrush(string filename)
        {
            ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute)));

            imageBrush.TileMode = TileMode.Tile;
            imageBrush.Viewport = new Rect(0, 0, this.tileSize, this.tileSize);
            imageBrush.ViewportUnits = BrushMappingMode.Absolute;

            return imageBrush;
        }

        private ImageBrush GetObjectBrush(string filename)
        {
            ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(filename, UriKind.RelativeOrAbsolute)));

            imageBrush.TileMode = TileMode.Tile;
            imageBrush.Viewport = new Rect(0, 0, this.tileSize, this.tileSize);
            imageBrush.ViewportUnits = BrushMappingMode.Absolute;

            return imageBrush;
        }

        private Drawing GetBackground()
        {
            Geometry g = new RectangleGeometry(new Rect(0, 0, this.width, this.height));

            return new GeometryDrawing(Brushes.Black, null, g);
        }

        private Drawing GetPlayer(Player player, ImageBrush brush)
        {
            Geometry g = new RectangleGeometry(new Rect(player.Point.X * this.tileSize, player.Point.Y * this.tileSize, this.tileSize, this.tileSize));

            return new GeometryDrawing(brush, null, g);
        }

        private Drawing GetTurbos()
        {
            GeometryGroup g = new GeometryGroup();

            foreach (TurboObject item in this.model.Turbos)
            {
                RectangleGeometry rg = new RectangleGeometry(new Rect(item.Point.X * this.tileSize, item.Point.Y * this.tileSize, this.tileSize, this.tileSize));
                g.Children.Add(rg);
            }

            return new GeometryDrawing(this.turboBrush, null, g);
        }

        private Drawing GetObstacles()
        {
            GeometryGroup g = new GeometryGroup();

            foreach (ObstacleObject item in this.model.Obstacles)
            {
                RectangleGeometry rg = new RectangleGeometry(new Rect(item.Point.X * this.tileSize, item.Point.Y * this.tileSize, this.tileSize, this.tileSize));
                g.Children.Add(rg);
            }

            return new GeometryDrawing(this.obstacleBrush, null, g);
        }

        private Drawing GetPlayer1Route()
        {
            GeometryGroup g = new GeometryGroup();

            for (int i = 0; i < this.model.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < this.model.GameField.GetLength(1); j++)
                {
                    if (this.model.GameField[i, j] != null && this.model.GameField[i, j] == this.model.Player1)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(j * this.tileSize, i * this.tileSize, this.tileSize, this.tileSize));

                        g.Children.Add(rg);
                    }
                }
            }

            return new GeometryDrawing(Brushes.Green, null, g);
        }

        private Drawing GetPlayer2Route()
        {
            GeometryGroup g = new GeometryGroup();

            for (int i = 0; i < this.model.GameField.GetLength(0); i++)
            {
                for (int j = 0; j < this.model.GameField.GetLength(1); j++)
                {
                    if (this.model.GameField[i, j] != null && this.model.GameField[i, j] == this.model.Player2)
                    {
                        RectangleGeometry rg = new RectangleGeometry(new Rect(j * this.tileSize, i * this.tileSize, this.tileSize, this.tileSize));

                        g.Children.Add(rg);
                    }
                }
            }

            return new GeometryDrawing(Brushes.Blue, null, g);
        }

    }
}
