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

namespace AnimationLib
{
    //Provides the index into a zero indexed list of frames for an animation
    //Can be used in a variety of ways

    public enum AnimationType
    {
        LOOP,
        SINGLE,
        WRAP
    };

    public class AnimationController
    {
        

        //Indicates if frames play in reverse order
        bool reverse;

        float frequency;
        float phase;

        int frameCount;
        AnimationType type;

        float dt;

        bool stopped;

        public List<Texture2D> frames;
        public int index;

        public AnimationController(AnimationType type_, float freq_, float phase_, bool rev_)
        {
            reverse = rev_;

            frequency = freq_;
            phase = phase_;

            type = type_;
            dt = 0;

            stopped = false;

            frames = new List<Texture2D>();
            
        }

        public void setFrames(List<Texture2D> frames_)
        {
            frames = frames_;

            if (reverse)
                index = frames.Count - 1;
        }

        public Texture2D getCurrentFrame()
        {
            return frames[index];
        }

        public void Update(int milli)
        {
            if (!stopped)
            {
                dt += milli;

                if ((dt + phase) >= (frequency * 1000))
                {
                    dt = 0;

                    if (!reverse)
                        index++;
                    else
                        index--;

                    if (index == frames.Count)
                    {
                        if (type == AnimationType.LOOP)
                        {
                            index = 0;
                        }
                        if (type == AnimationType.SINGLE)
                        {
                            stopped = true;
                            index--;
                        }
                        if (type == AnimationType.WRAP)
                        {
                            reverse = true;
                            index -= 2;
                        }
                    }
                    else if (index < 0)
                    {
                        if (type == AnimationType.LOOP)
                        {
                            index = frameCount - 1;
                        }
                        if (type == AnimationType.SINGLE)
                        {
                            stopped = true;
                            index++;
                        }
                        if (type == AnimationType.WRAP)
                        {
                            reverse = false;
                            index += 2;
                        }
                    }
                }
            }
        }
    }
}
