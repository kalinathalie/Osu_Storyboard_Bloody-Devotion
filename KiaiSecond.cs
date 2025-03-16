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
    public class KiaiSecond : StoryboardObjectGenerator
    {
        [Configurable] public string Background = "";
        [Configurable] public string Sakura = "";
        [Configurable] public string Cirno = "";
        [Configurable] public string FlashImage = "";
        [Configurable] public string CircleBig = "";
        [Configurable] public string SakuyaSpriteAttack = "";
        [Configurable] public string SakuyaStopTime = "";
        [Configurable] public string SakuyaSpriteRun = "";
        [Configurable] public string SakuyaSpriteRunStart = "";
        [Configurable] public string Fadas = "";
        [Configurable] public string Knife = "";
        [Configurable] public string Bomb = "";
        [Configurable] public string SakuyaSpriteRunBack = "";
        [Configurable] public string SakuyaSpriteRunStop = "";
        [Configurable] public string SakuyaSpriteStop = "";
        [Configurable] public string SakuyaSpriteSlide = "";
        [Configurable] public string SakuyaSpriteSlideSmoke = "";
        [Configurable] public string SakuyaSpriteJump1 = "";
        [Configurable] public string SakuyaSpriteJump2 = "";
        [Configurable] public string SakuyaSpriteGliding = "";
        [Configurable] public string ItemKnife = "";
        [Configurable] public string ItemCircle = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        [Configurable] public string Flash = "";
        [Configurable] public string Floor = "";
        [Configurable] public string Back = "";
        public override void Generate(){
		    var layer = GetLayer("KiaiSecond");
            var layerKiaiBackground = GetLayer("KiaiBackground");
            var bg = layerKiaiBackground.CreateSprite(Background, OsbOrigin.Centre);

            var sakuyaEndTime = EndTime-4400;

            bg.Fade(StartTime, 1);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, 1.4);
            bg.Color(StartTime, StartTime+1600, Color4.Red, Color4.White);

            var cirno = layerKiaiBackground.CreateAnimation(Cirno, 4, 40, OsbLoopType.LoopForever, OsbOrigin.Centre);
            var cirno2 = layerKiaiBackground.CreateAnimation(Cirno, 4, 4000, OsbLoopType.LoopForever, OsbOrigin.Centre);

            cirno.Fade(StartTime, 1);
            cirno.Fade(EndTime-822, 0);
            cirno.Move(StartTime, EndTime-822, -107, 120, 650, 100);
            cirno.Scale(StartTime, 0.7);
            cirno.FlipH(StartTime);
            cirno.Color(StartTime, Color4.DarkGray);

            cirno2.Fade(EndTime-822, 1);
            cirno2.Fade(EndTime, 0);
            cirno2.Move(EndTime-822, 650, 100);
            cirno2.Scale(EndTime-822, 0.7);
            cirno2.FlipH(EndTime-822);
            cirno2.Color(EndTime-822, Color4.DarkGray);

            var sakura = layer.CreateSprite(Sakura, OsbOrigin.BottomCentre);

            sakura.Fade(StartTime, 1);
            sakura.Scale(StartTime, 1.2);
            sakura.Fade(EndTime, 0);
            sakura.MoveX(StartTime, sakuyaEndTime, 2800-115, -1000+900);
            sakura.MoveY(StartTime, 390);


            var back = layer.CreateSprite(Back, OsbOrigin.BottomCentre);

            back.Fade(StartTime, 1);
            back.Scale(StartTime, 1.2);
            back.Fade(EndTime, 0);
            back.MoveX(StartTime, sakuyaEndTime, 2800-115-100, -2300-115+900-100);
            back.MoveY(StartTime, 434);

            //   _                       
            //  | |                      
            //  | |     ___   ___  _ __  
            //  | |    / _ \ / _ \| '_ \ 
            //  | |___| (_) | (_) | |_) |
            //  |______\___/ \___/| .__/ 
            //                    | |    
            //                    |_|    
            

            var currentTime = StartTime;
            var durationAttack = 1120;
            while (currentTime < sakuyaEndTime){
                if(currentTime==StartTime){
                    Run(currentTime, 2600, sakuyaEndTime, layer);
                    currentTime+=2600;
                    continue;
                }
                var initTime = currentTime;
                var lastTime = initTime+3950;

                Attack(currentTime, durationAttack, layer);
                currentTime+=durationAttack;
                Attack(currentTime, durationAttack, layer);
                currentTime+=durationAttack;
                
                
                
                Run(currentTime, lastTime-currentTime, sakuyaEndTime, layer);
                currentTime=lastTime;
            }

            var knives = layer.CreateSprite(ItemKnife, OsbOrigin.Centre);
            var circle = layer.CreateSprite(ItemCircle, OsbOrigin.Centre);

            knives.Fade(sakuyaEndTime-800, 1);
            knives.Scale(sakuyaEndTime+1800, sakuyaEndTime+2000, 1.2, 1.6);
            knives.Fade(sakuyaEndTime+1800, sakuyaEndTime+2000, 1, 0);
            knives.MoveX(sakuyaEndTime-800, sakuyaEndTime, 790, 640);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime-800, sakuyaEndTime-400, 355, 365);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime-400, sakuyaEndTime, 365, 355);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime, sakuyaEndTime+400, 355, 365);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime+400, sakuyaEndTime+800, 365, 355);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime+800, sakuyaEndTime+1200, 355, 365);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime+1200, sakuyaEndTime+1600, 365, 355);
            knives.MoveY(OsbEasing.InOutSine, sakuyaEndTime+1600, sakuyaEndTime+2000, 355, 365);

            circle.Fade(sakuyaEndTime-800, 1);
            circle.Fade(sakuyaEndTime+1800, sakuyaEndTime+2000, 1, 0);
            circle.Scale(sakuyaEndTime-800, 0.1);
            circle.Scale(sakuyaEndTime+1800, sakuyaEndTime+2000, 0.1, 0.2);
            circle.MoveX(sakuyaEndTime-800, sakuyaEndTime, 790, 640);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime-800, sakuyaEndTime-400, 355, 365);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime-400, sakuyaEndTime, 365, 355);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime, sakuyaEndTime+400, 355, 365);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime+400, sakuyaEndTime+800, 365, 355);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime+800, sakuyaEndTime+1200, 355, 365);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime+1200, sakuyaEndTime+1600, 365, 355);
            circle.MoveY(OsbEasing.InOutSine, sakuyaEndTime+1600, sakuyaEndTime+2000, 355, 365);

            var sakuyaRunBack = layer.CreateAnimation(SakuyaSpriteRunBack, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);

            sakuyaRunBack.Fade(currentTime+1350, 1);
            sakuyaRunBack.Scale(currentTime+1350, 1.2);
            sakuyaRunBack.Fade(currentTime+1350 + 315, 0);
            sakuyaRunBack.MoveY(currentTime+1350, 382);
            sakuyaRunBack.MoveX(currentTime+1350, currentTime+1350+315, 630, 580);
            sakuyaRunBack.FlipH(currentTime+1350);

            var sakuyaRunStop = layer.CreateAnimation(SakuyaSpriteRunStop, 5, 63, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);

            sakuyaRunStop.Move(currentTime+1350+315, 580, 382);
            sakuyaRunStop.Scale(currentTime+1350+315, 1.2);
            sakuyaRunStop.Fade(currentTime+1350+315, 1); // start frame
            sakuyaRunStop.Fade(currentTime+1350+315 + 315, 0); // 4 frames * 80ms
            sakuyaRunStop.FlipH(currentTime+1350+315);

            // Animação de parada
            var sakuyaStop = layer.CreateAnimation(SakuyaSpriteStop, 20, 50, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
            sakuyaStop.Move(currentTime+1350+315 + 315, 580, 382);
            sakuyaStop.Scale(currentTime+1350+315 + 315, 1.2);
            sakuyaStop.Fade(currentTime+1350+315 + 315, 1);
            sakuyaStop.Fade(EndTime-822, 0);
            sakuyaStop.FlipH(currentTime+1350+315 + 315);

            //    _____ _           _______ _                
            //   / ____| |         |__   __(_)               
            //  | (___ | |_ ___  _ __ | |   _ _ __ ___   ___ 
            //   \___ \| __/ _ \| '_ \| |  | | '_ ` _ \ / _ \
            //   ____) | || (_) | |_) | |  | | | | | | |  __/
            //  |_____/ \__\___/| .__/|_|  |_|_| |_| |_|\___|
            //                  | |                          
            //                  |_|                          
            
            for(double x = EndTime-822-822-411;x < EndTime-411; x+=105){
                string[] nomes = {"cafe", "dekki", "fabu", "hataki"};
                Random random = new Random();
                int indiceAleatorio = random.Next(nomes.Length);
                var fadaEscolhida = Fadas.Replace("troca", nomes[indiceAleatorio]);
                var yousei = layer.CreateAnimation(fadaEscolhida, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);

                var randomY = new Random().Next(-20, 150);
                if(random.Next(0,2)%2==0){
                    randomY = new Random().Next(260, 360);
                }

                yousei.Fade(x-500, 1);
                yousei.Move(x-500, x+1500, -120, randomY, 575, 360);
                yousei.Scale(x-500, 1.2);
                yousei.FlipH(x-500);
                yousei.Fade(EndTime-822, 0);

                var youseiPosition = yousei.PositionAt(EndTime-822);
                var yousei2 = layer.CreateAnimation(fadaEscolhida, 4, 1000, OsbLoopType.LoopForever, OsbOrigin.Centre, youseiPosition);
                yousei2.Fade(EndTime-822, 1);
                yousei2.Fade(EndTime, 0);
                yousei2.Scale(EndTime-822, 1.2);
                yousei2.FlipH(EndTime-822);
            }

            var flashStopTime = layer.CreateSprite(FlashImage, OsbOrigin.Centre);

            flashStopTime.Fade(EndTime-822, 0.5);
            flashStopTime.Color(EndTime-822, Color4.Blue);
            flashStopTime.Fade(EndTime, 0);
            flashStopTime.Additive(EndTime-822);
            flashStopTime.Scale(EndTime-822, 0.65);

            var circleBig = layer.CreateSprite(CircleBig, OsbOrigin.Centre);

            circleBig.Scale(OsbEasing.Out, EndTime-822, EndTime, 2.7, 0);
            circleBig.Color(EndTime-822, Color4.Red);
            circleBig.Move(EndTime-822, new Vector2(575, 360) + new Vector2(-10, -55));
            circleBig.Fade(EndTime-822, 0.7);

            var sakuyaStopTime = layer.CreateAnimation(SakuyaStopTime, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(580, 382));

            sakuyaStopTime.Fade(EndTime-822, 1);
            sakuyaStopTime.Scale(EndTime-822, 1.2);
            sakuyaStopTime.Fade(EndTime, 0);
            sakuyaStopTime.FlipH(EndTime-822);

            
            var floor = layer.CreateSprite(Floor, OsbOrigin.BottomCentre);

            floor.Fade(StartTime, 1);
            floor.Scale(StartTime, 1.2);
            floor.Fade(EndTime, 0);
            floor.MoveX(StartTime, sakuyaEndTime, 2800-100, -2300+900-100);
            floor.MoveY(StartTime, 479);

            var circleBigBlack = layer.CreateSprite(CircleBig, OsbOrigin.Centre);

            circleBigBlack.Scale(OsbEasing.In, EndTime-411, EndTime, 0, 2.7);
            circleBigBlack.Color(EndTime-411, Color4.Black);
            circleBigBlack.Move(EndTime-411, new Vector2(575, 360) + new Vector2(-10, -55));
            circleBigBlack.Fade(EndTime-411, EndTime, 0.5, 1);

            var black = GetLayer("BlackLine").CreateSprite(Flash, OsbOrigin.Centre);
            
            black.Fade(StartTime-1, 1);
            black.ScaleVec(OsbEasing.OutCirc, StartTime, StartTime+822, 1, 1, 1, 0);
            black.Color(StartTime, Color4.Black);
            black.Rotate(OsbEasing.InCirc, StartTime, StartTime+822, MathHelper.DegreesToRadians(-30), MathHelper.DegreesToRadians(30));
            black.MoveY(OsbEasing.In, StartTime, StartTime+822, 240, 0);            
        }

        //   ______                _   _                 
        //  |  ____|              | | (_)                
        //  | |__ _   _ _ __   ___| |_ _  ___  _ __  ___ 
        //  |  __| | | | '_ \ / __| __| |/ _ \| '_ \/ __|
        //  | |  | |_| | | | | (__| |_| | (_) | | | \__ \
        //  |_|   \__,_|_| |_|\___|\__|_|\___/|_| |_|___/
        
        private void Attack(int startTime, int duration, StoryboardLayer layer){
            var sakuyaAttack = layer.CreateAnimation(SakuyaSpriteAttack, 28, 40, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            
            var endTime = startTime+duration;

            sakuyaAttack.Fade(startTime, 1);
            sakuyaAttack.Scale(startTime, 1.2);
            sakuyaAttack.Move(startTime, 30, 382);
            sakuyaAttack.Fade(endTime, 0);


            string[] nomes = {"cafe", "dekki", "fabu", "hataki"};
            

            int[] knives = {40, 480, 680, 960};

            foreach(int knivetime in knives){
                Random random = new Random();
                int indiceAleatorio = random.Next(nomes.Length);
                var fadaEscolhida = Fadas.Replace("troca", nomes[indiceAleatorio]);
                var yousei = layer.CreateAnimation(fadaEscolhida, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);
                var newknivetime = startTime+knivetime;

                Vector2 playerPosition = new Vector2(30, 382);
                var initPosition = playerPosition+new Vector2(0, -50);

                var knife = layer.CreateSprite(Knife, OsbOrigin.CentreLeft);
                knife.Fade(newknivetime, 1);
                knife.Fade(newknivetime+200, 0);
                knife.Scale(newknivetime, 1.2);
                
                yousei.Fade(newknivetime-1000, 1);
                yousei.MoveX(newknivetime-1000, newknivetime+1000, 750, -120);
                yousei.MoveY(OsbEasing.InOutSine, newknivetime-1000, newknivetime-500, 330, 300);
                yousei.MoveY(OsbEasing.InOutSine, newknivetime-500, newknivetime, 300, 330);
                yousei.MoveY(OsbEasing.InOutSine, newknivetime, newknivetime+500, 330, 300);
                yousei.MoveY(OsbEasing.InOutSine, newknivetime+500, newknivetime+1000, 300, 330);
                yousei.Scale(newknivetime-1000, 1.2);
                yousei.Fade(newknivetime+200, 0);

                Vector2 deathPosition = yousei.PositionAt(newknivetime+200);
                knife.Move(newknivetime, newknivetime+200, initPosition.X, initPosition.Y, deathPosition.X, initPosition.Y);

                var bomb = layer.CreateAnimation(Bomb, 13, 24, OsbLoopType.LoopOnce, OsbOrigin.Centre);

                bomb.Fade(newknivetime+200, 1);
                bomb.Scale(newknivetime+200, 1.6);
                Vector2 randBomb = new Vector2(random.Next(-20, 20), random.Next(-20, 20));
                bomb.Move(newknivetime+200, newknivetime+512, deathPosition+randBomb, deathPosition+randBomb - new Vector2(50, 0));
                bomb.Fade(newknivetime+512, 0);

                var circleDeath = layer.CreateSprite(ItemCircle, OsbOrigin.Centre);

                circleDeath.Scale(newknivetime+200, newknivetime+500, 0.1, 0.2);
                circleDeath.Move(newknivetime+200, newknivetime+512, deathPosition, deathPosition - new Vector2(50, 0));
                circleDeath.Fade(newknivetime+200, newknivetime+500, 1, 0);
                

                
            }
            

            

        }

        private void Run(int startTime, int duration, int sakuyaEndTime, StoryboardLayer layer){
            
            var sakuyaRunStart = layer.CreateAnimation(SakuyaSpriteRunStart, 9, 24, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaRun = layer.CreateAnimation(SakuyaSpriteRun, 16, 24, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            var endTime = startTime+duration;

            sakuyaRunStart.Fade(startTime, 1);
            sakuyaRunStart.Fade(startTime+216, 0);
            sakuyaRunStart.Scale(startTime, 1.2);
            sakuyaRunStart.Move(startTime, 30, 382);
            sakuyaRun.Fade(startTime+216, 1);
            sakuyaRun.Scale(startTime+216, 1.2);
            if(endTime>sakuyaEndTime){
                sakuyaRun.Move(sakuyaEndTime, endTime+1350, 30, 382, 630, 382);
                sakuyaRun.Fade(endTime+1350, 0);
            }else{
                sakuyaRun.Move(startTime+216, 30, 382);
                sakuyaRun.Fade(endTime, 0);
            }
        }      
    }
}
