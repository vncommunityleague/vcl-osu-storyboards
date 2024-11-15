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

public class HitObjectHighlightManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        GenerateGlowWithRingHighlight(49142, 66285);
        GenerateGlowWithRingHighlight(67428, 83428);
        GenerateGlowWithRingHighlight(85714, 101714);

        GenerateRingHighlight(101714, 104000, false);
        GenerateSquareHighlight(103928, 112642, false);
        GenerateVerticalLineHighlight(113142, 121142, false);
        GenerateVerticalLineHighlight(154371, 167521);
        GenerateVerticalLineHighlight(248579, 257722, false);

        GenerateGlowHighlight(193779, 217366);
        GenerateGlowHighlight(218914, 229725);
        GenerateGlowHighlight(268007, 304007);
        GenerateGlowHighlight(322865, 341150);
    }

    private void GenerateGlowWithRingHighlight(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        using OsbSpritePool glowPool = new OsbSpritePool(GetLayer("glow-with-ring-highlight"), "sb/e/hl.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });
        using OsbSpritePool ringPool = new OsbSpritePool(GetLayer("glow-with-ring-highlight"), "sb/e/c.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });

        foreach (OsuHitObject hitObject in Beatmap.HitObjects)
        {
            if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
            {
                continue;
            }

            double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) + beatDuration * 4;

            OsbSprite glowSprite = glowPool.Get(hitObject.StartTime, endtime);
            OsbSprite ringSprite = ringPool.Get(hitObject.StartTime, endtime);

            glowSprite.Move(hitObject.StartTime, hitObject.Position);
            glowSprite.Scale(OsbEasing.OutCirc, hitObject.StartTime, hitObject.StartTime + beatDuration, 0.2, 0.7);
            glowSprite.Fade(hitObject.StartTime, endtime, 0.75, 0);
            glowSprite.Additive(hitObject.StartTime, endtime);
            glowSprite.Color(hitObject.StartTime, hitObject.Color);

            ringSprite.Move(hitObject.StartTime, hitObject.Position);
            ringSprite.Scale(OsbEasing.OutCirc, hitObject.StartTime, hitObject.StartTime + beatDuration, 0.1, 0.2);
            ringSprite.Fade(hitObject.StartTime, endtime, 0.25, 0);
            ringSprite.Additive(hitObject.StartTime, endtime);
        }
    }

    private void GenerateRingHighlight(double startTime, double endTime, bool useComboColor = true)
    {
        using OsbSpritePool pool = new OsbSpritePool(GetLayer("ring-highlight"), "sb/e/c.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });

        foreach (OsuHitObject hitObject in Beatmap.HitObjects)
        {
            if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
            {
                continue;
            }

            double beatDuration = GetBeatDuration(hitObject.StartTime);
            double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) + beatDuration * 4;

            OsbSprite glowSprite = pool.Get(hitObject.StartTime, endtime);

            glowSprite.Move(hitObject.StartTime, hitObject.Position);
            glowSprite.Scale(OsbEasing.OutCubic, hitObject.StartTime, endtime, 0.05, 0.2);
            glowSprite.Fade(hitObject.StartTime, endtime, 0.75, 0);
            glowSprite.Additive(hitObject.StartTime, endtime);

            if (useComboColor)
            {
                glowSprite.Color(hitObject.StartTime, hitObject.Color);
            }
        }
    }

    private void GenerateSquareHighlight(double startTime, double endTime, bool useComboColor = true)
    {
        using OsbSpritePool pool = new OsbSpritePool(GetLayer("square-highlight"), "sb/e/sq.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });

        foreach (OsuHitObject hitObject in Beatmap.HitObjects)
        {
            if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
            {
                continue;
            }

            double beatDuration = GetBeatDuration(hitObject.StartTime);
            double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) + beatDuration * 4;

            OsbSprite glowSprite = pool.Get(hitObject.StartTime, endtime);

            glowSprite.Move(hitObject.StartTime, hitObject.Position);
            glowSprite.Scale(OsbEasing.OutCubic, hitObject.StartTime, endtime, 0.05, 0.2);
            glowSprite.Rotate(OsbEasing.Out, hitObject.StartTime, endtime, 0, Random(Math.PI * 1.75, Math.PI * 1.25));
            glowSprite.Fade(hitObject.StartTime, endtime, 0.75, 0);
            glowSprite.Additive(hitObject.StartTime, endtime);

            if (useComboColor)
            {
                glowSprite.Color(hitObject.StartTime, hitObject.Color);
            }
        }
    }

    private void GenerateVerticalLineHighlight(double startTime, double endTime, bool useComboColor = true)
    {
        using OsbSpritePool pool = new OsbSpritePool(GetLayer("vertical-line"), "sb/e/p.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });

        foreach (OsuHitObject hitObject in Beatmap.HitObjects)
        {
            if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
            {
                continue;
            }

            double beatDuration = GetBeatDuration(hitObject.StartTime);
            double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) + beatDuration * 4;

            OsbSprite glowSprite = pool.Get(hitObject.StartTime, endtime);

            glowSprite.Move(hitObject.StartTime, hitObject.Position.X, 240);
            glowSprite.ScaleVec(OsbEasing.OutExpo, hitObject.StartTime, endtime, 0, 480, 42, 480);
            glowSprite.Fade(OsbEasing.Out, hitObject.StartTime, endtime, 0.2, 0);
            glowSprite.Additive(hitObject.StartTime, endtime);

            if (useComboColor)
            {
                glowSprite.Color(hitObject.StartTime, hitObject.Color);
            }
        }
    }

    private void GenerateGlowHighlight(double startTime, double endTime, double beatMultiply = 4, int beatDivisor = 32, bool followSlider = true)
    {
        using OsbSpritePool pool = new OsbSpritePool(GetLayer("glow"), "sb/e/hl.png", OsbOrigin.Centre, (sprite, poolStartTime, poolEndTime) => { });

        foreach (OsuHitObject hitObject in Beatmap.HitObjects)
        {
            if (hitObject.StartTime < startTime - 5 || endTime - 5 < hitObject.StartTime)
            {
                continue;
            }

            double beatDuration = GetBeatDuration(hitObject.StartTime);
            double endtime = (hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime) + beatDuration * 3;
            OsbSprite sprite = pool.Get(hitObject.StartTime, endtime);

            sprite.Move(hitObject.StartTime, hitObject.Position);
            sprite.Scale(OsbEasing.OutCirc, hitObject.StartTime, endtime, 0.2, 0.5);
            sprite.Fade(hitObject is OsuSpinner ? hitObject.EndTime : hitObject.StartTime, endtime, 0.75, 0);
            sprite.Additive(hitObject.StartTime, endtime);
            sprite.Color(hitObject.StartTime, hitObject.Color);

            if (hitObject is OsuSlider && followSlider)
            {
                double timestep = Beatmap.GetTimingPointAt((int)hitObject.StartTime).BeatDuration / beatDivisor;
                double start_time = hitObject.StartTime;

                while (true)
                {
                    double end_time = start_time + timestep;

                    OsbSprite sliderSprite = pool.Get(start_time, start_time + beatDuration * 3);
                    sliderSprite.Move(start_time, hitObject.PositionAtTime(start_time));
                    sliderSprite.Scale(OsbEasing.OutCirc, start_time, start_time + beatDuration * 3, 0.2, 0.5);
                    sliderSprite.Fade(start_time, start_time + beatDuration * 3, 0.75, 0);
                    sliderSprite.Additive(start_time, start_time + beatDuration * 3);
                    sliderSprite.Color(start_time, hitObject.Color);

                    bool isCompleted = hitObject.EndTime - end_time < 5;

                    if (isCompleted)
                    {
                        break;
                    }

                    start_time += timestep;
                }
            }
        }
    }

    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;

    private double GetOffset(double time)
        => Beatmap.GetTimingPointAt((int)time).Offset;
}