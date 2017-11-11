using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;

namespace Puzzle_Barbarian_Invasion.PuzzleSystem
{
    class Grid
    {
        private ContentManager Content;
        private TmxMap Grille;

        public Vector2 _taille { get; private set; }
        private Vector2 _offset;
        private Texture2D _texture;
        private Texture2D _background;//Afficher le background derriere la Grille
        private String _picName;

        public int[][] _cells { get; private set; }

        public Grid(ContentManager content)
        {
            Content = content;

            Initialize();
        }

        private void Initialize()
        {
            // TODO: Ajoutez ici votre code d'initialisation
            Grille = new TmxMap("Content/_tmxGrid_/grid.tmx");
            _picName = Grille.ImageLayers[0].Name;

            _taille = new Vector2(Grille.Width, Grille.Height);
            _cells = new int[(int)_taille.Y][];
            for (int i = 0; i < _taille.Y; i++)
            {
                _cells[i] = new int[(int)_taille.X];
            }

            _offset = new Vector2(Grille.Tilesets[0].TileWidth, Grille.Tilesets[0].TileHeight);

            int nbLayers = Grille.Layers.Count;

            int line = 0;
            int column = 0;

            for (int i = 0; i < Grille.Layers[0].Tiles.Count; i++)
            {
                int gid = Grille.Layers[0].Tiles[i].Gid;

                if (gid == 1)
                {
                    _cells[line][column] = 1;
                }
                else if (gid == 2)
                {
                    _cells[line][column] = -2;
                }
                else
                {
                    _cells[line][column] = -1;
                }

                column++;
                if (column == _taille.X)
                {
                    column = 0;
                    line++;
                }
            }

        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/Case");
            _background = Content.Load<Texture2D>("_images_/"+_picName);
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_background, new Rectangle(0, 0,_background.Width,_background.Height), Color.White);
            for (int j = 0; j < _taille.Y; j++)
            {
                for (int i = 0; i < _taille.X; i++)
                {
                    if (_cells[j][i] >= 0)
                    {
                        spriteBatch.Draw(_texture,
                            new Rectangle(i * (int)_offset.X, j * (int)_offset.Y, (int)_offset.X, (int)_offset.Y),
                            new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y),
                            Color.White);
                    }
                }
            }
        }
    }
}
