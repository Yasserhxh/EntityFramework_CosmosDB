﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repository.Data;

namespace Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220621093231_CosmosDbMigration")]
    partial class CosmosDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Authentication.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Cosmos:PropertyName", "_etag");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Domain.Entities.Declaration", b =>
                {
                    b.Property<int>("Declaration_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Declaration_Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Declaration_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Declaration_DateValidation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Declaration_Localisation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_LongLat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Validateur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Declaration_Ville")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Declaration_ID");

                    b.ToTable("declarations");

                    b
                        .HasAnnotation("Cosmos:ContainerName", "Declaration");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Cosmos:PropertyName", "_etag");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");
                });

            modelBuilder.Entity("Domain.Entities.Declaration", b =>
                {
                    b.OwnsMany("Domain.Entities.Intervention", "Interventions", b1 =>
                        {
                            b1.Property<int>("Intervention_ID")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Declaration_ID")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Commentaire")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Date")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_DeclarationID")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Equipe")
                                .HasColumnType("int");

                            b1.Property<int>("Intervention_Resultat")
                                .HasColumnType("int");

                            b1.HasKey("Intervention_ID");

                            b1.HasIndex("Declaration_ID");

                            b1.ToTable("Intervention");

                            b1.WithOwner("Declaration")
                                .HasForeignKey("Declaration_ID");

                            b1.Navigation("Declaration");
                        });

                    b.Navigation("Interventions");
                });
#pragma warning restore 612, 618
        }
    }
}
