using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace enumerable_to_queryable_linq.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMainDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DocumentStatus",
                table: "MainDocuments",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentStatus",
                table: "MainDocuments");
        }
    }
}
