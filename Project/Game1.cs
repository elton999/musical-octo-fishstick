using UmbrellaToolsKit;
using UmbrellaToolsKit.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project.Entities;

namespace Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private AssetManagement _assetManagement;
        private GameManagement _gameManagement;

        private int _currentLevel = 1;
        private int _maxLevel = 9;
        private bool _loadScene = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameManagement = new GameManagement(this);
            _gameManagement.Game = this;
            _gameManagement.Start();

            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _assetManagement = new AssetManagement();
            _assetManagement.Set<Gameplay.SceneTransition>("player", Layers.UI);
            _assetManagement.Set<Gameplay.LightPointEffect>("lightPoint", Layers.MIDDLEGROUND);
            _assetManagement.Set<Gameplay.Light>("player", Layers.BACKGROUND);

            _assetManagement.Set<Player>("player", Layers.PLAYER);
            _assetManagement.Set<Enemy>("enemy", Layers.ENEMIES);

            _assetManagement.Set<Gameplay.Nodes>("node", Layers.FOREGROUND);
            _assetManagement.Set<Gameplay.Ladder>("ladder", Layers.MIDDLEGROUND);
            _assetManagement.Set<Gameplay.Platform>("platform", Layers.MIDDLEGROUND);
            _assetManagement.Set<Gameplay.Spike>("spike", Layers.MIDDLEGROUND);
            _assetManagement.Set<Text>("ShowText", Layers.MIDDLEGROUND);

            _assetManagement.Set<Gameplay.Door>("door", Layers.MIDDLEGROUND);
            _assetManagement.Set<Gameplay.HideWayHitBox>("HideWayHitBox", Layers.MIDDLEGROUND);
            _assetManagement.Set<Gameplay.Key>("key", Layers.FOREGROUND);
            _assetManagement.Set<Gameplay.RedKey>("redKey", Layers.FOREGROUND);

            KeyBoardHandler.AddInput(Keys.Left);
            KeyBoardHandler.AddInput(Keys.Right);
            KeyBoardHandler.AddInput(Keys.Up);
            KeyBoardHandler.AddInput(Keys.Down);
            KeyBoardHandler.AddInput(Keys.Z);

            LoadScene();

            Player.OnDieDaley += ReloadLevel;
            Gameplay.Door.OnEnterDoorDelay += LoadNextLevel;
        }

        protected override void UnloadContent()
        {
            Player.OnDieDaley += ReloadLevel;
            Gameplay.Door.OnEnterDoorDelay += LoadNextLevel;

            base.UnloadContent();
        }

        public void ReloadLevel() => _loadScene = true;

        public void LoadNextLevel()
        {
            _loadScene = true;
            _currentLevel++;

            if (_currentLevel > _maxLevel) _currentLevel = 1;
        }

        public void UpdateScene()
        {
            LoadScene();
            _loadScene = false;
        }

        public void DestroyCurrentLevel()
        {
            var scene = _gameManagement.SceneManagement.MainScene;
            scene.Dispose();
        }

        public void LoadScene()
        {
            var scene = _gameManagement.SceneManagement.MainScene;
            LoadLevel(scene);
        }

        public void LoadLevel(Scene scene)
        {
            scene.Dispose();
            scene.SetLevel(_currentLevel);
            scene.SetBackgroundColor = Color.Black;
            scene.Camera.Position = scene.Sizes.ToVector2() / 2f;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _gameManagement.Update(gameTime);
            base.Update(gameTime);

            if (_loadScene) UpdateScene();
        }

        protected override void Draw(GameTime gameTime)
        {
            _gameManagement.Draw(_spriteBatch, gameTime);

            base.Draw(gameTime);
        }
    }
}
