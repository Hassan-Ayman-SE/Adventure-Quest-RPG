using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{
    public abstract class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }

        protected Skill(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public abstract void Use(Player player, Monster enemy);
    }

    public class HealingSkill : Skill
    {
        public int HealAmount { get; set; }

        public HealingSkill(string name, string description, int healAmount)
            : base(name, description)
        {
            HealAmount = healAmount;
        }

        public override void Use(Player player, Monster enemy)
        {
            player.Health = Math.Min(100, player.Health + HealAmount); // Assuming max health is 100
            Console.WriteLine($"{player.Name} used {Name} and healed {HealAmount} health.");
        }
    }

    public class SpecialAttackSkill : Skill
    {
        public int Damage { get; set; }

        public SpecialAttackSkill(string name, string description, int damage)
            : base(name, description)
        {
            Damage = damage;
        }

        public override void Use(Player player, Monster enemy)
        {
            enemy.Health = Math.Max(0, enemy.Health - Damage);
            Console.WriteLine($"{player.Name} used {Name} and dealt {Damage} damage to {enemy.Name}.");
        }
    }
}
