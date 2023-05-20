namespace ConsoleRPG{
    public class Player
    {
        public Player(string name)
        {
            this.Name = name;
            Level = Vitality_Stat + Strength_Stat + Intelligence_Stat + Speed_Stat - 3;
        }
        public string Name;
        public int Required_XP = 1000;
        public int XP = 0;
        public int Upgrade_Points = 0;
        public int HP_Max = 100;
        public int Current_HP = 100;
        public bool IsAlive = true;

        public int Vitality_Stat = 1;
        public int Strength_Stat = 1;
        public int Intelligence_Stat = 1;
        public int Speed_Stat = 1;

        public int Level;
        public double Armor_Stat = 1;
        public double Magical_Resistance_Stat = 1;
        public double Physical_Attack_Stat = 1;
        public double Magical_Attack_Stat = 1;
        public int Mend_Wounds_Charges = 1;

        public int Money = 0;

        public bool IsProtecting = false;

        public List<Item> Inventory = new List<Item>(20);
        public List<Weapon> Worn_Weapons = new List<Weapon>(1);
        public List<Armor> Worn_Armor = new List<Armor>(1);

        public int Current_Room = 1;

        public void Update_Name(string name)
        {
            Name = name;
        }

        public void Earn_Money(int money){
            Money += money;
        }

        public void Lose_Money(int money){
            Money -= money;
        }
        public void Gain_XP(int xp)
        {
            XP += xp;
            Update_UpgradePoints();
        }

        public void Update_UpgradePoints()
        {
            while (XP >= Required_XP)
            {
                Console.WriteLine($"Level up ! You have {Upgrade_Points + 1} upgrade points !");
                XP -= Required_XP;
                Upgrade_Points += 1;
                Required_XP += 500;
            }
        }
        public void Update_Level()
        {
            Level = Vitality_Stat + Strength_Stat + Intelligence_Stat + Speed_Stat - 3;
        }

        public void Update_MaxHP()
        {
            if (Vitality_Stat > 1)
            {
                HP_Max += 25 * (Vitality_Stat - 1);
            }
        }
        public void Upgrade_Vitality(int vitality)
        {
            Vitality_Stat += vitality;
            Upgrade_Points -= vitality;
            Update_MaxHP();
            Current_HP += 25 * vitality;
            Update_Level();
        }
        public void Upgrade_Strength(int strength)
        {
            Strength_Stat += strength;
            Upgrade_Points -= strength;
            Update_Level();
        }
        public void Upgrade_Intelligence(int intelligence)
        {
            Intelligence_Stat += intelligence;
            Upgrade_Points -= intelligence;
            Update_Level();
        }
        public void Upgrade_Speed(int speed)
        {
            Speed_Stat += speed;
            Upgrade_Points -= speed;
            Update_Level();
        }
        public void Update_Armor()
        {
            if (Worn_Armor.Count == 1)
            {
                Armor_Stat = Worn_Armor[0].Armor_Points + 1;
            }
            else
            {
                Armor_Stat = 1;
            }
        }
        public void Update_MagicResist()
        {
            if (Worn_Armor.Count == 1)
            {
                Magical_Resistance_Stat = Worn_Armor[0].Magical_Resistance + 1;
            }
            else
            {
                Magical_Resistance_Stat = 1;
            }
        }
        public void Update_PhysicalAttack()
        {
            if (Worn_Weapons.Count == 1)
            {
                Physical_Attack_Stat = Math.Floor(Worn_Weapons[0].Attack_Damage + (Worn_Weapons[0].Attack_Damage * (0.05 * Strength_Stat)));
            }
            else
            {
                Physical_Attack_Stat = 1 + 1 * (0.05 * Strength_Stat);
            }
        }
        public void Update_MagicalAttack()
        {
            if (Worn_Weapons.Count == 1)
            {
                Magical_Attack_Stat = Math.Floor(Worn_Weapons[0].Magical_Damage + (Worn_Weapons[0].Magical_Damage * (0.05 * Intelligence_Stat)));
            }
            else
            {
                Magical_Attack_Stat = 1 + 1 * (0.05 * Intelligence_Stat);
            }
        }

        public void Attack_Enemy(Enemy target)
        {
            var Physical_Damage = Physical_Attack_Stat;
            var Magical_Damage = Magical_Attack_Stat;
            var Total_Brut_Damage = Physical_Damage + Magical_Damage;
            target.Receive_Damage(Physical_Damage, Magical_Damage);
        }
        public void Receive_Damage(double physical_damage, double magical_damage)
        {
            var Received_Physical_Damage = Convert.ToInt32(Math.Floor(physical_damage - (physical_damage * (0.04 * Armor_Stat))));
            var Received_Magical_Damage = Convert.ToInt32(Math.Floor(magical_damage - (magical_damage * (0.04 * Magical_Resistance_Stat))));
            var Total_Received_Damage = Received_Magical_Damage + Received_Physical_Damage;

            if (IsProtecting)
            {
                Current_HP -= Total_Received_Damage / 2;
            }
            else
            {
                Current_HP -= Total_Received_Damage;
            }
            CheckifAlive();
        }

        public void Equip_Weapon(Weapon weapon)
        {
            if (Worn_Weapons.Count == 1)
            {
                Unequip_Weapon(Worn_Weapons[0]);
            }
            Worn_Weapons.Add(weapon);
            Update_PhysicalAttack();
            Update_MagicalAttack();
        }
        public void Unequip_Weapon(Weapon weapon)
        {
            if (Worn_Weapons.Count == 1)
            {
                Worn_Weapons.Remove(weapon);
                Inventory.Add(weapon);
                Update_PhysicalAttack();
                Update_MagicalAttack();
            }
        }
        public void Discard_Weapon(Weapon weapon)
        {
            Worn_Weapons.Remove(weapon);
            Update_PhysicalAttack();
            Update_MagicalAttack();
        }

        public void Equip_Armor(Armor armor)
        {
            if (Worn_Armor.Count == 1)
            {
                Unequip_Armor(Worn_Armor[0]);
            }
            Worn_Armor.Add(armor);
            Update_Armor();
        }
        public void Unequip_Armor(Armor armor)
        {
            Worn_Armor.Remove(armor);
            Inventory.Add(armor);
            Update_Armor();
        }
        public void Discard_Armor(Armor armor)
        {
            Worn_Armor.Remove(armor);
            Update_Armor();
        }
        public void Discard_Item(Item item_name)
        {
            Inventory.Remove(item_name);
        }

        public void AddToInventory(Item item_name)
        {
            Inventory.Add(item_name);
        }

        public void CheckifAlive()
        {
            if (Current_HP <= 0)
            {
                IsAlive = false;
            }
        }

        public bool CheckifArmed()
        {
            if (Worn_Weapons.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckifArmored()
        {
            if (Worn_Armor.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Mend_Wounds()
        {
            Current_HP += Convert.ToInt32(Math.Floor(0.3 * HP_Max));
            if (Current_HP > HP_Max)
            {
                Current_HP = HP_Max;
            }
            Mend_Wounds_Charges -= 1;
        }

        public void Protect()
        {
            IsProtecting = true;
        }

        public void Update_Mend_Wounds_Charges( ){
            if(Mend_Wounds_Charges <=0){
                Mend_Wounds_Charges += 1;
            }
        }

    }
}