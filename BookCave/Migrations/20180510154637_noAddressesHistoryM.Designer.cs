﻿// <auto-generated />
using BookCave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BookCave.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180510154637_noAddressesHistoryM")]
    partial class noAddressesHistoryM
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookCave.Data.EntityModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int>("HouseNum");

                    b.Property<string>("Street");

                    b.Property<string>("UserId");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author");

                    b.Property<string>("Description");

                    b.Property<string>("Genre");

                    b.Property<string>("ImageLink");

                    b.Property<int>("NumberOfUserRating");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.Property<int>("UserRatingAvg");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("CartId");

                    b.Property<int>("GroupingId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("UserId");

                    b.Property<string>("UserReview");

                    b.HasKey("Id");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.OldCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookId");

                    b.Property<string>("CartId");

                    b.Property<int>("GroupingId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.ToTable("OldCartItems");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ItemGroupingId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BookCave.Data.EntityModels.TempAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<int>("HouseNum");

                    b.Property<string>("Street");

                    b.Property<string>("UserId");

                    b.Property<int>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("TempAddresses");
                });
#pragma warning restore 612, 618
        }
    }
}
