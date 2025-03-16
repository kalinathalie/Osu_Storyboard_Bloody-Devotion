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
    public class LastPreKiaiKanji : StoryboardObjectGenerator{
        [Configurable]
        public string SubtitlesPath = "lyrics.srt";
        [Configurable]
        public string FontName = "Verdana";
        [Configurable]
        public string SpritesPath = "sb/kanjiKiai";
        [Configurable]
        public string TimestampSplit = "";
        [Configurable]
        public int FontSize = 26;
        [Configurable]
        public float FontScale = 0.5f;
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
        public Color4 ShadowColor = new Color4(0, 0, 0, 100);
        [Configurable]
        public Vector2 Padding = Vector2.Zero;
        [Configurable]
        public float SubtitleX = 240;
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
        public class Time{
            public int FirstTime { get; set; }
            public int LastTime { get; set; }
            public float FirstPosition { get; set; }
            public float LastPosition { get; set; }
        }

        public override void Generate(){

            var layer = GetLayer("Kanji");

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
            var LineStart = 0;
            var LineEnd = 0;
            var RunLine = 0f;
            var RunLineY = 0f;
            var RunLineX = 0f;
            var InitTime = subtitles.Lines.First().StartTime;

            string json = File.ReadAllText("projects/Bloody Devotion/time/times.json");
            List<Time> intervalos = JsonConvert.DeserializeObject<List<Time>>(json);

            foreach (var subtitleLine in subtitles.Lines){
                var tempoInicial = subtitleLine.StartTime;
                var tempoFinal = subtitleLine.EndTime;
                float addX1 = 0f;
                float addX2 = 0f;
                var initTime = 0;
                var finalTime = 0;
                foreach (var line in subtitleLine.Text.Split('\0')){
                    var letterX = SubtitleX;
                    var letterY = SubtitleY;

                    var lineWidth = 0f;
                    var lineHeight = 0f;

                    for(int x = 0; x<TimestampArray.Length; x++){
                        if(LineStart == TimestampArray[x]) continue;
                        if(TimestampArray[x] <= subtitleLine.StartTime && TimestampArray[x+1] >= subtitleLine.StartTime){
                            LineStart = TimestampArray[x];
                            LineEnd   = TimestampArray[x+1];
                            RunLine   = 0f;
                            RunLineY  = 0f;
                            RunLineX -= 40f;
                            break;
                        }
                    }

                    foreach (var letter in line){
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                        if (!texture.IsEmpty){
                            var position = new Vector2(letterX+RunLineX, letterY+RunLineY)
                            + texture.OffsetFor(Origin) * FontScale;
                            var sprite = layer.CreateSprite(texture.Path, Origin);
                            sprite.Color(InitTime, Color4.Crimson);
                            //begin
                            sprite.Scale(InitTime, FontScale-0.2f);
                            if(InitTime>217731){
                                foreach (var intervalo in intervalos){
                                    initTime = intervalo.FirstTime;
                                    finalTime = intervalo.LastTime;
                                    addX1 = intervalo.FirstPosition;
                                    addX2 = intervalo.LastPosition;
                                    sprite.Move((initTime==217801)? OsbEasing.Out : OsbEasing.None, initTime, finalTime, position.X+addX1-900, position.Y-20, position.X+addX2-900, position.Y-20);
                                }
                            }else{
                                sprite.Move(InitTime, position);
                            }
                            
                            //pulse
                            sprite.Scale(subtitleLine.StartTime, subtitleLine.StartTime + 100, FontScale-0.2f, FontScale);
                            sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + 100, 0.6, 1);
                            sprite.Color(subtitleLine.StartTime, subtitleLine.StartTime + 100, Color4.Red, Color4.White);
                            //end
                            sprite.Scale(subtitleLine.StartTime + 100, subtitleLine.EndTime+tick(0, 1), FontScale, FontScale-0.2f);
                            sprite.Fade(subtitleLine.StartTime + 100, subtitleLine.EndTime+tick(0, 1), 1, 0.6);
                            sprite.Fade(subtitles.Lines.Last().EndTime+tick(0, 1), subtitles.Lines.Last().EndTime+tick(0, 0.5), 0.6, 0);
                        }
                        letterX += texture.BaseWidth * FontScale;
                    }
                    RunLine+=letterX-SubtitleX;
                    RunLineY+=32;
                    letterY += lineHeight;
                }
            }
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
