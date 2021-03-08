using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class ProjectileController
    {
        private List<Sprite> sprites ;

        private Texture2D image;

        public ProjectileController(Texture2D image)
        {
            sprites =new List<Sprite>();
            this.image = image;

        }

        /// <summary>
        /// Create a new projectile sprite and add it to the
        /// list of projectiles.
        /// </summary>
        public void Fire(Vector2 position, Directions direction)
        {
            Sprite sprite = new Sprite((int)position.X, (int)position.Y);
            
            sprite.Image = image;
            sprite.Speed = Sprite.MinSpeed * 2;

            sprite.IsActive = true;
            sprite.Direction = direction;

            if(direction == Directions.Left)
            {
                position.X -= (float)sprite.Width / 2;
            }
            else if (direction == Directions.Right)
            {
                position.X += (float)sprite.Width / 2;
            }
            else if (direction == Directions.Down)
            {
                position.Y += (float)sprite.Height / 2;
            }
            else if (direction == Directions.Up)
            {
                position.Y -= (float)sprite.Height / 2;
            }

            sprite.Position = position;

            sprites.Add(sprite);
        }

        /// <summary>
        /// TODO Need to delete those projectiles that
        /// have gone off the screen/map
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Sprite sprite in sprites)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public bool IsLoaded()
        {
            return sprites.Count > 0;
        }

        public void CheckforHits(EnemySprite enemy)
        {
            foreach(Sprite projectile in sprites)
            {
                if(projectile.BoundingBox.Intersects(enemy.BoundingBox))
                {
                    enemy.IsAlive = false;
                    projectile.IsAlive = false;
                }
            }

            sprites.RemoveAll(p => !p.IsAlive);
        }
    }
}
