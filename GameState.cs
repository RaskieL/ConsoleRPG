namespace Jeu_Test{

    public class GameState{
        static public List<string> Gamestates = new List<string>{};
        static public List<Enemy> Available_Enemies = new List<Enemy>();

        static public Player Player1 = new Player("Unknown");
        static public void Update_Gamestate(string gamestate){
            switch(gamestate){

                case "Menu":
                Console.Clear();
                Menu();
                break;

                case "Exploration":
                Console.Clear();
                Player1.Update_Mend_Wounds_Charges();
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

        static void Explore(){
            Console.WriteLine("You are exploring");
            Random random = new Random();
            int randomvalue = random.Next(1,101);
            
            switch(randomvalue){

                case <= 10:
                Console.WriteLine("\nThere's nothing here, let's move on");
                Player1.Current_Room += 1;
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Gamestates.Remove("Exploration");
                Gamestates.Add("Menu");
                Update_Gamestate(Gamestates[0]);
                break;

                case <= 20:
                Console.WriteLine("\nThere's some loot here, looks like you found a weapon");
                Player1.Current_Room += 1;
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Gamestates.Remove("Exploration");
                Gamestates.Add("Menu");
                Update_Gamestate(Gamestates[0]);
                break;

                case <= 30:
                Console.WriteLine("\nThere's some loot here, looks like you found an armor");
                Player1.Current_Room += 1;
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Gamestates.Remove("Exploration");
                Gamestates.Add("Menu");
                Update_Gamestate(Gamestates[0]);
                break;

                case <= 40:
                Console.WriteLine("\nThere's some loot here, looks like you found an item");
                Player1.Current_Room += 1;
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                Gamestates.Remove("Exploration");
                Gamestates.Add("Menu");
                Update_Gamestate(Gamestates[0]);
                break;

                case <= 100:
                Console.WriteLine("It looks like there are some enemies here.");
                Player1.Current_Room += 1;
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

        static void Combat(){
            Random rnd = new Random();
            int enemyseed = rnd.Next(0,Available_Enemies.Count);
            Enemy CurrentEnemy = Available_Enemies[enemyseed];
            CurrentEnemy.Reset_Enemies();
            CurrentEnemy.Scale_Enemy(Player1);
            Console.WriteLine($"You are in combat ! You encountered an enemy, it's a {CurrentEnemy.Name} !\nPress Enter to continue.");
            Console.ReadLine();
            bool InCombat = true;
            bool? IsPlayerFaster = Combat_CheckSpeed(CurrentEnemy);
            List<object> TurnOrder = new List<object>();
            foreach (object entity in TurnOrder){
                try {TurnOrder.Remove(entity);
                }catch(System.InvalidOperationException){}
            }
            if(IsPlayerFaster == true){
                TurnOrder.Add(Player1);
                TurnOrder.Add(CurrentEnemy);
            }else if(IsPlayerFaster == false){
                TurnOrder.Add(CurrentEnemy);
                TurnOrder.Add(Player1);
            }else{
                int a = rnd.Next(0,2);
                switch(a){
                    case 0:
                    TurnOrder.Add(Player1);
                    TurnOrder.Add(CurrentEnemy);
                    break;
                    case 1:
                    TurnOrder.Add(CurrentEnemy);
                    TurnOrder.Add(Player1);
                    break;
                    default:
                    TurnOrder.Add(Player1);
                    TurnOrder.Add(CurrentEnemy);
                    break;
                }
            }
            while(InCombat){
                switch(TurnOrder[0]){
                    case Player:
                    Combat_PlayerTurn(CurrentEnemy);
                    if(!Player1.IsAlive || !CurrentEnemy.IsAlive ){
                        InCombat = false;
                        if(Player1.IsAlive){
                            Console.Clear();
                            Player1.Gain_XP(CurrentEnemy.XP_Reward);
                            Player1.Update_Mend_Wounds_Charges();
                            Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name} and gained {CurrentEnemy.XP_Reward} !");
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                            Gamestates.Remove("Combat");
                            Gamestates.Add("Menu");
                            Update_Gamestate(Gamestates[0]);
                        }else{
                            Console.Clear();
                            Console.WriteLine("You died.\n");
                            Console.WriteLine("Press enter to continue.");
                            Environment.Exit(0);
                        }
                    }
                    Combat_EnemyTurn(CurrentEnemy);
                    if(!Player1.IsAlive || !CurrentEnemy.IsAlive ){
                        InCombat = false;
                        if(Player1.IsAlive){
                            Console.Clear();
                            Player1.Gain_XP(CurrentEnemy.XP_Reward);
                            Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name} and gained {CurrentEnemy.XP_Reward} !");
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                            Gamestates.Remove("Combat");
                            Gamestates.Add("Menu");
                            Update_Gamestate(Gamestates[0]);
                        }else{
                            Console.Clear();
                            Console.WriteLine("You died.\n");
                            Console.WriteLine("Press enter to continue.");
                            Environment.Exit(0);
                        }
                    }
                    break;

                    case Enemy:
                    Combat_EnemyTurn(CurrentEnemy);
                    if(!Player1.IsAlive || !CurrentEnemy.IsAlive ){
                        InCombat = false;
                        if(Player1.IsAlive){
                            Console.Clear();
                            Player1.Gain_XP(CurrentEnemy.XP_Reward);
                            Player1.Update_Mend_Wounds_Charges();
                            Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name} and gained {CurrentEnemy.XP_Reward} !");
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                            Gamestates.Remove("Combat");
                            Gamestates.Add("Menu");
                            Update_Gamestate(Gamestates[0]);
                        }else{
                            Console.Clear();
                            Console.WriteLine("You died.\n");
                            Console.WriteLine("Press enter to continue.");
                            Environment.Exit(0);
                        }
                    }
                    Combat_PlayerTurn(CurrentEnemy);
                    if(!Player1.IsAlive || !CurrentEnemy.IsAlive ){
                        InCombat = false;
                        if(Player1.IsAlive){
                            Console.Clear();
                            Player1.Gain_XP(CurrentEnemy.XP_Reward);
                            Player1.Update_Mend_Wounds_Charges();
                            Console.WriteLine($"Awesome ! You vanquished the enemy {CurrentEnemy.Name} and gained {CurrentEnemy.XP_Reward} !");
                            Console.WriteLine("Press enter to continue.");
                            Console.ReadLine();
                            Gamestates.Remove("Combat");
                            Gamestates.Add("Menu");
                            Update_Gamestate(Gamestates[0]);
                        }else{
                            Console.Clear();
                            Console.WriteLine("You died.\n");
                            Console.WriteLine("Press enter to continue.");
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

        public static void Combat_PlayerTurn(Enemy enemy){
            Player1.IsProtecting = false;
            Console.Clear();
            Console.WriteLine("It's your turn !\n");
            Console.WriteLine("What do you want to do ?");
            Console.WriteLine($"HP: {Player1.Current_HP}/{Player1.HP_Max}   Enemy {enemy.Name}'s HP: {enemy.Current_HP}/{enemy.HP_Max}\n");
            Console.WriteLine("[1] - Attack");
            Console.WriteLine("[2] - Protect");
            Console.WriteLine("[3] - Use Item");
            Console.WriteLine("[4] - Flee\n");
            switch (Console.ReadLine()){
                case "1":
                Console.Clear();
                Player1.Attack_Enemy(enemy);
                Console.WriteLine($"You attacked the {enemy.Name}\nPress Enter to continuer.");
                Console.ReadLine();
                break;

                case "2":
                Console.Clear();
                Console.WriteLine("You are protecting yourself for the rest of your turn !");
                Player1.Protect();
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
                if(Player1.Speed_Stat > enemy.Speed_Stat){
                    Console.WriteLine("You managed to flee the combat");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    Gamestates.Remove("Combat");
                    Gamestates.Add("Menu");
                    Update_Gamestate(Gamestates[0]);
                }else if(Player1.Speed_Stat < enemy.Speed_Stat){
                    Console.WriteLine("You didn't manage to flee the combat");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }else{
                    Random rnd = new Random();
                    int a = rnd.Next(0,2);
                    switch(a){
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

        public static void Combat_EnemyTurn(Enemy enemy){
            Console.Clear();
            Console.WriteLine("It's the enemy's turn !");
            enemy.Attack_Player(Player1);
            Console.WriteLine("They attacked you !\n");
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        static bool? Combat_CheckSpeed(Enemy currentenemy){
            if(Player1.Speed_Stat > currentenemy.Speed_Stat){
                return true;
            }else if(Player1.Speed_Stat < currentenemy.Speed_Stat){
                return false;
            }else{
                return null;
            }
        }

        static public void Main_Menu(){
            Console.WriteLine("Welcome to the main menu of the demo !");
            Console.WriteLine("Choose what you want to do :");
            Console.WriteLine("[1] - Play");
            Console.WriteLine("[2] - Quit Game");

            switch(Console.ReadLine()){

                        case "1":
                        Gamestates.Remove("Main Menu");
                        Gamestates.Add("Character Creation");
                        Update_Gamestate(Gamestates[0]);
                        break;

                        case "2":
                        Console.Clear();
                        Console.WriteLine("Thank you for playing !");
                        Environment.Exit(0);
                        break;

                        default:
                        Console.Clear();
                        Main_Menu();
                        break;
                    }
        }

        static public void Character_Creation(){
            Console.WriteLine("Enter your character's name:");
                string? player_name = Console.ReadLine();
                    if(player_name == ""){
                        Player player = new Player("Unknown");
                        Player1 = player;
                    }else if(player_name != null){
                        Player player = new Player(player_name);
                        Player1 = player;
                    }else {
                        Player player = new Player("Unknown");
                        Player1 = player;
                    }
                    Console.Clear();
        }

        static public void Weapon_Choice(){
            Console.WriteLine("Choose your starting weapon:");
            Console.WriteLine("[1] - Sword (30 damage)");
            Console.WriteLine("[2] - Super Bow (15 damage, 15 magical damage)");
            Console.WriteLine("[3] - Magic Staff (30 magical damage)");

            switch(Console.ReadLine()){
                        case "1":
                        Weapon Sword = new Weapon("Sword", 30 , 0);
                        Player1.Equip_Weapon(Sword);
                        Console.Clear();
                        break;

                        case "2":
                        Weapon Bow = new Weapon("Bow", 15, 15);
                        Player1.Equip_Weapon(Bow);
                        Console.Clear();
                        break;

                        case "3":
                        Weapon MagicStaff = new Weapon("Magic Staff", 0, 30);
                        Player1.Equip_Weapon(MagicStaff);
                        Console.Clear();
                        break;

                        default:
                        Console.Clear();
                        Weapon_Choice();
                        break;
                    }
        }

        static public void Armor_Choice(){
            Console.WriteLine("Choose your starting armor:");
            Console.WriteLine("[1] - Leather Armor (3 Armor Points, 0.5 Magical Resistance)");
            Console.WriteLine("[2] - Magical Lizard Leather Armor (2 Armor Points, 2 Magical Resistance)");
            Console.WriteLine("[3] - Mage Robe (0.5 Armor Points, 3 Magical Resistance)");

            switch(Console.ReadLine()){
                        case "1":
                        Armor LeatherArmor = new Armor("Leather Armor", 3 , 0.5);
                        Player1.Equip_Armor(LeatherArmor);
                        Console.Clear();
                        break;

                        case "2":
                        Armor MagicalLizardLeatherArmor = new Armor("Magical Lizard Leather Armor", 2, 2);
                        Player1.Equip_Armor(MagicalLizardLeatherArmor);
                        Console.Clear();
                        break;

                        case "3":
                        Armor MageRobe = new Armor("Mage Robe", 0.5, 3);
                        Player1.Equip_Armor(MageRobe);
                        Console.Clear();
                        break;

                        default:
                        Console.Clear();
                        Armor_Choice();
                        break;
            }
        }

        static public void Menu(){
            Console.WriteLine("Menu:");
            Console.WriteLine("Please choose what you will do next.");
            Console.WriteLine("[1] - Explore");
            Console.WriteLine("[2] - Check Inventory");
            Console.WriteLine("[3] - Check Player stats");
            Console.WriteLine("[4] - Mend Wounds ("+ Player1.Mend_Wounds_Charges +" charges left)");
            Console.WriteLine("[5] - Quit Game");

            switch(Console.ReadLine()){

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
                        Console.Clear();
                        Console.WriteLine("Thank you for playing !");
                        Environment.Exit(0);
                        break;

                        default:
                        Console.Clear();
                        Update_Gamestate(Gamestates[0]);
                        break;

                    }
        }

        static void Player_Heal(){
            if(Player1.Current_HP != Player1.HP_Max){
                if (Player1.Mend_Wounds_Charges > 0){
                    Console.WriteLine("You had "+ Player1.Current_HP +" HP / " + Player1.HP_Max + " HP.");
                    Player1.Mend_Wounds();
                    Console.WriteLine("You now have "+ Player1.Current_HP +" HP / " + Player1.HP_Max + " HP\n");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.WriteLine("You have no charges left. It will recharge at the end of the next combat");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }else{
                Console.WriteLine("You are already at maximum HP.");
                Console.WriteLine("Press enter to continue..");
                Console.ReadLine();
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
                }
        }

        static void ShowPlayer_Stats(){
            Console.WriteLine("Here is a summary of your stats:");
            Console.WriteLine("\nPlayer: "+ Player1.Name );

            Console.WriteLine("Level: "+ Player1.Level);
            Console.WriteLine("XP: "+ Player1.XP+ "/" + Player1.Required_XP);
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            Console.WriteLine("HP: "+ Player1.Current_HP +"/"+ Player1.HP_Max);

            Console.WriteLine("\nStats:");
            Console.WriteLine("Vitality : "+ Player1.Vitality_Stat);
            Console.WriteLine("Strength : "+ Player1.Strength_Stat);
            Console.WriteLine("Intelligence : "+ Player1.Intelligence_Stat);
            Console.WriteLine("Speed : "+ Player1.Speed_Stat);
            Console.WriteLine("\nArmor : "+ Player1.Armor_Stat);

            Console.WriteLine("Magic Resistance : "+ Player1.Magical_Resistance_Stat);
            Console.WriteLine("Physical Attack : "+ Player1.Physical_Attack_Stat);
            Console.WriteLine("Magical Attack : "+ Player1.Magical_Attack_Stat);

            Console.WriteLine("\n[1] - Level up");
            Console.WriteLine("[2] - Back");

            switch(Console.ReadLine())
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

        static void ShowPlayer_Inventory(){
            Console.WriteLine("You go through your inventory.");
            if(Player1.CheckifArmed()){
                Console.WriteLine("Your equipped weapon: "+ Player1.Worn_Weapons[0].Name + " (" + Player1.Worn_Weapons[0].Attack_Damage + " physical damage / " + Player1.Worn_Weapons[0].Magical_Damage + " magical damage)");
            }else{
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if(Player1.CheckifArmored()){
                Console.WriteLine("Your equipped armor: " + Player1.Worn_Armor[0].Name + " (" + Player1.Worn_Armor[0].Armor_Points + " armor / " + Player1.Worn_Armor[0].Magical_Resistance + " magical resistance)\n");
            }else{
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            foreach(Item item in Player1.Inventory){
                Console.WriteLine("- "+ item.Name);
            }
            Console.WriteLine("\n[1] - Mangage Weapons");
            Console.WriteLine("[2] - Manage Armors");
            Console.WriteLine("[3] - Manage Items");
            Console.WriteLine("[4] - Back");
            switch(Console.ReadLine()){
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

        static void Manage_Weapons(){
            if(Player1.CheckifArmed()){
                Console.WriteLine("Your equipped weapon: "+ Player1.Worn_Weapons[0].Name + " (" + Player1.Worn_Weapons[0].Attack_Damage + " physical damage / " + Player1.Worn_Weapons[0].Magical_Damage + " magical damage)");
            }else{
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if(Player1.CheckifArmored()){
                Console.WriteLine("Your equipped armor: " + Player1.Worn_Armor[0].Name + " (" + Player1.Worn_Armor[0].Armor_Points + " armor / " + Player1.Worn_Armor[0].Magical_Resistance + " magical resistance)\n");
            }else{
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            foreach(Item weapon in Player1.Inventory){
                if(weapon is Weapon){
                    Console.WriteLine("- "+ weapon.Name);
                }
            }
            Console.WriteLine("\n[1] - Equip weapon");
            Console.WriteLine("[2] - Unequip weapon");
            Console.WriteLine("[3] - Discard weapon");
            Console.WriteLine("[4] - Back\n");
            switch(Console.ReadLine()){
                case "1":
                Console.Clear();
                Equip_Weapon();
                break;

                case "2":
                if(Player1.Worn_Weapons.Count != 0){
                    Console.Clear();
                    Player1.Unequip_Weapon(Player1.Worn_Weapons[0]);
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                break;

                case "3":
                if(Player1.Worn_Weapons.Count != 0){
                    Console.Clear();
                    Discard_Weapon();
                }else{
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

        static void Manage_Armors(){
            if(Player1.CheckifArmed()){
                Console.WriteLine("Your equipped weapon: "+ Player1.Worn_Weapons[0].Name + " (" + Player1.Worn_Weapons[0].Attack_Damage + " physical damage / " + Player1.Worn_Weapons[0].Magical_Damage + " magical damage)");
            }else{
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if(Player1.CheckifArmored()){
                Console.WriteLine("Your equipped armor: " + Player1.Worn_Armor[0].Name + " (" + Player1.Worn_Armor[0].Armor_Points + " armor / " + Player1.Worn_Armor[0].Magical_Resistance + " magical resistance)\n");
            }else{
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            foreach(Item armor in Player1.Inventory){
                if(armor is Armor){
                    Console.WriteLine("- "+ armor.Name);
                }
            }
            Console.WriteLine("\n[1] - Equip armor");
            Console.WriteLine("[2] - Unequip armor");
            Console.WriteLine("[3] - Discard armor\n");
            Console.WriteLine("[4] - Back");
            switch(Console.ReadLine()){
                case "1":
                Console.Clear();
                Equip_Armor();
                break;

                case "2":
                if(Player1.Worn_Armor.Count == 1 ){
                    Console.Clear();
                    Player1.Unequip_Armor(Player1.Worn_Armor[0]);
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
                break;

                case "3":
                if(Player1.Worn_Armor.Count == 1){
                    Console.Clear();
                    Discard_Armor();
                    Update_Gamestate(Gamestates[0]);
                }else{
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

        static void Manage_Items(){
            if(Player1.CheckifArmed()){
                Console.WriteLine("Your equipped weapon: "+ Player1.Worn_Weapons[0].Name + " (" + Player1.Worn_Weapons[0].Attack_Damage + " physical damage / " + Player1.Worn_Weapons[0].Magical_Damage + " magical damage)");
            }else{
                Console.WriteLine("Your equipped weapon: None\n");
            }
            if(Player1.CheckifArmored()){
                Console.WriteLine("Your equipped armor: " + Player1.Worn_Armor[0].Name + " (" + Player1.Worn_Armor[0].Armor_Points + " armor / " + Player1.Worn_Armor[0].Magical_Resistance + " magical resistance)\n");
            }else{
                Console.WriteLine("Your equipped armor: None\n");
            }
            Console.WriteLine("Your inventory: ");
            foreach(Item item in Player1.Inventory){
                Console.WriteLine("- "+ item.Name);
            }
            Console.WriteLine("\n[1] - Use item (not available yet)");
            Console.WriteLine("[2] - Discard item");
            Console.WriteLine("[3] - Back\n");
            switch(Console.ReadLine()){
                case "1":
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
                break;

                case "2":
                if(Player1.Inventory.Count != 0){
                    Discard_Item();
                    Update_Gamestate(Gamestates[0]);
                }else{
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

        static void Discard_Item(){
            Console.Clear();
            Console.WriteLine("Your inventory: ");
            int a = 0;
            foreach(Item item in Player1.Inventory){
                Console.WriteLine("["+ a++ +"] - "+ item.Name);
            }
            Console.WriteLine("\nType the number of the item you want to discard : ");
            Console.WriteLine("Tybe 'back' to cancel");
            var b = Console.ReadLine();
            if(b == "back"){
                Console.Clear();
                Manage_Items();
            }else{
                try{
                Convert.ToInt32(b);
                }catch(System.FormatException){
                Console.Clear();
                Console.WriteLine("Please type a number\n");
                Discard_Item();
                }
                Player1.Discard_Item(Player1.Inventory[Convert.ToInt32(b)]);
            }
        }

        static void Discard_Armor(){
            Console.Clear();
            Console.WriteLine("Are you sure ? You will discard the armor you are currently wearing.");
            Console.WriteLine("\n[1] - Yes");
            Console.WriteLine("[2] - No");
            switch(Console.ReadLine()){
                case "1":
                Player1.Discard_Armor(Player1.Worn_Armor[0]);
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
        static void Discard_Weapon(){
            Console.Clear();
            Console.WriteLine("Are you sure ? You will discard the weapon you are currently wielding.");
            Console.WriteLine("\n[1] - Yes");
            Console.WriteLine("[2] - No");
            switch(Console.ReadLine()){
                case "1":
                Player1.Discard_Weapon(Player1.Worn_Weapons[0]);
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

        static void Equip_Weapon(){
            Console.Clear();
            Console.WriteLine("Your Inventory: ");
            List<int> AvailableIndexes = new List<int>();
            foreach (Item weapon in Player1.Inventory){
                if(weapon is Weapon){
                    int b = Player1.Inventory.IndexOf(weapon);
                    Console.WriteLine("["+ b + "] - "+ weapon.Name);
                    AvailableIndexes.Add(b);
                }
            }
            Console.WriteLine("Type the ID of the weapon you want to equip.");
            Console.WriteLine("Type 'back' to cancel");
            var a = Console.ReadLine();
            if(a == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                Convert.ToInt32(a);
                }catch(System.FormatException){
                Console.Clear();
                Console.WriteLine("Please type a number\n");
                Equip_Weapon();
                }
                if(AvailableIndexes.Contains(Convert.ToInt32(a))){
                        Player1.Equip_Weapon(Player1.Inventory[Convert.ToInt32(a)] as Weapon);
                        Player1.Discard_Item(Player1.Inventory[Convert.ToInt32(a)]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Equip_Weapon();
                }
            }
        }

        static void Equip_Armor(){
            Console.Clear();
            Console.WriteLine("Your Inventory: ");
            List<int> AvailableIndexes = new List<int>();
            foreach (Item armor in Player1.Inventory){
                if(armor is Armor){
                    int b = Player1.Inventory.IndexOf(armor);
                    Console.WriteLine("["+ b + "] - "+ armor.Name);
                    AvailableIndexes.Add(b);
                }
            }
            Console.WriteLine("Type the ID of the armor you want to equip.");
            Console.WriteLine("Type 'back' to cancel");
            var a = Console.ReadLine();
            if(a == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                Convert.ToInt32(a);
                }catch(System.FormatException){
                Console.Clear();
                Console.WriteLine("Please type a number\n");
                Equip_Weapon();
                }
                if(AvailableIndexes.Contains(Convert.ToInt32(a))){
                    Player1.Equip_Armor(Player1.Inventory[Convert.ToInt32(a)]! as Armor);
                    Player1.Discard_Item(Player1.Inventory[Convert.ToInt32(a)]);
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Equip_Armor();
                }
            }
        }

        static void LevelUp_Screen(){
            Console.WriteLine("\nPlayer: "+ Player1.Name );

            Console.WriteLine("Level: "+ Player1.Level);
            Console.WriteLine("XP: "+ Player1.XP+ "/" + Player1.Required_XP);
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            Console.WriteLine("HP: "+ Player1.Current_HP +"/"+ Player1.HP_Max);

            Console.WriteLine("\nArmor : "+ Player1.Armor_Stat);
            Console.WriteLine("Magic Resistance : "+ Player1.Magical_Resistance_Stat);
            Console.WriteLine("Physical Attack : "+ Player1.Physical_Attack_Stat);
            Console.WriteLine("Magical Attack : "+ Player1.Magical_Attack_Stat);

            Console.WriteLine("\nStats: Choose which one to upgrade");
            Console.WriteLine("[1] - Vitality : "+ Player1.Vitality_Stat);
            Console.WriteLine("[2] - Strength : "+ Player1.Strength_Stat);
            Console.WriteLine("[3] - Intelligence : "+ Player1.Intelligence_Stat);
            Console.WriteLine("[4] - Speed : "+ Player1.Speed_Stat + "\n");
            Console.WriteLine("[5] - Back");

            switch(Console.ReadLine()){

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

        static void LevelUp_Vitality(){
            Console.WriteLine("You want to upgrade Vitality\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            Console.WriteLine("Type 'back' to go cancel.\n");
            var points = Console.ReadLine();
            if(points == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                    Convert.ToInt32(points);
                }
                catch(System.FormatException){
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Vitality();
                }
                if(Player1.Upgrade_Points > 0){
                    Player1.Upgrade_Vitality(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Strength(){
            Console.WriteLine("You want to upgrade Strength\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            var points = Console.ReadLine();
            if(points == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                    Convert.ToInt32(points);
                }
                catch(System.FormatException){
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Strength();
                }
                if(Player1.Upgrade_Points > 0){
                    Player1.Upgrade_Strength(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Intelligence(){
            Console.WriteLine("You want to upgrade Intelligence\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            var points = Console.ReadLine();
            if(points == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                    Convert.ToInt32(points);
                }
                catch(System.FormatException){
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Intelligence();
                }
                if(Player1.Upgrade_Points > 0){
                    Player1.Upgrade_Intelligence(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
                    Console.Clear();
                    Console.WriteLine("You do not have any upgrade points left.");
                    Console.WriteLine("Press enter to continue..");
                    Console.ReadLine();
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }
            }
        }
        static void LevelUp_Speed(){
            Console.WriteLine("You want to upgrade Speed\n");
            Console.WriteLine("Type how many points you want to add: ");
            Console.WriteLine("Upgrade Points available: " + Player1.Upgrade_Points);
            var points = Console.ReadLine();
            if(points == "back"){
                Console.Clear();
                Update_Gamestate(Gamestates[0]);
            }else{
                try{
                    Convert.ToInt32(points);
                }
                catch(System.FormatException){
                    Console.Clear();
                    Console.WriteLine("Please type either a number or 'back'.\n");
                    LevelUp_Speed();
                }
                if(Player1.Upgrade_Points > 0){
                    Player1.Upgrade_Speed(Convert.ToInt32(points));
                    Console.Clear();
                    Update_Gamestate(Gamestates[0]);
                }else{
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