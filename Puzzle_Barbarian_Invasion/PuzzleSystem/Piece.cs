using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.PuzzleSystem
{
    class Piece
    {
        private ContentManager Content;

        private Vector2 _offset;
        public Vector2 _position { get; set; }
        private Texture2D _texture;
        private Rectangle _source;
        public String _color { get; private set; }

        public Piece(ContentManager content,Vector2 Position, int colorID)
        {
            Content = content;

            Initialize(Position, colorID);
        }

        public Piece(ContentManager content, Vector2 Position, string color)
        {
            Content = content;

            Initialize(Position, color);
        }

        private void Initialize(Vector2 Position, int colorID)
        {
            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);

            _position = Position;

            switch (colorID)
            {
                case 0:
                    _source = new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y);
                    _color = "blue";
                    break;
                case 1:
                    _source = new Rectangle((int)_offset.X, 0, (int)_offset.X, (int)_offset.Y);
                    _color = "green";
                    break;
                case 2:
                    _source = new Rectangle((int)_offset.X * 2, 0, (int)_offset.X, (int)_offset.Y);
                    _color = "red";
                    break;
            }
            LoadContent();
        }

        private void Initialize(Vector2 Position, string color)
        {
            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);

            _position = Position;
            _color = color;

            switch (_color)
            {
                case "blue":
                    _source = new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y);
                    break;
                case "green":
                    _source = new Rectangle((int)_offset.X, 0, (int)_offset.X, (int)_offset.Y);
                    break;
                case "red":
                    _source = new Rectangle((int)_offset.X * 2, 0, (int)_offset.X, (int)_offset.Y);
                    break;
            }
            LoadContent();
        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/pieces");
        }

        public bool RangeEqual(Vector2 position)
        {
            if (position.X >= _position.X && position.X < _position.X + _offset.X
                && position.Y >= _position.Y && position.Y < _position.Y + _offset.Y)
            {
                return true;
            }
            return false;
        }

        public bool RangeEqual(int x, int y)
        {
            if (x > _position.X && x < _position.X + _offset.X
                && y > _position.Y && y < _position.Y + _offset.Y)
            {
                return true;
            }
            return false;
        }

        public bool PositionEqual(Vector2 position)
        {
            if (position == _position)
            {
                return true;
            }
            return false;
        }

        public bool PositionEqual(int x, int y)
        {
            if (new Vector2(x, y) == _position)
            {
                return true;
            }
            return false;
        }

        public bool ColorEqual(string color)
        {
            if (color == _color)
            {
                return true;
            }
            return false;
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, (int)_offset.X, (int)_offset.Y), _source, Color.White);
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            // TODO: write your implementation of Equals() here
            Piece p = obj as Piece;
            if (!_texture.Equals(p._texture))
            {
                Console.WriteLine("TEXTURE FAUX");
                return false;
            }
            if (!_position.Equals(p._position))
            {
                return false;
            }
            if (!_color.Equals(p._color))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            // Some comment to explain if there is a real problem with providing GetHashCode() 
            // or if I just don't see a need for it for the given class
            throw new Exception("Sorry I don't know what GetHashCode should do for this class");
        }


    }
}
