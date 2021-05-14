using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ForeignSubstance.StateManagement;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using ForeignSubstance.Sprites;
using ForeignSubstance.Rooms;
using ForeignSubstance.Collisions;

namespace ForeignSubstance.Screens
{
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteBatch _spriteBatch;
        private Player _player;
        private MechaSprite _mecha;

        private int[,] _layout = new int[,] { { 5, 1, 1 } };
        private LevelBuilder _level;

        private float _pauseAlpha;
        private readonly InputAction _pauseAction;



        public GameplayScreen(Player player)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
            _player = player;
            _player.gameScreen = this;
            _mecha = new MechaSprite(new Vector2(300,300),_player);
            _mecha.gameScreen = this;

            _level = new LevelBuilder(_layout);

            _pauseAction = new InputAction(
                new[] { Buttons.Start, Buttons.Back },
                new[] { Keys.Back, Keys.Escape }, true);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            _spriteBatch = new SpriteBatch(ScreenManager.GraphicsDevice);


            _level.LoadContent(_content);
            _player.LoadContent(_content);
            _mecha.LoadContent(_content);
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            
            base.Update(gameTime, otherScreenHasFocus, false);
            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                _player.Update(gameTime);
                _mecha.Update(gameTime);
            }

        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];
            var gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected && input.GamePadWasConnected[playerIndex];

            PlayerIndex player;
            if (_pauseAction.Occurred(input, ControllingPlayer, out player) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
                // Otherwise move the player position.
                var movement = Vector2.Zero;

                if (keyboardState.IsKeyDown(Keys.Left))
                    movement.X--;

                if (keyboardState.IsKeyDown(Keys.Right))
                    movement.X++;

                if (keyboardState.IsKeyDown(Keys.Up))
                    movement.Y--;

                if (keyboardState.IsKeyDown(Keys.Down))
                    movement.Y++;

                var thumbstick = gamePadState.ThumbSticks.Left;

                movement.X += thumbstick.X;
                movement.Y -= thumbstick.Y;

                if (movement.Length() > 1)
                    movement.Normalize();
            }
        }

        public bool CheckCollision(BoundingRectangle b)
        {
            return _level.CheckCollision(b);
        }

        public override void Draw(GameTime gameTime)
        {

            ScreenManager.GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
            float _gameScale;
            Vector2 _gameOffset;
            if(ScreenManager.GraphicsDevice.Viewport.AspectRatio < ScreenManager.Game.GraphicsDevice.Viewport.AspectRatio)
            {
                // letterbox vertically
                // Scale game to screen width
                _gameScale = (float)ScreenManager.GraphicsDevice.Viewport.Width / ScreenManager.Game.GraphicsDevice.Viewport.Width;
                // translate vertically
                _gameOffset.Y = (ScreenManager.GraphicsDevice.Viewport.Height - ScreenManager.Game.GraphicsDevice.Viewport.Height * _gameScale) / 2f;
                _gameOffset.X = 0;
            }
            else
            {
                // letterbox horizontally
                // Scale game to screen height 
                _gameScale = (float)ScreenManager.GraphicsDevice.Viewport.Height / ScreenManager.Game.GraphicsDevice.Viewport.Height;
                // translate horizontally
                _gameOffset.X = (ScreenManager.GraphicsDevice.Viewport.Width - ScreenManager.Game.GraphicsDevice.Viewport.Width * _gameScale) / 2f;
                _gameOffset.Y = 0;
            }
            // Determine the necessary transform to scale and position game on-screen
            Matrix transform =
                Matrix.CreateScale(_gameScale) * // Scale the game to screen size 
                Matrix.CreateTranslation(_gameOffset.X, _gameOffset.Y, 0); // Translate game to letterbox position

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transform);
            _level.Draw(gameTime, _spriteBatch);
            _player.Draw(gameTime, _spriteBatch);
            _mecha.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

        }
    }
}
