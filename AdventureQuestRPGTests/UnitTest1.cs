using AdventureQuestRPG;

namespace AdventureQuestRPGTests
{
    public class UnitTest1
    {
        [Fact]
        public void PlayerAttackReducesEnemyHealth()
        {
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 60, 15, 5, 50);

            BattleSystem.Attack(player, monster);

            Assert.True(monster.Health < 50);
        }

        [Fact]
        public void EnemyAttackReducesPlayerHealth()
        {
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 50, 15, 5, 50);

            BattleSystem.Attack(monster, player);

            Assert.True(player.Health < 100);
        }

        [Fact]
        public void WinnerHealthIsGreaterThanZero()
        {
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 50, 15, 5, 50);

            BattleSystem.StartBattle(player, monster);

            Assert.True(player.Health > 0 || monster.Health > 0);
        }

        [Fact]
        public void PlayerGainsExperienceAfterDefeatingMonster()
        {
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 50, 15, 5, 50);

            BattleSystem.StartBattle(player, monster);

            Assert.True(player.Experience >= 50);
        }

        [Fact]
        public void PlayerLevelsUpAfterGainingEnoughExperience()
        {
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 50, 15, 5, 150);

            BattleSystem.StartBattle(player, monster);

            Assert.True(player.Level > 1);
        }
    }
}