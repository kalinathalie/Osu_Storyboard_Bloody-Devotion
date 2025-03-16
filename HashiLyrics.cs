using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Saheki;
using static Saheki.Helpers;
using StorybrewCommon.Storyboarding.Commands;

namespace StorybrewScripts
{
    public class HashiLyrics : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            lyrics(99445, new string[]
                {
                    "SlaveofBlood",
                    "RainofKnives",
                    "Wastethemall",
                    "Destroy",
                }
            );

            lyrics(178349, new string[]
                {
                    "SlaveofBlood",
                    "RainofKnives",
                    "Wastethemall",
                    "Thoroughly",
                }
            );

            lyrics2(184924, new string[]
                {
                    "Destroy",
                    "Sacrifice",
                    "KillEmAll",
                    "KillEmAll",
                }
            );

            lyrics(263828, new string[]
                {
                    "SlaveofBlood",
                    "RainofKnives",
                    "Wastethemall",
                    "Destroy",
                }
            );

        }


        public void lyrics(int startTime, string[] paths)
        {
            const int interval = 1644;
            const int beat = 411;

            for (int index = 0; index != paths.Length; ++index)
            {
                int time = startTime + index * interval;

                var filePath = $"{paths[index]}0.png";
                var file2Path = $"{paths[index]}1.png";
                
                var scale = 853f / SafeBitmapSize(Path.Combine("sb", "BridgeLyrics", filePath)).X;
                scale *= 0.9f;  

                var posicao = new Vector2(320,240);
                var sprite2 = GetLayer("BridgeLyricsFront").CreateSprite(Path.Combine("sb", "BridgeLyrics", filePath), OsbOrigin.Centre, posicao);
                var sprite = GetLayer("BridgeLyricsFront").CreateSprite(Path.Combine("sb", "BridgeLyrics", filePath), OsbOrigin.Centre, posicao);
                var sprite3 = GetLayer("BridgeLyricsBack").CreateSprite(Path.Combine("sb", "BridgeLyrics", file2Path), OsbOrigin.Centre, posicao);
                
                sprite.Fade(time, 1);
                sprite.Scale(time, scale);
                sprite.Color(time, Color4.Red);
                sprite.Fade(time + beat, time + beat + beat / 4, 1, 0);
                
                time += beat;

                sprite2.Fade(time, 1);
                sprite2.Scale(time, scale);
                sprite2.Color(time, Color4.Black);
                sprite2.Fade(time + beat / 2, time + beat / 2 + beat / 4, 1, 0);
                
                
                time += beat / 2;

                sprite3.Fade(time, 1);
                sprite3.Scale(time, scale);
                sprite3.Color(time, Color4.Black);
                if(startTime == 263828 && index == 3){
                    sprite3.Fade(startTime + index * interval + interval, 271226, 1, 0);
                }else{
                    sprite3.Fade(startTime + index * interval + interval, 0);
                }
            }
        }

        public void lyrics2(int startTime, string[] paths)
        {
            const int interval = 1644;
            const int beat = 411;

            for (int index = 0; index != paths.Length; ++index)
            {
                int time = startTime + index * interval;

                var filePath = $"{paths[index]}0.png";
                var file2Path = $"{paths[index]}1.png";
                
                var scale = 853f / SafeBitmapSize(Path.Combine("sb", "BridgeLyrics", file2Path)).X;
                scale *= 0.9f;

                var posicao = new Vector2(320,240);
                if(index==1){
                    posicao = new Vector2(320,150);
                }

                var sprite2 = GetLayer("BridgeLyricsFront").CreateSprite(Path.Combine("sb", "BridgeLyrics", file2Path), OsbOrigin.Centre, posicao);
                var sprite = GetLayer("BridgeLyricsFront").CreateSprite(Path.Combine("sb", "BridgeLyrics", file2Path), OsbOrigin.Centre, posicao);
                sprite.Fade(time, 1);
                sprite.Scale(time, scale);
                sprite.Color(time, Color4.Red);
                sprite.Fade(time + beat, time + beat + beat / 4, 1, 0);
                
                time += beat;

                sprite2.Fade(time, 1);
                sprite2.Scale(time, scale);
                sprite2.Color(time, Color4.Black);
                sprite2.Fade(time + beat / 2, time + beat / 2 + beat / 4, 1, 0);
                
                time += beat / 2;

                var sprite3 = GetLayer("Edu").CreateSprite(Path.Combine("sb", "BridgeLyrics", file2Path), OsbOrigin.Centre, posicao);
                sprite3.Fade(time, 1);
                sprite3.Scale(time, scale);
                sprite3.Color(time, Color4.Black);
                if(index == 3){
                    sprite3.Fade(startTime + index * interval + interval, 191500, 1, 0);
                    Log($"Edu");
                }else{
                    sprite3.Fade(startTime + index * interval + interval, 0);
                }
            }
        }

        // Utils

        private static Vector2 calculateNewPosition(Vector2 startPosition, float angleRadians, float distance)
        {
            float newX = startPosition.X + distance * (float)Math.Cos(angleRadians);
            float newY = startPosition.Y + distance * (float)Math.Sin(angleRadians);

            return new Vector2(newX, newY);
        }

        // Note: GetBitmapSize is unsafe it might not dispose correcly, adds boild plate and is slow.
        private Vector2 SafeBitmapSize(string path)
        {
            Image image = null;
            try
            {
                image = Image.FromFile(Path.Combine(MapsetPath, path));
                var result = new Vector2(image.Width, image.Height);

                return result;
            }
            finally
            {
                if (image != null) image.Dispose();
            }
        }

        private OsbSprite Spritehelper(Vector2 position, string layer = "", string filePath = "", int startTime = 0, float opacity = 1f, float scale = 1f, float rotation = 0, bool combine = true)
        {
            var sprite = GetLayer(layer).CreateSprite(combine ? Path.Combine("sb", filePath) : filePath, OsbOrigin.Centre, position);
            if (opacity != 0) sprite.Fade(startTime, opacity);
            if (scale != 0) sprite.Scale(startTime, scale);
            if (rotation != 0) sprite.Rotate(startTime, rotation);

            return sprite;
        }
    }
}