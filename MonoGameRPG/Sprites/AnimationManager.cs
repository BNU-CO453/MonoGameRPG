﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameRPG.Helpers;

namespace MonoGameRPG.Sprites
{
    public class AnimationManager
    {
        public Texture2D SpriteSheet;

        public Vector2 Position = Vector2.Zero;
        
        public Color Color = Color.White;
        
        public Vector2 Origin;
        
        public float Rotation = 0f;
        
        public float Scale = 1f;
        
        public SpriteEffects SpriteEffect;
        
        public Rectangle[] Rectangles;
        
        protected int FrameIndex = 0;

        public AnimationManager(Texture2D Texture, int frames)
        {
            this.SpriteSheet = Texture;
            int width = Texture.Width  / frames;
            int height = Texture.Height;

            Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
                Rectangles[i] = new Rectangle(i * width, 0, width, Texture.Height);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, 
                Position, Rectangles[FrameIndex], 
                Color, Rotation, Origin, Scale, SpriteEffect, 1f);
        }
    }

    public class SpriteAnimation : AnimationManager
    {
        public bool IsLooping = true;


        private float timeToUpdate; //default, you may have to change it

        private float timeElapsed;

        public SpriteAnimation(Texture2D Texture, int frames, int fps)
            : base(Texture, frames) 
        {
            timeElapsed = 0;
            timeToUpdate = 1.0f / (float)fps; 
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed = 0;

                if (FrameIndex < Rectangles.Length - 1)
                    FrameIndex++;

                else if (IsLooping)
                    FrameIndex = 0;
            }
        }

        public void SetFrame(int frame)
        {
            FrameIndex = frame;
        }
    }
}