﻿// <auto-generated />
using System;
using BuildsByBrickwellNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BuildsByBrickwellNew.Migrations
{
    [DbContext(typeof(IntexProjectContext))]
    [Migration("20240410173847_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BuildsByBrickwellNew.Models.Customer", b =>
                {
                    b.Property<double?>("Age")
                        .HasColumnType("float")
                        .HasColumnName("age");

                    b.Property<string>("BirthDate")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("birth_date");

                    b.Property<string>("CountryOfResidence")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country_of_residence");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_ID");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("last_name");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BuildsByBrickwellNew.Models.LineItem", b =>
                {
                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_ID");

                    b.Property<int?>("Qty")
                        .HasColumnType("int")
                        .HasColumnName("qty");

                    b.Property<int?>("Rating")
                        .HasColumnType("int")
                        .HasColumnName("rating");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int")
                        .HasColumnName("transaction_ID");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("BuildsByBrickwellNew.Models.Order", b =>
                {
                    b.Property<double?>("Amount")
                        .HasColumnType("float")
                        .HasColumnName("amount");

                    b.Property<string>("Bank")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("bank");

                    b.Property<string>("CountryOfTransaction")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country_of_transaction");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("customer_ID");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("date");

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("day_of_week");

                    b.Property<string>("EntryMode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("entry_mode");

                    b.Property<int?>("Fraud")
                        .HasColumnType("int")
                        .HasColumnName("fraud");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("shipping_address");

                    b.Property<int?>("Time")
                        .HasColumnType("int")
                        .HasColumnName("time");

                    b.Property<int?>("TransactionId")
                        .HasColumnType("int")
                        .HasColumnName("transaction_ID");

                    b.Property<string>("TypeOfCard")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type_of_card");

                    b.Property<string>("TypeOfTransaction")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("type_of_transaction");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BuildsByBrickwellNew.Models.Product", b =>
                {
                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("category");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<string>("ImgLink")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("img_link");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.Property<int?>("NumParts")
                        .HasColumnType("int")
                        .HasColumnName("num_parts");

                    b.Property<int?>("Price")
                        .HasColumnType("int")
                        .HasColumnName("price");

                    b.Property<string>("PrimaryColor")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("primary_color");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("product_ID");

                    b.Property<string>("SecondaryCategory")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("secondary_category");

                    b.Property<string>("SecondaryColor")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("secondary_color");

                    b.Property<string>("TertiaryCategory")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("tertiary_category");

                    b.Property<int?>("Year")
                        .HasColumnType("int")
                        .HasColumnName("year");

                    b.ToTable("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
