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

public class CreditsManager : StoryboardObjectGenerator
{
    [Configurable] public bool ClientSize = false;

    private readonly string LightFontWeightPath = "assets/fonts/Torus-Light.otf";
    private readonly string RegularFontWeightPath = "assets/fonts/Torus-Regular.otf";
    private readonly string HeavyFontWeightPath = "assets/fonts/Torus-Heavy.otf";
    private readonly string FigeronaBlackWeightPath = "assets/fonts/Figerona-Black.ttf";

    private readonly string LightFontWeightDirectory = "sb/f/torus/light";
    private readonly string RegularFontWeightDirectory = "sb/f/torus/regular";
    private readonly string HeavyFontWeightDirectory = "sb/f/torus/heavy";
    private readonly string FigeronaBlackWeightDirectory = "sb/f/figerona/black";

    private readonly float RoleFontScale = 0.26f;
    private readonly float BigUsernameFontScale = 0.2f;
    private readonly float SmallUsernameFontScale = 0.12f;
    private readonly float TitleFontScale = 0.36f;

    private static string LetterPool = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private OsbSprite[] RegularLetterSprites = new OsbSprite[LetterPool.Length];
    private OsbSprite[] HeavyLetterSprites = new OsbSprite[LetterPool.Length];


    public override void Generate()
    {
        FontGenerator lightFontWeightFont = SetupFont(LightFontWeightPath, LightFontWeightDirectory);
        FontGenerator regularFontWeightFont = SetupFont(RegularFontWeightPath, RegularFontWeightDirectory);
        FontGenerator heavyFontWeightFont = SetupFont(HeavyFontWeightPath, HeavyFontWeightDirectory);
        FontGenerator figeronaBlackWeightFont = SetupFont(FigeronaBlackWeightPath, FigeronaBlackWeightDirectory);

        RegularLetterSprites = InitializeLetterSprites(regularFontWeightFont);
        HeavyLetterSprites = InitializeLetterSprites(heavyFontWeightFont);

        GenerateLine("Vietnam osu! Championship 2024", 101714, 104000, heavyFontWeightFont, new Vector2(320, 225), 0.3f);
        GenerateLine("Knockout Week 3 Tiebreaker", 101714, 104000, lightFontWeightFont, new Vector2(320, 255), 0.3f);

        OsbSprite logo = GetLayer("").CreateSprite("sb/l/kagetora.png", OsbOrigin.Centre, new Vector2(320, 230));
        logo.ScaleVec(OsbEasing.OutExpo, 104000, 106285, 0.4, 0.4, 0.16, 0.16);
        logo.ScaleVec(OsbEasing.InExpo, 112000, 113142, 0.16, 0.16, 0, 0.16);

        GenerateLine("UNPR3C3D3NT3D TRAV3L3R", 108571, 113142, figeronaBlackWeightFont, new Vector2(320, 275), 0.4f);

        GenerateRoleLine("COMPOSER", 341150, 350293, heavyFontWeightFont, new Vector2(0, 200));
        AddUser("KAGETORA.", "sb/a/kagetora.png", 341150, 350293, new Vector2(100, 300), 148, regularFontWeightFont, BigUsernameFontScale);

        GenerateRoleLine("MAPPERS", 342293, 350293, heavyFontWeightFont, new Vector2(-20, 70));

        Dictionary<string, string> mappers = new Dictionary<string, string>
        {
            { "ALVEARIA", "sb/a/6248691.jpg" },
            { "DUCKY-", "sb/a/9351565.jpg" },
            { "VERMASIUM", "sb/a/11106442.png" },
            { "YUMERIOS", "sb/a/11681430.jpg" },
            { "- REM -", "sb/a/10489063.jpg" },
            { "PHO", "sb/a/3624692.png" },
            { "[BOY]DALAT", "sb/a/8266808.jpg" },
            { "HOAQ", "sb/a/7696512.jpg" }
        };

        float positionX = 20;
        foreach (KeyValuePair<string, string> mapper in mappers)
        {
            AddUser(mapper.Key, mapper.Value, 342293, 350293, new Vector2(positionX, 120), 48, regularFontWeightFont, SmallUsernameFontScale);

            positionX += 700f / mappers.Count;
        }


        GenerateRoleLine("HITSOUNDER", 343436, 350293, heavyFontWeightFont, new Vector2(230, 200));
        AddUser("VERMASIUM", "sb/a/11106442.png", 343436, 350293, new Vector2(270, 250), 48, regularFontWeightFont, SmallUsernameFontScale);

        GenerateRoleLine("STORYBOARDER", 343436, 350293, heavyFontWeightFont, new Vector2(430, 200));
        AddUser("NINGGUANG", "sb/a/8500334.jpg", 343436, 350293, new Vector2(470, 250), 48, regularFontWeightFont, SmallUsernameFontScale);

        GenerateRoleLine("ARTIST", 344579, 350293, heavyFontWeightFont, new Vector2(230, 330));
        AddUser("ZEKLEWA", "sb/a/3314158.png", 344579, 350293, new Vector2(270, 380), 48, regularFontWeightFont, SmallUsernameFontScale);

        GenerateRoleLine("DESIGNER", 344579, 350293, heavyFontWeightFont, new Vector2(430, 330));
        AddUser("SUGOSUGIII", "sb/a/15118952.jpg", 344579, 350293, new Vector2(470, 380), 48, regularFontWeightFont, SmallUsernameFontScale);

        AddTournamentCredits("sb/c/vnoc5-staff.png", 350293, AudioDuration + 1140);
    }

