using static AdventureQuestRPG.Goblin;

namespace AdventureQuestRPG
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("****************** Adventure Quest RPG ******************");

            Adventure adventure = new Adventure();
            adventure.Start();
        }
      
    }
}
