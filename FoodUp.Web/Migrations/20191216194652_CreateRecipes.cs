using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodUp.Web.Migrations
{
  public partial class CreateRecipes : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Recipe",
          columns: table => new {
              Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
              Title = table.Column<string>(nullable: false),
              Content = table.Column<string>(nullable: false),
              CreatorId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Recipe", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Recipe");
    }
  }
}
