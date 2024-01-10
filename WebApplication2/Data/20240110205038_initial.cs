using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Data
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserCred",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AlterColumn<double>(
                name: "GradesAvg",
                table: "Students",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIncrease",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Brithday",
                table: "Students",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QST_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QST_Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QST_Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QST_Id);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    ANS_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ANS_QST_ID = table.Column<int>(type: "int", nullable: false),
                    ANS_Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ANS_isCorrectAns = table.Column<int>(type: "int", nullable: false),
                    ANS_Votes = table.Column<int>(type: "int", nullable: false),
                    QuestionQST_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.ANS_Id);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionQST_Id",
                        column: x => x.QuestionQST_Id,
                        principalTable: "Question",
                        principalColumn: "QST_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionQST_Id",
                table: "Answer",
                column: "QuestionQST_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.AlterColumn<double>(
                name: "GradesAvg",
                table: "Students",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateIncrease",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Brithday",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "UserCred",
                columns: new[] { "UserId", "Email", "Password", "UserName" },
                values: new object[] { 1, "AdminTest@gmail.com", "12345", "UserAdminTest" });
        }
    }
}
