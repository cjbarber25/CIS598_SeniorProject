using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using ForeignSubstance.StateManagement;
namespace ForeignSubstance.Screens
{
    public class CutSceneScreen : GameScreen
    {
        ContentManager _content;
        InputAction _skip;

        public CutSceneScreen()
        {
            _skip = new InputAction(new Buttons[] { Buttons.A }, new Keys[] { Keys.Space, Keys.Enter }, true);
        }
        public override void Activate()
        {
            if(_content == null)
            {
                _content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            
        }

        public override void HandleInput(GameTime gameTime, InputState input)
        {
            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            
        }

        public override void Deactivate()
        {
            
        }

        public override void Draw(GameTime gameTime)
        {
            
        }
    }
}
