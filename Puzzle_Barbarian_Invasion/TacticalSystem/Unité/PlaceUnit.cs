using DefineZone;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement;
using Puzzle_Barbarian_Invasion.TacticalSystem.MyPathFinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.Unité
{
    class PlaceUnit
    {
        private Trajet _trajet;// Le trajet effectué pour aller à un endroit
        private PathFinding _path;
        private ZoneDeplacement _zone;
        private List<Unit> _units;

        private Unit _unit;//unité sélectionner à déplacer
        public Vector2 _posPerso { get; private set; }//Position du perso avant placement de l'unité

        private Vector2 _position;//Position du curseur

        private ZEcart _ecart;
        private int _mapPosStart;//Position en x de la map

        private float _time;//temps d'attente avant un clic

        public PlaceUnit(Texture2D textureFleche,Texture2D textureZone,Map map,List<Unit> units,Vector2 position)
        {
            _units = units;
            _posPerso = position;//A la construction la position est la position du perso
      //      _position = position;

            _ecart = new ZEcart(Constantes.CASE_W, Constantes.CASE_H);
            _mapPosStart = map._posStart;

            _unit = new Unit(_units.Last());
            _unit._position = _posPerso;

            int id = _units.IndexOf(_unit);

            _unit = _units[id];

            _zone = new ZoneDeplacement(textureZone, _ecart,
                        new ZTaille((int)map._tailleEnPixel.X, (int)map._tailleEnPixel.Y),
                        new ZPoint((int)_posPerso.X - _mapPosStart, (int)_posPerso.Y), 
                        map, _mapPosStart, _unit._rayon);

            _trajet = new Trajet(textureFleche, _zone, _ecart._x, _ecart._y);
        }

        public void Tracer_chemin(Vector2 position)
        {
            _position = position;
            if(_trajet._count == 0 && position != _posPerso)
            {
                _trajet.AddChemin(position);
                _trajet.AddChemin(position);
                _path = new PathFinding(_trajet, _zone);
            }
            else if (_trajet._count > 0)
            {
                _trajet.AddChemin(position);
                _path = new PathFinding(_trajet, _zone);
            }
        }

        public bool Place(MouseState souris, KeyboardState state, GameTime gameTime)
        {
            this._time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 0.16f)
            {
                if ((souris.LeftButton == ButtonState.Pressed || state.IsKeyDown(Keys.Space)))
                {
                    if (_zone.Contains(_position))
                    {
                        _unit._position = _position;
                        _unit._placer = true;
                        return true;
                    }
                    return true;
                }
            }
            return false;
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            _zone.Draw(spriteBatch);
            if(_trajet._count >0)
            {
                _trajet.Draw(spriteBatch);
            }
        }
    }
}
