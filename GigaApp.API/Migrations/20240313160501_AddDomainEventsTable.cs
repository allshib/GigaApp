using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaApp.API.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DomainEvents",
                columns: table => new
                {
                    DomainEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmittedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ContentBlob = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DomainEvents", x => x.DomainEventId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DomainEvents");
        }
    }
}
