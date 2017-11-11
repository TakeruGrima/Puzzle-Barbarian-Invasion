using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DefineZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puzzle_Barbarian_Invasion.TacticalSystem.Unité;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement
{
    class ZoneDeplacement : Zone
    {
        private Texture2D _texture;
        private Map _map;

        private int _rayon;
        public int _posStart { get; private set; }
        private ZPoint _depart;

        private List<ZPoint> _interdit;//list contenant les bord de la zone

        public ZoneDeplacement(Texture2D texture,ZEcart ecart, ZTaille taille, ZPoint depart, Map map, int posStart, int rayon) : base(ecart, taille, depart)
        {
            _texture = texture;
            _map = map;
            _rayon = rayon;
            _posStart = posStart;
            _depart = depart;
            _interdit = new List<ZPoint>();

            GetGroupe(depart);
        }

        public List<ZPoint> GetInterdit()
        {
            return _interdit;
        }

        public List<ZPoint> GetZone()
        {
            return this._points;
        }

        public bool PosEquals(Vector2 position)
        {
            if(position.X!=_depart._x+_posStart)
            {
                return false;
            }
            if(position.Y != _depart._y)
            {
                return false;
            }
            return true;
        }

        public bool Contains(Vector2 position)
        {
            ZPoint p = new ZPoint((int)position.X - _posStart, (int)position.Y);

            return _points.Contains(p);
        }

        public int GetID(Vector2 position)
        {
            ZPoint p = new ZPoint((int)position.X - _posStart, (int)position.Y);

            return _points.IndexOf(p);
        }

        public bool PassageImpossible(Vector2 position)
        {
            ZPoint p = new ZPoint((int)position.X - _posStart, (int)position.Y);

            return _interdit.Contains(p);
        }

        public Vector2 GetPosPerso()
        {
            return new Vector2(_depart._x + _posStart, _depart._y); 
        }

        public override bool Contrainte(ZPoint p)
        {
            if (base.Contrainte(p) && !_interdit.Contains(p))
            {
                /*Unit ghostUnit = new Unit(_units.Last());//On crée une unité fantome c'est à dire une unité qui n'est pas vraiment
                //sur la map, on vérifie si elle n'existe pas
                ghostUnit._position = new Vector2(p._x + _posStart, p._y);//On lui donne la position du point à tester

                if(_units.Contains(ghostUnit))//on test si l'unité fantome existe
                {
                    //si oui l'unité ne peut marcher sur une autre unité
                    _interdit.Add(p);
                    return false;
                }*/
                if (_map.getGid(p._x, p._y) == 0 && p.Distance(_depart, _ecart) <= _rayon)
                {
                    return true;
                }
                else
                {
                    _interdit.Add(p);
                    return false;
                }
            }
            return false;
        }

        public override bool Contrainte(int x, int y)
        {
            ZPoint p = new ZPoint(x, y);

            return Contrainte(p);
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (ZPoint curr in _points)
            {
                Rectangle source = new Rectangle(0, 0, _ecart._x, _ecart._y);
                spriteBatch.Draw(_texture, new Rectangle(curr._x + _posStart, curr._y, _ecart._x, _ecart._y), source, Color.White * 0.5f);
            }
            foreach (ZPoint curr in _interdit)
            {
                Rectangle source = new Rectangle(0, _ecart._y, _ecart._x, _ecart._y);
                spriteBatch.Draw(_texture, new Rectangle(curr._x + _posStart, curr._y, _ecart._x, _ecart._y), source, Color.White * 0.5f);
            }
        }
    }
}
