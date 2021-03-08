using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameRPG.Tools;

namespace MonoGameRPG.Sprites
{
    /// <summary>
    /// This Sprite contains an Array of Animations
    /// one for each of four directions, Up,Down,Left & Right
    /// When the sprite stops the animation stops
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        public GraphicsDevice Graphics { get; set; }

        public int MaxAnimations = 12;

        public SpriteAnimation Animation { get; set; }

        public SpriteAnimation[] Animations { get; set; }


        public AnimatedSprite(int x, int y) : base(x, y) 
        {
            Animations = new SpriteAnimation[MaxAnimations];
        }

        /// <summary>
        /// Animations [0] to [3] are walks in the four directions
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Animation != null)
            {
                if((Image == null) && (Graphics != null))
                {
                    Image = Animation.SpriteSheet.CreateTexture(
                        Graphics, Animation.Rectangles[0]);
                }

                Animation = Animations[(int)Direction];

                Animation.Position = Position;

                if (IsActive)
                    Animation.Update(gameTime);
                else
                    Animation.SetFrame(1);
            }
        }

        /// <summary>
        /// If a current animation is set draw that 
        /// otherwise draw a static image
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Animation != null)
            {
                Animation.Draw(spriteBatch);
            }
            else
            {
                spriteBatch.Draw(Image,
                    Position,
                    new Rectangle(0, 0, Width, Height),
                    Color, Rotation, Origin,
                    Scale, SpriteEffect, 0f);
            }
        }

    }
}
