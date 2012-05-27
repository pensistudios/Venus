using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Particle
    {
        public float mass;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 accleration;

        public Vector2 force;

        public bool physicsActivated;

        public int index;

        public Particle(float mass_, Vector2 pos_)
        {
            mass = mass_;
            position = pos_;
            physicsActivated = true;
        }

    }
}
