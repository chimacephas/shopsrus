﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopsRUs.Persistence.Data;

namespace ShopsRUs.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ShopsRUs.Domain.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAnAffliate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAnEmployee")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e59c73c3-cfca-42e0-8e5a-59efcc2eba8e"),
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 877, DateTimeKind.Local).AddTicks(9515),
                            IsAnAffliate = true,
                            IsAnEmployee = false,
                            Name = "John"
                        },
                        new
                        {
                            Id = new Guid("a136f64e-f9ac-46fb-8db8-cbef60af30cd"),
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8715),
                            IsAnAffliate = false,
                            IsAnEmployee = true,
                            Name = "Alice"
                        },
                        new
                        {
                            Id = new Guid("a50a1f19-4d5d-490f-8083-f63a23856f5b"),
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8732),
                            IsAnAffliate = false,
                            IsAnEmployee = false,
                            Name = "Doe"
                        },
                        new
                        {
                            Id = new Guid("47a1ee21-2409-476d-8cc9-6c1af7431ebd"),
                            CreatedAt = new DateTime(2018, 2, 11, 15, 28, 10, 878, DateTimeKind.Local).AddTicks(8735),
                            IsAnAffliate = false,
                            IsAnEmployee = false,
                            Name = "Max"
                        });
                });

            modelBuilder.Entity("ShopsRUs.Domain.Entities.Discount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Percentage")
                        .HasColumnType("REAL");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Discounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("babeeb39-ef8d-464a-aae7-cdcb511bf75d"),
                            Amount = 0m,
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4595),
                            Name = "Affliate Discount",
                            Percentage = 10.0,
                            Type = "affliatediscount"
                        },
                        new
                        {
                            Id = new Guid("25489664-525c-4540-906d-54b86056b4d6"),
                            Amount = 0m,
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4622),
                            Name = "Employee Discount",
                            Percentage = 30.0,
                            Type = "employeediscount"
                        },
                        new
                        {
                            Id = new Guid("bcc7c913-1233-4136-9abd-1dd88d647d0a"),
                            Amount = 0m,
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4625),
                            Name = "Old Customer Discount",
                            Percentage = 5.0,
                            Type = "oldCustomerdiscount"
                        },
                        new
                        {
                            Id = new Guid("c8847be1-1206-4131-ac3a-bfc9ae516f14"),
                            Amount = 0m,
                            CreatedAt = new DateTime(2021, 2, 11, 15, 28, 10, 880, DateTimeKind.Local).AddTicks(4627),
                            Name = "Percent Per $100 Bill Discount",
                            Percentage = 5.0,
                            Type = "PercentPer100DollarDiscount"
                        });
                });

            modelBuilder.Entity("ShopsRUs.Domain.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ShopsRUs.Domain.Entities.Invoice", b =>
                {
                    b.HasOne("ShopsRUs.Domain.Entities.Customer", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ShopsRUs.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
