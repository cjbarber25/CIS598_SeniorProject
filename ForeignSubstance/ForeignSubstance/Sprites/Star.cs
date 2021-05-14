using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class Star : Sprite
    {
        private List<Texture2D> _stars;
        private double animationTimer;
        private int animationFrame;
        public bool done = false;
        private Vector2 _position;
        private Viewport _viewport;

        public override bool CheckCollision(BoundingRectangle other)
        {
            throw new NotImplementedException();
        }

        public Star(Viewport viewport)
        {
            _viewport = viewport;
            System.Random rand = new System.Random();
            _position = new Vector2((float)rand.NextDouble() * viewport.Width, (float)rand.NextDouble() * viewport.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (animationTimer > 0.3f)
            {
                animationFrame++;
                if (animationFrame > 6)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.3f;
            }
            spriteBatch.Draw(_stars[animationFrame], _position, new Rectangle(0, 0, _stars[animationFrame].Width, _stars[animationFrame].Height), Color.White, 0.0f, new Vector2(0, 0), 3f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            _stars = new List<Texture2D>();
            for (int i = 1; i < 8; i++)
            {
                _stars.Add(content.Load<Texture2D>("SpaceBackgrounds/Star2/Yellow/star" + i));
            }
        }

        public override void Update(GameTime gametime)
        {

        }
    }
}
