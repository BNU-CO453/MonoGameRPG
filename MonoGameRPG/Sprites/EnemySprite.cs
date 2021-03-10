using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class EnemySprite : AnimatedSprite
    {
        public int EnemyNo { get; set; }

        public PlayerSprite Player { get; set; }

        private bool debug = true;

        private int enemySpeed = 120;

        public EnemySprite(Texture2D image, int x, int y) : 
            base(image, x, y)
        {
            Speed = enemySpeed;
        }

        /// <summary>
        /// Move this sprite towards the player's position
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            // Update Animation but do not move

            base.Update(gameTime);


            Vector2 direction = Player.Position - Position;
            direction.Normalize();
            
            //Position += (direction * Speed) * deltaTime;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if(debug)
            {
                //spriteBatch.DrawString()
            }

        }
    }
}
