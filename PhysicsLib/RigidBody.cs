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
    class RigidBody
    {
        List<Particle> particles;
        List<RigidConnector> connectors;

        public List<Vector2> externalGlobalForces;

        public RigidBody()
        {
            particles = new List<Particle>();
            connectors = new List<RigidConnector>();
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
