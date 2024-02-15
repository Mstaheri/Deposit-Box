using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddBankSafeDocumentAndBankSafeTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSharePrices_Boxs_NameBox",
                table: "UserSharePrices");

            migrationBuilder.DropTable(
                name: "Boxs");

            migrationBuilder.CreateTable(
                name: "BankSafes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "BankSafeDocument",
                columns: table => new
                {
                    CodeDocuments = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBox = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Situation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafeDocument", x => x.CodeDocuments);
                    table.ForeignKey(
                        name: "FK_BankSafeDocument_BankAccounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankSafeDocument_BankSafes_NameBox",
                        column: x => x.NameBox,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankSafeTransactions",
                columns: table => new
                {
                    CodeTransactions = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBox = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepositPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafeTransactions", x => x.CodeTransactions);
                    table.ForeignKey(
                        name: "FK_BankSafeTransactions_BankAccounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankSafeTransactions_BankSafes_NameBox",
                        column: x => x.NameBox,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeDocument_AccountNumber",
                table: "BankSafeDocument",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeDocument_NameBox",
                table: "BankSafeDocument",
                column: "NameBox");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeTransactions_AccountNumber",
                table: "BankSafeTransactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeTransactions_NameBox",
                table: "BankSafeTransactions",
                column: "NameBox");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSharePrices_BankSafes_NameBox",
                table: "UserSharePrices",
                column: "NameBox",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSharePrices_BankSafes_NameBox",
                table: "UserSharePrices");

            migrationBuilder.DropTable(
                name: "BankSafeDocument");

            migrationBuilder.DropTable(
                name: "BankSafeTransactions");

            migrationBuilder.DropTable(
                name: "BankSafes");

            migrationBuilder.CreateTable(
                name: "Boxs",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boxs", x => x.Name);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSharePrices_Boxs_NameBox",
                table: "UserSharePrices",
                column: "NameBox",
                principalTable: "Boxs",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
