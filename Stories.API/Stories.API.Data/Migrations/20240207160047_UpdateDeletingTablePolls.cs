using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stories.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeletingTablePolls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Polls_PollId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.RenameColumn(
                name: "PollId",
                table: "Votes",
                newName: "StoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_PollId",
                table: "Votes",
                newName: "IX_Votes_StoryId");

            migrationBuilder.AddColumn<string>(
                name: "Departament",
                table: "Stories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Stories_StoryId",
                table: "Votes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Stories_StoryId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Departament",
                table: "Stories");

            migrationBuilder.RenameColumn(
                name: "StoryId",
                table: "Votes",
                newName: "PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_StoryId",
                table: "Votes",
                newName: "IX_Votes_PollId");

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Polls_StoryId",
                table: "Polls",
                column: "StoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Polls_PollId",
                table: "Votes",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
