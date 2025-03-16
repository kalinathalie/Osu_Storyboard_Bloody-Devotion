using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StorybrewScripts{
    public class KanjiKiai : StoryboardObjectGenerator{
        [Configurable]
        public string SubtitlesPath = "";
        [Configurable]
        public string SpritesPath = "sb/lyrics";
        [Configurable]
        public string TimestampSplit = "";
        [Configurable]
        public int FontSize = 26;
        [Configurable]
        public float FontScale = 0.5f;
        [Configurable] 
        public float PulseScale = 1.0f;
        [Configurable]
        public Color4 FontColor = Color4.White;
        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;
        [Configurable]
        public int OutlineThickness = 3;
        [Configurable]
        public Color4 OutlineColor = new Color4(50, 50, 50, 200);
        [Configurable]
        public int ShadowThickness = 0;
        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 255);
        [Configurable]
        public Vector2 Padding = Vector2.Zero;
        [Configurable]
        public float SubtitleY = 400;
        [Configurable]
        public bool TrimTransparency = true;
        [Configurable]
        public bool EffectsOnly = false;
        [Configurable]
        public bool Debug = false;
        [Configurable]
        public OsbOrigin Origin = OsbOrigin.Centre;
        Random rnd;
        public class Time{
            public int FirstTime { get; set; }
            public int LastTime { get; set; }
            public float FirstPosition { get; set; }
            public float LastPosition { get; set; }
        }
        public override void Generate(){

            var layer = GetLayer("KiaiBackground");

            rnd = new Random();

            var font = LoadFont(SpritesPath, new FontDescription(){
                FontPath = "fonts/DotGothic16-Regular.ttf",
                FontSize = FontSize,
                Color = FontColor,
                Padding = Padding,
                FontStyle = FontStyle,
                TrimTransparency = TrimTransparency,
                EffectsOnly = EffectsOnly,
                Debug = Debug,
            },
            new FontOutline(){
                Thickness = OutlineThickness,
                Color = OutlineColor,
            },
            new FontShadow(){
                Thickness = ShadowThickness,
                Color = ShadowColor,
            });

            var subtitles = LoadSubtitles(SubtitlesPath);
            generatePerCharacter(font, subtitles, layer, TimestampSplit);
        }

        public void generatePerCharacter(FontGenerator font, SubtitleSet subtitles, StoryboardLayer layer, string TimestampSplit){
            
            var TimestampArray = Array.ConvertAll(TimestampSplit.Split(','), s => int.Parse(s));
            var InitTime = subtitles.Lines.First().StartTime;
            List<int> EndLines = [];
            List<int> InitLines = [];
            List<float> SizeLines = [];
            int index = 0;
            bool nextline = true;
            float lineWidth = 0f;

            foreach (var letter in subtitles.Lines){
                var texture = font.GetTexture(letter.Text);
                lineWidth += texture.BaseWidth * FontScale/2;

                if(nextline){
                    InitLines.Add((int)letter.StartTime);
                    nextline = false;
                }
                if(letter.Text.Contains('$')){
                    EndLines.Add((int)letter.EndTime);
                    nextline = true;
                    SizeLines.Add(lineWidth);
                    lineWidth = 0;
                }
            }
            
            float runLine = 0;

            foreach (var subtitleLine in subtitles.Lines){
                var letterX = 290 - SizeLines[index] * 0.5f;

                var letterY = SubtitleY;
                var lineHeight = 0f;
                                
                foreach (var letter in subtitleLine.Text){
                    var texture = font.GetTexture(letter.ToString());
                    lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale/2);
                    if (!texture.IsEmpty){
                        var position = new Vector2(letterX+runLine, letterY)
                            + texture.OffsetFor(Origin) * FontScale/2;
                        var sprite = layer.CreateSprite(texture.Path, Origin);
                        if(letter == '$'){
                            InitTime = InitLines[index];
                            index+=1;
                            runLine = 0;
                            continue;
                        }
                        if(letter == '|'){
                            runLine += texture.BaseWidth * FontScale/2;
                            continue;
                        }

                        //begin
                        sprite.Fade(InitLines[index]-100, InitLines[index], 0, 0.5);
                        
                        //pulse
                        sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + 100, 0.5, 1);
                        sprite.Scale(subtitleLine.StartTime, subtitleLine.StartTime + 100, FontScale-0.2f, PulseScale);
                        sprite.Color(OsbEasing.In, subtitleLine.StartTime, subtitleLine.EndTime+tick(0, 1), Color4.Crimson, Color4.White);

                        sprite.Move(InitLines[index], EndLines[index] + tick(0,1), position + new Vector2(20, 0), position - new Vector2(20, 0));


                        //end
                        sprite.Scale(subtitleLine.StartTime + 100, subtitleLine.EndTime+tick(0, 1), PulseScale, FontScale-0.2f);
                        sprite.Fade(EndLines[index], EndLines[index] + tick(0,1), 1, 0);
                        runLine += texture.BaseWidth * FontScale/2;
                        runLine += 4;
                    }
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
