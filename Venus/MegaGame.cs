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
using PhysicsLib;
using AnimationLib;

namespace Venus
{
    class MegaGame : IPlayable
    {
        BackgroundManager backgrounds;
        Vector2 levelSize;
        Vector2 screenSize;
        Vector2 screenPos;

        //Physics Test
        PhysicalReality ps;
        PhysicalReality ps2;
        Vector2 startPos;
        float mass = 10.0f;
        Texture2D img;
        Texture2D sp;
        Vector2 cockInKrisButt;
        Ground ground;

        //Player Test
        Player player;

        

        public MegaGame(ContentManager cm, Vector2 dim)
        {
            levelSize = new Vector2(4000.0f, 1024.0f);
            screenSize = dim;
            screenPos = new Vector2();

            

            #region Player
            player = new Player();
            //put in player.loadAnimation()
            player.animation.Add(player.standing, new AnimationController(AnimationType.WRAP, 0.25f, 0.0f, false));
            player.animation[player.standing].frames.Add(cm.Load<Texture2D>("raptorstand1"));
            player.animation[player.standing].frames.Add(cm.Load<Texture2D>("raptorstand2"));
            player.animation[player.standing].frames.Add(cm.Load<Texture2D>("raptorstand3"));
            player.animation.Add(player.moving, new AnimationController(AnimationType.WRAP, 0.2f, 0.0f, false));
            player.animation[player.moving].frames.Add(cm.Load<Texture2D>("raptorstand3"));
            player.animation[player.moving].frames.Add(cm.Load<Texture2D>("raptorstand2"));
            player.animation[player.moving].frames.Add(cm.Load<Texture2D>("raptorstand1"));
            
            /*string playerfile = "Walking\\balls";
            for(int i = 1; i <= 9; i++)
            {
                player.animation[player.moving].frames.Add(cm.Load<Texture2D>(playerfile + i));
            }*/
            player.currentAnimation = player.standing;
            player.screenPosition.Y = 545;
            player.animation[player.standing].setFrames(player.animation[player.standing].frames);
            player.animation[player.moving].setFrames(player.animation[player.moving].frames);
            player.physics.particles.Add(new Particle(1.0f, player.screenPosition));
            player.physics.externalGlobalForces.Add(new Vector2(0.0f, 120.0f));
            #endregion

            ground = new Ground(new Vector2(1280,1024));
            ground.geometry.Add(new Line(0, 768, 500, 868));
            ground.geometry.Add(new Line(500, 868, 700, 768));
            ground.geometry.Add(new Line(700, 768, 1024, 768));

            #region Backgrounds
            /*
            Texture2D main = cm.Load<Texture2D>("player");
            levelSize.X = main.Width;
            
            backgrounds = new BackgroundManager(levelSize, screenSize);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.2f, 0.0f), cm.Load<Texture2D>("forestp1"), Vector2.Zero, -2), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.2f, 0.0f), cm.Load<Texture2D>("forestp2"), Vector2.Zero, -3), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.15f, 0.0f), cm.Load<Texture2D>("forestp3"), Vector2.Zero, -4), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.2f, 0.0f), cm.Load<Texture2D>("forestLIGHT1"), Vector2.Zero, -1), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.2f, 0.0f), cm.Load<Texture2D>("forestLIGHT2"), Vector2.Zero, 3), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.3f, 0.0f), cm.Load<Texture2D>("dirt"), new Vector2(0.0f, 500.0f),1), false);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.3f, 0.0f), main, new Vector2(0.0f, 495.0f),2), true);
            backgrounds.Add(new StaticBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.1f, 0.0f), cm.Load<Texture2D>("skybignowidescreen"), Vector2.Zero, -6), false);

            DynamicBackground rocks = new DynamicBackground(screenSize, new Vector2(0.1f, 0.0f), Vector2.Zero, -4);
            
            Entity rock1 = new Entity();
            AnimationController arock1 = new AnimationController(AnimationType.SINGLE, 0.0f, 0.0f, false);
            rock1.worldPosition.X = 100.0f;
            rock1.worldPosition.Y = 700.0f;
            arock1.frames.Add(cm.Load<Texture2D>("platform1"));
            rock1.animation.Add("1", arock1);
            rock1.currentAnimation = "1";
            Entity rock2 = new Entity();
            AnimationController arock2 = new AnimationController(AnimationType.SINGLE, 0.0f, 0.0f, false);
            rock2.worldPosition.X = 400.0f;
            rock2.worldPosition.Y = 600.0f;
            arock2.frames.Add(cm.Load<Texture2D>("platform2"));
            rock2.animation.Add("1", arock2);
            rock2.currentAnimation = "1";
            Entity rock3 = new Entity();
            AnimationController arock3 = new AnimationController(AnimationType.SINGLE, 0.0f, 0.0f, false);
            rock3.worldPosition.X = 900.0f;
            rock3.worldPosition.Y = 500.0f;
            arock3.frames.Add(cm.Load<Texture2D>("platform3"));
            rock3.animation.Add("1", arock3);
            rock3.currentAnimation = "1";

            rocks.objects.Add(rock1);
            rocks.objects.Add(rock2);
            rocks.objects.Add(rock3);
            rocks.SetDimensions(levelSize);
            backgrounds.Add(rocks, false);

            DynamicBackground sun = new DynamicBackground(screenSize, Vector2.Zero, Vector2.Zero, -5);
            Entity sunOb = new Entity();
            sunOb.worldPosition.X = 650.0f;
            sunOb.worldPosition.Y = 70.0f;
            //sunOb.reverseSprite = false;
            AnimationController sunA = new AnimationController(AnimationType.LOOP, 0.05f, 0.0f, false);
            string sunFile = "Sun\\Sun_Layer ";

            for (int i = 1; i <= 40; i++)
            {
                sunA.frames.Add(cm.Load<Texture2D>(sunFile + i));
            }
            sunOb.animation.Add("1", sunA);
            sunOb.currentAnimation = "1";
            sun.objects.Add(sunOb);
            sun.SetDimensions(levelSize);
            backgrounds.Add(sun,false);

            DynamicBackground clouds = new DynamicBackground(new Vector2(1280.0f, 1024.0f), new Vector2(0.2f, 0.0f), Vector2.Zero, 0);
            Entity c1 = new Entity();
            c1.worldPosition.X = 200.0f;
            c1.worldPosition.Y = 200.0f;
            AnimationController a1 = new AnimationController(AnimationType.WRAP, 0.7f,0.0f,false);
            AnimationController a2 = new AnimationController(AnimationType.WRAP, 0.05f, 0.23f, false);
            a1.frames.Add(cm.Load<Texture2D>("Clouds\\Cloud1\\C1"));
            a1.frames.Add(cm.Load<Texture2D>("Clouds\\Cloud1\\C2"));
            a1.frames.Add(cm.Load<Texture2D>("Clouds\\Cloud1\\C3"));
            a1.frames.Add(cm.Load<Texture2D>("Clouds\\Cloud1\\C4"));
            a1.frames.Add(cm.Load<Texture2D>("Clouds\\Cloud1\\C5"));

            string filename = "Clouds\\Cloud9\\Cloud9_Layer ";
            for (int i = 1; i <= 82; i++)
            {
                a2.frames.Add(cm.Load<Texture2D>(filename + i));
            }

            Entity c2 = new Entity();
            c2.worldPosition.X = 600.0f;
            c2.worldPosition.Y = 300.0f;
            c1.animation.Add("1", a1);
            c1.currentAnimation = "1";
            c2.animation.Add("1", a2);
            c2.currentAnimation = "1";
            clouds.objects.Add(c1);
            clouds.objects.Add(c2);
            clouds.SetDimensions(levelSize);
            backgrounds.Add(clouds,false);

            backgrounds.Initialize();
            */
            #endregion

            
            #region Physics
            
            ps = new PhysicalReality();
            ps2 = new PhysicalReality();
            startPos = new Vector2(200.0f, 200.0f);
            Random rand = new Random();
            cockInKrisButt = new Vector2(1000.0f, 200.0f);

            List<Vector2> start;
            start = new List<Vector2>();
            start.Add(new Vector2(200.0f, 200.0f));
            start.Add(new Vector2(200.0f, 400.0f));
            start.Add(new Vector2(400.0f, 400.0f));
            start.Add(new Vector2(400.0f, 200.0f));

            for (int i = 0; i < 4; i++)
            {
                ps.particles.Add(new Particle(mass, start[i]));
                //ps.particles[i].force += new Vector2((float)(12000.0 * Math.Cos((i * Math.PI) / 20)), (float)(18000 * Math.Sin((i * Math.PI) / 20)));
            }
            for (int i = 0; i < (ps.particles.Count - 1); i++)
            {
                ps.springs.Add(new Spring(40.0f, 20.0f, 200.0f));
                ps.springs[i].index1 = i;
                ps.springs[i].index2 = i + 1;
            }

            Spring s1 = new Spring(20.0f, 20.0f, (float)Math.Sqrt(80000.0));
            Spring s2 = new Spring(20.0f, 20.0f, (float)Math.Sqrt(80000.0));
            Spring b1 = new Spring(40.0f, 20.0f, 200.0f);
            s1.index1 = 0;
            s1.index2 = 2;
            s2.index1 = 1;
            s2.index2 = 3;
            b1.index1 = 0;
            b1.index2 = 3;
            ps.springs.Add(s1);
            ps.springs.Add(s2);
            ps.springs.Add(b1);

            Particle fix = new Particle(mass, new Vector2(700.0f, 100.0f));
            //fix.physicsActivated = false;
            Particle p1 = new Particle(mass, new Vector2(800.0f, 100.0f));

            ps2.particles.Add(fix);
            ps2.particles.Add(p1);

            Spring sfix1 = new Spring(20.0f, 20.0f, 100.0f);
            sfix1.index1 = 0;
            sfix1.index2 = 1;

            Particle p2 = new Particle(mass, new Vector2(900.0f, 100.0f));
            ps2.particles.Add(p2);

            Spring s12 = new Spring(20.0f, 20.0f, 100.0f);
            s12.index1 = 1;
            s12.index2 = 2;

            Particle p3 = new Particle(mass, new Vector2(1000.0f, 100.0f));
            ps2.particles.Add(p3);

            Spring s23 = new Spring(20.0f, 20.0f, 100.0f);
            s23.index1 = 2;
            s23.index2 = 3;

            Particle p4 = new Particle(mass, new Vector2(1100.0f, 100.0f));
            ps2.particles.Add(p4);

            Spring s34 = new Spring(20.0f, 20.0f, 100.0f);
            s34.index1 = 3;
            s34.index2 = 4;

            Particle p5 = new Particle(mass, new Vector2(1200.0f, 100.0f));
            ps2.particles.Add(p5);

            Spring s45 = new Spring(20.0f, 20.0f, 100.0f);
            s45.index1 = 4;
            s45.index2 = 5;

            ps2.springs.Add(sfix1);
            ps2.springs.Add(s12);
            ps2.springs.Add(s23);
            ps2.springs.Add(s34);
            ps2.springs.Add(s45);

            //ps.externalGlobalForces.Add(new Vector2(0.0f, 120.81f));
            ps2.externalGlobalForces.Add(new Vector2(0.0f, 120.81f));

            img = cm.Load<Texture2D>("redsquare");
            //sp = cm.Load<Texture2D>("spring");
            
            #endregion

            
        }

