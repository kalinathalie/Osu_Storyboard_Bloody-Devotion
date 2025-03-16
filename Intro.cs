using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using System;
using System.Linq;

namespace StorybrewScripts
{
    public class Intro : StoryboardObjectGenerator
    {
        [Group("Timing")]
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        
        [Group("Sprites")]
        [Configurable] public string BGBlur = "";
        [Configurable] public double Opacity = 0.2;
        [Configurable] public string Akatsuki = "";
        [Configurable] public string Black = "";
        [Configurable] public string Vig = "";
        //[Configurable] public string SakuyaIntro = "";
        [Configurable] public string SakuyaImage = "";
        [Configurable] public string DotMask = "";

        public override void Generate()
        {

            var redPoints = GetLayer("").CreateSprite(DotMask, OsbOrigin.Centre);

            redPoints.Scale(815, 0.6);
            redPoints.Rotate(815, MathHelper.DegreesToRadians(-8));
            redPoints.Move(815, 5747, 190, 230, 490, 200);
            redPoints.Fade((OsbEasing) 10, 815, 2459, 0, 1);
            redPoints.Fade(5747, 0);

            /*var sakuya = GetLayer("").CreateSprite(SakuyaIntro, OsbOrigin.Centre);

            sakuya.Scale(815, 0.37);
            sakuya.Rotate(815, MathHelper.DegreesToRadians(-8));
            sakuya.Move(815, 5747, 0, 280, 300, 250);
            sakuya.Fade((OsbEasing) 10, 815, 2459, 0, 1);
            sakuya.Fade(5747, 0);*/

            var bitmap = GetMapsetBitmap(BGBlur);

            var bg = GetLayer("").CreateSprite(BGBlur, OsbOrigin.Centre);

            bg.Scale(7391, 480.0f / bitmap.Height);
            bg.Fade(5747, 0.2);
            bg.Fade(20542, 0);

            var sakuyaShadow = GetLayer("").CreateSprite(SakuyaImage, OsbOrigin.Centre);

            sakuyaShadow.Scale(815, 0.6);
            sakuyaShadow.Rotate(815, MathHelper.DegreesToRadians(-8));
            sakuyaShadow.Move(815, 5747, 80, 360, 380, 330);
            sakuyaShadow.Fade((OsbEasing) 10, 815, 2459, 0, 1);
            sakuyaShadow.Fade(5747, 0);
            sakuyaShadow.Color(815, Color4.Red);

            var sakuyaSprite = GetLayer("").CreateSprite(SakuyaImage, OsbOrigin.Centre);

            sakuyaSprite.Scale(815, 0.6);
            sakuyaSprite.Rotate(815, MathHelper.DegreesToRadians(-8));
            sakuyaSprite.Move(815, 5747, 40, 380, 340, 350);
            sakuyaSprite.Fade((OsbEasing) 10, 815, 2459, 0, 1);
            sakuyaSprite.Fade(5747, 0);

            var dim = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);

            dim.Color(815, Color4.Black);
            dim.Fade(815, 0.7);
            dim.Fade(5747, 0);


            var black1 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);
            var black2 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);

            black1.MoveY((OsbEasing)18, 6569, 7391, -20, -180);
            black1.Color(EndTime, Color4.Black);
            black1.Fade(5747, 1);
            black1.Fade(20542, 0);
            black1.Scale(EndTime, 0.7);

            black2.MoveY((OsbEasing)12, 6569, 7391, 510, 660);
            black2.Color(EndTime, Color4.Black);
            black2.Fade(5747, 1);
            black2.Fade(20542, 0);
            black2.Scale(EndTime, 0.7);

            black1.MoveY(OsbEasing.InCirc, 20131, 20542, -180, -20);
            black2.MoveY(OsbEasing.InCirc, 20131, 20542, 660, 510);

            var AkatsukiSprite = GetLayer("").CreateSprite(Akatsuki, OsbOrigin.Centre);

            AkatsukiSprite.Fade(EndTime, 1);
            AkatsukiSprite.Fade((OsbEasing) 12, 7391, 10268, 1, 0);
            AkatsukiSprite.Scale(EndTime, 0.5);
            AkatsukiSprite.MoveY(EndTime, 7391, 220, 230);

            var vig = GetLayer("").CreateSprite(Vig, OsbOrigin.Centre);

            vig.Scale(815, 0.45);
            vig.Fade(815, 1);
            vig.Fade(20542, 0);

            var block1 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);
            var block2 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);
            var block3 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);

            block1.Fade(5747, 1);
            block1.Fade(6158, 0);
            
            block1.ScaleVec(5747, 0.1, 0.23);
            block1.MoveX(5747, 155);
            block1.Rotate(5747, MathHelper.DegreesToRadians(16));
            block1.Color(5747, Color4.Black);

            block2.Fade(5747, 1);
            block2.Fade(6569, 0);

            block3.Fade(5747, 1);
            block3.Fade(6569, 0);

            block2.ScaleVec(5747, 0.25, 0.12);
            block2.Move(5747, 410, 190);
            block3.ScaleVec(5747, 0.015, 0.03);
            block3.Move(5747, 327, 242);

            block2.Color(5747, Color4.Black);
            block3.Color(5747, Color4.Black);
        }
    }
}
