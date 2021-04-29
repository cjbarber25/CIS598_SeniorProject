using System;
using System.Collections.Generic;
using System.Text;
using ForeignSubstance.Sprites;

namespace ForeignSubstance.Items
{
    public class Inventory
    {
        private List<Item> _inventory;
        private int _capacity;
        private Player _player;

        public Inventory(int capacity, Player player)
        {
            this._capacity = capacity;
            this._player = player;
            _inventory = new List<Item>(_capacity);
        }

        public bool AddItem(Item item)
        {
            if(_inventory.Count < _capacity)
            {
                item.PickUp(_player);
                _inventory.Add(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DropItem(Item item)
        {
            if (_inventory.Contains(item))
            {
                _inventory.Remove(item);
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Item> ViewInventory()
        {
            return _inventory;
        }
    }
}
