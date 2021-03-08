using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using MonoGameRPG.Sprites;
using MonoGameRPG.Tools;
using System.Collections.Generic;

namespace MonoGameRPG
{
    public class RPG_Game : Game
    {
        // constants

        public const int HD_Height = 720;
        public const int HD_Width = 1280;

        // Attributes/variables

        private GraphicsDeviceManager graphicsManager;
        private GraphicsDevice graphics;

        private SpriteBatch spriteBatch;

        private Texture2D playerImage;

        private Texture2D walkDownImages;
        private Texture2D walkUpImages;
        private Texture2D walkLeftImages;
        private Texture2D walkRightImages;

        private Texture2D backgroundImage;
        private Texture2D ballImage;
        private Texture2D skullImage;

        private PlayerSprite player;
        private EnemyController enemyController;

        private AdjustableCamera adjustableCamera;
        private Camera camera;

        private Viewport viewPort;

        public RPG_Game()
        {
            graphicsManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Setup the screen to the preferred size in
        /// this example it is HD 720p (1280 x 720)
        /// full HD is 1080P (1920 x 1080) pixells
        /// </summary>
        protected override void Initialize()
        {
            graphicsManager.PreferredBackBufferWidth = HD_Width;
            graphicsManager.PreferredBackBufferHeight = HD_Height;
            graphicsManager.ApplyChanges();

            graphics = graphicsManager.GraphicsDevice;

            viewPort = new Viewport(0,0, HD_Width, HD_Height);

            adjustableCamera = new AdjustableCamera(viewPort);

            camera = new Camera(graphicsManager.GraphicsDevice);
            
            base.Initialize();
        }

        /// <summary>
        /// Load all the images for the game from the
        /// Content pipeline.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            playerImage = Content.Load<Texture2D>("Player/player");
            
            walkDownImages = Content.Load<Texture2D>("Player/walkDown");
            walkUpImages = Content.Load<Texture2D>("Player/walkUp");
            walkRightImages = Content.Load<Texture2D>("Player/walkRight");
            walkLeftImages = Content.Load<Texture2D>("Player/walkLeft");

            backgroundImage = Content.Load<Texture2D>("background");
            ballImage = Content.Load<Texture2D>("ball");
            skullImage = Content.Load<Texture2D>("skull");

            SetupPlayerSprite();

            SetupEnemySprites();
        }

        private void SetupEnemySprites()
        {
            EnemySprite enemy = new EnemySprite(500, 400);
            enemy.Graphics = graphics;

            enemy.Animations[0] = new SpriteAnimation(skullImage, 10, 8);

            enemy.Animation = enemy.Animations[0];
            enemy.Player = player;

            enemyController = new EnemyController(enemy);
        }

        private void SetupPlayerSprite()
        {
            player = new PlayerSprite(800, 700);
            player.Speed = 200;

            player.Image = playerImage;

            player.Animations[(int)Directions.Down] = 
                new SpriteAnimation(walkDownImages, 4, 10);

            player.Animations[(int)Directions.Left] =
                new SpriteAnimation(walkLeftImages, 4, 10);

            player.Animations[(int)Directions.Right] =
                new SpriteAnimation(walkRightImages, 4, 10);

            player.Animations[(int)Directions.Up] =
                new SpriteAnimation(walkUpImages, 4, 10);

           player.Animation = player.Animations[(int)Directions.Left];

           player.AddProjectiles(ballImage);

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);

            enemyController.Update(gameTime);

            foreach (EnemySprite enemy in enemyController.Enemies)
            {
                player.Projectiles.CheckforHits(enemy);
            }

            enemyController.Enemies.RemoveAll(e => !e.IsAlive);

            camera.Position = player.Position;
            camera.Update(gameTime);

            //camera.Update(viewPort);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // spriteBatch.Begin(transformMatrix: camera.Transform);
            spriteBatch.Begin(camera);

            Vector2 position = new Vector2(0, 0);

            spriteBatch.Draw(
                backgroundImage, position, Color.White);

            player.Draw(spriteBatch);

            enemyController.Draw(spriteBatch);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
