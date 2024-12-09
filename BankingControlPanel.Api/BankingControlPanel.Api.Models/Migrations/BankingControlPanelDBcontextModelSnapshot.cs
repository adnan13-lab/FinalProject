﻿// <auto-generated />
using BankingControlPanel.Api.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankingControlPanel.Api.Models.Migrations
{
    [DbContext(typeof(BankingControlPanelDBcontext))]
    partial class BankingControlPanelDBcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountId");

                    b.HasIndex("ClientId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("AddressId");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(59)
                        .HasColumnType("nvarchar(59)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(59)
                        .HasColumnType("nvarchar(59)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalId")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("ProfilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Search", b =>
                {
                    b.Property<int>("SeachId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeachId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(59)
                        .HasColumnType("nvarchar(59)");

                    b.HasKey("SeachId");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("Searches");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Account", b =>
                {
                    b.HasOne("BankingControlPanel.Api.Models.Models.Client", "Client")
                        .WithMany("Account")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Address", b =>
                {
                    b.HasOne("BankingControlPanel.Api.Models.Models.Client", "Client")
                        .WithOne("Address")
                        .HasForeignKey("BankingControlPanel.Api.Models.Models.Address", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Search", b =>
                {
                    b.HasOne("BankingControlPanel.Api.Models.Models.Client", "Client")
                        .WithOne("Search")
                        .HasForeignKey("BankingControlPanel.Api.Models.Models.Search", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankingControlPanel.Api.Models.Models.Client", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("Address");

                    b.Navigation("Search");
                });
#pragma warning restore 612, 618
        }
    }
}
