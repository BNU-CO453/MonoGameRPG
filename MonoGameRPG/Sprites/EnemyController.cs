using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameRPG.Sprites
{
    public class EnemyController
    {
        public List<EnemySprite> Enemies { get; set; }
        public static double MaxTime = 5.0;

        public EnemySprite EnemyTemplate { get; set; }

        private double timer;

        public EnemyController(EnemySprite enemyTemplate)
        {
            EnemyTemplate = enemyTemplate;
            Enemies = new List<EnemySprite>();
            Enemies.Add(enemyTemplate);
            
            timer = MaxTime;
        }

        public void Update(GameTime gameTime)
        {
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if(timer <= 0)
            {
                EnemySprite enemy = EnemyTemplate.Clone() as EnemySprite;

                Enemies.Add(enemy);
                timer = MaxTime;
            }

            foreach (EnemySprite enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EnemySprite enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}
