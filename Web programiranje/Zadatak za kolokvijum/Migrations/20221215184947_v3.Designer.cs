﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    [Migration("20221215184947_v3")]
    partial class v3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Film", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("KategorijaID")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProdukcijskaKucaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("KategorijaID");

                    b.HasIndex("ProdukcijskaKucaID");

                    b.ToTable("Filmovi");
                });

            modelBuilder.Entity("Kategorija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("KategorijaProdukcijskaKuca", b =>
                {
                    b.Property<int>("Kategorija")
                        .HasColumnType("int");

                    b.Property<int>("ProdukcijskaKuca")
                        .HasColumnType("int");

                    b.ToTable("KatProdKuca");
                });

            modelBuilder.Entity("Ocena", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BrojOcena")
                        .HasColumnType("int");

                    b.Property<int?>("FilmID")
                        .HasColumnType("int");

                    b.Property<int>("Suma")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("FilmID");

                    b.ToTable("Ocene");
                });

            modelBuilder.Entity("ProdukcijskaKuca", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("ProdukcijskeKuce");
                });

            modelBuilder.Entity("Film", b =>
                {
                    b.HasOne("Kategorija", "Kategorija")
                        .WithMany("ListaFilmova")
                        .HasForeignKey("KategorijaID");

                    b.HasOne("ProdukcijskaKuca", "ProdukcijskaKuca")
                        .WithMany("ListaFilmova")
                        .HasForeignKey("ProdukcijskaKucaID");

                    b.Navigation("Kategorija");

                    b.Navigation("ProdukcijskaKuca");
                });

            modelBuilder.Entity("Ocena", b =>
                {
                    b.HasOne("Film", "Film")
                        .WithMany()
                        .HasForeignKey("FilmID");

                    b.Navigation("Film");
                });

            modelBuilder.Entity("Kategorija", b =>
                {
                    b.Navigation("ListaFilmova");
                });

            modelBuilder.Entity("ProdukcijskaKuca", b =>
                {
                    b.Navigation("ListaFilmova");
                });
#pragma warning restore 612, 618
        }
    }
}
