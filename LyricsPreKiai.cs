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
    public class LyricsPreKiai : StoryboardObjectGenerator
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
    
            generateLineRight("流れる砂よ", 53418, 62870); //58349
            generateLineRight("その息を殺せ", 55472, 62870);
            generateLineRight("歌うペンデュラム底へ", 58144, 62870); //61226
            generateLineRight("と沈め", 61226, 62870);
            generateLineLeft("影 縫い付け", 63281, 72733);
            generateLineLeft("落ちかけの奇術師", 65130, 72733);
            generateLineLeft("仮面 割れぬ間に", 68212, 72733);
            generateLineLeft("踊れ", 70267, 72733);

            yValueRight = 137;
            yValueLeft = 137;

            generateLineRight("例えその愛が", 132322, 141774); 
            generateLineRight("偽りであろうと", 134582, 141774);
            generateLineRight("例えこの身切り裂か", 137253, 141774); 
            generateLineRight("れようと", 140335, 141774);
            generateLineLeft("貴女の為刃", 142185, 151637);
            generateLineLeft("踊らせよう", 145267, 151637);
            generateLineLeft("果てる夜まで貴女", 147116, 151637);
            generateLineLeft("の傍で", 150198, 151637);

            var pasta = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\osu!\\Songs\\2037900 Akatsuki Records - Bloody Devotion\\sb\\lyrics\\kanjiprekiai\\";
            string[] arquivos = Directory.GetFiles(pasta);

            // Itera sobre cada arquivo e imprime o nome
            foreach (string arquivo in arquivos)
            {
                Bitmap originalImage = new Bitmap(arquivo);
                Bitmap skewedImage = SkewImage(originalImage, -15);

                var trocado = arquivo.Replace("kanjiprekiai", "kanjiprekiai2");
                
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
            
        static double yValueRight = 137;

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

            double posX = 475;

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

        static double yValueLeft = 140;

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

            double posX = -50;

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
