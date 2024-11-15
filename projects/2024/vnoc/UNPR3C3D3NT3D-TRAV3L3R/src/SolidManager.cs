using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.CommandValues;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StorybrewScripts
{
    public class SolidManager : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            GenerateSolid("solid-1", 83428, 85714, "#121212");
            GenerateSolid("solid-1", 121142, 122285, Color4.White);
            GenerateSolid("solid-1", 137932, 139576, Color4.White);
            GenerateSolid("solid-1", 139576, 141220, Color4.Black);
            GenerateSolid("solid-1", 192179, 193779, Color4.Black);
            GenerateSolid("solid-1", 247436, 248579, "#121212");
            GenerateSolid("solid-1", 266865, 268007, Color4.Black);
            GenerateSolid("solid-1", 358207, 363365, Color4.Black);
        }

        private void GenerateSolid(string layer, double startTime, double endTime, CommandColor color)
        {
            OsbSprite sprite = GetLayer(layer).CreateSprite("sb/e/p.png");
            sprite.ScaleVec(startTime, 854, 480);
            sprite.Color(startTime, color);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);
        }
    }
}