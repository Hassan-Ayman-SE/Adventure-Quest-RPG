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
        public Player(string name, int health = 100, int attackPower = 30, int defense = 10)
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
