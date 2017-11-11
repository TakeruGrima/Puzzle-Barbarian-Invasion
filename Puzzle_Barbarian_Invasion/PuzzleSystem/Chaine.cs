using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzle_Barbarian_Invasion.PuzzleSystem
{
    class Chaine
    {
        ContentManager Content;

        private GridPieces _gridPieces;

        private List<Piece> _pSelect;// pièces contenu dans la chaine
        private string _color = "";//couleur de la sélection
        private Texture2D _texture;

        public Chaine(ContentManager content, GridPieces gridPieces)
        {
            Content = content;

            Initialize(gridPieces);
        }

        private void Initialize(GridPieces gridPieces)
        {
            _pSelect = new List<Piece>();
            _gridPieces = gridPieces;
        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/selection");
        }

        public void add(Piece p)
        {
            _pSelect.Add(p);
        }

        public bool verifRempli()
        {
            for (int i = 0; i < _gridPieces._tailleGrid.X; i++)
            {
                for (int j = 0; j < _gridPieces._tailleGrid.Y; j++)
                {
                    if (_gridPieces._grille[j][i] == 0)
                    {
                        return false;
                    }
                }
                _gridPieces._cree[i] = false;
            }
            return true;
        }

        public void select(MouseState souris, GameTime gameTime)//renvoit true si il y a eu selection et suppression
        {
            if (verifRempli())
            {
                if (souris.LeftButton == ButtonState.Pressed)
                {
                    //Vecteur contenant la position de la souris mais dans la Grille des pièces
                    Vector2 posSouris = new Vector2((int)(souris.X / _gridPieces._offset.X) * _gridPieces._offset.X
                    , (int)(souris.Y / _gridPieces._offset.Y) * _gridPieces._offset.Y);

                    if (_color == "")
                    {
                        //Liste des possibilité de pièces
                        List<Piece> nextPiece = new List<Piece>();
                        nextPiece.Add(new Piece(Content, posSouris, "blue"));
                        nextPiece.Add(new Piece(Content, posSouris, "red"));
                        nextPiece.Add(new Piece(Content, posSouris, "green"));

                        //Test des differentes possibilités
                        foreach (Piece curr in nextPiece)
                        {
                            Console.WriteLine(curr._position);
                            if (_gridPieces.Contains(curr))//possibilité bonne?
                            {
                                _color = curr._color;
                                _pSelect.Add(curr);//Ajout dans la sélection
                            }
                        }
                    }
                    else
                    {
                        //Piece contenant la seule possibilité accepté
                        Piece next = new Piece(Content, posSouris, _color);

                        if (_gridPieces.Contains(next))//possibilité bonne?
                        {
                            if (_pSelect.Count > 1 && next.Equals(_pSelect.ElementAt(_pSelect.Count - 2)))
                            {
                                _pSelect.Remove(_pSelect.Last());
                            }
                            else
                            {
                                Vector2 last = _pSelect.Last()._position;
                                bool pieceX = (last.X + _gridPieces._offset.X == next._position.X
                                    || last.X - _gridPieces._offset.X == next._position.X)
                                    && last.Y == next._position.Y;
                                bool pieceY = (last.Y + _gridPieces._offset.Y == next._position.Y
                                    || last.Y - _gridPieces._offset.Y == next._position.Y)
                                    && last.X == next._position.X;

                                if (pieceX || pieceY)
                                {
                                    if (!_pSelect.Contains(next))
                                    {
                                        _pSelect.Add(next);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (souris.LeftButton == ButtonState.Released)
                {
                    if (_pSelect.Count < 3)
                    {
                        _pSelect.RemoveRange(0, _pSelect.Count);
                        _color = "";
                    }
                    else if (_pSelect.Count > 2)
                    {
                        foreach (Piece curr in _pSelect)
                        {
                            _gridPieces._pieces.Remove(curr);
                            _gridPieces._grille[(int)curr._position.Y / Constantes.CASE_H]
                                [(int)curr._position.X / Constantes.CASE_W] = 0;
                        }

                        _pSelect.RemoveRange(0, _pSelect.Count);

                        _color = "";
                    }
                }
            }
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Piece curr in _pSelect)
            {
                spriteBatch.Draw(_texture, new Rectangle(
                    (int)curr._position.X, (int)curr._position.Y,
                    (int)Constantes.CASE_W, (int)Constantes.CASE_H),
                    Color.White);
            }
        }
    }
}
