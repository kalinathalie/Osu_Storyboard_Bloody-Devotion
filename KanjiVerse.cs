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
using System.Drawing.Drawing2D;
using System.IO;

namespace StorybrewScripts
{
    public class KanjiVerse : StoryboardObjectGenerator
    {
        [Configurable]
        public string SpritesPath = "sb/lyricjp";
        [Configurable]
        public int FontSize = 26;
        [Configurable]
        public Color4 FontColor = Color4.White;
        [Configurable]
        public FontStyle FontStyle = FontStyle.Regular;
        [Configurable]
        public int ShadowThickness = 0;
        [Configurable]
        public Color4 ShadowColor = new Color4(0, 0, 0, 100);
        [Configurable]
        public Vector2 Padding = Vector2.Zero;
        [Configurable]
        public string pixel = "";
        [Configurable]
        public bool TrimTransparency = true;
        [Configurable]
        public bool EffectsOnly = false;
        StoryboardLayer layer;
        FontGenerator japFont;
        Random rnd;
        public override void Generate()
        {
            layer = GetLayer("lyrics"); 

            rnd = new Random(420);

		    japFont = LoadFont(SpritesPath, new FontDescription()
            {
                FontPath = "fonts/ShipporiMincho-bold.ttf",
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
    
            generateLineRight("シルクの闇に微睡み", 27117, 40268);
            generateLineRight("融け出した篝火", 34720, 40268);
            generateLineRight("十六夜の月", 38213, 40268);
            
            generateLineLeft("祈りも慈悲も憐れみも", 40268, 51774);
            generateLineLeft("今宵は叶わない", 47870, 51774);


            var pasta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!\\Songs\\2037900 Akatsuki Records - Bloody Devotion\\sb\\lyricjp\\";
            string[] arquivos = Directory.GetFiles(pasta);

            // Itera sobre cada arquivo e imprime o nome
            foreach (string arquivo in arquivos)
            {
                Bitmap originalImage = new Bitmap(arquivo);
                Bitmap skewedImage = SkewImage(originalImage, -15);

                var trocado = arquivo.Replace("lyricjp", "lyricjp2");
                
                skewedImage.Save(trocado);
            }
        }   
        static Bitmap SkewImage(Bitmap image, float skewX)
        {
            // Calcula o fator de skew em radianos
            float tanSkewX = (float)Math.Tan(skewX * Math.PI / 180);

            // Calcula a nova largura necessária para acomodar o skew
            int newWidth = (int)(image.Width + Math.Abs(tanSkewX * image.Height));

            // Cria um novo bitmap com a nova largura e a mesma altura
            Bitmap skewedImage = new Bitmap(newWidth, image.Height);

            using (Graphics g = Graphics.FromImage(skewedImage))
            {
                // Definir a qualidade gráfica
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                // Criar a matriz de transformação
                Matrix matrix = new Matrix();

                // Ajusta a translação para evitar cortes
                float offsetX = tanSkewX > 0 ? 0 : Math.Abs(tanSkewX * image.Height);

                // Posiciona a imagem corretamente antes de aplicar o skew
                matrix.Translate(offsetX, 0);
                matrix.Shear(tanSkewX, 0); // Skew apenas no eixo X

                // Aplicar a transformação
                g.Transform = matrix;

                // Desenhar a imagem original
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            }
            return skewedImage;
        }
            
        static double yValueRight = 180;

        public void generateLineRight(String lyric, int StartTime, int EndTime){

            double scale = 0.3;
            double scaleY = 0.5;
            double lineWidth = 0f;
            int fadeDuration = 250;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }

            double posX = 450;
            var xInicial = posX;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());

                if(!texture.IsEmpty)
                {
                    var sprite = layer.CreateSprite(texture.Path.Replace("lyricjp", "lyricjp2"), OsbOrigin.Centre, new Vector2((float) posX , (float) yValueRight));
                    sprite.ScaleVec(StartTime, scale, scaleY);
                    sprite.Fade(StartTime, StartTime + fadeDuration, 0 , 1);

                    if(rnd.Next(-50,+50)>0)
                    {
                        var yoffset = rnd.Next(7,17);

                        sprite.MoveY(OsbEasing.OutCirc, StartTime, StartTime + 1233, yValueRight - yoffset, yValueRight);
                    }
                    else
                    {
                        var yoffset = rnd.Next(7,17);
                        var yoffsetFinal = rnd.Next(0,3);

                        sprite.MoveY(OsbEasing.OutExpo, StartTime, StartTime + 1233, yValueRight + yoffset, yValueRight);
                    }

                    sprite.Fade(EndTime - fadeDuration, EndTime, 1, 0);
                }
                posX += texture.BaseWidth * scale;
            }
            var diferenca = posX - xInicial;

            var pixelLinha = layer.CreateSprite(pixel, OsbOrigin.CentreLeft, new Vector2((float) xInicial - 10, (float) yValueRight + 20));
            pixelLinha.ScaleVec(StartTime, StartTime + 200, 0, 0.35, diferenca*0.5 , 0.35);
            pixelLinha.Fade(StartTime, 0.4);
            pixelLinha.Fade(EndTime, 0);

            yValueRight += 60;
        }

        static double yValueLeft = 200;

        public void generateLineLeft(String lyric, int StartTime, int EndTime){

            double scale = 0.3;
            double scaleY = 0.4;
            double lineWidth = 0f;
            int fadeDuration = 250;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * scale;
            }

            double posX = -20;

            var xInicial = posX;

            foreach (var letter in lyric)
            {
                var texture = japFont.GetTexture(letter.ToString());

                if(!texture.IsEmpty)
                {
                    var sprite = layer.CreateSprite(texture.Path.Replace("lyricjp", "lyricjp2"), OsbOrigin.Centre, new Vector2((float) posX , (float) yValueLeft));
                    sprite.ScaleVec(StartTime, scale, scaleY);
                    sprite.Fade(StartTime, StartTime + fadeDuration, 0 , 1);

                    if(rnd.Next(-50,+50)>0)
                    {
                        var yoffset = rnd.Next(7,17);

                        sprite.MoveY(OsbEasing.OutCirc, StartTime, StartTime + 1233, yValueLeft - yoffset, yValueLeft);
                    }
                    else
                    {
                        var yoffset = rnd.Next(7,17);
                        var yoffsetFinal = rnd.Next(0,3);

                        sprite.MoveY(OsbEasing.OutExpo, StartTime, StartTime + 1233, yValueLeft + yoffset, yValueLeft);
                    }

                    sprite.Fade(EndTime - fadeDuration, EndTime, 1, 0);
                }
                posX += texture.BaseWidth * scale;
            }
            var diferenca = posX - xInicial;

            var pixelLinha = layer.CreateSprite(pixel, OsbOrigin.CentreLeft, new Vector2((float) xInicial - 10, (float) yValueLeft + 20));
            pixelLinha.ScaleVec(StartTime, StartTime + 200, 0, 0.35, diferenca*0.5 , 0.35);
            pixelLinha.Fade(StartTime, 0.4);
            pixelLinha.Fade(EndTime, 0);

            yValueLeft += 60;
        }
    }
}
