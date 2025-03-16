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
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;

namespace StorybrewScripts
{
    public class SakuyaShadow : StoryboardObjectGenerator
    {
        [Configurable] public string Background = "";
        [Configurable] public string Black = "";
        [Configurable] public string ShadowImage = "";
        [Configurable] public string SakuyaTimeStop = "";
        [Configurable] public string SakuyaStand = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        [Configurable] public string FogImage = "";
        [Configurable] public string Circle = "";
        [Configurable] public string CircleBig = "";

        public override void Generate()
        {

            var fog = GetLayer("").CreateSprite(FogImage, OsbOrigin.Centre);

            fog.Scale(StartTime, 0.8);
            fog.Fade(StartTime, 1);
            fog.MoveX(StartTime, EndTime, 800, -300);
            fog.MoveX(EndTime, EndTime + 2465, -300, -300);
            fog.MoveY(StartTime, 200);

            var bg = GetLayer("").CreateSprite(Background, OsbOrigin.Centre);

            bg.Scale(StartTime, 0.5);
            bg.Fade(StartTime, 0.7);
            bg.Fade(EndTime+2465, 0);
            bg.MoveX(StartTime, StartTime + 7398, 180, 320);
            bg.MoveX(StartTime + 9864, EndTime, 320, 460);
            bg.MoveY(StartTime, 200);
            bg.Color(StartTime, Color4.White);
            bg.Color(EndTime, Color4.Purple);

            var sakuya = GetLayer("").CreateAnimation(ShadowImage, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre);

            sakuya.Scale(EndTime-2450, EndTime, 2, 1.5);
            sakuya.Fade(StartTime, StartTime + 1232, 0, 1);
            sakuya.MoveX(StartTime, StartTime + 7398, 380, 310);
            sakuya.MoveX(StartTime + 9864, EndTime, 310, 260);
            sakuya.MoveY(StartTime, 391);
            sakuya.Color(StartTime, Color4.Black);
            sakuya.Color(EndTime-2450, EndTime-500, Color4.Black, Color4.White);

            var circleBig = GetLayer("").CreateSprite(CircleBig, OsbOrigin.Centre);

            circleBig.Scale(OsbEasing.Out, EndTime, EndTime+1233, 2.7, 0);
            circleBig.Color(EndTime, Color4.Red);
            circleBig.Move(EndTime, new Vector2(265, 320) + new Vector2(15, -100));
            circleBig.Fade(EndTime, 0.7);
            circleBig.Additive(EndTime);

            var sakuyaStopTime = GetLayer("").CreateAnimation(SakuyaTimeStop, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(265, 391));

            sakuyaStopTime.Fade(EndTime, 1);
            sakuyaStopTime.Fade(EndTime+1233, 0);
            sakuyaStopTime.Scale(EndTime, 1.5);
            sakuyaStopTime.MoveY(EndTime, EndTime+1233, 391, 312);

            var sakuyaStandSprite = GetLayer("").CreateAnimation(SakuyaStand, 4, 80, OsbLoopType.LoopForever, OsbOrigin.BottomCentre, new Vector2(265, 311));

            sakuyaStandSprite.Fade(EndTime+1233, 1);
            sakuyaStandSprite.Fade(EndTime+1848, 0);
            sakuyaStandSprite.Scale(EndTime+1233, 1.5);

            var circle = GetLayer("").CreateSprite(Circle, OsbOrigin.Centre);

            circle.Scale(OsbEasing.OutCubic, StartTime, StartTime+408, 2.2, 0.51);
            circle.Color(StartTime, Color4.Black);
            circle.Move(StartTime, StartTime+1233, new Vector2(173, 170), new Vector2(195, 175));
            circle.Fade(OsbEasing.InCubic, StartTime, StartTime+1233, 1, 0);

            


            var sakuya1 = GetLayer("").CreateSprite("sb/sakuya/drop/2.png", OsbOrigin.Centre);

            sakuya1.Move(EndTime+1848, new Vector2(532, 391));
            sakuya1.Fade(EndTime+1848, 1);
            sakuya1.Fade(EndTime+2054, 0);
            sakuya1.Scale(EndTime+1848, 1.5);
            bg.Color(EndTime+1848, Color4.Red);

            var sakuya2 = GetLayer("").CreateSprite("sb/sakuya/drop/3.png", OsbOrigin.Centre);

            sakuya2.Move(EndTime+2054, new Vector2(310, 240));
            sakuya2.Fade(EndTime+2054, 1);
            sakuya2.Fade(EndTime+2260, 0);
            sakuya2.Scale(EndTime+2054, 1.5);
            bg.Color(EndTime+2054, Color4.Purple);

            var sakuya3 = GetLayer("").CreateSprite("sb/sakuya/drop/1.png", OsbOrigin.Centre);

            sakuya3.Move(EndTime+2260, new Vector2(88, 80));
            sakuya3.Fade(EndTime+2260, 1);
            sakuya3.Fade(EndTime+2465, 0);
            sakuya3.Scale(EndTime+2260, 1.5);
            bg.Color(EndTime+2260, Color4.Red);

            var black1 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);
            var black2 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);

            black1.MoveY(StartTime, -180);
            black1.MoveY(EndTime, EndTime+1233, -180, -80);
            black1.Color(StartTime, Color4.Black);
            black1.Fade(StartTime, 1);
            black1.Fade(EndTime+2465, 0);
            black1.Scale(StartTime, 0.7);
            black1.MoveX(StartTime, 320);

            black1.Rotate(StartTime, MathHelper.DegreesToRadians(0));
            black1.Rotate(EndTime+1848, MathHelper.DegreesToRadians(90));
            black1.MoveY(EndTime+1848, 100);

            black2.MoveY(StartTime, 660);
            black2.MoveY(EndTime, EndTime+1233, 660, 580);
            black2.Color(StartTime, Color4.Black);
            black2.Fade(StartTime, 1);
            black2.Fade(EndTime+2465, 0);
            black2.Scale(StartTime, 0.7);
            black2.MoveX(StartTime, 320);

            black2.Rotate(StartTime, MathHelper.DegreesToRadians(0));
            black2.Rotate(EndTime+1848, MathHelper.DegreesToRadians(90));
            black2.MoveY(EndTime+1848, 100);
            black2.Scale(EndTime+1848, 0.9);
            
            black1.MoveX(EndTime+1848, 870);
            black2.MoveX(EndTime+1848, 120);

            black1.MoveX(EndTime+2054, 650);
            black2.MoveX(EndTime+2054, -100);

            black1.MoveX(EndTime+2260, 500);
            black1.Scale(EndTime+2260, 0.9);
            black2.MoveX(EndTime+2260, -320);

            black1.MoveX(OsbEasing.InCirc, EndTime+2260, EndTime+2466, 500, 430);
            black2.MoveX(OsbEasing.InCirc, EndTime+2260, EndTime+2466, -320, -250);
        }
    }
}
