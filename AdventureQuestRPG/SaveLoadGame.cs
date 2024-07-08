using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
namespace AdventureQuestRPG
{


    public static class SaveLoadGame
    {
        private static string saveFilePath = "savegame.json";

        public static void SaveGame(Player player)
        {
            PlayerData playerData = new PlayerData(player);
            string json = JsonSerializer.Serialize(playerData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(saveFilePath, json);
            Console.WriteLine("Game saved.");
        }

        public static Player LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                string json = File.ReadAllText(saveFilePath);
                PlayerData playerData = JsonSerializer.Deserialize<PlayerData>(json);
                Player player = playerData.ToPlayer();
                Console.WriteLine("Game loaded.");
                return player;
            }
            else
            {
                Console.WriteLine("Save file not found.");
                return null;
            }
        }
    }

    public class PlayerData
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int ExperienceToLevelUp { get; set; }
        public Location Location { get; set; }
        public List<ItemData> Inventory { get; set; }

        public PlayerData() { }

        public PlayerData(Player player)
        {
            Name = player.Name;
            Health = player.Health;
            AttackPower = player.AttackPower;
            Defense = player.Defense;
            Experience = player.Experience;
            Level = player.Level;
            ExperienceToLevelUp = player.ExperienceToLevelUp;
            Location = player.Location;
            Inventory = player.Inventory.GetItems().ConvertAll(item => new ItemData(item));
        }

        public Player ToPlayer()
        {
            Player player = new Player(Name, Health, AttackPower, Defense, Location)
            {
                Experience = Experience,
                Level = Level,
                ExperienceToLevelUp = ExperienceToLevelUp
            };
            foreach (var itemData in Inventory)
            {
                player.Inventory.AddItem(itemData.ToItem());
            }
            return player;
        }
    }

    public class ItemData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ItemType { get; set; }
        public int Power { get; set; }

        public ItemData() { }

        public ItemData(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            if (item is Weapon weapon)
            {
                ItemType = "Weapon";
                Power = weapon.AttackPower;
            }
            else if (item is Armor armor)
            {
                ItemType = "Armor";
                Power = armor.Defense;
            }
            else if (item is Potion potion)
            {
                ItemType = "Potion";
                Power = potion.HealAmount;
            }
        }

        public Item ToItem()
        {
            return ItemType switch
            {
                "Weapon" => new Weapon(Name, Description, Power),
                "Armor" => new Armor(Name, Description, Power),
                "Potion" => new Potion(Name, Description, Power),
                _ => throw new Exception("Unknown item type")
            };
        }
    }

}
