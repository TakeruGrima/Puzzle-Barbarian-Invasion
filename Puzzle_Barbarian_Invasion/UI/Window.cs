using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Puzzle_Barbarian_Invasion.UI
{
    class Window
    {
        private Texture2D _texture;

        private List<Frame> _frames;

        public Window(Texture2D texture, XmlDocument xdoc)
        {
            _texture = texture;

            _frames = new List<Frame>();

            var texturesAtlas = xdoc.SelectNodes("TextureAtlas");

            foreach (XmlNode textureAtlas in texturesAtlas)
            {
                foreach (XmlNode frame in textureAtlas.SelectNodes("SubTexture"))
                {
                    var name = frame.Attributes.GetNamedItem("name").Value;

                    _frames.Add(new Frame(
                        _texture,
                        Int32.Parse(frame.Attributes.GetNamedItem("x").Value),
                        Int32.Parse(frame.Attributes.GetNamedItem("y").Value),
                        Int32.Parse(frame.Attributes.GetNamedItem("width").Value),
                        Int32.Parse(frame.Attributes.GetNamedItem("height").Value),
                        frame.Attributes.GetNamedItem("name").Value)
                        );
                }
            }
        }

        //Méthode de Draw
        public void Draw(SpriteBatch spriteBatch, Vector2 position, int width, int height)//en nombre de case de 16*16
        {
            int frameWidth = _frames[0]._width;
            int frameHeight = _frames[0]._height;

            int rightPosition = (int)position.X + frameWidth * (width + 1);
            int downPosition = (int)position.Y + frameHeight * (height + 1);

            //Draw des coins de la fenetre
            _frames[0].Draw(spriteBatch, position);//coin supérieur gauche
            _frames[1].Draw(spriteBatch, new Vector2(rightPosition, position.Y));//coin supérieur droit

            _frames[2].Draw(spriteBatch, new Vector2(position.X, downPosition));//coin inférieur gauche
            _frames[3].Draw(spriteBatch, new Vector2(rightPosition,downPosition));//coin inférieur droit

            //Draw fenetre
            //Bordure Haut/Bas

            int cpt = 1;

            while(width>=cpt)
            {
                _frames[5].Draw(spriteBatch,new Vector2(position.X+frameWidth*cpt,position.Y));
                _frames[6].Draw(spriteBatch, new Vector2(position.X + frameWidth * cpt, downPosition));
                cpt++;
            }

            //Bordule Left/Right

            cpt = 1;

            while (height >= cpt)
            {
                _frames[7].Draw(spriteBatch, new Vector2(position.X, position.Y+frameHeight*cpt));
                _frames[8].Draw(spriteBatch, new Vector2(rightPosition, position.Y + frameHeight * cpt));
                cpt++;
            }

            //centre
            for(int i=1;i<=width;i++)
            {
                for (int j = 1; j <= height; j++)
                {
                    _frames[4].Draw(spriteBatch, new Vector2(position.X+frameWidth*i, position.Y + frameHeight * j));
                }
            }
        }
    }
}
