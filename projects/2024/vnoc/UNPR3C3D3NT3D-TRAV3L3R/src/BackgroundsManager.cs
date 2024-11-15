using OpenTK;
using OpenTK.Graphics;
using StorybrewCommon.Animations;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Util;
using System;
using System.Drawing;

namespace StorybrewScripts;

public enum BackgroundType
{
    Default,
    DefaultWithoutCharacter,
    Blur,
    BlurWithoutCharacter,
    BlackWhite,
    BlackWhiteWithoutCharacter,
    BlackWhiteBlur,
    BlackWhiteBlurWithoutCharacter,
    RadialBlur,
    BlackWhiteRadialBlur,
}

public class BackgroundsManager : StoryboardObjectGenerator
{
    private Bitmap BackgroundBitmap;

    private readonly int BeatDivisor = 4;
    private readonly Vector2 Scale = new (1, 80);
    private readonly int LogScale = 600;
    private readonly double Tolerance = 0.2;
    private readonly int CommandDecimals = 1;
    private readonly float MinimalHeight = 12f;
    private readonly OsbEasing FftEasing = OsbEasing.InExpo;

    public override void Generate()
    {
        BackgroundBitmap = GetMapsetBitmap(GetBackgroundPath(BackgroundType.Default));

        DeleteBackground();

        AddIntroBackground();

        AddMainBackground(154371, 167521, 0.9);
        AddKiaiBackground(268007, 304579);
        AddKiaiBackground(268007, 304579);
        AddKiaiBackground(322865, 341150);
        AddBlackWhiteBlurBackground(104000, 122285);
        AddBlackWhiteBlurWithoutCharacterBackground(122285, 137932, 0.15);
        AddBlackWhiteBlurWithoutCharacterBackground(141220, 167521, 0.16);
        AddBlackWhiteBlurWithoutCharacterBackground(229725, 247436, 0.25);
        AddRadialBlurBackground(248579);
        AddCharacter(248579, 268007);
        AddBlurWithoutCharacterBackground(167521, 193779, 0.2);
        AddBlurWithoutCharacterBackground(248579, 268007);
        AddBlurWithoutCharacterBackground(193779, 229725, 0.75);
        AddBlurWithoutCharacterBackground(304579, 322865, 0.2);

        GenerateMovingOutParticlesBackground(21714, 49142);
        GenerateMovingOutRaysBackground(30857, 49142);

        AddCroppingBackground(67428, 83428);
        AddCroppingBackground2(85714, 101714);
        AddCroppingBackground3(304579, 313722);
        AddCroppingBackground4(313722, 322865);

        GeneratePerspectiveGridBackground(167521, 192179);
        GenerateSpectrum(167521, 192179, 854);

        AddIntroSectionBackground();
        GenerateUnnamedBackground1();

        GenerateRotatingSquare(113142, 120857, -Math.PI / 4, Math.PI * 2, OsbEasing.OutCirc);

        GenerateFillInPixelBackground(BackgroundType.Default, 121142, 122285);

        AddCreditsBackground(341150, AudioDuration + 1140);
    }

    private void DeleteBackground()
    {
        GetLayer("delete-background").CreateSprite(Beatmap.BackgroundPath, OsbOrigin.Centre, new Vector2(0, 0)).Fade(0, 0);
    }


    private void AddCharacter(double startTime, double endTime)
    {
        OsbSprite sprite = GetLayer("character").CreateSprite("sb/bg/c.png");

        sprite.Scale(startTime, GetScaleRatio());
        sprite.Fade(startTime, 1);
        sprite.Fade(endTime, 0);
    }


