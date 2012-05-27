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
    class DynamicBackground : Background
    {
        public List<Entity> objects;

        public DynamicBackground(Vector2 sD, Vector2 speed, Vector2 offset_, int depth_)
        {
            objects = new List<Entity>();
            screenDimensions = sD;
            scrollSpeed = speed;
            offset = offset_;
            depth = depth_;
            screenPos = new Vector2();
        }

        public override void Update(int milli, ScrollEvent e)
        {
            base.Update(milli, e);

            for (int i = 0; i < objects.Count; i++)
            {
                if (onScreen(objects[i]))
                {
                    objects[i].screenPosition = objects[i].worldPosition - screenPos;
                    //System.Console.WriteLine(objects[i].worldPosition + " scree " + screenPos);
                    objects[i].Update(milli);
                }
            }
            
            
        }

        public override void Draw(SpriteBatch s)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                if (onScreen(objects[i]))
                {
                    objects[i].Draw(s);
                }
            }
            
        }

        private bool onScreen(Entity e)
        {
            Texture2D i = e.animation[e.currentAnimation].getCurrentFrame();

            return Helper.rectangleInRectangle(e.worldPosition, new Vector2(i.Width,i.Height), screenPos, screenDimensions);
        }
    }
}
