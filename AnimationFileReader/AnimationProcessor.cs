using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using System.IO;

// TODO: replace these with the processor input and output types.
using TInput = System.String;
using TOutput = List<Microsoft.Xna.Framework.Graphics.Texture2D>;

namespace AnimationFileReader
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "AnimationFileReader.AnimationProcessor")]
    public class AnimationProcessor : ContentProcessor<TInput, TOutput>
    {
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            String[] returnData = null;

            try
            {
                StreamReader streamReader = new StreamReader(input);
                String line;

                while ((line = streamReader.ReadLine()) != null)
                {
                    
                }

                streamReader.Close();
            }
            catch (Exception ex)
            {
                // Do things here incase it can't read the file
            }

            returnData = (String[])stageTitles.ToArray(typeof(String));
            return returnData;
        }
    }
}