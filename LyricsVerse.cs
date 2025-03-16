using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace StorybrewScripts{
    public class LyricsVerse : StoryboardObjectGenerator{        
        [Configurable] public string SubtitlesPath = "";
        [Configurable] public string pixel = "";
        [Configurable] public string SpritesPath = "sb/lyrics";
        [Configurable] public string TimestampSplit = "";
        [Configurable] public int LineLastTime = 26;
        [Configurable] public int Mid = 26;
        [Configurable] public int FontSize = 26;
        [Configurable] public float FontScale = 0.5f;
        [Configurable] public float PulseScale = 1.0f;
        [Configurable] public Color4 FontColor = Color4.White;
        [Configurable] public FontStyle FontStyle = FontStyle.Regular;
        [Configurable] public int OutlineThickness = 3;
        [Configurable] public Color4 OutlineColor = new Color4(50, 50, 50, 200);
        [Configurable] public int ShadowThickness = 0;
        [Configurable] public Color4 ShadowColor = new Color4(0, 0, 0, 255);
        [Configurable] public Vector2 Padding = Vector2.Zero;
        [Configurable] public float SubtitleX = 240;
        [Configurable] public float SubtitleY = 400;
        [Configurable] public bool TrimTransparency = true;
        [Configurable] public bool EffectsOnly = false;
        [Configurable] public bool Debug = false;
        [Configurable] public OsbOrigin Origin = OsbOrigin.Centre;

        Random rnd;

        public override void Generate(){

            var layer = GetLayer("Lyrics");

            rnd = new Random();

            var font = LoadFont(SpritesPath, new FontDescription(){
                FontPath = "fonts/ShipporiMincho-bold.ttf",
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
            var aniLyrics = 0;
            float lineWidth0 = 0;
            List<float> SizeLines = [];
            var index = 0;

            foreach (var letter in subtitles.Lines){
                var texture = font.GetTexture(letter.Text);
                lineWidth0 += texture.BaseWidth * FontScale/2;

                if(letter.Text.Contains('$')){
                    SizeLines.Add(lineWidth0);
                    lineWidth0 = 0;
                }
            }

            foreach (var subtitleLine in subtitles.Lines){
                foreach (var line in subtitleLine.Text.Split('\0')){
                    var letterX = Mid - SizeLines[index] * 0.5f;
                    var letterY = SubtitleY;

                    float lineWidth = 0f;
                    var lineHeight = 0f;

                    for(int x = 0; x<TimestampArray.Length; x++){
                        if(LineStart == TimestampArray[x]) continue;
                        if(TimestampArray[x] <= subtitleLine.StartTime && TimestampArray[x+1] >= subtitleLine.StartTime){
                            LineStart = TimestampArray[x];
                            LineEnd = TimestampArray[x+1];
                            RunLine = -10f;
                            aniLyrics = 0;
                            break;
                        }
                    }

                    foreach (var letter in line){
                        var texture = font.GetTexture(letter.ToString());
                        lineWidth += texture.BaseWidth * FontScale;
                        lineHeight = Math.Max(lineHeight, texture.BaseHeight * FontScale);
                        if (!texture.IsEmpty){
                            var position = new Vector2(letterX+RunLine, letterY)
                            + texture.OffsetFor(Origin) * FontScale;
                            var sprite = layer.CreateSprite(texture.Path, Origin);
                            if(letter == '$'){
                                letterX += texture.BaseWidth * FontScale;
                                index+=1;
                                continue;
                            }
                            
                            //begin
                            sprite.Fade(LineStart-200, LineStart+aniLyrics, 0, 0.5);
                            
                            //pulse
                            sprite.Fade(subtitleLine.StartTime, subtitleLine.StartTime + 100, 0.4, 1);
                            sprite.Scale(subtitleLine.StartTime, subtitleLine.StartTime + 100, FontScale-0.2f, PulseScale);
                            sprite.Color(OsbEasing.Out, subtitleLine.StartTime, subtitleLine.EndTime+tick(0, 1), Color4.DarkBlue, Color4.White);
                                       
                            int shakeInterval = 40; // Intervalo entre cada movimento em milissegundos

                            // Comece na posição central
                            Vector2 previousPosition1 = position;

                            // Loop para o período de tempo desejado
                            
                            for (double t = subtitleLine.StartTime - 100; t < subtitleLine.EndTime; t += shakeInterval)
                            {
                                // Gere deslocamentos aleatórios dentro de -1 a +1 pixel
                                float offsetX = (float)(rnd.NextDouble() * 1 - 0.5);
                                float offsetY = (float)(rnd.NextDouble() * 1 - 0.5);

                                // Calcule a nova posição aleatória
                                Vector2 newPosition = new Vector2(position.X + offsetX, position.Y + offsetY);

                                // Anime o movimento do sprite para a nova posição
                                sprite.Move(OsbEasing.InOutSine, t, t + shakeInterval, previousPosition1, newPosition);
                                // Atualize a posição anterior para o próximo loop
                                previousPosition1 = newPosition;
                            }

                            //end
                            sprite.Scale(subtitleLine.StartTime + 100, subtitleLine.EndTime+tick(0, 1), PulseScale, FontScale-0.2f);
                            sprite.Fade(subtitleLine.StartTime + 100, subtitleLine.EndTime+tick(0, 1), 1, 0);
                            RunLine += texture.BaseWidth * FontScale/2;
                        }
                    }
                }
            }
            var xInicial = 370;
            var diferenca = 320;

            var pixelLinha2 = layer.CreateSprite(pixel, OsbOrigin.CentreLeft, new Vector2((float) xInicial+10, (float) 274));

            pixelLinha2.ScaleVec(TimestampArray[0], TimestampArray[0] + 200, 0, 0.35, diferenca*0.53 , 0.35);
            pixelLinha2.Fade(TimestampArray[0], 0.7);
            pixelLinha2.MoveX(TimestampArray[^1], TimestampArray[^1]+400, xInicial, 0);
            pixelLinha2.ScaleVec(TimestampArray[^1], TimestampArray[^1]+400, diferenca*0.53, 0.35, diferenca*0.37 , 0.35);
            pixelLinha2.Fade(LineLastTime, LineLastTime+400, 0.7, 0);
            pixelLinha2.Color(TimestampArray[0], Color4.Black);

            var pixelLinha = layer.CreateSprite(pixel, OsbOrigin.CentreLeft, new Vector2((float) xInicial, (float) 273));
            pixelLinha.ScaleVec(TimestampArray[0], TimestampArray[0] + 200, 0, 0.35, diferenca*0.53 , 0.35);
            pixelLinha.Fade(TimestampArray[0], 0.7);
            pixelLinha.MoveX(TimestampArray[^1], TimestampArray[^1]+400, xInicial, 0);
            pixelLinha.ScaleVec(TimestampArray[^1], TimestampArray[^1]+400, diferenca*0.53, 0.35, diferenca*0.37 , 0.35);
            pixelLinha.Fade(LineLastTime, LineLastTime+400, 0.7, 0);
        }
        double tick(double start, double divisor){
            return Beatmap.GetTimingPointAt((int)start).BeatDuration / divisor;
        }
    }
}
