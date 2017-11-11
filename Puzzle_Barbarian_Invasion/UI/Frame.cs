using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.UI
{
    class Frame
    {
        private Texture2D _texture;

        private int _x;
        private int _y;
        public int _width { get; private set; }
        public int _height { get; private set; }
        public String _name { get; private set; }

        public Frame(Texture2D texture,int x, int y, int width, int height,String name)
        {
            _texture = texture;

            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _name = name;
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch,Vector2 position)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)position.Y, _width, _height),
                new Rectangle(_x, _y, _width, _height), Color.White);
        }
    }
}
