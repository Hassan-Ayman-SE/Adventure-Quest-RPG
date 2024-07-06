using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{
    public static class BattleSystem
    {
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
                Attack(player, enemy);

                if (enemy.Health <= 0)
                {
                    Console.WriteLine("Victory! The monster has been defeated.");
                    player.GainExperience(enemy.ExperienceReward);
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
    }
}
