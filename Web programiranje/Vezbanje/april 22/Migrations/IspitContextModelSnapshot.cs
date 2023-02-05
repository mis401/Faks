﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

#nullable disable

namespace WebTemplate.Migrations
{
    [DbContext(typeof(IspitContext))]
    partial class IspitContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artikal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("BrendID")
                        .HasColumnType("int");

                    b.Property<double>("Cena")
                        .HasColumnType("float");

                    b.Property<string>("Naziv")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<int?>("ProdavnicaID")
                        .HasColumnType("int");

                    b.Property<string>("Velicina")
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("ID");

                    b.HasIndex("BrendID");

                    b.HasIndex("ProdavnicaID");

                    b.ToTable("Artikli");
                });

            modelBuilder.Entity("Brend", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Brendovi");
                });

            modelBuilder.Entity("Prodavnica", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Naziv")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("ID");

                    b.ToTable("Prodavnice");
                });

            modelBuilder.Entity("Artikal", b =>
                {
                    b.HasOne("Brend", "Brend")
                        .WithMany("Artikal")
                        .HasForeignKey("BrendID");

                    b.HasOne("Prodavnica", "Prodavnica")
                        .WithMany("Artikal")
                        .HasForeignKey("ProdavnicaID");

                    b.Navigation("Brend");

                    b.Navigation("Prodavnica");
                });

            modelBuilder.Entity("Brend", b =>
                {
                    b.Navigation("Artikal");
                });

            modelBuilder.Entity("Prodavnica", b =>
                {
                    b.Navigation("Artikal");
                });
#pragma warning restore 612, 618
        }
    }
}
