using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B2BApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_FIRMA_TANIM",
                columns: table => new
                {
                    firm_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firm_adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firm_tel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_FIRMA_TANIM", x => x.firm_id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_KATEGORI_TANIM",
                columns: table => new
                {
                    ktgr_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ktgr_adi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_KATEGORI_TANIM", x => x.ktgr_id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TEDARIKCILER",
                columns: table => new
                {
                    tdrk_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tdrk_adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tdrk_tel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TEDARIKCILER", x => x.tdrk_id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SUBE_TANIM",
                columns: table => new
                {
                    sube_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    firm_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sube_adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sube_tel = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SUBE_TANIM", x => x.sube_id);
                    table.ForeignKey(
                        name: "FK_TBL_SUBE_TANIM_TBL_FIRMA_TANIM_firm_id",
                        column: x => x.firm_id,
                        principalTable: "TBL_FIRMA_TANIM",
                        principalColumn: "firm_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_KULLANICI_TANIM",
                columns: table => new
                {
                    klnc_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    klnc_adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tdrk_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    klnc_sifre_salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    klnc_sifre_hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_KULLANICI_TANIM", x => x.klnc_id);
                    table.ForeignKey(
                        name: "FK_TBL_KULLANICI_TANIM_TBL_TEDARIKCILER_tdrk_id",
                        column: x => x.tdrk_id,
                        principalTable: "TBL_TEDARIKCILER",
                        principalColumn: "tdrk_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_URUN_TANIM",
                columns: table => new
                {
                    urun_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ktgr_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urun_adi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tdrk_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urun_fiyat = table.Column<double>(type: "float", nullable: false),
                    urun_satis_fiyat = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_URUN_TANIM", x => x.urun_id);
                    table.ForeignKey(
                        name: "FK_TBL_URUN_TANIM_TBL_KATEGORI_TANIM_ktgr_id",
                        column: x => x.ktgr_id,
                        principalTable: "TBL_KATEGORI_TANIM",
                        principalColumn: "ktgr_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_URUN_TANIM_TBL_TEDARIKCILER_tdrk_id",
                        column: x => x.tdrk_id,
                        principalTable: "TBL_TEDARIKCILER",
                        principalColumn: "tdrk_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SATIS",
                columns: table => new
                {
                    sats_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sube_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urun_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sats_miktari = table.Column<double>(type: "float", nullable: false),
                    sats_tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sats_toplam = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SATIS", x => x.sats_id);
                    table.ForeignKey(
                        name: "FK_TBL_SATIS_TBL_SUBE_TANIM_sube_id",
                        column: x => x.sube_id,
                        principalTable: "TBL_SUBE_TANIM",
                        principalColumn: "sube_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_SATIS_TBL_URUN_TANIM_urun_id",
                        column: x => x.urun_id,
                        principalTable: "TBL_URUN_TANIM",
                        principalColumn: "urun_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SIPARIS",
                columns: table => new
                {
                    sprs_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sube_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urun_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tdrk_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sprs_adet = table.Column<double>(type: "float", nullable: false),
                    sprs_toplam = table.Column<double>(type: "float", nullable: false),
                    sprs_tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sprs_is_active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SIPARIS", x => x.sprs_id);
                    table.ForeignKey(
                        name: "FK_TBL_SIPARIS_TBL_SUBE_TANIM_sube_id",
                        column: x => x.sube_id,
                        principalTable: "TBL_SUBE_TANIM",
                        principalColumn: "sube_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_SIPARIS_TBL_TEDARIKCILER_tdrk_id",
                        column: x => x.tdrk_id,
                        principalTable: "TBL_TEDARIKCILER",
                        principalColumn: "tdrk_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_SIPARIS_TBL_URUN_TANIM_urun_id",
                        column: x => x.urun_id,
                        principalTable: "TBL_URUN_TANIM",
                        principalColumn: "urun_id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SUBE_STOK",
                columns: table => new
                {
                    sstk_ = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sube_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    urun_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sstk_stok_adet = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SUBE_STOK", x => x.sstk_);
                    table.ForeignKey(
                        name: "FK_TBL_SUBE_STOK_TBL_SUBE_TANIM_sube_id",
                        column: x => x.sube_id,
                        principalTable: "TBL_SUBE_TANIM",
                        principalColumn: "sube_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_SUBE_STOK_TBL_URUN_TANIM_urun_id",
                        column: x => x.urun_id,
                        principalTable: "TBL_URUN_TANIM",
                        principalColumn: "urun_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_KULLANICI_TANIM_tdrk_id",
                table: "TBL_KULLANICI_TANIM",
                column: "tdrk_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SATIS_sube_id",
                table: "TBL_SATIS",
                column: "sube_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SATIS_urun_id",
                table: "TBL_SATIS",
                column: "urun_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SIPARIS_sube_id",
                table: "TBL_SIPARIS",
                column: "sube_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SIPARIS_tdrk_id",
                table: "TBL_SIPARIS",
                column: "tdrk_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SIPARIS_urun_id",
                table: "TBL_SIPARIS",
                column: "urun_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUBE_STOK_sube_id",
                table: "TBL_SUBE_STOK",
                column: "sube_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUBE_STOK_urun_id",
                table: "TBL_SUBE_STOK",
                column: "urun_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUBE_TANIM_firm_id",
                table: "TBL_SUBE_TANIM",
                column: "firm_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_URUN_TANIM_ktgr_id",
                table: "TBL_URUN_TANIM",
                column: "ktgr_id");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_URUN_TANIM_tdrk_id",
                table: "TBL_URUN_TANIM",
                column: "tdrk_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_KULLANICI_TANIM");

            migrationBuilder.DropTable(
                name: "TBL_SATIS");

            migrationBuilder.DropTable(
                name: "TBL_SIPARIS");

            migrationBuilder.DropTable(
                name: "TBL_SUBE_STOK");

            migrationBuilder.DropTable(
                name: "TBL_SUBE_TANIM");

            migrationBuilder.DropTable(
                name: "TBL_URUN_TANIM");

            migrationBuilder.DropTable(
                name: "TBL_FIRMA_TANIM");

            migrationBuilder.DropTable(
                name: "TBL_KATEGORI_TANIM");

            migrationBuilder.DropTable(
                name: "TBL_TEDARIKCILER");
        }
    }
}
