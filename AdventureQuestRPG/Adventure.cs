using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventureQuestRPG.Goblin;

namespace AdventureQuestRPG
{
    // Enum representing different possible locations in the game.
    public enum Location
    {
        Forest,
        Cave,
        Village,
        Castle
    }

    // Adventure Class.
    public class Adventure
    {
        private Player? player;
        private List<Monster> monsters;
        private Random random;
        private List<Location> availableLocations;
        private List<Location> discoveredLocations;

        // Constructor to initialize the Adventure game.
        public Adventure()
        {
            InitializePlayer();
            monsters = new List<Monster>
        {
            new Goblin(),
            new Zombie(),
            new Skullton(),
            new Dragon(),
            new BossMonster()
        };

            random = new Random();

            availableLocations = new List<Location>
        {
            Location.Cave,
            Location.Village,
            Location.Castle
        };
            discoveredLocations = new List<Location>();
        }

        // Initializes the player by prompting for a name and creating a Player object.
        private void InitializePlayer()
        {
            string playerName = GetPlayerName();
            player = new Player(playerName);
            Console.WriteLine($"Welcome, {player.Name}!");
        }

        // Prompts the player to enter his name and returns it after some validation.
        public static string GetPlayerName()
        {
            Console.WriteLine("Enter your name:");
            string name;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                name = Console.ReadLine();
                bool test_name = Int32.TryParse(name, out int num_name);
                if (!String.IsNullOrEmpty(name) && test_name == false)
                {
                    return name;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Your name must not be empty nor a number!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
            }
        }

        // Starts the main game loop, allowing the player to choose actions.
        public void Start()
        {
            while (true)
            {
                DisplayPlayerInfo(player);

                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"========================================");
                Console.WriteLine("Choose an option:\n 1- Choose a new location \n 2- Attack a monster \n 3- End the game");
                Console.WriteLine($"========================================");
                Console.ForegroundColor = ConsoleColor.Blue;

                string action = Console.ReadLine().ToLower();

                switch (action)
                {
                    case "1":
                        DiscoverLocation();
                        break;
                    case "2":
                        EncounterMonster();
                        break;

                    case "3":
                        Console.WriteLine("Ending the game. Goodbye!");
                        return;
                        


                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid action. Please try again.");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                }
            }
        }

        // Displays the player's current information, such as location.
        private void DisplayPlayerInfo(Player player)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"========================================");
            Console.WriteLine($"You are now in the {player.Location}");
            Console.WriteLine($"========================================");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        // Allows the player to discover a new location from the available locations.
        private void DiscoverLocation()
        {
            if (availableLocations.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"========================================");
                Console.WriteLine("You have discovered all locations.");
                Console.WriteLine($"========================================");
                Console.ForegroundColor = ConsoleColor.Blue;
                return;
            }

            int randomIndex = random.Next(availableLocations.Count);
            Location location = availableLocations[randomIndex];
            availableLocations.RemoveAt(randomIndex);
            discoveredLocations.Add(location);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"========================================");
            Console.WriteLine($"You have discovered a new location: {location}!");
            Console.WriteLine($"========================================");
            Console.ForegroundColor = ConsoleColor.Blue;

            player.Location = location;
        }

        // Initiates an encounter with a randomly selected monster.
        private void EncounterMonster()
        {
            Monster monster = monsters[random.Next(monsters.Count)];

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"========================================");
            Console.WriteLine($"You encounter a {monster.Name}!");
            Console.WriteLine($"========================================");
            Console.ForegroundColor = ConsoleColor.Blue;

            BattleSystem.StartBattle(player, monster);

            if (player.Health <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"========================================");
                Console.WriteLine("Game over! You have been defeated.");
                Console.WriteLine($"========================================");
                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine("Would you like to play again? (y/n)");

                string response = "n";
                while (true)
                {
                    response = Console.ReadLine().ToLower();

                    if (response != "n" && response != "y")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"========================================");
                        Console.WriteLine("Invalid input, try again:");
                        Console.WriteLine($"========================================");
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                        break;
                }

                if (response == "y")
                {
                    Adventure adventure = new Adventure();
                    adventure.Start();
                }

                Environment.Exit(0);
            }
        }
    }

}
