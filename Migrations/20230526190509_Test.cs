using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleRPG.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Required_XP = table.Column<int>(type: "INTEGER", nullable: false),
                    XP = table.Column<int>(type: "INTEGER", nullable: false),
                    Upgrade_Points = table.Column<int>(type: "INTEGER", nullable: false),
                    HP_Max = table.Column<int>(type: "INTEGER", nullable: false),
                    Current_HP = table.Column<int>(type: "INTEGER", nullable: false),
                    IsAlive = table.Column<bool>(type: "INTEGER", nullable: false),
                    Vitality_Stat = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength_Stat = table.Column<int>(type: "INTEGER", nullable: false),
                    Intelligence_Stat = table.Column<int>(type: "INTEGER", nullable: false),
                    Speed_Stat = table.Column<int>(type: "INTEGER", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    Armor_Stat = table.Column<double>(type: "REAL", nullable: false),
                    Magical_Resistance_Stat = table.Column<double>(type: "REAL", nullable: false),
                    Physical_Attack_Stat = table.Column<double>(type: "REAL", nullable: false),
                    Magical_Attack_Stat = table.Column<double>(type: "REAL", nullable: false),
                    Mend_Wounds_Charges = table.Column<int>(type: "INTEGER", nullable: false),
                    Money = table.Column<int>(type: "INTEGER", nullable: false),
                    Current_Room = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: true),
                    Armor_Points = table.Column<double>(type: "REAL", nullable: true),
                    Magical_Resistance = table.Column<double>(type: "REAL", nullable: true),
                    PlayerId1 = table.Column<int>(type: "INTEGER", nullable: true),
                    Attack_Damage = table.Column<int>(type: "INTEGER", nullable: true),
                    Magical_Damage = table.Column<int>(type: "INTEGER", nullable: true),
                    Weapon_PlayerId1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Players_PlayerId1",
                        column: x => x.PlayerId1,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Item_Players_Weapon_PlayerId1",
                        column: x => x.Weapon_PlayerId1,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerId",
                table: "Item",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_PlayerId1",
                table: "Item",
                column: "PlayerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Weapon_PlayerId1",
                table: "Item",
                column: "Weapon_PlayerId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