        private bool scroll()
        {
            return (player.worldPosition.X >= (screenSize.X / 2.0f) && player.worldPosition.X <= (levelSize.X - (screenSize.X / 2.0f)));
        }
        


        public void Update(int milli, Input input)
        {
            ScrollEvent s = new ScrollEvent();
            /*if (input.playerMoveLeft)
            {
                //ps.turnOff();
                //ps2.turnOn();
                if (scroll())
                {
                    s.scrollLeft();
                    screenPos = backgrounds.screenPosition;
                }
                
            }
            else if (input.playerMoveRight)
            {
                //ps.turnOn();
                if (scroll())
                {
                    s.scrollRight();
                    screenPos = backgrounds.screenPosition;
                }
            }
            else
            {
                s.stopScroll();
            }
            */
            if (input.playerJump)
            {
                Vector2 jump = new Vector2();
                jump.Y = -10.0f;
                jump.X = (player.reverseSprite) ? -5.0f : 5.0f;
                player.physics.particles[0].velocity += jump;
            }

            //backgrounds.Update(milli, s);

            player.Update(milli, input,s);

            #region PhysicsUpdate
           if (milli > 0)
            {
                
                for (int i = 0; i < ps.particles.Count; i++)
                {
                    ps.particles[i].force += ps.particles[i].mass * new Vector2(0.0f,120.81f);
                    if (ground.testIntersection(ps.particles[i].position) == PointIntersection.ABOVE)
                    {
                        
                        //ps.particles[i].position.Y = 767;
                        
                        Vector2 force = -2000*(2*ps.particles[i].mass*ps.particles[i].velocity)/milli;
                        
                        ps.particles[i].force += force;
                    }
                    
                }

                for (int i = 0; i < ps2.particles.Count; i++)
                {
                    if ((ps2.particles[i].position.Y < (cockInKrisButt.Y + 4)) && (ps2.particles[i].position.Y > (cockInKrisButt.Y - 4)))
                    {
                        if ((ps2.particles[i].position.X < (cockInKrisButt.X + 4)) && (ps2.particles[i].position.X > (cockInKrisButt.X - 4)))
                        {
                            ps2.particles[i].physicsActivated = false;
                        }
                    }
                }

                ps.stepSimulation(milli);
                ps2.stepSimulation(milli);

            }
            #endregion
        }

        public void Draw(SpriteBatch s, int milli)
        {
            //backgrounds.DrawBackLayers(g.spriteBatch);
           
            for (int i = 0; i < ps.particles.Count; i++)
            {
                s.Draw(img, ps.particles[i].position, Color.White);
                
            }
            for (int i = 0; i < ps2.particles.Count; i++)
            {
                s.Draw(img, ps2.particles[i].position, Color.White);

            }
           
            player.Draw(s);
            foreach (Line l in ground.geometry)
            {
                l.Draw(s);
            }

            //backgrounds.DrawFrontLayers(g.spriteBatch);

        }
    }
}
