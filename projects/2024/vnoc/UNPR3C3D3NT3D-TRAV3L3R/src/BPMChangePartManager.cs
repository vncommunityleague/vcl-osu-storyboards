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
using System.Globalization;
using System.Linq;

namespace StorybrewScripts;

public class BPMChangePartManager : StoryboardObjectGenerator
{
    private readonly string FontPath = "assets/fonts/Torus-Bold.otf";
    private readonly string FontPath2 = "assets/fonts/Torus-Thin.otf";

    private readonly string FontDirectory = "sb/f/torus/bold";
    private readonly string FontDirectory2 = "sb/f/torus/thin";
    private readonly float FontScale = 0.4f;

    private FontGenerator Font;
    private FontGenerator Font2;

    public override void Generate()
    {
        Font = SetupFont(FontPath, FontDirectory);
        Font2 = SetupFont(FontPath2, FontDirectory2);

        GenerateBpmText(122285, 137932);
        GenerateBpmChangeText(122285, 137932);
        GenerateRing(122285, 137932);
    }

    private void GenerateBpmText(double startTime, double endTime)
    {
        string text = "BPM";
        float letterX = 255;

        foreach (char letter in text)
        {
            FontTexture texture = Font2.GetTexture(letter.ToString());

            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, 220) + texture.OffsetFor(OsbOrigin.CentreRight) * FontScale;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.CentreRight, position);

                sprite.Scale(startTime, FontScale);
                sprite.Fade(startTime, 1);
                sprite.Fade(endTime, 0);
            }

            letterX += Font2.GetTexture(letter.ToString()).BaseWidth * FontScale;
        }
    }

    private void GenerateBpmChangeText(double startTime, double endTime)
    {
        ControlPoint[] timingPoints = Beatmap.TimingPoints.Where((point) => startTime <= point.Offset && point.Offset <= endTime).ToArray();
        ControlPoint lastTimingPoint = Beatmap.GetTimingPointAt((int)(startTime - 32));
        string lastTimingPointBpm = Math.Round(lastTimingPoint.Bpm).ToString(CultureInfo.InvariantCulture);

        float letterX2 = 325;
        float delay2 = 0;

        List<OsbSprite> sprites = new List<OsbSprite>();

        foreach (char digit in lastTimingPointBpm)
        {
            FontTexture texture = Font.GetTexture(digit.ToString());

            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX2, 220) + texture.OffsetFor(OsbOrigin.CentreRight) * FontScale;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.CentreRight, position);

                sprite.Scale(startTime, FontScale);
                sprite.MoveY(OsbEasing.OutCubic, startTime, startTime + 250, position.Y, position.Y + 20);
                sprite.Fade(OsbEasing.Out, startTime, startTime + 250, 1, 0);
                sprites.Add(sprite);
            }

            letterX2 += (Font.GetTexture(digit.ToString()).BaseWidth * 1.25f) * FontScale;
        }

        for (int i = 0; i < timingPoints.Length; ++i)
        {
            ControlPoint currentTimingPoint = timingPoints[i];
            string currentBpm = Math.Round(currentTimingPoint.Bpm).ToString(CultureInfo.InvariantCulture);
            double beatDuration = currentTimingPoint.BeatDuration;

            float letterX = 325;
            double delay = 0;

            for (int digitIndex = 0; digitIndex < currentBpm.Length; ++digitIndex)
            {
                if (currentBpm[digitIndex] != lastTimingPointBpm[digitIndex])
                {
                    FontTexture texture = Font.GetTexture(currentBpm[digitIndex].ToString());

                    if (!texture.IsEmpty)
                    {
                        Vector2 position = new Vector2(letterX, 220) + texture.OffsetFor(OsbOrigin.CentreRight) * FontScale;
                        OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.CentreRight, position);

                        sprite.Scale(currentTimingPoint.Offset, FontScale);
                        sprite.MoveY(OsbEasing.OutCubic, currentTimingPoint.Offset + delay, currentTimingPoint.Offset + delay + beatDuration * 0.25, position.Y - 20, position.Y);
                        sprite.Fade(OsbEasing.Out, currentTimingPoint.Offset + delay, currentTimingPoint.Offset + delay + beatDuration * 0.25, 0, 1);

                        if (i < timingPoints.Length - 1 && currentBpm[digitIndex] != timingPoints[i + 1].Bpm.ToString(CultureInfo.InvariantCulture)[digitIndex])
                        {
                            sprite.MoveY(OsbEasing.OutCubic, timingPoints[i + 1].Offset - beatDuration * 0.25, timingPoints[i + 1].Offset, position.Y, position.Y + 20);
                            sprite.Fade(OsbEasing.Out, timingPoints[i + 1].Offset - beatDuration * 0.25, timingPoints[i + 1].Offset, 1, 0);
                        }

                        delay += 25;
                        sprites[digitIndex] = sprite;
                    }
                }
                else
                {
                    if (i < timingPoints.Length - 1)
                    {
                        sprites[digitIndex].Fade(currentTimingPoint.Offset, timingPoints[i + 1].Offset, 1, 1);
                    }
                }

                letterX += (Font.GetTexture(currentBpm[digitIndex].ToString()).BaseWidth * 1.25f) * FontScale;
            }

            lastTimingPointBpm = currentBpm;
        }

        foreach (OsbSprite sprite in sprites)
        {
            sprite.Fade(endTime, 1);
        }
    }

    private void GenerateRing(double startTime, double endTime)
    {
        double moveDuration = 4000;

        double tiltAngle = Math.PI / 3;
        double time = startTime;

        for (double angle = 0; angle < 2 * Math.PI; angle += Math.PI / 32)
        {
            Vector2 position = new Vector2(
                (float)(Constant.CenterPosition.X + Math.Cos(angle + tiltAngle) * 140),
                (float)(Constant.CenterPosition.Y + Math.Sin(angle) * 80)
            );
            Vector2 moveRadius = new Vector2(position.X - Constant.CenterPosition.X, position.Y - Constant.CenterPosition.Y);
            OsbSprite sprite = GetLayer("ring").CreateSprite("sb/e/d.png", OsbOrigin.Centre, position);

            sprite.Scale(time, 0.05);
            sprite.StartLoopGroup(startTime, 32);
            sprite.MoveY(OsbEasing.InSine, 0, moveDuration * 0.25, position.Y, position.Y - moveRadius.Y);
            sprite.MoveY(OsbEasing.OutSine, moveDuration * 0.25, moveDuration * 0.5, position.Y - moveRadius.Y, position.Y - 2 * moveRadius.Y);
            sprite.MoveY(OsbEasing.InSine, moveDuration * 0.5, moveDuration * 0.75, position.Y - 2 * moveRadius.Y, position.Y - moveRadius.Y);
            sprite.MoveY(OsbEasing.OutSine, moveDuration * 0.75, moveDuration, position.Y - moveRadius.Y, position.Y);
            sprite.EndGroup();
            sprite.StartLoopGroup(time, 120);
            sprite.Scale(0, 500, 0.1, 0.01);
            sprite.EndGroup();
            sprite.Fade(time, 1);
            sprite.Fade(endTime, 0);

            time += 8;
        }
    }

    private FontGenerator SetupFont(string fontPath, string fontDirectory)
    {
        return LoadFont(fontDirectory, new FontDescription()
        {
            FontPath = fontPath,
            FontSize = 60,
            Color = Color4.White
        });
    }
}