    private void GenerateLine(string sentence, double startTime, double endTime, FontGenerator font, Vector2 center, float fontSize)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0;
        float lineHeight = 0;

        foreach (var letter in sentence)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += (texture.BaseWidth * 1.25f) * fontSize;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontSize);
        }

        float letterX = center.X - lineWidth * 0.5f;

        foreach (var letter in sentence)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, center.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * fontSize;
                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime, fontSize);
                sprite.MoveX(OsbEasing.OutExpo, startTime, endTime - beatDuration * 3, 320, position.X);
                sprite.MoveX(OsbEasing.InExpo, endTime - beatDuration * 3, endTime, position.X, 320);
                sprite.Fade(startTime, startTime + beatDuration * 2, 0, 1);
                sprite.Fade(OsbEasing.InExpo, endTime - beatDuration * 2, endTime, 1, 0);
            }

            letterX += (texture.BaseWidth * 1.25f) * fontSize;
        }
    }

    private void GenerateRoleLine(string role, double startTime, double endTime, FontGenerator font, Vector2 anchorPoint)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0;
        float lineHeight = 0;

        foreach (char letter in role)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += (texture.BaseWidth * 1.25f) * RoleFontScale;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * RoleFontScale);
        }

        float letterX = anchorPoint.X;

        OsbSprite box = GetLayer("role").CreateSprite("sb/e/p.png", OsbOrigin.CentreLeft, new Vector2(anchorPoint.X - 12, anchorPoint.Y + lineHeight * 0.32f));
        box.ScaleVec(OsbEasing.OutExpo, startTime, startTime + beatDuration * 2, 0, lineHeight * 0.75, lineWidth + 32, lineHeight * 0.75);
        box.Color(startTime, "#fece2c");
        box.Fade(startTime, 0.75);

        box.StartLoopGroup(endTime - beatDuration * 2, 4);
        box.Fade(0, beatDuration * 0.5, 0.75, 0);
        box.EndGroup();

        double timestep = beatDuration / 4;

        for (int i = 0; i < role.Length; ++i)
        {
            char letter = role[i];
            FontTexture texture = font.GetTexture(letter.ToString());

            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, anchorPoint.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * RoleFontScale;

                int currentNumber = PickLetter(0, LetterPool.Length);
                double duration = timestep * ((i + 2) * 2);
                float opacity = 0;

                for (double time = startTime; time < startTime + duration - 10; time += timestep)
                {
                    int newNumber = PickLetter(currentNumber, LetterPool.Length);
                    SpawnLetter(HeavyLetterSprites, newNumber, time, time + timestep, position, RoleFontScale, opacity);

                    currentNumber = newNumber;
                    opacity += 1.0f / ((i + 2) * 2);
                }

                OsbSprite sprite = GetLayer("role").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime + duration, RoleFontScale);
                sprite.Fade(startTime + duration, 1);

                sprite.StartLoopGroup(endTime - beatDuration * 2, 4);
                sprite.Fade(0, beatDuration * 0.5, 1, 0);
                sprite.EndGroup();
            }

            letterX += (texture.BaseWidth * 1.25f) * RoleFontScale;
        }
    }

    private void GenerateUsernameLine(FontGenerator font, string sentence, double startTime, double endTime, Vector2 center, float fontSize)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        float lineWidth = 0;
        float lineHeight = 0;

        foreach (char letter in sentence)
        {
            FontTexture texture = font.GetTexture(letter.ToString());
            lineWidth += (texture.BaseWidth * 1.2f) * fontSize;
            lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontSize);
        }

        float letterX = center.X - lineWidth * 0.5f;
        double timestep = beatDuration / 4;

        for (int i = 0; i < sentence.Length; ++i)
        {
            char letter = sentence[i];
            FontTexture texture = font.GetTexture(letter.ToString());

            if (!texture.IsEmpty)
            {
                Vector2 position = new Vector2(letterX, center.Y + lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * fontSize;

                int currentNumber = PickLetter(0, LetterPool.Length);
                double duration = timestep * ((i + 2) * 2);
                float opacity = 0;

                for (double time = startTime; time < startTime + duration - 10; time += timestep)
                {
                    int newNumber = PickLetter(currentNumber, LetterPool.Length);
                    SpawnLetter(RegularLetterSprites, newNumber, time, time + timestep, position, fontSize, opacity);

                    currentNumber = newNumber;
                    opacity += 1.0f / ((i + 2) * 2);
                }

                OsbSprite sprite = GetLayer("").CreateSprite(texture.Path, OsbOrigin.Centre, position);

                sprite.Scale(startTime + duration, fontSize);
                sprite.Fade(startTime + duration, 1);

                sprite.StartLoopGroup(endTime - beatDuration * 2, 4);
                sprite.Fade(0, beatDuration * 0.5, 1, 0);
                sprite.EndGroup();
            }

            letterX += (texture.BaseWidth * 1.2f) * fontSize;
        }
    }

    private void AddUser(string username, string avatar, double startTime, double endTime, Vector2 position, float avatarSize, FontGenerator font, float fontSize)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Bitmap bitmap = GetMapsetBitmap(avatar);
        OsbSprite sprite = GetLayer("").CreateSprite(avatar, OsbOrigin.Centre, position);

        sprite.Scale(startTime, avatarSize / bitmap.Height);

        sprite.StartLoopGroup(startTime, 4);
        sprite.Fade(0, beatDuration * 0.5, 0, 1);
        sprite.EndGroup();

        sprite.StartLoopGroup(endTime - beatDuration * 2, 4);
        sprite.Fade(0, beatDuration * 0.5, 1, 0);
        sprite.EndGroup();

        GenerateUsernameLine(font, username, startTime, endTime, new Vector2(position.X, position.Y + avatarSize * 0.5f + 2), fontSize);
    }

    private void AddTournamentCredits(string filepath, double startTime, double endTime)
    {
        double beatDuration = Beatmap.GetTimingPointAt((int)startTime).BeatDuration;

        Bitmap bitmap = GetMapsetBitmap(filepath);
        OsbSprite sprite = GetLayer("tournament-credits").CreateSprite(filepath);

        sprite.Scale(OsbEasing.OutExpo, startTime, startTime + beatDuration * 4, (480.0f / bitmap.Height) * 2f, 480.0f / bitmap.Height);
        sprite.Scale(OsbEasing.InSine, startTime + beatDuration * 4, endTime, sprite.ScaleAt(startTime + beatDuration * 4).X, (480.0f / bitmap.Height) * 0.96f);
        sprite.Fade(startTime, 1);
        sprite.Fade(endTime, 0);
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

    private OsbSprite[] InitializeLetterSprites(FontGenerator font)
    {
        OsbSprite[] sprites = new OsbSprite[LetterPool.Length];

        for (int i = 0; i < LetterPool.Length; i++)
        {
            FontTexture texture = font.GetTexture(LetterPool[i].ToString());

            if (!texture.IsEmpty)
            {
                sprites[i] = GetLayer("").CreateSprite(texture.Path);
            }
        }

        return sprites;
    }

    private int PickLetter(int current, int max)
    {
        int next = Random(0, max);

        while (next == current)
        {
            next = Random(0, max);
        }

        return next;
    }

    private void SpawnLetter(OsbSprite[] sprites, int index, double startTime, double endTime, Vector2 position, float fontScale, float opacity)
    {
        OsbSprite sprite = GetLayer("").CreateSprite(sprites[index].TexturePath, OsbOrigin.Centre, position);

        sprite.Scale(startTime, fontScale);
        sprite.Fade(startTime, endTime, opacity, opacity + 0.5);
    }
}