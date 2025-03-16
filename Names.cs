using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Subtitles;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace StorybrewScripts
{
    public class Names : StoryboardObjectGenerator
    {
       [Configurable] public string Pixel = "";
        public override void Generate()
        {
            var layer = GetLayer("Intro");
		    // Setting the font
            var font = LoadFont("sb/lyrics/intro", new FontDescription(){
                FontPath = "fonts/ShipporiMincho-Bold.ttf",
                FontSize = 72,
                Color = Color4.White,
                Padding = Vector2.Zero,
                //FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontOutline(){
                Thickness = 0,
                //Color = Color.Transparent,
            },
            new FontShadow(){
                Thickness = 0,
               // Color = Color.Transparent,
            });

            var font2 = LoadFont("sb/lyrics/intro2", new FontDescription(){
                FontPath = "fonts/microsoft-himalaya.ttf",
                FontSize = 90,
                Color = Color4.White,
                Padding = Vector2.Zero,
                //FontStyle = FontStyle.Regular,
                TrimTransparency = true,
                EffectsOnly = false,
                Debug = false,
            },
            new FontOutline(){
                Thickness = 0,
                //Color = Color.Transparent,
            },
            new FontShadow(){
                Thickness = 0,
               // Color = Color.Transparent,
            });


            var mapper = font.GetTexture("K4L1");
            var mapperSprite = layer.CreateSprite(mapper.Path);
            mapperSprite.Move(10473, 320, 98);
            mapperSprite.Scale(10473,0.2);
            mapperSprite.Fade(OsbEasing.InOutCirc, 10473-80,10473,0,1);
            mapperSprite.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);

            var album = font.GetTexture("暁Records・DOWN DOWN DOLL -to the beginning 07-");
            var albumSprite = layer.CreateSprite(album.Path);
            albumSprite.Move(10473, 320, 380);
            albumSprite.Scale(10473,0.2);
            albumSprite.Fade(OsbEasing.InOutCirc, 10473-80,10473,0,1);
            albumSprite.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);
            
            var songname = font2.GetTexture("BLOODY DEVOTION");
            var songSprite = layer.CreateSprite(songname.Path);
            songSprite.Move(10473, 320, 240);
            songSprite.ScaleVec(10473, 1, 1.6);
            songSprite.ScaleVec(OsbEasing.OutExpo, 13761,13863, 1, 1.6, 1.3, 2.08);
            songSprite.Fade(OsbEasing.InOutCirc, 10473-80,10473,0,1);
            songSprite.Fade(OsbEasing.OutExpo, 13761,13863,1,0);
            songSprite.Color(10473, Color4.Red);
            
            var pixel1 = layer.CreateSprite(Pixel, OsbOrigin.Centre);
            var pixel2 = layer.CreateSprite(Pixel, OsbOrigin.Centre);
            var pixel3 = layer.CreateSprite(Pixel, OsbOrigin.Centre);
            var pixel4 = layer.CreateSprite(Pixel, OsbOrigin.Centre);
            var pixel5 = layer.CreateSprite(Pixel, OsbOrigin.Centre);
            var pixel6 = layer.CreateSprite(Pixel, OsbOrigin.Centre);

            // Define a posição central do quadrado
            float centerX = 320; // X central
            float centerY = 240; // Y central
            float size = 294; // Tamanho do quadrado

            // Linhas do quadrado
            // Superior
            pixel1.Move((OsbEasing) 4, 7391, 10473, (centerX - size / 2), centerY - size / 2, (centerX - size / 2)-70, centerY - size / 2);
            pixel1.Move(7391, (centerX - size / 2)-70, centerY - size / 2);
            pixel1.Fade(7391, 1);
            pixel1.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);
            pixel1.ScaleVec(7391, 180, 0.5);

            // Inferior
            pixel2.Move((OsbEasing) 4, 7391, 10473, (centerX - size / 2), centerY + size / 2, (centerX - size / 2)-190, centerY + size / 2);
            pixel2.Move(7391, (centerX - size / 2)-190, centerY + size / 2);
            pixel2.Fade(7391, 1);
            pixel2.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);
            pixel2.ScaleVec(7391, 70, 0.5);

            // Lateral esquerda
            pixel3.Move((OsbEasing) 4, 7391, 10473, (centerX + size / 2), centerY - size / 2, (centerX + size / 2)+70, centerY - size / 2);
            pixel3.Move(7391, (centerX + size / 2)+70, centerY - size / 2);
            pixel3.Fade(7391, 1);
            pixel3.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);
            pixel3.ScaleVec(7391, 180, 0.5);

            // Lateral direita
            pixel4.Move((OsbEasing) 4, 7391, 10473, (centerX + size / 2), centerY + size / 2, (centerX + size / 2)+190, centerY + size / 2);
            pixel4.Move(7391, (centerX + size / 2)+190, centerY + size / 2);
            pixel4.Fade(7391, 1);
            pixel4.Fade(OsbEasing.OutExpo, 13555,13555+100,1,0);
            pixel4.ScaleVec(7391, 70, 0.5);

            pixel5.Move(7391 , -90, 240);
            pixel5.Fade(7391, 1);
            pixel5.Fade(OsbEasing.OutExpo, 13658,13658+100,1,0);
            pixel5.ScaleVec((OsbEasing) 4, 7391, 10473, 0.5, 0, 0.5, 140);

            pixel6.Move(7391 , 730, 240);
            pixel6.Fade(7391, 1);
            pixel6.Fade(OsbEasing.OutExpo, 13658,13658+100,1,0);
            pixel6.ScaleVec((OsbEasing) 4, 7391, 10473, 0.5, 0, 0.5, 140);
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
                
                // Calcula o deslocamento baseado na direção do skew
                float offset;
                if (skewX > 0)
                {
                    offset = 0; // Se o skew é positivo, mantém a imagem à esquerda
                }
                else
                {
                    offset = Math.Abs(tanSkewX * image.Height); // Se o skew é negativo, move para a direita
                }
                
                // Aplica a translação com o offset calculado
                matrix.Translate(offset, 0);
                
                // Aplica o skew
                matrix.Shear(tanSkewX, 0);
                
                // Aplicar a transformação
                g.Transform = matrix;
                
                // Desenhar a imagem original
                g.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height));
            }
            
            return skewedImage;
        }
    }
}
