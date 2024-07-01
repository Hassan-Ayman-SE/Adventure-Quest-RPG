# Adventure Quest RPG

## Overview
Adventure Quest RPG is a console-based adventure game where players can go on an epic journey, battle monsters, and explore dungeons.

## Features
- Player and Monster characters with attributes like Health, AttackPower, Defense, Experience, and Level.
- Battle system to handle attacks and manage health.
- Console messages to indicate the progress of the battle.
- Leveling system where players gain experience points from defeating monsters and improve their stats.
- Unit tests to validate the game logic.

## Getting Started
1. Clone the repository.
2. Open the solution in your preferred IDE.
3. Build the solution to restore dependencies.
4. Run the project to start the game.

## Classes
### Player
Represents the player-controlled character.
- Properties: Name, Health, AttackPower, Defense, Experience, Level, ExperienceToLevelUp.
- Methods: `GainExperience(int amount)`, `LevelUp()`.

### Monster
Abstract base class for monsters.
- Properties: Name, Health, AttackPower, Defense, ExperienceReward.
- Abstract method: `Attack(Player player)`.

### Goblin
Derived class from Monster.
- Implements the `Attack` method to simulate goblin attacking the player.

### BattleSystem
Handles the battle logic.
- Methods: `Attack(dynamic attacker, dynamic target)`, `StartBattle(Player player, Monster enemy)`.

## Unit Tests
Unit tests are written using XUnit to validate the game logic.
- Tests include checking health reduction after attacks, ensuring the winner's health is greater than zero, verifying experience gain, and checking the leveling up mechanism.

## Error Handling
- Basic error handling is implemented to ensure the game runs smoothly.
- Input validation is included where necessary.

## License
This project is licensed under the MIT License.
