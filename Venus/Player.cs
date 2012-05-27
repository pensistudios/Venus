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
    class Player : Entity
    {

        public string moving;
        public string standing;
        public Vector2 moveSpeed;
        bool jumping;

        public Player()
        {
            moving = "MOVING";
            standing = "STANDING";
            moveSpeed = new Vector2(5.0f, 0.0f);
        }

        public void Update(int milli, Input input, ScrollEvent s)
        {
            //System.Console.WriteLine(s.isScrolling + " input " + input.playerMoveRight);
            if (input.playerMoveLeft)
            {
                worldPosition -= moveSpeed;
                currentAnimation = moving;
                reverseSprite = true;
                if (!s.isScrolling)
                {
                    screenPosition -= moveSpeed;
                }
            }
            else if (input.playerMoveRight)
            {
                if (!s.isScrolling)
                {
                    //System.Console.WriteLine(screenPosition);
                    screenPosition += moveSpeed;
                }
                worldPosition += moveSpeed;
                currentAnimation = moving;
                reverseSprite = false;
            }
            else
            {
                currentAnimation = standing;
            }
            /*if (input.playerJump)
                jumping = true;*/

           // physics.particles[0].position = screenPosition;
            
            base.Update(milli);

           //if(jumping)
             //   screenPosition = physics.particles[0].position;
        }

        public void Draw(SpriteBatch s)
        {
            base.Draw(s);
        }

    }
}
