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

namespace StorybrewScripts;

public class IntroPartManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        AddVclLogo(1142, 3428);
        AddVnocLogo(3428, 8000);
        GenerateParticles(1142, 3428, 64);
        GenerateFlashingSquares(21714, 30571);
        UnnamedEffect1();
    }

    private void AddVclLogo(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
        OsbSprite sprite = GetLayer("logo").CreateSprite("sb/v/vcl.png", OsbOrigin.Centre, Constant.CenterPosition);

        sprite.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 0, 0.25, 0.25, 0.25);
        sprite.ScaleVec(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, 0.25, 0.25, 0, 0);
        sprite.Fade(startTime, 1);
    }

    private void AddVnocLogo(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
        OsbSprite logo = GetLayer("logo").CreateSprite("sb/v/vnoc-2024.png");

        logo.Scale(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 0.75, 0.25);
        logo.Scale(OsbEasing.InExpo, endTime - beatDuration * 3, endTime, 0.25, 0);
        logo.Fade(startTime, 1);
    }

    private void UnnamedEffect1()
    {
        double beatDuration = Beatmap.GetTimingPointAt(21714).BeatDuration;

        OsbSprite sprite = GetLayer("unnamed-effect1--bar").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 21714, 22285, 854, 854, 4, 32);
        sprite.Rotate(OsbEasing.OutSine, 21714, 22285, -2 * Math.PI / 2, 0);
        sprite.Fade(21714, 1);

        sprite.StartLoopGroup(22285, 2);
        sprite.Fade(0, beatDuration, 0, 0.75);
        sprite.EndGroup();
        sprite.MoveX(OsbEasing.OutCirc, 22857, 22857 + beatDuration * 2, 320, 120);
        sprite.Fade(22857, 22857 + beatDuration, 0, 1);
        sprite.Fade(30285, 0);

        sprite = GetLayer("unnamed-effect1--bar").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(22857, 4, 32);
        sprite.MoveX(OsbEasing.OutCirc, 22857, 22857 + beatDuration * 2, 320, 520);
        sprite.Fade(22857, 22857 + beatDuration, 0, 1);
        sprite.Fade(30285, 0);

        sprite = GetLayer("unnamed-effect1--bar").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(320, 240 - 16 + 2));

        sprite.ScaleVec(OsbEasing.OutCirc, 22857, 22857 + beatDuration * 2, 0, 4, 400 - 2, 4);
        sprite.Fade(22857, 22857 + beatDuration, 0, 1);
        sprite.Fade(30285, 0);

        sprite = GetLayer("unnamed-effect1--bar").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(320, 240 + 16 - 2));

        sprite.ScaleVec(OsbEasing.OutCirc, 22857, 22857 + beatDuration * 2, 0, 4, 400 - 2, 4);
        sprite.Fade(22857, 22857 + beatDuration, 0, 1);
        sprite.Fade(30285, 0);

        sprite = GetLayer("unnamed-effect1--bar").CreateSprite("sb/e/p.png", OsbOrigin.CentreLeft, new Vector2(320 - 400 * 0.5f + 4, 240));

        sprite.ScaleVec(23428, 29428, 4, 20, 400 - 8, 20);
        sprite.Fade(23428, 1);
        sprite.Fade(30285, 0);


        OsbSprite circle = GetLayer("unnamed-effect-1--circle").CreateSprite("sb/e/hl.png");

        circle.Scale(21714, 3);
        circle.Color(21714, Color4.Black);
        circle.Fade(21714, 1);
        circle.Fade(30857, 0);


        OsbSprite solid = GetLayer("unnamed-effect-1--solid").CreateSprite("sb/e/p.png");

        solid.ScaleVec(21714, 854, 480);
        solid.Color(21714, "#5c331c");
        solid.Fade(21714, 1);
        solid.Fade(30857, 0);
    }

    private void GenerateParticles(double startTime, double endTime, int particleCount, string filePath = "sb/e/d.png", float scale = 0.02f, double opacity = 1)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        for (int i = 0; i < particleCount; i++)
        {
            double angle = Random(0, Math.PI * 2);
            float radiusX = Random(-280, 280f);
            float radiusY = Random(-240, 240f);

            Vector2 endPosition = new Vector2(
                (float)(Constant.CenterPosition.X + Math.Cos(angle) * radiusX),
                (float)(Constant.CenterPosition.Y + Math.Sin(angle) * radiusY)
            );

            float size = Random(0.02f, 0.05f);
            OsbSprite sprite = GetLayer("particles").CreateSprite(filePath, OsbOrigin.Centre, Constant.CenterPosition);

            sprite.Move(OsbEasing.OutExpo, startTime, endTime - beatDuration * 4, Constant.CenterPosition, endPosition);
            sprite.Move(OsbEasing.InExpo, endTime - beatDuration * 4, endTime, endPosition, Constant.CenterPosition);
            sprite.Scale(startTime, size);
            sprite.Additive(startTime, endTime);
        }
    }

    private void GenerateFlashingSquares(double startTime, double endTime, int particleCount = 8)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Floor(duration / beatDuration));

        for (int i = 0; i < particleCount; i++)
        {
            OsbSprite sprite = GetLayer("flashing-squares").CreateSprite("sb/e/p.png");
            double time = startTime;

            for (int j = 0; j < loopCount; j++)
            {
                sprite.Move(time, new Vector2(Random(-107, 747), Random(0, 480)));
                sprite.Scale(startTime, Random(8, 84));
                sprite.Rotate(time, Random(Math.PI * 2));
                sprite.Fade(time, time + beatDuration, 0.5, 0);

                time += beatDuration;
            }
        }
    }
}