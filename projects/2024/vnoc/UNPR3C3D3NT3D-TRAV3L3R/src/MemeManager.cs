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

namespace StorybrewScripts;

public class MemeManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        AddHoaqPog();
        AddNeuronActivation();
    }

    private void AddHoaqPog()
    {
        OsbSprite sprite = GetLayer("hoaq-pog").CreateSprite("sb/m/hoaqpog.png");

        sprite.Scale(10285, 1.2);
        sprite.Fade(OsbEasing.InCirc, 10285, 12571, 0, 0.25);
    }

    private void AddNeuronActivation()
    {
        double beatDuration = GetBeatDuration(0);
        OsbSprite sprite = GetLayer("neuron-activation").CreateSprite("sb/m/monkey.jpg", OsbOrigin.Centre, new Vector2(240, 340));

        sprite.Scale(304579, 0.8);
        sprite.Fade(OsbEasing.OutCirc, 304579, 304579 + beatDuration * 4, 0, 0.05);
        sprite.Fade(OsbEasing.InCirc, 313722 - beatDuration * 4, 313722, 0.05, 0);
    }

    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;
}