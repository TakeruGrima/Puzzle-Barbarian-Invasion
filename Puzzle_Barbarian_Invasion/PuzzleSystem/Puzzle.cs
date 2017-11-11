using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace Puzzle_Barbarian_Invasion.PuzzleSystem
{
    class Puzzle
    {
        ContentManager Content;
        
        Grid _grid;
        GridPieces _gridPieces;
        Chaine _chaine;

        List<Piece> _pieces = new List<Piece>();

        public Puzzle(ContentManager content)
        {
            Content = content;

            Initialize();
        }

        private void Initialize()
        {
            // TODO: Ajoutez ici votre code d'initialisation
            _grid = new Grid(Content);
            _gridPieces = new GridPieces(_pieces, Content, _grid);
            _chaine = new Chaine(Content, _gridPieces);

        }

        public void LoadContent()
        {
            //Initialisation de la texture des cases de la grille
            _grid.LoadContent();
            //Initialisation de la texture des pièces
            _gridPieces.LoadContent();
            //Initialisation de la texture d'une chaine ( selection de formes)
            _chaine.LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            _gridPieces.Remplir(gameTime);
            _chaine.select(Mouse.GetState(), gameTime);
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch)
        {
            _grid.Draw(spriteBatch);
            _gridPieces.Draw(spriteBatch);
            _chaine.Draw(spriteBatch);
        }
    }
}
