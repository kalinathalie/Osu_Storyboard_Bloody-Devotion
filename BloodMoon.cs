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
using System.Linq;

namespace StorybrewScripts
{
    public class BloodMoon : StoryboardObjectGenerator
    {
        [Configurable] public string Background = "";
        [Configurable] public string Black = "";
        [Configurable] public string BatAnimation = "";
        [Configurable] public string Pixel = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        public override void Generate()
        {
            var layer = GetLayer("BloodMoon");
		    var bg = layer.CreateSprite(Background, OsbOrigin.Centre);

            bg.Scale(StartTime, 0.55);
            bg.Fade(StartTime, StartTime+1000, 0, 0.7);
            bg.Fade(EndTime, 0);
            bg.MoveX(StartTime, 198075, 455, 184);
            bg.MoveY(StartTime, 280);
            bg.Color(198075, 201363, Color4.White, Color4.Crimson);
            bg.MoveX(198075, 203006, 184, 100);
            bg.Scale(198075, 203006, 0.55, 0.7);
            bg.MoveY(OsbEasing.OutBounce, 203006, 203417, 300, 280);
            bg.MoveY(OsbEasing.OutBounce, 203417, 203828, 260, 280);
            bg.MoveY(OsbEasing.OutBounce, 203828, 204239, 300, 280);
            bg.MoveY(OsbEasing.OutBounce, 204239, EndTime, 260, 280);

            Random randomBat = new Random();
            for(int i=0; i<20; i+=1){
                var bat = layer.CreateAnimation(BatAnimation, 8, 65, OsbLoopType.LoopForever, OsbOrigin.Centre);
                // Posição inicial aleatória no ambiente
                float startX = (float)randomBat.NextDouble() * 640; // Largura da tela
                float startY = (float)randomBat.NextDouble() * 480; // Altura da tela

                // Posição final aleatória próxima ao centro (320, 240)
                float endX = 320 + (float)(randomBat.NextDouble() - 0.5f) * 50; // +/-50 em torno do centro X
                float endY = 135 + (float)(randomBat.NextDouble() - 0.2f) * 10; // +/-50 em torno do centro Y

                // Definição do tempo de animação
                int startTime = 198075;
                int endTime = EndTime; // Duração de 3000ms (3 segundos)

                // Número de passos para criar a interpolação suave
                int steps = 60;

                // Lista para armazenar os tempos
                List<int> times = new List<int>();
                for (int j = 0; j <= steps; j++)
                {
                    times.Add(startTime + ((endTime - startTime) * j) / steps);
                }

                // Lista para armazenar as posições calculadas
                List<Vector2> positions = new List<Vector2>();
                float amplitude = 40; // Amplitude da onda
                float cycles = 3;     // Número de ciclos completos da onda

                // Fase inicial aleatória entre 0 e 2π
                float phase = (float)(randomBat.NextDouble() * 2 * Math.PI);

                for (int j = 0; j <= steps; j++)
                {
                    float progress = (float)j / steps;

                    // Interpolação linear para X
                    float x = startX + (endX - startX) * progress;

                    // Movimento harmônico simples para Y com fase inicial aleatória
                    float y = startY + (endY - startY) * progress + amplitude * (float)Math.Sin(2 * Math.PI * cycles * progress + phase);

                    positions.Add(new Vector2(x, y));
                }

                // Aplicação das posições calculadas ao sprite
                for (int j = 0; j < steps; j++)
                {
                    bat.Move(times[j], times[j + 1], positions[j], positions[j + 1]);
                }
                bat.Fade(198075, 198897, 0, 1);
                bat.Fade(EndTime-408, EndTime, 1, 0.5);
                bat.Scale(198075, 1);
            }

            var random = new Random();

            foreach (OsuHitObject hitobject in Beatmap.HitObjects){
                if ((StartTime != 0 || EndTime != 0) && 
                    (hitobject.StartTime < StartTime - 5 || EndTime - 5 <= hitobject.StartTime))
                    continue;
                var line = layer.CreateSprite(Pixel, OsbOrigin.Centre, hitobject.Position);
                line.ScaleVec(hitobject.StartTime, hitobject.StartTime+800, 0.4, 480, 0, 480);
                var flip = random.Next(0, 2);
                var angle = flip%2==0 ? random.Next(-10, -4) : random.Next(4, 10);
                line.Rotate(hitobject.StartTime, MathHelper.DegreesToRadians(angle));
                if(hitobject.StartTime>=198075){
                    line.Color(hitobject.StartTime, Color4.Red);
                }
                line.Move(OsbEasing.OutCirc, hitobject.StartTime, hitobject.StartTime+800, hitobject.Position, hitobject.Position+ new Vector2(random.Next(-20,20), 0));

            }

            var flash = layer.CreateSprite(Black, OsbOrigin.Centre);

            flash.Color(203006, Color4.Crimson);
            flash.Fade(OsbEasing.OutCirc, 203006, 203417, 0.4, 0.1);
            flash.Fade(OsbEasing.OutCirc, 203417, 203828, 0.4, 0.1);
            flash.Fade(OsbEasing.OutCirc, 203828, 204239, 0.4, 0.1);
            flash.Fade(OsbEasing.InCirc, 204239, 204650, 0.1, 1);

            var black1 = layer.CreateSprite(Black, OsbOrigin.TopCentre, new Vector2(320, 0));
            var black2 = layer.CreateSprite(Black, OsbOrigin.BottomCentre, new Vector2(320, 479));

            black1.Color(198075, Color4.Black);
            black1.Fade(198075, 1);
            black1.Fade(EndTime, 0);
            black1.ScaleVec(198075, 201363, 0.7, 0, 0.7, 0.08);

            black2.Color(198075, Color4.Black);
            black2.Fade(198075, 1);
            black2.Fade(EndTime, 0);
            black2.ScaleVec(198075, 201363, 0.7, 0, 0.7, 0.08);
        }
    }
}
