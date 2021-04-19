using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class FloorSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;

        public FloorSprite(Vector2 position)
        {
            _position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("scifi-tileset");
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, new Rectangle(32, 32, 32, 32), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
        }
    }
}
