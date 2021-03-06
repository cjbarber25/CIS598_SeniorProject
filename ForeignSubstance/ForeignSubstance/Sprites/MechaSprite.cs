using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;
using ForeignSubstance.Screens;
using ForeignSubstance.Rooms;
using Microsoft.Xna.Framework.Audio;

namespace ForeignSubstance.Sprites
{
    public class MechaSprite : Enemy
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
        public bool dead = false;
        private BoundingRectangle _bounds;
        private bool damagedCurrent = false;
        private bool damagedPrior = false;

        private SoundEffect shootSound;
        private SoundEffect damagedSound;
        private SoundEffect killedSound;
        public Vector2 MuzzlePosition => _muzzlePosition;
        public Vector2 Direction => _direction;
        enum States
        {
            idle,
            attacking,
            walking
        }
        public MechaSprite(Vector2 spawnLocation, Player player, Room room)
        {
            _spriteEffects = SpriteEffects.None;
            _player = player;
            State = States.idle;
            _position = spawnLocation;
            _muzzlePosition = _position + new Vector2(75,0);
            bullets = new List<Bullet>();
            _damageValue = 1;
            _healthMax = 6;
            _healthRemaining = 6;
            _roomPosition = new Tuple<int, int>(1, 1);
            _room = room;
            _bounds = new BoundingRectangle(_position.X-150, _position.Y+120, 75, 60);
        }

        public override void LoadContent(ContentManager content)
        {
            _content = content;
            _textureIdle = content.Load<Texture2D>("Mecha/idle");
            _textureAttack = content.Load<Texture2D>("Mecha/prep attack");
            _textureWalking = content.Load<Texture2D>("Mecha/walk");
            _textureDamaged = content.Load<Texture2D>("Mecha/damaged and death");
            shootSound = content.Load<SoundEffect>("Sounds/Laser_Shoot");
            damagedSound = content.Load<SoundEffect>("Sounds/EnemyDamaged");
            killedSound = content.Load<SoundEffect>("Sounds/GetMoney");
            _textureActive = _textureIdle;
        }

        public override bool CheckIfAlive()
        {
            if (!dead)
            {
                return true;
            }
            else
            {
                return false;
            }
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
                        animationFrameNum = 3;
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


                        Vector2 newPosition = _position + _direction * 20 * (float)gametime.ElapsedGameTime.TotalSeconds;
                        _bounds.X = newPosition.X;
                        if (!_gameplayScreen.CheckCollision(_bounds))
                        {
                            _position.X = _bounds.X;
                        }
                        else
                        {
                            _bounds.X = _position.X - 150;
                        }
                        _bounds.Y = newPosition.Y;
                        if (!_gameplayScreen.CheckCollision(_bounds))
                        {
                            _position.Y = _bounds.Y;
                        }
                        else
                        {
                            _bounds.Y = _position.Y + 120;
                        }


                        if (flipped) _muzzlePosition = _position + new Vector2(-75, 0);
                        else _muzzlePosition = _position + new Vector2(75, 0);
                        if (timer > 2)
                        {
                            State = States.attacking;
                            firingCounter = 69;
                            stateChangeCurrent = true;
                            timer -= 2;
                        }
                        if (stateChangeCurrent && !stateChangePrior)
                        {
                            animationFrame = 0;
                        }
                        break;
                    case States.attacking:
                        _textureActive = _textureAttack;
                        animationFrameNum = 2;
                        firingPrior = firingCurrent;
                        if (firingCounter > 0) firingCounter--;
                        if (timer >= 0.6f && timer < 0.7f && firingCounter % 2 == 0 && !firingPrior) //last frame of attack = fire
                        {
                            firingCurrent = true;
                            var bullet = new Bullet(this, new Rectangle(4, 220, 16, 16));
                            bullets.Add(bullet);
                            bullet.LoadContent(_content);
                            shootSound.Play();
                        }
                        else firingCurrent = false;
                        if (timer > 0.9f)
                        {
                            State = States.walking;
                            stateChangeCurrent = true;
                            timer -= 0.9f;
                        }
                        if (stateChangeCurrent && !stateChangePrior)
                        {
                            animationFrame = 0;
                        }
                        break;
                }
                if (bullets != null)
                {
                    foreach (var bullet in bullets)
                    {
                        bullet.Update(gametime);
                        if (bullet.CheckCollision(_player.Bounds)) _player.Damaged(_damageValue);
                    }
                }
                stateChangeCurrent = false;
                damagedCurrent = false;
            }
        }

        public void Damaged(int damage)
        {
            damagedCurrent = true;
            if(!damagedPrior)
            {
                _healthRemaining -= damage;
                _textureActive = _textureDamaged;
                animationFrame = 0;
                animationFrameNum = 4;
                color = Color.Red;
                damagedSound.Play();
            }
            if(_healthRemaining <= 0)
            {
                animationFrameNum = 11;
                dying = true;
                
            }
        }

        public override bool CheckCollision(BoundingRectangle other)
        {
            if (_bounds.CollidesWith(other))
            {
                return true;
            }
            else
            {
                return false;
            }
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
            if(dying && !dead)
            {
                if (animationTimer > 0.3f)
                {
                    animationFrame++;
                    animationTimer -= 0.3f;
                    if (animationFrame > 2)
                    {
                        killedSound.Play();
                        dead = true;
                        _player._money += 100;
                        animationFrame = 0;
                    }
                }
            }
            if (flipped) _spriteEffects = SpriteEffects.FlipHorizontally;
            else _spriteEffects = SpriteEffects.None;
            _sourceRect = new Rectangle(0, animationFrame * 60, 75, 60);
            if(!dead) spriteBatch.Draw(_textureActive, _position, _sourceRect, color, 0.0f, new Vector2(37.5f, 30), 2.5f, _spriteEffects, 0);
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
                    bullet.Draw(gameTime, spriteBatch);
                }
            }
        }

    }
}
