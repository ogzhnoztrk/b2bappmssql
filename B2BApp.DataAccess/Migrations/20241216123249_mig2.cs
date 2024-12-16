using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B2BApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sstk_",
                table: "TBL_SUBE_STOK",
                newName: "sstk_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sstk_id",
                table: "TBL_SUBE_STOK",
                newName: "sstk_");
        }
    }
}
