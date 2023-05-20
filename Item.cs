namespace ConsoleRPG{
   public class Item {

        private static int currentID;
        public string Name;
        protected int ID {get; set;}

        public Item(){
            ID = 0;
            Name = "Unknown";
        }

        public Item(string name){
            this.ID = GetNextID();
            Name = name;
        }

        static Item() => currentID = 0;
        protected int GetNextID() => ++currentID;

        public void Update(string name){
            Name = name;
        }
    }

    public class Armor : Item {

        public double Armor_Points;
        public double Magical_Resistance;

        public Armor(){
            this.Armor_Points = 1;
            this.Magical_Resistance = 1;
        }
        public Armor(string name, double armor, double magic_resist){
            Name = name;
            this.ID = GetNextID();
            this.Armor_Points = armor;
            this.Magical_Resistance = magic_resist;
        }
    }
    public class Weapon : Item {
        public int Attack_Damage;
        public int Magical_Damage;

        public Weapon(){
            this.Attack_Damage = 1;
            this.Magical_Damage = 1;
        }
        public Weapon(string name, int attack, int magic){
            Name = name;
            this.ID = GetNextID();
            this.Attack_Damage = attack;
            this.Magical_Damage = magic;
        }
    }

    public class Health_Potion : Item{
        public int Heal_Amount;
        public int Uses_Available;

        public Health_Potion(Player player){
            this.Heal_Amount = 50 + Convert.ToInt32(Math.Floor(Convert.ToDouble(player.HP_Max) * 0.25));
            this.Uses_Available = 1;
        }

        public Health_Potion(Player player, string name, int healamount, int uses_available, double healpercentage){
            this.Heal_Amount = healamount + Convert.ToInt32(Math.Floor(Convert.ToDouble(player.HP_Max) * healpercentage));
            this.Name = name;
            this.Uses_Available = uses_available;
            this.ID = GetNextID();
        }
    }
}