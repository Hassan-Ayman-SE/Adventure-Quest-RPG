namespace AdventureQuestRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************** Adventure Quest RPG ******************");
            Player player = new Player("Hero", 100, 20, 10);
            Monster monster = new Goblin("Goblin", 60, 15, 5, 50);

            BattleSystem.StartBattle(player, monster);

            Console.WriteLine("Adventure complete!");
        }
    }
}
