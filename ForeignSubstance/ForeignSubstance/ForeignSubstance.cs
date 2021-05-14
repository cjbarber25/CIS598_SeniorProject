using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ForeignSubstance.Screens;
using ForeignSubstance.StateManagement;

namespace ForeignSubstance
{
    public class ForeignSubstance : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly ScreenManager _screenManager;

        public ForeignSubstance()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var screenFactory = new ScreenFactory();
            Services.AddService(typeof(IScreenFactory), screenFactory);

            _screenManager = new ScreenManager(this);
            Components.Add(_screenManager);

            // Use full-screen at screen resolution
            DisplayMode screen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = screen.Width;
            _graphics.PreferredBackBufferHeight = screen.Height;
            Mouse.SetCursor(MouseCursor.Crosshair);
            AddInitialScreens();
        }

        public void BindMouse()
        {
            int mouseX = Mouse.GetState().X;
            int MouseY = Mouse.GetState().Y;
            bool Off = false;
            if (mouseX > this.GraphicsDevice.Viewport.Width)
            {
                mouseX = this.GraphicsDevice.Viewport.Width;
                Off = true;
            }
            else if (mouseX < 0)
            {
               mouseX = 0;
               Off = true;
            }

            if (MouseY > this.GraphicsDevice.Viewport.Height)
            {
                MouseY = this.GraphicsDevice.Viewport.Height;
                Off = true;
            }
            else if (MouseY < 0)
            {
                MouseY = 0;
                Off = true;
            }
            if (Off) Mouse.SetPosition(mouseX, MouseY);
        }

        private void AddInitialScreens()
        {
            _screenManager.AddScreen(new BackgroundScreen(), null);
            _screenManager.AddScreen(new MainMenuScreen(), null);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            /*_graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();*/

            base.Initialize();
        }

        protected override void LoadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BindMouse();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
