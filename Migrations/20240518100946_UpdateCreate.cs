using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_be.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historypeminjaman_Transaksipeminjaman_IDTRANSAKSI",
                table: "Historypeminjaman");

            migrationBuilder.AddForeignKey(
                name: "FK_Historypeminjaman_Transaksipeminjaman_IDTRANSAKSI",
                table: "Historypeminjaman",
                column: "IDTRANSAKSI",
                principalTable: "Transaksipeminjaman",
                principalColumn: "IDTRANSAKSI",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Historypeminjaman_Transaksipeminjaman_IDTRANSAKSI",
                table: "Historypeminjaman");

            migrationBuilder.AddForeignKey(
                name: "FK_Historypeminjaman_Transaksipeminjaman_IDTRANSAKSI",
                table: "Historypeminjaman",
                column: "IDTRANSAKSI",
                principalTable: "Transaksipeminjaman",
                principalColumn: "IDTRANSAKSI",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
