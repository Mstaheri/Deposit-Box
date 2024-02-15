using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditNameBankSafe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBox",
                table: "BankSafeDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBox",
                table: "BankSafeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSharePrices_BankSafes_NameBox",
                table: "UserSharePrices");

            migrationBuilder.RenameColumn(
                name: "NameBox",
                table: "UserSharePrices",
                newName: "NameBankSafe");

            migrationBuilder.RenameIndex(
                name: "IX_UserSharePrices_NameBox",
                table: "UserSharePrices",
                newName: "IX_UserSharePrices_NameBankSafe");

            migrationBuilder.RenameColumn(
                name: "NameBox",
                table: "BankSafeTransactions",
                newName: "NameBankSafe");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeTransactions_NameBox",
                table: "BankSafeTransactions",
                newName: "IX_BankSafeTransactions_NameBankSafe");

            migrationBuilder.RenameColumn(
                name: "NameBox",
                table: "BankSafeDocument",
                newName: "NameBankSafe");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocument_NameBox",
                table: "BankSafeDocument",
                newName: "IX_BankSafeDocument_NameBankSafe");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBankSafe",
                table: "BankSafeDocument",
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
                name: "FK_UserSharePrices_BankSafes_NameBankSafe",
                table: "UserSharePrices",
                column: "NameBankSafe",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBankSafe",
                table: "BankSafeDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBankSafe",
                table: "BankSafeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSharePrices_BankSafes_NameBankSafe",
                table: "UserSharePrices");

            migrationBuilder.RenameColumn(
                name: "NameBankSafe",
                table: "UserSharePrices",
                newName: "NameBox");

            migrationBuilder.RenameIndex(
                name: "IX_UserSharePrices_NameBankSafe",
                table: "UserSharePrices",
                newName: "IX_UserSharePrices_NameBox");

            migrationBuilder.RenameColumn(
                name: "NameBankSafe",
                table: "BankSafeTransactions",
                newName: "NameBox");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeTransactions_NameBankSafe",
                table: "BankSafeTransactions",
                newName: "IX_BankSafeTransactions_NameBox");

            migrationBuilder.RenameColumn(
                name: "NameBankSafe",
                table: "BankSafeDocument",
                newName: "NameBox");

            migrationBuilder.RenameIndex(
                name: "IX_BankSafeDocument_NameBankSafe",
                table: "BankSafeDocument",
                newName: "IX_BankSafeDocument_NameBox");

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeDocument_BankSafes_NameBox",
                table: "BankSafeDocument",
                column: "NameBox",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BankSafeTransactions_BankSafes_NameBox",
                table: "BankSafeTransactions",
                column: "NameBox",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSharePrices_BankSafes_NameBox",
                table: "UserSharePrices",
                column: "NameBox",
                principalTable: "BankSafes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
