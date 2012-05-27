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

namespace PhysicsLib
{
    public class Spring
    {
        public float k;
        public float damp;
        public float eqLength;

        public int index1;
        public int index2;

        public Spring(float k_, float damp_, float eqLength_)
        {
            k = k_;
            damp = damp_;
            eqLength = eqLength_;
        }
    }
}
