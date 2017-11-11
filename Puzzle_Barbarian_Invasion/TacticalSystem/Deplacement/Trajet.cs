using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using DefineZone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement
{
    class Trajet
    {
        public Texture2D _texture { get; private set; }

        private ZoneDeplacement _zone;
        public List<MoveFleche> _trajet { get; private set; }
        private Vector2 _posPerso;
        public int _width { get; private set; }//taille fleche
        public int _height { get; private set; }//taille fleche

        private int _mapPosStart;//position de la map
        public bool _vide = true; // indique que la liste est vide
        public int _count = 0;

        public Trajet(Texture2D texture, ZoneDeplacement zone, int width, int height)
        {
            _texture = texture;

            _width = width;
            _height = height;

            _trajet = null;
            _vide = true;

            _zone = zone;

            _mapPosStart = _zone._posStart;
            _posPerso = _zone.GetPosPerso();
        }

        public void SetZone(ZoneDeplacement zone)
        {
            _zone = zone;

            if (_zone != null)
            {
                if (_posPerso != _zone.GetPosPerso())
                {
                    _mapPosStart = _zone._posStart;
                    _posPerso = _zone.GetPosPerso();

                    if(_trajet != null)
                    {
                        Clear();
                    }
                }
            }
        }

        public Vector2 GetLastPosition()
        {
            return _trajet.Last()._position;
        }

        public Vector2 GetFirstPosition()
        {
            return _posPerso;
        }

        public void Clear()
        {
            _vide = true;
            _trajet.Clear();
            _count = 0;
        }

        public bool AddChemin(Vector2 position)//retourne true si il y a eu ajout
        {

            //parcourt la map pour savoir si le curseur est sur un obstacle en cas d'obstacle on sort de la fonction
            if (_zone == null)
            {
                return false;
            }
            if(_zone.PassageImpossible(position) || !_zone.Contains(position))
            {
                return false;
            }

            if (_vide == true)
            {
                MoveFleche curr = new MoveFleche(_texture, _posPerso, _width, _height);
                _trajet = new List<MoveFleche>();//on initialise une nouvelle liste

                if (position.X > _posPerso.X)//droite
                {
                    curr.setSource(6);
                }
                else if (position.X < _posPerso.X)//gauche
                {
                    curr.setSource(4);
                }
                else if (position.Y > _posPerso.Y)//bas
                {
                    curr.setSource(2);
                }
                else if (position.Y < _posPerso.Y)//haut
                {
                    curr.setSource(8);
                }
                else
                {
                    return false;
                }
                _trajet.Add(curr);//on ajoute la fleche à la liste
                _vide = false;
                _count = 1;
                return true;
            }
            else
            {
                MoveFleche curr = new MoveFleche(_texture, position, _width, _height);

                Vector2 posPrec = _trajet.Last()._position;

                if (position.X > posPrec.X)//droite
                {
                    if (_trajet.Last().getSens() != 6)
                    {
                        //on modifie le bout de fleche précédent pour quel forme un virage
                        _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 6);
                    }
                    curr.setSource(6);
                }
                else if (position.X < posPrec.X)//gauche
                {
                    if (_trajet.Last().getSens() != 4)
                    {
                        //on modifie le bout de fleche précédent pour quel forme un virage
                        _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 4);
                    }
                    curr.setSource(4);
                }
                else if (position.Y > posPrec.Y)//bas
                {
                    if (_trajet.Last().getSens() != 2)
                    {
                        //on modifie le bout de fleche précédent pour quel forme un virage
                        _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 2);
                    }
                    curr.setSource(2);
                }
                else if (position.Y < posPrec.Y)//haut
                {
                    if (_trajet.Last().getSens() != 8)
                    {
                        //on modifie le bout de fleche précédent pour quel forme un virage
                        _trajet.Last().setSource(_trajet.Last().getSens() * 10 + 8);
                    }
                    curr.setSource(8);
                }
                else
                {
                    return false;
                }
                _trajet.Add(curr);//on ajoute le bout de fleche
                _count++;
                return true;
            }
        }

        //Méthode pour afficher le trajet avec les fleches
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (MoveFleche curr in _trajet)
            {
                if(curr == _trajet[0])
                {
                    curr.Draw(spriteBatch, _posPerso, 1);//affiche la fin de la fleche
                }
                else if (curr == _trajet.Last())
                {
                    curr.Draw(spriteBatch, _posPerso, 2);//affiche la fin de la fleche
                }
                else
                {
                    curr.Draw(spriteBatch, _posPerso, 0);//affiche un bout de la fleche
                }
            }
        }
    }
}
