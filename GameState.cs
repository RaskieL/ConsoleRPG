using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;


namespace ConsoleRPG
{
    public class GameState
    {
        static public List<string> Gamestates = new List<string> { };
        static public List<Enemy> Available_Enemies = new List<Enemy>();
        static public Player player = new Player();

        static PlayersContext db = new PlayersContext();
        static public void Update_Gamestate(string gamestate)
        {
            switch (gamestate)
            {

                case "Menu":
                    Console.Clear();
                    Menu();
                    break;

                case "Load Menu":
                    Console.Clear();
                    LoadMenu();
                    break;

                case "Exploration":
                    Console.Clear();
                    player.Update_Mend_Wounds_Charges();
                    Explore();
                    break;

                case "Combat":
                    Console.Clear();
                    Combat();
                    break;

                case "Inventory":
                    Console.Clear();
                    ShowPlayer_Inventory();
                    break;

                case "Manage Weapons":
                    Console.Clear();
                    Manage_Weapons();
                    break;

                case "Manage Armors":
                    Console.Clear();
                    Manage_Armors();
                    break;

                case "Manage Items":
                    Console.Clear();
                    Manage_Items();
                    break;

                case "Player Stats":
                    Console.Clear();
                    ShowPlayer_Stats();
                    break;

                case "Level Up":
                    Console.Clear();
                    LevelUp_Screen();
                    break;

                case "Character Creation":
                    Console.Clear();
                    Character_Creation();
                    Weapon_Choice();
                    Armor_Choice();
                    Gamestates.Remove("Character Creation");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "Main Menu":
                    Console.Clear();
                    Main_Menu();
                    break;

                default:
                    Gamestates.RemoveAt(0);
                    Gamestates.Add("Main Menu");
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
            }
        }

        static void LoadMenu()
        {
            Console.Clear();
            Console.WriteLine("Save Files:\n");
            foreach (Player p in db.Players)
            {
                int b = db.Players.ToList().IndexOf(p);
                Console.WriteLine($"[{b}] - {p.Name}");
            }
            Console.WriteLine("\nType the ID of the save file you want to load.");
            Console.WriteLine("Type 'back' to go back to the Main Menu.");
            string? a = Console.ReadLine();
            if (a is not null && a.ToLower() == "back")
            {
                Console.Clear();
                Gamestates.Remove("Load Menu");
                Gamestates.Add("Main Menu");
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    int c = Convert.ToInt32(a);
                }
                catch (System.FormatException)
                {
                    LoadMenu();
                }
            }
            int d = Convert.ToInt32(a);
            try
            {
                player = db.Players.ToList()[d];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                LoadMenu();
            }
            Console.WriteLine($"You loaded {player.Name}.");
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            Gamestates.Remove("Load Menu");
            Gamestates.Add("Menu");
            Update_Gamestate(Gamestates[0]);
        }

        static void Explore()
        {
            Console.WriteLine("You are exploring");
            Random random = new Random();
            int randomvalue = random.Next(1, 101);

            switch (randomvalue)
            {

                case <= 10:
                    Console.WriteLine("\nThere's nothing here, let's move on");
                    player.Current_Room += 1;
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Gamestates.Remove("Exploration");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case <= 20:
                    Console.WriteLine("\nThere's some loot here, looks like you found a weapon");
                    player.Current_Room += 1;
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Gamestates.Remove("Exploration");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case <= 30:
                    Console.WriteLine("\nThere's some loot here, looks like you found an armor");
                    player.Current_Room += 1;
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Gamestates.Remove("Exploration");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case <= 40:
                    Console.WriteLine("\nThere's some loot here, looks like you found an item");
                    player.Current_Room += 1;
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Gamestates.Remove("Exploration");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case <= 100:
                    Console.WriteLine("It looks like there are some enemies here.");
                    player.Current_Room += 1;
                    Gamestates.Remove("Exploration");
                    Gamestates.Add("Combat");
                    Update_Gamestate(Gamestates[0]);
                    break;

                //case <= 60:
                //break;

                //case <= 70:
                //break;

                //case <= 80:
                //break;

                //case <= 90:
                //break;

                //case <= 100:
                //break;

                default:
                    break;
            }
        }

