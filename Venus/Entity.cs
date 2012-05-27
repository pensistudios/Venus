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
    class Entity 
    {
        
        //Bounding Area

        //IPhysical physicsData;
        public Vector2 worldPosition;
        public Vector2 screenPosition;
        public Dictionary<string, AnimationController> animation;
        public string currentAnimation;

        //public Vector2 moveSpeed;

        public PhysicalReality physics;

        //Rendering Info
        public bool reverseSprite;

        public Entity()
        {
            animation = new Dictionary<string, AnimationController>();
            worldPosition = new Vector2();
            screenPosition = new Vector2();
            reverseSprite = false;
            physics = new PhysicalReality();
        }


        public void Update(int milli)
        {
            animation[currentAnimation].Update(milli);
            //physics.stepSimulation(milli);
        }

        public void Draw(SpriteBatch s)
        {
            if (reverseSprite)
                s.Draw(animation[currentAnimation].getCurrentFrame(), screenPosition, null, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.FlipHorizontally, 0.0f);
            else
                s.Draw(animation[currentAnimation].getCurrentFrame(), screenPosition, Color.White);
        }
        

    }
}
