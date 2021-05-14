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
    public class Store : Room
    {
        private Sprite[,] _sprites;
        Basic _room = new Basic();

        public override void Build(int length, int width, Vector2 position)
        {
            _sprites = new Sprite[5, 5];
            _room.Build(length, width, position);
            _sprites[0, 0] = new ShopSprite( new Vector2(position.Y + ((width / 2) * 64), position.Y + ((length / 2) * 64)));
        }

        public override bool CheckForOutOfBounds(BoundingRectangle playerBounds)
        {
            bool flag = false;
            if (_room.CheckForOutOfBounds(playerBounds))
            {
                flag = true;
            }
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j] != null)
                    {
                        if(_sprites[i, j].CheckCollision(playerBounds))
                        {
                            flag = true;
                        }
                    }
                }
            }
            return flag;
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            _room.Draw(gametime,spriteBatch);
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j] != null)
                    {
                        _sprites[i, j].Draw(gametime, spriteBatch);
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            _room.LoadContent(content);
            
            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i,j] != null)
                    {
                        _sprites[i, j].LoadContent(content);
                    }
                }
            }
        }

        public override void Update(GameTime gametime)
        {
            _room.Update(gametime);
        }
    }
}