    private void AddIntroBackground()
    {
        OsbSprite sprite = GetLayer("intro").CreateSprite(GetBackgroundPath(BackgroundType.DefaultWithoutCharacter));

        float scale = GetScaleRatio();

        sprite.Scale(8000, scale * 2f);
        sprite.Fade(OsbEasing.OutCirc, 8000, 8571, 1, 0.5);
        sprite.MoveX(OsbEasing.OutCirc, 8000, 8571, 220, 480);
        sprite.MoveY(8000, 490);
        sprite.MoveX(8571, 700);
        sprite.Fade(OsbEasing.OutCirc, 8571, 9142, 1, 0.5);
        sprite.MoveY(OsbEasing.OutCirc, 8571, 9142, 280, 100);
        sprite.MoveX(9142, -100);
        sprite.Fade(OsbEasing.OutCirc, 9142, 10285, 1, 0.5);
        sprite.MoveY(OsbEasing.OutCirc, 9142, 10285, 120, 280);
        sprite.Fade(OsbEasing.OutExpo, 10285, 12000, 1, 0.25);
        sprite.Scale(OsbEasing.OutExpo, 10285, 12000, scale * 3f, scale * 1.5f);
        sprite.Fade(OsbEasing.InCirc, 12000, 12571, 0.25, 0.8);
        sprite.Scale(OsbEasing.InCirc, 12000, 12571, scale * 1.5f, scale);
        sprite.MoveX(10285, 320);
        sprite.MoveY(10285, 240);

        sprite = GetLayer("intro").CreateSprite(GetBackgroundPath(BackgroundType.BlurWithoutCharacter));

        sprite.Scale(OsbEasing.OutExpo, 12571, 14285, scale * 5f, scale * 1.15f);
        sprite.Scale(OsbEasing.InSine, 14285, 17142, scale * 1.15f, scale);
        sprite.MoveX(12571, 320);
        sprite.MoveY(12571, 240);
        sprite.Scale(17142, scale * 4.5f);
        sprite.MoveX(OsbEasing.OutExpo, 17142, 19428, 900, 180);
        sprite.MoveY(17142, 940);

        OsbSprite character = GetLayer("intro").CreateSprite("sb/bg/c.png");

        character.Scale(OsbEasing.OutExpo, 12571, 14285, scale * 5f, scale * 1.1f);
        character.Scale(OsbEasing.InSine, 14285, 17142, scale * 1.1f, scale);
        character.MoveX(12571, 320);
        character.MoveY(12571, 240);
        character.Scale(17142, scale * 4.5f);
        character.MoveX(OsbEasing.OutExpo, 17142, 19428, 900, 180);
        character.MoveY(17142, 940);

        sprite = GetLayer("intro").CreateSprite(Beatmap.BackgroundPath);
        scale = 480f / GetMapsetBitmap(Beatmap.BackgroundPath).Height;

        sprite.Scale(OsbEasing.OutExpo, 19428, 20571, scale * 5f, scale);
        sprite.Scale(OsbEasing.InExpo, 20571, 21714, scale, scale * 5f);
    }

    private void AddMainBackground(double startTime, double endTime, double opacity = 1)
    {
        OsbSprite sprite = GetLayer("main").CreateSprite(GetBackgroundPath(BackgroundType.Default));

        sprite.Scale(startTime, GetScaleRatio());
        sprite.Fade(startTime, opacity);
        sprite.Fade(endTime, 0);
    }

    private void AddBlackWhiteBlurBackground(double startTime, double endTime)
    {
        OsbSprite sprite = GetLayer("black-white-blur").CreateSprite(GetBackgroundPath(BackgroundType.BlackWhiteBlur));

        sprite.Scale(startTime, GetScaleRatio());

        if (endTime <= 122285)
        {
            sprite.Fade(startTime, 108571, 0, 0.25);
            sprite.Fade(113142, 114285, 0.25, 0.5);
        }

        if (endTime <= 322865)
        {
        }

        sprite.Fade(endTime, 0.5);
    }

    private void AddBlackWhiteBlurWithoutCharacterBackground(double startTime, double endTime, double opacity = 1)
    {
        double beatDuration = GetBeatDuration(startTime);

        OsbSprite sprite = GetLayer("black-white-blur-without-character").CreateSprite(GetBackgroundPath(BackgroundType.BlackWhiteBlurWithoutCharacter));

        sprite.Fade(startTime, opacity);
        sprite.Fade(endTime, 0);

        if (startTime < 122284)
        {
            return;
        }

        double duration = endTime - startTime;
        double loopDuration = beatDuration * 20;
        int loopCount = Math.Max(1, (int)Math.Ceiling(duration / loopDuration));

        float scale = GetScaleRatio();

        sprite.StartLoopGroup(startTime, loopCount);
        sprite.Scale(OsbEasing.InOutSine, 0, loopDuration * 0.5, scale * 1.15, scale * 1.2);
        sprite.Scale(OsbEasing.InOutSine, loopDuration * 0.5, loopDuration, scale * 1.2, scale * 1.15);
        sprite.EndGroup();

        MoveBackground(ref sprite, startTime, endTime, new Vector2(8), Math.PI / 132, loopDuration);
    }

