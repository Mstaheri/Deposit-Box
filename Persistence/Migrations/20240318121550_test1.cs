using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_UserName",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                table: "BankSafeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                table: "LoanDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocuments_Loans_CodeLoan",
                table: "LoanDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_BankSafes_NameBankSafe",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                table: "LoanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTransactions_Loans_CodeLoan",
                table: "LoanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAndNumberOfShares_BankSafes_NameBankSafe",
                table: "UserAndNumberOfShares");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAndNumberOfShares_Users_UserName",
                table: "UserAndNumberOfShares");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_UserName",
                table: "BankAccounts",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                table: "BankSafeTransactions",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                table: "LoanDocuments",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocuments_Loans_CodeLoan",
                table: "LoanDocuments",
                column: "CodeLoan",
                principalTable: "Loans",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_BankSafes_NameBankSafe",
                table: "Loans",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                table: "LoanTransactions",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanTransactions_Loans_CodeLoan",
                table: "LoanTransactions",
                column: "CodeLoan",
                principalTable: "Loans",
                principalColumn: "Code");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndNumberOfShares_BankSafes_NameBankSafe",
                table: "UserAndNumberOfShares",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndNumberOfShares_Users_UserName",
                table: "UserAndNumberOfShares",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Users_UserName",
                table: "BankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                table: "BankSafeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                table: "LoanDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDocuments_Loans_CodeLoan",
                table: "LoanDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_BankSafes_NameBankSafe",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                table: "LoanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanTransactions_Loans_CodeLoan",
                table: "LoanTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAndNumberOfShares_BankSafes_NameBankSafe",
                table: "UserAndNumberOfShares");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAndNumberOfShares_Users_UserName",
                table: "UserAndNumberOfShares");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Users_UserName",
                table: "BankAccounts",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocuments_BankSafes_NameBankSafe",
                table: "BankSafeDocuments",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                table: "BankSafeTransactions",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocuments_BankSafes_NameBankSafe",
                table: "LoanDocuments",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDocuments_Loans_CodeLoan",
                table: "LoanDocuments",
                column: "CodeLoan",
                principalTable: "Loans",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_BankSafes_NameBankSafe",
                table: "Loans",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanTransactions_BankSafes_NameBankSafe",
                table: "LoanTransactions",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanTransactions_Loans_CodeLoan",
                table: "LoanTransactions",
                column: "CodeLoan",
                principalTable: "Loans",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndNumberOfShares_BankSafes_NameBankSafe",
                table: "UserAndNumberOfShares",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAndNumberOfShares_Users_UserName",
                table: "UserAndNumberOfShares",
                column: "UserName",
                principalTable: "Users",
                principalColumn: "UserName",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
