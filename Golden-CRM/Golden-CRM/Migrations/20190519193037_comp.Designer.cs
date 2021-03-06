﻿// <auto-generated />
using System;
using Golden_CRM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Golden_CRM.Migrations
{
    [DbContext(typeof(GoldenDbContext))]
    [Migration("20190519193037_comp")]
    partial class comp
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Golden_CRM.Models.Customer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedOwner");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastVisited");

                    b.Property<string>("PhoneNumber");

                    b.Property<int>("Priority");

                    b.HasKey("ID");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "jd@doe.com",
                            FirstName = "John",
                            LastName = "Doe",
                            LastVisited = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "555-555-5555",
                            Priority = 0
                        });
                });

            modelBuilder.Entity("Golden_CRM.Models.Note", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(max)");

                    b.Property<int>("ContactType");

                    b.Property<int>("CustomerID");

                    b.Property<DateTime>("Date");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("Golden_CRM.Models.ToDo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedOwner");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(max)");

                    b.Property<bool>("Complete");

                    b.Property<DateTime>("CompletedDate");

                    b.Property<int>("ContactType");

                    b.Property<int>("CustomerID");

                    b.Property<string>("CustomerName");

                    b.Property<DateTime>("DueDate");

                    b.Property<string>("UserID");

                    b.HasKey("ID");

                    b.ToTable("ToDos");
                });

            modelBuilder.Entity("Golden_CRM.Models.Note", b =>
                {
                    b.HasOne("Golden_CRM.Models.Customer")
                        .WithMany("Notes")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
