﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MiroBello.Data;
using System;

namespace MiroBello.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180522181839_AddTotalPricePerProductAndOthers")]
    partial class AddTotalPricePerProductAndOthers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("MiroBello.Models.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientAccountId");

                    b.Property<string>("DeliveryType");

                    b.Property<DateTime>("OrderPlacementDate");

                    b.Property<string>("OrderStatus");

                    b.Property<string>("PaymentModality");

                    b.Property<double>("TotalPrice");

                    b.HasKey("BillId");

                    b.HasIndex("ClientAccountId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("MiroBello.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MiroBello.Models.ClientAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<int?>("ClientCartId");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("PhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("ClientCartId")
                        .IsUnique();

                    b.ToTable("ClientAccounts");
                });

            modelBuilder.Entity("MiroBello.Models.ClientCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientAccoundId");

                    b.Property<double?>("TotalPriceOfCartForUser");

                    b.HasKey("Id");

                    b.ToTable("ClientCart");
                });

            modelBuilder.Entity("MiroBello.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Currency");

                    b.Property<string>("Details");

                    b.Property<string>("ImageURL");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MiroBello.Models.ProductsOnBills", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BillId");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsOnBill");
                });

            modelBuilder.Entity("MiroBello.Models.ProductsOnCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ClientCartId");

                    b.Property<int?>("ProductId");

                    b.Property<double>("Quantity");

                    b.Property<double?>("TotalPricePerProduct");

                    b.HasKey("Id");

                    b.HasIndex("ClientCartId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductsOnCart");
                });

            modelBuilder.Entity("MiroBello.Models.Bill", b =>
                {
                    b.HasOne("MiroBello.Models.ClientAccount", "Client")
                        .WithMany()
                        .HasForeignKey("ClientAccountId");
                });

            modelBuilder.Entity("MiroBello.Models.ClientAccount", b =>
                {
                    b.HasOne("MiroBello.Models.ClientCart", "Cart")
                        .WithOne("Client")
                        .HasForeignKey("MiroBello.Models.ClientAccount", "ClientCartId");
                });

            modelBuilder.Entity("MiroBello.Models.Product", b =>
                {
                    b.HasOne("MiroBello.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("MiroBello.Models.ProductsOnBills", b =>
                {
                    b.HasOne("MiroBello.Models.Bill", "Bill")
                        .WithMany("Products")
                        .HasForeignKey("BillId");

                    b.HasOne("MiroBello.Models.Product", "Product")
                        .WithMany("Bills")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("MiroBello.Models.ProductsOnCart", b =>
                {
                    b.HasOne("MiroBello.Models.ClientCart", "ClientCart")
                        .WithMany("Products")
                        .HasForeignKey("ClientCartId");

                    b.HasOne("MiroBello.Models.Product", "Product")
                        .WithMany("Carts")
                        .HasForeignKey("ProductId");
                });
#pragma warning restore 612, 618
        }
    }
}
