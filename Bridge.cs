using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using StorybrewCommon.Mapset;
using StorybrewCommon.Scripting;
using StorybrewCommon.Storyboarding;
using StorybrewCommon.Storyboarding.Util;
using StorybrewCommon.Subtitles;
using StorybrewCommon.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StorybrewScripts
{
    public class Bridge : StoryboardObjectGenerator
    {
        //[Configurable] public string SakuyaImage = "";
        [Configurable] public int StartTime = 0;
        [Configurable] public int EndTime = 0;
        [Configurable] public string KnifeImage = "";
        [Configurable] public string Line = "";
        [Configurable] public string Sakuya1WhiteImage = "";
        [Configurable] public string Sakuya1Image = "";
        [Configurable] public string Sakuya2WhiteImage = "";
        [Configurable] public string Sakuya2Image = "";
        [Configurable] public string Sakuya3WhiteImage = "";
        [Configurable] public string Sakuya3Image = "";
        [Configurable] public string Sakuya4WhiteImage = "";
        [Configurable] public string Sakuya4Image = "";
        [Configurable] public string Circle = "";
        [Configurable] public string Pixel = "";
        public override void Generate()
        {

            var background = GetLayer("BridgeBackground").CreateSprite(Path.Combine("sb","diamond.png"), OsbOrigin.Centre);
            var backgroundScale = 854f / GetMapsetBitmap(Path.Combine("sb", "diamond.png")).Width;
            background.Scale(StartTime, backgroundScale);
            background.Fade(StartTime, 1);
            
            // 154 | 14 | 42

            //  var background = GetLayer("BridgeBackground").CreateSprite(Line, OsbOrigin.Centre);
            //  background.Color(StartTime, Color4.Crimson);
            //  background.Fade(StartTime, 0.7);

            if (EndTime != 270404)
            {
                background.Fade(EndTime, 0);
            }
            else
            {
                background.Color(269582, Color4.White);
                background.Fade(269582, 1);
                background.Fade(EndTime, 271226, 1, 0);
            }


            int columns = 13;
            int rows = 7;

            // Define the coordinate ranges as per your request
            float minX = -220;
            float maxX = 847;
            float minY = -50;
            float maxY = 520;

            // Calculate the total width and height
            float totalWidth = maxX - minX;   // 854 units
            float totalHeight = maxY - minY;  // 480 units

            // Define padding and calculate grid dimensions
            float paddingX = 50; // Original padding
            float paddingY = 30;
            float gridWidth = totalWidth - 2 * paddingX;
            float gridHeight = totalHeight - 2 * paddingY;

            // Calculate spacing between sprites
            float columnSpacing = gridWidth / (columns - 1);
            float rowSpacing = gridHeight / (rows - 1);

            // Movement distance along the rotated x-axis
            float movementDistance = 75; // Original movement distance

            // Rotation in radians
            float rotationDegrees1 = 10;
            float rotationRadians1 = rotationDegrees1 * (float)(Math.PI / 180);

            float rotationDegrees2 = -10;
            float rotationRadians2 = rotationDegrees2 * (float)(Math.PI / 180);

            // Precompute cosine and sine of rotation angle
            float cosTheta1 = (float)Math.Cos(rotationRadians1);
            float sinTheta1 = (float)Math.Sin(rotationRadians1);

            float cosTheta2 = (float)Math.Cos(rotationRadians2);
            float sinTheta2 = (float)Math.Sin(rotationRadians2);

            // Precompute movement vector components in screen coordinates
            float movementDx1 = movementDistance * cosTheta1;
            float movementDy1 = movementDistance * sinTheta1;

            float movementDx2 = movementDistance * cosTheta2;
            float movementDy2 = movementDistance * sinTheta2;

            // Screen center coordinates (center of the defined coordinate plane)
            float screenCenterX = (minX + maxX) / 2; // 320
            float screenCenterY = (minY + maxY) / 2; // 240

            for (int row = 0; row < rows; row++)
            {
                // Determine movement direction for this row
                int direction = (row % 2 == 0) ? 1 : -1; // 1 for right, -1 for left

                for (int col = 0; col < columns; col++)
                {
                    // Create the sprite
                    var spriteShadow = GetLayer("Bridge").CreateSprite(KnifeImage, OsbOrigin.Centre);
                    var sprite = GetLayer("Bridge").CreateSprite(KnifeImage, OsbOrigin.Centre);

                    // Calculate grid position (x0, y0) centered at (0,0)
                    float x0 = (col - (columns - 1) / 2.0f) * columnSpacing;
                    float y0 = (row - (rows - 1) / 2.0f) * rowSpacing;

                    // Rotate (x0, y0) by rotationRadians to get (x, y)
                    float x1 = x0 * cosTheta1 - y0 * sinTheta1;
                    float y1 = x0 * sinTheta1 + y0 * cosTheta1;

                    float x2 = x0 * cosTheta2 - y0 * sinTheta2;
                    float y2 = x0 * sinTheta2 + y0 * cosTheta2;

                    // Offset to screen coordinates
                    float xStart1 = x1 + screenCenterX;
                    float yStart1 = y1 + screenCenterY;

                    float xStart2 = x2 + screenCenterX;
                    float yStart2 = y2 + screenCenterY;

                    // Calculate end position based on movement direction
                    float xEnd1 = xStart1 + direction * movementDx1;
                    float yEnd1 = yStart1 + direction * movementDy1;

                    float xEnd2 = xStart2 + direction * movementDx2;
                    float yEnd2 = yStart2 + direction * movementDy2;

                    // Animate movement over time
                    sprite.Move(StartTime, StartTime + 1632, xStart1, yStart1, xEnd1, yEnd1);

                    sprite.Move(StartTime + 1632, StartTime + 3264, xStart2, yStart2, xEnd2, yEnd2);

                    spriteShadow.Move(StartTime, StartTime + 1632, xStart1 + 5, yStart1 + 5, xEnd1 + 5, yEnd1 + 5);
                    spriteShadow.Move(StartTime + 1632, StartTime + 3264, xStart2 + 5, yStart2 + 5, xEnd2 + 5, yEnd2 + 5);

                    // Set Fade and Scale
                    sprite.Fade(StartTime, 1);
                    sprite.Fade(StartTime + 3264, 0);
                    sprite.Scale(StartTime, 0.1f);

                    spriteShadow.Color(StartTime, Color4.Black);
                    spriteShadow.Fade(StartTime, 0.3);
                    spriteShadow.Scale(StartTime, 0.1f);
                    if (direction == 1)
                    {
                        sprite.Rotate(StartTime, StartTime + 3264, MathHelper.DegreesToRadians(col * 30), MathHelper.DegreesToRadians(col * 30 + 180));
                        spriteShadow.Rotate(StartTime, StartTime + 3264, MathHelper.DegreesToRadians(col * 30), MathHelper.DegreesToRadians(col * 30 + 180));
                    }
                    else
                    {
                        sprite.Rotate(StartTime, StartTime + 3264, MathHelper.DegreesToRadians(col * 30 + 180), MathHelper.DegreesToRadians(col * 30));
                        spriteShadow.Rotate(StartTime, StartTime + 3264, MathHelper.DegreesToRadians(col * 30 + 180), MathHelper.DegreesToRadians(col * 30));
                    }
                    sprite.Color(StartTime, Color4.Black);
                    sprite.Color(StartTime + 408, Color4.Red);
                    sprite.Color(StartTime + 612, Color4.White);

                    sprite.Color(StartTime + 1632, Color4.Black);
                    sprite.Color(StartTime + 1632 + 408, Color4.Red);
                    sprite.Color(StartTime + 1632 + 612, Color4.White);
                }
            }

            columns = 8;
            rows = 13;

            minY = -220;
            maxY = 747;
            minX = -107;
            maxX = 747;

            totalWidth = maxX - minX;   // 854 units
            totalHeight = maxY - minY;  // 480 units

            // Define padding and calculate grid dimensions
            paddingY = 60; // Original padding
            paddingX = 90;
            gridWidth = totalWidth - 2 * paddingX;
            gridHeight = totalHeight - 2 * paddingY;

            movementDistance = 100; // Original movement distance

            // Calculate spacing between sprites
            columnSpacing = gridWidth / (columns - 1);
            rowSpacing = gridHeight / (rows - 1);

            // Rotation in radians
            rotationDegrees1 = 100;
            rotationRadians1 = rotationDegrees1 * (float)(Math.PI / 180);

            rotationDegrees2 = 80;
            rotationRadians2 = rotationDegrees2 * (float)(Math.PI / 180);

            // Precompute cosine and sine of rotation angle
            cosTheta1 = (float)Math.Cos(rotationRadians1);
            sinTheta1 = (float)Math.Sin(rotationRadians1);

            cosTheta2 = (float)Math.Cos(rotationRadians2);
            sinTheta2 = (float)Math.Sin(rotationRadians2);

            // Precompute movement vector components in screen coordinates
            movementDx1 = movementDistance * cosTheta1;
            movementDy1 = movementDistance * sinTheta1;

            movementDx2 = movementDistance * cosTheta2;
            movementDy2 = movementDistance * sinTheta2;

            // Screen center coordinates (center of the defined coordinate plane)
            screenCenterX = (minX + maxX) / 2; // 320
            screenCenterY = (minY + maxY) / 2; // 240

            for (int row = 0; row < rows; row++)
            {
                // Determine movement direction for this row
                int direction = (row % 2 == 0) ? 1 : -1; // 1 for right, -1 for left

                for (int col = 0; col < columns; col++)
                {
                    // Create the sprite
                    var spriteShadow = GetLayer("Bridge").CreateSprite(KnifeImage, OsbOrigin.Centre);
                    var sprite = GetLayer("Bridge").CreateSprite(KnifeImage, OsbOrigin.Centre);

                    // Calculate grid position (x0, y0) centered at (0,0)
                    float x0 = (col - (columns - 1) / 2.0f) * columnSpacing;
                    float y0 = (row - (rows - 1) / 2.0f) * rowSpacing;

                    // Rotate (x0, y0) by rotationRadians to get (x, y)
                    float x1 = x0 * cosTheta1 - y0 * sinTheta1;
                    float y1 = x0 * sinTheta1 + y0 * cosTheta1;

                    float x2 = x0 * cosTheta2 - y0 * sinTheta2;
                    float y2 = x0 * sinTheta2 + y0 * cosTheta2;

                    // Offset to screen coordinates
                    float xStart1 = x1 + screenCenterX;
                    float yStart1 = y1 + screenCenterY;

                    float xStart2 = x2 + screenCenterX;
                    float yStart2 = y2 + screenCenterY;

                    // Calculate end position based on movement direction
                    float xEnd1 = xStart1 + direction * movementDx1;
                    float yEnd1 = yStart1 + direction * movementDy1;

                    float xEnd2 = xStart2 + direction * movementDx2;
                    float yEnd2 = yStart2 + direction * movementDy2;

                    // Animate movement over time
                    sprite.Move(StartTime + 3264, StartTime + 3264 + 1632, xStart1, yStart1, xEnd1, yEnd1);
                    sprite.Move(StartTime + 3264 + 1632, StartTime + 5753, xStart2, yStart2, xEnd2, yEnd2);

                    spriteShadow.Move(StartTime + 3264, StartTime + 3264 + 1632, xStart1 + 5, yStart1 + 5, xEnd1 + 5, yEnd1 + 5);
                    spriteShadow.Move(StartTime + 3264 + 1632, StartTime + 5753, xStart2 + 5, yStart2 + 5, xEnd2 + 5, yEnd2 + 5);

                    // Set Fade and Scale
                    sprite.Fade(StartTime + 3264, 1);
                    spriteShadow.Fade(StartTime + 3264, 0.3f);
                    if (EndTime == 270404)
                    {
                        sprite.Fade(EndTime, 271226, 1, 0);
                        spriteShadow.Fade(EndTime, 271226, 0.3, 0);
                    }
                    else
                    {
                        sprite.Fade(EndTime, 0);
                        spriteShadow.Fade(EndTime, 0);
                    }

                    sprite.Scale(StartTime + 3264, 0.1f);
                    spriteShadow.Scale(StartTime + 3264, 0.1f);

                    if (direction == 1)
                    {
                        sprite.Rotate(StartTime + 3264, StartTime + 5753, MathHelper.DegreesToRadians(col * 30), MathHelper.DegreesToRadians(col * 30 + 180));
                        spriteShadow.Rotate(StartTime + 3264, StartTime + 5753, MathHelper.DegreesToRadians(col * 30), MathHelper.DegreesToRadians(col * 30 + 180));
                    }
                    else
                    {
                        sprite.Rotate(StartTime + 3264, StartTime + 5753, MathHelper.DegreesToRadians(col * 30 + 180), MathHelper.DegreesToRadians(col * 30));
                        spriteShadow.Rotate(StartTime + 3264, StartTime + 5753, MathHelper.DegreesToRadians(col * 30 + 180), MathHelper.DegreesToRadians(col * 30));
                    }

                    spriteShadow.Color(StartTime + 3264, Color4.Black);

                    sprite.Color(StartTime + 3264, Color4.Black);
                    sprite.Color(StartTime + 3264 + 408, Color4.Red);
                    sprite.Color(StartTime + 3264 + 612, Color4.White);

                    sprite.Color(StartTime + 1632 + 3264, Color4.Black);
                    sprite.Color(StartTime + 1632 + 3264 + 408, Color4.Red);
                    sprite.Color(StartTime + 1632 + 3264 + 612, Color4.White);

                    if (EndTime == 270404)
                    {
                        sprite.Color(269582, Color4.Black);

                    }
                }
            }


            var circleSprite1 = GetLayer("Bridge").CreateSprite(Circle, OsbOrigin.Centre);
            var circleSprite2 = GetLayer("Bridge").CreateSprite(Circle, OsbOrigin.Centre);
            var circleSprite3 = GetLayer("Bridge").CreateSprite(Circle, OsbOrigin.Centre);
            var circleSprite4 = GetLayer("Bridge").CreateSprite(Circle, OsbOrigin.Centre);

            // Circle Sakuya 1
            circleSprite1.Move(StartTime + 408, new Vector2(340, 280));
            circleSprite1.Color(StartTime + 408, Color4.Black);
            circleSprite1.Scale(OsbEasing.OutCirc, StartTime + 408, StartTime + 900, 0.7, 2.25);
            circleSprite1.Fade(StartTime + 408, 1);
            circleSprite1.Fade(StartTime + 900, 0);

            circleSprite2.Move(StartTime + 408, new Vector2(340, 280));
            circleSprite2.Color(StartTime + 408, Color4.Black);
            circleSprite2.Scale(OsbEasing.OutCirc, StartTime + 408, StartTime + 900, 0.6, 1.95);
            circleSprite2.Fade(StartTime + 408, 1);
            circleSprite2.Fade(StartTime + 900, 0);

            circleSprite3.Move(StartTime + 408, new Vector2(340, 280));
            circleSprite3.Color(StartTime + 408, Color4.Red);
            circleSprite3.Scale(OsbEasing.OutCirc, StartTime + 612, StartTime + 900, 0.7, 2.25);
            circleSprite3.Fade(StartTime + 612, 1);
            circleSprite3.Fade(StartTime + 900, 0);

            circleSprite4.Move(StartTime + 408, new Vector2(340, 280));
            circleSprite4.Color(StartTime + 408, Color4.Red);
            circleSprite4.Scale(OsbEasing.OutCirc, StartTime + 612, StartTime + 900, 0.6, 1.95);
            circleSprite4.Fade(StartTime + 612, 1);
            circleSprite4.Fade(StartTime + 900, 0);

            // Circle Sakuya 2

            circleSprite1.Move(StartTime + 1632 + 408, new Vector2(280, 280));
            circleSprite1.Scale(OsbEasing.OutCirc, StartTime + 1632 + 408, StartTime + 1632 + 900, 0.77, 2.475);
            circleSprite1.Fade(StartTime + 1632 + 408, 1);
            circleSprite1.Fade(StartTime + 1632 + 900, 0);

            circleSprite2.Move(StartTime + 1632 + 408, new Vector2(280, 280));
            circleSprite2.Scale(OsbEasing.OutCirc, StartTime + 1632 + 408, StartTime + 1632 + 900, 0.66, 2.145);
            circleSprite2.Fade(StartTime + 1632 + 408, 1);
            circleSprite2.Fade(StartTime + 1632 + 900, 0);

            circleSprite3.Move(StartTime + 1632 + 408, new Vector2(280, 280));
            circleSprite3.Scale(OsbEasing.OutCirc, StartTime + 1632 + 612, StartTime + 1632 + 900, 0.77, 2.475);
            circleSprite3.Fade(StartTime + 1632 + 612, 1);
            circleSprite3.Fade(StartTime + 1632 + 900, 0);

            circleSprite4.Move(StartTime + 1632 + 408, new Vector2(280, 280));
            circleSprite4.Scale(OsbEasing.OutCirc, StartTime + 1632 + 612, StartTime + 1632 + 900, 0.66, 2.145);
            circleSprite4.Fade(StartTime + 1632 + 612, 1);
            circleSprite4.Fade(StartTime + 1632 + 900, 0);

            // Circle Sakuya 3

            circleSprite1.Move(StartTime + 3264 + 408, new Vector2(320, 240));
            circleSprite1.Scale(OsbEasing.OutCirc, StartTime + 3264 + 408, StartTime + 3264 + 900, 0.77, 2.475);
            circleSprite1.Fade(StartTime + 3264 + 408, 1);
            circleSprite1.Fade(StartTime + 3264 + 900, 0);

            circleSprite2.Move(StartTime + 3264 + 408, new Vector2(320, 240));
            circleSprite2.Scale(OsbEasing.OutCirc, StartTime + 3264 + 408, StartTime + 3264 + 900, 0.66, 2.145);
            circleSprite2.Fade(StartTime + 3264 + 408, 1);
            circleSprite2.Fade(StartTime + 3264 + 900, 0);

            circleSprite3.Move(StartTime + 3264 + 408, new Vector2(320, 240));
            circleSprite3.Scale(OsbEasing.OutCirc, StartTime + 3264 + 612, StartTime + 3264 + 900, 0.77, 2.475);
            circleSprite3.Fade(StartTime + 3264 + 612, 1);
            circleSprite3.Fade(StartTime + 3264 + 900, 0);

            circleSprite4.Move(StartTime + 3264 + 408, new Vector2(320, 240));
            circleSprite4.Scale(OsbEasing.OutCirc, StartTime + 3264 + 612, StartTime + 3264 + 900, 0.66, 2.145);
            circleSprite4.Fade(StartTime + 3264 + 612, 1);
            circleSprite4.Fade(StartTime + 3264 + 900, 0);

            // Circle Sakuya 4

            circleSprite1.Move(StartTime + 4896 + 408, new Vector2(320, 240));
            circleSprite1.Scale(OsbEasing.OutCirc, StartTime + 4896 + 408, StartTime + 4896 + 900, 0.77, 2.475);
            circleSprite1.Fade(StartTime + 4896 + 408, 1);
            circleSprite1.Fade(StartTime + 4896 + 900, 0);

            circleSprite2.Move(StartTime + 4896 + 408, new Vector2(320, 240));
            circleSprite2.Scale(OsbEasing.OutCirc, StartTime + 4896 + 408, StartTime + 4896 + 900, 0.66, 2.145);
            circleSprite2.Fade(StartTime + 4896 + 408, 1);
            circleSprite2.Fade(StartTime + 4896 + 900, 0);

            circleSprite3.Move(StartTime + 4896 + 408, new Vector2(320, 240));
            circleSprite3.Scale(OsbEasing.OutCirc, StartTime + 4896 + 612, StartTime + 4896 + 900, 0.77, 2.475);
            circleSprite3.Fade(StartTime + 4896 + 612, 1);
            circleSprite3.Fade(StartTime + 4896 + 900, 0);

            circleSprite4.Move(StartTime + 4896 + 408, new Vector2(320, 240));
            circleSprite4.Scale(OsbEasing.OutCirc, StartTime + 4896 + 612, StartTime + 4896 + 900, 0.66, 2.145);
            circleSprite4.Fade(StartTime + 4896 + 612, 1);
            circleSprite4.Fade(StartTime + 4896 + 900, 0);

            // Transitions Out
            var whiteLine = GetLayer("Bridge").CreateSprite(Line, OsbOrigin.Centre);

            whiteLine.ScaleVec(OsbEasing.InCirc, StartTime + 1224, StartTime + 1632, 0, 1.5, 1, 1.5);
            whiteLine.Fade(StartTime + 1224, 1);
            whiteLine.Fade(StartTime + 1632, 0);
            whiteLine.Rotate(OsbEasing.InCirc, StartTime + 1224, StartTime + 1632, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(90));


            for (int x = -107; x <= 780; x += 40)
            {
                for (int y = 0; y <= 500; y += 40)
                {
                    var pixel = GetLayer("Bridge").CreateSprite(Pixel, OsbOrigin.Centre, new Vector2(x, y));
                    pixel.Color(StartTime + 2652, Color4.Black);
                    pixel.Fade(StartTime + 2652, 1);
                    pixel.Scale(OsbEasing.Out, StartTime + 2652, StartTime + 2856 + 408, 0, 20);
                    pixel.Rotate(OsbEasing.Out, StartTime + 2652, StartTime + 2856 + 408, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(90));
                    pixel.Fade(StartTime + 2856 + 408, 0);
                }
            }

            for (int y = 40; y <= 440; y += 80)
            {
                var x = (y / 80 % 2 == 0) ? -107 : 747;
                var bar = GetLayer("Bridge").CreateSprite(Pixel, (x == 747) ? OsbOrigin.CentreRight : OsbOrigin.CentreLeft, new Vector2(x, y));
                var currentTime = StartTime + (y / 80) * 102;
                bar.Fade(currentTime + 4315, 1);
                bar.ScaleVec(OsbEasing.OutCirc, currentTime + 4315, currentTime + 4315 + 102, 0, 40, 500, 40);
                bar.Fade(StartTime + 4931, 0);
            }

            var lineLast = GetLayer("Bridge").CreateSprite(Pixel, OsbOrigin.Centre);

            if (EndTime == 270404)
            {

            }
            else
            {
                lineLast.Color(EndTime - 408, Color4.Black);
                lineLast.Fade(EndTime - 408, 1);
                lineLast.ScaleVec(OsbEasing.In, EndTime - 408, EndTime, 450, 0, 450, 240);
                lineLast.Fade(EndTime, 0);
            }


            // Sakuya 1

            var sakuya1white = GetLayer("Bridge").CreateSprite(Sakuya1WhiteImage, OsbOrigin.Centre, new Vector2(380, 240));

            sakuya1white.Color(StartTime, Color4.Black);
            sakuya1white.Color(StartTime + 408, Color4.Red);
            sakuya1white.Color(StartTime + 612, Color4.White);
            sakuya1white.Fade(StartTime, 1);
            sakuya1white.Fade(StartTime + 1632, 0);
            sakuya1white.Scale(StartTime, 0.5);
            sakuya1white.MoveX(OsbEasing.Out, StartTime, StartTime + 408, 380, 410);
            sakuya1white.MoveX(OsbEasing.InCirc, StartTime + 1224, StartTime + 1632, 410, 1200);

            var sakuya1 = GetLayer("Bridge").CreateSprite(Sakuya1Image, OsbOrigin.Centre, new Vector2(410, 240));

            sakuya1.Fade(StartTime + 612, 1);
            sakuya1.Fade(EndTime, 0);
            sakuya1.Scale(StartTime + 816, 0.5);
            sakuya1.Fade(StartTime + 1632, 0);
            sakuya1.MoveX(OsbEasing.InCubic, StartTime + 1224, StartTime + 1632, 410, 1200);

            // Sakuya 2

            var sakuya2white = GetLayer("Bridge").CreateSprite(Sakuya2WhiteImage, OsbOrigin.Centre, new Vector2(200, 310));

            sakuya2white.Color(StartTime + 1632, Color4.Black);
            sakuya2white.Color(StartTime + 1632 + 408, Color4.Red);
            sakuya2white.Color(StartTime + 1632 + 612, Color4.White);
            sakuya2white.Fade(StartTime + 1632, 1);
            sakuya2white.Fade(StartTime + 1632 + 1632, 0);
            sakuya2white.Scale(StartTime + 1632, 0.5);
            sakuya2white.MoveY(OsbEasing.Out, StartTime + 1632, StartTime + 1632 + 408, 310, 280);
            sakuya2white.MoveY(OsbEasing.InCirc, StartTime + 1632 + 1224, StartTime + 1632 + 1632, 280, -350);

            var sakuya2 = GetLayer("Bridge").CreateSprite(Sakuya2Image, OsbOrigin.Centre, new Vector2(200, 280));

            sakuya2.Fade(StartTime + 1632 + 612, 1);
            sakuya2.Fade(EndTime, 0);
            sakuya2.Scale(StartTime + 1632 + 816, 0.5);
            sakuya2.Fade(StartTime + 1632 + 1632, 0);
            sakuya2.MoveY(OsbEasing.InCubic, StartTime + 1632 + 1224, StartTime + 1632 + 1632, 280, -350);

            // Sakuya 3

            var sakuya3white = GetLayer("Bridge").CreateSprite(Sakuya3WhiteImage, OsbOrigin.Centre, new Vector2(340, 200));

            sakuya3white.Color(StartTime + 3264, Color4.Black);
            sakuya3white.Color(StartTime + 3264 + 408, Color4.Red);
            sakuya3white.Color(StartTime + 3264 + 612, Color4.White);
            sakuya3white.Fade(StartTime + 3264, 1);
            sakuya3white.Fade(StartTime + 3264 + 1632, 0);
            sakuya3white.Scale(OsbEasing.Out, StartTime + 3264, StartTime + 3264 + 408, 0.3, 0.5);
            sakuya3white.MoveY(OsbEasing.Out, StartTime + 3264, StartTime + 3264 + 408, 200, 230);
            sakuya3white.MoveY(OsbEasing.InCirc, StartTime + 3264 + 1224, StartTime + 3264 + 1632, 230, 600);
            sakuya3white.Scale(OsbEasing.InCubic, StartTime + 3264 + 1224, StartTime + 3264 + 1632, 0.5, 0.2);

            var sakuya3 = GetLayer("Bridge").CreateSprite(Sakuya3Image, OsbOrigin.Centre, new Vector2(340, 230));

            sakuya3.Fade(StartTime + 3264 + 612, 1);
            sakuya3.Fade(EndTime, 0);
            sakuya3.Scale(StartTime + 3264 + 816, 0.5);
            sakuya3.Fade(StartTime + 3264 + 1632, 0);
            sakuya3.MoveY(OsbEasing.InCubic, StartTime + 3264 + 1224, StartTime + 3264 + 1632, 230, 600);
            sakuya3.Scale(OsbEasing.InCubic, StartTime + 3264 + 1224, StartTime + 3264 + 1632, 0.5, 0.2);

            // Sakuya 4

            var sakuya4white = GetLayer("Bridge").CreateSprite(Sakuya4WhiteImage, OsbOrigin.Centre, new Vector2(340, 200));

            sakuya4white.Color(StartTime + 4896, Color4.Black);
            sakuya4white.Color(StartTime + 4896 + 408, Color4.Red);
            sakuya4white.Color(StartTime + 4896 + 612, Color4.White);
            sakuya4white.Fade(StartTime + 4896, 1);
            if (EndTime == 270404)
            {
                sakuya4white.Fade(EndTime, 271226, 1, 0);
            }
            else
            {
                sakuya4white.Fade(StartTime + 4896 + 1632, 0);
            }
            sakuya4white.Scale(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 408, 1, 0.5);
            sakuya4white.MoveY(OsbEasing.Out, StartTime + 4896, StartTime + 4896 + 408, 310, 280);
            if (EndTime != 270404)
            {
                sakuya4white.MoveY(OsbEasing.InCirc, StartTime + 4896 + 1224, StartTime + 4896 + 1632, 280, -350);
            }

            var sakuya4 = GetLayer("Bridge").CreateSprite(Sakuya4Image, OsbOrigin.Centre, new Vector2(340, 200));

            sakuya4.Fade(StartTime + 4896 + 612, 1);
            sakuya4.Fade(EndTime, 0);
            sakuya4.Scale(StartTime + 4896 + 816, 0.5);
            sakuya4.MoveY(OsbEasing.InCubic, StartTime + 4896 + 1224, StartTime + 4896 + 1632, 280, -350);

            if (EndTime == 270404)
            {
                sakuya4.Fade(269582, 0);
                sakuya4white.Color(269582, Color4.Black);
            }
            else
            {
                sakuya4.Fade(StartTime + 4896 + 1632, 0);
            }

            // Transition Lines Sakuya 1
            var line2 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);

            line2.Move(StartTime, new Vector2(-107, 0));
            line2.Color(StartTime, Color4.Red);
            line2.Fade(StartTime, 1);
            line2.ScaleVec(OsbEasing.OutExpo, StartTime, StartTime + 816, 1.1, 1, 1.1, 0.2);
            line2.ScaleVec(OsbEasing.InExpo, StartTime + 816, StartTime + 1632, 1.1, 0.2, 1.1, 0);
            line2.Rotate(StartTime, MathHelper.DegreesToRadians(-45));

            var line1 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);

            line1.Move(StartTime, new Vector2(-107, 0));
            line1.Color(StartTime, Color4.Black);
            line1.Fade(StartTime, 0.5);
            line1.ScaleVec(OsbEasing.OutExpo, StartTime, StartTime + 816, 1.1, 1, 1.1, 0.15);
            line1.ScaleVec(OsbEasing.InExpo, StartTime + 816, StartTime + 1632, 1.1, 0.15, 1.1, 0);
            line1.Rotate(StartTime, MathHelper.DegreesToRadians(-45));

            var line3 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);

            line3.Move(StartTime, new Vector2(-107, 0));
            line3.Color(StartTime, Color4.Black);
            line3.Fade(StartTime, 1);
            line3.ScaleVec(OsbEasing.OutExpo, StartTime, StartTime + 816, 1.1, 1, 1.1, 0.1);
            line3.ScaleVec(OsbEasing.InExpo, StartTime + 816, StartTime + 1632, 1.1, 0.1, 1.1, 0);
            line3.Rotate(StartTime, MathHelper.DegreesToRadians(-45));

            // Transition Lines Sakuya 2

            line2.Color(StartTime + 1632, Color4.Red);
            line2.Move(StartTime + 1632, new Vector2(0, -200));
            line2.ScaleVec(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, 1.3, 1, 1.3, 0.075);
            line2.ScaleVec(OsbEasing.InExpo, StartTime + 1632 + 816, StartTime + 1632 + 1632, 1.3, 0.075, 1.3, 0);
            line2.Rotate(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(15));

            line1.Color(StartTime + 1632, Color4.White);
            line1.Move(StartTime + 1632, new Vector2(0, -200));
            line1.ScaleVec(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, 1.3, 1, 1.3, 0.05);
            line1.ScaleVec(OsbEasing.InExpo, StartTime + 1632 + 816, StartTime + 1632 + 1632, 1.3, 0.05, 1.3, 0);
            line1.Rotate(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(15));

            line3.Color(StartTime + 1632, Color4.White);
            line3.Move(StartTime + 1632, new Vector2(0, -200));
            line3.ScaleVec(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, 1.3, 1, 1.3, 0.025);
            line3.ScaleVec(OsbEasing.InExpo, StartTime + 1632 + 816, StartTime + 1632 + 1632, 1.3, 0.025, 1.3, 0);
            line3.Rotate(OsbEasing.OutExpo, StartTime + 1632, StartTime + 1632 + 816, MathHelper.DegreesToRadians(0), MathHelper.DegreesToRadians(15));

            // Transition Lines Sakuya 3

            line2.Color(StartTime + 3264, Color4.Red);
            line2.Move(StartTime + 3264, new Vector2(0, 480));
            line2.ScaleVec(OsbEasing.OutExpo, StartTime + 3264, StartTime + 3264 + 816, 1.3, 1, 1.3, 0.075);
            line2.ScaleVec(OsbEasing.InExpo, StartTime + 3264 + 816, StartTime + 3264 + 1632, 1.3, 0.075, 1.3, 0);
            line2.Rotate(StartTime + 3264, MathHelper.DegreesToRadians(180));

            line1.Color(StartTime + 3264, Color4.Black);
            line1.Move(StartTime + 3264, new Vector2(0, 480));
            line1.ScaleVec(OsbEasing.OutExpo, StartTime + 3264, StartTime + 3264 + 816, 1.3, 1, 1.3, 0.05);
            line1.ScaleVec(OsbEasing.InExpo, StartTime + 3264 + 816, StartTime + 3264 + 1632, 1.3, 0.05, 1.3, 0);
            line1.Rotate(StartTime + 3264, MathHelper.DegreesToRadians(180));

            line3.Color(StartTime + 3264, Color4.Black);
            line3.Move(StartTime + 3264, new Vector2(0, 480));
            line3.ScaleVec(OsbEasing.OutExpo, StartTime + 3264, StartTime + 3264 + 816, 1.3, 1, 1.3, 0.025);
            line3.ScaleVec(OsbEasing.InExpo, StartTime + 3264 + 816, StartTime + 3264 + 1632, 1.3, 0.025, 1.3, 0);
            line3.Rotate(StartTime + 3264, MathHelper.DegreesToRadians(180));

            // Transition Lines Sakuya 4

            line2.Color(StartTime + 4896, Color4.Red);
            line2.Move(StartTime + 4896, new Vector2(-107, 480));
            line2.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.075);
            line2.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.075, 1.3, 0);
            line2.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(270));

            line1.Color(StartTime + 4896, Color4.White);
            line1.Move(StartTime + 4896, new Vector2(-107, 480));
            line1.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.05);
            line1.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.05, 1.3, 0);
            line1.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(270));

            line3.Color(StartTime + 4896, Color4.White);
            line3.Move(StartTime + 4896, new Vector2(-107, 480));
            line3.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.025);
            line3.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.025, 1.3, 0);
            line3.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(270));

            var line5 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);
            var line4 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);
            var line6 = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.TopCentre);

            line5.Color(StartTime + 4896, Color4.White);
            line5.Fade(StartTime + 4896, 1);
            line5.Move(StartTime + 4896, new Vector2(747, 480));
            line5.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.05);
            line5.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.05, 1.3, 0);
            line5.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(90));

            line4.Color(StartTime + 4896, Color4.Red);
            line4.Fade(StartTime + 4896, 0.5);
            line4.Move(StartTime + 4896, new Vector2(747, 480));
            line4.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.075);
            line4.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.075, 1.3, 0);
            line4.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(90));

            line6.Color(StartTime + 4896, Color4.White);
            line6.Fade(StartTime + 4896, 1);
            line6.Move(StartTime + 4896, new Vector2(747, 480));
            line6.ScaleVec(OsbEasing.OutExpo, StartTime + 4896, StartTime + 4896 + 816, 1.3, 1, 1.3, 0.025);
            line6.ScaleVec(OsbEasing.InExpo, StartTime + 4896 + 816, StartTime + 4896 + 1632, 1.3, 0.025, 1.3, 0);
            line6.Rotate(StartTime + 4896, MathHelper.DegreesToRadians(90));

            // Gloal Dim
            var dim = GetLayer("BridgeTop").CreateSprite(Line, OsbOrigin.Centre);

            dim.Color(StartTime, Color4.Black);
            dim.Fade(StartTime, 0.2);
            if (EndTime != 270404)
            {
                dim.Fade(EndTime, 0);
            }
            else
            {
                dim.Fade(269582, 0);
            }

        }
    }
}