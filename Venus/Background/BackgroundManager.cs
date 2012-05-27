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
    class BackgroundManager
    {
        public List<Background> backgrounds;
        // need accessor methods for each background

        Vector2 worldDimensions;
       
        
        Vector2 screenDimensions;
        public Vector2 screenPosition;

        private int count;
        private int mainBackgroundIndex;

        public BackgroundManager(Vector2 worldDim_ , Vector2 screenDim_)
        {
            worldDimensions = worldDim_;
            screenDimensions = screenDim_;
            screenPosition = new Vector2();
            backgrounds = new List<Background>();

            
        }

        public void Add(Background b, bool flagAsMain)
        {
            backgrounds.Add(b);

            if(flagAsMain)
                mainBackgroundIndex = backgrounds.IndexOf(b);
        }

        public void SetDimensions(Vector2 dim)
        {
            foreach (Background b in backgrounds)
            {
                b.SetDimensions(dim);
            }
        }

        public void Initialize()
        {
            Background b = backgrounds[mainBackgroundIndex];
            backgrounds.Sort(new BackgroundDepthSorter());
            mainBackgroundIndex = backgrounds.IndexOf(b);
        }

        public void Update(int milli, ScrollEvent s)
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                backgrounds[i].Update(milli, s);
                
                if (i == mainBackgroundIndex)
                {
                    screenPosition = backgrounds[i].screenPos;
                }
            }
        }

        public void DrawBackLayers(SpriteBatch s)
        {
            count = 0;

            while (backgrounds[count].depth < 0)
            {
                backgrounds[count].Draw(s);
                count++;
            }
        }

        public void DrawFrontLayers(SpriteBatch s)
        {

            while (count < backgrounds.Count)
            {
                backgrounds[count].Draw(s);
                count++;

            }
        }

    }
}
