using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace enumerable_to_queryable_linq.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDocumentAs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDocumentAs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDocumentAs_MainDocuments_MainDocumentId",
                        column: x => x.MainDocumentId,
                        principalTable: "MainDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionDocumentBs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MainDocumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    DocumentNumber = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    DocumentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Details = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionDocumentBs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionDocumentBs_MainDocuments_MainDocumentId",
                        column: x => x.MainDocumentId,
                        principalTable: "MainDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocumentAs_MainDocumentId",
                table: "TransactionDocumentAs",
                column: "MainDocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionDocumentBs_MainDocumentId",
                table: "TransactionDocumentBs",
                column: "MainDocumentId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionDocumentAs");

            migrationBuilder.DropTable(
                name: "TransactionDocumentBs");

            migrationBuilder.DropTable(
                name: "MainDocuments");
        }
    }
}
