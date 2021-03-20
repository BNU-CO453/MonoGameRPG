using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameRPG.Helpers;

namespace MonoGameRPG.Sprites
{
    /// <summary>
    /// This Sprite contains an Array of Animations
    /// one for each of four directions, Up,Down,Left & Right
    /// When the sprite stops the animation stops
    /// </summary>
    public class AnimatedSprite : Sprite, System.ICloneable
    {
        public GraphicsDevice Graphics { get; set; }

        public int MaxAnimations = 12;

        public SpriteAnimation Animation { get; set; }

        public SpriteAnimation[] Animations { get; set; }

        public int LastAnimation { get; set; }

        public AnimatedSprite() : base(null) 
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

                if (Animations[3] != null)
                {
                    if(Direction.X > 0 && Direction.Y < Direction.X)
                        Animation = Animations[0];

                    else if (Direction.Y > 0 && Direction.X < Direction.Y)
                        Animation = Animations[1];

                    else if (Direction.X < 0 && Direction.X < Direction.Y)
                        Animation = Animations[2];

                    else if (Direction.Y < 0 && Direction.Y < Direction.X)
                        Animation = Animations[3];
                }
                else
                    Animation = Animations[0];

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
                if (debug)
                {
                    TextHelper.DrawString(
                        $"({Position.X:0},{Position.Y:0})", Position);
                }

                Animation.Scale = Scale;
                Animation.Draw(spriteBatch);
            }
            else
            {
                base.Draw(spriteBatch);
            }
        }

        public override object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
