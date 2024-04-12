using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildsByBrickwellNew.Models;

public partial class IntexProjectContext : IdentityDbContext
{
    public IntexProjectContext()
    {
    }

    public IntexProjectContext(DbContextOptions<IntexProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LineItem> LineItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Auth_new_user_rec> Auth_New_User_Recs { get; set; }

    public virtual DbSet<Customer2_rec> Customer2_Recs { get; set; }

    public virtual DbSet<Item_based_rec> Item_Based_Recs { get; set; }

    public virtual DbSet<High_rated_rec> High_Rated_Recs { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //   => optionsBuilder.UseSqlServer("Data Source=IntexProject.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customers");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.CountryOfResidence).HasColumnName("country_of_residence");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {

            entity.ToTable("LineItems");
            entity.HasKey(e => e.TransactionId); // Add a primary key property
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Orders");
            entity.HasKey(e => e.CustomerId); // Add a primary key property

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Bank).HasColumnName("bank");
            entity.Property(e => e.CountryOfTransaction).HasColumnName("country_of_transaction");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.EntryMode).HasColumnName("entry_mode");
            entity.Property(e => e.Fraud).HasColumnName("fraud");
            entity.Property(e => e.ShippingAddress).HasColumnName("shipping_address");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
            entity.Property(e => e.TypeOfCard).HasColumnName("type_of_card");
            entity.Property(e => e.TypeOfTransaction).HasColumnName("type_of_transaction");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.SecondaryCategory).HasColumnName("secondary_category");
            entity.Property(e => e.TertiaryCategory).HasColumnName("tertiary_category");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ImgLink).HasColumnName("img_link");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.NumParts).HasColumnName("num_parts");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PrimaryColor).HasColumnName("primary_color");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.SecondaryColor).HasColumnName("secondary_color");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<Auth_new_user_rec>(entity =>
        {
            entity.ToTable("Auth_new_user_rec");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName).HasColumnName("product_name");
            entity.Property(e => e.ImgLink).HasColumnName("img_link");
        });

        modelBuilder.Entity<Customer2_rec>(entity =>
        {
            entity.ToTable("Customer2_rec");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Rec1).HasColumnName("rec_1");
            entity.Property(e => e.Rec2).HasColumnName("rec_2");
            entity.Property(e => e.Rec3).HasColumnName("rec_3");
            entity.Property(e => e.Rec4).HasColumnName("rec_4");
            entity.Property(e => e.Rec5).HasColumnName("rec_5");
            entity.Property(e => e.Rec6).HasColumnName("rec_6");
            entity.Property(e => e.Rec7).HasColumnName("rec_7");
            entity.Property(e => e.Rec8).HasColumnName("rec_8");
            entity.Property(e => e.Rec9).HasColumnName("rec_9");
        });

        modelBuilder.Entity<High_rated_rec>(entity =>
        {
            entity.ToTable("high_rated_rec");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ImgLink).HasColumnName("img_link");
        });

        modelBuilder.Entity<Item_based_rec>(entity =>
        {
            entity.ToTable("item_based_rec");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RecommendedProductId1).HasColumnName("recommended_product_ID_1");
            entity.Property(e => e.RecommendedProductId2).HasColumnName("recommended_product_ID_2");
            entity.Property(e => e.RecommendedProductId3).HasColumnName("recommended_product_ID_3");
            entity.Property(e => e.RecommendedProductId4).HasColumnName("recommended_product_ID_4");
            entity.Property(e => e.RecommendedProductId5).HasColumnName("recommended_product_ID_5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
