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
    public class Kiai : StoryboardObjectGenerator
    {
        [Configurable] public string Background = "";
        [Configurable] public string Sakura = "";
        [Configurable] public string Marisa = "";
        [Configurable] public string SakuyaSpriteAttack = "";
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
		    var layer = GetLayer("Kiai");
            var layerKiaiBackground = GetLayer("KiaiBackground");
            var bg = layerKiaiBackground.CreateSprite(Background, OsbOrigin.Centre);

            var sakuyaEndTime = EndTime-4400;

            bg.Fade(StartTime, 1);
            bg.Fade(EndTime, 0);
            bg.Scale(StartTime, 1.4);
            bg.Color(StartTime, StartTime+1600, Color4.Red, Color4.White);

            var marisa = layerKiaiBackground.CreateAnimation(Marisa, 4, 40, OsbLoopType.LoopForever, OsbOrigin.Centre);

            marisa.Fade(StartTime, 1);
            marisa.Fade(EndTime, 0);
            marisa.Move(StartTime, EndTime, -107, 120, 700, 100);
            marisa.Scale(StartTime, 0.5);
            marisa.FlipH(StartTime);
            marisa.Color(StartTime, Color4.DarkGray);

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
            back.MoveX(StartTime, sakuyaEndTime, 2800-115-50, -2300-115+900-50);
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
            var durationSlide = 1000;
            var durationJump = 1500;
            while (currentTime < sakuyaEndTime){
                if(currentTime==StartTime){
                    Run(currentTime, 2600, sakuyaEndTime, layer);
                    currentTime+=2600;
                    continue;
                }
                var initTime = currentTime;
                var lastTime = initTime+3950;
                var rand1 = new Random().Next(0,2);
                var rand2 = new Random().Next(0,2);

                if(rand1%2==0){
                    Slide(currentTime, durationSlide, layer);
                    Yousei(currentTime, layer);
                    currentTime+=durationSlide;
                }else{
                    Jump(currentTime, durationJump, layer);
                    Yousei(currentTime, layer);
                    currentTime+=durationJump;
                }

                Run(currentTime, 500, sakuyaEndTime, layer);
                currentTime+=500;
                
                if(rand2%2==0){
                    Slide(currentTime, durationSlide, layer);
                    Yousei(currentTime, layer);
                    currentTime+=durationSlide;
                }else{
                    Jump(currentTime, durationJump, layer);
                    Yousei(currentTime, layer);
                    currentTime+=durationJump;
                }
                
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
            sakuyaStop.Fade(EndTime-822-822, 0);
            sakuyaStop.FlipH(currentTime+1350+315 + 315);

            var sakuyaAttack = layer.CreateAnimation(SakuyaSpriteAttack, 16, 60, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            sakuyaAttack.Move(EndTime-822-822, 570, 382);
            sakuyaAttack.Scale(EndTime-822-822, 1.2);
            sakuyaAttack.Fade(EndTime-822-822, 1);
            sakuyaAttack.Fade(EndTime, 0);
            sakuyaAttack.FlipH(EndTime-822-822);


            for(double x = EndTime-822-822;x < EndTime-120; x+=105){
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
                if(x+500>EndTime){
                    yousei.Fade(EndTime, 0);
                }else{
                    yousei.Fade(x+500, 0);
                }
                if(x+800 > EndTime){
                    continue;
                }

                Vector2 playerPosition = sakuyaAttack.PositionAt(x);
                var deathPosition = yousei.PositionAt(x+500);
                var initPosition = playerPosition+new Vector2(0, -30);
                
                var knife = layer.CreateSprite(Knife, OsbOrigin.CentreRight);
                knife.Fade(x, 1);
                knife.Scale(x, 1.2);
                knife.Move(x, x+500, initPosition, deathPosition);
                if(x+500>EndTime){
                    knife.Fade(EndTime, 0);
                }else{
                    knife.Fade(x+500, 0);
                }

                double angle = Math.Atan2(deathPosition.Y - initPosition.Y, deathPosition.X - initPosition.X) * (180 / Math.PI);
                knife.Rotate(x, MathHelper.DegreesToRadians(angle));

                var bomb = layer.CreateAnimation(Bomb, 13, 24, OsbLoopType.LoopOnce, OsbOrigin.Centre);

                bomb.Fade(x+500, 1);
                bomb.Scale(x+500, 1.6);
                bomb.Move(x+500, deathPosition);
                bomb.Fade(x+500+312, 0);
                

                var circleDeath = layer.CreateSprite(ItemCircle, OsbOrigin.Centre);

                circleDeath.Scale(x+500, x+800, 0.1, 0.2);
                circleDeath.Move(x+500, deathPosition);
                circleDeath.Fade(x+500, x+800, 1, 0);
            }




            
            var floor = layer.CreateSprite(Floor, OsbOrigin.BottomCentre);

            floor.Fade(StartTime, 1);
            floor.Scale(StartTime, 1.2);
            floor.Fade(EndTime, 0);
            floor.MoveX(StartTime, sakuyaEndTime, 2800-50, -2300+900-50);
            floor.MoveY(StartTime, 479);

            var black = GetLayer("BlackLine").CreateSprite(Flash, OsbOrigin.Centre);
            
            black.Fade(StartTime-1, 1);
            black.ScaleVec(OsbEasing.OutCirc, StartTime, StartTime+822, 1, 1, 1, 0);
            black.Color(StartTime, Color4.Black);
            black.Rotate(OsbEasing.InCirc, StartTime, StartTime+822, MathHelper.DegreesToRadians(-30), MathHelper.DegreesToRadians(30));
            black.MoveY(OsbEasing.In, StartTime, StartTime+822, 240, 0);

            black.Fade(EndTime-822, 1);
            black.ScaleVec(OsbEasing.InCirc, EndTime-822, EndTime, 1, 0, 1, 1.8);
            black.Color(EndTime-822, Color4.Black);
            black.Rotate(OsbEasing.InCirc, EndTime-822, EndTime, MathHelper.DegreesToRadians(-30-90), MathHelper.DegreesToRadians(30-90));
            black.MoveY(OsbEasing.Out, EndTime-822, EndTime, 300, -0);
            
        }

        //   ______                _   _                 
        //  |  ____|              | | (_)                
        //  | |__ _   _ _ __   ___| |_ _  ___  _ __  ___ 
        //  |  __| | | | '_ \ / __| __| |/ _ \| '_ \/ __|
        //  | |  | |_| | | | | (__| |_| | (_) | | | \__ \
        //  |_|   \__,_|_| |_|\___|\__|_|\___/|_| |_|___/
        
        private void Jump(int startTime, int duration, StoryboardLayer layer){
            var sakuyaJumpOne = layer.CreateAnimation(SakuyaSpriteJump1, 2, 60, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaJumpTwo = layer.CreateAnimation(SakuyaSpriteJump2, 6, 60, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaGliding = layer.CreateAnimation(SakuyaSpriteGliding, 12, 70, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);

            sakuyaJumpOne.Scale(startTime, 1.2);
            sakuyaJumpOne.MoveX(startTime, 30);
            sakuyaJumpOne.MoveY(OsbEasing.Out, startTime, startTime+205, 382, 360);

            sakuyaJumpTwo.Scale(startTime+205, 1.2);
            sakuyaJumpTwo.MoveX(startTime+205, 30);
            sakuyaJumpTwo.MoveY(OsbEasing.Out, startTime+205, startTime+616, 360, 240);

            sakuyaGliding.Scale(startTime+616, 1.2);
            sakuyaGliding.MoveX(startTime+616, 30);
            sakuyaGliding.MoveY(OsbEasing.In, startTime+616, startTime+duration, 240, 382);
        }

        private void Slide(int startTime, int duration, StoryboardLayer layer){
            var sakuyaSlide = layer.CreateAnimation(SakuyaSpriteSlide, 8, 120, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var endTime = startTime+duration;
            float smokeTime = (endTime-startTime)/10.0f;

            sakuyaSlide.Fade(startTime, 1);
            sakuyaSlide.Fade(endTime, 0);
            sakuyaSlide.Scale(startTime, 1.2);
            sakuyaSlide.Move(startTime, 30, 382);

            for(int x=1; x<=10; x++){
                var smoke = layer.CreateAnimation(SakuyaSpriteSlideSmoke, 8, 120, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                smoke.Fade(startTime+smokeTime*x, 1);
                smoke.Fade(startTime+smokeTime*x+700, 0);
                smoke.Scale(startTime+smokeTime*x, 0.7);
                smoke.MoveY(startTime+smokeTime*x, 382);
                smoke.MoveX(startTime+smokeTime*x, startTime+smokeTime*x+700, 30, -100);

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
        private void Yousei(int startTime, StoryboardLayer layer){
            string[] nomes = {"cafe", "dekki", "fabu", "hataki"};
            Random random = new Random();
            int indiceAleatorio = random.Next(nomes.Length);
            var fadaEscolhida = Fadas.Replace("troca", nomes[indiceAleatorio]);

            var yousei = layer.CreateAnimation(fadaEscolhida, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);

            yousei.MoveX(startTime-1000, startTime+1000, 750, -120);

            yousei.MoveY(OsbEasing.InOutSine, startTime-1000, startTime-500, 330, 300);
            yousei.MoveY(OsbEasing.InOutSine, startTime-500, startTime, 300, 330);
            yousei.MoveY(OsbEasing.InOutSine, startTime, startTime+500, 330, 300);
            yousei.MoveY(OsbEasing.InOutSine, startTime+500, startTime+1000, 300, 330);
            
            yousei.Scale(startTime-1000, 1.2);
        }        
    }
}
