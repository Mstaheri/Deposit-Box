using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankSafes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SharePrice = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    NationalIDNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    Wage = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "BankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Users_UserName",
                        column: x => x.UserName,
                        principalTable: "Users",
                        principalColumn: "UserName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAndNumberOfShares",
                columns: table => new
                {
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    NumberOfShares = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    Situation = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDocuments", x => x.Code);
                    table.ForeignKey(
                        name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_LoanDocuments_Loans_CodeLoan",
                        column: x => x.CodeLoan,
                        principalTable: "Loans",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "LoanTransactions",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodeLoan = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfInstallments = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanTransactions", x => x.Code);
                    table.ForeignKey(
                        name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_LoanTransactions_Loans_CodeLoan",
                        column: x => x.CodeLoan,
                        principalTable: "Loans",
                        principalColumn: "Code");
                });

            migrationBuilder.CreateTable(
                name: "BankSafeDocuments",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    RegistrationDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    Situation = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafeDocuments", x => x.Code);
                    table.ForeignKey(
                        name: "FK_BankSafeDocuments_BankAccounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankSafeTransactions",
                columns: table => new
                {
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameBankSafe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Deposit = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    Withdrawal = table.Column<decimal>(type: "decimal(18,2)", maxLength: 12, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankSafeTransactions", x => x.Code);
                    table.ForeignKey(
                        name: "FK_BankSafeTransactions_BankAccounts_AccountNumber",
                        column: x => x.AccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                        column: x => x.NameBankSafe,
                        principalTable: "BankSafes",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_UserName",
                table: "BankAccounts",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeDocuments_AccountNumber",
                table: "BankSafeDocuments",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeDocuments_NameBankSafe",
                table: "BankSafeDocuments",
                column: "NameBankSafe");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeTransactions_AccountNumber",
                table: "BankSafeTransactions",
                column: "AccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BankSafeTransactions_NameBankSafe",
                table: "BankSafeTransactions",
                column: "NameBankSafe");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankSafeDocuments");

            migrationBuilder.DropTable(
                name: "BankSafeTransactions");

            migrationBuilder.DropTable(
                name: "LoanDocuments");

            migrationBuilder.DropTable(
                name: "LoanTransactions");

            migrationBuilder.DropTable(
                name: "UserAndNumberOfShares");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BankSafes");
        }
    }
}
