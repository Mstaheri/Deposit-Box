using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddLoanAndLoanTransactionsAndLoanDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocument_BankAccounts_AccountNumber",
                table: "BankSafeDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBankSafe",
                table: "BankSafeDocument");

            migrationBuilder.DropTable(
                name: "UserSharePrices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankSafeDocument",
                table: "BankSafeDocument");

            migrationBuilder.RenameTable(
                name: "BankSafeDocument",
                newName: "BankSafeDocuments");

            migrationBuilder.RenameColumn(
                name: "WithdrawalPrice",
                table: "BankSafeTransactions",
                newName: "Withdrawal");

            migrationBuilder.RenameColumn(
                name: "DepositPrice",
                table: "BankSafeTransactions",
                newName: "Deposit");

            migrationBuilder.RenameColumn(
                name: "CodeTransactions",
                table: "BankSafeTransactions",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "WithdrawalPrice",
                table: "BankSafeDocuments",
                newName: "Withdrawal");

            migrationBuilder.RenameColumn(
                name: "DepositPrice",
                table: "BankSafeDocuments",
                newName: "Deposit");

            migrationBuilder.RenameColumn(
                name: "CodeDocuments",
                table: "BankSafeDocuments",
                newName: "Code");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocument_NameBankSafe",
                table: "BankSafeDocuments",
                newName: "IX_BankSafeDocuments_NameBankSafe");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocument_AccountNumber",
                table: "BankSafeDocuments",
                newName: "IX_BankSafeDocuments_AccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankSafeDocuments",
                table: "BankSafeDocuments",
                column: "Code");

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Wage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Loans_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAndNumberOfShares",
                columns: table => new
                {
                    NameBankSafe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfShares = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAndNumberOfShares", x => new { x.UserName, x.NameBankSafe });
                    table.ForeignKey(
                        name: "FK_UserAndNumberOfShares_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAndNumberOfShares_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanDocuments",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeLoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Situation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDocuments", x => x.Code);
                    table.ForeignKey(
                        name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanDocuments_Loans_CodeLoan",
                        column: x => x.CodeLoan,
                        principalTable: "Loans",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoanTransactions",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeLoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactions", x => x.Code);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_Loans_CodeLoan",
                        column: x => x.CodeLoan,
                        principalTable: "Loans",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanDocuments_CodeLoan",
                table: "LoanDocuments",
                column: "CodeLoan");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDocuments_NameBankSafe",
                table: "LoanDocuments",
                column: "NameBankSafe");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_NameBankSafe",
                table: "Loans",
                column: "NameBankSafe");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactions_CodeLoan",
                table: "LoanTransactions",
                column: "CodeLoan");

            migrationBuilder.CreateIndex(
                name: "IX_LoanTransactions_NameBankSafe",
                table: "LoanTransactions",
                column: "NameBankSafe");

            migrationBuilder.CreateIndex(
                name: "IX_UserAndNumberOfShares_NameBankSafe",
                table: "UserAndNumberOfShares",
                column: "NameBankSafe");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocuments_BankAccounts_AccountNumber",
                table: "BankSafeDocuments",
                column: "AccountNumber",
                principalTable: "BankAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocuments_BankAccounts_AccountNumber",
                table: "BankSafeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments");

            migrationBuilder.DropTable(
                name: "LoanDocuments");

            migrationBuilder.DropTable(
                name: "LoanTransactions");

            migrationBuilder.DropTable(
                name: "UserAndNumberOfShares");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankSafeDocuments",
                table: "BankSafeDocuments");

            migrationBuilder.RenameTable(
                name: "BankSafeDocuments",
                newName: "BankSafeDocument");

            migrationBuilder.RenameColumn(
                name: "Withdrawal",
                table: "BankSafeTransactions",
                newName: "WithdrawalPrice");

            migrationBuilder.RenameColumn(
                name: "Deposit",
                table: "BankSafeTransactions",
                newName: "DepositPrice");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "BankSafeTransactions",
                newName: "CodeTransactions");

            migrationBuilder.RenameColumn(
                name: "Withdrawal",
                table: "BankSafeDocument",
                newName: "WithdrawalPrice");

            migrationBuilder.RenameColumn(
                name: "Deposit",
                table: "BankSafeDocument",
                newName: "DepositPrice");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "BankSafeDocument",
                newName: "CodeDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocuments_NameBankSafe",
                table: "BankSafeDocument",
                newName: "IX_BankSafeDocument_NameBankSafe");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocuments_AccountNumber",
                table: "BankSafeDocument",
                newName: "IX_BankSafeDocument_AccountNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankSafeDocument",
                table: "BankSafeDocument",
                column: "CodeDocuments");

            migrationBuilder.CreateTable(
                name: "UserSharePrices",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NumberOfShares = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSharePrices", x => new { x.UserName, x.NameBankSafe });
                    table.ForeignKey(
                        name: "FK_UserSharePrices_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSharePrices_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSharePrices_NameBankSafe",
                table: "UserSharePrices",
                column: "NameBankSafe");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocument_BankAccounts_AccountNumber",
                table: "BankSafeDocument",
                column: "AccountNumber",
                principalTable: "BankAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBankSafe",
                table: "BankSafeDocument",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
