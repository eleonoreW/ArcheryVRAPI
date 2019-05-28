using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Archery.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sujet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sujet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GradeID = table.Column<int>(nullable: false),
                    SujetID = table.Column<int>(nullable: false),
                    QuestionText = table.Column<string>(unicode: false, maxLength: 400, nullable: false),
                    Difficult = table.Column<int>(nullable: false),
                    Explanation = table.Column<string>(unicode: false, maxLength: 400, nullable: false),
                    GoodAnswers = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    BadAnswers = table.Column<string>(unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.ID);
                    table.ForeignKey(
                        name: "Question_FK_Grade",
                        column: x => x.GradeID,
                        principalTable: "Grade",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Question_FK_Sujet",
                        column: x => x.SujetID,
                        principalTable: "Sujet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Grade__C7D1C61E128B661D",
                table: "Grade",
                column: "Nom",
                unique: true,
                filter: "[Nom] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Question_GradeID",
                table: "Question",
                column: "GradeID");

            migrationBuilder.CreateIndex(
                name: "IX_Question_SujetID",
                table: "Question",
                column: "SujetID");

            migrationBuilder.CreateIndex(
                name: "UQ__Sujet__C7D1C61E3B68B3AE",
                table: "Sujet",
                column: "Nom",
                unique: true,
                filter: "[Nom] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Sujet");
        }
    }
}