        static void Combat()
        {
            Random rnd = new Random();
            int enemyseed = rnd.Next(0, Available_Enemies.Count);
            Enemy CurrentEnemy = Available_Enemies[enemyseed];
            CurrentEnemy.Scale_Enemy(player);
            Console.WriteLine($"You are in combat ! You encountered an enemy, it's a {CurrentEnemy.Name} !\nPress Enter to continue.");
            Console.ReadLine();
            bool InCombat = true;
            bool? IsPlayerFaster = Combat_CheckSpeed(CurrentEnemy);
            List<object> TurnOrder = new List<object>();
            foreach (object entity in TurnOrder)
            {
                try
                {
                    TurnOrder.Remove(entity);
                }
                catch (System.InvalidOperationException) { }
            }
            if (IsPlayerFaster == true)
            {
                TurnOrder.Add(player);
                TurnOrder.Add(CurrentEnemy);
            }
            else if (IsPlayerFaster == false)
            {
                TurnOrder.Add(CurrentEnemy);
                TurnOrder.Add(player);
            }
            else
            {
                int a = rnd.Next(0, 2);
                switch (a)
                {
                    case 0:
                        TurnOrder.Add(player);
                        TurnOrder.Add(CurrentEnemy);
                        break;
                    case 1:
                        TurnOrder.Add(CurrentEnemy);
                        TurnOrder.Add(player);
                        break;
                    default:
                        TurnOrder.Add(player);
                        TurnOrder.Add(CurrentEnemy);
                        break;
                }
            }
            while (InCombat)
            {
                switch (TurnOrder[0])
                {
                    case Player:
                        Combat_PlayerTurn(CurrentEnemy);
                        if (!player.IsAlive || !CurrentEnemy.IsAlive)
                        {
                            InCombat = false;
                            if (player.IsAlive)
                            {
                                Console.Clear();
                                player.Gain_XP(CurrentEnemy.XP_Reward);
                                player.Earn_Money(CurrentEnemy.Money_Reward);
                                player.Update_Mend_Wounds_Charges();
                                Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name}! You gained {CurrentEnemy.XP_Reward} XP and earned {CurrentEnemy.Money_Reward} gold coins !");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Gamestates.Remove("Combat");
                                Gamestates.Add("Menu");
                                Update_Gamestate(Gamestates[0]);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("You died.\n");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        Combat_EnemyTurn(CurrentEnemy);
                        if (!player.IsAlive || !CurrentEnemy.IsAlive)
                        {
                            InCombat = false;
                            if (player.IsAlive)
                            {
                                Console.Clear();
                                player.Gain_XP(CurrentEnemy.XP_Reward);
                                player.Earn_Money(CurrentEnemy.Money_Reward);
                                Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name}! You gained {CurrentEnemy.XP_Reward} XP and earned {CurrentEnemy.Money_Reward} gold coins !");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Gamestates.Remove("Combat");
                                Gamestates.Add("Menu");
                                Update_Gamestate(Gamestates[0]);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("You died.\n");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        break;

                    case Enemy:
                        Combat_EnemyTurn(CurrentEnemy);
                        if (!player.IsAlive || !CurrentEnemy.IsAlive)
                        {
                            InCombat = false;
                            if (player.IsAlive)
                            {
                                Console.Clear();
                                player.Gain_XP(CurrentEnemy.XP_Reward);
                                player.Earn_Money(CurrentEnemy.Money_Reward);
                                player.Update_Mend_Wounds_Charges();
                                Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name}! You gained {CurrentEnemy.XP_Reward} XP and earned {CurrentEnemy.Money_Reward} gold coins !");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Gamestates.Remove("Combat");
                                Gamestates.Add("Menu");
                                Update_Gamestate(Gamestates[0]);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("You died.\n");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        Combat_PlayerTurn(CurrentEnemy);
                        if (!player.IsAlive || !CurrentEnemy.IsAlive)
                        {
                            InCombat = false;
                            if (player.IsAlive)
                            {
                                Console.Clear();
                                player.Gain_XP(CurrentEnemy.XP_Reward);
                                player.Earn_Money(CurrentEnemy.Money_Reward);
                                player.Update_Mend_Wounds_Charges();
                                Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name}! You gained {CurrentEnemy.XP_Reward} XP and earned {CurrentEnemy.Money_Reward} gold coins !");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Gamestates.Remove("Combat");
                                Gamestates.Add("Menu");
                                Update_Gamestate(Gamestates[0]);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("You died.\n");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Environment.Exit(0);
                            }
                        }
                        break;

                    default:
                        InCombat = false;
                        break;
                }
            }
        }

        public static void Combat_PlayerTurn(Enemy enemy)
        {
            player.IsProtecting = false;
            Console.Clear();
            Console.WriteLine("It's your turn !\n");
            Console.WriteLine("What do you want to do ?");
            Console.WriteLine($"HP: {player.Current_HP}/{player.HP_Max}   Enemy {enemy.Name}'s HP: {enemy.Current_HP}/{enemy.HP_Max}\n");
            Console.WriteLine("[1] - Attack");
            Console.WriteLine("[2] - Protect");
            Console.WriteLine("[3] - Use Item");
            Console.WriteLine("[4] - Flee\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    player.Attack_Enemy(enemy);
                    Console.WriteLine($"You attacked the {enemy.Name}\nPress Enter to continue.");
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("You are protecting yourself for the rest of your turn !");
                    player.Protect();
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Function not available for the moment. Press enter to continue");
                    Console.ReadLine();
                    Combat_PlayerTurn(enemy);
                    break;

                case "4":
                    Console.Clear();
                    if (player.Speed_Stat > enemy.Speed_Stat)
                    {
                        Console.WriteLine("You managed to flee the combat");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                        Gamestates.Remove("Combat");
                        Gamestates.Add("Menu");
                        Update_Gamestate(Gamestates[0]);
                    }
                    else if (player.Speed_Stat < enemy.Speed_Stat)
                    {
                        Console.WriteLine("You didn't manage to flee the combat");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Random rnd = new Random();
                        int a = rnd.Next(0, 2);
                        switch (a)
                        {
                            case 0:
                                Console.WriteLine("You didn't manage to flee the combat");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                break;

                            case 1:
                                Console.WriteLine("You managed to flee the combat");
                                Console.WriteLine("Press enter to continue.");
                                Console.ReadLine();
                                Gamestates.Remove("Combat");
                                Gamestates.Add("Menu");
                                Update_Gamestate(Gamestates[0]);
                                break;
                        }
                    }
                    break;

                default:
                    Console.Clear();
                    Combat_PlayerTurn(enemy);
                    break;
            }
        }

        public static void Combat_EnemyTurn(Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine("It's the enemy's turn !");
            enemy.Attack_Player(player);
            Console.WriteLine("They attacked you !\n");
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        static bool? Combat_CheckSpeed(Enemy currentenemy)
        {
            if (player.Speed_Stat > currentenemy.Speed_Stat)
            {
                return true;
            }
            else if (player.Speed_Stat < currentenemy.Speed_Stat)
            {
                return false;
            }
            else
            {
                return null;
            }
        }

        static public void Main_Menu()
        {
            Console.WriteLine("Welcome to the main menu of the demo !");
            Console.WriteLine("Choose what you want to do :");
            Console.WriteLine("[1] - New Game");
            Console.WriteLine("[2] - Load Game");
            Console.WriteLine("[3] - Quit Game");

            switch (Console.ReadLine())
            {

                case "1":
                    Gamestates.Remove("Main Menu");
                    Gamestates.Add("Character Creation");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "2":
                    Gamestates.Remove("Main Menu");
                    Gamestates.Add("Load Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("Thank you for playing !");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Main_Menu();
                    break;
            }
        }

        static public void Character_Creation()
        {
            player.Player_Reset();
            Console.WriteLine("Enter your character's name:");
            string? player_name = Console.ReadLine();
            if (player_name == "")
            {
                player.Update_Name("Unknown");
            }
            else if (player_name != null)
            {
                player.Update_Name(player_name);
            }
            else
            {
                player.Update_Name("Unknown");
            }
            Console.Clear();
        }

        static public void Weapon_Choice()
        {
            Console.WriteLine("Choose your starting weapon:");
            Console.WriteLine("[1] - Sword (30 damage)");
            Console.WriteLine("[2] - Super Bow (15 damage, 15 magical damage)");
            Console.WriteLine("[3] - Magic Staff (30 magical damage)");

            switch (Console.ReadLine())
            {
                case "1":
                    Weapon Sword = new Weapon("Sword", 30, 0);
                    player.Equip_Weapon(Sword);
                    Console.Clear();
                    break;

                case "2":
                    Weapon Bow = new Weapon("Bow", 15, 15);
                    player.Equip_Weapon(Bow);
                    Console.Clear();
                    break;

                case "3":
                    Weapon MagicStaff = new Weapon("Magic Staff", 0, 30);
                    player.Equip_Weapon(MagicStaff);
                    Console.Clear();
                    break;

                default:
                    Console.Clear();
                    Weapon_Choice();
                    break;
            }
        }

        static public void Armor_Choice()
        {
            Console.WriteLine("Choose your starting armor:");
            Console.WriteLine("[1] - Leather Armor (3 Armor Points, 0.5 Magical Resistance)");
            Console.WriteLine("[2] - Magical Lizard Leather Armor (2 Armor Points, 2 Magical Resistance)");
            Console.WriteLine("[3] - Mage Robe (0.5 Armor Points, 3 Magical Resistance)");

            switch (Console.ReadLine())
            {
                case "1":
                    Armor LeatherArmor = new Armor("Leather Armor", 3, 0.5);
                    player.Equip_Armor(LeatherArmor);
                    Console.Clear();
                    break;

                case "2":
                    Armor MagicalLizardLeatherArmor = new Armor("Magical Lizard Leather Armor", 2, 2);
                    player.Equip_Armor(MagicalLizardLeatherArmor);
                    Console.Clear();
                    break;

                case "3":
                    Armor MageRobe = new Armor("Mage Robe", 0.5, 3);
                    player.Equip_Armor(MageRobe);
                    Console.Clear();
                    break;

                default:
                    Console.Clear();
                    Armor_Choice();
                    break;
            }
        }

        static public void Menu()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("Please choose what you will do next.");
            Console.WriteLine("[1] - Explore");
            Console.WriteLine("[2] - Check Inventory");
            Console.WriteLine("[3] - Check Player stats");
            Console.WriteLine($"[4] - Mend Wounds ({player.Mend_Wounds_Charges} charges left)");
            Console.WriteLine("[5] - Save Game");
            Console.WriteLine("[6] - Return to Main Menu");
            Console.WriteLine("[7] - Quit Game");

            switch (Console.ReadLine())
            {

                case "1":
                    Gamestates.Remove("Menu");
                    Gamestates.Add("Exploration");
                    Console.Clear();

                    Update_Gamestate(Gamestates[0]);
                    break;

                case "2":
                    Gamestates.Remove("Menu");
                    Gamestates.Add("Inventory");

                    Update_Gamestate(Gamestates[0]);
                    break;

                case "3":
                    Gamestates.Remove("Menu");
                    Gamestates.Add("Player Stats");

                    Update_Gamestate(Gamestates[0]);
                    break;

                case "4":
                    Console.Clear();
                    Player_Heal();
                    break;

                case "5":
                    if (db.Players.ToList().Contains(player))
                    {
                        Console.Clear();
                        Console.WriteLine("Saving..");
                        
                        if(player.Name != db.Players.ToList()[player.Id - 1].Name){
                            player.Id += 1;
                            db.Players.Add(player);
                        }else{
                            db.Players.Update(player);
                        }
                        db.SaveChanges();
                        Console.WriteLine("Saved !");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                        Update_Gamestate(Gamestates[0]);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Saving..");
                        db.Players.Add(player);
                        db.SaveChanges();
                        Console.WriteLine("Saved !");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "6":
                    Console.Clear();
                    Gamestates.Remove("Menu");
                    Gamestates.Add("Main Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "7":
                    Console.Clear();
                    Console.WriteLine("Thank you for playing !");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;

                default:
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;

            }
        }

        static void Player_Heal()
        {
            if (player.Current_HP != player.HP_Max)
            {
                if (player.Mend_Wounds_Charges > 0)
                {
                    Console.WriteLine($"You had {player.Current_HP} HP / {player.HP_Max} HP.");
                    player.Mend_Wounds();
                    Console.WriteLine($"You now have {player.Current_HP} HP / {player.HP_Max} HP\n");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.WriteLine("You have no charges left. It will recharge at the end of the next combat");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
            else
            {
                Console.WriteLine("You are already at maximum HP.");
                Console.WriteLine("Press enter to continue..");
                Console.ReadLine();
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
        }

        static void ShowPlayer_Stats()
        {
            Console.WriteLine("Here is a summary of your stats:");
            Console.WriteLine($"\nPlayer: {player.Name}");

            Console.WriteLine($"Level:{player.Level}");
            Console.WriteLine($"XP: {player.XP}/{player.Required_XP}");
            Console.WriteLine($"Upgrade Points available:{player.Upgrade_Points}");
            Console.WriteLine($"HP:{player.Current_HP}/{player.HP_Max}");
            Console.WriteLine($"Money: {player.Money} Gold Coins");

            Console.WriteLine("\nStats:");
            Console.WriteLine($"Vitality : {player.Vitality_Stat}");
            Console.WriteLine($"Strength : {player.Strength_Stat}");
            Console.WriteLine($"Intelligence : {player.Intelligence_Stat}");
            Console.WriteLine($"Speed : {player.Speed_Stat}");
            Console.WriteLine($"\nArmor : {player.Armor_Stat}");

            Console.WriteLine($"Magic Resistance : {player.Magical_Resistance_Stat}");
            Console.WriteLine($"Physical Attack : {player.Physical_Attack_Stat}");
            Console.WriteLine($"Magical Attack : {player.Magical_Attack_Stat}");

            Console.WriteLine("\n[1] - Level up");
            Console.WriteLine("[2] - Back");

            switch (Console.ReadLine())
            {
                case "1":
                    Gamestates.Remove("Player Stats");
                    Gamestates.Add("Level Up");
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "2":
                    Gamestates.Remove("Player Stats");
                    Gamestates.Add("Menu");
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;

                default:
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
            }
        }

        static void ShowPlayer_Inventory()
        {
            Console.WriteLine("You go through your inventory.");
            if (player.CheckifArmed())
            {
                Console.WriteLine($"Your equipped weapon: {player.Worn_Weapons[0].Name} ({player.Worn_Weapons[0].Attack_Damage} physical damage / {player.Worn_Weapons[0].Magical_Damage} magical damage)");
            }
            else
            {
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if (player.CheckifArmored())
            {
                Console.WriteLine($"Your equipped armor: {player.Worn_Armor[0].Name} ({player.Worn_Armor[0].Armor_Points} armor / {player.Worn_Armor[0].Magical_Resistance} magical resistance)\n");
            }
            else
            {
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            Console.WriteLine($"Money: {player.Money} Gold Coins\n");
            foreach (Item item in player.Inventory)
            {
                Console.WriteLine("- " + item.Name);
            }
            Console.WriteLine("\n[1] - Mangage Weapons");
            Console.WriteLine("[2] - Manage Armors");
            Console.WriteLine("[3] - Manage Items");
            Console.WriteLine("[4] - Back");
            switch (Console.ReadLine())
            {
                case "1":
                    Gamestates.Remove("Inventory");
                    Gamestates.Add("Manage Weapons");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "2":
                    Gamestates.Remove("Inventory");
                    Gamestates.Add("Manage Armors");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "3":
                    Gamestates.Remove("Inventory");
                    Gamestates.Add("Manage Items");
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "4":
                    Gamestates.Remove("Inventory");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                    break;
            }
        }

        static void Manage_Weapons()
        {
            if (player.CheckifArmed())
            {
                Console.WriteLine($"Your equipped weapon: {player.Worn_Weapons[0].Name} ({player.Worn_Weapons[0].Attack_Damage} physical damage / {player.Worn_Weapons[0].Magical_Damage} magical damage)");
            }
            else
            {
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if (player.CheckifArmored())
            {
                Console.WriteLine($"Your equipped armor: {player.Worn_Armor[0].Name} ({player.Worn_Armor[0].Armor_Points} armor / {player.Worn_Armor[0].Magical_Resistance} magical resistance)\n");
            }
            else
            {
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            Console.WriteLine($"Money: {player.Money} Gold Coins\n");
            foreach (Item weapon in player.Inventory)
            {
                if (weapon is Weapon)
                {
                    Console.WriteLine($"- {weapon.Name}");
                }
            }
            Console.WriteLine("\n[1] - Equip weapon");
            Console.WriteLine("[2] - Unequip weapon");
            Console.WriteLine("[3] - Discard weapon");
            Console.WriteLine("[4] - Back\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Equip_Weapon();
                    break;

                case "2":
                    if (player.Worn_Weapons.Count != 0)
                    {
                        Console.Clear();
                        player.Unequip_Weapon(player.Worn_Weapons[0]);
                        Update_Gamestate(Gamestates[0]);
                    }
                    else
                    {
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "3":
                    if (player.Worn_Weapons.Count != 0)
                    {
                        Console.Clear();
                        Discard_Weapon();
                    }
                    else
                    {
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "4":
                    Console.Clear();
                    Gamestates.Remove("Manage Weapons");
                    Gamestates.Add("Inventory");
                    Update_Gamestate(Gamestates[0]);
                    break;

                default:
                    Console.Clear();
                    Manage_Weapons();
                    break;
            }
        }

        static void Manage_Armors()
        {
            if (player.CheckifArmed())
            {
                Console.WriteLine($"Your equipped weapon: {player.Worn_Weapons[0].Name} ({player.Worn_Weapons[0].Attack_Damage} physical damage / {player.Worn_Weapons[0].Magical_Damage} magical damage)");
            }
            else
            {
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if (player.CheckifArmored())
            {
                Console.WriteLine($"Your equipped armor: {player.Worn_Armor[0].Name} ({player.Worn_Armor[0].Armor_Points} armor / {player.Worn_Armor[0].Magical_Resistance} magical resistance)\n");
            }
            else
            {
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            Console.WriteLine($"Money: {player.Money} Gold Coins\n");
            foreach (Item armor in player.Inventory)
            {
                if (armor is Armor)
                {
                    Console.WriteLine($"- {armor.Name}");
                }
            }
            Console.WriteLine("\n[1] - Equip armor");
            Console.WriteLine("[2] - Unequip armor");
            Console.WriteLine("[3] - Discard armor\n");
            Console.WriteLine("[4] - Back");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Equip_Armor();
                    break;

                case "2":
                    if (player.Worn_Armor.Count == 1)
                    {
                        Console.Clear();
                        player.Unequip_Armor(player.Worn_Armor[0]);
                        Update_Gamestate(Gamestates[0]);
                    }
                    else
                    {
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "3":
                    if (player.Worn_Armor.Count == 1)
                    {
                        Console.Clear();
                        Discard_Armor();
                        Update_Gamestate(Gamestates[0]);
                    }
                    else
                    {
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "4":
                    Console.Clear();
                    Gamestates.Remove("Manage Armors");
                    Gamestates.Add("Inventory");
                    Update_Gamestate(Gamestates[0]);
                    break;

                default:
                    Console.Clear();
                    Manage_Armors();
                    break;
            }
        }

        static void Manage_Items()
        {
            if (player.CheckifArmed())
            {
                Console.WriteLine($"Your equipped weapon: {player.Worn_Weapons[0].Name} ({player.Worn_Weapons[0].Attack_Damage} physical damage / {player.Worn_Weapons[0].Magical_Damage} magical damage)");
            }
            else
            {
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if (player.CheckifArmored())
            {
                Console.WriteLine($"Your equipped armor: {player.Worn_Armor[0].Name} ({player.Worn_Armor[0].Armor_Points} armor / {player.Worn_Armor[0].Magical_Resistance} magical resistance)\n");
            }
            else
            {
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            Console.WriteLine($"Money: {player.Money} Gold Coins\n");
            foreach (Item item in player.Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
            Console.WriteLine("\n[1] - Use item (not available yet)");
            Console.WriteLine("[2] - Discard item");
            Console.WriteLine("[3] - Back\n");
            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;

                case "2":
                    if (player.Inventory.Count != 0)
                    {
                        Discard_Item();
                        Update_Gamestate(Gamestates[0]);
                    }
                    else
                    {
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                    }
                    break;

                case "3":
                    Console.Clear();
                    Gamestates.Remove("Manage Items");
                    Gamestates.Add("Inventory");
                    Update_Gamestate(Gamestates[0]);
                    break;

                default:
                    Console.Clear();
                    Manage_Items();
                    break;
            }
        }

        static void Discard_Item()
        {
            Console.Clear();
            Console.WriteLine("Your inventory: ");
            Console.WriteLine($"Money: {player.Money} Gold Coins\n");
            int a = 0;
            foreach (Item item in player.Inventory)
            {
                Console.WriteLine($"[{a++}] - {item.Name}");
            }
            Console.WriteLine("\nType the number of the item you want to discard : ");
            Console.WriteLine("Type 'back' to cancel");
            string? b = Console.ReadLine();
            if (b?.ToLower() == "back")
            {
                Console.Clear();
                Manage_Items();
            }
            else
            {
                try
                {
                    Convert.ToInt32(b);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Discard_Item();
                }
                try
                {
                    player.Discard_Item(player.Inventory[Convert.ToInt32(b)]);
                }
                catch (System.ArgumentOutOfRangeException)
                {
                    Console.Clear();
                    Discard_Item();
                }
            }
        }

        static void Discard_Armor()
        {
            Console.Clear();
            Console.WriteLine("Are you sure ? You will discard the armor you are currently wearing.");
            Console.WriteLine("\n[1] - Yes");
            Console.WriteLine("[2] - No");
            switch (Console.ReadLine())
            {
                case "1":
                    player.Discard_Armor(player.Worn_Armor[0]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
                case "2":
                    Console.Clear();
                    Manage_Armors();
                    break;
                default:
                    Console.Clear();
                    Discard_Armor();
                    break;
            }
        }
        static void Discard_Weapon()
        {
            Console.Clear();
            Console.WriteLine("Are you sure ? You will discard the weapon you are currently wielding.");
            Console.WriteLine("\n[1] - Yes");
            Console.WriteLine("[2] - No");
            switch (Console.ReadLine())
            {
                case "1":
                    player.Discard_Weapon(player.Worn_Weapons[0]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
                case "2":
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
                default:
                    Console.Clear();
                    Discard_Weapon();
                    break;
            }
        }

        static void Equip_Weapon()
        {
            Console.Clear();
            Console.WriteLine("Your Inventory: ");
            List<int> AvailableIndexes = new List<int>();
            foreach (Item weapon in player.Inventory)
            {
                if (weapon is Weapon)
                {
                    int b = player.Inventory.IndexOf(weapon);
                    Console.WriteLine($"[{b}] - {weapon.Name}");
                    AvailableIndexes.Add(b);
                }
            }
            Console.WriteLine("Type the ID of the weapon you want to equip.");
            Console.WriteLine("Type 'back' to cancel");
            var a = Console.ReadLine();
            if (a == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(a);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type a number\n");
                    Equip_Weapon();
                }
                int b = Convert.ToInt32(a);
                if (AvailableIndexes.Contains(b))
                {
                    player.Equip_Weapon(player.Inventory[b] as Weapon);
                    player.Discard_Item(player.Inventory[b]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Equip_Weapon();
                }
            }
        }

        static void Equip_Armor()
        {
            Console.Clear();
            Console.WriteLine("Your Inventory: ");
            List<int> AvailableIndexes = new List<int>();
            foreach (Item armor in player.Inventory)
            {
                if (armor is Armor)
                {
                    int b = player.Inventory.IndexOf(armor);
                    Console.WriteLine("[{b}] - {armor.Name}");
                    AvailableIndexes.Add(b);
                }
            }
            Console.WriteLine("Type the ID of the armor you want to equip.");
            Console.WriteLine("Type 'back' to cancel");
            var a = Console.ReadLine();
            if (a == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(a);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type a number\n");
                    Equip_Weapon();
                }
                if (AvailableIndexes.Contains(Convert.ToInt32(a)))
                {
                    player.Equip_Armor(player.Inventory[Convert.ToInt32(a)]! as Armor);
                    player.Discard_Item(player.Inventory[Convert.ToInt32(a)]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Equip_Armor();
                }
            }
        }

        static void LevelUp_Screen()
        {
            Console.WriteLine($"\nPlayer: {player.Name}");

            Console.WriteLine($"Level: {player.Level}");
            Console.WriteLine($"XP: {player.XP} / {player.Required_XP}");
            Console.WriteLine($"Upgrade Points available: {player.Upgrade_Points}");
            Console.WriteLine($"HP: {player.Current_HP} / {player.HP_Max}");

            Console.WriteLine($"\nArmor : {player.Armor_Stat}");
            Console.WriteLine($"Magic Resistance : {player.Magical_Resistance_Stat}");
            Console.WriteLine($"Physical Attack : {player.Physical_Attack_Stat}");
            Console.WriteLine($"Magical Attack : {player.Magical_Attack_Stat}");

            Console.WriteLine("\nStats: Choose which one to upgrade");
            Console.WriteLine($"[1] - Vitality : {player.Vitality_Stat}");
            Console.WriteLine($"[2] - Strength : {player.Strength_Stat}");
            Console.WriteLine($"[3] - Intelligence : {player.Intelligence_Stat}");
            Console.WriteLine($"[4] - Speed : {player.Speed_Stat}\n");
            Console.WriteLine("[5] - Back");

            switch (Console.ReadLine())
            {

                case "1":
                    LevelUp_Vitality();
                    break;

                case "2":
                    LevelUp_Strength();
                    break;

                case "3":
                    LevelUp_Intelligence();
                    break;

                case "4":
                    LevelUp_Speed();
                    break;

                case "5":
                    Gamestates.Remove("Level Up");
                    Gamestates.Add("Player Stats");
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;

                default:
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                    break;
            }
        }

        static void LevelUp_Vitality()
        {
            Console.WriteLine("You want to upgrade Vitality\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine($"Upgrade Points available: {player.Upgrade_Points}");
            Console.WriteLine("Type 'back' to go cancel.\n");
            var points = Console.ReadLine();
            if (points == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(points);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Vitality();
                }
                if (player.Upgrade_Points > 0)
                {
                    player.Upgrade_Vitality(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Strength()
        {
            Console.WriteLine("You want to upgrade Strength\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine($"Upgrade Points available: {player.Upgrade_Points}");
            var points = Console.ReadLine();
            if (points == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(points);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Strength();
                }
                if (player.Upgrade_Points > 0)
                {
                    player.Upgrade_Strength(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Intelligence()
        {
            Console.WriteLine("You want to upgrade Intelligence\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine($"Upgrade Points available: {player.Upgrade_Points}");
            var points = Console.ReadLine();
            if (points == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(points);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Intelligence();
                }
                if (player.Upgrade_Points > 0)
                {
                    player.Upgrade_Intelligence(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Speed()
        {
            Console.WriteLine("You want to upgrade Speed\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine($"Upgrade Points available: {player.Upgrade_Points}");
            var points = Console.ReadLine();
            if (points == "back")
            {
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }
            else
            {
                try
                {
                    Convert.ToInt32(points);
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Speed();
                }
                if (player.Upgrade_Points > 0)
                {
                    player.Upgrade_Speed(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
    }
}
