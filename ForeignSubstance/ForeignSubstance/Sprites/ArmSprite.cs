using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

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
        public Bullet[] bullets;
        private ContentManager _content;
        public int _damageValue;
        private double timer;
        private bool gunlock = false;
        private SoundEffect shootSound;

        public bool Flipped => flip;
        public Vector2 Direction => _direction;
        public Vector2 Position => _position;
        public Vector2 MuzzlePosition => muzzlePosition;

        public GunTypes currentGun;
        public enum GunTypes
        {
            Single,
            Auto,
            Shotgun
        }
        public ArmSprite(Player player)
        {
            _player = player;
            _position = player.Position + new Vector2(8,31);
            muzzlePosition = _position + new Vector2(22, 0);
            pivot = new Vector2(0,2.5f);
            _damageValue = 1;
            currentGun = GunTypes.Single;
        }
        public override void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Scifi Character/arm_cannon");
            _content = content;
            bullets = new Bullet[]
            {
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
                new Bullet(_player,new Rectangle(4, 220, 16, 16)),
            };
            foreach(var bullet in bullets)
            {
                bullet.LoadContent(content);
            }
            shootSound = content.Load<SoundEffect>("Sounds/Laser_Shoot");
        }
        public override void Update(GameTime gametime)
        {
            priorMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            _direction = new Vector2(currentMouseState.X - _position.X, currentMouseState.Y - _position.Y);
            _position = _player.Position + new Vector2(8, 31);
            angle = (float)Math.Atan2(_direction.Y, _direction.X);
            foreach (var bullet in bullets)
            {
               if(!bullet.IsRemoved)
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

            switch (currentGun)
            {
                case GunTypes.Single:
                    _damageValue = 1;
                    timer += gametime.ElapsedGameTime.TotalSeconds;
                    if (currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released && !gunlock)
                    {
                        foreach (var bullet in bullets)
                        {
                            if (bullet.IsRemoved)
                            {
                                bullet.Direction = this.Direction;
                                bullet.IsRemoved = false;
                                bullet.BulletReset(bullet, false);
                                shootSound.Play();
                                gunlock = true;
                                break;
                            }
                        }
                    }
                    if(timer > 1)
                    {
                        gunlock = false;
                        timer -= 1;
                    }
                    break;
                case GunTypes.Auto:
                    _damageValue = 1;
                    timer += gametime.ElapsedGameTime.TotalSeconds;
                    if (currentMouseState.LeftButton == ButtonState.Pressed && !gunlock)
                    {
                        foreach (var bullet in bullets)
                        {
                            if (bullet.IsRemoved)
                            {
                                bullet.Direction = this.Direction;
                                bullet.IsRemoved = false;
                                bullet.BulletReset(bullet, false);
                                shootSound.Play();
                                gunlock = true;
                                break;
                            }
                        }
                    }
                    if (timer > .2f)
                    {
                        gunlock = false;
                        timer -= .2f;
                    }
                    break;
                case GunTypes.Shotgun:
                    _damageValue = 1;
                    int chamberNum = 0;
                    Bullet[] chamber = new Bullet[3];
                    timer += gametime.ElapsedGameTime.TotalSeconds;
                    if (currentMouseState.LeftButton == ButtonState.Pressed && priorMouseState.LeftButton == ButtonState.Released && !gunlock)
                    {
                        foreach (var bullet in bullets)
                        {
                            if (bullet.IsRemoved)
                            {
                                chamber[chamberNum] = bullet;
                                chamberNum++;
                                if(chamberNum == 3)
                                {
                                    chamber[0].Direction = this.Direction + new Vector2(50,50);
                                    chamber[0].IsRemoved = false;
                                    chamber[0].BulletReset(bullet, true);
                                    chamber[1].Direction = this.Direction;
                                    chamber[1].IsRemoved = false;
                                    chamber[1].BulletReset(bullet, false);
                                    chamber[2].Direction = this.Direction + new Vector2(50,-50);
                                    chamber[2].IsRemoved = false;
                                    chamber[2].BulletReset(bullet, true);
                                    shootSound.Play();
                                    gunlock = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (timer > 1.5)
                    {
                        gunlock = false;
                        timer -= 1.5;
                    }
                    break;
            }
        }
        public override bool CheckCollision(BoundingRectangle other)
        {
            return false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects spriteEffects = (flip) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            foreach (var bullet in bullets)
            {
                bullet.Draw(gameTime,spriteBatch);
            }
            spriteBatch.Draw(_texture, _position, new Rectangle(0,0,9,5), Color.White, angle, pivot, 2.5f, spriteEffects, 0);
        }

    }
}
