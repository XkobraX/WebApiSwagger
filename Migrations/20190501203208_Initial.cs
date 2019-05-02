using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UserApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Login = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    CPF = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    senha = table.Column<string>(nullable: true),
                    acessKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CPF", "DataCadastro", "DataNascimento", "Email", "Login", "Nome", "acessKey", "senha" },
                values: new object[] { 1, "059.268.940-93", new DateTime(2019, 5, 1, 17, 32, 8, 247, DateTimeKind.Local).AddTicks(8439), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pedromail@pedro.com", "PedroADM", "Pedro Josué", "94be650011cf412ca906fc335f615cdc", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
