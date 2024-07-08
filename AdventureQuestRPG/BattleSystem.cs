using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{
    public static class BattleSystem
    {
        private static Random random = new Random();
        public static void Attack(IBattleStates attacker, IBattleStates target)
        {
            // Calculate the damage inflicted by the goblin on the player
            int damage = Math.Max(0, attacker.AttackPower - target.Defense);
            target.Health = Math.Max(0, target.Health - damage);
            // Display information about the attack
            Console.WriteLine($"{attacker.Name} attacks {target.Name} for {damage} damage. {target.Name} has {target.Health} health remaining.");
        }

        public static void StartBattle(Player player, Monster enemy)
        {
            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine("Player's turn:");
                Console.WriteLine("Choose an action:\n1- Attack\n2- Use Skill\n3- Use Item");
                string action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "1":
                        Attack(player, enemy);
                        break;
                    case "2":
                        UseSkill(player, enemy);
                        break;
                    case "3":
                        UseItem(player);
                        break;
                    default:
                        Console.WriteLine("Invalid action. Try again.");
                        continue;
                }

                if (enemy.Health <= 0)
                {
                    Console.WriteLine("Victory! The monster has been defeated.");
                    player.GainExperience(enemy.ExperienceReward);
                    HandleItemDrop(player);
                    break;
                }

                Console.WriteLine("Enemy's turn:");
                Attack(enemy, player);

                if (player.Health <= 0)
                {
                    Console.WriteLine("Defeat! The player has been defeated.");
                    break;
                }
            }
        }

        private static void UseSkill(Player player, Monster enemy)
        {
            if (player.Skills == null || player.Skills.Count == 0)
            {
                Console.WriteLine("You have no skills to use.");
                return;
            }

            Console.WriteLine("Available skills:");
            for (int i = 0; i < player.Skills.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Skills[i].Name} - {player.Skills[i].Description}");
            }

            Console.WriteLine("Choose a skill:");
            string input = Console.ReadLine();
            int skillIndex;
            if (int.TryParse(input, out skillIndex) && skillIndex >= 1 && skillIndex <= player.Skills.Count)
            {
                Skill selectedSkill = player.Skills[skillIndex - 1];
                selectedSkill.Use(player, enemy);
            }
            else
            {
                Console.WriteLine("Invalid skill choice.");
            }
        }

        private static void UseItem(Player player)
        {
            player.Inventory.DisplayInventory();
            Console.WriteLine("Choose an item to use:");
            string itemName = Console.ReadLine();
            player.UseItem(itemName);
        }

        private static void HandleItemDrop(Player player)
        {
            int dropChance = random.Next(0, 100);
            // 30% chance to drop an item
            if (dropChance < 30)
            {
                Item droppedItem = GenerateRandomItem();
                player.Inventory.AddItem(droppedItem);
            }
        }

        private static Item GenerateRandomItem()
        {
            int itemType = random.Next(0, 3);

            switch (itemType)
            {
                case 0:
                    return new Weapon("Sword", "A sharp blade.", 10);
                case 1:
                    return new Armor("Shield", "A sturdy shield.", 5);
                case 2:
                    return new Potion("Health Potion", "Restores 20 health.", 20);
                default:
                    return new Potion("Health Potion", "Restores 20 health.", 20);
            }
        }
    }
}
