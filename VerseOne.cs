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
    public class VerseOne : StoryboardObjectGenerator
    {
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        [Configurable] public string Sakuya1WhiteImage = "";
        [Configurable] public string Sakuya1Image = "";
        [Configurable] public string Sakuya2WhiteImage = "";
        [Configurable] public string Sakuya2Image = "";
        [Configurable] public string Flash = "";
        [Configurable] public string Pixel = "";
        [Configurable] public string Vig = "";
        [Configurable] public string KnifeImage = "";
        [Configurable] public string BigClock = "";
        [Configurable] public string BigHand = "";
        [Configurable] public string SmallHand = "";
        [Configurable] public string MidCircle = "";
        public override void Generate()
        {
		    // Crie o sprite na posição central desejada
            var layer = GetLayer("Main");

            var background = GetLayer("BGVerse").CreateSprite(Flash, OsbOrigin.Centre);

            int midTime = (StartTime + EndTime) /2;

            background.Color(StartTime, Color4.Crimson);
            background.Fade(StartTime, 0.5);
            background.Fade(EndTime, 0);

            int x1 = -200;
            int x2 = 840;
            Random randomKnife = new Random();
            for(int i = StartTime-102; i<=midTime;i+=51){
                var knife = GetLayer("Main").CreateSprite(KnifeImage, OsbOrigin.CentreLeft);
                var line =  GetLayer("Main").CreateSprite(Pixel, OsbOrigin.CentreRight);
                
                int y1 = randomKnife.Next(0, 481); // y1 aleatório entre 0 e 480
                int y2 = randomKnife.Next(0, 481); // y2 aleatório entre 0 e 480
                int vel = randomKnife.Next(0, 1632);
                
                float randomScale = (float)(randomKnife.NextDouble() * (0.3 - 0.1) + 0.1);
                float randomFade = (float)(randomKnife.NextDouble() * (1 - 0.2) + 0.2);
                // Calcular diferenças
                double deltaY = y2 - y1;
                double deltaX = x2 - x1; // 747 - (-107) = 854

                // Calcular o ângulo em radianos
                double angleInRadians = Math.Atan2(deltaY, deltaX);

                knife.Fade(i, randomFade);
                knife.Move(i, i+408+vel, x1, y1, x2, y2);
                if(i+408+vel > midTime){
                    knife.Fade(midTime, midTime+400, randomFade, 0);
                }else{
                    knife.Fade(i+408+vel, 0);
                }
                
                knife.Scale(i, randomScale);
                knife.Color(i, Color4.Black);
                knife.Rotate(i, angleInRadians);

                line.Color(i, Color4.Black);
                line.Rotate(i, angleInRadians + MathHelper.DegreesToRadians(0));
                line.Fade(i, randomFade);
                line.ScaleVec(i, i+408+vel, 0, 8*randomScale, 200/randomScale, 8*randomScale);
                line.Move(i, i+408+vel, x1, y1, x2, y2);
                if(i+408+vel > midTime){
                    line.Fade(midTime, midTime+400, randomFade, 0);
                }else{
                    line.Fade(OsbEasing.OutCirc, i+408+vel, i+408+vel+812, randomFade, 0);
                }
                line.ScaleVec(OsbEasing.OutCirc, i+408+vel, i+408+vel+812, 200/randomScale, 8*randomScale, 200/randomScale, 0);
            }

            var fadeClock = 0.4;

            var clockSprite = GetLayer("Main").CreateSprite(BigClock, OsbOrigin.Centre, new Vector2(120, 240));

            clockSprite.Scale(OsbEasing.OutExpo, midTime, midTime+204, 0.2*1.1, 0.14*1.1);
            clockSprite.Fade(OsbEasing.OutExpo, midTime, midTime+204, 0, fadeClock);
            clockSprite.Fade(EndTime, 0);
            clockSprite.Color(midTime, Color4.Black);

            var SmallHandSprite = GetLayer("Main").CreateSprite(SmallHand, OsbOrigin.BottomCentre, new Vector2(120, 240));
            var BigHandSprite = GetLayer("Main").CreateSprite(BigHand, OsbOrigin.BottomCentre, new Vector2(120, 240));

            SmallHandSprite.Scale(OsbEasing.OutExpo, midTime, midTime+204, 0.4*1.1, 0.28*1.1);
            SmallHandSprite.Fade(OsbEasing.OutExpo, midTime, midTime+204, 0, fadeClock);
            SmallHandSprite.Fade(EndTime, 0);
            SmallHandSprite.Color(midTime, Color4.Black);
            
            BigHandSprite.Scale(OsbEasing.OutExpo, midTime, midTime+204, 0.4*1.1, 0.28*1.1);
            BigHandSprite.Fade(OsbEasing.OutExpo, midTime, midTime+204, 0, fadeClock);
            BigHandSprite.Fade(EndTime, 0);
            BigHandSprite.Color(midTime, Color4.Gray);

            int[] tempos = new int[] { 13966, 14172, 14377, 14583, 14788, 14994, 15199, 15302, 15405, 15610, 15815, 16021, 16226, 16432, 16637, 16843, 17048, 17459, 17665, 17870, 18076, 18281, 18487, 18589, 18692, 18898, 19103, 19309, 19514, 19720, 19925, 20131, 20233, 20336, 20439, 20542};
            for (int i = 0; i < tempos.Length-1; i++){
                BigHandSprite.Rotate(OsbEasing.OutExpo, midTime + tempos[i]-13966, midTime + tempos[i+1]-13966, MathHelper.DegreesToRadians(i*30), MathHelper.DegreesToRadians(i*30+30));
            }

            for (int t = 0; t < tempos.Length-1; t++){
                BigHandSprite.Rotate(OsbEasing.OutExpo, midTime + tempos[t]-13966+6575+1, midTime + tempos[t+1]-13966+6575, MathHelper.DegreesToRadians(t*30 -30), MathHelper.DegreesToRadians(t*30));
            }

            var dotSprite = GetLayer("Main").CreateSprite(MidCircle, OsbOrigin.Centre, new Vector2(120, 240));

            dotSprite.Scale(OsbEasing.OutExpo, midTime, midTime+204, 0.6, 0.42);
            dotSprite.Fade(OsbEasing.OutExpo, midTime, midTime+204, 0, fadeClock);
            dotSprite.Fade(EndTime, 0);
            dotSprite.Color(midTime, Color4.Gray);

            dotSprite.Scale(OsbEasing.InExpo, EndTime-408, EndTime, 0.42*1.1, 0.84*1.1);
            BigHandSprite.Scale(OsbEasing.InExpo, EndTime-408, EndTime, 0.28*1.1, 0.84*1.1);
            SmallHandSprite.Scale(OsbEasing.InExpo, EndTime-408, EndTime, 0.28*1.1, 0.84*1.1);
            clockSprite.Scale(OsbEasing.InExpo, EndTime-408, EndTime, 0.14*1.1, 0.42*1.1);

            List<int> numeros = new List<int>();
            
            for(int i = midTime+204; i < EndTime; i+=411){
                numeros.Add(i);
            }

            numeros.AddRange(new List<int> { midTime+5650, midTime+5650+46535-45918, midTime+5650+46740-45918, midTime+5650+52494-45918, midTime+5650+52905-45918, midTime+5650+53007-45918, midTime+5650+53110-45918, midTime+5650+53316-45918, midTime+5650+53418-45918 });
            numeros.Sort();

            int j = 0;
            
            for (int i = 0; i < numeros.Count-1; i++)
            {
                SmallHandSprite.Rotate(OsbEasing.OutExpo, numeros[i], numeros[i+1], MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                j+=30;                
            }

            Random random = new Random();
            
            var initY = 460;
            var SteamBackground = GetLayer("SteamBackground");
            for(int s = 0; s<16;s++){
                for(int k = -80;k<=730;k+=100){
                    var RunPositionY = initY;
                    var steam = SteamBackground.CreateSprite("sb/steam/g"+random.Next(1,7)+".png", OsbOrigin.Centre);
                    var decrease = random.Next(30,40);
                    float randomFade = (float)(randomKnife.NextDouble() * 0.2);
                    steam.Fade(midTime, 0);
                    steam.Fade(numeros[0], randomFade);
                    steam.Color(midTime, Color4.Black);
                    steam.Fade(EndTime, 0);
                    steam.MoveX(numeros[0], k+random.Next(-50,50));
                    steam.Scale(numeros[0], 0.1);
                    j = 0;
                    for (int i = 0; i < numeros.Count-1; i++){

                        if(RunPositionY <=0){
                            steam.Fade(numeros[i], 0);
                            break;    
                        }
                        if(RunPositionY >= 580){
                            RunPositionY-=decrease;
                            continue;
                        }
                        steam.MoveY(OsbEasing.OutCirc, numeros[i], numeros[i+1], RunPositionY, RunPositionY-decrease);
                        steam.Rotate(OsbEasing.OutCirc, numeros[i], numeros[i+1], MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                        RunPositionY-=decrease;
                        j+=30;
                    }
                }
                initY+=60;
            }
            SteamBackground.RotationDegrees = 12;

            var Sakuya1Shadow = layer.CreateSprite(Sakuya1WhiteImage, OsbOrigin.Centre);
            var Sakuya1 = layer.CreateSprite(Sakuya1Image, OsbOrigin.Centre);
            Sakuya1.Scale(StartTime, 0.5);

            var Sakuya2Shadow = layer.CreateSprite(Sakuya2WhiteImage, OsbOrigin.Centre);
            var Sakuya2 = layer.CreateSprite(Sakuya2Image, OsbOrigin.Centre);
            Sakuya2.Scale(midTime, 0.5);
            Vector2 centerPosition1 = new Vector2(220, 280); // Substitua pelas coordenadas do centro desejado
            Vector2 centerPosition2 = new Vector2(480, 280); // Substitua pelas coordenadas do centro desejado
            if(StartTime==106020){
                centerPosition1 = new Vector2(130, 280); // Substitua pelas coordenadas do centro desejado
                centerPosition2 = new Vector2(630, 300); // Substitua pelas coordenadas do centro desejado
            }

            Sakuya1Shadow.Color(StartTime, Color4.Black);
            Sakuya2Shadow.Color(midTime, Color4.Black);
            // Defina o intervalo de tempo para o efeito
            int shakeInterval = 411; // Intervalo entre cada movimento em milissegundos

            // Comece na posição central
            Vector2 previousPosition1 = centerPosition1;

            // Loop para o período de tempo desejado
            Vector2 shadowOffset = new Vector2(15, 15);
            for (int t = StartTime; t < midTime-510; t += shakeInterval)
            {
                // Gere deslocamentos aleatórios dentro de -20 a +20 pixels
                float offsetX = (float)(random.NextDouble() * 10 - 5);
                float offsetY = (float)(random.NextDouble() * 10 - 5);

                // Calcule a nova posição aleatória
                Vector2 newPosition = new Vector2(centerPosition1.X + offsetX, centerPosition1.Y + offsetY);

                // Anime o movimento do sprite para a nova posição
                Sakuya1.Move(OsbEasing.InOutSine, t, t + shakeInterval, previousPosition1, newPosition);
                Sakuya1Shadow.Move(OsbEasing.InOutSine, t, t + shakeInterval, previousPosition1+shadowOffset, newPosition+shadowOffset);

                if(t + shakeInterval > midTime-510){
                    Sakuya1.Move(OsbEasing.InCirc, t + shakeInterval, midTime, newPosition, newPosition - new Vector2(800, 0));
                    Sakuya1Shadow.Move(OsbEasing.InCirc, t + shakeInterval, midTime, newPosition+shadowOffset, newPosition+shadowOffset - new Vector2(800, 0));
                }
                // Atualize a posição anterior para o próximo loop
                previousPosition1 = newPosition;
            }

            
            Sakuya2.Move(OsbEasing.OutCirc, midTime, midTime+204, centerPosition2.X+400, centerPosition2.Y, centerPosition2.X, centerPosition2.Y);
            Sakuya2Shadow.Move(OsbEasing.OutCirc, midTime, midTime+204, centerPosition2.X+400+shadowOffset.X, centerPosition2.Y+shadowOffset.X, centerPosition2.X+shadowOffset.X, centerPosition2.Y+shadowOffset.X);
            Sakuya2.Fade(midTime, 1);
            Sakuya2.Fade(EndTime, 0);

            Sakuya2Shadow.Fade(midTime, 1);
            Sakuya2Shadow.Fade(EndTime, 0);
            Vector2 previousPosition2 = centerPosition2;

            for (int t = midTime+shakeInterval; t + shakeInterval < EndTime-(shakeInterval*4); t += shakeInterval)
            {
                // Gere deslocamentos aleatórios dentro de -20 a +20 pixels
                float offsetX = (float)(random.NextDouble() * 10 - 5);
                float offsetY = (float)(random.NextDouble() * 10 - 5);

                // Calcule a nova posição aleatória
                Vector2 newPosition = new Vector2(centerPosition2.X + offsetX, centerPosition2.Y + offsetY);

                // Anime o movimento do sprite para a nova posição
                Sakuya2.Move(OsbEasing.InOutSine, t, t + shakeInterval, previousPosition2, newPosition);
                Sakuya2Shadow.Move(OsbEasing.InOutSine, t, t + shakeInterval, previousPosition2+shadowOffset, newPosition+shadowOffset);

                // Atualize a posição anterior para o próximo loop
                previousPosition2 = newPosition;

                if(t+(shakeInterval*2) >= EndTime-412){
                    Sakuya2.Move(OsbEasing.InCirc, t + shakeInterval, EndTime, newPosition, newPosition + new Vector2(400, 0));
                    Sakuya2Shadow.Move(OsbEasing.InCirc,t + shakeInterval, EndTime, newPosition + shadowOffset, newPosition + shadowOffset + new Vector2(400, 0));
                }
            }

            
            
            Sakuya2.Scale(OsbEasing.OutCirc, EndTime-411*4, EndTime-411*3, 0.5, 0.6);
            Sakuya2.Scale(OsbEasing.OutCirc, EndTime-411*3, EndTime-411*2, 0.6, 0.7);
            Sakuya2.Scale(OsbEasing.OutCirc, EndTime-411*2, EndTime-615, 0.7, 0.8);

            Sakuya2.Move(OsbEasing.OutCirc, EndTime-411*4, EndTime-411*3, previousPosition2, previousPosition2-new Vector2(0, -60));
            Sakuya2.Move(OsbEasing.OutCirc, EndTime-411*3, EndTime-411*2, previousPosition2-new Vector2(0, -60), previousPosition2-new Vector2(0, -120));
            Sakuya2.Move(OsbEasing.OutCirc, EndTime-411*2, EndTime-615, previousPosition2-new Vector2(0, -120), previousPosition2-new Vector2(0, -180));
            
            Sakuya2.Move(OsbEasing.InCirc, EndTime-615, EndTime, previousPosition2-new Vector2(0, -180),  previousPosition2-new Vector2(-400, -180));

            Sakuya2Shadow.Scale(OsbEasing.OutCirc, EndTime-411*4, EndTime-411*3, 1, 1.2);
            Sakuya2Shadow.Scale(OsbEasing.OutCirc, EndTime-411*3, EndTime-411*2, 1.2, 1.4);
            Sakuya2Shadow.Scale(OsbEasing.OutCirc, EndTime-411*2, EndTime-615, 1.4, 1.6);

            Sakuya2Shadow.Move(OsbEasing.OutCirc, EndTime-411*4, EndTime-411*3, previousPosition2 + shadowOffset, previousPosition2-new Vector2(0, -60) + shadowOffset);
            Sakuya2Shadow.Move(OsbEasing.OutCirc, EndTime-411*3, EndTime-411*2, previousPosition2-new Vector2(0, -60) + shadowOffset, previousPosition2-new Vector2(0, -120) + shadowOffset);
            Sakuya2Shadow.Move(OsbEasing.OutCirc, EndTime-411*2, EndTime-615, previousPosition2-new Vector2(0, -120) + shadowOffset, previousPosition2-new Vector2(0, -180) + shadowOffset);

            Sakuya2Shadow.Move(OsbEasing.InCirc, EndTime-615, EndTime, previousPosition2-new Vector2(0, -180) + shadowOffset,  previousPosition2-new Vector2(-400, -180) + shadowOffset);
            

            int previousAngle = 0;
            Random randInt = new Random();
            for (int t = StartTime; t < midTime-612; t += shakeInterval*3)
            {
                int randAngle = randInt.Next(10);
                randAngle-=5;
                Sakuya1.Rotate(OsbEasing.InOutSine, t, t+shakeInterval*3, MathHelper.DegreesToRadians(previousAngle), MathHelper.DegreesToRadians(randAngle));
                Sakuya1Shadow.Rotate(OsbEasing.InOutSine, t, t+shakeInterval*3, MathHelper.DegreesToRadians(previousAngle), MathHelper.DegreesToRadians(randAngle));
                previousAngle = randAngle;
            }

            for (int t = midTime+204; t < EndTime-413; t += shakeInterval*3)
            {
                int randAngle = randInt.Next(10);
                randAngle-=5;
                Sakuya2.Rotate(OsbEasing.InOutSine, t, t+shakeInterval*3, MathHelper.DegreesToRadians(previousAngle), MathHelper.DegreesToRadians(randAngle));
                Sakuya2Shadow.Rotate(OsbEasing.InOutSine, t, t+shakeInterval*3, MathHelper.DegreesToRadians(previousAngle), MathHelper.DegreesToRadians(randAngle));
                previousAngle = randAngle;
            }

            var black1 = GetLayer("").CreateSprite(Flash, OsbOrigin.BottomCentre);
            var black2 = GetLayer("").CreateSprite(Flash, OsbOrigin.TopCentre);

            black1.MoveY(OsbEasing.OutCirc, StartTime, StartTime+204, 240, 70);
            black1.Color(EndTime, Color4.Black);
            black1.Fade(StartTime, 1);
            black1.Fade(EndTime, 0);
            black1.Scale(StartTime, 0.8);
            black1.MoveY(OsbEasing.InCubic, EndTime-822, EndTime, 70, 240);
            black1.Rotate(OsbEasing.InCubic, EndTime-822, EndTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(20));

            black2.MoveY(OsbEasing.OutCirc, StartTime, StartTime+204, 240, 410);
            black2.Color(EndTime, Color4.Black);
            black2.Fade(StartTime, 1);
            black2.Fade(EndTime, 0);
            black2.Scale(StartTime, 0.8);
            black2.MoveY(OsbEasing.InCubic, EndTime-822, EndTime, 410, 240);
            black2.Rotate(OsbEasing.InCubic, EndTime-822, EndTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(20));

            var vig = layer.CreateSprite(Vig, OsbOrigin.Centre);

            vig.Scale(StartTime, 0.45);
            vig.Fade(StartTime, 1);
            vig.Fade(EndTime, 0); 
        }
    }
}
