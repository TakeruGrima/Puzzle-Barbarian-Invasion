using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Puzzle_Barbarian_Invasion.TacticalSystem
{
    class Map
    {
        private ContentManager Content;

        private TmxMap _mapTMX;
        private Texture2D _tileset;

        private int _tileWidth;
        private int _tileHeight;
        public int _mapWidth;
        public int _mapHeight;
        private int _tilesetColums;
        private int _tilesetLines;

        public Vector2 _tailleEnPixel { get; private set; }
        public int _posStart { get; private set; }

        public Map(ContentManager content,int positionStart)
        {
            Content = content;

            Initialize(positionStart);
        }

        private void Initialize(int positionStart)
        {
            // TODO: Ajoutez ici votre code d'initialisation

            _mapTMX = new TmxMap("Content/_tmxMap_/map.tmx");

            _posStart = positionStart;

            _tileWidth = _mapTMX.Tilesets[0].TileWidth;
            _tileHeight = _mapTMX.Tilesets[0].TileHeight;

            _mapWidth = _mapTMX.Width;
            _mapHeight = _mapTMX.Height;

            _tailleEnPixel = new Vector2(_mapWidth * _tileWidth, _mapHeight * _tileHeight);
        }

        public void LoadContent()
        {
            _tileset = Content.Load<Texture2D>("_images_/" + _mapTMX.Tilesets[0].Name);

            _tilesetColums = _tileset.Width / _tileWidth;
            _tilesetLines = _tileset.Height / _tileHeight;
        }

        public int getGid(double x, double y)
        {
            int column = (int)x / _tileWidth;
            int line = (int)y / _tileHeight;

            int count = _mapWidth * line + column;

            return _mapTMX.Layers[1].Tiles[count].Gid;
        }

        //Méthode Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            int nbLayers = _mapTMX.Layers.Count;

            int line;
            int column;

            for (int nLayer = 0; nLayer < nbLayers; nLayer++)
            {
                line = 0;
                column = 0;

                for (int i = 0; i < _mapTMX.Layers[nLayer].Tiles.Count; i++)
                {
                    int gid = _mapTMX.Layers[nLayer].Tiles[i].Gid;

                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int tilesetColumn = tileFrame % _tilesetColums;
                        int tilesetLine = (int)Math.Floor((double)tileFrame / (double)_tilesetColums);

                        float x = column * _tileWidth;
                        float y = line * _tileHeight;

                        Rectangle tilesetRec = new Rectangle(_tileWidth * tilesetColumn, _tileHeight * tilesetLine, _tileWidth, _tileHeight);

                        Vector2 position = new Vector2(_posStart + x, y);
                        spriteBatch.Draw(_tileset, position, tilesetRec, Color.White);
                    }

                    column++;
                    if (column == _mapWidth)
                    {
                        column = 0;
                        line++;
                    }
                }
            }
        }
    }
}
