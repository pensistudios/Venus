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
    class Ground
    {
        public List<Line> geometry; //ASSUMES LINES ARE CONNECTED Line1.Point2 = Line2.Point1
        private List<Line> onScreenLines;

        Vector2 dimensions;

        public Ground(Vector2 screenDim)
        {
            geometry = new List<Line>();
            dimensions = screenDim;
            onScreenLines = new List<Line>();
        }

        public PointIntersection testIntersection(Vector2 point)
        {
            for (int i = 0; i < geometry.Count; i++)
            {
                if (geometry[i].pointInXDomain(point))
                {
                    return geometry[i].testPoint(point);
                }
            }
            return PointIntersection.ONTOP;
        }

        public void Update(Vector2 screenPos)
        {
            //generateOnScreen(screenPos);
        }

        private void generateOnScreen(Vector2 screenPos)
        {
            onScreenLines.Clear();
            bool startFound = false;

            for (int i = 0; i < geometry.Count; i++)
            {
                if(geometry[i].pointInXDomain(screenPos))
                {
                    startFound = true;
                }

                if (startFound)
                {
                    onScreenLines.Add(geometry[i]);

                    if (geometry[i].pointInXDomain(screenPos + dimensions))
                    {
                        break;
                    }
                }
            }
        }

        

    }
}
