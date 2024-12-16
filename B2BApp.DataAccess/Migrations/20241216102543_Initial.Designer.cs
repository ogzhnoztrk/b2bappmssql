﻿// <auto-generated />
using System;
using B2BApp.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace B2BApp.DataAccess.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20241216102543_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("B2BApp.Entities.Concrete.Firma", b =>
                {
                    b.Property<Guid>("FirmaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("firm_id");

                    b.Property<string>("FirmaAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firm_adi");

                    b.Property<string>("FirmaTel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("firm_tel");

                    b.HasKey("FirmaId");

                    b.ToTable("TBL_FIRMA_TANIM");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Kategori", b =>
                {
                    b.Property<Guid>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ktgr_id");

                    b.Property<string>("KategoriAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ktgr_adi");

                    b.HasKey("KategoriId");

                    b.ToTable("TBL_KATEGORI_TANIM");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Kullanici", b =>
                {
                    b.Property<Guid>("KullaniciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("klnc_id");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("klnc_adi");

                    b.Property<byte[]>("SifreHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("klnc_sifre_hash");

                    b.Property<byte[]>("SifreSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("klnc_sifre_salt");

                    b.Property<Guid>("TedarikciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tdrk_id");

                    b.HasKey("KullaniciId");

                    b.HasIndex("TedarikciId");

                    b.ToTable("TBL_KULLANICI_TANIM");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Satis", b =>
                {
                    b.Property<int>("SatisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("sats_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SatisId"));

                    b.Property<double>("SatisMiktari")
                        .HasColumnType("float")
                        .HasColumnName("sats_miktari");

                    b.Property<DateTime>("SatisTarihi")
                        .HasColumnType("datetime2")
                        .HasColumnName("sats_tarihi");

                    b.Property<Guid>("SubeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sube_id");

                    b.Property<double>("Toplam")
                        .HasColumnType("float")
                        .HasColumnName("sats_toplam");

                    b.Property<Guid>("UrunId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("urun_id");

                    b.HasKey("SatisId");

                    b.HasIndex("SubeId");

                    b.HasIndex("UrunId");

                    b.ToTable("TBL_SATIS");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Siparis", b =>
                {
                    b.Property<int>("SiparisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("sprs_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SiparisId"));

                    b.Property<double>("Adet")
                        .HasColumnType("float")
                        .HasColumnName("sprs_adet");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("sprs_is_active");

                    b.Property<DateTime>("SiparisTarih")
                        .HasColumnType("datetime2")
                        .HasColumnName("sprs_tarih");

                    b.Property<Guid>("SubeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sube_id");

                    b.Property<Guid>("TedarikciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tdrk_id");

                    b.Property<double>("Toplam")
                        .HasColumnType("float")
                        .HasColumnName("sprs_toplam");

                    b.Property<Guid>("UrunId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("urun_id");

                    b.HasKey("SiparisId");

                    b.HasIndex("SubeId");

                    b.HasIndex("TedarikciId");

                    b.HasIndex("UrunId");

                    b.ToTable("TBL_SIPARIS");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Sube", b =>
                {
                    b.Property<Guid>("SubeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sube_id");

                    b.Property<Guid>("FirmaId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("firm_id");

                    b.Property<string>("SubeAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sube_adi");

                    b.Property<string>("SubeTel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("sube_tel");

                    b.HasKey("SubeId");

                    b.HasIndex("FirmaId");

                    b.ToTable("TBL_SUBE_TANIM");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.SubeStok", b =>
                {
                    b.Property<Guid>("SubeStokId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sstk_");

                    b.Property<double>("Stok")
                        .HasColumnType("float")
                        .HasColumnName("sstk_stok_adet");

                    b.Property<Guid>("SubeId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("sube_id");

                    b.Property<Guid>("UrunId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("urun_id");

                    b.HasKey("SubeStokId");

                    b.HasIndex("SubeId");

                    b.HasIndex("UrunId");

                    b.ToTable("TBL_SUBE_STOK");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Tedarikci", b =>
                {
                    b.Property<Guid>("TedarikciId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tdrk_id");

                    b.Property<string>("TedarikciAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tdrk_adi");

                    b.Property<string>("TedarikciTel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tdrk_tel");

                    b.HasKey("TedarikciId");

                    b.ToTable("TBL_TEDARIKCILER");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Urun", b =>
                {
                    b.Property<Guid>("UrunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("urun_id");

                    b.Property<double>("Fiyat")
                        .HasColumnType("float")
                        .HasColumnName("urun_fiyat");

                    b.Property<Guid>("KategoriId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ktgr_id");

                    b.Property<double?>("SatisFiyati")
                        .HasColumnType("float")
                        .HasColumnName("urun_satis_fiyat");

                    b.Property<Guid>("TedarikciId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("tdrk_id");

                    b.Property<string>("UrunAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("urun_adi");

                    b.HasKey("UrunId");

                    b.HasIndex("KategoriId");

                    b.HasIndex("TedarikciId");

                    b.ToTable("TBL_URUN_TANIM");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Kullanici", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Tedarikci", "Tedarikci")
                        .WithMany()
                        .HasForeignKey("TedarikciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tedarikci");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Satis", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Sube", "Sube")
                        .WithMany()
                        .HasForeignKey("SubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B2BApp.Entities.Concrete.Urun", "Urun")
                        .WithMany()
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sube");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Siparis", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Sube", "Sube")
                        .WithMany()
                        .HasForeignKey("SubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B2BApp.Entities.Concrete.Tedarikci", "Tedarikci")
                        .WithMany()
                        .HasForeignKey("TedarikciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B2BApp.Entities.Concrete.Urun", "Urun")
                        .WithMany()
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sube");

                    b.Navigation("Tedarikci");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Sube", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Firma", "Firma")
                        .WithMany()
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firma");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.SubeStok", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Sube", "Sube")
                        .WithMany()
                        .HasForeignKey("SubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B2BApp.Entities.Concrete.Urun", "Urun")
                        .WithMany()
                        .HasForeignKey("UrunId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sube");

                    b.Navigation("Urun");
                });

            modelBuilder.Entity("B2BApp.Entities.Concrete.Urun", b =>
                {
                    b.HasOne("B2BApp.Entities.Concrete.Kategori", "Kategori")
                        .WithMany()
                        .HasForeignKey("KategoriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("B2BApp.Entities.Concrete.Tedarikci", "Tedarikci")
                        .WithMany()
                        .HasForeignKey("TedarikciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");

                    b.Navigation("Tedarikci");
                });
#pragma warning restore 612, 618
        }
    }
}
