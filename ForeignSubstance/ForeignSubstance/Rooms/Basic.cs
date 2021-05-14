using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using ForeignSubstance.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework.Audio;

namespace ForeignSubstance.Rooms
{
    public class Basic : Room
    {
        private Sprite[,] _sprites;
        private List<Enemy> _enemySprites;
        private List<Sprite> _otherSprites;
        private List<DoorSprite> _doors;

        private int _roomLength;
        private int _roomWidth;
        private Vector2 _roomPosition;

        private Player _player;

        private SoundEffect teleportSound;

        public List<DoorSprite> GetDoors()
        {
            return _doors;
        }

        public override void Build(int length, int width,Vector2 position, Player player)
        {
            _doors = new List<DoorSprite>();
            _sprites = new Sprite[length,width];
            
            _roomLength = length;
            _roomWidth = width;
            _roomPosition = position;
            _player = player;
            _enemySprites = new List<Enemy>();
            _otherSprites = new List<Sprite>();
            
            Vector2 tempPosition = position;
            float currentX = position.X;
            float currentY = position.Y;
            int xAdjust = 64;
            int yAdjust = 128;
            
            

            for(int i = 0;i < length; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    if(i == 0)
                    {
                        if (j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX,currentY), ("TOP", "LEFT", "CORNER"));
                            currentX += xAdjust;
                        }
                        else if(j == width - 1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("TOP", "RIGHT", "CORNER"));
                            currentX = position.X;
                            currentY += yAdjust;
                        }
                        else
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("TOP", "CENTER", "WALL"));
                            currentX += xAdjust;
                        }
                    }
                    else if(i == length - 1)
                    {
                        if (j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX,currentY), ("BOTTOM", "LEFT", "CORNER"));
                            currentX += xAdjust;
                        }
                        else if (j == width -1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("BOTTOM", "RIGHT", "CORNER"));
                        }
                        else
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("BOTTOM", "CENTER", "WALL"));
                            currentX += xAdjust;
                        }
                    }
                    else
                    {
                        if(j == 0)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("NORMAL","LEFT", "WALL"));
                            currentX += xAdjust;
                        }
                        else if(j == width - 1)
                        {
                            _sprites[i, j] = new WallSprite(new Vector2(currentX, currentY), ("NORMAL","RIGHT", "WALL"));
                            currentX = position.X;
                            currentY += 64;
                        }
                        else
                        {
                            _sprites[i, j] = new FloorSprite(new Vector2(currentX,currentY));
                            currentX += xAdjust;
                        }
                    }
                }
            }
        }
        public void AddObstacles(int NumberOfObstacles, int NumberOfEnemies)
        {
            List<BoxSprite> _obstacles = new List<BoxSprite>();
            List<MechaSprite> _enemies = new List<MechaSprite>();
            Random r = new Random(DateTime.Now.GetHashCode());
            for(int j = 0; j < NumberOfEnemies; j++)
            {
                MechaSprite newEnemy = new MechaSprite(_sprites[r.Next(2, _roomLength - 2), r.Next(2, _roomWidth - 2)].Position, _player, this);
                if (!newEnemy.CheckCollision(_player.Bounds))
                {
                    _enemies.Add(newEnemy);
                }
                else
                {
                    j--;
                }
            }
            for (int i = 0; i < NumberOfObstacles; i++)
            {
                BoxSprite newBox = new BoxSprite(_sprites[r.Next(2, _roomLength - 2), r.Next(2, _roomWidth - 2)].Position, true);
                if (!newBox.CheckCollision(_player.Bounds)){
                    _obstacles.Add(newBox);
                }
                else
                {
                    i--;
                }
                
            }
            
            foreach (BoxSprite b in _obstacles)
            {
                _otherSprites.Add(b);
            }
            foreach(Enemy e in _enemies)
            {
                e._gameplayScreen = _player.gameScreen;
                _enemySprites.Add(e);
            }
        }

        public override void LoadContent(ContentManager content)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].LoadContent(content);
                }
            }
            foreach(Enemy e in _enemySprites)
            {
                e.LoadContent(content);
            }
            foreach (DoorSprite d in _doors)
            {
                d.LoadContent(content);
            }
            foreach (Sprite s in _otherSprites)
            {
                s.LoadContent(content);
            }
            teleportSound = content.Load<SoundEffect>("Sounds/Teleport");
        }

        public override bool CheckForOutOfBounds(BoundingRectangle playerBounds)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if(_sprites[i, j].CheckCollision(playerBounds))
                    {
                        return true ;
                    }
                }
            }
            foreach(Sprite s in _otherSprites)
            {
                if (s.CheckCollision(playerBounds))
                {
                    return true;
                }
            }
   
            
            return false;
            
        }

        public override void AddDoors(int[,] layout, Tuple<int,int> currentPosition)
        {
            DoorHelper dh = new DoorHelper();
            foreach (DoorSprite d in dh.AddDoors(layout, currentPosition, _roomPosition, _roomLength, _roomWidth, new List<DoorSprite>(),_sprites))
            {
                _doors.Add(d);
            }
        }

        public override void Update(GameTime gametime)
        {
            foreach(Enemy e in _enemySprites)
            {
                e.Update(gametime);
            }
        }
        public override bool CheckDoorCollision(Player player, out Tuple<int,int> _destination)
        {
            _destination = new Tuple<int, int>(1, 1);
            foreach (DoorSprite d in _doors)
            {
                
                if (d.GoThrough(player, out _destination))
                {
                    teleportSound.Play();
                    return true;
                }
               
            }
            return false;
        }

        public override void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for(int j = 0; j < _sprites.GetLength(1); j++)
                {
                    _sprites[i, j].Draw(gameTime, spriteBatch);
                }
            }
            foreach (Sprite s in _enemySprites)
            {
                s.Draw(gameTime, spriteBatch);
            }
            foreach(Sprite s in _otherSprites)
            {
                s.Draw(gameTime, spriteBatch);
            }
            foreach(DoorSprite d in _doors)
            {
                d.Draw(gameTime, spriteBatch);
            }

        }

        public override void AddEnemy(Player player, Vector2 position)
        {
            throw new NotImplementedException();
        }
    }
}
