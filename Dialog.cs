using OpenTK;
using StorybrewCommon.Mapset;
using StorybrewCommon.Storyboarding;
using System;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Drawing;
using Saheki;
using static Saheki.Helpers;

namespace StorybrewScripts
{
    public class Dialog : StoryboardObjectGenerator
    {
        [Configurable] string FontPath = "fonts/monogram.ttf";
        [Configurable] int FontSize = 125;

        private FontGenerator Font;
        public override void Generate()
        {
            Font = LoadFont(Path.Combine("sb", "Dialog"), new FontDescription()
            {
                FontPath = FontPath,
                FontSize = FontSize,
                Color = Color.White,
            });

            var positionSakuya = new Vector2(40, 300);
            var positionRemilia = new Vector2(165, 125);
            var positionBoth = new Vector2(320, 240); // ScreenToOsu(1920 /2, 1080/2);
            
            string sakuyaHead = "sb/head/sakuyaCabecao.png";
            string remiliaHead = "sb/head/remiliaCabecao.png";
            if (!File.Exists(Path.Combine(MapsetPath, sakuyaHead))) throw new Exception($"missing File {sakuyaHead}");
            if (!File.Exists(Path.Combine(MapsetPath, remiliaHead))) throw new Exception($"missing File {remiliaHead}");

            GenerateDialog(positionSakuya, 205472, 206705, 206065, sakuyaHead, "Mistress...");
            GenerateDialog(positionRemilia, 206705, 208760, 207734, remiliaHead, "Shall we dance under\nthis beautiful moon?");
            GenerateDialog(positionSakuya, 208760, 211226, 209790, sakuyaHead, "M'lady, if that's what you wish\nI'll gladly dance with you.");
            GenerateDialog(positionRemilia, 211226, 213280, 212239, remiliaHead, "Excellent, I always\nappreciate your Devotion.");
            GenerateDialog(positionRemilia, 213280, 216157, 214302, remiliaHead, "This shall be the ultimate\ntest of your abilities.");
            GenerateDialog(positionBoth, 216157, 217390, 216745, "", "Let's go!");
        }

        private void GenerateDialog(Vector2 position, int startTime, int endTime, int endText, string headPath, string dialog)
        {
            const int textSize = 25;
            const int dialogPadding = 5;
            
            // assert pixelpath
            var pixelPath = Path.Combine(MapsetPath, "sb", "pixel.png");
            if (!File.Exists(pixelPath)) throw new Exception($"missing File {pixelPath}");

            var lines = dialog.Split('\n');
            var effect = new TextDelayNotGamer(Beatmap)
            {
                FadeIn = false,
            };

            // get total size of the text
            var totalSize = Vector2.Zero;
            foreach (var line in lines)
            {
                var size = Text.Size(Font, line, textSize);
                totalSize.X = Math.Max(size.X, totalSize.X);
                totalSize.Y += size.Y;
            }

            var cursor = position;

            // apply position offset
            cursor.Y -= totalSize.Y;
            cursor.X -= totalSize.X / 2;

            GenerateDialogBox(false, endText, position - new Vector2(0, totalSize.Y / 2), totalSize / 2 + new Vector2(dialogPadding, dialogPadding), startTime, endTime, 2);

            // generate text
            var lineAnimation = Snap(Beatmap, startTime, 1, 1);
            var textDelay = Snap(Beatmap, startTime, 1, 2);
            for (var index = 0; index != lines.Length; ++index)
            {
                var line = lines[index];
                cursor.Y += Text.Generate(this, Font, line, cursor, startTime + textDelay + lineAnimation * index, endTime, textSize, effect).Height;
            }

            if (headPath == "") return;

            var headImage = GetMapsetBitmap(headPath);
            var headSize = new Vector2(headImage.Width, headImage.Height);

            var positionCabecao = position;
            positionCabecao.X -= totalSize.X / 2 - headSize.X / 2;
            positionCabecao.Y -= totalSize.Y + headSize.Y + dialogPadding;

            GenerateDialogBox(true, endText, positionCabecao, headSize / 2 + new Vector2(dialogPadding, dialogPadding), startTime, endTime, 2);

            var animation = lineAnimation;
            var head = GetLayer("").CreateSprite(headPath);
            head.Scale(startTime, startTime + animation, 0, 1);
            head.Move(startTime, positionCabecao);
            head.Fade(startTime, startTime + animation, 0, 1);
            head.Fade(endTime - animation, endTime, 1, 0);
        }
        
