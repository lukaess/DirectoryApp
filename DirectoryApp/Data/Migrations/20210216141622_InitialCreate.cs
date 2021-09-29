using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Adress", "DateOfBirth", "Email", "IsAdmin", "Name", "Password", "Surname" },
                values: new object[,]
				{
					{ 1L, "Slavonski Brod, Branimirova 86", new DateTime(1995, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "lkristic@eko.hr", true, "Lukas", "Kong35", "Krištić" },
                    { 2L, "Slavonski Brod, Branimirova 86", new DateTime(1963, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "mato@eko.hr", true, "Mato", "Baki49", "Krištić" },
					{ 3L, "Osijek, Stjepana Radića 2", new DateTime(1991, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "tmintra@eko.hr", false, "Tony", "Legolas4", "Mitrandil" },
                    { 4L, "Rijeka, Ivana Držića 7", new DateTime(1993, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "aolimp@eko.hr", false, "Artemida", "Hades191", "Olimp" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
