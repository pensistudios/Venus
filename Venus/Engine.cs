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
    class Engine
    {
        /*
         * The main controller of all game states
         * also would be responisble for the menu
         */

        Texture2D splash;
        IPlayable currentGame;
        float splashScreenTime = 3.0f;
        float dt = 0.0f;
        bool flashOn = true;
        SoundEffect music;
        bool musicPlaying = false;

        public Engine(ContentManager cm, Vector2 dim)
        {
            currentGame = new MegaGame(cm,dim);
            splash = cm.Load<Texture2D>("splashscreen");
            music = cm.Load<SoundEffect>("8bit");
            

        }

        public void Update(int milli, Input input)
        {
            dt += milli;
            if ((dt > (splashScreenTime * 1000) || input.playerJump) && !musicPlaying)
            {
                flashOn = false;
                musicPlaying = true;
                music.Play();
            }

            if(!flashOn)
                currentGame.Update(milli,input);
        }

        public void Draw(SpriteBatch s, int milli)
        {
            if (flashOn)
            {
                s.Draw(splash, Vector2.Zero, Color.White);
            }
            else
            {
                currentGame.Draw(s, milli);
            }
        }

    }
}
