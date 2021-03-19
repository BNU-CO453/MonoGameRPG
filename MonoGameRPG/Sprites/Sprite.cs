using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameRPG.Helpers;
using System;

namespace MonoGameRPG.Sprites
{
    /// <summary>
    /// This is the base class for all 2D Sprites
    /// A Sprite is a 2D object which has at least
    /// one image, a postion and a speed of movement.
    /// </summary>
    /// <Author>
    /// Derek Peacock
    /// </Author>
    public class Sprite: ICloneable
    {
        // Structures
        public Texture2D Image { get; set; }

        public Vector2 Position { get; set; }

        // A rectangle limiting where the sprite can move
        public Rectangle Boundary { get; set; }

        // Point around which the sprite rotates
        public Vector2 Origin 
        {
            get 
            {
                if (Image == null)
                    return Vector2.Zero;
                else
                    return  new Vector2(Position.X - Width / 2,
                                        Position.Y - Height / 2);
            }
        }

        // Properties
        public int Speed { get; set; }

        public Vector2 Direction { get; set; }

        public Color Color = Color.White;


        public float Rotation = 0f;

        public float RotationSpeed = 0f;

        public float Scale = 1f;

        public SpriteEffects SpriteEffect;

        public SpriteFont TextFont { get; set; }

        public bool IsVisible { get; set; }

        public bool IsAlive { get; set; }

        public bool IsActive { get; set; }

        public int Width 
        {
            get { return (int)(Image.Width * Scale); }
        }

        public int Height 
        {
            get { return (int)(Image.Height * Scale); }
        }

        // The rectangle occupied by the unscaled image
        public Rectangle BoundingBox 
        {
            get
            {
                return new Rectangle
                (
                    (int)Position.X, 
                    (int)Position.Y,
                    Width, Height
                );
            }
        }
        
        // Variables

        protected float deltaTime;

        protected bool debug = true;

        /// <summary>
        /// Constructor initialises the sprite to face right
        /// position (0, 0) with no speed but is visible
        /// active and alive.
        /// </summary>
        public Sprite()
        {
            Position = new Vector2(100, 0);

            Direction = new Vector2(1, 0);

            IsVisible = true;
            IsAlive = true;
            IsActive = true;

            Scale = 1;
        }


        /// <summary>
        /// Add a single image for the sprite and
        /// then initialise it.
        /// </summary>
        /// <param name="image"></param>
        public Sprite(Texture2D image) : this()
        {
            Image = image;
        }

        public virtual void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            if(IsActive && IsAlive)
            {
                Vector2 newPosition = Position + ((Direction * Speed) * deltaTime);

                if(Boundary.Width == 0 || Boundary.Height == 0)
                {
                    Position = newPosition;
                }
                else if (newPosition.X >= Boundary.X && 
                    newPosition.Y >= Boundary.Y &&
                    newPosition.X + Width < Boundary.X + Boundary.Width &&
                    newPosition.Y + Height < Boundary.Y + Boundary.Height)
                {
                    Position = newPosition;
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(debug)
            {
                TextHelper.DrawString(
                    $"({Position.X:0},{Position.Y:0})", Position);
            }

            Rectangle destination = new Rectangle
                ((int)Position.X, (int)Position.Y, Width, Height);

            
            spriteBatch.Draw(Image, BoundingBox, Color.White);

            //spriteBatch.Draw
            //    (Image,
            //     Position,
            //     null,
            //     Color.White, Rotation, Origin,
            //     Scale, SpriteEffect, 10f);
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
