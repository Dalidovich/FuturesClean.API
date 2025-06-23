using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FuturesClean.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "futures_difference",
                columns: table => new
                {
                    pk_futures_difference_id = table.Column<Guid>(type: "uuid", nullable: false),
                    time_measured_utc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    interval = table.Column<string>(type: "character varying", nullable: false),
                    symbol_current = table.Column<string>(type: "character varying", nullable: false),
                    symbol_next = table.Column<string>(type: "character varying", nullable: false),
                    spread = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_futures_difference", x => x.pk_futures_difference_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "futures_difference");
        }
    }
}
