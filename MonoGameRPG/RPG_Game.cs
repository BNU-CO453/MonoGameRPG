using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using MonoGameRPG.Sprites;
using System.Collections.Generic;
using MonoGameRPG.Helpers;

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

        private SpriteFont gameFont;
        private SpriteFont timeFont;
        private SpriteFont arialFont;

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

            TextHelper.LoadFont(Content, spriteBatch);

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timeFont = Content.Load<SpriteFont>("timerFont");
            arialFont = Content.Load<SpriteFont>("Arial");

            backgroundImage = Content.Load<Texture2D>("game_background_4");

            ballImage = Content.Load<Texture2D>("ball");

            SetupPlayerSprite();

            SetupEnemySprites();
        }

        private void SetupEnemySprites()
        {

            Texture2D enemySheet = Content.Load<Texture2D>("rsc-sprite-sheet3");
            SpriteSheetHelper helper = new SpriteSheetHelper(graphics, enemySheet, 4, 3);

            EnemySprite enemy = new EnemySprite();
            enemy.Position = new Vector2(100, 820);
            enemy.Speed = 60;
            enemy.Graphics = graphics;

            enemy.Animations[0] = new SpriteAnimation(enemySheet, 10, 8);

            enemy.Animation = enemy.Animations[0];
            enemy.Scale = 3.0f;
            enemy.Player = player;
            enemy.Speed = 100;

            SetWalkAnimations(enemy, enemySheet);

            enemyController = new EnemyController(enemy);
        }

        private void SetupPlayerSprite()
        {
            Texture2D rscSheet = Content.Load<Texture2D>("rsc-sprite-sheet1");
            
            player = new PlayerSprite();
            player.Position = new Vector2(600, 820);

            player.Speed = 200;
            player.Scale = 3.0f;

            player.Boundary = new Rectangle(0, 810, backgroundImage.Width, 1740 - 810);

            player.TextFont = arialFont;

            SetWalkAnimations(player, rscSheet);

            player.AddProjectiles(ballImage);

        }

        private void SetWalkAnimations(AnimatedSprite sprite, Texture2D sheet)
        {
            SpriteSheetHelper helper = new SpriteSheetHelper(graphics,
                sheet, 4, 3);

            sprite.Animations[(int)DirectionsKeys.Down] =
                new SpriteAnimation(helper.AnimationRow[0], 3, 10);

            sprite.Animations[(int)DirectionsKeys.Left] =
                new SpriteAnimation(helper.AnimationRow[1], 3, 10);

            sprite.Animations[(int)DirectionsKeys.Right] =
                new SpriteAnimation(helper.AnimationRow[2], 3, 10);

            sprite.Animations[(int)DirectionsKeys.Up] =
                new SpriteAnimation(helper.AnimationRow[3], 3, 10);

            sprite.Animation = sprite.Animations[(int)DirectionsKeys.Left];
            sprite.LastAnimation = 4;

            Texture2D image = helper.FirstFrame;
            sprite.Image = image;
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
