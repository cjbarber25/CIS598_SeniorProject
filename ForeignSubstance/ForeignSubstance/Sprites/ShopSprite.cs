using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Items;

namespace ForeignSubstance.Sprites
{
    public class ShopSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private BoundingRectangle _bounds;
        private Rectangle _textureMapPosition;
        private Player _player;
        private Tuple<ArmSprite.GunTypes, int> _item;
        

        public ShopSprite(Vector2 position,Player player)
        {
            this._position = position;
            _player = player;
            _bounds = new BoundingRectangle(_position.X-16,_position.Y-25, 60, 99);
            _item = new Tuple<ArmSprite.GunTypes, int>(ArmSprite.GunTypes.Shotgun, 100);
        }

        public void BuyItem()
        {
            
            if (_player._money > 400 && _player.Arm.currentGun != ArmSprite.GunTypes.Auto)
            {
                _item = new Tuple<ArmSprite.GunTypes, int>(ArmSprite.GunTypes.Auto, 400);
                _player.BuyItem(_item);
            }
            else
            {
                _player.BuyItem(_item);
            }
            
        }
        
        public override bool CheckCollision(BoundingRectangle other)
        {
            if (_bounds.CollidesWith(other))
            {
                this.BuyItem();
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            _textureMapPosition = new Rectangle(101, 681, 20, 33);
            spriteBatch.Draw(_texture, _position, _textureMapPosition, Color.White, 0.0f, new Vector2(0, 0), 3.0f, SpriteEffects.None, 0);
        }

        public override void LoadContent(ContentManager content)
        {
            this._texture = content.Load<Texture2D>("19_Hospital_16x16");
        }

        public override void Update(GameTime gametime)
        {
            throw new NotImplementedException();
        }
    }
}
