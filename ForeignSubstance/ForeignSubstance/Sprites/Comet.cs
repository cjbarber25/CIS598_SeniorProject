using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class Comet : Sprite
    {
        private List<Texture2D> _comet;
        private double animationTimer;
        private int animationFrame;
        public bool done = false;
        private Vector2 _position;
        private Viewport _viewport;

        public override bool CheckCollision(BoundingRectangle other)
        {
            throw new NotImplementedException();
        }

        public Comet(Viewport viewport)
        {
            _viewport = viewport;
            System.Random rand = new System.Random();
            _position = new Vector2((float)rand.NextDouble() * viewport.Width, (float)rand.NextDouble() * viewport.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3)
            {
                if (animationFrame <= 7) animationFrame++;
                if (animationFrame > 7)
                {
                    done = true;
                    animationFrame = 0;
                }
                animationTimer -= 0.3;
            }
            if(!done) spriteBatch.Draw(_comet[animationFrame], _position, new Rectangle(0, 0, _comet[animationFrame].Width, _comet[animationFrame].Height), Color.White, 0.0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            _comet = new List<Texture2D>();
            for (int i = 1; i < 9; i++)
            {
                _comet.Add(content.Load<Texture2D>("SpaceBackgrounds/Comet/Yellow/Comet" + i));
            }
        }

        public override void Update(GameTime gametime)
        {
            if(done)
            {
                System.Random rand = new System.Random();
                _position = new Vector2((float)rand.NextDouble() * _viewport.Width, (float)rand.NextDouble() * _viewport.Height);
            }
        }
    }
}
