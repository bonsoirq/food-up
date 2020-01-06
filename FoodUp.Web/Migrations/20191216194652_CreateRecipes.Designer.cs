﻿// <auto-generated />
using System;
using FoodUp.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodUp.Web.Migrations
{
    [DbContext(typeof(FoodUpContext))]
    [Migration("20191216194652_CreateRecipes")]
    partial class CreateRecipes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0");

            modelBuilder.Entity("FoodUp.Web.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("EncryptedPassword")
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasAlternateKey("Login")
                        .HasName("AK_Login");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
