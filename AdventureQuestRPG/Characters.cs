using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{

    public class Player
    {
        //props
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }
        public int ExperienceToLevelUp { get; set; }
        public Player(string name, int health, int attackPower, int defense)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
            Defense = defense;
            Experience = 0;
            Level = 1;
            ExperienceToLevelUp = 100;
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
        }
    }

    public abstract class Monster
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

    public class Goblin : Monster
    {
        public Goblin(string name, int health, int attackPower, int defense, int experienceReward)
            : base(name, health, attackPower, defense, experienceReward)
        {
        }
        public override void Attack(Player player)
        {
            // Calculate the damage inflicted by the goblin on the player
            int damage = Math.Max(0, this.AttackPower - player.Defense);
            player.Health = Math.Max(0, player.Health - damage);
            // Display information about the attack
            Console.WriteLine($"{this.Name} attacks {player.Name} for {damage} damage. {player.Name} has {player.Health} health remaining.");
        }
    }
}
