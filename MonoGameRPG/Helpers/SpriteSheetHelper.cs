﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Helpers
{
    public class SpriteSheetHelper
    {
        public Texture2D SpriteSheet { get; set; }

        private readonly int rows;

        private  readonly int frames;

        private int sheetWidth;

        private int frameHeight;

        private int frameWidth;
        public Texture2D FirstFrame { get; }

        public Texture2D [] AnimationRow { get; }

        public SpriteSheetHelper(GraphicsDevice graphics, 
            Texture2D sheet, int rows, int cols)
        {
            SpriteSheet = sheet;
            this.rows = rows;
            this.frames = cols;

            frameHeight = sheet.Height / rows;
            sheetWidth = SpriteSheet.Width;
            frameWidth = sheetWidth / cols;

            AnimationRow = new Texture2D[rows];

            for (int row = 0; row < rows; row++)
            {
                Texture2D Image = SpriteSheet.CreateTexture(
                    graphics, new Rectangle(0, row * frameHeight, 
                                            sheetWidth, frameHeight));
                AnimationRow[row] = Image;
            }

            FirstFrame = AnimationRow[0].CreateTexture(
                graphics, new Rectangle(0, 0, frameWidth, frameHeight));
        }

    }
}
