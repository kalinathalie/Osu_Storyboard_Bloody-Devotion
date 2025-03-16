using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace StorybrewScripts
{
    public class LyricsEngKiai : StoryboardObjectGenerator
    {
        [Description("Path to a .sbv, .srt, .ass or .ssa file in your project's folder.\nThese can be made with a tool like aegisub.")]
        [Configurable] public string SubtitlesPath = "lyrics.srt";
        [Configurable] public float SubtitleY = 400;

        [Group("Font")]
        [Description("The name of a system font, or the path to a font relative to your project's folder.\nIt is preferable to add fonts to the project folder and use their file name rather than installing fonts.")]
        [Configurable] public string FontName = "Verdana";
        [Description("A path inside your mapset's folder where lyrics images will be generated.")]
        [Configurable] public string SpritesPath = "sb/f";
        [Description("The Size of the font.\nIncreasing the font size creates larger images.")]
        [Configurable] public int FontSize = 26;
        [Description("The Scale of the font.\nIncreasing the font scale does not creates larger images, but the result may be blurrier.")]
        [Configurable] public float FontScale = 0.5f;
        [Configurable] public Color4 FontColor = Color4.White;
        [Configurable] public FontStyle FontStyle = FontStyle.Regular;

        [Group("Outline")]
        [Configurable] public int OutlineThickness = 3;
        [Configurable] public Color4 OutlineColor = new Color4(50, 50, 50, 200);

        [Group("Shadow")]
        [Configurable] public int ShadowThickness = 0;
        [Configurable] public Color4 ShadowColor = new Color4(0, 0, 0, 100);

        [Group("Glow")]
        [Configurable] public int GlowRadius = 0;
        [Configurable] public Color4 GlowColor = new Color4(255, 255, 255, 100);
        [Configurable] public bool GlowAdditive = true;

        [Group("Misc")]
        [Configurable] public bool PerCharacter = true;
        [Configurable] public bool TrimTransparency = true;
        [Configurable] public bool EffectsOnly = false;
        [Description("How much extra space is allocated around the text when generating it.\nShould be increased when characters look cut off.")]
        [Configurable] public Vector2 Padding = Vector2.Zero;
        [Configurable] public OsbOrigin Origin = OsbOrigin.Centre;
        public class Time{
            public int FirstTime { get; set; }
            public int LastTime { get; set; }
            public float FirstPosition { get; set; }
            public float LastPosition { get; set; }
        }
        public override void Generate()
        {
            var font = LoadFont(SpritesPath, new FontDescription()
            {
                FontPath = FontName,
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

            var subtitles = LoadSubtitles(SubtitlesPath);

            if (GlowRadius > 0 && GlowAdditive)
            {
                var glowFont = LoadFont(Path.Combine(SpritesPath, "glow"), new FontDescription()
                {
                    FontPath = FontName,
                    FontSize = FontSize,
                    Color = FontColor,
                    Padding = Padding,
                    FontStyle = FontStyle,
                    TrimTransparency = TrimTransparency,
                    EffectsOnly = true,
                },
                new FontGlow()
                {
                    Radius = GlowRadius,
                    Color = GlowColor,
                });
                generatePerCharacter(glowFont, subtitles, GetLayer("glow"), true);
            }
            generatePerCharacter(font, subtitles, GetLayer(""), false);
        }

        public void generatePerCharacter(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer, bool additive)
        {
            string json = File.ReadAllText("projects/Bloody Devotion/time/times.json");
            List<Time> intervalos = JsonConvert.DeserializeObject<List<Time>>(json);
            
            foreach (var subtitleLine in subtitles.Lines)
            {
                var tempoInicial = subtitleLine.StartTime;
                var tempoFinal = subtitleLine.EndTime;
                float addX1 = 0f;
                float addX2 = 0f;
                var initTime = 0;
                var finalTime = 0;
                

                var letterY = SubtitleY;
                foreach (var line in subtitleLine.Text.Split('\n'))
                {
                    var lineWidth = 0f;
                    var lineHeight = 0f;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                    }

                    var letterX = 320 - lineWidth * 0.5f;
                    foreach (var letter in line)
                    {
                        var texture = font.GetTexture(letter.ToString());
                        if (!texture.IsEmpty)
                        {
                            
                            var position = new Vector2(letterX, letterY)
                                + texture.OffsetFor(Origin) * FontScale;

                            var sprite = layer.CreateSprite(texture.Path, Origin, position);
                            sprite.Scale(subtitleLine.StartTime, FontScale);
                            foreach (var intervalo in intervalos){
                                bool choque = 
                                (tempoInicial >= intervalo.FirstTime && tempoInicial <= intervalo.LastTime) || // Checa se tempoInicial está dentro do intervalo JSON
                                (tempoFinal >= intervalo.FirstTime && tempoFinal <= intervalo.LastTime) ||     // Checa se tempoFinal está dentro do intervalo JSON
                                (intervalo.FirstTime >= tempoInicial && intervalo.LastTime <= tempoFinal);    // Checa se o intervalo JSON está inteiramente dentro do intervalo dado
                                if (choque)
                                {
                                    initTime = intervalo.FirstTime;
                                    finalTime = intervalo.LastTime;
                                    addX1 = intervalo.FirstPosition;
                                    addX2 = intervalo.LastPosition;
                                    if(initTime<=subtitleLine.StartTime) sprite.Fade(initTime, 0);
                                    sprite.Move((initTime==217801)? OsbEasing.Out : OsbEasing.None, initTime, finalTime, position.X+addX1-320, position.Y, position.X+addX2-320, position.Y);
                                }
                            }
                            sprite.Fade(subtitleLine.StartTime, 1);
                            sprite.Fade(subtitleLine.EndTime, 0);
                            if (additive) sprite.Additive(subtitleLine.StartTime, subtitleLine.EndTime);
                        }
                        letterX += texture.BaseWidth * FontScale;
                    }
                    letterY += lineHeight;
                }
            }
        }
    }
}
