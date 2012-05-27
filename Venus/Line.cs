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
    enum PointIntersection
    {
        ABOVE,
        BELOW,
        ONTOP
    };

    class Line
    {
        // v*t + point1 = P(t)

        Vector2 worldPosition;
        Vector2 direction;
        Vector2 normal;

        static readonly float ep = 1.0f;
        static readonly float normalRes = 0.1f;

        public Line(Vector2 p1, Vector2 p2)
        {
            direction = p2 - p1;
            worldPosition = p1;
            normal = new Vector2(-direction.Y,direction.X);
            normal.Normalize();
        }

        public Line(int x1, int y1, int x2, int y2)
        {
            direction = new Vector2(x2-x1,y2-y1);
            worldPosition = new Vector2(x1,y1);
            normal = new Vector2(-direction.Y,direction.X);
            normal.Normalize();
            
        }

        public bool pointInXDomain(Vector2 point)
        {
            Vector2 p2 = direction + worldPosition;
            
            if (p2.X >= worldPosition.X)
            {
                if (point.X <= p2.X && point.X >= worldPosition.X)
                {
                    return true;
                }
            }
            else
            {
                if (point.X <= worldPosition.X && point.X >= p2.X)
                {
                    return true;
                }
            }
            return false;
        }

        public PointIntersection testPoint(Vector2 point)
        {
            float dot = Helper.dotProduct(normal, point - worldPosition);
            //System.Console.WriteLine(dot + " " + point);
            if (dot > 0.0f)
            {
                return PointIntersection.ABOVE;
            }
            else if (dot < 0.0f)
            {
                return PointIntersection.BELOW;
            }
            else
            {
                return PointIntersection.ONTOP;
            }
        }

        public PointIntersection testPointApprox(Vector2 point)
        {
            float dot = Helper.dotProduct(normal, point - worldPosition);

            if (dot > ep)
            {
                return PointIntersection.ABOVE;
            }
            else if (dot < -ep)
            {
                return PointIntersection.BELOW;
            }
            else
            {
                return PointIntersection.ONTOP;
            }

        }

        public void Draw(SpriteBatch s)
        {
            Vector2 point = new Vector2(Math.Abs(direction.X), Math.Abs(direction.Y));
            if (point.X < 5)
                point.X = 5;
            if (point.Y < 5)
                point.Y = 5;

            Texture2D t = new Texture2D(s.GraphicsDevice,(int) point.X,(int) point.Y);
            Color[] data = new Color[t.Width*t.Height];
            PointIntersection p;

            for (int y = 0; y < t.Height; y++)
            {
                for (int x = 0; x < t.Width; x++)
                {
                    if (direction.Y < 0.0f)
                    {
                        Vector2 vec = new Vector2(x + worldPosition.X, y + worldPosition.Y + direction.Y);
                        p = testPointApprox(vec);
                    }
                    else
                    {
                        p = testPointApprox(new Vector2(x, y) + worldPosition);
                    }

                    if (p == PointIntersection.ONTOP)
                    {
                        float r = ((float)x / (float)t.Width);
                        data[x + y * t.Width] = new Color(r, r, r);
                    }

                }
            }
            /*Vector2 param = new Vector2(t.Width/2.0f,t.Height/2.0f);
            for (int k = 1; k < 2; k++)
            {

                for (float time = 0.0f; time < 3.0; time += normalRes)
                {
                    data[(int)(param.X + param.Y * t.Width)] = Color.Violet;
                    param += normal * time;
                    System.Console.WriteLine(param);
                }
               // param = new Vector2((t.Width / 2.0f) + k, (t.Height / 2.0f) + k);
            }
            */
            

            t.SetData<Color>(data);

            if (direction.Y < 0.0f)
            {
                Vector2 pos = new Vector2(worldPosition.X, worldPosition.Y + direction.Y);
                s.Draw(t, pos, Color.White);
            }
            else
            {
                s.Draw(t, worldPosition, Color.White);
            }
        }

    }
}
