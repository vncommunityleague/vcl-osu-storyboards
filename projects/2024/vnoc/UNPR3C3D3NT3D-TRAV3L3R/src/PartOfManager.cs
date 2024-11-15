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

public class PartOfManager : StoryboardObjectGenerator
{
    private readonly string LightFontWeightPath = "assets/fonts/Torus-Light.otf";
    private readonly string RegularFontWeightPath = "assets/fonts/Torus-Regular.otf";
    private readonly string HeavyFontWeightPath = "assets/fonts/Torus-Heavy.otf";

    private readonly string LightFontWeightDirectory = "sb/f/torus/light";
    private readonly string RegularFontWeightDirectory = "sb/f/torus/regular";
    private readonly string HeavyFontWeightDirectory = "sb/f/torus/heavy";

    private readonly float InfoLineFontScale = 0.15f;
    private readonly float UsernameFontScale = 0.3f;
    private readonly float GenreFontScale = 0.22f;

    public override void Generate()
    {
        FontGenerator regularFontWeightFont = SetupFont(RegularFontWeightPath, RegularFontWeightDirectory);
        FontGenerator heavyFontWeightFont = SetupFont(HeavyFontWeightPath, HeavyFontWeightDirectory);
        FontGenerator lightFontWeightFont = SetupFont(LightFontWeightPath, LightFontWeightDirectory);

        GenerateInfoLine("section of", 1142, 341150, heavyFontWeightFont,  new Vector2(550, 352));
        GenerateInfoLine("genre", 1142, 341150, heavyFontWeightFont,  new Vector2(550, 395));

        GenerateMapperPart("Alvearia", "sb/a/6248691.jpg", 1142, 30857, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Ducky-", "sb/a/9351565.jpg", 30857, 49142, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Vermasium", "sb/a/11106442.png", 49142, 67428, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Yumerios", "sb/a/11681430.jpg", 67428, 85714, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("- Rem -", "sb/a/10489063.jpg", 85714, 122285, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Yumerios", "sb/a/11681430.jpg", 122285, 141220, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Pho", "sb/a/3624692.png", 141220, 167521, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Ducky-", "sb/a/9351565.jpg", 167521, 193779, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Alvearia", "sb/a/6248691.jpg", 193779, 229725, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("[Boy]DaLat", "sb/a/8266808.jpg", 229725, 268007, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Pho", "sb/a/3624692.png", 268007, 286293, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Vermasium", "sb/a/11106442.png", 286293, 304579, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("- Rem -", "sb/a/10489063.jpg", 304579, 322865, lightFontWeightFont, new Vector2(550, 368));
        GenerateMapperPart("Ducky-", "sb/a/9351565.jpg", 322865, 341150, lightFontWeightFont, new Vector2(550, 368));

        GenerateGenreLine("(Intro)", 1142, 21714, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Psycore", 21714, 67428, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Moombahton", 67428, 85714, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Glitch Hop", 85714, 101714, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("(Break)", 101714, 122285, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("(SpeedUp)", 122285, 137932, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Hi-Tech Full-on", 137932, 167521, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Speed Garage", 167521, 193779, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Hardstyle (Reverse Bass)", 193779, 206527, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Schranz", 206527, 229725, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("(Break)", 229725, 248579, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("J-core", 248579, 304579, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Chiptune", 304579, 322865, lightFontWeightFont, new Vector2(550, 411));
        GenerateGenreLine("Speedcore", 322865, 341150, lightFontWeightFont, new Vector2(550, 411));
    }

    private void GenerateInfoLine(string sentence, double startTime, double endTime, FontGenerator font, Vector2 anchorPoint)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0f;
        float lineHeight = 0f;

        foreach (char letter in sentence)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += texture.BaseWidth * 1.1f * InfoLineFontScale;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * InfoLineFontScale);
        }

        float letterX = anchorPoint.X - lineWidth;

        foreach (char letter in sentence)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, anchorPoint.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * InfoLineFontScale;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime, InfoLineFontScale);
                sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + beatDuration * 3, anchorPoint.X, position.X);
                sprite.MoveX(OsbEasing.InExpo, endTime - beatDuration * 3, endTime, position.X, anchorPoint.X);
                sprite.Fade(startTime, startTime + beatDuration * 1.5, 0, 1);
                sprite.Fade(OsbEasing.InExpo, endTime - beatDuration * 1.5, endTime, 1, 0);
            }

            letterX += texture.BaseWidth * 1.1f * InfoLineFontScale;
        }
    }

    private void GenerateMapperPart(string name, string avatar, double startTime, double endTime, FontGenerator font, Vector2 anchorPoint)
    {
        AddMapperAvatar(avatar, startTime, endTime);

        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0f;
        float lineHeight = 0f;
        foreach (char letter in name)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += texture.BaseWidth * UsernameFontScale;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * UsernameFontScale);
        }

        OsbSprite box = GetLayer("").CreateSprite("sb/e/p.png", OsbOrigin.BottomRight, new Vector2(anchorPoint.X + 8, anchorPoint.Y + lineHeight * 0.5f + 2));

        box.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 0, lineHeight * 0.5, lineWidth + 28, lineHeight * 0.5);
        box.ScaleVec(OsbEasing.InExpo, endTime - beatDuration * 2, endTime, lineWidth + 28, lineHeight * 0.5, 0, lineHeight * 0.5);
        box.Fade(startTime, 0.36);
        box.Color(startTime, Color.Black);

        float letterX = anchorPoint.X - lineWidth;

        foreach (char letter in name)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, anchorPoint.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * UsernameFontScale;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime, UsernameFontScale);
                sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + beatDuration * 3, anchorPoint.X, position.X);
                sprite.MoveX(OsbEasing.InExpo, endTime - beatDuration * 3, endTime, position.X, anchorPoint.X);
                sprite.Fade(startTime, startTime + beatDuration * 1.5, 0, 1);
                sprite.Fade(OsbEasing.InExpo, endTime - beatDuration * 1.5, endTime, 1, 0);
            }

            letterX += texture.BaseWidth * UsernameFontScale;
        }
    }

    private void GenerateGenreLine(string name, double startTime, double endTime, FontGenerator font, Vector2 anchorPoint)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0f;
        float lineHeight = 0f;
        foreach (char letter in name)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += texture.BaseWidth * GenreFontScale;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * GenreFontScale);
        }

        OsbSprite box = GetLayer("").CreateSprite("sb/e/p.png", OsbOrigin.BottomRight, new Vector2(anchorPoint.X + 8, anchorPoint.Y + lineHeight * 0.5f + 2));

        box.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 0, lineHeight * 0.5, lineWidth + 28, lineHeight * 0.5);
        box.ScaleVec(OsbEasing.InExpo, endTime - beatDuration * 2, endTime, lineWidth + 28, lineHeight * 0.5, 0, lineHeight * 0.5);
        box.Fade(startTime, 0.36);
        box.Color(startTime, Color.Black);

        float letterX = anchorPoint.X - lineWidth;

        foreach (char letter in name)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, anchorPoint.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * GenreFontScale;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime, GenreFontScale);
                sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + beatDuration * 3, anchorPoint.X, position.X);
                sprite.MoveX(OsbEasing.InExpo, endTime - beatDuration * 3, endTime, position.X, anchorPoint.X);
                sprite.Fade(startTime, startTime + beatDuration * 1.5, 0, 1);
                sprite.Fade(OsbEasing.InExpo, endTime - beatDuration * 1.5, endTime, 1, 0);
            }

            letterX += texture.BaseWidth * GenreFontScale;
        }
    }

    private void AddMapperAvatar(string avatar, double startTime, double endTime)
    {
        double beatDuration = GetBeatDuration(startTime);

        Bitmap bitmap = GetMapsetBitmap(avatar);
        OsbSprite sprite = GetLayer("").CreateSprite(avatar, OsbOrigin.Centre, new Vector2(600, 385));

        sprite.Scale(startTime, 72f / bitmap.Height);
        sprite.MoveX(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, 540, 600);
        sprite.MoveX(OsbEasing.InExpo, endTime - beatDuration * 1.5, endTime, 600, 540);
        sprite.Fade(OsbEasing.OutCirc, startTime, startTime + beatDuration * 2, 0, 1);
        sprite.Fade(OsbEasing.Out, endTime - beatDuration * 1.5, endTime, 1, 0);
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

    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;
}