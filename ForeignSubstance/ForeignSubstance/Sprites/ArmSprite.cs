using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework.Input;
namespace ForeignSubstance.Sprites
{
    public class ArmSprite : Sprite
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Player _player;
        private Vector2 _direction;
        private MouseState currentMouseState;
        private MouseState priorMouseState;
        private Vector2 pivot;
        private float angle;
        private bool flip = false;

        public bool Flipped => flip;
        public ArmSprite(Player player)
        {
            _player = player;
            _position = player.Position + new Vector2(31,29);
            pivot = new Vector2(0,2);
        }
        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Scifi Character/arm_cannon");
        }
        public override void Update(GameTime gametime)
        {
            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            _direction = new Vector2(currentMouseState.X - _position.X, currentMouseState.Y - _position.Y - pivot.Y);
            _position = _player.Position + new Vector2(31, 29);
            angle = (float)Math.Atan2(_direction.Y, _direction.X);
            if (angle >= Math.PI / 2 && angle <= Math.PI)
            {
                flip = true;
            }
            else if(angle >= -Math.PI && angle < -Math.PI/2)
            {
                flip = true;
            }
            else
            {
                flip = false;
            }
            if (flip)
            {
                _position = _player.Position + new Vector2(15, 31);
            }
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flip) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(_texture, _position, new Rectangle(0,0,9,5), Color.White, angle, pivot, 2.5f, spriteEffects, 0);
        }

    }
}
