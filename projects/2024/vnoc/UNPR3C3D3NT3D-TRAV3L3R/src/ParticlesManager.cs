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

public class ParticlesManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        GenerateMovingRightParticles(48000, AudioDuration + 1140);
        GenerateMovingRightParticles(48000, AudioDuration + 1140, "sb/e/c4.png", 32, 5200);

        GenerateMovingOutParticlesBackground(257150, 268007, 156, 1200);
    }

    private void GenerateMovingRightParticles(double startTime, double endTime, string filePath = "sb/e/d.png", int particleCount = 64, double lifetime = 4000)
    {
        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Floor(duration / lifetime));

        for (int i = 0; i < particleCount; i++)
        {
            double spawnAngle = Random(Math.PI * 2);
            float spawnDistance = (float)(520 * Math.Sqrt(Random(1f)));

            double moveAngle = Random(-Math.PI / 6, Math.PI / 6);
            double moveSpeed = Random(36, 64d);
            double moveDistance = moveSpeed * lifetime * 0.001;

            Vector2 startPosition = new Vector2(100, 240) + new Vector2((float)Math.Cos(spawnAngle), (float)Math.Sin(spawnAngle)) * spawnDistance;
            Vector2 endPosition = startPosition + new Vector2((float)(Math.Cos(moveAngle) * moveDistance), (float)(Math.Sin(moveAngle) * moveDistance));

            double loopDuration = duration / loopCount;
            double spriteStartTime = startTime + (i * loopDuration) / particleCount;
            double spriteEndTime = spriteStartTime + loopDuration * loopCount;

            OsbSprite sprite = GetLayer("moving-right").CreateSprite(filePath, OsbOrigin.Centre, startPosition);

            sprite.Scale(spriteStartTime, Random(0.02, 0.05));
            sprite.Fade(spriteStartTime, Random(0.5, 0.95));
            sprite.Color(spriteStartTime, Beatmap.ComboColors.ElementAt(Random(Beatmap.ComboColors.Count())));
            sprite.Additive(spriteStartTime, spriteEndTime);

            sprite.StartLoopGroup(spriteStartTime, loopCount);
            sprite.MoveX(0, loopDuration, startPosition.X, endPosition.X);
            sprite.MoveY(0, loopDuration, startPosition.Y, endPosition.Y);
            sprite.Fade(0, loopDuration * 0.2, 0, 1);
            sprite.Fade(loopDuration * 0.8, loopDuration, 1, 0);
            sprite.EndGroup();
        }
    }

    private void GenerateMovingOutParticlesBackground(double startTime, double endTime, int particleCount = 128, double lifetime = 4000)
    {
        double duration = endTime - startTime;
        int loopCount = Math.Max(1, (int)Math.Floor(duration / lifetime));

        for (int i = 0; i < particleCount; i++)
        {
            double moveAngle = Random(-Math.PI * 2, Math.PI * 2);
            double moveSpeed = Random(520, 640d);
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
}