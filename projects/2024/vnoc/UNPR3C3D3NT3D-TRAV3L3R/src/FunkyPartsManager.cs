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

public class FunkyPartsManager : StoryboardObjectGenerator
{
    public override void Generate()
    {
        GenerateFunkyTransition1();
        GenerateFunkyTransition2();
        GenerateFunkyTransition3();
        GenerateFunkyPart1();
    }

    private void GenerateFunkyTransition1()
    {
        OsbSprite sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png");

        sprite.Scale(OsbEasing.OutExpo, 83428, 83714, 12, 90);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/t3.png");

        sprite.Scale(OsbEasing.OutExpo, 83714, 83857, 0.36, 0.2);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/c4.png");

        sprite.Scale(OsbEasing.OutExpo, 83857, 84000, 0.2, 0.42);
        sprite.Fade(84571, 1);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/c.png");

        sprite.Scale(OsbEasing.OutExpo, 84000, 84142, 0.2, 0.36);
        sprite.Scale(OsbEasing.OutExpo, 84428, 84571, 0.36, 0.24);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(140, 240));

        sprite.Scale(OsbEasing.OutExpo, 84142, 84285, 0.08, 0.24);
        sprite.MoveX(OsbEasing.OutExpo, 84428, 84571, 140, 320);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(500, 240));

        sprite.Scale(OsbEasing.OutExpo, 84285, 84428, 0.08, 0.24);
        sprite.MoveX(OsbEasing.OutExpo, 84428, 84571, 500, 320);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/t.png");

        sprite.Scale(OsbEasing.OutExpo, 84571, 84714, 0.08, 0.24);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 84714, 84857, 0.32, 0.2);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(160, 240));

        sprite.ScaleVec(OsbEasing.OutBack, 84857, 84928, 24, 24, 32, 32);
        sprite.Rotate(84857, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(266.7f, 240));

        sprite.ScaleVec(OsbEasing.OutBack, 84928, 85000, 24, 24, 32, 32);
        sprite.Rotate(84928, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(426.4f, 240));

        sprite.ScaleVec(OsbEasing.OutBack, 85000, 85071, 24, 24, 32, 32);
        sprite.Rotate(85000, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(586.1f, 240));

        sprite.ScaleVec(OsbEasing.OutBack, 85071, 85142, 24, 24, 32, 32);
        sprite.Rotate(85071, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.TopLeft, new Vector2(270, 190));

        sprite.ScaleVec(85142, 100, 100);
        sprite.Rotate(OsbEasing.OutCirc, 85142, 85285, MathHelper.DegreesToRadians(120), 0);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.TopRight, new Vector2(370, 190));

        sprite.ScaleVec(85142, 100, 100);
        sprite.Rotate(OsbEasing.OutCirc, 85142, 85285, MathHelper.DegreesToRadians(120), 0);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.BottomRight, new Vector2(370, 290));

        sprite.ScaleVec(85142, 100, 100);
        sprite.Rotate(OsbEasing.OutCirc, 85142, 85285, MathHelper.DegreesToRadians(120), 0);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", OsbOrigin.BottomLeft, new Vector2(270, 290));

        sprite.ScaleVec(85142, 100, 100);
        sprite.Rotate(OsbEasing.OutCirc, 85142, 85285, MathHelper.DegreesToRadians(120), 0);

        sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutCirc, 85285, 85357, 100, 100, 100, 480);
        sprite.Fade(85714, 1);

        float height = 480f / 5;
        double time = 85357;

        for (int i = 0; i < 5; ++i)
        {
            for (int j = 0; j < 2; ++j)
            {
                sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", j % 2 == 0 ? OsbOrigin.CentreRight : OsbOrigin.CentreLeft, new Vector2(320 + (j % 2 == 0 ? -50 : 50), height * i + height / 2));

                sprite.ScaleVec(OsbEasing.OutExpo, time, 85714, 0, height, 377, height);
            }

            time += GetBeatDuration(time) * 0.25;
        }

        for (int i = 0; i < 2; ++i)
        {
            sprite = GetLayer("funky-transition-1").CreateSprite("sb/e/p.png", i % 2 == 0 ? OsbOrigin.CentreLeft : OsbOrigin.CentreRight, new Vector2(-107 + (i % 2 * 854), 240));

            sprite.ScaleVec(OsbEasing.OutExpo, time, time + GetBeatDuration(time) * 4, 427, 480, 0, 480);
        }
    }

    private void GenerateFunkyTransition2()
    {
        OsbSprite sprite = GetLayer("funky-part-2").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(220, 240));

        sprite.Scale(139576, 0.08);
        sprite.Rotate(139576, MathHelper.DegreesToRadians(45));
        sprite.Fade(139987, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(220, 240));

        sprite.Scale(139576, 24);
        sprite.Rotate(139576, MathHelper.DegreesToRadians(45));
        sprite.Fade(139987, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(320, 240));

        sprite.Scale(139987, 0.08);
        sprite.Rotate(139987, MathHelper.DegreesToRadians(45));
        sprite.Fade(140398, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(320, 240));

        sprite.Scale(139987, 24);
        sprite.Rotate(139987, MathHelper.DegreesToRadians(45));
        sprite.Fade(140398, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(420, 240));

        sprite.Scale(140398, 0.08);
        sprite.Rotate(140398, MathHelper.DegreesToRadians(45));
        sprite.Fade(140809, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(420, 240));

        sprite.Scale(140398, 24);
        sprite.Rotate(140398, MathHelper.DegreesToRadians(45));
        sprite.Fade(140809, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.CentreRight);

        sprite.ScaleVec(OsbEasing.OutExpo, 140809, 141220, 0, 480, 427, 480);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.CentreLeft);

        sprite.ScaleVec(OsbEasing.OutExpo, 140809, 141220, 0, 480, 427, 480);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.CentreLeft, new Vector2(-107, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 141220, 142042, 427, 480, 0, 480);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.CentreRight, new Vector2(747, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 141220, 142042, 427, 480, 0, 480);
    }

    private void GenerateFunkyTransition3()
    {
        OsbSprite sprite = GetLayer("funky-part-2").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(220, 240));

        sprite.Scale(192179, 0.08);
        sprite.Rotate(192179, MathHelper.DegreesToRadians(45));
        sprite.MoveX(192179, 500);
        sprite.MoveX(192578, 380);
        sprite.MoveX(192979, 260);
        sprite.MoveX(193378, 140);
        sprite.Fade(193779, 1);

        sprite = GetLayer("funky-part-2").CreateSprite("sb/e/p.png", OsbOrigin.Centre, new Vector2(220, 240));

        sprite.Scale(192179, 24);
        sprite.Rotate(192179, MathHelper.DegreesToRadians(45));
        sprite.MoveX(192179, 500);
        sprite.MoveX(192578, 380);
        sprite.MoveX(192979, 260);
        sprite.MoveX(193378, 140);
        sprite.Fade(193779, 1);


    }

    private void GenerateFunkyPart1()
    {
        double titleAngle = MathHelper.DegreesToRadians(45);

        // OsbSprite sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");
        //
        // sprite.ScaleVec(139576, 0.28, 0.5);
        // sprite.Rotate(139576, titleAngle);
        // sprite.ScaleVec(OsbEasing.OutExpo, 141015, 141220, sprite.ScaleAt(141015), 0.5, 0.5);
        // sprite.Rotate(OsbEasing.OutExpo, 141631, 142042, sprite.RotationAt(142042), 0);
        //
        // sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");
        //
        // sprite.ScaleVec(139576, 0.28, 0.5);
        // sprite.Rotate(139576, titleAngle);
        // sprite.Move(OsbEasing.OutExpo, 139576, 139987, Constant.CenterPosition, Constant.CenterPosition - new Vector2((float)(Math.Cos(titleAngle) * 60), (float)(Math.Sin(titleAngle) * 60)));
        // sprite.Move(OsbEasing.OutExpo, 140398, 140809, sprite.PositionAt(139987), Constant.CenterPosition - new Vector2((float)(Math.Cos(titleAngle) * 180), (float)(Math.Sin(titleAngle) * 180)));
        // sprite.Move(OsbEasing.OutExpo, 140809, 141015, sprite.PositionAt(140809), Constant.CenterPosition);
        //
        // sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");
        //
        // sprite.ScaleVec(139576, 0.28, 0.5);
        // sprite.Rotate(139576, titleAngle);
        // sprite.Move(OsbEasing.OutExpo, 139576, 139987, Constant.CenterPosition, Constant.CenterPosition + new Vector2((float)(Math.Cos(titleAngle) * 60), (float)(Math.Sin(titleAngle) * 60)));
        // sprite.Move(OsbEasing.OutExpo, 140398, 140809, sprite.PositionAt(139987), Constant.CenterPosition + new Vector2((float)(Math.Cos(titleAngle) * 180), (float)(Math.Sin(titleAngle) * 180)));
        // sprite.Move(OsbEasing.OutExpo, 140809, 141015, sprite.PositionAt(140809), Constant.CenterPosition);
        //
        // sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");
        //
        // sprite.ScaleVec(OsbEasing.OutExpo, 139987, 140398, 0, 1, 120, 1);
        // sprite.ScaleVec(OsbEasing.OutExpo, 140398, 140809, 120, 1, 350, 1);
        // sprite.ScaleVec(OsbEasing.OutExpo, 140809, 141015, 350, 1, 0, 1);
        // sprite.Rotate(139987, titleAngle);

        OsbSprite sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(141015, 0.64);
        sprite.MoveY(OsbEasing.OutExpo, 141015, 141220, 320, 240);
        sprite.Scale(OsbEasing.OutExpo, 141220, 141631, sprite.ScaleAt(141220).X, 0.32);
        sprite.Scale(OsbEasing.OutExpo, 141631, 142042, sprite.ScaleAt(142042).X, 0.52);
        sprite.Rotate(OsbEasing.OutExpo, 141631, 142042, sprite.RotationAt(141631), -MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 141220, 141631, 0.12, 0.32);
        sprite.Rotate(OsbEasing.OutExpo, 141631, 142042, 0, -MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c4.png");

        sprite.Scale(142042, 0.5);
        sprite.MoveY(OsbEasing.OutExpo, 142042, 142453, 200, 240);
        sprite.Fade(142453, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 142042, 142247, 0.3, 0.6, 0.16, 0.8);
        sprite.Rotate(OsbEasing.OutExpo, 142042, 142247, MathHelper.DegreesToRadians(120), MathHelper.DegreesToRadians(75));
        sprite.Fade(142453, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 142247, 142453, 0.5, 0.8, 0.23, 1.1);
        sprite.Rotate(OsbEasing.OutExpo, 142247, 142453, MathHelper.DegreesToRadians(24), MathHelper.DegreesToRadians(75));
        sprite.Fade(142453, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 142453, 142864, 0.4, 0.4, 0.2, 0.2);
        sprite.ScaleVec(OsbEasing.OutExpo, 142864, 143069, 0.2, 0.2, 0.2, -0.2);
        sprite.Fade(143267, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(200, 250));

        sprite.Scale(143069, 0.2);
        sprite.MoveY(OsbEasing.OutExpo, 143069, 143172, 280, 250);
        sprite.Fade(143267, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(440, 250));

        sprite.Scale(143172, 0.2);
        sprite.MoveY(OsbEasing.OutExpo, 143172, 143275, 280, 250);
        sprite.Fade(143267, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 143275, 143480, 0.4, 0.2);
        sprite.Rotate(OsbEasing.OutExpo, 143480, 143686, 0, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(143275, 143480, 0.4, 0.4);
        sprite.Rotate(OsbEasing.OutExpo, 143480, 143686, 0, -MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 143275, 143480, 0.4, 0.6);
        sprite.Rotate(OsbEasing.OutExpo, 143480, 143686, 0, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 143686, 144097, 0, 0.32, 0.32, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(220, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 143898, 144097, 0, 0.32, 0.32, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(420, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 143898, 144097, 0, 0.32, 0.32, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(120, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 143994, 144097, 0, 0.32, 0.32, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png", OsbOrigin.Centre, new Vector2(520, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 143994, 144097, 0, 0.32, 0.32, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c4.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 144097, 144302, 0.7, 0, 0.7, 0.7);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(200, 240));

        sprite.Scale(144302, 0.22);
        sprite.Rotate(OsbEasing.OutExpo, 144302, 144405, 0, MathHelper.DegreesToRadians(45));
        sprite.Fade(144508, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(440, 240));

        sprite.Scale(144405, 0.22);
        sprite.Rotate(OsbEasing.OutExpo, 144405, 144508, 0, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.Scale(OsbEasing.OutExpo, 144508, 144713, 64, 32);
        sprite.Rotate(144508, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 144713, 144919, 0.1, 0.22);
        sprite.Rotate(144713, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(144919, 0.5);
        sprite.MoveX(OsbEasing.OutExpo, 144919, 145330, 280, 320);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(144919, 0.5);
        sprite.MoveX(OsbEasing.OutExpo, 144919, 145330, 280, 320);
        sprite.Rotate(144919, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(145124, 0.26);
        sprite.MoveX(OsbEasing.OutExpo, 145124, 145330, 360, 320);
        sprite.Rotate(145124, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(144919, 0.26);
        sprite.MoveX(OsbEasing.OutExpo, 145124, 145330, 360, 320);
        sprite.Fade(145226, 0);
        sprite.Fade(145227, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 145330, 145946, 0, 0.28, 0.28, 0.28);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png", OsbOrigin.Centre, new Vector2(320, 160));

        sprite.ScaleVec(OsbEasing.OutExpo, 145535, 145946, 0, 0.28, 0.28, 0.28);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png", OsbOrigin.Centre, new Vector2(320, 320));

        sprite.ScaleVec(OsbEasing.OutExpo, 145535, 145946, 0, 0.28, 0.28, 0.28);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png", OsbOrigin.Centre, new Vector2(320, 80));

        sprite.ScaleVec(OsbEasing.OutExpo, 145741, 145946, 0, 0.28, 0.28, 0.28);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png", OsbOrigin.Centre, new Vector2(320, 400));

        sprite.ScaleVec(OsbEasing.OutExpo, 145741, 145946, 0, 0.28, 0.28, 0.28);

        float scale = 0.5f;
        for (int i = 0; i < 8; i++)
        {
            sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

            sprite.Scale(OsbEasing.OutExpo, 145946, 146152, 0.5, scale);
            sprite.Rotate(145946, MathHelper.DegreesToRadians(45));

            scale *= 0.75f;
        }

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png");

        sprite.Scale(OsbEasing.OutExpo, 146152, 146357, 0.2, 0.32);
        sprite.Rotate(OsbEasing.OutExpo, 146357, 146562, 0, Math.PI / 2);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(240, 240));

        sprite.Scale(146357, 0.32);
        sprite.Rotate(OsbEasing.OutExpo, 146357, 146562, 0, Math.PI / 2);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(400, 240));

        sprite.Scale(146357, 0.32);
        sprite.Rotate(OsbEasing.OutExpo, 146357, 146562, 0, Math.PI / 2);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.Scale(OsbEasing.OutExpo, 146562, 146768, 0.32, 0.42);
        sprite.Scale(OsbEasing.InExpo, 146768, 146973, 0.42, 0.2);


        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c4.png");

        sprite.Scale(146562, 0.32);
        sprite.MoveX(OsbEasing.OutExpo, 146562, 146768, 260, 200);
        sprite.MoveX(OsbEasing.InExpo, 146768, 146973, 200, 325);
        sprite.MoveX(OsbEasing.Out, 146973, 147179, 325, 390);
        sprite.MoveX(OsbEasing.InBack, 147179, 147384, 390, 320);
        sprite.Fade(147795, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(146973, 2, 42);
        sprite.Move(OsbEasing.OutExpo, 146973, 147384, 379, 200, 420, 160);
        sprite.Rotate(OsbEasing.OutExpo, 146973, 147384, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(146973, 2, 24);
        sprite.Move(OsbEasing.OutExpo, 146973, 147384, 368, 229, 450, 210);
        sprite.Rotate(OsbEasing.OutExpo, 146973, 147384, MathHelper.DegreesToRadians(20), -MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(146973, 2, 56);
        sprite.Move(OsbEasing.OutExpo, 146973, 147384, 356, 268, 480, 280);
        sprite.Rotate(OsbEasing.OutExpo, 146973, 147384, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(63));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(146973, 2, 34);
        sprite.Move(OsbEasing.OutExpo, 146973, 147384, 341, 310, 423, 330);
        sprite.Rotate(OsbEasing.OutExpo, 146973, 147384, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(72));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 147384, 147590, 0.3, 0.6, 0.16, 0.8);
        sprite.Rotate(OsbEasing.OutExpo, 147384, 147590, MathHelper.DegreesToRadians(20), MathHelper.DegreesToRadians(102));
        sprite.Fade(147795, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 147590, 147795, 0.5, 0.8, 0.23, 1.1);
        sprite.Rotate(OsbEasing.OutExpo, 147590, 147795, MathHelper.DegreesToRadians(182), MathHelper.DegreesToRadians(102));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 147795, 148001, 0.25, 0.2);
        sprite.Rotate(147795, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.Scale(OsbEasing.OutExpo, 147795, 148001, 0.25, 0.36);
        sprite.Rotate(147795, MathHelper.DegreesToRadians(45));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 148001, 148206, 0.36, 0.36, 0.28, 0.28);
        sprite.Rotate(148001, MathHelper.DegreesToRadians(180));
        sprite.Fade(148617, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(220, 250));

        sprite.Scale(148206, 0.28);
        sprite.MoveY(OsbEasing.OutExpo, 148206, 148412, 280, 250);
        sprite.Fade(148617, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(420, 250));

        sprite.Scale(148206, 0.28);
        sprite.MoveY(OsbEasing.OutExpo, 148206, 148412, 280, 250);
        sprite.Fade(148617, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(120, 240));

        sprite.Scale(148412, 0.28);
        sprite.MoveY(OsbEasing.OutExpo, 148412, 148617, 210, 240);
        sprite.Rotate(148412, MathHelper.DegreesToRadians(180));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png", OsbOrigin.Centre, new Vector2(520, 240));

        sprite.Scale(148412, 0.28);
        sprite.MoveY(OsbEasing.OutExpo, 148412, 148617, 210, 240);
        sprite.Rotate(148412, MathHelper.DegreesToRadians(180));

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c.png");

        sprite.Scale(OsbEasing.OutExpo, 148617, 148823, 0.32, 0.16);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c3.png");

        sprite.Scale(OsbEasing.OutExpo, 148823, 149028, 0.16, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.Scale(149028, 32);
        sprite.MoveX(149028, 200);
        sprite.MoveX(149131, 290);
        sprite.MoveX(149234, 350);
        sprite.MoveX(149336, 440);
        sprite.Fade(149439, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq2.png");

        sprite.Scale(OsbEasing.OutExpo, 149439, 149645, 0.32, 0.16);
        sprite.Rotate(149439, MathHelper.DegreesToRadians(45));
        sprite.Fade(150056, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq2.png");

        sprite.Scale(OsbEasing.OutExpo, 149645, 149747, 0.16, 0.28);
        sprite.Rotate(149645, MathHelper.DegreesToRadians(45));
        sprite.Scale(OsbEasing.OutExpo, 149850, 150056, 0.28, 0.16);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq2.png");

        sprite.Scale(OsbEasing.OutExpo, 149747, 149850, 0.28, 0.46);
        sprite.Rotate(149747, MathHelper.DegreesToRadians(45));
        sprite.Scale(OsbEasing.OutExpo, 149850, 150056, 0.46, 0.16);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t2.png");

        sprite.Scale(OsbEasing.OutExpo, 150056, 150261, 0.28, 0.46);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 150261, 150467, 0, 0.32, 0.32, 0.32);
        sprite.Fade(150878, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(210, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 150467, 150672, 0, 0.24, 0.24, 0.24);
        sprite.Fade(150878, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(430, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 150467, 150672, 0, 0.24, 0.24, 0.24);
        sprite.Fade(150878, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(130, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 150672, 150878, 0, 0.16, 0.16, 0.16);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/sq.png", OsbOrigin.Centre, new Vector2(510, 240));

        sprite.ScaleVec(OsbEasing.OutExpo, 150672, 150878, 0, 0.16, 0.16, 0.16);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 150878, 150980, 0, 0.28, 0.28, 0.28);
        sprite.Fade(151083, 1);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c4.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 150980, 151083, 0, 0.2, 0.2, 0.2);
        sprite.Fade(151083, 1);

        scale = 0.5f;
        for (int i = 0; i < 8; i++)
        {
            sprite = GetLayer("funky-part-1").CreateSprite("sb/e/vsq.png");

            sprite.ScaleVec(OsbEasing.OutExpo, 151083, 151494, scale * 3, scale, scale, scale);

            scale *= 0.75f;
        }

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/t.png");

        sprite.Scale(OsbEasing.OutExpo, 151494, 151905, 0.2, 0.32);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/c4.png");

        sprite.Scale(OsbEasing.OutExpo, 151905, 152110, 0.4, 0.12);

        sprite = GetLayer("funky-part-1").CreateSprite("sb/e/p.png");

        sprite.ScaleVec(OsbEasing.OutExpo, 152110, 152727, 12, 12, 40, 40);
        sprite.ScaleVec(OsbEasing.OutExpo, 152727, 153549, 40, 40, 854, 854);
        sprite.Fade(154371, 1);
    }

    private double GetBeatDuration(double time)
        => Beatmap.GetTimingPointAt((int)time).BeatDuration;
}