using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{
    public class Inventory
    {
        private List<Item> items = new List<Item> {

            new Weapon("Sword", "A sturdy blade.", 15),
            new Armor("Plate Armor", "Heavy protective gear.", 20),
            new Potion("Healing Potion", "Restores health.", 30)
        };

        public Inventory()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            items.Add(item);
            Console.WriteLine($"You have received: {item.Name}");
        }

        public void DisplayInventory()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
                return;
            }

            Console.WriteLine("Inventory:");
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Name}: {item.Description}");
            }
        }

        public Item GetItem(string itemName)
        {
            return items.FirstOrDefault(item => item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        }

        public List<Item> GetItems()
        {
            return new List<Item>(items);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
    }
}
