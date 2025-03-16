using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    
    public class LyricsPreKiaiEng : StoryboardObjectGenerator
    {
        [Configurable]
        public string SubtitlesPath = "lyrics.srt";

        [Configurable]
        public string SpritesPath = "sb/f";

        [Configurable]
        public string TimestampSplit = "";

        [Configurable]
        public string TracePath = "";

        [Configurable]
        public int FontSize = 26;

        [Configurable]
        public float FontScale = 0.5f;

        [Configurable]
        public Color4 FontColor = Color4.White;

        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;

        [Configurable]
        public int OutlineThickness = 0;

        [Configurable]
        public Color4 OutlineColor = new Color4(0, 0, 0, 100);

        [Configurable]
        public int ShadowThickness = 0;

        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 100);

        [Configurable]
        public Vector2 Padding = Vector2.Zero;

        [Configurable]
        public float SubtitleX = 240;

        [Configurable]
        public float SubtitleY = 400;

        [Configurable]
        public int StartTrace = 0;

        [Configurable]
        public int EndTrace = 0;

        [Configurable]
        public bool TrimTransparency = true;

        [Configurable]
        public bool EffectsOnly = false;

        [Configurable]
        public bool Debug = false;

        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;

        StoryboardLayer layer;

        FontGenerator japFont;
        public override void Generate()
        {
		    layer = GetLayer("lyrics"); 

		    japFont = LoadFont(SpritesPath, new FontDescription()
            {
                FontPath = "fonts/Berliman.otf",
                FontSize = FontSize,
                Color = FontColor,
                Padding = Padding,
                FontStyle = FontStyle,
                TrimTransparency = TrimTransparency,
                EffectsOnly = EffectsOnly,
            },
            new FontShadow()
            {
                Thickness = ShadowThickness,
                Color = ShadowColor,
            });

            generateLineSpecialRight("Wash  the  sand  away", 53418, 62870);
            generateLineSpecialRight("Kill  its  breath", 55472, 62870);
            generateLineSpecialRight("Sink  the  singing  pendulum", 58144, 62870);
            generateLineSpecialRight("To  the  bottom", 61226, 62870);
            generateLineSpecialLeft("Attach  the  chipped", 63281, 72733);
            generateLineSpecialLeft("Illusionist's  mask", 65130, 72733);
            generateLineSpecialLeft("To  a  shadow  -  does  it  not  crack", 68212, 72733);
            generateLineSpecialLeft("Dance  a", 70267, 72733);
            generateLineSpecialCenter("masquerade", 71294 , 72733);

            yValueRight = 175;
            yValueLeft = 175;

            generateLineSpecialRight("What  if  your  love", 132322, 141774);
            generateLineSpecialRight("Was  pure  fiction?", 134582, 141774);
            generateLineSpecialRight("What  if  you  tore", 137253, 141774);
            generateLineSpecialRight("My  body  apart?", 140335, 141774);
            generateLineSpecialLeft("For  my  mistress", 142185, 151637);
            generateLineSpecialLeft("I  will  let  my  blades  dance", 145267, 151637);
            generateLineSpecialLeft("Until  the  night  ends,   with  my  mistress", 147116, 151637);
            generateLineSpecialCenter("I  will  be", 150198, 151637);
        }

        static double yValueRight = 175;
        public void generateLineSpecialRight(String lyric, int StartTime, int EndTime){
            double scale = 0.15;
            double scaleY = 0.15;
            double lineWidth = 0f;
            int fadeDuration = 250;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }

            double posX = 500;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());

                if(!texture.IsEmpty)
                {
                    float adicional = (letter == 'q' || letter == 'g' || letter == 'y' || letter == 'p') ?  6.0f : 0;

                    adicional = (letter == 'f')  ?  3.0f : adicional;

                    var sprite = layer.CreateSprite(texture.Path, OsbOrigin.BottomCentre, new Vector2((float)posX, (float)yValueRight + adicional));
                    sprite.ScaleVec(StartTime, scale, scaleY);
                    sprite.Fade(StartTime, StartTime + fadeDuration, 0, 1);
                    sprite.Color(StartTime, 0.68, 0.85, 1);
                    sprite.Fade(EndTime - fadeDuration, EndTime, 1, 0);
                }
                posX += texture.BaseWidth * scale;
            }
            yValueRight += 60;
        }

        static double yValueLeft = 175;
        public void generateLineSpecialLeft(String lyric, int StartTime, int EndTime){
            double scale = 0.15;
            double scaleY = 0.15;
            double lineWidth = 0f;
            int fadeDuration = 125;
            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }
            double posX = -30;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());

                if(!texture.IsEmpty)
                {
                    float adicional = (letter == 'q' || letter == 'g' || letter == 'y' || letter == 'p') ?  6.0f : 0;

                    adicional = (letter == 'f')  ?  3.0f : adicional;

                    var sprite = layer.CreateSprite(texture.Path, OsbOrigin.BottomCentre, new Vector2((float)posX, (float)yValueLeft + adicional));
                    sprite.ScaleVec(StartTime, scale, scaleY);
                    sprite.Fade(StartTime, StartTime + fadeDuration, 0, 1);
                    sprite.Color(StartTime, 0.68, 0.85, 1);
                    sprite.Fade(EndTime - fadeDuration, EndTime, 1, 0);
                }
                posX += texture.BaseWidth * scale;
            }
            yValueLeft += 60;
        }
        static double yValueCenter = 355;
        public void generateLineSpecialCenter(String lyric, int StartTime, int EndTime){
            double scale = 0.2;
            double scaleY = 0.2;
            double lineWidth = 0f;
            int fadeDuration = 250;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }
            double posX = 50;
            if(lyric != "masquerade"){
                posX = -30;
            }

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());

                if(!texture.IsEmpty)
                {                    
                    float adicional = (letter == 'q' || letter == 'g' || letter == 'y' || letter == 'p') ?  6.0f : 0;

                    adicional = (letter == 'f')  ?  3.0f : adicional;

                    var sprite = layer.CreateSprite(texture.Path, OsbOrigin.BottomCentre, new Vector2((float)posX, (float)yValueCenter + adicional));
                    
                    sprite.ScaleVec(StartTime, scale, scaleY);  
                    sprite.Fade(StartTime, StartTime + fadeDuration, 0, 1);
                    sprite.Color(StartTime, Color4.Red);
                    sprite.Fade(EndTime - fadeDuration, EndTime, 1, 0);
                }
                posX += texture.BaseWidth * scale;
            }
        }
    }
}
