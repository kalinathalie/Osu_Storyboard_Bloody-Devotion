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
    public class PreKiai : StoryboardObjectGenerator
    {
        [Configurable] public string Background = "";
        [Configurable] public string Black = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        public override void Generate()
        {
		    var bg = GetLayer("").CreateSprite(Background, OsbOrigin.Centre);

            bg.Scale(StartTime, 0.5);
            bg.Fade(StartTime, 0.7);
            bg.Fade(EndTime, 0);
            bg.MoveX(StartTime, 60815, 100, 250);
            bg.MoveY(StartTime, 200);

            var black1 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);
            var black2 = GetLayer("").CreateSprite(Black, OsbOrigin.Centre);

            black1.MoveY(StartTime, -180);
            black1.Color(StartTime, Color4.Black);
            black1.Fade(StartTime, 1);
            black1.Fade(EndTime, 0);
            black1.Scale(StartTime, 0.7);

            black2.MoveY(StartTime, 660);
            black2.Color(StartTime, Color4.Black);
            black2.Fade(StartTime, 1);
            black2.Fade(EndTime, 0);
            black2.Scale(StartTime, 0.7);

            
        }
    }
}
