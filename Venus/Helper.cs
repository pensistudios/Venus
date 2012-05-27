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
    class Helper
    {
        public static float dotProduct(Vector2 a, Vector2 b)
        {
            return (a.X * b.X + a.Y * b.Y);
        }

        public static bool vectorInRectangle(Vector2 vector, Vector2 rectangleOrgin, Vector2 rectangleDimensions)
        {
            if ((vector.X >= rectangleOrgin.X) && (vector.X <= (rectangleOrgin.X + rectangleDimensions.X)))
            {
                if ((vector.Y >= rectangleOrgin.Y) && (vector.Y <= (rectangleOrgin.Y + rectangleDimensions.Y)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool rectangleInRectangle(Vector2 r1o, Vector2 r1d, Vector2 r2o, Vector2 r2d)
        {
            if (((r1o.X + r1d.X) >= r2o.X) && (r1o.X <= (r2d.X + r2o.X)))
            {
                if (((r1o.Y + r1d.Y) >= r2o.Y) && (r1o.Y <= (r2d.Y + r2o.Y)))
                {
                    return true;
                }
            }
            return false;
        }

   

    }
}
