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
using Newtonsoft.Json;
using System.IO;
using Saheki;
using static Saheki.Helpers;
using StorybrewCommon.Storyboarding.Commands;

namespace StorybrewScripts
{
    public class Hashi : StoryboardObjectGenerator
    {
        public override void Generate()
        {
            
            // Knifes();
            diamondBackgroundmovement();
            rains();
            moon();
            guinso();
            topbotRect();
            gears();
            rainReverses();
            
            clockBackground();
            clock(Vector2.One * 6, true);
            clock(Vector2.Zero, false);
            clockTransition();
        }

        public void Knifes()
        {
            var knife = GetLayer("").CreateSprite("sb/knife.png");
            knife.Fade(0, 30000, 0, 1);
            // knife.ScaleVec(OsbEasing.None, 0, 1, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
            knife.Color(0, Color4.White);
            knife.Scale(0, 0.12);
            var angle = MathHelper.DegreesToRadians(45 + 180);
            var angle2 = MathHelper.DegreesToRadians(45);
            knife.Rotate(0, angle);

            var endmovement = calculateNewPosition(ScreenToOsu(1920 * 1.1f, 1080 * 1.1f), -angle2, 100);

            knife.Move(OsbEasing.None, 0, 30000, ScreenToOsu(1920 * 1.1f, 1080 * 1.1f), endmovement);
        }

        private void moon()
        {
            var initialPosition = ScreenToOsu(0, 1080 * 3 / 8);
            var finalPosition = ScreenToOsu(1920, 1080 * 5 / 8);
            var movementAngle = (float)Math.Atan2(finalPosition.Y - initialPosition.Y, finalPosition.X - initialPosition.X);
            var totalDistance = (finalPosition - initialPosition).Length;
            var distanceDelta = totalDistance / 8;

            var startTime = 184924;
            var endTime = 185335;
            var animation = Snap(Beatmap, startTime, 1, 2);

            var moon0Bitmap = SafeBitmapSize(Path.Combine("sb", "c3.png"));
            float moon0Scale = 480f / moon0Bitmap.Y;

            var moon0 = Spritehelper(Vector2.Zero, "", "c3.png", startTime, 1, 0, 0);

            moon0.Scale(OsbEasing.OutSine, startTime, startTime + animation, moon0Scale * 4f, moon0Scale * 0.525f * 1.25f);
            moon0.Fade(endTime, 0);
            moon0.Color(startTime, Color4.Black);

            var currentPosition = calculateNewPosition(initialPosition, movementAngle, distanceDelta * 2);
            moon0.Move(OsbEasing.InSine, startTime, endTime, initialPosition, currentPosition);

            startTime = 185335;
            endTime = 185541;

            var moon2Bitmap = SafeBitmapSize(Path.Combine("sb", "moon3.png"));
            float moon2Scale = 480f / moon2Bitmap.Y;

            var moon2 = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "", "moon3.png", startTime, 1, 0, 0);
            moon2.Scale(OsbEasing.None, startTime, startTime + animation, moon2Scale * 1f * 0.95f, moon2Scale * 1f);
            moon2.Scale(startTime, moon2Scale * 1f);
            moon2.Fade(endTime, 0);
            moon2.Color(startTime, Color4.Red);
            moon2.Rotate(startTime, endTime, 0, MathHelper.DegreesToRadians(20 / 5));

            var nextcurrentPosition = calculateNewPosition(currentPosition, movementAngle, distanceDelta);
            moon2.Move(OsbEasing.None, startTime, endTime, currentPosition, nextcurrentPosition);

            var moon3Bitmap = SafeBitmapSize(Path.Combine("sb", "moon4.png"));
            float moon3Scale = 480f / moon3Bitmap.Y;

            var moon3 = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "", "moon4.png", startTime, 1, 0, 0);

            moon3.Scale(OsbEasing.None, startTime, startTime + animation, moon3Scale * 0.7f, moon3Scale * 1f);
            moon3.Rotate(startTime, endTime, MathHelper.DegreesToRadians(10), MathHelper.DegreesToRadians(-20));

            moon3.Color(startTime, endTime, Color4.Red, Color4.White);

