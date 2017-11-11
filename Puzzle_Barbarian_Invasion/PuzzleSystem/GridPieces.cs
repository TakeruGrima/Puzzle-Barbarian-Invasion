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
    class GridPieces
    {
        ContentManager Content;

        public List<Piece> _pieces { get; set; }
        public Vector2 _offset { get; private set; }
        public Texture2D _texture { get; private set; }

        public int[][] _grille { get; set; }
        public Vector2 _tailleGrid { get; private set; }

        public bool[] _cree { get; set; } // indique si une pièce à déja était crée dans la colonne

        Random rand = new Random();

        private float _time;
        int cpt = 0;

        public GridPieces(List<Piece> pieces,ContentManager content, Grid grille)
        {
            Content = content;

            Initialize(pieces, grille);
        }

        private void Initialize(List<Piece> pieces,Grid grille)
        {
            _grille = grille._cells;
            _tailleGrid = grille._taille;

            _pieces = pieces;

            _offset = new Vector2(Constantes.CASE_W, Constantes.CASE_H);

            _cree = new bool[(int)_tailleGrid.X];

            Generate();
        }

        public void LoadContent()
        {
            _texture = Content.Load<Texture2D>("_images_/pieces");
        }

        public void AddPiece(List<Piece> list, Vector2 position)
        {
            list.Add(new Piece(Content,position, rand.Next(3)));
        }

        public bool Contains(Piece p)
        {
            if (_pieces.Contains(p))
            {
                return true;
            }
            return false;
        }

        //Méthode pour comparer deux Pieces (pour le Sort)
        public static int CompareByPosition(Piece p1, Piece p2)
        {
            if (p1._position.X < p2._position.X)
            {
                return -1;
            }
            if (p1._position.X == p2._position.X)
            {
                if (p1._position.Y < p2._position.Y)
                {
                    return -1;
                }
            }
            return 1;
        }

        private void Generate()
        {
            for (int j = 0; j < _tailleGrid.Y; j++)
            {
                for (int i = 0; i < _tailleGrid.X; i++)
                {
                    if (_grille[j][i] == 1)
                    {
                        Vector2 position = new Vector2(i * _offset.X, j * _offset.Y);
                        _pieces.Add(new Piece(Content, position, rand.Next(3)));
                        _pieces.Sort(CompareByPosition);
                    }
                }
            }
        }

        private bool IfPieceUnder(Piece p)
        {
            if (p._position.Y + _offset.Y < Constantes.WIN_H)
            {
                int underX = (int)p._position.X / Constantes.CASE_W;
                int underY = ((int)p._position.Y) / Constantes.CASE_H + 1;

                if (underX > 0 && underX < _tailleGrid.X && underY > 0 && underY < _tailleGrid.Y)
                {
                    if (_grille[underY][underX] == 0 || _grille[underY][underX] == -2)
                    {
                        Vector2 under = new Vector2(p._position.X, p._position.Y + 4);
                   
                        p._position = under;

                        if (_grille[underY - 1][underX] == -2)
                        {
                            Console.WriteLine("Before: position:" + p._position.X / _offset.X + ";"
                            + p._position.Y / _offset.Y);
                            Console.WriteLine("UpEntier:(" + underX + ";" + (underY - 1) + ")");
                        }

                        if (_grille[underY - 1][underX] > -1)
                        {
                            _grille[underY - 1][underX] = 0;
                        }

                        if (_grille[underY][underX] != -2)
                        {
                            if (p._position.Y == underY * Constantes.CASE_H)
                            {
                                if (_grille[underY - 2][underX] == -2 && _grille[underY - 1][underX] == 0)
                                {
                                    Console.WriteLine("Bah michel?");
                                    bool add = true;
                                    foreach (Piece curr in _pieces)
                                    {
                                        if (curr.RangeEqual(new Vector2((underX) * _offset.X, (underY - 1) * _offset.Y)))
                                        {
                                            add = false;
                                        }
                                        else if (curr.RangeEqual(new Vector2((underX) * _offset.X,
                                            (underY - 2) * _offset.Y)))
                                        {
                                            add = false;
                                        }
                                    }
                                    if (add == true)
                                    {
                                        _cree[underX] = false;
                                    }
                                    Console.WriteLine("cree[" + underX + "] :" + _cree[underX]);
                                }
                                _grille[underY][underX] = 1;
                            }
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        public void Remplir(GameTime gameTime)
        {
            this._time += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_time > 0.12f)
            {
                foreach (Piece curr in _pieces)
                {
                    IfPieceUnder(curr);
                }
                for (int j = 0; j < _tailleGrid.Y; j++)
                {
                    for (int i = 0; i < _tailleGrid.X; i++)
                    {
                        if (_grille[j][i] == -2 && _grille[j + 1][i] == 0 && _cree[i] == false)
                        {
                            Console.WriteLine("cree[" + i + "] :" + _cree[i]);
                            Vector2 position = new Vector2(i * _offset.X, j * _offset.Y);
                            Console.WriteLine("COUCOU");
                            _pieces.Add(new Piece(Content, position, rand.Next(3)));
                            _pieces.Sort(CompareByPosition);

                            _cree[i] = true;
                            cpt++;
                        }
                    }
                }
            }
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Piece curr in _pieces)
            {
                curr.Draw(spriteBatch);
            }
        }
    }
}
