using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameRPG.Sprites
{
    public class PlayerSprite : AnimatedSprite
    {
        public ProjectileController Projectiles { get; set; }

        private KeyboardState lastKeyState;

        public PlayerSprite(int x, int y) : base(x, y) { }

        /// <summary>
        /// delta time is the time elapsed in second (usualy 1/60 second)
        /// 0.01667 seconds.  So to move 1 pixel at 60 fps the speed should
        /// be set to 60.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState keyState = Keyboard.GetState();

            float newX, newY;
            IsActive = false;

            if (keyState.IsKeyDown(Keys.Space) && 
                Projectiles != null &&
                lastKeyState.IsKeyUp(Keys.Space))
            {
                Projectiles.Fire(Position, Direction);
            }
            else
            {
                if (keyState.IsKeyDown(Keys.Right))
                {
                    Direction = Directions.Right;
                    IsActive = true;
                }

                if (keyState.IsKeyDown(Keys.Left))
                {
                    Direction = Directions.Left;
                    IsActive = true;
                }

                if (keyState.IsKeyDown(Keys.Up))
                {
                    Direction = Directions.Up;
                    IsActive = true;
                }

                if (keyState.IsKeyDown(Keys.Down))
                {
                    Direction = Directions.Down;
                    IsActive = true;
                }
            }


            lastKeyState = keyState;

            if (Projectiles != null)
                 Projectiles.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

             if(Projectiles != null  && Projectiles.IsLoaded())
            {
                 Projectiles.Draw(spriteBatch);
            }
        }

        public void AddProjectiles(Texture2D image)
        {
            Projectiles = new ProjectileController(image);
        }
    }
}
