﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using enumerable_to_queryable_linq.Models;

#nullable disable

namespace enumerable_to_queryable_linq.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.MainDocument", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.HasKey("Id");

                    b.ToTable("MainDocuments");
                });

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.TransactionDocumentA", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<Guid>("MainDocumentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MainDocumentId")
                        .IsUnique();

                    b.ToTable("TransactionDocumentAs");
                });

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.TransactionDocumentB", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime>("DocumentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DocumentNumber")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("character varying(6)");

                    b.Property<Guid>("MainDocumentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MainDocumentId")
                        .IsUnique();

                    b.ToTable("TransactionDocumentBs");
                });

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.TransactionDocumentA", b =>
                {
                    b.HasOne("enumerable_to_queryable_linq.Models.MainDocument", "MainDocument")
                        .WithOne("TransactionDocumentA")
                        .HasForeignKey("enumerable_to_queryable_linq.Models.TransactionDocumentA", "MainDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainDocument");
                });

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.TransactionDocumentB", b =>
                {
                    b.HasOne("enumerable_to_queryable_linq.Models.MainDocument", "MainDocument")
                        .WithOne("TransactionDocumentB")
                        .HasForeignKey("enumerable_to_queryable_linq.Models.TransactionDocumentB", "MainDocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MainDocument");
                });

            modelBuilder.Entity("enumerable_to_queryable_linq.Models.MainDocument", b =>
                {
                    b.Navigation("TransactionDocumentA");

                    b.Navigation("TransactionDocumentB");
                });
#pragma warning restore 612, 618
        }
    }
}
