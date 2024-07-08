using AdventureQuestRPG;
using static AdventureQuestRPG.Goblin;

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

        private Adventure SetupAdventureWithPlayer(string playerName)
        {
            var adventure = new Adventure();
            var playerField = typeof(Adventure).GetField("player", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var player = new Player(playerName);
            playerField.SetValue(adventure, player);
            return adventure;
        }

        [Fact]
        public void TestEncounterBossMonster()
        {
            // Arrange
            var adventure = SetupAdventureWithPlayer("TestPlayer");

            // Using reflection to access private methods
            var encounterMonsterMethod = typeof(Adventure).GetMethod("EncounterMonster", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var monstersField = typeof(Adventure).GetField("monsters", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var monsters = (List<Monster>)monstersField.GetValue(adventure);

            // Setting up to ensure the boss monster is included
            monsters.Add(new BossMonster());

            // Act
            bool bossEncountered = false;
            for (int i = 0; i < 100; i++) // Increase the iteration count for higher probability
            {
                encounterMonsterMethod.Invoke(adventure, null);
                var player = (Player)typeof(Adventure).GetField("player", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(adventure);
                if (player.Health > 0) // Player survived the battle
                {
                    var lastMonsterEncountered = (Monster)typeof(Adventure).GetField("lastMonsterEncountered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(adventure);
                    if (lastMonsterEncountered is BossMonster)
                    {
                        bossEncountered = true;
                        break;
                    }
                }
            }

            // Assert
            Assert.True(bossEncountered, "The boss monster was not encountered in 100 encounters.");
        }

        [Fact]
        public void TestMoveToNewLocation()
        {
            // Arrange
            var adventure = SetupAdventureWithPlayer("TestPlayer");
            var availableLocationsField = typeof(Adventure).GetField("availableLocations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var availableLocations = (List<Location>)availableLocationsField.GetValue(adventure);

            var discoveredLocationsField = typeof(Adventure).GetField("discoveredLocations", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var discoveredLocations = (List<Location>)discoveredLocationsField.GetValue(adventure);

            // Act
            var discoverLocationMethod = typeof(Adventure).GetMethod("DiscoverLocation", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            discoverLocationMethod.Invoke(adventure, null);

            // Assert
            Assert.True(discoveredLocations.Count > 0, "No new location was discovered.");
            Assert.True(availableLocations.Count < 3, "The count of available locations did not decrease.");
        }

    }
}