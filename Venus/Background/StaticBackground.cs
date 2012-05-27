using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace Venus
{
    /***************************
     * Represents a scrolling background layer
     * *************************
     */
    class StaticBackground : Background
    {
        public Texture2D image;

        public StaticBackground()
        {
            screenDimensions = new Vector2();
            screenPos = new Vector2();
            scrollSpeed = new Vector2();
            offset = new Vector2();
        }

        public StaticBackground(Vector2 sD, Vector2 speed, Texture2D i, Vector2 offset_, int depth_)
        {
            image = i;
            screenDimensions = sD;
            scrollSpeed = speed;
            offset = offset_;
            depth = depth_;
            backgroundDimesions = new Vector2(i.Width, i.Height);
            screenPos = new Vector2();
        }


        

        public override void Draw(SpriteBatch s)
        {
            s.Draw(image, offset, new Rectangle((int)screenPos.X, (int)screenPos.Y, (int)screenDimensions.X, (int)screenDimensions.Y), Color.White);
        }
        
    }
}