    private void AddBlurWithoutCharacterBackground(double startTime, double endTime, double opacity = 1)
    {
        OsbSprite sprite = GetLayer("blur-without-character").CreateSprite(GetBackgroundPath(BackgroundType.BlurWithoutCharacter));

        sprite.Scale(startTime, GetScaleRatio());
        sprite.Fade(startTime, opacity);
        sprite.Fade(endTime, 0);
    }

    private void AddRadialBlurBackground(double startTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        OsbSprite sprite = GetLayer("radial-blur").CreateSprite(GetBackgroundPath(BackgroundType.RadialBlur));

        sprite.Scale(startTime, GetScaleRatio());
        sprite.Fade(OsbEasing.Out, startTime, startTime + beatDuration * 4, 1, 0);
    }

    private void AddIntroSectionBackground()
    {
        double startTime = 1142;
        double endTime = 101714;
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        OsbSprite sprite = GetLayer("intro").CreateSprite(GetBackgroundPath(BackgroundType.BlurWithoutCharacter));

        sprite.Fade(857, 2285, 0, 0.3);
        sprite.Fade(8000, 0);
        sprite.Fade(49142, 0.15);

        sprite.StartLoopGroup(49142, 16);
        sprite.Fade(OsbEasing.Out, 0, beatDuration * 2, 0.4, 0.3);
        sprite.Fade(OsbEasing.Out, beatDuration * 2, beatDuration * 4, 0.3, 0.3);
        sprite.EndGroup();

        sprite.Fade(101714, 101714 + beatDuration * 4, 0.15, 0);

        double duration = endTime - startTime;
        double loopDuration = beatDuration * 28;
        int loopCount = Math.Max(1, (int)Math.Ceiling(duration / loopDuration));

        float scale = GetScaleRatio();

        sprite.StartLoopGroup(startTime, loopCount);
        sprite.Scale(OsbEasing.InOutSine, 0, loopDuration * 0.5, scale * 1.25, scale * 1.3);
        sprite.Scale(OsbEasing.InOutSine, loopDuration * 0.5, loopDuration, scale * 1.3, scale * 1.25);
        sprite.EndGroup();

        MoveBackground(ref sprite, startTime, endTime, new Vector2(8), Math.PI / 160, loopDuration);
    }

    private void GenerateMovingOutParticlesBackground(double startTime, double endTime, int particleCount = 128, double lifetime = 4000)
    {
        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Floor(duration / lifetime));

