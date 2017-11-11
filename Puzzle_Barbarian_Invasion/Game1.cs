using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using TiledSharp;
using Puzzle_Barbarian_Invasion.PuzzleSystem;
using Puzzle_Barbarian_Invasion.TacticalSystem;
using Puzzle_Barbarian_Invasion.TacticalSystem.Unité;
using Puzzle_Barbarian_Invasion.TacticalSystem.Deplacement;
using DefineZone;
using System.Linq;
using Puzzle_Barbarian_Invasion.TacticalSystem.MyPathFinding;
using System.Xml;
using Puzzle_Barbarian_Invasion.UI;

namespace Puzzle_Barbarian_Invasion
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Puzle
        Puzzle puzzle;

        bool combat = false;//change this value to true to view the puzzle system

        //Map
        int abscisseMap = 608;
        Map map;
        TmxMap mapTMX;
        Texture2D tileset;

        //Unités
        List<Unit> units;
        SelectUnit select;
        PlaceUnit place;

        //Fenetre
        Texture2D windowTexture;
        Window ui;

        private SpriteFont font;
        private int score = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = Constantes.WIN_W;
            graphics.PreferredBackBufferHeight = Constantes.WIN_H;

            Window.AllowUserResizing = true;

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Ajoutez ici votre code d'initialisation
            puzzle = new Puzzle(Content);

            map = new Map(Content, abscisseMap);
            units = new List<Unit>
            {
                new Unit(Content,abscisseMap/32+1,1,7),
                new Unit(Content,abscisseMap/ 32 + 5, 2, 7)
            };

            //Selection d'unités
            select = new SelectUnit(Content, units, map);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //Chargement des textures du puzzle
            puzzle.LoadContent();

            //Chargement de la map
            map.LoadContent();

            //Chargement unités
            foreach (Unit curr in units)
            {
                curr.LoadContent();
            }


            //Chargement de la texture du curseur
            select.LoadContent();

            //Fenetre initialisation
            windowTexture = Content.Load<Texture2D>("_images_/UIpack");

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("Content/UI.xml");

            ui = new Window(windowTexture, xdoc);

            font = Content.Load<SpriteFont>("_Fonts_/Font"); // Use the name of your sprite font file here instead of 'Score'.
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (combat == true)
            {
                this.IsMouseVisible = true;
                puzzle.Update(gameTime);
            }
            else
            {
                this.IsMouseVisible = false;

                select.Move(Keyboard.GetState(), Mouse.GetState(), gameTime);

                bool ifSelect = select.Select(Mouse.GetState(), Keyboard.GetState(), gameTime);

                if (ifSelect == true && (place == null || select._posSelect != place._posPerso))
                {
                    Unit currUnit = new Unit(units.Last());
                    currUnit._position = select._posSelect;


                    if (units.Contains(currUnit))
                    {
                        int id = units.IndexOf(currUnit);

                        place = new PlaceUnit(Content.Load<Texture2D>("_images_/fleche_move"),
                        Content.Load<Texture2D>("_images_/case_deplace"), map, units, currUnit._position);
                    }
                }
                if (ifSelect == true && place != null)
                {
                    place.Tracer_chemin(select._position);

                    if(place.Place(Mouse.GetState(), Keyboard.GetState(), gameTime))
                    {
                        place = null;
                        select._select = false;
                        select._posSelect = select._position;
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
       
            puzzle.Draw(spriteBatch);

            map.Draw(spriteBatch);

            if (place != null)
            {
                place.Draw(spriteBatch);
            }

            foreach (Unit curr in units)
            {
                curr.Draw(spriteBatch);
                //ui.Draw(spriteBatch, new Vector2(curr._position.X + 32, curr._position.Y), 1, 0);
               // spriteBatch.DrawString(font, "Attaquer", new Vector2(curr._position.X + 44, curr._position.Y), Color.White);
            }
            select.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
