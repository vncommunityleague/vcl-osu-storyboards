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

namespace StorybrewScripts
{
    public class SystemText : StoryboardObjectGenerator
    {
        private readonly string FontPath = "assets/fonts/JetBrainsMono-Light.ttf";

        private readonly string FontDirectory = "sb/f/jetbrainsmono/light";

        public override void Generate()
        {
            FontGenerator font = SetupFont();

            GenerateLine("booting sys_vn_community_league ............. good", 23428, 30285, font, new Vector2(320, 270), 0.16f);
            GenerateLine("verifying serenity keychip .................. good", 25178, 30285, font, new Vector2(320, 290), 0.16f);
            GenerateLine("connecting to vnoc_vcl network .............. good", 26928, 30285, font, new Vector2(320, 310), 0.16f);

            GenerateLine("getting packages from vncommunityleague registry .... good", 229725, 248579, font, new Vector2(320, 170), 0.16f);
            GenerateLine("initializing selenadia modules ...................... good", 231901, 248579, font, new Vector2(320, 190), 0.16f);
            GenerateLine("cd $HOME/vncommunityleague/vnoc-2024/ ............... good", 234113, 248579, font, new Vector2(320, 210), 0.16f);
            GenerateLine("bash ./UNPR3C3D3NT3D_TRAV3L3R-FiNALE.sh ..................", 236288, 248579, font, new Vector2(320, 230), 0.16f);
            GenerateLine("locating next destination ................................", 238463, 248579, font, new Vector2(320, 250), 0.16f);
            GenerateLine("[##############....................................]  27.9%", 240643, 248579, font, new Vector2(320, 270), 0.16f);
            GenerateLine("[################################..................]  72.7%", 242801, 248579, font, new Vector2(320, 290), 0.16f);
            GenerateLine("[#################################################.]  99.9%", 244972, 248579, font, new Vector2(320, 310), 0.16f);

            GenerateLine("DESTINATION UNKNOWN", 247436, 248579, font, new Vector2(320, 240), 0.32f);
        }

        private void GenerateLine(string sentence, double startTime, double endTime, FontGenerator font, Vector2 center, float fontSize)
        {
            double beatDuration = GetBeatDuration(startTime);

            float lineWidth = 0;
            float lineHeight = 0;

            foreach (var letter in sentence)
            {
                FontTexture texture = font.GetTexture(letter.ToString());
                lineWidth += texture.BaseWidth * fontSize;
                lineHeight = Math.Max(lineHeight, texture.BaseHeight * fontSize);
            }

            float letterX = center.X - lineWidth * 0.5f;
            double time = startTime;

            StoryboardLayer layer = sentence == "DESTINATION UNKNOWN" ? GetLayer("error") : GetLayer("");
            double divisor = sentence == "DESTINATION UNKNOWN" ? 12 : 8;
            double timestep = beatDuration * 0.2;

            foreach (char letter in sentence)
            {
                FontTexture texture = font.GetTexture(letter.ToString());
                if (!texture.IsEmpty)
                {
                    Vector2 position = new Vector2(letterX, center.Y - lineHeight * 0.5f) + texture.OffsetFor(OsbOrigin.Centre) * fontSize;

                    if (sentence == "DESTINATION UNKNOWN")
                    {
                        Color[] rgb = new Color[] { Color.Red, Color.Green, Color.Blue };
                        for (int i = 0; i < 3; i++)
                        {
                            OsbSprite color = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);

                            color.Scale(endTime - beatDuration, fontSize);
                            color.Color(endTime - beatDuration, rgb[i]);
                            color.Additive(endTime - beatDuration, endTime);

                            for (int j = 0; j < 5; j++)
                            {
                                color.Move(OsbEasing.Out, endTime - timestep * j, endTime - timestep * (j + 1), position, new Vector2(position.X + Random(-3f, 3f), position.Y + Random(-3f, 3f)));
                            }
                        }
                    }

                    OsbSprite sprite = layer.CreateSprite(texture.Path, OsbOrigin.Centre, position);

                    sprite.Scale(time, fontSize);
                    sprite.Additive(endTime - beatDuration, endTime);

                    if (startTime > 247436)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            sprite.Move(OsbEasing.Out, endTime - timestep * j, endTime - timestep * (j + 1), position, new Vector2(position.X + Random(-3f, 3f), position.Y + Random(-3f, 3f)));
                        }
                    }
                }

                letterX += texture.BaseWidth * fontSize;
                time += beatDuration / divisor;
            }
        }

        private FontGenerator SetupFont() => LoadFont(FontDirectory, new FontDescription()
        {
            FontPath = FontPath,
            FontSize = 60,
            Color = Color4.White
        });

        private double GetBeatDuration(double time)
            => Beatmap.GetTimingPointAt((int)time).BeatDuration;
    }
}