        for (int i = 0; i < particleCount; i++)
        {
            double moveAngle = Random(-Math.PI * 2, Math.PI * 2);
            double moveSpeed = Random(360, 480d);
            double moveDistance = moveSpeed * lifetime * 0.001;

            Vector2 startPosition = Constant.CenterPosition;
            Vector2 endPosition = startPosition + new Vector2((float)(Math.Cos(moveAngle) * moveDistance), (float)(Math.Sin(moveAngle) * moveDistance));

            double loopDuration = duration / loopCount;
            double spriteStartTime = startTime + (i * loopDuration) / particleCount;
            double spriteEndTime = spriteStartTime + loopDuration * loopCount;

            OsbSprite sprite = GetLayer("moving-out-particles").CreateSprite("sb/e/d.png", OsbOrigin.Centre, startPosition);

            sprite.Scale(spriteStartTime, Random(0.02, 0.05));
            sprite.Fade(spriteStartTime, Random(0.5, 0.95));
            sprite.Fade(endTime, 0);
            sprite.Additive(spriteStartTime, spriteEndTime);

            sprite.StartLoopGroup(spriteStartTime, loopCount);
            sprite.Move(OsbEasing.InCubic, 0, loopDuration, startPosition, endPosition);
            sprite.EndGroup();
        }
    }

    private void GenerateMovingOutRaysBackground(double startTime, double endTime, int particleCount = 48, double lifetime = 2000)
    {
        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Floor(duration / lifetime));

        for (int i = 0; i < particleCount; i++)
        {
            double moveAngle = Random(-Math.PI * 2, Math.PI * 2);
            double moveSpeed = Random(320, 480d);
            double moveDistance = moveSpeed * lifetime * 0.001;

            Vector2 startPosition = Constant.CenterPosition;
            Vector2 endPosition = startPosition + new Vector2((float)(Math.Cos(moveAngle) * moveDistance), (float)(Math.Sin(moveAngle) * moveDistance));

            double loopDuration = duration / loopCount;
            double spriteStartTime = startTime + (i * loopDuration) / particleCount;
            double spriteEndTime = spriteStartTime + loopDuration * loopCount;

            OsbSprite sprite = GetLayer("moving-out-rays").CreateSprite("sb/e/p.png", OsbOrigin.TopCentre, startPosition);

            sprite.Fade(spriteStartTime, 0.2);
            sprite.Fade(endTime, 0);
            sprite.Rotate(spriteStartTime, moveAngle - Math.PI / 2);
            sprite.Additive(spriteStartTime, spriteEndTime);

            sprite.StartLoopGroup(spriteStartTime, loopCount);
            sprite.ScaleVec(OsbEasing.None, 0, loopDuration, 5, 0, 5, 320);
            sprite.Move(OsbEasing.InCubic, loopDuration * 0.25, loopDuration, startPosition, endPosition);
            sprite.EndGroup();
        }
    }

    private void AddCroppingBackground(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Vector2 position = new Vector2(100, 240);
        int openAmount = 160;

        OsbSprite sprite = GetLayer("cropping").CreateSprite(GetBackgroundPath(BackgroundType.Default), OsbOrigin.Centre, new Vector2(-60, 280));

        sprite.MoveX(startTime, endTime, -60, 140);
        sprite.Scale(startTime, GetScaleRatio() * 1.3);

        for (int i = 0; i < 2; i++)
        {
            OsbSprite border = GetLayer("cropping").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.CentreLeft : OsbOrigin.CentreRight, new Vector2(position.X + (i % 2 == 0 ? -1 : 1) * 854, position.Y));

            border.ScaleVec(OsbEasing.OutBack, startTime, startTime + beatDuration * 2, 854, 480, 854 - openAmount * 0.5, 480);
            border.ScaleVec(OsbEasing.InBack, endTime - beatDuration * 2, endTime, 854 - openAmount * 0.5, 480, 854, 480);
            border.Color(startTime, Color4.Black);
        }
    }

    private void AddCroppingBackground2(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Vector2 position = new Vector2(320, 240);
        int openAmount = 160;

        OsbSprite sprite = GetLayer("cropping").CreateSprite(GetBackgroundPath(BackgroundType.Default), OsbOrigin.Centre, new Vector2(340, 240));

        sprite.MoveY(startTime, endTime, 260, 360);
        sprite.Scale(startTime, GetScaleRatio() * 1.5);

        for (int i = 0; i < 2; i++)
        {
            OsbSprite border = GetLayer("cropping").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.CentreLeft : OsbOrigin.CentreRight, new Vector2(position.X + (i % 2 == 0 ? -1 : 1) * 854, position.Y));

            border.ScaleVec(OsbEasing.OutBack, startTime, startTime + beatDuration * 2, 854, 480, 854 - openAmount * 0.5, 480);
            border.ScaleVec(OsbEasing.InBack, endTime - beatDuration * 2, endTime, 854 - openAmount * 0.5, 480, 854, 480);
            border.Color(startTime, Color4.Black);
        }
    }

    private void AddCroppingBackground3(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Vector2 position = new Vector2(320, 336);
        int openAmount = 180;

        OsbSprite sprite = GetLayer("cropping").CreateSprite(GetBackgroundPath(BackgroundType.Default), OsbOrigin.Centre, new Vector2(340, 240));

        sprite.MoveX(startTime, endTime, 380, 440);
        sprite.Scale(startTime, GetScaleRatio() * 1.8);

        for (int i = 0; i < 2; i++)
        {
            OsbSprite border = GetLayer("cropping").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(position.X, position.Y + (i % 2 == 0 ? -1 : 1) * 480));

            border.ScaleVec(OsbEasing.OutBack, startTime, startTime + beatDuration * 2, 854, 480, 854, 480 - openAmount * 0.5);
            border.ScaleVec(OsbEasing.InBack, endTime - beatDuration * 2, endTime, 854, 480 - openAmount * 0.5, 854, 480);
            border.Color(startTime, Color4.Black);
        }
    }

    private void AddCroppingBackground4(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Vector2 position = new Vector2(320, 140);
        int openAmount = 140;

        OsbSprite sprite = GetLayer("cropping").CreateSprite(GetBackgroundPath(BackgroundType.Default), OsbOrigin.Centre, new Vector2(340, 450));

        sprite.MoveX(startTime, endTime, 280, 200);
        sprite.Scale(startTime, GetScaleRatio() * 2.0);

        for (int i = 0; i < 2; i++)
        {
            OsbSprite border = GetLayer("cropping").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.TopCentre : OsbOrigin.BottomCentre, new Vector2(position.X, position.Y + (i % 2 == 0 ? -1 : 1) * 480));

            border.ScaleVec(OsbEasing.OutBack, startTime, startTime + beatDuration * 2, 854, 480, 854, 480 - openAmount * 0.5);
            border.ScaleVec(OsbEasing.InBack, endTime - beatDuration * 2, endTime, 854, 480 - openAmount * 0.5, 854, 480);
            border.Color(startTime, Color4.Black);
        }
    }

    private void GeneratePerspectiveGridBackground(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        int amount = 15;
        float positionX = -107 + (854f / amount);

        for (int i = 0; i < amount - 1; ++i)
        {
            OsbSprite sprite = GetLayer("perspective-grid").CreateSprite("sb/e/p.png", OsbOrigin.TopCentre, new Vector2(positionX, 240));
            sprite.Rotate(startTime, Math.PI / 2 - Math.Atan2(80, 320 - positionX));
            sprite.ScaleVec(startTime, 2, 420);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            positionX += (854f / amount);
        }

        double time = startTime;

        for (int i = 0; i < 24; ++i)
        {
            OsbSprite sprite = GetLayer("perspective-grid").CreateSprite("sb/e/p.png");

            sprite.StartLoopGroup(time - beatDuration * 8, 8);
            sprite.MoveY(OsbEasing.OutCubic, 0, beatDuration * 8, 490, 240);
            sprite.EndGroup();
            sprite.ScaleVec(startTime, 854, 2);
            sprite.Fade(startTime - 1, 0);
            sprite.Fade(startTime, 1);
            sprite.Fade(endTime, 0);

            time += beatDuration * 0.5;
        }
    }

    private void GenerateSpectrum(double startTime, double endTime, float width, int barCount = 32)
    {
        KeyframedValue<float>[] keyFramedValues = GetKeyframedValues(startTime, endTime, barCount);

        float barWidth = width / barCount;
        float positionX = 320 - width * 0.5f;

        for (var i = 0; i < barCount; i++)
        {
            KeyframedValue<float> keyframes = keyFramedValues[i];
            keyframes.Simplify1dKeyframes(Tolerance, h => h);


            OsbSprite bar = GetLayer("spectrum").CreateSprite("sb/e/p.png", OsbOrigin.BottomLeft, new Vector2(positionX + i * barWidth, 239));
            bar.CommandSplitThreshold = 5000;
            // bar.Color(startTime, colors[i][0] / 255.0, colors[i][1] / 255.0, colors[i][2] / 255.0);
            bar.Fade(startTime, 1);
            bar.Additive(startTime, endTime);

            double scaleX = Scale.X * barWidth;
            scaleX = (float)Math.Floor(scaleX * 10) / 10.0f;

            bool hasScale = false;
            keyframes.ForEachPair(
                (start, end) =>
                {
                    hasScale = true;
                    bar.ScaleVec(start.Time, end.Time,
                        scaleX, start.Value,
                        scaleX, end.Value);
                },
                MinimalHeight,
                s => (float)Math.Round(s, CommandDecimals)
            );

            if (!hasScale)
            {
                bar.ScaleVec(startTime, scaleX, MinimalHeight);
            }
        }
    }

    private void AddCreditsBackground(double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        OsbSprite sprite = GetLayer("credits").CreateSprite(GetBackgroundPath(BackgroundType.Blur));

        sprite.Scale(startTime - beatDuration * 2, 480.0f / BackgroundBitmap.Height);
        sprite.Fade(startTime - beatDuration * 2, startTime, 0, 1);
        sprite.Fade(OsbEasing.OutExpo, startTime, endTime, 1, 0);

        sprite = GetLayer("credits").CreateSprite(GetBackgroundPath(BackgroundType.BlackWhiteBlur));

        sprite.Scale(startTime, 480.0f / BackgroundBitmap.Height);
        sprite.Fade(startTime, 0.4);
        sprite.Fade(endTime, 0);
    }

    private void GenerateRotatingSquare(double startTime, double endTime, double startAngle, double endAngle, OsbEasing easing = OsbEasing.None)
    {
        OsbSprite sprite = GetLayer("rotating-square").CreateSprite("sb/e/p.png");

        sprite.Scale(startTime, 120);
        sprite.Rotate(easing, startTime, endTime, startAngle, endAngle);
    }

    private void GenerateFillInPixelBackground(BackgroundType backgroundType, double startTime, double endTime)
    {
        int step = 64;
        double beatDuration = GetBeatDuration(startTime);

        Bitmap bitmap = GetMapsetBitmap(GetBackgroundPath(backgroundType));
        float scale = GetScaleRatio();

        double time = startTime;

        for (int x = 0; x < bitmap.Width; x += step)
        {
            for (int y = 0; y < bitmap.Height; y += step)
            {
                Color pixel = bitmap.GetPixel(x, y);

                if (pixel.A <= 0)
                {
                    continue;
                }

                Vector2 position = new Vector2(
                    Constant.CenterPosition.X - bitmap.Width * scale * 0.5f + x * scale,
                    Constant.CenterPosition.Y - bitmap.Height * scale * 0.5f + y * scale
                ) + new Vector2(9, 6);

                OsbSprite sprite = GetLayer("fill-in-pixel").CreateSprite("sb/e/p.png", OsbOrigin.Centre, position);

                sprite.Scale(OsbEasing.OutExpo, time, time + beatDuration * 2, 0, 20);
                sprite.Scale(OsbEasing.OutExpo, endTime, endTime + beatDuration * 2, 20, 0);
                sprite.Rotate(OsbEasing.OutCirc, endTime, endTime + beatDuration * 2, 0, Math.PI * 1.25);
                sprite.Color(time, pixel.R / 255.0, pixel.G / 255.0, pixel.B / 255.0);
                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 1);

                sprite.StartLoopGroup(startTime + beatDuration * 2.5, 6);
                sprite.Fade(0, beatDuration * 0.25, 0.5, 0);
                sprite.EndGroup();
            }

            time += beatDuration * 0.05;
        }
    }

    private void AddKiaiBackground(double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);

        OsbSprite sprite = GetLayer("kiai-background").CreateSprite(GetBackgroundPath(BackgroundType.Default));

        sprite.Fade(startTime, 1);
        sprite.Fade(endTime, 0);

        if (startTime < 122284)
        {
            return;
        }

        double duration = endTime - startTime;
        double loopDuration = beatDuration * 20;
        int loopCount = Math.Max(1, (int)Math.Ceiling(duration / loopDuration));

        float scale = GetScaleRatio();

        sprite.StartLoopGroup(startTime, loopCount);
        sprite.Scale(OsbEasing.InOutSine, 0, loopDuration * 0.5, scale * 1.15, scale * 1.2);
        sprite.Scale(OsbEasing.InOutSine, loopDuration * 0.5, loopDuration, scale * 1.2, scale * 1.15);
        sprite.EndGroup();

        MoveBackground(ref sprite, startTime, endTime, new Vector2(8), Math.PI / 132, loopDuration);

        OsbSprite rb = GetLayer("radial-blur").CreateSprite(GetBackgroundPath(BackgroundType.RadialBlur));

        rb.Fade(OsbEasing.Out, startTime, startTime + beatDuration * 4, 1, 0);

        rb.StartLoopGroup(startTime, loopCount);
        rb.Scale(OsbEasing.InOutSine, 0, loopDuration * 0.5, scale * 1.15, scale * 1.2);
        rb.Scale(OsbEasing.InOutSine, loopDuration * 0.5, loopDuration, scale * 1.2, scale * 1.15);
        rb.EndGroup();

        MoveBackground(ref rb, startTime, endTime, new Vector2(8), Math.PI / 132, loopDuration);
    }

    private void GenerateUnnamedBackground1()
    {
        OsbSprite circle = GetLayer("unnamed-background-1--circle").CreateSprite("sb/e/hl.png");

        circle.Scale(30857, 3);
        circle.Color(30857, Color4.Black);
        circle.Fade(30857, 1);
        circle.Fade(49142, 0);


        OsbSprite solid = GetLayer("unnamed-background-1--solid").CreateSprite("sb/e/p.png");

        solid.ScaleVec(30857, 854, 480);
        solid.Color(30857, "#5c331c");
        solid.Fade(30857, 1);
        solid.Fade(49142, 0);
    }

    private void MoveBackground(ref OsbSprite sprite, double startTime, double endTime, Vector2 moveRadius, double rotateAmount, double loopDuration)
    {
        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Ceiling(duration / loopDuration));

        Vector2 startPosition = Constant.CenterPosition - moveRadius;
        Vector2 endPosition = Constant.CenterPosition + moveRadius;

        sprite.StartLoopGroup(startTime, loopCount);
        sprite.Move(OsbEasing.InOutSine, 0, loopDuration * 0.5, startPosition, endPosition);
        sprite.Move(OsbEasing.InOutSine, loopDuration * 0.5, loopDuration, endPosition, startPosition);
        sprite.Rotate(OsbEasing.InOutSine, 0, loopDuration * (1 / 3.0), -rotateAmount * 0.5, -rotateAmount);
        sprite.Rotate(OsbEasing.InOutSine, loopDuration * (1 / 3.0), loopDuration * (2 / 3.0), -rotateAmount, rotateAmount);
        sprite.Rotate(OsbEasing.InOutSine, loopDuration * (2 / 3.0), loopDuration, rotateAmount, -rotateAmount * 0.5);
        sprite.EndGroup();
    }

    private string GetBackgroundPath(BackgroundType type)
    {
        switch (type)
        {
            case BackgroundType.Default:
                return "sb/bg/d.jpg";

            case BackgroundType.DefaultWithoutCharacter:
                return "sb/bg/d2.jpg";

            case BackgroundType.Blur:
                return "sb/bg/b.jpg";

            case BackgroundType.BlurWithoutCharacter:
                return "sb/bg/b2.jpg";

            case BackgroundType.BlackWhite:
                return "sb/bg/bw.jpg";

            case BackgroundType.BlackWhiteWithoutCharacter:
                return "sb/bg/bw2.jpg";

            case BackgroundType.BlackWhiteBlur:
                return "sb/bg/bwb.jpg";

            case BackgroundType.BlackWhiteBlurWithoutCharacter:
                return "sb/bg/bwb2.jpg";

            case BackgroundType.RadialBlur:
                return "sb/bg/rb.jpg";

            case BackgroundType.BlackWhiteRadialBlur:
                return "sb/bg/bwrb.jpg";

            default:
                throw new Exception("Invalid background type");
        }
    }

    private float GetScaleRatio(bool fit = false)
    {
        float ratio = 854f / BackgroundBitmap.Width;

        if (fit)
        {
            ratio = Math.Min(ratio, 480f / BackgroundBitmap.Height);
        }

        return ratio;
    }

    private KeyframedValue<float>[] GetKeyframedValues(double startTime, double endTime, int barCount)
    {
        KeyframedValue<float>[] keyframes = new KeyframedValue<float>[barCount];
        for (int i = 0; i < barCount; i++)
        {
            keyframes[i] = new KeyframedValue<float>(null);
        }

        double fftTimeStep = GetBeatDuration(startTime) / BeatDivisor;
        double fftOffset = fftTimeStep * 0.2;
        for (double time = startTime; time < endTime; time += fftTimeStep)
        {
            float[] fft = GetFft(time + fftOffset, barCount, null, FftEasing);
            for (int i = 0; i < barCount; i++)
            {
                float height = (float)Math.Log10(1 + fft[i] * LogScale) * Scale.Y;
                if (height < MinimalHeight)
                {
                    height = MinimalHeight;
                }

                keyframes[i].Add(time, height);
            }
        }

        return keyframes;
    }

    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;
}