        public void GenerateDialogBox(bool isCabecao, int endText, Vector2 position, Vector2 size, int startTime, int endTime, int snap)
        {
            // Constants for dialog box appearance
            const float width = 0.5f; // Border width
            const float extention = 4; // Extension for scaling

            var backgroundColor = new Color4(58, 26, 2, 255);
            var pixelPath = Path.Combine(MapsetPath, "sb", "pixel.png");
            var background = GetLayer("").CreateSprite(pixelPath);

            // Calculate animation timing based on snap
            var animation = Snap(Beatmap, startTime, 1, snap);
            
            background.Color(startTime, backgroundColor);
            background.Move(startTime, position);
            if(!isCabecao){
                Vector2[] position1 = {
                    new Vector2(-1, -1),
                    new Vector2(1, -1),
                    new Vector2(1, 1),
                    new Vector2(-1, 1),
                };
                var i = 0;
                for(int x= startTime + animation; x<endText; x+=60){
                    background.Move(x, startTime + animation, position+position1[i%4], position+position1[(i+1)%4]);
                    i++;
                }
                background.Move(endText, endText+60, position+position1[i%4], position);
            }
            background.ScaleVec(OsbEasing.InOutSine, startTime, startTime + animation, Vector2.Zero, size);
            background.Fade(startTime, 1);
            background.Fade(endTime - animation, endTime, 1, 0);

            GenerateDialogBorder(isCabecao, endText, position, position - new Vector2(0, size.Y), new Vector2(size.X + extention, width), startTime, endTime, snap); // top
            GenerateDialogBorder(isCabecao, endText, position, position + new Vector2(0, size.Y), new Vector2(size.X + extention, width), startTime, endTime, snap); // bot
            GenerateDialogBorder(isCabecao, endText, position, position - new Vector2(size.X, 0), new Vector2(width, size.Y + extention), startTime, endTime, snap); // left
            GenerateDialogBorder(isCabecao, endText, position, position + new Vector2(size.X, 0), new Vector2(width, size.Y + extention), startTime, endTime, snap); // right

            // NOTE: talvez fazer a caixa tremer ?
        }

        public void GenerateDialogBorder(bool isCabecao, int endText, Vector2 initialPosition, Vector2 finalPosition, Vector2 size, int startTime, int endTime, int snap)
        {
            var pixelPath = Path.Combine(MapsetPath, "sb", "pixel.png");
            var color = new Color4(119, 93, 77, 255);
            var animation = Snap(Beatmap, startTime, 1, snap);
            var border = GetLayer("").CreateSprite(pixelPath);
            
            border.Color(startTime, color);
            border.Move(startTime, startTime+animation, initialPosition, finalPosition);
            if(!isCabecao){
                Vector2[] position1 = {
                    new Vector2(-1, -1),
                    new Vector2(1, -1),
                    new Vector2(1, 1),
                    new Vector2(-1, 1),
                };
                var i = 0;
                for(int x= startTime + animation; x<endText; x+=60){
                    border.Move(x, startTime + animation, finalPosition+position1[i%4], finalPosition+position1[(i+1)%4]);
                    i++;
                }
                border.Move(endText, endText+60, finalPosition+position1[i%4], finalPosition);
            }
            border.ScaleVec(startTime, startTime + animation, Vector2.Zero, size);
            border.Fade(startTime, 1);
            border.Fade(endTime - animation, endTime, 1, 0);
        }
    }

    public class TextDelayNotGamer : Text.IEffect
    {
        private readonly Beatmap Beatmap;

        public bool FadeIn = true;
        public bool FadeOut = true;
        public float Opacity = 1f;

        public TextDelayNotGamer(Beatmap beatmap) { Beatmap = beatmap; }

        public void Draw(OsbSprite sprite, Vector2 position, int startTime, int endTime, float scale, int index, 
            int count, float height, float width, char character, string text)
        {
            var delay = Helpers.Snap(Beatmap, startTime, 1, 1);
            int animation = index * delay / count;

            if (startTime + animation > endTime) throw new Exception($"TextDelayNotGamer, {text}, {startTime}: Duration is too short to apply this effect");

            sprite.Scale(startTime + animation, scale);
            sprite.Move(startTime + animation, position);

            if (FadeIn) sprite.Fade(OsbEasing.InOutSine, startTime + animation, startTime + animation + delay, 0, Opacity);
            else sprite.Fade(startTime + animation, Opacity);

            if (FadeOut) sprite.Fade(OsbEasing.InOutSine, endTime - delay , endTime, Opacity, 0);
            else sprite.Fade(endTime - 1, endTime, Opacity, 0);
        }
    }
}


