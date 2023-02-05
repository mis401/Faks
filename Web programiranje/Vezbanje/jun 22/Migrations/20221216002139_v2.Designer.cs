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
    [Migration("20221216002139_v2")]
    partial class v2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Automobil", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BojaID")
                        .HasColumnType("int");

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<int?>("MarkaID")
                        .HasColumnType("int");

                    b.Property<int?>("ModelID")
                        .HasColumnType("int");

                    b.Property<int?>("ProdavnicaID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BojaID");

                    b.HasIndex("MarkaID");

                    b.HasIndex("ModelID");

                    b.HasIndex("ProdavnicaID");

                    b.ToTable("Automobili");
                });

            modelBuilder.Entity("Boja", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Boje");
                });

            modelBuilder.Entity("Marka", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Marke");
                });

            modelBuilder.Entity("Model", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DatumProdaje")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naziv")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Modeli");
                });

            modelBuilder.Entity("Prodavnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Prodavnice");
                });

            modelBuilder.Entity("Automobil", b =>
                {
                    b.HasOne("Boja", "Boja")
                        .WithMany("Automobil")
                        .HasForeignKey("BojaID");

                    b.HasOne("Marka", "Marka")
                        .WithMany("Automobil")
                        .HasForeignKey("MarkaID");

                    b.HasOne("Model", "Model")
                        .WithMany("Automobil")
                        .HasForeignKey("ModelID");

                    b.HasOne("Prodavnica", "Prodavnica")
                        .WithMany("Automobil")
                        .HasForeignKey("ProdavnicaID");

                    b.Navigation("Boja");

                    b.Navigation("Marka");

                    b.Navigation("Model");

                    b.Navigation("Prodavnica");
                });

            modelBuilder.Entity("Boja", b =>
                {
                    b.Navigation("Automobil");
                });

            modelBuilder.Entity("Marka", b =>
                {
                    b.Navigation("Automobil");
                });

            modelBuilder.Entity("Model", b =>
                {
                    b.Navigation("Automobil");
                });

            modelBuilder.Entity("Prodavnica", b =>
                {
                    b.Navigation("Automobil");
                });
#pragma warning restore 612, 618
        }
    }
}