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
        private Vector2 muzzlePosition;
        private List<Bullet> bullets;
        private ContentManager _content;
        public bool Flipped => flip;
        public Vector2 Direction => _direction;
        public Vector2 Position => _position;
        public Vector2 MuzzlePosition => muzzlePosition;
        public ArmSprite(Player player)
        {
            _player = player;
            _position = player.Position + new Vector2(8,31);
            muzzlePosition = _position + new Vector2(22, 0);
            pivot = new Vector2(0,2.5f);
            bullets = new List<Bullet>();
        }
        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Scifi Character/arm_cannon");
            _content = content;
        }
        public override void Update(GameTime gametime)
        {
            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            _direction = new Vector2(currentMouseState.X - _position.X, currentMouseState.Y - _position.Y);
            _position = _player.Position + new Vector2(8, 31);
            angle = (float)Math.Atan2(_direction.Y, _direction.X);
            

            if(bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    bullet.Update(gametime);
                }
            }
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
                _position = _player.Position + new Vector2(-8, 31);
                _direction = new Vector2(currentMouseState.X - _position.X, currentMouseState.Y - _position.Y);
            }

            _direction.Normalize();
            muzzlePosition = _position + _direction * (this._texture.Width * 2.5f);

            if (currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released)
            {
                var bullet = new Bullet(_player);
                bullets.Add(bullet);
                bullet.LoadContent(_content);
            }
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flip) ? SpriteEffects.FlipVertically : SpriteEffects.None;

            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].IsRemoved)
                {
                    bullets[i] = null;
                    bullets.RemoveAt(i);
                }
            }
            if (bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    bullet.Draw(gameTime,spriteBatch);
                }
            }
            spriteBatch.Draw(_texture, _position, new Rectangle(0,0,9,5), Color.White, angle, pivot, 2.5f, spriteEffects, 0);
        }

    }
}
