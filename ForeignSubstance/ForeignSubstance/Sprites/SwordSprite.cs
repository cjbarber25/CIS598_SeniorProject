using ForeignSubstance.Collisions;
using ForeignSubstance.Rooms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForeignSubstance.Sprites
{
    public class SwordSprite : Sprite
    {

        #region textures
        private Texture2D _textureActive;
        private Texture2D _textureIdle;
        private Texture2D _textureAttack;
        private Texture2D _textureWalking;
        private Texture2D _textureDamaged;
        #endregion textures

        private Vector2 _position;
        private Rectangle _sourceRect;
        private BoundingRectangle _bounds;
        private States State;
        private SpriteEffects _spriteEffects;
        private double timer;
        private double animationTimer;
        private short animationFrame = 0;
        private short animationFrameNum;
        private Player _player;
        private Vector2 _playerPosition;
        private bool flipped = false;
        private bool stateChangeCurrent = false;
        private bool stateChangePrior = false;
        private bool firingCurrent = false;
        private bool firingPrior = false;
        private int firingCounter = 69;
        private List<Bullet> bullets;
        private Vector2 _direction;
        private ContentManager _content;
        private Vector2 _muzzlePosition;
        private int _damageValue;
        private int _healthMax;
        private int _healthRemaining;
        private Room _room;
        private Tuple<int, int> _roomPosition;
        private Color color;
        private bool dying = false;
        private bool dead = false;
        private double distance;
        private bool damagedCurrent = false;
        private bool damagedPrior = false;
        private SpriteFont _spriteFont;

        private SoundEffect damagedSound;
        private SoundEffect killedSound;
        public Vector2 Direction => _direction;
        enum States
        {
            idle,
            attacking,
            walking
        }

        public SwordSprite(Vector2 position, Player player)
        {
            _position = position;
            _player = player;
            _bounds = new BoundingRectangle(0, 0, 93, 63);
            _healthRemaining = 50;
            _healthMax = 20;
        }

        public void Damaged(int damage)
        {
            damagedCurrent = true;
            if (!damagedPrior)
            {
                _healthRemaining -= damage;
                _textureActive = _textureDamaged;
                animationFrame = 0;
                animationFrameNum = 4;
                color = Color.Red;
                damagedSound.Play();
            }
            if (_healthRemaining <= 0)
            {
                animationFrameNum = 11;
                dying = true;
            }
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            return _bounds.CollidesWith(other);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animationTimer += gameTime.ElapsedGameTime.TotalSeconds;

            if (animationTimer > 0.3f && !dying)
            {
                animationFrame++;
                if (animationFrame > animationFrameNum)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.3f;
            }
            if (flipped) _spriteEffects = SpriteEffects.FlipHorizontally;
            else _spriteEffects = SpriteEffects.None;
            _sourceRect = new Rectangle(0, animationFrame * 63, 93, 63);
            if (dying && !dead)
            {
                if (animationTimer > 0.3f)
                {
                    animationFrame++;
                    animationTimer -= 0.3f;
                    if (animationFrame > 11)
                    {
                        killedSound.Play();
                        dead = true;
                        animationFrame = 0;
                    }
                }
            }
            if (!dead) spriteBatch.Draw(_textureActive, _position, _sourceRect, color, 0.0f, new Vector2(37.5f, 30), 3f, _spriteEffects, 0);
            else
            {
                spriteBatch.DrawString(_spriteFont, "YOU HAVE WON", new Vector2(600, 600), Color.Gold);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            _content = content;
            _textureIdle = content.Load<Texture2D>("SwordDroid/Red Sword Idle");
            _textureAttack = content.Load<Texture2D>("SwordDroid/Red Sword Attack");
            _textureWalking = content.Load<Texture2D>("SwordDroid/Red Sword Run");
            _textureDamaged = content.Load<Texture2D>("SwordDroid/Red Sword Damaged and Death");
            damagedSound = content.Load<SoundEffect>("Sounds/EnemyDamaged");
            killedSound = content.Load<SoundEffect>("Sounds/GetMoney");
            _spriteFont = content.Load<SpriteFont>("File");
            _textureActive = _textureIdle;
        }

        public override void Update(GameTime gametime)
        {
            if (!dying)
            {
                damagedPrior = damagedCurrent;
                stateChangePrior = stateChangeCurrent;
                timer += gametime.ElapsedGameTime.TotalSeconds;
                _playerPosition = _player.Position;
                _direction = _playerPosition - _position;
                _direction.Normalize();
                color = Color.White;
                foreach (var bullet in _player.Arm.bullets)
                {
                    if (bullet.CheckCollision(this._bounds)) this.Damaged(_player.Arm._damageValue);
                }
                switch (State)
                {
                    case States.idle:
                        _textureActive = _textureIdle;
                        animationFrameNum = 5;
                        if (timer > 1)
                        {
                            State = States.walking;
                            stateChangeCurrent = true;
                            timer -= 1;
                        }
                        if (stateChangeCurrent && !stateChangePrior)
                        {
                            animationFrame = 0;
                        }
                        break;
                    case States.walking:
                        _textureActive = _textureWalking;
                        animationFrameNum = 5;
                        if (_direction.X < 0)
                        {
                            flipped = true;
                        }
                        else
                        {
                            flipped = false;
                        }

                        _position += _direction * 40 * (float)gametime.ElapsedGameTime.TotalSeconds;
                        _bounds.X = _position.X;
                        _bounds.Y = _position.Y;
                        distance = (Math.Pow(this.Position.X - _player.Position.X, 2) + Math.Pow(this.Position.Y - _player.Position.Y, 2));
                        distance = Math.Sqrt(distance);
                        if (distance < 1000)
                        {
                            State = States.attacking;
                            stateChangeCurrent = true;
                        }
                        if (stateChangeCurrent && !stateChangePrior)
                        {
                            animationFrame = 0;
                        }
                        break;
                    case States.attacking:
                        _textureActive = _textureAttack;
                        animationFrameNum = 10;
                        distance = (Math.Pow(this.Position.X - _player.Position.X, 2) + Math.Pow(this.Position.Y - _player.Position.Y, 2));
                        distance = Math.Sqrt(distance);
                        if (distance < 700)
                        {
                            _player.Damaged(2);
                        }
                        if (timer > 1f)
                        {
                            State = States.walking;
                            stateChangeCurrent = true;
                            timer -= 1f;
                        }
                        if (stateChangeCurrent && !stateChangePrior)
                        {
                            animationFrame = 0;
                        }
                        break;
                }
                stateChangeCurrent = false;
                damagedCurrent = false;
            }
        }
    }
}
