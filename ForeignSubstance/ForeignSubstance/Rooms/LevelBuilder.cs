using ForeignSubstance.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Rooms
{
   public enum ID : int
    {
        EMPTY = 0,
        BASIC = 1,
        CAFETERIA = 2,
        HALLWAYUP = 3,
        HALLWAYSIDE = 4,
        STORE = 5,
        BOSS = 6

    }
    public class LevelBuilder
    {
        private Room[,] _level;
        private int _levelHeight = 5;
        private int _levelWidth = 5;
        private Tuple<int,int> _activeRoom;
        private int[,] _layout;
        private bool currentDoorCollisionState = false;
        private Player _player;

        public LevelBuilder(int[,] layout, Player player)
        {
            _level = new Room[_levelHeight, _levelWidth];
            _layout = layout;
            _player = player;
            _activeRoom = new Tuple<int, int>(3,0 );
            for(int i  = 0; i < _levelHeight; i++)
            {
                for(int j = 0; j < _levelWidth; j++)
                {
                    switch (layout[i, j])
                    {
                        case 0:
                            _level[i, j] = null;
                            break;
                        case 1:
                            _level[i, j] = new NormalRoom();
                            break;
                        case 2:
                            _level[i, j] = new Cafeteria();
                            break;
                        case 3:
                            _level[i, j] = new Hallway(true);
                            break;
                        case 4:
                            _level[i, j] = new Hallway(false);
                            break;
                        case 5:
                            _level[i, j] = new Store();
                            break;
                        case 6:
                            _level[i, j] = new BossRoom();
                            break;
                    }
                }
            }

        }


        public void Update(GameTime gametime)
        {
            _level[_activeRoom.Item1, _activeRoom.Item2].Update(gametime);

        }

        public bool CheckCollision(BoundingRectangle b)
        {
            return _level[_activeRoom.Item1, _activeRoom.Item2].CheckForOutOfBounds(b);
        }

        public void LoadContent(ContentManager content)
        {
            DisplayMode screen = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            Random rand = new Random(DateTime.Now.Ticks.GetHashCode());
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if(_level[i,j] != null)
                    {
                        int height = rand.Next(8, 12);
                        int width = rand.Next(8, 16);
                        _level[i, j].Build(height, width, new Vector2((screen.Width/2)-(height*64/2), (screen.Height/2)-(width*64/2)),_player);
                        _level[i, j].AddDoors(_layout, new Tuple<int,int>(i,j));
                        _level[i, j].LoadContent(content);
                    }
                }
            }
        }


        public void CheckDoorCollision(Player player)
        {
            Tuple<int, int> destination;
            Vector2 newPosition = Vector2.Zero;
            _player = player;
            if (_level[_activeRoom.Item1, _activeRoom.Item2].CheckDoorCollision(player, out destination))
            {
                if (!currentDoorCollisionState)
                {
                    currentDoorCollisionState = true;
                    if (_activeRoom.Item1 > destination.Item1)//going Up
                    {
                        foreach (DoorSprite d in (_level[destination.Item1, destination.Item2]._room.GetDoors()))
                        {
                            if (d.GetBounds().Y > newPosition.Y || newPosition == Vector2.Zero)
                            {
                                newPosition = new Vector2(d.GetBounds().X+32, d.GetBounds().Y-64);
                            }
                        }
                    }
                    else if (_activeRoom.Item1 < destination.Item1) //going down
                    {
                        foreach (DoorSprite d in (_level[destination.Item1, destination.Item2]._room.GetDoors()))
                        {
                            if (d.GetBounds().Y < newPosition.Y || newPosition == Vector2.Zero)
                            {
                                newPosition = new Vector2(d.GetBounds().X+32, d.GetBounds().Y+64);
                            }
                        }
                    }
                    else if (_activeRoom.Item2 > destination.Item2) //going left
                    {
                        foreach (DoorSprite d in (_level[destination.Item1, destination.Item2]._room.GetDoors()))
                        {
                            if (d.GetBounds().X > newPosition.X || newPosition == Vector2.Zero)
                            {
                                newPosition = new Vector2(d.GetBounds().X, d.GetBounds().Y);
                            }
                        }
                    }
                    else if (_activeRoom.Item2 < destination.Item2) //going right
                    {
                        foreach (DoorSprite d in (_level[destination.Item1, destination.Item2]._room.GetDoors()))
                        {
                            if (d.GetBounds().X < newPosition.X || newPosition == Vector2.Zero)
                            {
                                newPosition = new Vector2(d.GetBounds().X+64, d.GetBounds().Y);
                            }
                        }
                    }
                    player.Position = newPosition;
                    _activeRoom = destination;
                }
            }
            else
            {
                currentDoorCollisionState = false;
            }

        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            _level[_activeRoom.Item1,_activeRoom.Item2].Draw(gametime, spriteBatch);
        }
    }
}
