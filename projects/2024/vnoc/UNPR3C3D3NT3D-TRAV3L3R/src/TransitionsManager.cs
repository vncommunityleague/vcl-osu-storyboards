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
using StorybrewCommon.Storyboarding.CommandValues;

namespace StorybrewScripts;

public class TransitionsManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        GenerateClosingTransition(26285, 30285);

        GenerateSplitScaleCloseUpDown(30285, 30857);

        GenerateRectangleSplitOpen(30857, 32000);

        GenerateInterlacedDiagonalClose(48000, 49142);
        GenerateSplitScaleCloseVertical(137932, 139576, Color4.Black);

        GenerateSplitUpTransition(191357, 192179);

        GenerateSquareClosing(66142, 67428);

        GenerateBoxExpand(120857);

        GenerateSlideClosingHorizontal(82857, 83428);
        GenerateSplitClosingHorizontal(137110, 137932);
        GenerateSplitScaleCloseLeftRight(165878, 167521);

        GenerateSlideClosingHorizontal(247150, 247436);
        GenerateRotatedSlideClosingHorizontal(257722, 266865);

        GeneratePreKiaiTransition1(266865, 268007);

        GenerateFlash(3428, 3428 + Beatmap.GetTimingPointAt(3428).BeatDuration * 4);
        GenerateFlash(8000, 8000 + Beatmap.GetTimingPointAt(8000).BeatDuration * 2);
        GenerateFlash(8571, 8571 + Beatmap.GetTimingPointAt(8571).BeatDuration * 2, 0.75);
        GenerateFlash(9142, 9142 + Beatmap.GetTimingPointAt(9142).BeatDuration * 4, 0.75);
        GenerateFlash(10285, 10285 + Beatmap.GetTimingPointAt(10285).BeatDuration * 4, 0.75);
        GenerateFlash(12571, 12571 + Beatmap.GetTimingPointAt(12571).BeatDuration * 4);
        GenerateFlash(30857, 30857 + Beatmap.GetTimingPointAt(30857).BeatDuration * 2);
        GenerateFlash(49142, 49142 + Beatmap.GetTimingPointAt(49142).BeatDuration * 4);
        GenerateFlash(67428, 67428 + Beatmap.GetTimingPointAt(67428).BeatDuration * 4, 0.75);
        GenerateFlash(85714, 85714 + Beatmap.GetTimingPointAt(85714).BeatDuration * 4, 0.75);
        GenerateFlash(101714, 101714 + Beatmap.GetTimingPointAt(101714).BeatDuration * 6);
        GenerateFlash(104000, 104000 + Beatmap.GetTimingPointAt(104000).BeatDuration * 4);
        GenerateFlash(113142, 113142 + Beatmap.GetTimingPointAt(113142).BeatDuration * 8);
        GenerateFlash(122285, 122285 + Beatmap.GetTimingPointAt(122285).BeatDuration, 0.5);
        GenerateFlash(154371, 154371 + Beatmap.GetTimingPointAt(154371).BeatDuration * 4);
        GenerateFlash(167521, 167521 + Beatmap.GetTimingPointAt(167521).BeatDuration * 4);
        GenerateFlash(193779, 193779 + Beatmap.GetTimingPointAt(193779).BeatDuration * 8);
        GenerateFlash(229725, 229725 + Beatmap.GetTimingPointAt(229725).BeatDuration * 4);
        GenerateFlash(248579, 248579 + Beatmap.GetTimingPointAt(248579).BeatDuration * 4);
        GenerateFlash(257722, 257722 + Beatmap.GetTimingPointAt(257722).BeatDuration * 4, 0.5);
        GenerateFlash(268007, 268007 + Beatmap.GetTimingPointAt(268007).BeatDuration * 2, 0.5);
        GenerateFlash(286293, 286293 + Beatmap.GetTimingPointAt(286293).BeatDuration * 4);
        GenerateFlash(295436, 295436 + Beatmap.GetTimingPointAt(295436).BeatDuration * 4);
        GenerateFlash(304579, 304579 + Beatmap.GetTimingPointAt(304579).BeatDuration * 8);
        GenerateFlash(313722, 313722 + Beatmap.GetTimingPointAt(322865).BeatDuration * 4, 0.5);
        GenerateFlash(322865, 322865 + Beatmap.GetTimingPointAt(322865).BeatDuration * 4);
        GenerateFlash(332007, 332007 + Beatmap.GetTimingPointAt(332007).BeatDuration * 4, 0.5);
        GenerateFlash(341150, 341150 + Beatmap.GetTimingPointAt(341150).BeatDuration * 6, 0.75);
        GenerateFlash(350293, 350293 + Beatmap.GetTimingPointAt(350293).BeatDuration * 8, 0.75);

        GenerateWhiteout(267436, 268007, 1);

        GenerateBlackout(350293, AudioDuration + 1140, 1, OsbEasing.InCubic);

        GenerateUnnamedTransition2(20571, 21714);

        OsbSprite sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 153549, 154371, 40, 40, 854, 854);
        sprite.Color(153549, Color.Black);
        sprite.Fade(153549, 0.25);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 153857, 154371, 40, 40, 854, 854);
        sprite.Color(153857, Color.Black);
        sprite.Fade(153857, 0.5);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 154165, 154371, 40, 40, 854, 854);
        sprite.Color(154165, Color.Black);
        sprite.Fade(154165, 0.75);
    }

    private void GenerateClosingTransition(double startTime, double endTime)
    {
        const int radius = 490;

        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
        double moveDuration = beatDuration * 16;
        int loopCount = (int)Math.Ceiling((endTime - startTime) / moveDuration);

        for (int i = 0; i < 4; ++i)
        {
            double offsetAngle = (-Math.PI / 2) * i;
            Vector2 position = CalculatePosition(offsetAngle, radius);
            OsbSprite sprite = GetLayer("transition-4").CreateSprite("sb/e/p.png", OsbOrigin.TopCentre, position);

            sprite.ScaleVec(OsbEasing.OutCubic, startTime, startTime + beatDuration * 12, 960, 0, 960, radius * 0.36);
            sprite.ScaleVec(OsbEasing.InExpo, startTime + beatDuration * 12, endTime, 960, radius * 0.36, 960, radius);
            sprite.Color(startTime, "#464646");

            sprite.StartLoopGroup(startTime, loopCount);

            for (int j = 0; j < 4; j++)
            {
                double angle = offsetAngle + (Math.PI / 2) * (j + 1);
                Vector2 nextPosition = CalculatePosition(angle, radius);

                if (i % 2 == 0)
                {
                    sprite.MoveX(j % 2 == 0 ? OsbEasing.InSine : OsbEasing.OutSine, moveDuration * (0.25 * j), moveDuration * (0.25 * (j + 1)), position.X, nextPosition.X);
                    sprite.MoveY(j % 2 == 0 ? OsbEasing.OutSine : OsbEasing.InSine, moveDuration * (0.25 * j), moveDuration * (0.25 * (j + 1)), position.Y, nextPosition.Y);
                }
                else
                {
                    sprite.MoveX(j % 2 == 0 ? OsbEasing.OutSine : OsbEasing.InSine, moveDuration * (0.25 * j), moveDuration * (0.25 * (j + 1)), position.X, nextPosition.X);
                    sprite.MoveY(j % 2 == 0 ? OsbEasing.InSine : OsbEasing.OutSine, moveDuration * (0.25 * j), moveDuration * (0.25 * (j + 1)), position.Y, nextPosition.Y);
                }

                position = nextPosition;
            }

            sprite.Rotate(0, moveDuration, offsetAngle + Math.PI / 2, Math.PI * 2 + offsetAngle + Math.PI / 2);
            sprite.EndGroup();
        }
    }

    private void GenerateSplitScaleCloseUpDown(double startTime, double endTime, double beatMultiply = 0.25)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        double spriteAmount = Math.Round((endTime - startTime) / (beatDuration * beatMultiply));
        double width = 854.0 / spriteAmount;
        double positionX = -107 + (float)width * 0.5f;
        double delay = 0;

        for (int i = 0; i < spriteAmount; i++)
        {
            OsbSprite sprite = GetLayer("split-scale-close-updown").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre,
                new Vector2((float)positionX, i % 2 == 0 ? 0 : 480));
            sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, width, 0, width, 480);
            sprite.Color(startTime + delay, Color4.Black);

            positionX += width;
            delay += beatDuration * beatMultiply;
        }
    }

    private void GenerateRectangleSplitOpen(double startTime, double endTime, double angle = -Math.PI / 3)
    {
        for (int i = 0; i < 2; ++i)
        {
            Vector2 position = new Vector2(
                Constant.CenterPosition.X + 420 * (float)Math.Cos(angle + i % 2 * Math.PI),
                Constant.CenterPosition.Y + 420 * (float)Math.Sin(angle + i % 2 * Math.PI)
            );
            OsbSprite sprite = GetLayer("diagonal-split-open").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.BottomCentre : OsbOrigin.TopCentre, position);

            sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 980, 420, 980, 0);
            sprite.Rotate(startTime, angle - Math.PI / 2);
            sprite.Color(startTime, "#464646");
        }
    }

    private void GenerateInterlacedDiagonalClose(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);

        double angle = Math.Atan2(107, 480);
        double width = 960.0 / 4;

        Vector2 position = new Vector2(-100 + (float)(width * 0.5f), -40);
        double time = startTime;

        for (int i = 0; i < 4; i++)
        {
            OsbSprite sprite = GetLayer("interlaced-diagonal-close").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, position);

            sprite.ScaleVec(OsbEasing.OutQuart, time, startTime + beatDuration * 2, width, 0, width, 560);
            sprite.Fade(time, 1);
            sprite.Fade(endTime, 0);
            sprite.Rotate(time, angle);
            sprite.Color(time, Color4.Black);

            if (i % 2 == 0)
            {
                position.X += (float)width * 0.5f;
            }
            else
            {
                if (i == 1)
                {
                    position.X += (float)width * 0.5f;
                }

                position.X += (float)width;
            }


            position.Y = -40 + 560 * ((i + 1) % 2);
            time += beatDuration * 0.5;
        }

        time = startTime + beatDuration;
        float scale = 0.16f;

        for (int i = 0; i < 3; ++i)
        {
            OsbSprite sprite = GetLayer("interlaced-diagonal-close").CreateSprite("sb/e/sq.png");

            sprite.Rotate(time, Math.PI / 4);
            sprite.Scale(OsbEasing.OutExpo, time, startTime + beatDuration * 3, 0, scale);
            sprite.Scale(OsbEasing.InExpo, endTime - beatDuration, endTime, scale, 0);
            sprite.Fade(time, 1);
            sprite.Fade(endTime, 0);

            time += beatDuration * 0.25;
            scale += 0.06f;
        }

        OsbSprite circle = GetLayer("interlaced-diagonal-close").CreateSprite("sb/e/c4.png");

        circle.Scale(OsbEasing.OutExpo, startTime + beatDuration, endTime - beatDuration, 0, 0.12);
        circle.Scale(OsbEasing.InExpo, endTime - beatDuration, endTime, 0.12, 0);

        for (int i = 0; i < 2; ++i)
        {
            OsbSprite sprite = GetLayer("interlaced-diagonal-close").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.BottomCentre : OsbOrigin.TopCentre, new Vector2(320, i % 2 == 0 ? 480 : 0));

            sprite.ScaleVec(OsbEasing.OutExpo, endTime, endTime + beatDuration * 4, 854, 240, 854, 0);
            sprite.Color(endTime, Color4.Black);
        }
    }

    private void GenerateSquareClosing(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);

        OsbSprite sprite = GetLayer("square-closing").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, startTime + beatDuration * 0.5, startTime + beatDuration * 1.5, 80, 80, 80, 480);
        sprite.ScaleVec(OsbEasing.OutExpo, startTime + beatDuration * 1.5, endTime, 80, 480, 854, 480);
        sprite.MoveY(OsbEasing.OutExpo, startTime, startTime + beatDuration * 1.5, 540, 240);
        sprite.Rotate(OsbEasing.OutCubic, startTime, startTime + beatDuration * 1.5, -Math.PI * 2, 0);

        double time = startTime + beatDuration * 2;
        float opacity = 0.25f;

        for (int i = 0; i < 5; i++)
        {
            OsbSprite rectangle = GetLayer("square-closing").CreateSprite("sb/e/p.png");

            rectangle.ScaleVec(OsbEasing.OutCirc, time, endTime, 0, 480, 854, 480);
            rectangle.Color(time, Color4.Black);
            rectangle.Fade(time, opacity);
            rectangle.Fade(endTime, 0);

            time += beatDuration * 0.5;
            opacity += 0.2f / 8;
        }

        double angle = Math.PI / 6;

        for (int i = 0; i < 2; ++i)
        {
            Vector2 position = new Vector2(
                Constant.CenterPosition.X + 480 * (float)Math.Cos(angle + i % 2 * Math.PI),
                Constant.CenterPosition.Y + 480 * (float)Math.Sin(angle + i % 2 * Math.PI)
            );
            OsbSprite rectangle = GetLayer("square-closing").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.BottomCentre : OsbOrigin.TopCentre, position);

            rectangle.ScaleVec(OsbEasing.OutExpo, endTime, endTime + beatDuration * 4, 980, 480, 980, 0);
            rectangle.Rotate(endTime, angle - Math.PI / 2);
            rectangle.Color(endTime, Color4.Black);
        }
    }

    private void GenerateBoxExpand(double startTime)
    {
        double beatDuration = GetBeatDuration(startTime);
        OsbSprite sprite = GetLayer("box-expand").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutCubic, startTime, startTime + beatDuration * 0.5, 120, 120, 854, 40);
        sprite.ScaleVec(OsbEasing.In, startTime + beatDuration * 0.5, startTime + beatDuration, 854, 40, 854, 480);
    }

    private void GenerateSplitClosingHorizontal(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);
        double spriteAmount = Math.Round((endTime - startTime) / (beatDuration / 32));
        double width = 910.0 / spriteAmount;
        double positionX = -146 + (float)width * 0.5f;
        double time = startTime;

        for (int i = 0; i < spriteAmount; i++)
        {
            OsbSprite sprite = GetLayer("split-closing-horizontal").CreateSprite("sb/e/p.png", OsbOrigin.CentreLeft, new Vector2((float)positionX, 240));
            sprite.ScaleVec(OsbEasing.OutExpo, time, endTime, 0, 490, width * 1.001, 490);
            sprite.Rotate(time, Math.PI / 30);

            positionX += width;
            time += beatDuration / 32;
        }
    }

    private void GenerateSlideClosingHorizontal(double startTime, double endTime)
    {
        for (int i = 0; i < 2; i++)
        {
            OsbSprite sprite = GetLayer("slide-closing-horizontal").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(320, i % 2 * 480));

            sprite.ScaleVec(OsbEasing.OutExpo, startTime, endTime, 854, 0, 854, 240);
            sprite.Color(startTime, "#121212");
        }
    }

    private void GenerateRotatedSlideClosingHorizontal(double startTime, double endTime, double angle = -Math.PI / 2.4)
    {
        for (int i = 0; i < 2; i++)
        {
            Vector2 position = new Vector2(
                Constant.CenterPosition.X + 350 * (float)Math.Cos(angle + i % 2 * Math.PI),
                Constant.CenterPosition.Y + 350 * (float)Math.Sin(angle + i % 2 * Math.PI)
            );
            OsbSprite sprite = GetLayer("rotated-slide-closing-horizontal").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.BottomCentre : OsbOrigin.TopCentre, position);

            sprite.ScaleVec(OsbEasing.InOutCirc, startTime, endTime, 980, 0, 980, 350);
            sprite.Rotate(startTime, angle - Math.PI / 2);
            sprite.Color(startTime, Color.Black);
        }
    }

    private void GenerateSplitScaleCloseVertical(double startTime, double endTime, CommandColor color)
    {
        double beatDuration = GetBeatDuration(startTime);

        double spriteAmount = Math.Round((endTime - startTime) / (beatDuration * 0.25));
        double height = 480.0 / spriteAmount;
        double positionY = (float)height * 0.5f;
        double time = startTime;

        for (int i = 0; i < spriteAmount; i++)
        {
            OsbSprite sprite = GetLayer("split-scale-close-horizontal").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(320, (float)positionY));
            sprite.ScaleVec(OsbEasing.OutExpo, time, endTime, 854, 0, 854, height);
            sprite.Color(time, color);

            positionY += height;
            time += beatDuration * 0.25;
        }
    }

    private void GenerateSplitScaleCloseLeftRight(double startTime, double endTime, double beatMultiply = 0.25)
    {
        double beatDuration = GetBeatDuration(startTime);

        double spriteAmount = Math.Round((endTime - startTime) / (beatDuration * beatMultiply));
        double height = 480.0 / spriteAmount;
        double positionY = 480 - (float)height * 0.5f;
        double delay = 0;

        for (int i = 0; i < spriteAmount; i++)
        {
            OsbSprite sprite = GetLayer("split-scale-close-leftright").CreateSprite("sb/e/p.png",
                i % 2 == 0 ? OsbOrigin.CentreLeft : OsbOrigin.CentreRight,
                new Vector2(i % 2 == 0 ? -107 : 747, (float)positionY));
            sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, 0, height, 854, height);

            positionY -= height;
            delay += beatDuration * beatMultiply;
        }
    }

    private void GenerateSplitUpTransition(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);
        double spriteAmount = Math.Round((endTime - startTime) / (beatDuration * 0.25));
        double width = 854.0 / spriteAmount;
        double positionX = -107 + (float)width * 0.5f;
        double delay = 0;

        for (int i = 0; i < spriteAmount; i++)
        {
            OsbSprite sprite = GetLayer("split-up-transition").CreateSprite("sb/e/p.png", OsbOrigin.BottomCentre, new Vector2((float)positionX, 480));
            sprite.ScaleVec(OsbEasing.OutExpo, startTime + delay, endTime, width, 0, width, 480);

            positionX += width;
            delay += beatDuration * 0.25;
        }
    }

    private void GeneratePreKiaiTransition1(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);

        OsbSprite sprite = GetLayer("pre-kiai-transition-1").CreateSprite("sb/e/sq2.png");

        sprite.Scale(OsbEasing.OutExpo, startTime, startTime + beatDuration * 1.5, 2, 0.4);
        sprite.Scale(OsbEasing.InExpo, startTime + beatDuration * 1.5, endTime, 0.4, 0);
        sprite.Rotate(startTime, Math.PI / 4);

        double time = startTime + beatDuration * 1.5;
        double opacity = 0.2;

        for (int i = 0; i < 5; i++)
        {
            sprite = GetLayer("pre-kiai-transition-1").CreateSprite("sb/e/p.png");

            sprite.Scale(OsbEasing.OutCirc, time, endTime, 960, 0);
            sprite.Rotate(time, Math.PI / 4);
            sprite.Fade(time, opacity);

            time += beatDuration * 0.5;
            opacity += 0.8 / 5;
        }
    }

    private void GenerateUnnamedTransition2(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;
        double timestep = beatDuration / 16;

        Vector2 position = new Vector2(-187, 480);
        for (int i = 0; i < 47; i++)
        {
            OsbSprite sprite = GetLayer("unnamed transition 2").CreateSprite("sb/e/p.png", OsbOrigin.BottomLeft, position);

            sprite.ScaleVec(OsbEasing.OutExpo, startTime + timestep * i, endTime, 0, 0, 20, 540);
            sprite.Rotate(startTime + timestep * i, Math.PI / 20);

            position.X += 20;
        }
    }

    private void GenerateFlash(double startTime, double endTime, double opacity = 1)
    {
        OsbSprite sprite = GetLayer("flash").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(startTime, 854, 480);
        sprite.Fade(startTime, endTime, opacity, 0);
    }

    private void GenerateBlackout(double startTime, double endTime, double opacity = 1, OsbEasing easing = OsbEasing.None)
    {
        OsbSprite sprite = GetLayer("blackout").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(startTime, 854, 480);
        sprite.Color(startTime, Color4.Black);
        sprite.Fade(easing, startTime, endTime, 0, opacity);
    }

    private void GenerateWhiteout(double startTime, double endTime, double opacity = 1, OsbEasing easing = OsbEasing.None)
    {
        OsbSprite sprite = GetLayer("whiteout").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(startTime, 854, 480);
        sprite.Fade(easing, startTime, endTime, 0, opacity);
    }


    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;

    private Vector2 CalculatePosition(double angle, double radius) => new Vector2(
        Constant.CenterPosition.X + (float)Math.Cos(angle) * (float)radius,
        Constant.CenterPosition.Y + (float)Math.Sin(angle) * (float)radius
    );
}