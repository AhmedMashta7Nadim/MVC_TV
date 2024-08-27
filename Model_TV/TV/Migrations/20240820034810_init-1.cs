using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TV.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TV_Languages_Languages_Id",
                table: "TV_Languages");

            migrationBuilder.CreateIndex(
                name: "IX_TV_Languages_LanguagesId",
                table: "TV_Languages",
                column: "LanguagesId");

            migrationBuilder.AddForeignKey(
                name: "FK_TV_Languages_Languages_LanguagesId",
                table: "TV_Languages",
                column: "LanguagesId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TV_Languages_Languages_LanguagesId",
                table: "TV_Languages");

            migrationBuilder.DropIndex(
                name: "IX_TV_Languages_LanguagesId",
                table: "TV_Languages");

            migrationBuilder.AddForeignKey(
                name: "FK_TV_Languages_Languages_Id",
                table: "TV_Languages",
                column: "Id",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
