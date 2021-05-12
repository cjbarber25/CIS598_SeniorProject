﻿using ForeignSubstance.Collisions;
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
        LOOT = 2,
        HALLWAYUP = 3,
        HALLWAYSIDE = 4,
        STORE = 5,
        BOSS = 6

    }
    public class LevelBuilder
    {
        private int[,] _layout;
        private Room[,] _level;
        private int _levelHeight;
        private int _levelWidth;
        private Tuple<int,int> _activeRoom;

        public LevelBuilder(int[,] layout)
        {
            
            _activeRoom = new Tuple<int, int>(0, 0);
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
                            _level[i, j] = new Basic();
                            
                            break;
                        case 2:
                            _level[i, j] = new Loot();
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
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if (_level[i, j] != null)
                    {
                        _level[i, j].Update(gametime);
                    }
                }
            }
        }

        public bool CheckCollision(BoundingRectangle b)
        {
            return _level[_activeRoom.Item1, _activeRoom.Item2].CheckForOutOfBounds(b);
        }

        public void LoadContent(ContentManager content)
        {
            for (int i = 0; i < _levelHeight; i++)
            {
                for (int j = 0; j < _levelWidth; j++)
                {
                    if(_level[i,j] != null)
                    {
                        _level[i, j].LoadContent(content);
                        _level[i, j].Build(5, 5, new Vector2(200, 200));
                    }
                }
            }
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            _level[_activeRoom.Item1,_activeRoom.Item2].Draw(gametime, spriteBatch);
        }
    }
}
