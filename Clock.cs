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
    public class Clock : StoryboardObjectGenerator
    {
        [Configurable] public string BigClock = "";
        [Configurable] public string BigHand = "";
        [Configurable] public string SmallHand = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        [Configurable] public string MidCircle = "";
        [Configurable] public string Steam1 = "";
        [Configurable] public string Steam2 = "";
        [Configurable] public string Steam3 = "";
        [Configurable] public string Steam4 = "";
        public override void Generate()
        {
		    var clockSprite = GetLayer("Clock").CreateSprite(BigClock, OsbOrigin.Centre);

            clockSprite.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.2, 0.14);
            clockSprite.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);
            clockSprite.Fade(EndTime, 0);
            clockSprite.Color(StartTime, Color4.Red);

            var SmallHandSprite = GetLayer("Clock").CreateSprite(SmallHand, OsbOrigin.BottomCentre);
            var BigHandSprite = GetLayer("Clock").CreateSprite(BigHand, OsbOrigin.BottomCentre);

            SmallHandSprite.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.4, 0.28);
            SmallHandSprite.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);
            SmallHandSprite.Fade(EndTime, 0);
            SmallHandSprite.Color(StartTime, Color4.Red);
            
            BigHandSprite.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.4, 0.28);
            BigHandSprite.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);
            BigHandSprite.Fade(EndTime, 0);
            BigHandSprite.Color(StartTime, Color4.Crimson);

            int[] tempos = new int[] { 13966, 14172, 14377, 14583, 14788, 14994, 15199, 15302, 15405, 15610, 15815, 16021, 16226, 16432, 16637, 16843, 17048, 17459, 17665, 17870, 18076, 18281, 18487, 18589, 18692, 18898, 19103, 19309, 19514, 19720, 19925, 20131, 20233, 20336, 20439, 20542};
            for (int i = 0; i < tempos.Length-1; i++){
                BigHandSprite.Rotate(OsbEasing.OutExpo, tempos[i], tempos[i+1], MathHelper.DegreesToRadians(i*30), MathHelper.DegreesToRadians(i*30+30));   
            }

            var dotSprite = GetLayer("Clock").CreateSprite(MidCircle, OsbOrigin.Centre);

            dotSprite.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.6, 0.42);
            dotSprite.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);
            dotSprite.Fade(EndTime, 0);
            dotSprite.Color(StartTime, Color4.Crimson);

            dotSprite.Scale(OsbEasing.InExpo, 20131, EndTime, 0.42, 0.84);
            BigHandSprite.Scale(OsbEasing.InExpo, 20131, EndTime, 0.28, 0.84);
            SmallHandSprite.Scale(OsbEasing.InExpo, 20131, EndTime, 0.28, 0.84);
            clockSprite.Scale(OsbEasing.InExpo, 20131, EndTime, 0.14, 0.42);




            var steam1 = GetLayer("Clock").CreateSprite(Steam1, OsbOrigin.Centre, new Vector2(0, 100));

            steam1.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.2, 0.14);
            steam1.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);


            var steam2 = GetLayer("Clock").CreateSprite(Steam2, OsbOrigin.Centre, new Vector2(40, 500));

            steam2.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.6, 0.42);
            steam2.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);

            var steam3 = GetLayer("Clock").CreateSprite(Steam3, OsbOrigin.Centre, new Vector2(700, 400));

            steam3.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.6, 0.42);
            steam3.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);

            var steam4 = GetLayer("Clock").CreateSprite(Steam4, OsbOrigin.Centre, new Vector2(600, 0));

            steam4.Scale(OsbEasing.OutExpo, 13863, StartTime, 0.4, 0.28);
            steam4.Fade(OsbEasing.OutExpo, 13863, StartTime, 0, 1);

            int j = 0;
            int k = 0;
            List<int> numeros = new List<int>();
            for (int i = 14172; i <= EndTime; i+=412){
                numeros.Add(i);
                steam1.MoveX(OsbEasing.OutExpo, i, i+412, k*8, ((k+1)*8));
                steam2.MoveY(OsbEasing.OutExpo, i, i+412, 500-k*8, 500-((k+1)*8));
                steam3.MoveX(OsbEasing.OutExpo, i, i+412, 700-k*11, 700-((k+1)*11));
                steam4.MoveY(OsbEasing.OutExpo, i, i+412, k*9, ((k+1)*9));

                if(i==19940){
                    steam1.Fade(20131, 0);
                    steam2.Fade(20233, 0);
                    steam3.Fade(20439, 0);
                    steam4.Fade(20336, 0);
                    steam1.Rotate(OsbEasing.OutExpo, i, i+412, MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                }else if(i>19940){
                }
                else{
                    steam1.Rotate(OsbEasing.OutExpo, i, i+412, MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                }
                steam2.Rotate(OsbEasing.OutExpo, i, i+412, MathHelper.DegreesToRadians(-j), MathHelper.DegreesToRadians(-j-30));
                steam3.Rotate(OsbEasing.OutExpo, i, i+412, MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                steam4.Rotate(OsbEasing.OutExpo, i, i+412, MathHelper.DegreesToRadians(-j), MathHelper.DegreesToRadians(-j-30));
                j+=30;
                k+=1;
            }
            numeros.AddRange(new List<int> {19617, 20233, 20439, 20542});
            numeros.Sort();
            j=0;
            for (int i = 0; i < numeros.Count-1; i++)
            {
                SmallHandSprite.Rotate(OsbEasing.OutExpo, numeros[i], numeros[i+1], MathHelper.DegreesToRadians(j), MathHelper.DegreesToRadians(j+30));
                j+=30;
            }
        }
    }
}
