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
using System.Text;

namespace PhysicsLib
{
    public class PhysicalReality
    {
        public List<Particle> particles;
        public List<Spring> springs;

        public List<Vector2> externalGlobalForces;

        public PhysicalReality()
        {
            particles = new List<Particle>();
            springs = new List<Spring>();
            externalGlobalForces = new List<Vector2>();
        }

        public void turnOff()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].physicsActivated = false;
            }

        }

        public void turnOn()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].physicsActivated = true;
            }
        }


        public void stepSimulation(int milli)
        {
            float dt = milli / 1000.0f;

            calcForces();

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].physicsActivated)
                {
                    particles[i].accleration = particles[i].force / particles[i].mass;
                    particles[i].velocity += particles[i].accleration * dt;
                    particles[i].position += particles[i].velocity * dt;
                }
            }
            zeroForce();
        }


        private void calcForces()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                //if (particles[i].physicsActivated)
                {
                    for (int j = 0; j < externalGlobalForces.Count; j++)
                    {
                        particles[i].force += externalGlobalForces[j];
                    }
                }
            }

            for (int i = 0; i < springs.Count; i++)
            {
                //if (particles[springs[i].index1].physicsActivated && particles[springs[i].index2].physicsActivated)
                {
                    Vector2 dr = particles[springs[i].index1].position - particles[springs[i].index2].position;
                    Vector2 dv = particles[springs[i].index1].velocity - particles[springs[i].index2].velocity;

                    Vector2 force1 = -(springs[i].k * (dr.Length() - springs[i].eqLength) + springs[i].damp * ((dv.X * dr.X + dv.Y * dr.Y) / dr.Length())) * (dr / dr.Length());
                    Vector2 force2 = -force1;


                    particles[springs[i].index1].force += force1;
                    particles[springs[i].index2].force += force2;
                }
            }
        }

        private void zeroForce()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                //if(particles[i].physicsActivated)
                    particles[i].force = Vector2.Zero;
            }
        }

    }
}