            moon3.Move(OsbEasing.None, startTime, endTime, currentPosition, nextcurrentPosition);
            currentPosition = nextcurrentPosition;

            startTime = 185335;
            endTime = 186568;

            var moonRingBitmap = SafeBitmapSize(Path.Combine("sb", "c2.png"));
            float moonRingScale = 480f / moonRingBitmap.Y;

            var positionRing = ScreenToOsu(1920 * 3 / 8, 1080 * 5 / 8);
            var moonRing = Spritehelper(Vector2.Zero, "", "c2.png", startTime, 1f, moonRingScale * 1.5f, 0);
            moonRing.Fade(endTime, 0);
            moonRing.Color(startTime, Color4.White);
            moonRing.Color(185541, Color4.Red);
            moonRing.Move(OsbEasing.OutSine, startTime, endTime, positionRing, positionRing + new Vector2(160, 90) / 2);

            startTime = 185541;
            endTime = 186568;
            animation = Snap(Beatmap, startTime, 1, 4);

            var moonBitmap = SafeBitmapSize(Path.Combine("sb", "moon2.png"));
            float moonScale = 480f / moonBitmap.Y;

            var moon = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "", "moon2.png", startTime, 1, 0, 0);

            // moon.Scale(OsbEasing.OutBounce, startTime, startTime + animation, moonScale * 1f * 0.95f, moonScale * 1f);
            moon.Scale(OsbEasing.InSine, startTime, endTime, moonScale * 1f, moonScale * 1.5f);
            moon.Rotate(startTime, endTime, 0, MathHelper.DegreesToRadians(20));
            moon.Fade(endTime, 0);
            nextcurrentPosition = calculateNewPosition(currentPosition, movementAngle, distanceDelta * 5);
            moon.Move(OsbEasing.None, startTime, endTime, currentPosition, nextcurrentPosition);


            moon3.Scale(OsbEasing.InSine, startTime, endTime, moonScale * 1f, moonScale * 1.5f);
            moon3.Rotate(startTime, endTime, MathHelper.DegreesToRadians(-20), MathHelper.DegreesToRadians(-40));
            moon3.Fade(OsbEasing.OutSine, startTime, endTime, 1, 0);

            moon3.Move(OsbEasing.None, startTime, endTime, currentPosition, nextcurrentPosition);
        }



        private void rains()
        {
            const int rains = 18;
            const int rainMinSize = 40;
            const int rainMaxSize = 240;

            var random = new Random(321);

            const int step = 1920 / rains;

            for (int i = 0; i < rains; i++)
            {
                var x = random.Next(step) + step * i;
                var size = rainMinSize + random.Next(rainMaxSize - rainMinSize - 1);

                rain(ScreenToOsu(x, 0), size);
            }
        }
 
        private void rain(Vector2 position, int size)
        {
            const int startTime = 184924;
            const int endTime = 186568;

            var angle = MathHelper.DegreesToRadians(60); // ccw
            var animation = Snap(Beatmap, startTime, 1, 1);

            var nextPosition = calculateNewPosition(position, angle, size * 2);

            var rain = Spritehelper(Vector2.Zero, "", "pixel.png", startTime, 1, 0, angle);
            rain.ScaleVec(OsbEasing.InSine, startTime, endTime, new Vector2(0.25f, 0.25f), new Vector2(size * 2, 0.25f));
            rain.Move(OsbEasing.InSine, startTime, endTime, position, nextPosition);
            rain.Fade(endTime - animation / 2, endTime, 1, 0);

            // var knifeBitmap = GetMapsetBitmap("sb/knife.png");
            // float knifeScale = 480f / knifeBitmap.Height;
            // // float knifeScale2 = 480f / knifeBitmap.Height;
            // knifeBitmap.Dispose();

            float sizeScaling = size / 100f;

            var knifeOffset = calculateNewPosition(position, angle, 24 * sizeScaling);

            nextPosition = calculateNewPosition(position, angle, size * 4 + 24 * sizeScaling);

            var knife = Spritehelper(Vector2.Zero, "", "knife2.png", startTime, 0, 0.05f * sizeScaling, angle);
            knife.Move(OsbEasing.InSine, startTime, endTime, knifeOffset, nextPosition);
            knife.Fade(startTime, animation + startTime, 0, 1);
            knife.Fade(endTime, 0);
            knife.Color(startTime, Color4.Black);

            const float space = 8;

            var knifeShadow = Spritehelper(Vector2.Zero, "", "knife2.png", startTime, 0, 0.05f * sizeScaling, angle);
            knifeShadow.Move(OsbEasing.InSine, startTime, endTime, knifeOffset + new Vector2(space, space) * sizeScaling, nextPosition + new Vector2(space, space) * sizeScaling);
            knifeShadow.Fade(startTime, animation + startTime, 0, 0.25f);
            knifeShadow.Fade(endTime, 0);
            knifeShadow.Color(startTime, Color4.Black);
        }

        private void topbotRect()
        {
            const int startTime = 186157;
            const int endTime = 186568;
            const int endTime2 = 186979; //188212; 
                                         // const int delta = endTime - startTime;

            var top = Spritehelper(Vector2.Zero, "kalindraz", "pixel.png", startTime, 1, 0, 0);
            top.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 / 2, 0), ScreenToOsu(1920 / 2, 1080 / 4));
            top.Fade(endTime2, 0);
            top.ScaleVec(OsbEasing.InSine, startTime, endTime, PixelToOsu(1920, 0), PixelToOsu(1920, 1080 / 4));
            top.Color(startTime, Color4.DarkRed);
            top.ScaleVec(OsbEasing.InSine, endTime, endTime2, PixelToOsu(1920, 1080 / 4), PixelToOsu(1920, 0));
            top.Move(OsbEasing.InSine, endTime, endTime2, ScreenToOsu(1920 / 2, 1080 / 4), ScreenToOsu(1920 / 2, 1080 / 2));

            var bot = Spritehelper(Vector2.Zero, "kalindraz", "pixel.png", startTime, 1, 0, 0);
            bot.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 / 2, 1080), ScreenToOsu(1920 / 2, 1080 * 3 / 4));
            bot.Fade(endTime2, 0);
            bot.ScaleVec(OsbEasing.InSine, startTime, endTime, PixelToOsu(1920, 0), PixelToOsu(1920, 1080 / 4));
            bot.Color(startTime, Color4.DarkRed);
            bot.ScaleVec(OsbEasing.InSine, endTime, endTime2, PixelToOsu(1920, 1080 / 4), PixelToOsu(1920, 0));
            bot.Move(OsbEasing.InSine, endTime, endTime2, ScreenToOsu(1920 / 2, 1080 * 3 / 4), ScreenToOsu(1920 / 2, 1080 / 2));
        }

        private void guinso()
        {
            int startTime = 186568;
            int endTime = 186774;
            const float scale = 1f;

            var shadowOffset = new Vector2(20, 20);

            var bitmap = SafeBitmapSize(Path.Combine("sb", "knife2.png"));
            var width = bitmap.X;

            var knifeShadow = Spritehelper(Vector2.Zero, "", "knife2.png", startTime, 0.25f, scale, MathHelper.DegreesToRadians(180));
            var knife = Spritehelper(Vector2.Zero, "", "knife2.png", startTime, 1, scale, MathHelper.DegreesToRadians(180));
            knife.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920, 1080 / 1.5f) + new Vector2(width / 2 * scale, 0), ScreenToOsu(1920 * 5 / 8, 1080 / 1.5f));

            knife.Color(startTime, Color4.Black);
            knife.Color(186979, Color4.Red);
            knife.Color(187184, Color4.White);


            knifeShadow.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920, 1080 / 1.5f) + new Vector2(width / 2 * scale, 0) + shadowOffset, ScreenToOsu(1920 * 5 / 8, 1080 / 1.5f) + shadowOffset);
            knifeShadow.Color(startTime, Color4.Black);

            startTime = 186774;
            endTime = 187595;

            knife.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 * 5 / 8, 1080 / 1.5f), ScreenToOsu(1920 * 3 / 8, 1080 / 1.5f));

            knifeShadow.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 * 5 / 8, 1080 / 1.5f) + shadowOffset, ScreenToOsu(1920 * 3 / 8, 1080 / 1.5f) + shadowOffset);

            startTime = 187595;
            endTime = 188006;

            knife.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 * 3 / 8, 1080 / 1.5f), ScreenToOsu(0, 1080 / 1.5f) - new Vector2(width / 2 * scale, 0));

            knifeShadow.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920 * 3 / 8, 1080 / 1.5f) + shadowOffset, ScreenToOsu(0, 1080 / 1.5f) - new Vector2(width / 2 * scale, 0) + shadowOffset);

            endTime = 188212;
            knife.Fade(endTime, 0);
            // var knife2 = Spritehelper(Vector2.Zero, "", "knife2.png", startTime, 1, 1.5f, MathHelper.DegreesToRadians(180));
            // knife2.Move(OsbEasing.InSine, startTime, endTime, ScreenToOsu(1920, 1080 / 2) + new Vector2(w / 2 * 1.5f, 0), ScreenToOsu(1920 / 2, 1080 / 2));
            // knife2.Fade(endTime2, 0);
            // knife2.Color(startTime, Color4.Black);


        }

        private void rainReverses()
        {
            const int rains = 8;
            const int rainMinSize = 1080 * 3 / 8;
            const int rainMaxSize = 1080 * 5 / 8;

            var background = GetLayer("mario").CreateSprite(Path.Combine("sb", "pixel.png"), OsbOrigin.Centre, Vector2.Zero);

            background.Fade(OsbEasing.InOutSine, 187801, 188623, 1, 1);
            background.Color(187801, 188623, Color4.White, Color4.Red);
            background.Move(187801, 188623, ScreenToOsu(1920, 1080), ScreenToOsu(0, 0));
            background.ScaleVec(187801, 188006, PixelToOsu(1920, 0), PixelToOsu(1920, 2203));
            background.ScaleVec(188007, 188623, PixelToOsu(1920, 2203), PixelToOsu(1920,0));
            background.Rotate(187801, MathHelper.DegreesToRadians(-60));

            var random = new Random(321);

            const float step = 1920 * 1.5f / rains;

            for (int index = 0; index < rains; index++)
            {
                var x = step * index + step / 2 - 1920 / 2;
                var size = rainMinSize + random.Next(rainMaxSize - rainMinSize - 1);

                rainReverse(ScreenToOsu(x, 1080), size, + index * 52, true);
                rainReverse(ScreenToOsu(x - 32, 1080), size, + index * 52);
            }
        }

        private void rainReverse(Vector2 position, int size, int delay, bool shadow = false)
        {
            const int startTime = 187801;
            int endTime = 188623 + delay - 411;

            var angle = MathHelper.DegreesToRadians(-60); // ccw
            var animation = Snap(Beatmap, startTime, 1, 4);

            position.Y -= (float)(20 * Math.Sin(angle));

            var nextPosition = calculateNewPosition(position, angle, size);

            var rain = Spritehelper(Vector2.Zero, "mario", "pixel.png", startTime, shadow ? 0.5f : 1, 0, angle);
            rain.ScaleVec(OsbEasing.InSine, startTime, endTime - delay, new Vector2(2f, 2f), new Vector2(size, 40f));
            rain.ScaleVec(OsbEasing.InSine, endTime - delay + 1, endTime, new Vector2(size, 40f), new Vector2(size, 0f));
            rain.Move(OsbEasing.InSine, startTime, endTime, position, nextPosition);
            rain.Fade(endTime, 0);
            rain.Color(startTime, endTime, Color4.Black, shadow ? Color4.Black : Color4.Red);

            if (shadow) return;

            float sizeScaling = size / 100f;

            var knifeOffset = calculateNewPosition(position, angle, 24 * sizeScaling / 2);

            const float larger = 4.25f;

            nextPosition = calculateNewPosition(position, angle, size * 2 + 24 * sizeScaling * larger);

            var knifeShadow = Spritehelper(Vector2.Zero, "mario", "knife2.png", startTime, 0, 0.05f * sizeScaling, angle);
            var knife = Spritehelper(Vector2.Zero, "mario", "knife2.png", startTime, 0, 0, angle);
            knife.Move(OsbEasing.InSine, startTime, endTime - delay / 2, knifeOffset, nextPosition);
            knife.Fade(startTime, animation + startTime, 0, 1);
            knife.Fade(endTime, 0);
            knife.Color(startTime, Color4.Red);
            knife.Scale(OsbEasing.InSine, startTime, endTime, 0.01f * sizeScaling, 0.05f * sizeScaling * larger);

            const float space = 2;

            knifeShadow.Move(OsbEasing.InSine, startTime, endTime - delay / 2, knifeOffset + new Vector2(space, space) * sizeScaling, nextPosition + new Vector2(space, space) * sizeScaling);
            knifeShadow.Fade(startTime, animation + startTime, 0, 0.5f);
            knifeShadow.Fade(endTime, 0);
            knifeShadow.Color(startTime, Color4.Black);
            knifeShadow.Scale(OsbEasing.InSine, startTime, endTime, 0.01f * sizeScaling, 0.05f * sizeScaling * larger);
        }

        private void gears()
        {
            List<Vector2> positions =
            [
                new (130, 460),
                new (20, 30),
                new (480, 30),
                new (620, 460),
            ];

            var random = new Random(400);
            var numero_dela = 0;

            foreach (var position in positions)
            {
                var id = random.Next(5) + 1;
                var scale = 0.4f + (float)random.NextDouble() * 0.6f;
                var scale2 = (float)random.NextDouble() * 0.45f;
                var revolutions = (float)random.NextDouble() * 4 + 1;
                Gear(numero_dela, id, position, scale, scale2, revolutions > 3 ? -revolutions + 3 : revolutions);
                numero_dela+=1;
            }
        }

        private void Gear(int numero_dela, int id, Vector2 position, float scale, float scale2, float revolutions)
        {
            const int startTime = 188212;
            const int endTime = 189856;
            const int delta = endTime - startTime;

            string fileName = 'g' + id.ToString() + ".png";

            var gearBitmap = SafeBitmapSize("sb/steam/" + fileName);
            var gearScale = 480f / gearBitmap.Y * scale;
            var borderScale = 1.02f;
            
            var rotations = (float)MathHelper.DegreesToRadians(360 * revolutions);
            var rotationStep = rotations / (delta - (188828 - 188623));

            var gearShadow = Spritehelper(position + new Vector2(30, 30) * gearScale * borderScale, "", "steam/" + fileName, startTime, 0.25f, gearScale * borderScale, 0);
            var gearBorder = Spritehelper(position, "", "steam/" + fileName, startTime, 0.8f, gearScale * borderScale, 0);

            var gear = Spritehelper(position, "", "steam/" + fileName, startTime, 1, gearScale, 0);
            
            gear.Scale(OsbEasing.OutSine, startTime, 188417, 0, gearScale);
            gear.Fade(endTime, 0);
            gear.Color(startTime, Color4.Black);
            gear.Rotate(startTime, 188623, 0, (188623 - startTime) * rotationStep);
            gear.Rotate(188828, endTime, (188623 - startTime) * rotationStep, (endTime - 188828) * rotationStep);
            gear.Color(188623, revolutions > 0 ? Color4.Red : new Color4(192, 0, 0, 255));
            gear.Color(188828, numero_dela %2 == 0 ? new Color4(113, 121, 126, 255) : new Color4(56, 60, 63, 255));
            gear.Move(OsbEasing.InSine, startTime, 188623, ScreenToOsu(1920 / 2, 1080 / 2), position);

            var moveTime = 189445;
            var animation = Snap(Beatmap, moveTime, 1, 8);

            gear.Move(OsbEasing.InOutSine, moveTime, endTime, position, ScreenToOsu(1920 * 3 / 4, 1080 / 2));
            gear.Scale(OsbEasing.InOutSine, moveTime, endTime, gearScale, scale2);

            gearBorder.Scale(OsbEasing.OutSine, startTime, 188417, 0, gearScale * borderScale);
            gearBorder.Fade(endTime, 0);
            gearBorder.Color(startTime, Color4.Black);
            gearBorder.Rotate(startTime, 188623, 0, (188623 - startTime) * rotationStep);
            gearBorder.Rotate(188828, endTime, (188623 - startTime) * rotationStep, (endTime - 188828) * rotationStep);
            gearBorder.Move(OsbEasing.InSine, startTime, 188623, ScreenToOsu(1920 / 2, 1080 / 2), position);

            gearBorder.Move(OsbEasing.InOutSine, moveTime, endTime, position, ScreenToOsu(1920 * 3 / 4, 1080 / 2));
            gearBorder.Scale(OsbEasing.InOutSine, moveTime, endTime, gearScale * borderScale, scale2 * borderScale);

            gearShadow.Scale(OsbEasing.OutSine, startTime, 188417, 0, gearScale * borderScale);
            gearShadow.Fade(endTime, 0);
            gearShadow.Color(startTime, Color4.Black);
            gearShadow.Rotate(startTime, 188623, 0, (188623 - startTime) * rotationStep);
            gearShadow.Rotate(188828, endTime, (188623 - startTime) * rotationStep, (endTime - 188828) * rotationStep);
            gearShadow.Move(OsbEasing.InSine, startTime, 188623, ScreenToOsu(1920 / 2, 1080 / 2) + new Vector2(30, 30) * gearScale * borderScale, position + new Vector2(30, 30) * gearScale * borderScale);

            gearShadow.Move(OsbEasing.InOutSine, moveTime + animation, endTime, position + new Vector2(30, 30) * gearScale * borderScale, ScreenToOsu(1920 * 3 / 4, 1080 / 2) + new Vector2(30, 30) * gearScale * borderScale);
            gearShadow.Scale(OsbEasing.InOutSine, moveTime + animation, endTime, gearScale * borderScale, scale2 * borderScale);
        } 

        private List<Vector2> MakeSpiral(int points, double distance, Vector2 position, double startAngle, double grow)
        {
            var result = new List<Vector2>(points);
            double ratio = 1.618; // golden ratio
            double angle = startAngle;
            var currentPoint = position;
            for (int i = 0; i < points; i++)
            {
                var x = (float)(currentPoint.X + Math.Cos(angle) * distance);
                var y = (float)(currentPoint.Y + Math.Sin(angle) * distance);
                
                currentPoint = new Vector2(x, y);
                result.Add(ScreenToOsu(currentPoint));

                angle += ratio * (1 / grow) / (i + 1);
            }
            
            return result;
        }

        private void clockTransition()
        {
            int startTime = 189856;
            var animation = Snap(Beatmap, startTime, 1, 2);

            var background = GetLayer("Neto").CreateSprite(Path.Combine("sb", "pixel.png"), OsbOrigin.Centre, ScreenToOsu(1920 / 2, 1080 / 2));
            background.Color(startTime, startTime + animation, Color4.White, Color4.Red);
            background.ScaleVec(startTime, PixelToOsu(1920, 2203));
            background.Fade(startTime, startTime + animation, 1, 1);

            // var white = Spritehelper(ScreenToOsu(1920 * 3 / 4, 1080 / 2), "", "black.png", startTime, 1, 0, 0);
            // white.Color(startTime, Color4.White);
            // white.Fade(startTime, startTime + animation, 1, 1);
            // white.Scale(startTime, startTime + animation, 0, PixelToOsu(6));

            var black = Spritehelper(ScreenToOsu(1920 * 3 / 4, 1080 / 2), "Neto", "c3.png", startTime, 1, 0, 0);
            black.Color(startTime, Color4.Black);
            black.Fade(startTime, 1);
            black.Scale(startTime, startTime + animation, 0, PixelToOsu(6));
            black.Fade(startTime + animation, startTime + 2 * animation, 1, 0);
        }

        private void clockBackground()
        {
            int startTime = 189856;
            int endTime = 191500;

            var backgroundBase = GetLayer("").CreateSprite(Path.Combine("sb","diamond.png"), OsbOrigin.Centre);
            var backgroundBaseScale = 834f / SafeBitmapSize(Path.Combine("sb", "diamond.png")).X;
            backgroundBase.Scale(startTime, backgroundBaseScale);
            backgroundBase.Fade(startTime, endTime, 1, 0);
     
            var backgroundBitmap = SafeBitmapSize(Path.Combine("sb","sakuya.png"));
            float backgroundScale = 480f / backgroundBitmap.Y;

            // var background2 = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "", "sakuya.jpg", startTime, 0.66f, backgroundScale, 0, false);
            
            var sakuyaShadow = Spritehelper(ScreenToOsu(1920 / 2 + 6, 1080 / 2 + 6), "Shykes", Path.Combine("sb","sakuyamask.png"), startTime, 1, backgroundScale, 0, false);
            sakuyaShadow.Fade(startTime, 191500, 0.66f, 0.66f);
            sakuyaShadow.Color(startTime, Color4.Black);

            var sakuyaMask = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "Shykes", Path.Combine("sb","sakuyamask.png"), startTime, 1, backgroundScale, 0, false);
            sakuyaMask.Fade(startTime, 191500, 1, 1);
            sakuyaMask.Color(startTime, Color4.Black);
            startTime = 190267;

            var sakuya = Spritehelper(ScreenToOsu(1920 / 2, 1080 / 2), "Shykes", Path.Combine("sb","sakuya.png"), startTime, 1, backgroundScale, 0, false);
            // background.Additive(startTime, endTime);
            sakuya.Fade(endTime, 0);

            // background2.Fade(endTime, 0);

            startTime = 190267;
            endTime = 190472;
            sakuya.Color(startTime, Color4.Red); // (128, 0, 0, 255)
            // background2.Color(startTime, Color4.Red);

            startTime = 190472;
            endTime = 191500;

            sakuya.Fade(startTime, endTime, 1, 0);
            sakuya.Color(startTime, Color4.White);
            // background2.Color(startTime, Color4.DimGray);
        }

        private void clock(Vector2 offset, bool shadown = false)
        {
            int startTime = 189856;
            int endTime = 191500;
            float opacity = shadown ? 0.66f : 1;

            var clockBitmap = SafeBitmapSize(Path.Combine("sb", "clock", "clock.png"));
            var clockScale = 480f / clockBitmap.Y;

            var clock = Spritehelper(ScreenToOsu(1920 * 3 / 4 + offset.X, 1080 / 2 + offset.Y), "Akaeboshi", "clock/clock.png", startTime, 0, clockScale * 0.90f, 0);

            var animation = Snap(Beatmap, startTime, 1, 2);
            clock.Fade(startTime, startTime + animation, 0, opacity);
            clock.Fade(190472, endTime, opacity, 0);
            if (shadown) clock.Color(0, Color4.Black);

            var clockHandle1Bitmap = SafeBitmapSize(Path.Combine("sb", "clock", "big.png"));
            var clockHandle1Scale = 480f / clockHandle1Bitmap.Y;

            var clockHandle2Bitmap = SafeBitmapSize(Path.Combine("sb", "clock", "small.png"));
            var clockHandle2Scale = 480f / clockHandle2Bitmap.Y;

            float startRotation = (float)MathHelper.DegreesToRadians(360 / 12 * 5.45);
            float rotationAmount = (float)MathHelper.DegreesToRadians(360 / 12 * 7.45);

            const int rotationEnd = 190472;

            var clockHandle1 = GetLayer("Akaeboshi").CreateSprite("sb/clock/small.png", OsbOrigin.BottomCentre, ScreenToOsu(1920 * 3 / 4 + offset.X, 1080 / 2 + offset.Y));
            clockHandle1.Fade(startTime, startTime + animation, 0, opacity);
            clockHandle1.Fade(190472, endTime, opacity, 0);
            clockHandle1.Scale(startTime, clockHandle2Scale * 0.20f * 0.90f);
            clockHandle1.Rotate(OsbEasing.InOutSine, startTime, rotationEnd, startRotation, rotationAmount);
            if (shadown) clockHandle1.Color(0, Color4.Black);

            var clockHandle2 = GetLayer("Akaeboshi").CreateSprite("sb/clock/big.png", OsbOrigin.BottomCentre, ScreenToOsu(1920 * 3 / 4 + offset.X, 1080 / 2 + offset.Y));
            clockHandle2.Fade(startTime, startTime + animation, 0, opacity);
            clockHandle2.Fade(190472, endTime, opacity, 0);
            clockHandle2.Scale(startTime, clockHandle1Scale * 0.33f * 0.90f);
            clockHandle2.Rotate(OsbEasing.InOutSine, startTime, rotationEnd, startRotation * 12, rotationAmount * 12);
            if (shadown) clockHandle2.Color(0, Color4.Black);

            var middleBitmap = SafeBitmapSize(Path.Combine("sb", "c3.png"));
            var middleScale = 480f / middleBitmap.Y;
            var middleScale2 = PixelToOsu(24) / middleBitmap.X;

            var middle = Spritehelper(ScreenToOsu(1920 * 3 / 4 + offset.X, 1080 / 2 + offset.Y), "Akaeboshi", "c3.png", startTime, 0, middleScale * middleScale2, 0);
            middle.Fade(startTime, startTime + animation, 0, opacity);
            middle.Fade(190472, endTime, opacity, 0);
            if (shadown) middle.Color(0, Color4.Black);
        }

        private void diamondBackgroundmovement()
        {
            const int startTime = 184924;
            const int endTime = 189856;
            const int delta = endTime - startTime;
            const float scale = 0.75f;

            const int loops = 24;
            const float movement = 200 * scale; // magic number
            const float time_step = delta / loops;

            var position = ScreenToOsu(1920 / 2, 1080 / 2); // center

            float startRotation = (float)MathHelper.DegreesToRadians(180 - 45 / 2 + 10);
            float rotationAmount = (float)MathHelper.DegreesToRadians(45);
            float angle_step = rotationAmount / loops;

            // var diamondoBitmap = GetMapsetBitmap("sb/diamond.jpg");
            // diamondoBitmap.Dispose();

            var diamondBackground = Spritehelper(position, "HashiBackground", "diamond.png", startTime, 1f, scale, 0);

            float currentAngle = startRotation;
            float currentTime = startTime;
            var currentMovement2 = Vector2.Zero;

            const float distanceMovement2 = 360 * scale;
            const float distanceMovement2_step = distanceMovement2 / loops;
            float angleMovement2 = (float)MathHelper.DegreesToRadians(100);

            for (int i = 0; i < loops; i++)
            {
                var nextAngle = currentAngle + angle_step;
                var nextPosition = calculateNewPosition(position, nextAngle, movement);

                var nextMovement2 = calculateNewPosition(currentMovement2, angleMovement2, distanceMovement2_step);

                diamondBackground.Move(OsbEasing.None, currentTime, currentTime + time_step, position + currentMovement2, nextPosition + nextMovement2);

                currentMovement2 = nextMovement2;
                currentAngle += angle_step;
                currentTime += time_step;
            }

            diamondBackground.Rotate(OsbEasing.None, startTime, endTime, startRotation, startRotation + rotationAmount);
            diamondBackground.Fade(189445, endTime, 1, 0);
        }
            
            // Utils

        private static Vector2 calculateNewPosition(Vector2 startPosition, float angleRadians, float distance)
        {
            float newX = startPosition.X + distance * (float)Math.Cos(angleRadians);
            float newY = startPosition.Y + distance * (float)Math.Sin(angleRadians);

            return new Vector2(newX, newY);
        }
        
        // Note: GetBitmapSize is unsafe it might not dispose correcly, adds boild plate and is slow.
        private Vector2 SafeBitmapSize(string path)
        {
            // Image image = null;
            // try
            // {
            var image = Image.FromFile(Path.Combine(MapsetPath, path));
            var result = new Vector2(image.Width, image.Height);
            image.Dispose();
            
            return result;
            // }
            // finally
            // {
            // }
        }

        private OsbSprite Spritehelper(Vector2 position, string layer = "", string filePath = "", int startTime = 0, float opacity = 1f, float scale = 1f, float rotation = 0, bool combine = true)
        {
            var sprite = GetLayer(layer).CreateSprite(combine ? Path.Combine("sb", filePath) : filePath, OsbOrigin.Centre, position);
            if (opacity != 0) sprite.Fade(startTime, opacity);
            if (scale != 0) sprite.Scale(startTime, scale);
            if (rotation != 0) sprite.Rotate(startTime, rotation);

            return sprite;
        }
    }
}