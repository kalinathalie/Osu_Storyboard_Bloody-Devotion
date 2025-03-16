using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class Stage : StoryboardObjectGenerator
    {
        [Configurable] public string BackgroundImage = "";
        [Configurable] public string Map1Image = "";
        [Configurable] public string Map1FrontImage = "";
        [Configurable] public string Map2Image = "";
        [Configurable] public string MagicalImage = "";
        [Configurable] public string Pixel = "";
        [Configurable] public string FlashImage = "";
        [Configurable] public string Circle = "";
        [Configurable] public string CircleBig = "";
        [Configurable] public string Lamp = "";
        [Configurable] public string Knife = "";
        [Configurable] public string AfterBunerAnimation = "";
        [Configurable] public string EnergyAnimation = "";
        [Configurable] public string BigShotAnimation = "";
        [Configurable] public string BigShotAnimation2 = "";
        [Configurable] public string RemiliaAnimation = "";
        [Configurable] public string RemiliaStopped = "";
        [Configurable] public string SakuyaFalling = "";
        [Configurable] public string SakuyaDown = "";
        [Configurable] public string SakuyaSpriteRun = "";
        [Configurable] public string SakuyaStopTime = "";
        [Configurable] public string SakuyaJump1 = "";
        [Configurable] public string SakuyaJump2 = "";
        [Configurable] public string SakuyaJumpAttack = "";
        [Configurable] public string SakuyaSpriteBack = "";
        [Configurable] public string SakuyaSpriteStop = "";
        [Configurable] public string SakuyaSpriteRunStart = "";
        [Configurable] public string SakuyaSpriteRunStop = "";
        [Configurable] public string SakuyaSpriteRunBack = "";
        [Configurable] public string BigHand = "";
        [Configurable] public string SmallHand = "";
        [Configurable] public string ClockCenter = "";
        [Configurable] public string BigHandShadow = "";
        [Configurable] public string SmallHandShadow = "";
        [Configurable] public string ClockCenterShadow = "";
        [Configurable] public string GreenBall = "";
        [Configurable] public string GreenBallOther = "";
        [Configurable] public string BlackBall = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        
        public class Time{
            public int FirstTime { get; set; }
            public int LastTime { get; set; }
            public float FirstPosition { get; set; }
            public float LastPosition { get; set; }
        }
        public override void Generate()
        {
		    var MapLayer = GetLayer("MapLayer");
            var PlayerLayer = GetLayer("PlayerLayer");
            var LampLayer = GetLayer("LampLayer");
            var ShotLayer = GetLayer("ShotLayer");
            var BigShotLayer = GetLayer("BigShotLayer");

            float scaleMap = 1;
            var increasePosition = 130;

            var background = MapLayer.CreateSprite(BackgroundImage, OsbOrigin.Centre);
            background.Fade(StartTime, 1);
            background.Fade(EndTime, 0);
            background.Scale(StartTime, 1.35);

            var clockmap = MapLayer.CreateSprite(Map2Image, OsbOrigin.BottomCentre);
            clockmap.MoveX(StartTime, 320);
            clockmap.MoveY(StartTime, 397);
            clockmap.Fade(StartTime, 1);
            clockmap.Fade(EndTime, 0);
            clockmap.Scale(StartTime, 1.2*scaleMap);


            var map1 = LampLayer.CreateSprite(Map1Image, OsbOrigin.BottomCentre);
            map1.MoveX(StartTime, 321);
            map1.MoveY(StartTime, 480);
            map1.Fade(StartTime, 1);
            map1.Fade(EndTime, 0);
            map1.Scale(StartTime, 1.3*scaleMap);

            var bighandshadow = MapLayer.CreateSprite(BigHandShadow, OsbOrigin.TopCentre, new Vector2(283, 260));

            bighandshadow.Fade(StartTime, 0.6);
            bighandshadow.Fade(EndTime, 0);
            bighandshadow.Scale(StartTime, 1.2*scaleMap);
            bighandshadow.Rotate(StartTime, MathHelper.DegreesToRadians(220));

            var smallhandshadow = MapLayer.CreateSprite(SmallHandShadow, OsbOrigin.TopCentre, new Vector2(358, 256));

            smallhandshadow.Fade(StartTime, 0.6);
            smallhandshadow.Fade(EndTime, 0);
            smallhandshadow.Scale(StartTime, 1.2*scaleMap);
            smallhandshadow.Rotate(StartTime, MathHelper.DegreesToRadians(128));

            var centreshadow = MapLayer.CreateSprite(ClockCenterShadow, OsbOrigin.TopCentre, new Vector2(320, 162));

            centreshadow.Fade(StartTime, 0.6);
            centreshadow.Fade(EndTime, 0);
            centreshadow.Scale(StartTime, 1.2*scaleMap);

            var bighand = MapLayer.CreateAnimation(BigHand, 30, 20, OsbLoopType.LoopForever, OsbOrigin.TopCentre, new Vector2(283, 250));

            bighand.Fade(StartTime, 1);
            bighand.Fade(EndTime, 0);
            bighand.Scale(StartTime, 1.2*scaleMap);
            bighand.Rotate(StartTime, MathHelper.DegreesToRadians(220));

            var smallhand = MapLayer.CreateAnimation(SmallHand, 48, 16, OsbLoopType.LoopForever, OsbOrigin.TopCentre, new Vector2(366, 242));

            smallhand.Fade(StartTime, 1);
            smallhand.Fade(EndTime, 0);
            smallhand.Scale(StartTime, 1.2*scaleMap);
            smallhand.Rotate(StartTime, MathHelper.DegreesToRadians(128));

            var center = MapLayer.CreateAnimation(ClockCenter, 28, 50, OsbLoopType.LoopForever, OsbOrigin.Centre, new Vector2(320, 208));

            center.Fade(StartTime, 1);
            center.Fade(EndTime, 0);
            center.Scale(StartTime, 1.2*scaleMap);

            var greenball1 = MapLayer.CreateSprite(GreenBall, OsbOrigin.Centre, new Vector2(253, 153));

            greenball1.Fade(StartTime, 1);
            greenball1.Fade(EndTime, 0);
            greenball1.Scale(StartTime, 1.4*scaleMap);

            var greenball2 = MapLayer.CreateSprite(GreenBall, OsbOrigin.Centre, new Vector2(360, 157));

            greenball2.Fade(StartTime, 1);
            greenball2.Fade(EndTime, 0);
            greenball2.Scale(StartTime, 1.3*scaleMap);

            var greenball3 = MapLayer.CreateSprite(GreenBallOther, OsbOrigin.Centre, new Vector2(298, 232));

            greenball3.Fade(StartTime, 1);
            greenball3.Fade(EndTime, 0);
            greenball3.Scale(StartTime, 1.2*scaleMap);

            var greenball4 = MapLayer.CreateSprite(GreenBallOther, OsbOrigin.Centre, new Vector2(356, 234));

            greenball4.Fade(StartTime, 1);
            greenball4.Fade(EndTime, 0);
            greenball4.Scale(StartTime, 1.2*scaleMap);

            var blackball = MapLayer.CreateSprite(BlackBall, OsbOrigin.Centre, new Vector2(340, 181));

            blackball.Fade(StartTime, 1);
            blackball.Fade(EndTime, 0);
            blackball.Scale(StartTime, 1.2*scaleMap);

            var map1front = LampLayer.CreateSprite(Map1FrontImage, OsbOrigin.BottomCentre);
            map1front.MoveX(StartTime, 320);
            map1front.MoveY(StartTime, 458);
            map1front.Fade(StartTime, 1);
            map1front.Fade(EndTime, 0);
            map1front.Scale(StartTime, 1.3*scaleMap);

            var staticLampString = Lamp.Substring(0, 29) + "_0.png";
            
            var lamp1 = MapLayer.CreateAnimation(Lamp, 8, 50, OsbLoopType.LoopForever, OsbOrigin.BottomLeft);
            var lamp2 = MapLayer.CreateAnimation(Lamp, 8, 50, OsbLoopType.LoopForever, OsbOrigin.BottomLeft);
            var lamp3 = MapLayer.CreateAnimation(Lamp, 8, 50, OsbLoopType.LoopForever, OsbOrigin.BottomLeft);
            var lamp4 = MapLayer.CreateAnimation(Lamp, 8, 50, OsbLoopType.LoopForever, OsbOrigin.BottomLeft);
            var lamp1Static = MapLayer.CreateSprite(staticLampString, OsbOrigin.BottomLeft);
            var lamp2Static = MapLayer.CreateSprite(staticLampString, OsbOrigin.BottomLeft);
            var lamp3Static = MapLayer.CreateSprite(staticLampString, OsbOrigin.BottomLeft);
            var lamp4Static = MapLayer.CreateSprite(staticLampString, OsbOrigin.BottomLeft);
            lamp1.MoveX(StartTime, -52);
            lamp1.MoveY(StartTime, 396);
            lamp1.Fade(StartTime, 1);
            lamp1.Fade(EndTime, 0);
            lamp1.Scale(StartTime, 1.2*scaleMap);

            lamp2.MoveX(StartTime, 73);
            lamp2.MoveY(StartTime, 396);
            lamp2.Fade(StartTime, 1);
            lamp2.Fade(EndTime, 0);
            lamp2.Scale(StartTime, 1.2*scaleMap);

            lamp3.MoveX(StartTime, 531);
            lamp3.MoveY(StartTime, 396);
            lamp3.Fade(StartTime, 1);
            lamp3.Fade(EndTime, 0);
            lamp3.Scale(StartTime, 1.2*scaleMap);

            lamp4.MoveX(StartTime, 655);
            lamp4.MoveY(StartTime, 396);
            lamp4.Fade(StartTime, 1);
            lamp4.Fade(EndTime, 0);
            lamp4.Scale(StartTime, 1.2*scaleMap);

            var magical = PlayerLayer.CreateSprite(MagicalImage, OsbOrigin.Centre);
            var remilia = PlayerLayer.CreateAnimation(RemiliaAnimation, 16, 50, OsbLoopType.LoopForever, OsbOrigin.Centre, new Vector2(320, 150));

            remilia.Fade(OsbEasing.InCubic, StartTime-408, StartTime, 0, 0.6);
            remilia.Fade(StartTime, 1);
            remilia.Fade(EndTime-411, 0);
            remilia.Scale(StartTime, 1.2);

            // Inicializando a posição da Remilia
            float remiliaYCenter = 140f; // Centro do movimento vertical
            float remiliaAmplitude = 20f; // Amplitude do movimento vertical (±20)

            // Definindo as animações da Sakuya
            var beforePosition = new Vector2(28, 396);
            var initialPosition = new Vector2(28+increasePosition, 396);
            var sakuyaBack = PlayerLayer.CreateAnimation(SakuyaSpriteBack, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            // Iniciando com a Sakuya parada
            sakuyaBack.Move(StartTime, beforePosition);
            sakuyaBack.Move(OsbEasing.Out, 217801, 218623, beforePosition, initialPosition);
            sakuyaBack.Scale(StartTime, 1.2);
            sakuyaBack.Fade(StartTime, 1);
            sakuyaBack.Fade(218623, 0); // Desaparece quando começa a se mover

            var currentTime = 218623;
            var isFacingRight = true;
            var random = new Random(RandomSeed);


            var currentPositionMap1 = new Vector2(320+increasePosition, 479);
            var currentPositionClockmap = new Vector2(320+increasePosition, 397);
            var currentPositionMap1Front = new Vector2(320+increasePosition, 458);

            var currentPositionBighandshadow = new Vector2(283+increasePosition, 260);
            var currentPositionSmallhandshadow = new Vector2(358+increasePosition, 256);

            var currentPositionCentreshadow = new Vector2(320+increasePosition, 162);
            var currentPositionBighand = new Vector2(283+increasePosition, 250);
            var currentPositionSmallhand = new Vector2(366+increasePosition, 242);
            var currentPositionCenter = new Vector2(320+increasePosition, 208);
            var currentPositionGreenball1 = new Vector2(253+increasePosition, 153);
            var currentPositionGreenball2 = new Vector2(360+increasePosition, 157);
            var currentPositionGreenball3 = new Vector2(298+increasePosition, 232);
            var currentPositionGreenball4 = new Vector2(356+increasePosition, 234);
            var currentPositionBlackball = new Vector2(340+increasePosition, 181);

            var currentPositionLamp1 = new Vector2(-52+increasePosition, 396);
            var currentPositionLamp2 = new Vector2(73+increasePosition, 396);
            var currentPositionLamp3 = new Vector2(531+increasePosition, 396);
            var currentPositionLamp4 = new Vector2(655+increasePosition, 396);

            map1.MoveX(OsbEasing.Out, 217801, 218623, 320, currentPositionMap1.X);
            clockmap.MoveX(OsbEasing.Out, 217801, 218623, 320, currentPositionClockmap.X);
            map1front.MoveX(OsbEasing.Out, 217801, 218623, 320, currentPositionMap1Front.X);

            bighandshadow.MoveX(OsbEasing.Out, 217801, 218623, 283, currentPositionBighandshadow.X);
            smallhandshadow.MoveX(OsbEasing.Out, 217801, 218623, 358, currentPositionSmallhandshadow.X);
            centreshadow.MoveX(OsbEasing.Out, 217801, 218623, 320, currentPositionCentreshadow.X);
            bighand.MoveX(OsbEasing.Out, 217801, 218623, 283, currentPositionBighand.X);
            smallhand.MoveX(OsbEasing.Out, 217801, 218623, 366, currentPositionSmallhand.X);
            center.MoveX(OsbEasing.Out, 217801, 218623, 320, currentPositionCenter.X);
            greenball1.MoveX(OsbEasing.Out, 217801, 218623, 253, currentPositionGreenball1.X);
            greenball2.MoveX(OsbEasing.Out, 217801, 218623, 360, currentPositionGreenball2.X);
            greenball3.MoveX(OsbEasing.Out, 217801, 218623, 298, currentPositionGreenball3.X);
            greenball4.MoveX(OsbEasing.Out, 217801, 218623, 356, currentPositionGreenball4.X);
            blackball.MoveX(OsbEasing.Out, 217801, 218623, 340, currentPositionBlackball.X);

            lamp1.MoveX(OsbEasing.Out, 217801, 218623, -52, currentPositionLamp1.X);
            lamp2.MoveX(OsbEasing.Out, 217801, 218623, 73, currentPositionLamp2.X);
            lamp3.MoveX(OsbEasing.Out, 217801, 218623, 531, currentPositionLamp3.X);
            lamp4.MoveX(OsbEasing.Out, 217801, 218623, 655, currentPositionLamp4.X);
            

            var remiliaCurrentPosition = new Vector2(320f+increasePosition, remiliaYCenter);
            remilia.Move(OsbEasing.Out, 217801, 218623, 320, remiliaYCenter, 320+increasePosition, remiliaYCenter);

            List<Time> TimeList = new List<Time>();
            TimeList.Add(new Time{
                FirstTime = 217801,
                LastTime = 218623,
                FirstPosition = 320,
                LastPosition = 320+increasePosition
            });

            int AltMove = 0;

            var sakuyaEndTime = 257253;

            magical.Scale(OsbEasing.OutCirc, 216979, 217801, 0, 0.15);
            magical.Move(OsbEasing.Out, 217801, 218623, 320, remiliaYCenter, 320+increasePosition, remiliaYCenter);

            magical.Scale(217801, 0.15);
            magical.Color(217801, Color4.Red);
            magical.Fade(217801, 0.3);
            magical.Fade(EndTime, 0);

            // Remilia attacks
                
            var energy1 = PlayerLayer.CreateAnimation(EnergyAnimation, 8, 20, OsbLoopType.LoopForever, OsbOrigin.Centre);
            var energy2 = PlayerLayer.CreateAnimation(EnergyAnimation, 8, 20, OsbLoopType.LoopForever, OsbOrigin.Centre);
            var energy3 = PlayerLayer.CreateAnimation(EnergyAnimation, 8, 20, OsbLoopType.LoopForever, OsbOrigin.Centre);
            var energy1Pos = new Vector2(0, -70);     // Topo
            var energy2Pos = new Vector2(60, 30);      // Inferior esquerdo
            var energy3Pos = new Vector2(-60, 30);     // Inferior direito
            
            energy1.Scale(OsbEasing.In, 217801, 217801+100, 0, 1);
            energy2.Scale(OsbEasing.In, 217801+250, 217801+350, 0, 1);
            energy3.Scale(OsbEasing.In, 217801+500, 217801+600, 0, 1);

            energy1.Move(OsbEasing.Out, 217801, 218623, 320, remiliaYCenter+energy1Pos.Y, 320+increasePosition, remiliaYCenter+energy1Pos.Y);
            energy2.Move(OsbEasing.Out, 217801, 218623, 320+energy2Pos.X, remiliaYCenter+energy2Pos.Y, 320+increasePosition+energy2Pos.X, remiliaYCenter+energy2Pos.Y);
            energy3.Move(OsbEasing.Out, 217801, 218623, 320+energy3Pos.X, remiliaYCenter+energy3Pos.Y, 320+increasePosition+energy3Pos.X, remiliaYCenter+energy3Pos.Y);

            energy1.Fade(217801, 1);
            var currentPosition = initialPosition;
            var endPosition = currentPosition;
            var bigshot2NextPosition = new Vector2(0,0);
            var bigshot3NextPosition = new Vector2(0,0);

            float velocity = 0.5f;
            bool SakuyaisMoving = true;

            //   _                       
            //  | |                      
            //  | |     ___   ___  _ __  
            //  | |    / _ \ / _ \| '_ \ 
            //  | |___| (_) | (_) | |_) |
            //  |______\___/ \___/| .__/ 
            //                    | |    
            //                    |_|    
       

            while (currentTime < sakuyaEndTime)
            {
                int actionDuration = random.Next(600,1200);
                int initTime = currentTime;
                var firstPosition = currentPosition;
                if (currentTime + actionDuration > sakuyaEndTime)
                {
                    actionDuration = sakuyaEndTime - currentTime;
                }

                var sakuyaRunStart = PlayerLayer.CreateAnimation(SakuyaSpriteRunStart, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
                var sakuyaRun = PlayerLayer.CreateAnimation(SakuyaSpriteRun, 16, 30, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
                var sakuyaStop = PlayerLayer.CreateAnimation(SakuyaSpriteStop, 20, 50, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
                var sakuyaRunBack = PlayerLayer.CreateAnimation(SakuyaSpriteRunBack, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
                var sakuyaRunStop = PlayerLayer.CreateAnimation(SakuyaSpriteRunStop, 5, 63, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);

                SakuyaisMoving = AltMove % 2 == 0;
                bool RemiliaisMoving = AltMove % 5 == 0;

                if (currentTime == 218623)
                {
                    SakuyaisMoving = true;
                }

                
                            
                // **Movimento da Sakuya**
                if (SakuyaisMoving)
                {
                    // Movimento da Sakuya (código existente)
                    if (random.Next(0, 2) == 0)
                    {
                        var nextPositionX = currentPosition.X + actionDuration / 3;
                        if (nextPositionX > 730)
                        {
                            endPosition = new Vector2(currentPosition.X - actionDuration / 3, 396);
                        }
                        else
                        {
                            endPosition = new Vector2(nextPositionX, 396);
                        }
                    }
                    else
                    {
                        var nextPositionX = currentPosition.X - actionDuration / 3;
                        if (nextPositionX < -90)
                        {
                            endPosition = new Vector2(currentPosition.X + actionDuration / 3, 396);
                        }
                        else
                        {
                            endPosition = new Vector2(nextPositionX, 396);
                        }
                    }

                    bool movingRight = endPosition.X >= currentPosition.X;
                    var needsBack = movingRight != isFacingRight;
                    if (needsBack)
                    {
                        TurnBack(currentTime, currentPosition, movingRight, sakuyaRunBack);
                        currentTime += 315;
                        currentPosition += new Vector2(isFacingRight == false ? 54 : -54, 0);
                        isFacingRight = movingRight;
                    }

                    StartMoving(currentTime, actionDuration, currentPosition, endPosition, isFacingRight, sakuyaRunStart, sakuyaRun, sakuyaStop, sakuyaRunStop, sakuyaRunBack, needsBack);
                    currentPosition = endPosition;
                    currentTime += actionDuration;
                    currentTime += 315;
                }
                else
                {
                    StopMoving(currentTime, actionDuration, currentPosition, isFacingRight, sakuyaRunStop, sakuyaStop, sakuyaRun, sakuyaRunStart, sakuyaRunBack);
                    currentTime += actionDuration;
                    currentTime += 315;
                }      

                //   __  __                  __  __             
                //  |  \/  |                |  \/  |            
                //  | \  / | _____   _____  | \  / | __ _ _ __  
                //  | |\/| |/ _ \ \ / / _ \ | |\/| |/ _` | '_ \ 
                //  | |  | | (_) \ V /  __/ | |  | | (_| | |_) |
                //  |_|  |_|\___/ \_/ \___| |_|  |_|\__,_| .__/ 
                //                                       | |    
                //                                       |_|    
                
                float moved = 0.6f*(firstPosition.X-endPosition.X);

                TimeList.Add(new Time{
                    FirstTime = initTime,
                    LastTime = currentTime,
                    FirstPosition = currentPositionMap1.X,
                    LastPosition = currentPositionMap1.X+moved
                });

                map1.MoveX(initTime, currentTime, currentPositionMap1.X, currentPositionMap1.X+moved);
                currentPositionMap1.X += moved;

                clockmap.MoveX(initTime, currentTime, currentPositionClockmap.X, currentPositionClockmap.X+moved);
                currentPositionClockmap.X += moved;

                map1front.MoveX(initTime, currentTime, currentPositionMap1Front.X, currentPositionMap1Front.X+moved);
                currentPositionMap1Front.X += moved;

                bighandshadow.MoveX(initTime, currentTime, currentPositionBighandshadow.X, currentPositionBighandshadow.X+moved);
                currentPositionBighandshadow.X += moved;
                smallhandshadow.MoveX(initTime, currentTime, currentPositionSmallhandshadow.X, currentPositionSmallhandshadow.X+moved);
                currentPositionSmallhandshadow.X += moved;
                centreshadow.MoveX(initTime, currentTime, currentPositionCentreshadow.X, currentPositionCentreshadow.X+moved);
                currentPositionCentreshadow.X += moved;
                bighand.MoveX(initTime, currentTime, currentPositionBighand.X, currentPositionBighand.X+moved);
                currentPositionBighand.X += moved;
                smallhand.MoveX(initTime, currentTime, currentPositionSmallhand.X, currentPositionSmallhand.X+moved);
                currentPositionSmallhand.X += moved;
                center.MoveX(initTime, currentTime, currentPositionCenter.X, currentPositionCenter.X+moved);
                currentPositionCenter.X += moved;
                greenball1.MoveX(initTime, currentTime, currentPositionGreenball1.X, currentPositionGreenball1.X+moved);
                currentPositionGreenball1.X += moved;
                greenball2.MoveX(initTime, currentTime, currentPositionGreenball2.X, currentPositionGreenball2.X+moved);
                currentPositionGreenball2.X += moved;
                greenball3.MoveX(initTime, currentTime, currentPositionGreenball3.X, currentPositionGreenball3.X+moved);
                currentPositionGreenball3.X += moved;
                greenball4.MoveX(initTime, currentTime, currentPositionGreenball4.X, currentPositionGreenball4.X+moved);
                currentPositionGreenball4.X += moved;
                blackball.MoveX(initTime, currentTime, currentPositionBlackball.X, currentPositionBlackball.X+moved);
                currentPositionBlackball.X += moved;

                lamp1.MoveX(initTime, currentTime, currentPositionLamp1.X, currentPositionLamp1.X+moved);
                currentPositionLamp1.X += moved;
                lamp2.MoveX(initTime, currentTime, currentPositionLamp2.X, currentPositionLamp2.X+moved);
                currentPositionLamp2.X += moved;
                lamp3.MoveX(initTime, currentTime, currentPositionLamp3.X, currentPositionLamp3.X+moved);
                currentPositionLamp3.X += moved;
                lamp4.MoveX(initTime, currentTime, currentPositionLamp4.X, currentPositionLamp4.X+moved);
                currentPositionLamp4.X += moved;

                sakuyaRun.Fade(currentTime, 0);
                sakuyaStop.Fade(currentTime, 0);

                //   _____                _ _ _       
                //  |  __ \              (_) (_)      
                //  | |__) |___ _ __ ___  _| |_  __ _ 
                //  |  _  // _ \ '_ ` _ \| | | |/ _` |
                //  | | \ \  __/ | | | | | | | | (_| |
                //  |_|  \_\___|_| |_| |_|_|_|_|\__,_|
                                   
                // Ela sempre se move no eixo Y (SHM)
                float remiliaActionStartTime = initTime;
                double numIntervals = 50f; // Número de intervalos para movimento suave
                float remiliaActionEndTime = currentTime;
                double remiliaRunTime = remiliaActionEndTime - remiliaActionStartTime;
                double intervalDuration = remiliaRunTime / numIntervals;

                double remiliaOmega = 2 * Math.PI / remiliaRunTime; // Período do SHM em milissegundos

                
                // Posição X depende se a Remilia está se movendo no X
                Vector2 remiliaPosition0 = new Vector2(0,0);
                Vector2 remiliaPosition1 = new Vector2(0,0);
                
                // A Remilia acompanha a Sakuya no eixo X
                float remiliaStartX = remiliaCurrentPosition.X;
                float remiliaEndX = endPosition.X;
                float minX = -70;
                float maxX = 710;

                // Calcular os possíveis intervalos
                float leftIntervalStart = minX;
                float leftIntervalEnd = remiliaStartX - 200;

                float rightIntervalStart = remiliaStartX + 200;

                // Ajustar os intervalos para estarem dentro dos limites
                leftIntervalEnd = Math.Min(leftIntervalEnd, maxX);
                leftIntervalEnd = Math.Max(leftIntervalEnd, minX);

                rightIntervalStart = Math.Max(rightIntervalStart, minX);
                rightIntervalStart = Math.Min(rightIntervalStart, maxX);

                // Calcular o tamanho dos intervalos
                float leftIntervalLength = leftIntervalEnd - leftIntervalStart;
                float rightIntervalLength = maxX - rightIntervalStart;

                // Garantir que os tamanhos sejam não negativos
                leftIntervalLength = Math.Max(leftIntervalLength, 0);
                rightIntervalLength = Math.Max(rightIntervalLength, 0);

                float totalLength = leftIntervalLength + rightIntervalLength;
                float randomValue = (float)random.NextDouble() * totalLength;
                float remiliaRandomX = 0f;
                float desiredDistance = 20f;

                if (randomValue < leftIntervalLength)
                {
                    // Selecionar aleatoriamente dentro do intervalo esquerdo
                    remiliaRandomX = leftIntervalStart + randomValue;
                }else{
                    // Selecionar aleatoriamente dentro do intervalo direito
                    remiliaRandomX = rightIntervalStart + (randomValue - leftIntervalLength);
                }
                if(random.Next(0,2)==1){ // WhereRemiliaGo
                    remiliaEndX = remiliaRandomX;
                }

                for (int i = 0; i < numIntervals; i++){
                    double t0 = remiliaActionStartTime + i * intervalDuration;
                    double t1 = remiliaActionStartTime + (i + 1) * intervalDuration;

                    // Calcula as posições Y usando SHM
                    float y0 = remiliaYCenter + remiliaAmplitude * (float)Math.Sin(remiliaOmega * (t0 - initTime));
                    float y1 = remiliaYCenter + remiliaAmplitude * (float)Math.Sin(remiliaOmega * (t1 - initTime));

                    if (RemiliaisMoving)
                    {     

                        double progress0 = (t0 - remiliaActionStartTime) / (remiliaRunTime);
                        double progress1 = (t1 - remiliaActionStartTime) / (remiliaRunTime);

                        float x0 = (float)(remiliaStartX + (remiliaEndX - remiliaStartX) * progress0);
                        float x1 = (float)(remiliaStartX + (remiliaEndX - remiliaStartX) * progress1);

                        remiliaPosition0 = new Vector2(x0, y0);
                        remiliaPosition1 = new Vector2(x1, y1);

                    }
                    else
                    {

                        remiliaEndX = remiliaStartX + moved;
                        if(remiliaEndX<=-90)remiliaEndX=-90;
                        if(remiliaEndX>=730)remiliaEndX=730;

                        double progress0 = (t0 - remiliaActionStartTime) / (remiliaRunTime);
                        double progress1 = (t1 - remiliaActionStartTime) / (remiliaRunTime);

                        float x0 = (float)(remiliaStartX + (remiliaEndX - remiliaStartX) * progress0);
                        float x1 = (float)(remiliaStartX + (remiliaEndX - remiliaStartX) * progress1);

                        remiliaPosition0 = new Vector2(x0, y0);
                        remiliaPosition1 = new Vector2(x1, y1);
                    }
                    
                    remilia.Move((int)t0, (int)t1, remiliaPosition0, remiliaPosition1);
                    magical.Move((int)t0, (int)t1, remiliaPosition0, remiliaPosition1);
                    

                    if(!SakuyaisMoving){ // First Attack Part
                        if(i==0){
                            
                            energy2.Fade((int)t0, 0);
                            energy3.Fade((int)t0, 0);

                            energy1.Fade(OsbEasing.InCirc, (int)t0, (int)t0+100, 0, 1);
                            energy1.Scale(OsbEasing.InCirc, (int)t0, (int)t0+100, 0, 1);
                            
                            energy2.Scale(OsbEasing.InCirc, (int)t0+250, (int)t0+350, 0, 1);
                            energy2.Fade(OsbEasing.InCirc, (int)t0+250, (int)t0+350, 0, 1);

                            energy3.Scale(OsbEasing.InCirc, (int)t0+500, (int)t0+600, 0, 1);
                            energy3.Fade(OsbEasing.InCirc, (int)t0+500, (int)t0+600, 0, 1);
                        }

                        energy1.Move((int)t0, (int)t1, remiliaPosition0+energy1Pos, remiliaPosition1+energy1Pos);
                        energy2.Move((int)t0, (int)t1, remiliaPosition0+energy2Pos, remiliaPosition1+energy2Pos);
                        energy3.Move((int)t0, (int)t1, remiliaPosition0+energy3Pos, remiliaPosition1+energy3Pos);
                        
                    }else{ // Second Attack part
                        if(i==0){

                            energy1.Fade((int)t0, 0);
                            energy2.Fade((int)t0, 1);
                            energy3.Fade((int)t0, 1);

                            energy2.Fade((int)t0+350, 0);
                            energy3.Fade((int)t0+700, 0);

                            var bigshot1NextPosition = firstPosition+new Vector2(clockmap.PositionAt(remiliaActionStartTime+600).X-clockmap.PositionAt(remiliaActionStartTime).X, 0);

                            float dist = CalculateDistance(remiliaPosition0+energy1Pos, bigshot1NextPosition);

                            var bigshot1SecondPosition = new Vector2(remiliaPosition0.X+energy1Pos.X, remiliaPosition0.Y+energy1Pos.Y);

                            double bigshot1DeltaY = bigshot1SecondPosition.Y - firstPosition.Y;
                            double bigshot1DeltaX = bigshot1SecondPosition.X - firstPosition.X;

                            double angleInRadians1 = Math.Atan2(bigshot1DeltaY, bigshot1DeltaX);
                            angleInRadians1+= 90 * (MathF.PI / 180);
                            float time = dist / velocity;

                            Vector2 firstBuner = remiliaPosition0+energy1Pos;
                            Vector2 lastBuner = bigshot1NextPosition;

                            // Calcula a direção normalizada entre os pontos
                            Vector2 direction = Vector2.Normalize(lastBuner - firstBuner);

                            // Lista para armazenar as posições geradas
                            List<Vector2> positions = new List<Vector2>();

                            // Adiciona o ponto inicial
                            positions.Add(firstBuner);

                            // Gera os pontos ao longo da linha, com a distância desejada
                            Vector2 currentPoint = firstBuner;
                            while (CalculateDistance(currentPoint, lastBuner) >= desiredDistance)
                            {
                                // Calcula o próximo ponto, avançando na direção
                                currentPoint += direction * desiredDistance;
                                positions.Add(currentPoint);
                            }

                            // Adiciona o ponto final, se necessário
                            if (CalculateDistance(positions[^1], lastBuner) > 0)
                            {
                                positions.Add(lastBuner);
                            }

                            var l = 0;
                            var AfterBunerRuntime = time/positions.Count;
                            foreach (var pos in positions)
                            {
                                var afterbuner = ShotLayer.CreateAnimation(AfterBunerAnimation, 35, 20, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                                var newRandom = new Vector2(random.Next(-15,15), random.Next(-15,15));
                                if(700+remiliaActionStartTime+l*AfterBunerRuntime > remiliaActionEndTime){
                                    afterbuner.Move(remiliaActionStartTime+l*AfterBunerRuntime, remiliaActionEndTime, pos+newRandom, pos+newRandom + new Vector2(clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(remiliaActionStartTime+l*AfterBunerRuntime).X, 0));
                                }else{
                                    afterbuner.Move(remiliaActionStartTime+l*AfterBunerRuntime, 700+remiliaActionStartTime+l*AfterBunerRuntime, pos+newRandom, pos+newRandom+ new Vector2(clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(remiliaActionEndTime-700).X, 0));
                                }
                                afterbuner.Fade(remiliaActionStartTime+l*AfterBunerRuntime, 1);
                                afterbuner.Fade(700+remiliaActionStartTime+l*AfterBunerRuntime, 0);
                                l+=1;
                            }

                            var bigshot1Start = BigShotLayer.CreateAnimation(BigShotAnimation, 10, 60, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                            var bigshot1Loop = BigShotLayer.CreateAnimation(BigShotAnimation2, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);
                            
                            bigshot1Start.Move((int)t0, (int)t0+time, remiliaPosition0+energy1Pos, bigshot1NextPosition);
                            bigshot1Loop.Move((int)t0, (int)t0+time, remiliaPosition0+energy1Pos, bigshot1NextPosition);

                            bigshot1Start.Fade((int)t0, 1);
                            bigshot1Loop.Fade((int)t0, 0);

                            if(time>600){
                                bigshot1Start.Fade(t0+600, 0);
                                bigshot1Loop.Fade(t0+600, 1);
                                bigshot1Loop.Fade(t0+time, 0);
                            }else{
                                bigshot1Start.Fade((int)t0+time, 0);
                            }

                            var circle = ShotLayer.CreateSprite(Circle, OsbOrigin.Centre);
                            var timeCircle = remiliaActionStartTime+time;
                            circle.Fade(OsbEasing.InQuad, timeCircle, timeCircle+300, 1, 0);
                            circle.Scale(OsbEasing.OutCirc, timeCircle, timeCircle+300, 0, 0.5f);
                            circle.Color(timeCircle, Color4.Red);
                            circle.MoveY(timeCircle, bigshot1NextPosition.Y);

                            if(timeCircle+300 > remiliaActionEndTime){
                                circle.MoveX(timeCircle, remiliaActionEndTime, bigshot1NextPosition.X, bigshot1NextPosition.X+clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(timeCircle).X);
                            }else{
                                circle.MoveX(timeCircle, timeCircle+300, bigshot1NextPosition.X, bigshot1NextPosition.X+clockmap.PositionAt(timeCircle+300).X-clockmap.PositionAt(timeCircle).X);
                            }
                            
                            bigshot1Start.Rotate(remiliaActionStartTime, angleInRadians1);
                            bigshot1Loop.Rotate(remiliaActionStartTime, angleInRadians1);

                        }
                        if(i<=35){

                            energy1.Move((int)t0, (int)t1, remiliaPosition0+energy1Pos, remiliaPosition1+energy1Pos);
                            energy2.Move((int)t0, (int)t1, remiliaPosition0+energy2Pos, remiliaPosition1+energy2Pos);
                            energy3.Move((int)t0, (int)t1, remiliaPosition0+energy3Pos, remiliaPosition1+energy3Pos);
                        }
                        if(i==49){

                            bigshot2NextPosition = firstPosition+new Vector2(sakuyaRun.PositionAt(remiliaActionStartTime+350).X-sakuyaRun.PositionAt(remiliaActionStartTime).X, 0);
                            bigshot3NextPosition = firstPosition+new Vector2(sakuyaRun.PositionAt(remiliaActionStartTime+700).X-sakuyaRun.PositionAt(remiliaActionStartTime+350).X, 0);
                            
                            var bigshot2FirstPosition = (Vector2)remilia.PositionAt(remiliaActionStartTime+350)+energy2Pos;
                            var bigshot3FirstPosition = (Vector2)remilia.PositionAt(remiliaActionStartTime+700)+energy3Pos;
                            
                            double bigshot2DeltaY = bigshot2FirstPosition.Y - sakuyaRun.PositionAt(remiliaActionStartTime+350).Y;
                            double bigshot2DeltaX = bigshot2FirstPosition.X - sakuyaRun.PositionAt(remiliaActionStartTime+350).X;
  
                            double bigshot3DeltaY = bigshot3FirstPosition.Y - sakuyaRun.PositionAt(remiliaActionStartTime+700).Y;
                            double bigshot3DeltaX = bigshot3FirstPosition.X - sakuyaRun.PositionAt(remiliaActionStartTime+700).X;

                            double angleInRadians2 = Math.Atan2(bigshot2DeltaY, bigshot2DeltaX);
                            angleInRadians2+= 90 * (MathF.PI / 180);

                            double angleInRadians3 = Math.Atan2(bigshot3DeltaY, bigshot3DeltaX);
                            angleInRadians3+= 90 * (MathF.PI / 180);
                            
                            
                            float dist2 = CalculateDistance(bigshot2FirstPosition, bigshot2NextPosition);
                            float dist3 = CalculateDistance(bigshot3FirstPosition, bigshot3NextPosition);

                            float time2 = dist2 / velocity;
                            float time3 = dist3 / velocity;

                            Vector2 firstBuner2 = bigshot2FirstPosition;
                            Vector2 lastBuner2 = bigshot2NextPosition;

                            // Calcula a direção normalizada entre os pontos
                            Vector2 direction2 = Vector2.Normalize(lastBuner2 - firstBuner2);

                            // Lista para armazenar as posições geradas
                            List<Vector2> positions2 = new List<Vector2>();

                            // Adiciona o ponto inicial
                            positions2.Add(firstBuner2);

                            // Gera os pontos ao longo da linha, com a distância desejada
                            Vector2 currentPoint2 = firstBuner2;
                            while (CalculateDistance(currentPoint2, lastBuner2) >= desiredDistance)
                            {
                                // Calcula o próximo ponto, avançando na direção
                                currentPoint2 += direction2 * desiredDistance;
                                positions2.Add(currentPoint2);
                            }

                            // Adiciona o ponto final, se necessário
                            if (CalculateDistance(positions2[^1], lastBuner2) > 0)
                            {
                                positions2.Add(lastBuner2);
                            }

                            var l = 0;
                            var AfterBunerRuntime2 = time2/positions2.Count;
                            foreach (var pos2 in positions2)
                            {
                                var afterbuner2 = ShotLayer.CreateAnimation(AfterBunerAnimation, 35, 20, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                                var newRandom2 = new Vector2(random.Next(-15,15), random.Next(-15,15));
                                if(700+350+remiliaActionStartTime+l*AfterBunerRuntime2 > remiliaActionEndTime){
                                    afterbuner2.Move(350+remiliaActionStartTime+l*AfterBunerRuntime2, remiliaActionEndTime, pos2+newRandom2, pos2+newRandom2 + new Vector2(clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(350+remiliaActionStartTime+l*AfterBunerRuntime2).X, 0));
                                }else{
                                    afterbuner2.Move(350+remiliaActionStartTime+l*AfterBunerRuntime2, 350+700+remiliaActionStartTime+l*AfterBunerRuntime2, pos2+newRandom2, pos2+newRandom2+ new Vector2(clockmap.PositionAt(remiliaActionStartTime+700).X-clockmap.PositionAt(remiliaActionStartTime).X, 0));
                                }
                                afterbuner2.Fade(350+remiliaActionStartTime+l*AfterBunerRuntime2, 1);
                                afterbuner2.Fade(350+700+remiliaActionStartTime+l*AfterBunerRuntime2, 0);
                                l+=1;
                            }

                            Vector2 firstBuner3 = bigshot3FirstPosition;
                            Vector2 lastBuner3 = bigshot3NextPosition;

                            // Calcula a direção normalizada entre os pontos
                            Vector2 direction3 = Vector2.Normalize(lastBuner3 - firstBuner3);

                            // Lista para armazenar as posições geradas
                            List<Vector2> positions3 = new List<Vector2>();

                            // Adiciona o ponto inicial
                            positions2.Add(firstBuner3);

                            // Gera os pontos ao longo da linha, com a distância desejada
                            Vector2 currentPoint3 = firstBuner3;
                            while (CalculateDistance(currentPoint3, lastBuner3) >= desiredDistance)
                            {
                                // Calcula o próximo ponto, avançando na direção
                                currentPoint3 += direction3 * desiredDistance;
                                positions3.Add(currentPoint3);
                            }

                            // Adiciona o ponto final, se necessário
                            if (CalculateDistance(positions3[^1], lastBuner3) > 0)
                            {
                                positions3.Add(lastBuner3);
                            }

                            l = 0;
                            var AfterBunerRuntime3 = time3/positions3.Count;
                            foreach (var pos3 in positions3)
                            {
                                var afterbuner3 = ShotLayer.CreateAnimation(AfterBunerAnimation, 35, 20, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                                var newRandom3 = new Vector2(random.Next(-15,15), random.Next(-15,15));
                                if(700+700+remiliaActionStartTime+l*AfterBunerRuntime3 > remiliaActionEndTime){
                                    afterbuner3.Move(700+remiliaActionStartTime+l*AfterBunerRuntime3, remiliaActionEndTime, pos3+newRandom3, pos3+newRandom3 + new Vector2(clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(700+remiliaActionStartTime+l*AfterBunerRuntime3).X, 0));
                                }else{
                                    afterbuner3.Move(700+remiliaActionStartTime+l*AfterBunerRuntime3, 700+700+remiliaActionStartTime+l*AfterBunerRuntime3, pos3+newRandom3, pos3+newRandom3+ new Vector2(clockmap.PositionAt(remiliaActionStartTime+700).X-clockmap.PositionAt(remiliaActionStartTime).X, 0));
                                }
                                afterbuner3.Fade(700+remiliaActionStartTime+l*AfterBunerRuntime3, 1);
                                afterbuner3.Fade(700+700+remiliaActionStartTime+l*AfterBunerRuntime3, 0);
                                l+=1;
                            }

                            var bigshot3Start = BigShotLayer.CreateAnimation(BigShotAnimation, 10, 60, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                            var bigshot2Start = BigShotLayer.CreateAnimation(BigShotAnimation, 10, 60, OsbLoopType.LoopOnce, OsbOrigin.Centre);
                            bigshot2Start.Rotate(remiliaActionStartTime+350, angleInRadians2);
                            bigshot3Start.Rotate(remiliaActionStartTime+700, angleInRadians3);
                            bigshot2Start.Fade(remiliaActionStartTime+350, 1);
                            bigshot3Start.Fade(remiliaActionStartTime+700, 1);

                            var circle2 = ShotLayer.CreateSprite(Circle, OsbOrigin.Centre);
                            var timeCircle2 = remiliaActionStartTime+350+time2;
                            circle2.Fade(OsbEasing.InQuad, timeCircle2, timeCircle2+300, 1, 0);
                            circle2.Scale(OsbEasing.OutCirc, timeCircle2, timeCircle2+300, 0, 0.5f);
                            circle2.Color(timeCircle2, Color4.Red);
                            circle2.MoveY(timeCircle2, bigshot2NextPosition.Y);

                            var circle3 = ShotLayer.CreateSprite(Circle, OsbOrigin.Centre);
                            var timeCircle3 = remiliaActionStartTime+700+time3;
                            circle3.Fade(OsbEasing.InQuad, timeCircle3, timeCircle3+300, 1, 0);
                            circle3.Scale(OsbEasing.OutCirc, timeCircle3, timeCircle3+300, 0, 0.5f);
                            circle3.Color(timeCircle3, Color4.Red);
                            circle3.MoveY(timeCircle3, bigshot3NextPosition.Y);
                            
                            if(time2>600){ // entrou em loop
                                bigshot2Start.Fade(remiliaActionStartTime+350+600, 0);

                                var bigshot2Loop = BigShotLayer.CreateAnimation(BigShotAnimation2, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);

                                bigshot2Loop.Rotate(remiliaActionStartTime+350+600, angleInRadians2);
                                bigshot2Loop.Move(remiliaActionStartTime+350, remiliaActionStartTime+350+time2, bigshot2FirstPosition, bigshot2NextPosition);
                                bigshot2Loop.Fade(remiliaActionStartTime+350, 0);
                                bigshot2Loop.Fade(remiliaActionStartTime+350+600, 1);
                                bigshot2Loop.Fade(timeCircle2, 0);
                            }else{ // bateu antes do loop
                                bigshot2Start.Fade(timeCircle2, 0);
                            }
                            if(timeCircle2+300 > remiliaActionEndTime){
                                circle2.MoveX(timeCircle2, remiliaActionEndTime, bigshot2NextPosition.X, bigshot2NextPosition.X+clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(timeCircle2).X);
                            }else{
                                circle2.MoveX(timeCircle2, timeCircle2+300, bigshot2NextPosition.X, bigshot2NextPosition.X+clockmap.PositionAt(timeCircle2+300).X-clockmap.PositionAt(timeCircle2).X);
                            }

                            if(timeCircle3+300 > remiliaActionEndTime){
                                circle3.MoveX(timeCircle3, remiliaActionEndTime, bigshot3NextPosition.X, bigshot3NextPosition.X+clockmap.PositionAt(remiliaActionEndTime).X-clockmap.PositionAt(timeCircle3).X);
                            }else{
                                circle3.MoveX(timeCircle3, timeCircle3+300, bigshot3NextPosition.X, bigshot3NextPosition.X+clockmap.PositionAt(timeCircle3+300).X-clockmap.PositionAt(timeCircle3).X);
                            }

                            if(time3>600){
                                bigshot3Start.Fade(remiliaActionStartTime+700+600, 0);

                                var bigshot3Loop = BigShotLayer.CreateAnimation(BigShotAnimation2, 4, 60, OsbLoopType.LoopForever, OsbOrigin.Centre);

                                bigshot3Loop.Rotate(remiliaActionStartTime+700+600, angleInRadians3);
                                bigshot3Loop.Fade(timeCircle3, 0);
                                bigshot3Loop.Fade(remiliaActionStartTime+700+600, 1);
                                bigshot3Loop.Fade(remiliaActionStartTime+700, 0);
                                bigshot3Loop.Move(remiliaActionStartTime+700, remiliaActionStartTime+700+time3, bigshot3FirstPosition, bigshot3NextPosition);
                            }else{
                                bigshot3Start.Fade(remiliaActionStartTime+700+time3, 0);
                            }

                            bigshot2Start.Move(remiliaActionStartTime+350, remiliaActionStartTime+350+time2, bigshot2FirstPosition, bigshot2NextPosition);
                            bigshot3Start.Move(remiliaActionStartTime+700, remiliaActionStartTime+700+time3, bigshot3FirstPosition, bigshot3NextPosition);
                        }
                    }
                }
                // Atualiza a posição atual da Remilia
                remiliaCurrentPosition.Y = 140f;
                remiliaCurrentPosition.X = remiliaPosition1.X;

                AltMove += 1;
            }
            // Escrevendo os dados em um arquivo JSON
            string json = JsonConvert.SerializeObject(TimeList, Formatting.Indented);
            File.WriteAllText("projects/Bloody Devotion/time/times.json", json);

            //    _____ _           _______ _                
            //   / ____| |         |__   __(_)               
            //  | (___ | |_ ___  _ __ | |   _ _ __ ___   ___ 
            //   \___ \| __/ _ \| '_ \| |  | | '_ ` _ \ / _ \
            //   ____) | || (_) | |_) | |  | | | | | | |  __/
            //  |_____/ \__\___/| .__/|_|  |_|_| |_| |_|\___|
            //                  | |                          
            //                  |_|                          

            magical.Rotate(216979 ,currentTime, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(3600));

            lamp1Static.Fade(currentTime, 1);
            lamp1Static.Fade(EndTime-822, 0);
            lamp1Static.Move(currentTime, lamp1.PositionAt(currentTime));
            lamp1Static.Scale(currentTime, 1.2*scaleMap);
            lamp1.Fade(currentTime, 0);
            lamp1.Fade(EndTime-822, 1);

            lamp2Static.Fade(currentTime, 1);
            lamp2Static.Fade(EndTime-822, 0);
            lamp2Static.Move(currentTime, lamp2.PositionAt(currentTime));
            lamp2Static.Scale(currentTime, 1.2*scaleMap);
            lamp2.Fade(currentTime, 0);
            lamp2.Fade(EndTime-822, 1);

            lamp3Static.Fade(currentTime, 1);
            lamp3Static.Fade(EndTime-822, 0);
            lamp3Static.Move(currentTime, lamp3.PositionAt(currentTime));
            lamp3Static.Scale(currentTime, 1.2*scaleMap);
            lamp3.Fade(currentTime, 0);
            lamp3.Fade(EndTime-822, 1);

            lamp4Static.Fade(currentTime, 1);
            lamp4Static.Fade(EndTime-822, 0);
            lamp4Static.Move(currentTime, lamp4.PositionAt(currentTime));
            lamp4Static.Scale(currentTime, 1.2*scaleMap);
            lamp4.Fade(currentTime, 0);
            lamp4.Fade(EndTime-822, 1);

            var remilia2 = PlayerLayer.CreateSprite(RemiliaStopped, OsbOrigin.Centre, remiliaCurrentPosition);
            remilia.Fade(currentTime, 0);
            remilia.Fade(EndTime-822, 1);
            remilia2.Scale(currentTime, 1.2);
            remilia2.Fade(currentTime, 1);
            remilia2.Fade(EndTime-411, 1);
            remilia2.Fade(EndTime-822, 0);
            remilia2.Color(currentTime, Color4.White);
            remilia2.Color(EndTime-411, Color4.Red);
            remilia2.Color(EndTime-359, Color4.Black);
            remilia2.Color(EndTime-308, Color4.Red);
            remilia2.Color(EndTime-257, Color4.Black);

            var flashStopTime = LampLayer.CreateSprite(FlashImage, OsbOrigin.Centre);

            flashStopTime.Fade(currentTime, 0.5);
            flashStopTime.Color(currentTime, Color4.Blue);
            flashStopTime.Fade(EndTime-822, 0);
            flashStopTime.Additive(currentTime);
            flashStopTime.Scale(currentTime, 0.65);

            var circleBig = PlayerLayer.CreateSprite(CircleBig, OsbOrigin.Centre);

            circleBig.Scale(OsbEasing.Out, currentTime, currentTime+1200, 2.7, 0);
            circleBig.Color(currentTime, Color4.Red);
            circleBig.Move(currentTime, currentPosition + new Vector2(15, -80));
            circleBig.Fade(currentTime, 0.7);

            var sakuyaStopTime = LampLayer.CreateAnimation(SakuyaStopTime, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, currentPosition);

            sakuyaStopTime.Fade(currentTime, 1);
            sakuyaStopTime.Fade(currentTime+1200, 0);
            sakuyaStopTime.Scale(currentTime, 1.2);

            var sakuyaRunStart2 = LampLayer.CreateAnimation(SakuyaSpriteRunStart, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaRun2 = LampLayer.CreateAnimation(SakuyaSpriteRun, 16, 30, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
            var sakuyaStop2 = LampLayer.CreateAnimation(SakuyaSpriteStop, 20, 50, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
            var sakuyaRunBack2 = LampLayer.CreateAnimation(SakuyaSpriteRunBack, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaRunBack3 = LampLayer.CreateAnimation(SakuyaSpriteRunBack, 9, 35, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaRunStop2 = LampLayer.CreateAnimation(SakuyaSpriteRunStop, 5, 63, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaRunStop3 = LampLayer.CreateAnimation(SakuyaSpriteRunStop, 5, 63, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaStop3 = LampLayer.CreateAnimation(SakuyaSpriteStop, 20, 50, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            var sakuyaJumpOne = LampLayer.CreateAnimation(SakuyaJump1, 2, 60, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaJumpTwo = LampLayer.CreateAnimation(SakuyaJump2, 6, 60, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);
            var sakuyaJumpAttack = LampLayer.CreateAnimation(SakuyaJumpAttack, 16, 60, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);
            StartMoving(currentTime+1200, 600, currentPosition, new Vector2(0, 396), isFacingRight, sakuyaRunStart2, sakuyaRun2, sakuyaStop2, sakuyaRunStop2, sakuyaRunBack2, false);
            TurnBack(currentTime+1800+315, new Vector2(0, 396), true, sakuyaRunBack3);
            StopMoving(currentTime+1800+315+315, 242,  new Vector2(55, 396), true, sakuyaRunStop3, sakuyaStop3, sakuyaRun2, sakuyaRunStart2, sakuyaRunBack2);

            sakuyaJumpOne.Scale(260541, 1.2);
            sakuyaJumpOne.MoveX(260541, 55);
            sakuyaJumpOne.MoveY(OsbEasing.Out, 260541, 260746, 396, 345);

            sakuyaJumpTwo.Scale(260746, 1.2);
            sakuyaJumpTwo.MoveX(260746, 55);
            sakuyaJumpTwo.MoveY(OsbEasing.Out, 260746, 261157, 345, 225);

            sakuyaJumpAttack.Scale(261157, 1.2);
            sakuyaJumpAttack.MoveX(261157, 55);
            sakuyaJumpAttack.MoveY(OsbEasing.Out, 261157, 262801, 225, 260);

            float opa = 0;
            for(double x = 261157;x < 262698; x+=80){
                var knife = LampLayer.CreateSprite(Knife, OsbOrigin.CentreLeft);
                var firstKnifePosition = new Vector2(50, 160.0f+opa);
                var lastKnifePosition = firstKnifePosition+new Vector2(50+random.Next(-40, 30), random.Next(-7, 7));
                knife.Move(OsbEasing.OutCirc, x, x+120, firstKnifePosition, lastKnifePosition);
                knife.Fade(x, 1);
                knife.Fade(263417, 0);
                knife.Rotate(x, MathHelper.DegreesToRadians(-5));
                opa+=5.1f;
                knife.Scale(x, 1.2);
                knife.Move(263006, 263417, lastKnifePosition.X, lastKnifePosition.Y, lastKnifePosition.X+540, lastKnifePosition.Y-60);
            }

            var sakuyaFalling = LampLayer.CreateAnimation(SakuyaFalling, 5, 70, OsbLoopType.LoopOnce, OsbOrigin.BottomCentre);

            sakuyaFalling.Scale(262801, 1.2);
            sakuyaFalling.MoveX(262801, 55);
            sakuyaFalling.MoveY(262801, 263212, 260, 404);

            var sakuyaDown = LampLayer.CreateAnimation(SakuyaDown, 4, 70, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            sakuyaDown.Scale(263212, 1.2);
            sakuyaDown.Move(263212, 55, 396);
            sakuyaDown.Fade(263417, 1);
            sakuyaDown.Fade(EndTime, 0);

            var circleBlack = LampLayer.CreateSprite(CircleBig, OsbOrigin.Centre);
            circleBlack.Fade(263417, EndTime, 0.3, 1);
            circleBlack.Scale(OsbEasing.In  ,263417, EndTime, 0, 3.3);
            circleBlack.Color(263417, Color4.Black);
            circleBlack.Move(263417, remiliaCurrentPosition);

            var flash = LampLayer.CreateSprite(FlashImage, OsbOrigin.Centre);

            flash.Color(StartTime, Color4.DarkRed);
            flash.Fade(OsbEasing.In, StartTime, StartTime+1300, 0.7, 0);

            for(int x = -107; x<=780;x+=40){
                for(int y=0;y<=500;y+=40){
                    var pixel = GetLayer("Squares").CreateSprite(Pixel,OsbOrigin.Centre, new Vector2(x, y));
                    pixel.Color(StartTime, Color4.Crimson);
                    pixel.Fade(OsbEasing.None, StartTime, StartTime+1100, 1, 0);
                    pixel.Scale(OsbEasing.OutCubic, StartTime, StartTime+1100, 20, 0);
                    pixel.Rotate(OsbEasing.OutCubic, StartTime, StartTime+1100, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(90));
                }
            }    
        }

        //   ______                _   _                 
        //  |  ____|              | | (_)                
        //  | |__ _   _ _ __   ___| |_ _  ___  _ __  ___ 
        //  |  __| | | | '_ \ / __| __| |/ _ \| '_ \/ __|
        //  | |  | |_| | | | | (__| |_| | (_) | | | \__ \
        //  |_|   \__,_|_| |_|\___|\__|_|\___/|_| |_|___/

        static float CalculateDistance(Vector2 start, Vector2 end)
        {
            float deltaX = end.X - start.X;
            float deltaY = end.Y - start.Y;
            return MathF.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }

        private void StartMoving(int startTime, int duration, Vector2 startPosition, Vector2 endPosition, bool facingRight,
            OsbSprite sakuyaRunStart, OsbSprite sakuyaRun, OsbSprite sakuyaStop, OsbSprite sakuyaRunStop, OsbSprite sakuyaRunBack, bool needsBack){
            // Configura a direção
            if(!facingRight){
                sakuyaRun.FlipH(startTime+315);
                sakuyaRunStart.FlipH(startTime, startTime + 315);
            }
            var pixel = GetLayer("").CreateSprite(Pixel, OsbOrigin.Centre);
            pixel.Move(startTime, startTime + 315 + duration, startPosition, endPosition);
            pixel.Fade(startTime, 0);

            // Desativa outras animações
            sakuyaStop.Fade(startTime, 0);
            sakuyaRunStop.Fade(startTime, 0);

            if(needsBack){
                sakuyaRunStart.Fade(startTime, 0);
                sakuyaRunBack.Fade(startTime+315, 0);
                // Animação de corrida
                sakuyaRun.Move(startTime, startTime + 315 + duration, startPosition, endPosition);
                sakuyaRun.Scale(startTime + 315, 1.2);
                sakuyaRun.Fade(startTime + 315, 1);
                sakuyaRun.Fade(startTime + 315 + duration, 0);
            }else{
                var sakuyaRunStartPosition = pixel.PositionAt(startTime+315);
                sakuyaRunBack.Fade(startTime, 0);
                // Animação de corrida
                sakuyaRun.Move(startTime+315, startTime + 315 + duration, sakuyaRunStartPosition, endPosition);
                sakuyaRun.Scale(startTime + 315, 1.2);
                //sakuyaRun.Fade(startTime, 0);
                sakuyaRun.Fade(startTime + 315, 1);
                sakuyaRun.Fade(startTime + 315 + duration, 0);

                // Animação de início da corrida
                sakuyaRunStart.Move(startTime, startTime+315, startPosition, sakuyaRunStartPosition);
                sakuyaRunStart.Scale(startTime, 1.2);
                sakuyaRunStart.Scale(startTime, 1.2);
                sakuyaRunStart.Fade(startTime, 1);
                sakuyaRunStart.Fade(startTime + 315, 0); // 4 frames * 80ms             
            }         
        }                                                                        

        private void StopMoving(int startTime, int duration, Vector2 position, bool facingRight,
            OsbSprite sakuyaRunStop, OsbSprite sakuyaStop, OsbSprite sakuyaRun, OsbSprite sakuyaRunStart, OsbSprite sakuyaRunBack)
        {
            // Configura a direção
            if(!facingRight){
                sakuyaRunStop.FlipH(startTime, startTime + 315);
                sakuyaStop.FlipH(startTime + 315, startTime + 315 + duration);
            }
            
            // Desativa outras animações
            sakuyaRun.Fade(startTime, 0);
            sakuyaRunStart.Fade(startTime, 0);
            sakuyaRunBack.Fade(startTime, 0);

            // Animação de parada da corrida
            sakuyaRunStop.Move(startTime, position);
            sakuyaRunStop.Scale(startTime, 1.2);
            sakuyaRunStop.Fade(startTime, 1); // start frame
            sakuyaRunStop.Fade(startTime + 315, 0); // 4 frames * 80ms

            // Animação de parada
            sakuyaStop.Move(startTime + 315, position);
            sakuyaStop.Scale(startTime + 315, 1.2);
            sakuyaStop.Fade(startTime + 315, 1);
            sakuyaStop.Fade(startTime + 315 + duration, 0);
        }

        private void TurnBack(int startTime, Vector2 position, bool facingRight, OsbSprite sakuyaRunBack)
        {
            // Configura a direção
            if(!facingRight){
                sakuyaRunBack.FlipH(startTime, startTime + 315);
                sakuyaRunBack.Move(startTime, startTime+315, position, position - new Vector2(50, 0));
            }else{
                sakuyaRunBack.Move(startTime, startTime+315, position, position + new Vector2(50, 0));
            }

            // Desativa outras animações
            sakuyaRunBack.Fade(startTime, 1);
            sakuyaRunBack.Scale(startTime, 1.2);
            sakuyaRunBack.Fade(startTime + 315, 0);

            
        }
    }
}
