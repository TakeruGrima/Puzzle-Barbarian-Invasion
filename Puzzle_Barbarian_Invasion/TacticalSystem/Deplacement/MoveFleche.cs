using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement
{
    class MoveFleche
    {
        private Texture2D _texture;

        //instances de la classe
        private Rectangle _source;
        public Vector2 _position { get; private set; }


        private int _sens;//indique le sens dans lequel va la fleche (sera expliqué plus bas en détail)
        private Rectangle _affichage;//rectangle gérant la position de la fleche
        private int _height;//hauteur
        private int _width;//longueur

        //Constructeurs
        public MoveFleche(Texture2D texture,Vector2 position, int width,int height)
        {
            _texture = texture;
            _position = position;

            _width = width;
            _height = height;

            _affichage = new Rectangle((int)position.X,(int)position.Y, width, height);
        }

        //Méthodes 

        //Fonction qui dessine une partie de fleche(début,milieu,fin)
        public void Draw(SpriteBatch spriteBatch, Vector2 pos_perso, int spec)//spec prend 1 si la fleche est le début et 2 si fin
        {
            //source est vide
            Rectangle source = new Rectangle(0 * _width, 0 * _height, 0, 0);

            if (spec == 1)//début de fleche
            {
                //explication du sens (représente des fleches directionnelles):
                //   8
                //4     6
                //   2
                if (_sens == 6)//droite
                {
                    source = new Rectangle(0 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 2)//bas
                {
                    source = new Rectangle(1 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 8)//haut
                {
                    source = new Rectangle(0 * _width, 1 * _height, _width, _height);
                }
                else if (_sens == 4)//gauche
                {
                    source = new Rectangle(1 * _width, 1 * _height, _width, _height);
                }
                //affichage du début de flèche sous l'unité
                spriteBatch.Draw(_texture, new Rectangle((int)pos_perso.X, (int)pos_perso.Y,_width,_height), source, Color.White);
            }
            else if (spec == 2)//fin de fleche
            {
                if (_sens == 6)//droite
                {
                    source = new Rectangle(6 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 2)//bas
                {
                    source = new Rectangle(7 * _width, 0 * _height, _width, _height);
                }
                else if (_sens == 8)//haut
                {
                    source = new Rectangle(6 * _width, 1 * _height, _width, _height);
                }
                else if (_sens == 4)//gauche
                {
                    source = new Rectangle(7 * _width, 1 * _height, _width, _height);
                }
                //affichage de la flèche
                spriteBatch.Draw(_texture, _affichage, source, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, _affichage, _source, Color.White);
            }
        }

        //méthodes set

        //Fonction qui initialise la sélection dans l'image en fonction du sens
        public void setSource(int sens)
        {
            _sens = sens;
            _source.Width = _width;
            _source.Height = _height;

            _affichage.Height = _height;
            _affichage.Width = _width;
            switch (sens)
            {
                case 8://on va en haut
                    _source.X = 2 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 86://en était aller en haut et on tourne à droite
                    _source.X = 4 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 84://en était aller en haut et on tourne à gauche
                    _source.X = 5 * _width;
                    _source.Y = 0 * _height;
                    break;

                case 2://on va en bas
                    _source.X = 2 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 26://en était aller en bas et on tourne à droite
                    _source.X = 4 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 24://en était aller en bas et on tourne à gauche
                    _source.X = 5 * _width;
                    _source.Y = 1 * _height;
                    break;

                case 6://on va à droite
                    _source.X = 3 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 68://en était aller à droite et va en haut
                    _source.X = 5 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 62://en était aller à droite et va en bas
                    _source.X = 5 * _width;
                    _source.Y = 0 * _height;
                    break;

                case 4://on va à gauche
                    _source.X = 3 * _width;
                    _source.Y = 0 * _height;
                    break;
                case 48://en était aller à gauche et va en haut
                    _source.X = 4 * _width;
                    _source.Y = 1 * _height;
                    break;
                case 42://en était aller à gauche et va en bas
                    _source.X = 4 * _width;
                    _source.Y = 0 * _height;
                    break;
            }
        }

        public void setSens(int sens)
        {
            _sens = sens;
        }

        //méthode get

        public int getSens()
        {
            return _sens;
        }
    }
}
