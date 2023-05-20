namespace ConsoleRPG{
    public class Enemy {
        private static int currentID;
        public string Name;
        protected int ID{get; set;}

        static Enemy() => currentID = 0;
        protected int GetNextID() => ++currentID;

        public int Base_Magical_Attack_Stat;
        public int Base_Physical_Attack_Stat;
        public int Base_Magical_Resistance_Stat;
        public int Base_Armor_Stat;
        public int Base_HP_Max;
        public int Base_Speed_Stat;
        public int Base_XP_Reward;
        public int Base_Money_Reward;

        public int Armor_Stat;
        public int Magical_Resistance_Stat;

        public int Speed_Stat;

        public int HP_Max;
        public int Current_HP;
        public bool IsAlive;

        public int Physical_Attack_Stat;
        public int Magical_Attack_Stat;

        public int XP_Reward;
        public int Money_Reward;

        public Enemy(){
            ID = 0;
            Name = "Unknown";
        }
        public Enemy(string name,int hpmax, int armor, int magic_resist, int attack, int magic_attack,int speed, int xpreward, int moneyreward){
            this.Name = name;
            this.ID = GetNextID();

            this.Base_Armor_Stat = armor;
            this.Base_Magical_Resistance_Stat = magic_resist;
            this.Base_Physical_Attack_Stat = attack;
            this.Base_Magical_Attack_Stat = magic_attack;
            this.Base_Speed_Stat = speed;
            this.Base_XP_Reward = xpreward;
            this.Base_Money_Reward = moneyreward;

            this.Base_HP_Max = hpmax;
            this.IsAlive = true;
        }

        public void Attack_Player(Player target){
            var Physical_Damage = Physical_Attack_Stat; 
            var Magical_Damage = Magical_Attack_Stat;
            var Total_Brut_Damage = Physical_Damage + Magical_Damage;
            target.Receive_Damage(Physical_Damage, Magical_Damage);
        }

        public void Receive_Damage(double physical_damage, double magical_damage){
            var Received_Physical_Damage = Convert.ToInt32(Math.Floor(physical_damage - (physical_damage * (0.04 * Armor_Stat))));
            var Received_Magical_Damage = Convert.ToInt32(Math.Floor(magical_damage - (magical_damage * (0.04 * Magical_Resistance_Stat))));
            var Total_Received_Damage = Received_Magical_Damage + Received_Physical_Damage;
            
            Current_HP -= Total_Received_Damage;
            CheckifAlive();
        }

        public void CheckifAlive(){
            if(Current_HP <= 0){
                IsAlive = false;
            }
        }

        public void Scale_Enemy(Player player){
            var Scale = 0.0;
            if(player.Level == 1){
                Scale = 0;
            }else{
                Scale = (player.Level - 1) * 0.1;
            }

            HP_Max = Convert.ToInt32(Math.Floor(Base_HP_Max + (Base_HP_Max * Scale)));
            Armor_Stat = Convert.ToInt32(Math.Floor(Base_Armor_Stat + (Base_Armor_Stat * Scale)));
            Magical_Resistance_Stat = Convert.ToInt32(Math.Floor(Base_Magical_Resistance_Stat + (Base_Magical_Resistance_Stat * Scale)));
            Physical_Attack_Stat = Convert.ToInt32(Math.Floor(Base_Physical_Attack_Stat + (Base_Physical_Attack_Stat * Scale)));
            Magical_Attack_Stat = Convert.ToInt32(Math.Floor(Base_Magical_Attack_Stat + (Base_Magical_Attack_Stat * Scale)));
            Speed_Stat = Convert.ToInt32(Math.Floor(Base_Speed_Stat + Convert.ToDouble((Base_Speed_Stat * (player.Level / 4)))));
            XP_Reward = Convert.ToInt32(Math.Floor(Base_XP_Reward + (Base_XP_Reward * Scale)));
            Money_Reward = Convert.ToInt32(Math.Floor(Base_Money_Reward + (Base_Money_Reward * Scale)));
            Current_HP = HP_Max;
            IsAlive = true;
        }

        static public void Create(){
            Enemy Goblin_Warrior = new Enemy("Goblin Warrior", 100, 2, 1, 15, 0, 1, 400, 35);
            Enemy Goblin_Mage= new Enemy("Goblin Mage", 75, 0, 2, 0, 15, 1, 300, 20);
            Enemy Goblin_Lord= new Enemy("Goblin Lord", 135, 3, 3, 15, 10, 1, 800, 150);
            Enemy Goblin_Peasant= new Enemy("Goblin Peasant", 50, 0, 0, 10, 0, 1, 200, 5);
            Enemy Goblin = new Enemy("Goblin", 65, 1, 0, 12, 0, 1, 250, 10);

            Enemy Skeleton = new Enemy("Skeleton", 65, 0, 1, 15, 0, 1, 275, 10);
            Enemy Skeleton_Warrior = new Enemy("Skeleton Warrior", 90, 2, 2, 15, 0, 1, 350, 50);
            Enemy Skeleton_Thief = new Enemy("Skeleton Thief", 50, 1, 1, 15, 5, 2, 300, 100);
            Enemy Skeleton_King = new Enemy("Skeleton King", 150, 3, 3, 25, 5, 2, 1200, 225);

            Enemy Slime = new Enemy("Slime", 100, 5, 5, 8, 5, 1, 350, 75);
            Enemy Big_Slime = new Enemy("Big Slime", 400, 12, 12, 10, 10, 0, 2500, 400);

            Enemy Bandit = new Enemy("Bandit", 80, 1, 0, 15, 0, 1, 300, 40);
            Enemy Bandit_Archer = new Enemy("Bandit Archer", 65, 1, 0, 20, 0, 2, 300, 40);
            Enemy Bandit_Mage = new Enemy("Bandit Mage", 60, 1, 1, 0, 20, 1, 350, 50);
            Enemy Bandit_Chief = new Enemy("Bandit Chief", 115, 2, 2, 30, 0, 2, 1000, 150);
            Enemy Bandit_Scout = new Enemy("Bandit Scout", 50, 1, 0, 15, 3, 2, 150, 35);

            Enemy Blue_Dragon = new Enemy("Blue Dragon", 725, 8, 12, 8, 55, 0, 7000, 3000);
            Enemy Red_Dragon = new Enemy("Red Dragon", 1000, 12, 8, 70, 12, 0, 10000, 4500);

            GameState.Available_Enemies.Add(Goblin_Warrior);
            GameState.Available_Enemies.Add(Goblin_Mage);
            GameState.Available_Enemies.Add(Goblin_Lord);
            GameState.Available_Enemies.Add(Goblin_Peasant);
            GameState.Available_Enemies.Add(Goblin);

            GameState.Available_Enemies.Add(Skeleton);
            GameState.Available_Enemies.Add(Skeleton_Warrior);
            GameState.Available_Enemies.Add(Skeleton_Thief);
            GameState.Available_Enemies.Add(Skeleton_King);

            GameState.Available_Enemies.Add(Slime);
            GameState.Available_Enemies.Add(Big_Slime);

            GameState.Available_Enemies.Add(Bandit);
            GameState.Available_Enemies.Add(Bandit_Archer);
            GameState.Available_Enemies.Add(Bandit_Mage);
            GameState.Available_Enemies.Add(Bandit_Chief);
            GameState.Available_Enemies.Add(Bandit_Scout);

            GameState.Available_Enemies.Add(Blue_Dragon);
            GameState.Available_Enemies.Add(Red_Dragon);
        }
    }
}