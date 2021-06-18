using Microsoft.EntityFrameworkCore.Migrations;

namespace constructionCompanyAPI.Migrations
{
    public partial class ConstructionCompanyCreatedByIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ConstructionCompanies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConstructionCompanies_CreatedById",
                table: "ConstructionCompanies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ConstructionCompanies_Users_CreatedById",
                table: "ConstructionCompanies",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConstructionCompanies_Users_CreatedById",
                table: "ConstructionCompanies");

            migrationBuilder.DropIndex(
                name: "IX_ConstructionCompanies_CreatedById",
                table: "ConstructionCompanies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ConstructionCompanies");
        }
    }
}
