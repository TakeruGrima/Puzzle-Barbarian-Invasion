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
    class SelectUnit
    {
        private ContentManager Content;

        private Texture2D _texture;
        public Vector2 _position;
        private Vector2 _offset;

        private int _posStart;// position de la map en x
        private Vector2 _taille;// taille de la map

        private List<Unit> _units;
        public bool _select = false;
        public Vector2 _posSelect { get; set; }

        private float _time;//temps d'exécution pour déplacement du curseur

        private Vector2 _precSouris; // ancienne position de la souris ( en case)

        private Map _map;

        public SelectUnit(ContentManager content,List<Unit> units, Map map)
        {
            Content = content;

            _map = map;

            _units = units;
            _posStart = map._posStart;
            _taille = map._tailleEnPixel;

            Console.WriteLine(units[0]._position);
            _position = new Vector2(_posStart,0);
            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);

            _precSouris = new Vector2(-1, -1);
        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/curseur");
        }

        public bool Select(MouseState souris, KeyboardState state, GameTime gameTime)
        {
            if ((souris.LeftButton == ButtonState.Pressed || state.IsKeyDown(Keys.Space)))
            {
                if(souris.LeftButton == ButtonState.Pressed)
                {
                    _position.X = (int)(souris.X / _offset.X) * _offset.X;
                    _position.Y = (int)(souris.Y / _offset.Y) * _offset.Y;
                }
                _posSelect = _position;

                Unit unitSelect = new Unit(Content, _position, 0);

                
                if (_units.Contains(unitSelect))
                {
                    int id = _units.IndexOf(unitSelect);

                    if(_units[id]._placer == false)
                    {
                        _select = true;
                        return _select;
                    }
                }
            }
            return _select;
        }

        //méthode déplacement
        public void Move(KeyboardState state, MouseState souris, GameTime gameTime)
        {
            this._time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 0.16f)
            {
                if (souris.X > _posStart && souris.X < _posStart + _taille.X && souris.Y > 0 && souris.Y < _taille.Y)
                {
                    Vector2 posSouris = new Vector2(
                        (int)(souris.X / _offset.X) * _offset.X,
                        (int)(souris.Y / _offset.Y) * _offset.Y
                        );
                    if (_precSouris != posSouris)
                    {
                        _position = posSouris;
                        _precSouris = _position;
                    }
                }
                if (state.IsKeyDown(Keys.Z) || state.IsKeyDown(Keys.Up))
                {
                    if (_position.Y > 0)//le curseur se déplace vers le haut
                    {
                        _position.Y -= 32;
                        _time = 0;
                    }
                }
                if (state.IsKeyDown(Keys.Q) || state.IsKeyDown(Keys.Left))//le curseur se déplace vers la gauche
                {
                    if (_position.X > _posStart)
                    {
                        _position.X -= 32;
                        _time = 0;
                    }
                }
                if (state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))//le curseur se déplace vers le bas
                {
                    if (_position.Y < _taille.Y - _offset.Y)
                    {
                        _position.Y += 32;
                        _time = 0;
                    }
                }
                if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
                {
                    if (_position.X < _posStart + _taille.X - _offset.X)//le curseur se déplace vers la droite
                    {
                        _position.X += 32;
                        _time = 0;
                    }
                }
            }
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, new Rectangle((int)_position.X, (int)_position.Y, (int)_offset.X, (int)_offset.Y), Color.White);
        }
    }
}
