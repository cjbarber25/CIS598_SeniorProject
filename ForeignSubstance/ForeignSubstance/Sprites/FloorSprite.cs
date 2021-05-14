using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Collisions;

namespace ForeignSubstance.Sprites
{
    public class FloorSprite : Sprite
    {
        private Texture2D _texture;
        public BoundingRectangle _bounds;
        public FloorSprite(Vector2 position)
        {
            this.Position = position;
            
        }

        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("scifi-tileset");
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, this.Position, new Rectangle(32, 32, 32, 32), Color.White, 0, new Vector2(0, 0), 2.0f, SpriteEffects.None, 0);
        }
    }
}
