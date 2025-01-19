using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace library_be.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masterbuku",
                columns: table => new
                {
                    IDBUKU = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JUDUL = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PENGARANG = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PENERBIT = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TAHUNTERBIT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masterbuku", x => x.IDBUKU);
                });

            migrationBuilder.CreateTable(
                name: "Mastermahasiswa",
                columns: table => new
                {
                    NIM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NAMA = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FAKULTAS = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JURUSAN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mastermahasiswa", x => x.NIM);
                });

            migrationBuilder.CreateTable(
                name: "Inventorybuku",
                columns: table => new
                {
                    IDSTOK = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDBUKU = table.Column<long>(type: "bigint", nullable: false),
                    LOKASIRAK = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JUMLAHSTOK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventorybuku", x => x.IDSTOK);
                    table.ForeignKey(
                        name: "FK_Inventorybuku_Masterbuku_IDBUKU",
                        column: x => x.IDBUKU,
                        principalTable: "Masterbuku",
                        principalColumn: "IDBUKU",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaksipeminjaman",
                columns: table => new
                {
                    IDTRANSAKSI = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TANGGALPINJAM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TANGGALKEMBALI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IDBUKU = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaksipeminjaman", x => x.IDTRANSAKSI);
                    table.ForeignKey(
                        name: "FK_Transaksipeminjaman_Mastermahasiswa_NIM",
                        column: x => x.NIM,
                        principalTable: "Mastermahasiswa",
                        principalColumn: "NIM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Historypeminjaman",
                columns: table => new
                {
                    IDHISTORY = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDTRANSAKSI = table.Column<long>(type: "bigint", nullable: true),
                    NIM = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IDBUKU = table.Column<long>(type: "bigint", nullable: false),
                    TANGGALPINJAM = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TANGGALKEMBALI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LAMAPINJAM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historypeminjaman", x => x.IDHISTORY);
                    table.ForeignKey(
                        name: "FK_Historypeminjaman_Masterbuku_IDBUKU",
                        column: x => x.IDBUKU,
                        principalTable: "Masterbuku",
                        principalColumn: "IDBUKU");
                    table.ForeignKey(
                        name: "FK_Historypeminjaman_Mastermahasiswa_NIM",
                        column: x => x.NIM,
                        principalTable: "Mastermahasiswa",
                        principalColumn: "NIM");
                    table.ForeignKey(
                        name: "FK_Historypeminjaman_Transaksipeminjaman_IDTRANSAKSI",
                        column: x => x.IDTRANSAKSI,
                        principalTable: "Transaksipeminjaman",
                        principalColumn: "IDTRANSAKSI",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Historypeminjaman_IDBUKU",
                table: "Historypeminjaman",
                column: "IDBUKU");

            migrationBuilder.CreateIndex(
                name: "IX_Historypeminjaman_IDTRANSAKSI",
                table: "Historypeminjaman",
                column: "IDTRANSAKSI");

            migrationBuilder.CreateIndex(
                name: "IX_Historypeminjaman_NIM",
                table: "Historypeminjaman",
                column: "NIM");

            migrationBuilder.CreateIndex(
                name: "IX_Inventorybuku_IDBUKU",
                table: "Inventorybuku",
                column: "IDBUKU");

            migrationBuilder.CreateIndex(
                name: "IX_Transaksipeminjaman_NIM",
                table: "Transaksipeminjaman",
                column: "NIM");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Historypeminjaman");

            migrationBuilder.DropTable(
                name: "Inventorybuku");

            migrationBuilder.DropTable(
                name: "Transaksipeminjaman");

            migrationBuilder.DropTable(
                name: "Masterbuku");

            migrationBuilder.DropTable(
                name: "Mastermahasiswa");
        }
    }
}
