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
        private float distance;


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
        }

        public void Damaged(int damage)
        {
            _healthRemaining -= damage;
            animationFrame = 0;
            _textureActive = _textureDamaged;
            animationFrameNum = 3;
            color = Color.Red;
            damagedSound.Play();
            if (_healthMax <= 0)
            {
                animationFrameNum = 8;
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

            if (animationTimer > 0.3f)
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
            if (dying && animationFrame >= 9)
            {
                dead = true;
                killedSound.Play();
            }
            if (!dead) spriteBatch.Draw(_textureActive, _position, _sourceRect, color, 0.0f, new Vector2(37.5f, 30), 3f, _spriteEffects, 0);
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
            _textureActive = _textureIdle;
        }

        public override void Update(GameTime gametime)
        {
            stateChangePrior = stateChangeCurrent;
            timer += gametime.ElapsedGameTime.TotalSeconds;
            _playerPosition = _player.Position;
            _direction = _playerPosition - _position;
            _direction.Normalize();
            color = Color.White;
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
                    distance = (float)(Math.Pow(this.Position.X - _player.Position.X, 2) + Math.Pow(this.Position.Y - _player.Position.Y, 2));
                    if (_direction.X < 0)
                    {
                        flipped = true;
                    }
                    else
                    {
                        flipped = false;
                    }



                    _position += _direction * 40 * (float)gametime.ElapsedGameTime.TotalSeconds;



                    if (distance < 40)
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
                    animationFrameNum = 19;
                    if (timer > 5.7f)
                    {
                        State = States.walking;
                        stateChangeCurrent = true;
                        timer -= 5.7f;
                    }
                    if (stateChangeCurrent && !stateChangePrior)
                    {
                        animationFrame = 0;
                    }
                    break;
            }
            stateChangeCurrent = false;
        }
    }
}
