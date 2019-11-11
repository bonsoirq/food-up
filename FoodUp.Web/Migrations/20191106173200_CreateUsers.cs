using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodUp.Web.Migrations
{
  public partial class CreateUsers : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "User",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
            Login = table.Column<string>(nullable: false),
            EncryptedPassword = table.Column<string>(nullable: false),
            Birthday = table.Column<DateTime>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_User", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "User");
    }
  }
}
