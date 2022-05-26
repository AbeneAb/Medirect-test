using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exchange.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "exchange");

            migrationBuilder.CreateSequence(
                name: "accountseq",
                schema: "exchange",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "transactionseq",
                schema: "exchange",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "exchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Currency_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency_Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyType",
                schema: "exchange",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionStatus",
                schema: "exchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "transactions",
                schema: "exchange",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FromCurrency_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromCurrency_Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCurrency_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCurrency_Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    _buyerId = table.Column<int>(type: "int", nullable: false),
                    Converted = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExchangeRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_transactions_Accounts__buyerId",
                        column: x => x._buyerId,
                        principalSchema: "exchange",
                        principalTable: "Accounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_transactions_TransactionStatus_TransactionStatus",
                        column: x => x.TransactionStatus,
                        principalSchema: "exchange",
                        principalTable: "TransactionStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transactions__buyerId",
                schema: "exchange",
                table: "transactions",
                column: "_buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_transactions_TransactionStatus",
                schema: "exchange",
                table: "transactions",
                column: "TransactionStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyType",
                schema: "exchange");

            migrationBuilder.DropTable(
                name: "transactions",
                schema: "exchange");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "exchange");

            migrationBuilder.DropTable(
                name: "TransactionStatus",
                schema: "exchange");

            migrationBuilder.DropSequence(
                name: "accountseq",
                schema: "exchange");

            migrationBuilder.DropSequence(
                name: "transactionseq",
                schema: "exchange");
        }
    }
}
