using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Collisions;

namespace ForeignSubstance.Sprites
{
    public class ArmSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Player _player;
        public ArmSprite(Player player)
        {
            _player = player;
            _position = player.Position + new Vector2(31,25);
        }
        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Scifi Character/arm_cannon");
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }
        public override void Update(GameTime gametime)
        {
            _position = _player.Position + new Vector2(31, 25);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, new Rectangle(0,0,9,5), Color.White, 0.0f, new Vector2(0, 0), 2.5f, SpriteEffects.None, 0);
        }
    }
}
