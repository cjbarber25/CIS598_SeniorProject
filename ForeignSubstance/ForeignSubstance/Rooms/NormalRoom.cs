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
    public class NormalRoom : Room
    {
        private Sprite[,] _sprites;
        private List<Sprite> _otherSprites;

        public override void Build(int length, int width, Vector2 position,Player player)
        {
            _room = new Basic();
            _sprites = new Sprite[0, 5];
            _otherSprites = new List<Sprite>();
            _room.Build(length, width, position,player);
            Random r = new Random(DateTime.Now.GetHashCode());
            //this.AddObstacles(r.Next(5,10),1);
            this.AddObstacles(1, 1);

        }

        public void AddObstacles(int NumberOfObstacles, int NumberOfEnemies)
        {
            _room.AddObstacles(NumberOfObstacles, NumberOfEnemies);
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
                        if (_sprites[i, j].CheckCollision(playerBounds))
                        {
                            flag = true;
                        }
                    }
                }
            }
            foreach (Sprite s in _otherSprites)
            {
                if (s.CheckCollision(playerBounds))
                {
                    flag = true;
                }
            }
            return flag;
        }
        public override bool CheckDoorCollision(Player player, out Tuple<int, int> _destination)
        {

            return _room.CheckDoorCollision(player, out _destination);
        }

        public override void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            _room.Draw(gametime, spriteBatch);
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
            foreach (Sprite s in _otherSprites)
            {
                s.Draw(gametime, spriteBatch);
            }
            
        }

        public override void LoadContent(ContentManager content)
        {
            _room.LoadContent(content);

            for (int i = 0; i < _sprites.GetLength(0); i++)
            {
                for (int j = 0; j < _sprites.GetLength(1); j++)
                {
                    if (_sprites[i, j] != null)
                    {
                        _sprites[i, j].LoadContent(content);
                    }
                }
            }
            foreach (Sprite s in _otherSprites)
            {
                s.LoadContent(content);
            }

        }
        public override void AddEnemy(Player player, Vector2 position)
        {
            _room.AddEnemy(player,position);
        }
        public override void AddDoors(int[,] layout, Tuple<int, int> currentPosition)
        {
            _room.AddDoors(layout, currentPosition);
        }


        public override void Update(GameTime gametime)
        {
            _room.Update(gametime);
        }
    }
}
