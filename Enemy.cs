namespace Jeu_Test{
    public class Enemy {
        private static int currentID;
        public string Name;
        protected int ID{get; set;}

        static Enemy() => currentID = 0;
        protected int GetNextID() => ++currentID;

        public int Armor_Stat;
        public int Magical_Resistance_Stat;

        public int Speed_Stat;

        public int HP_Max;
        public int Current_HP;
        public bool IsAlive;

        public int Physical_Attack_Stat;
        public int Magical_Attack_Stat;

        public int XP_Reward;

        public Enemy(){
            ID = 0;
            Name = "Unknown";
        }
        public Enemy(string name,int hpmax, int armor, int magic_resist, int attack, int magic_attack,int speed, int xpreward){
            this.Name = name;
            this.ID = GetNextID();

            this.Armor_Stat = armor;
            this.Magical_Resistance_Stat = magic_resist;
            this.Physical_Attack_Stat = attack;
            this.Magical_Attack_Stat = magic_attack;
            this.XP_Reward = xpreward;
            this.Speed_Stat = speed;

            this.HP_Max = hpmax;
            this.Current_HP = HP_Max;
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

        public void Reset_Enemies(){
            switch(this.Name){
                case "Goblin Warrior":
                HP_Max = 100;
                Armor_Stat = 2;
                Magical_Resistance_Stat = 1;
                Physical_Attack_Stat = 15;
                Magical_Attack_Stat = 0;
                Speed_Stat = 1;
                XP_Reward = 400;
                break;

                case "Goblin Mage":
                HP_Max = 75;
                Armor_Stat = 0;
                Magical_Resistance_Stat = 2;
                Physical_Attack_Stat = 0;
                Magical_Attack_Stat = 15;
                Speed_Stat = 1;
                XP_Reward = 300;
                break;

                case "Goblin Lord":
                HP_Max = 135;
                Armor_Stat = 3;
                Magical_Resistance_Stat = 3;
                Physical_Attack_Stat = 15;
                Magical_Attack_Stat = 10;
                Speed_Stat = 1;
                XP_Reward = 800;
                break;

                case "Goblin Peasant":
                HP_Max = 50;
                Armor_Stat = 0;
                Magical_Resistance_Stat = 0;
                Physical_Attack_Stat = 10;
                Magical_Attack_Stat = 0;
                Speed_Stat = 1;
                XP_Reward = 200;
                break;

                case "Goblin":
                HP_Max = 65;
                Armor_Stat = 1;
                Magical_Resistance_Stat = 0;
                Physical_Attack_Stat = 12;
                Magical_Attack_Stat = 0;
                Speed_Stat = 1;
                XP_Reward = 250;
                break;

                default:
                HP_Max = 100;
                Armor_Stat = 1;
                Magical_Resistance_Stat = 1;
                Physical_Attack_Stat = 10;
                Magical_Attack_Stat = 10;
                Speed_Stat = 1;
                XP_Reward = 500;
                break;
            }
        }
        public void Scale_Enemy(Player player){
            var Scale = 0.0;
            if(player.Level == 1){
                Scale = 0;
            }else{
                Scale = (player.Level - 1) * 0.1;
            }

            HP_Max = Convert.ToInt32(Math.Floor(HP_Max + (HP_Max * Scale)));
            Armor_Stat = Convert.ToInt32(Math.Floor(Armor_Stat + (Armor_Stat * Scale)));
            Magical_Resistance_Stat = Convert.ToInt32(Math.Floor(Magical_Resistance_Stat + (Magical_Resistance_Stat * Scale)));
            Physical_Attack_Stat = Convert.ToInt32(Math.Floor(Physical_Attack_Stat + (Physical_Attack_Stat * Scale)));
            Magical_Attack_Stat = Convert.ToInt32(Math.Floor(Magical_Attack_Stat + (Magical_Attack_Stat * Scale)));
            Speed_Stat = Convert.ToInt32(Math.Floor(Speed_Stat + Convert.ToDouble((Speed_Stat * (player.Level / 4)))));
            XP_Reward = Convert.ToInt32(Math.Floor(XP_Reward + (XP_Reward * Scale)));
            Current_HP = HP_Max;
            IsAlive = true;
        }

        static public void Create(){
            Enemy Goblin_Warrior = new Enemy("Goblin Warrior", 100, 2, 1, 15, 0, 1, 400);
            Enemy Goblin_Mage= new Enemy("Goblin Mage", 75, 0, 2, 0, 15, 1, 300);
            Enemy Goblin_Lord= new Enemy("Goblin Lord", 135, 3, 3, 15, 10, 1, 800);
            Enemy Goblin_Peasant= new Enemy("Goblin Peasant", 50, 0, 0, 10, 0, 1, 200);
            Enemy Goblin = new Enemy("Goblin", 65, 1, 0, 12, 0, 1, 250);

            GameState.Available_Enemies.Add(Goblin_Warrior);
            GameState.Available_Enemies.Add(Goblin_Mage);
            GameState.Available_Enemies.Add(Goblin_Lord);
            GameState.Available_Enemies.Add(Goblin_Peasant);
            GameState.Available_Enemies.Add(Goblin);
        }
    }
}