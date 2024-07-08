using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{

    public class Player : IBattleStates
    {
        //props
        public string Name { get; set; }
        public int Health { get; set; } = 100;
        public int AttackPower { get; set; } = 30;
        public int Defense { get; set; } = 10;
        public int Experience { get; set; }
        public int Level { get; set; }
        public int ExperienceToLevelUp { get; set; }

        public Location Location { get; set; }

        public Inventory? Inventory { get; set; }

        public List<Skill>? Skills { get; set; }
        public Player(string name, int health = 100, int attackPower = 30, int defense = 10, Location location = Location.Forest)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
            Experience = 0;
            Level = 1;
            ExperienceToLevelUp = 100;
            Location = location;
            Inventory = new Inventory();
            Skills = new List<Skill>
            {
                new SpecialAttackSkill ("Fireball", "A powerful fire attack.", 10),
                new HealingSkill("Heal", "Restores 20 health points.", 20)
            };
        }
        public void GainExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} gained {amount} XP!");
            if (Experience >= ExperienceToLevelUp)
            {
                LevelUp();
            }
        }
        private void LevelUp()
        {
            Level++;
            Experience -= ExperienceToLevelUp;
            ExperienceToLevelUp = (int)(ExperienceToLevelUp * 1.5);
            Health += 20;
            AttackPower += 5;
            Defense += 5;
            Console.WriteLine($"{Name} leveled up to level {Level}!");
            Console.WriteLine($"New stats - Health: {Health}, AttackPower: {AttackPower}, Defense: {Defense}");

            LearnNewSkill();
        }

        private void LearnNewSkill()
        {
            if (Level == 2)
            {
                Skills.Add(new HealingSkill("Heal", "Restores 20 health.", 20));
                Console.WriteLine($"{Name} learned a new skill: Heal!");
            }
            else if (Level == 3)
            {
                Skills.Add(new SpecialAttackSkill("Fireball", "Deals 40 damage.", 40));
                Console.WriteLine($"{Name} learned a new skill: Fireball!");
            }
        }

        public void UseItem(string itemName)
        {
            Item item = Inventory.GetItem(itemName);
            if (item == null)
            {
                Console.WriteLine("Item not found in inventory.");
                return;
            }

            if (item is Potion potion)
            {
                // Assuming max health is 100
                Health = Math.Min(100, Health + potion.HealAmount); 
                Console.WriteLine($"{Name} used {item.Name} and healed {potion.HealAmount} health.");
            }
            else if (item is Weapon weapon)
            {
                AttackPower += weapon.AttackPower;
                Console.WriteLine($"{Name} equipped {item.Name} and increased attack power by {weapon.AttackPower}.");
            }
            else if (item is Armor armor)
            {
                Defense += armor.Defense;
                Console.WriteLine($"{Name} equipped {item.Name} and increased defense by {armor.Defense}.");
            }

            Inventory.RemoveItem(item);
        }

        public void UseSkill(string skillName, Monster enemy)
        {
            Skill skill = Skills.FirstOrDefault(s => s.Name.Equals(skillName, StringComparison.OrdinalIgnoreCase));
            if (skill == null)
            {
                Console.WriteLine("Skill not found.");
                return;
            }

            skill.Use(this, enemy);
        }
    }

    public abstract class Monster : IBattleStates
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int ExperienceReward { get; set; }
        public Monster(string name, int health, int attackPower, int defense, int experienceReward)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
            ExperienceReward = experienceReward;
        }
        public abstract void Attack(Player player);
    }

    public class Zombie : Monster
    {
        public Zombie(string name = "Zombie", int health = 70, int attackPower = 10, int defense = 6, int experienceReward = 4)
            : base(name, health, attackPower, defense, experienceReward) { }

        public override void Attack(Player player)
        {
            BattleSystem.Attack(this, player);
        }
    }

    public class Goblin : Monster
    {
        public Goblin(string name = "Goblin", int health = 80, int attackPower = 10, int defense = 5, int experienceReward = 5)
            : base(name, health, attackPower, defense, experienceReward)
        {
        }
        public override void Attack(Player player)
        {
            BattleSystem.Attack(this, player);
        }

        public class Skullton : Monster
        {
            public Skullton(string name = "Skullton", int health = 90, int attackPower = 15, int defense = 10, int experienceReward = 10)
                : base(name, health, attackPower, defense, experienceReward) { }

            public override void Attack(Player player)
            {
                BattleSystem.Attack(this, player);
            }
        }


        public class Dragon : Monster
        {
            public Dragon(string name = "Dragon", int health = 200, int attackPower = 30, int defense = 20, int experienceReward = 15)
                : base(name, health, attackPower, defense, experienceReward) { }

            public override void Attack(Player player)
            {
                BattleSystem.Attack(this, player);
            }
        }

        public class BossMonster : Monster
        {
            public BossMonster(string name = "Boss Monster", int health = 300, int attackPower = 40, int defense = 25, int experienceReward = 20)
                : base(name, health, attackPower, defense, experienceReward) { }

            public override void Attack(Player player)
            {
                BattleSystem.Attack(this, player);
            }
        }
    }
}
