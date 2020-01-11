using Microsoft.EntityFrameworkCore.Migrations;

namespace FoodUp.Web.Migrations
{
  public partial class CreateReviews : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Review",
          columns: table => new {
              Id = table.Column<int>(nullable: false)
                  .Annotation("Sqlite:Autoincrement", true),
              Rating = table.Column<int>(nullable: false),
              Comment = table.Column<string>(nullable: false),
              ReviewerId = table.Column<int>(nullable: false),
              RecipeId = table.Column<int>(nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Review", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Review");
    }
  }
}
