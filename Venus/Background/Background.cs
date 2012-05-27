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
    abstract class Background
    {
        //Screen Coords
        public Vector2 screenPos;
        protected Vector2 screenDimensions;

        //World Coords
        protected Vector2 backgroundDimesions;
        
        //more props
        protected Vector2 scrollSpeed;
        protected Vector2 offset;

        public int depth;

        public virtual void SetDimensions(Vector2 dim)
        {
            backgroundDimesions = dim;
        }

        public virtual void Update(int milli, ScrollEvent e)
        {
            if (e.isScrolling)
            {
                Vector2 dr = scrollSpeed * milli;
                
                dr.X = (e.scrollingXPositive) ? dr.X : -dr.X;
                dr.Y = (e.scrollingYPositive) ? dr.Y : -dr.Y;

                screenPos += dr;

                if (screenPos.X < 0.0f)
                    screenPos.X = 0.0f;
                if (screenPos.Y < 0.0f)
                    screenPos.Y = 0.0f;
                if ((screenPos.X + screenDimensions.X) >= backgroundDimesions.X)
                    screenPos.X -= dr.X;
                if ((screenPos.Y + screenDimensions.Y) >= backgroundDimesions.Y)
                    screenPos.Y -= dr.Y;
            }
        }

        public abstract void Draw(SpriteBatch s);
    }

    class BackgroundDepthSorter : IComparer<Background>
    {
        public int Compare(Background b1, Background b2)
        {
            return b1.depth.CompareTo(b2.depth);
        }
    }
}
