using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;

namespace ForeignSubstance.Sprites
{
    public class MechaSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        public MechaSprite()
        {
            _position = new Vector2(350, 100);
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Mecha/prep attack");
        }

        public override void Update(GameTime gametime)
        {

        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, new Rectangle(0,0,135,120), Color.White, 0.0f, new Vector2(0, 0), 2.5f, SpriteEffects.FlipHorizontally, 0);
        }
    }
}
