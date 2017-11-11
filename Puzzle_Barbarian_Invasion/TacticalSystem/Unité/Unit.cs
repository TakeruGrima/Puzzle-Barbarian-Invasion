using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DefineZone;
using Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.Unité
{
    class Unit
    {
        private ContentManager Content;

        private Vector2 _offset;
        public Vector2 _position { get; set; }
        public Texture2D _texture { get; set; }
        private Rectangle _source;

        public int _rayon { get; protected set; }

        public bool _placer=false;

        public Unit(ContentManager content,Vector2 position, int rayon)
        {
            Content = content;

            _position = position;
            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);
            _source = new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y);

            _rayon = rayon;
        }

        public Unit(ContentManager content,int xTab, int yTab, int rayon)
        {
            Content = content;

            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);
            _position = new Vector2(xTab * _offset.X, yTab * _offset.Y);
            _source = new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y);

            _rayon = rayon;
        }

        public Unit(Unit unit)
        {
            _texture = unit._texture;
            _offset = unit._offset;
            _position = unit._position;
            _source = unit._source;

            _rayon = unit._rayon;
        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/unit");
        }

        public bool PositionEqual(double x, double y)
        {
            if (!(x > _position.X && x < _position.X + _offset.X))
            {
                return false;
            }
            if (!(y > _position.Y && y < _position.Y + _offset.Y))
            {
                return false;
            }
            return true;
        }

        public bool PositionEqual(Vector2 position)
        {
            if (!(position.X > _position.X && position.X < _position.X + _offset.X))
            {
                return false;
            }
            if (!(position.Y > _position.Y && position.Y < _position.Y + _offset.Y))
            {
                return false;
            }
            return true;
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            if(_placer == false)
            {
                _source = new Rectangle(0, 0, (int)_offset.X, (int)_offset.Y);
            }
            else
            {
                _source = new Rectangle((int)_offset.X, 0, (int)_offset.X, (int)_offset.Y);
            }
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
            Unit unit = obj as Unit;
            if (!_position.Equals(unit._position))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            // Some comment to explain if there is a real problem with providing GetHashCode() 
            // or if I just don't see a need for it for the given class
            return _position.GetHashCode();
        }
    }
}
