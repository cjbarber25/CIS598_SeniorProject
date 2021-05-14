using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ForeignSubstance.StateManagement;
using System.Collections.Generic;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Screens
{
    // The background screen sits behind all the other menu screens.
    // It draws a background image that remains fixed in place regardless
    // of whatever transitions the screens on top of it may be doing.
    public class BackgroundScreen : GameScreen
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;
        private Texture2D _planet;
        private List<Comet> _comets;
        private List<Star> _stars;
        private double timer;
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, whereas if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            _comets = new List<Comet>();
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            for(int i = 0; i < 6; i++)
            {
                _comets.Add(new Comet(viewport));
            }
            foreach(var comet in _comets)
            {
                comet.LoadContent(_content);
            }

            _stars = new List<Star>();
            for (int i = 0; i < 12; i++)
            {
                _stars.Add(new Star(viewport));
            }
            foreach (var star in _stars)
            {
                star.LoadContent(_content);
            }

            _backgroundTexture = _content.Load<Texture2D>("SpaceBackgrounds/Backgrounds/Blue1");
            _planet = _content.Load<Texture2D>("Planets/Terran");
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // Unlike most screens, this should not transition off even if
        // it has been covered by another screen: it is supposed to be
        // covered, after all! This overload forces the coveredByOtherScreen
        // parameter to false in order to stop the base Update method wanting to transition off.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if(timer > 1)
            {
                foreach(var comet in _comets)
                {
                    if(comet.done)
                    {
                        comet.done = false;
                        break;
                    }
                }
                timer -= 1;
            }
            foreach (var comet in _comets)
            {
                comet.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexture, fullscreen,
                new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.Draw(_planet, new Vector2(200,500), new Rectangle(0,0,_planet.Width, _planet.Height), Color.White, 0.0f, new Vector2(0, 0), 5f, SpriteEffects.None, 0);
            foreach(var comet in _comets)
            {
                comet.Draw(gameTime,spriteBatch);
            }
            foreach (var star in _stars)
            {
                star.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
        }
    }
}
