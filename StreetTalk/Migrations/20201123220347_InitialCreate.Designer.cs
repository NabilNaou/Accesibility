﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StreetTalk;

namespace StreetTalk.Migrations
{
    [DbContext(typeof(StreetTalkContext))]
    [Migration("20201123220347_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("StreetTalk.Models.Comment", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("content")
                        .HasMaxLength(600)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("postId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("postId");

                    b.HasIndex("userId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("StreetTalk.Models.Like", b =>
                {
                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("postId")
                        .HasColumnType("INTEGER");

                    b.HasKey("userId", "postId");

                    b.HasIndex("postId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("StreetTalk.Models.Photo", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("filename")
                        .HasColumnType("TEXT");

                    b.Property<bool>("sensitive")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("StreetTalk.Models.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("content")
                        .HasColumnType("text");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("photoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("title")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("posts");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Post");
                });

            modelBuilder.Entity("StreetTalk.Models.Profile", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("city")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("dateOfBirth")
                        .HasColumnType("Date");

                    b.Property<string>("firstName")
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<int?>("houseNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("houseNumberAddition")
                        .HasMaxLength(5)
                        .HasColumnType("TEXT");

                    b.Property<string>("lastName")
                        .HasMaxLength(45)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("street")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.Property<int>("userId")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("StreetTalk.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("accessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("emailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("lockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("lockoutEndTime")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("modifiedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("passwordHash")
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("StreetTalk.Models.AnonymousPost", b =>
                {
                    b.HasBaseType("StreetTalk.Models.Post");

                    b.Property<string>("pseudonym")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasIndex("photoId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("AnonymousPost");
                });

            modelBuilder.Entity("StreetTalk.Models.PublicPost", b =>
                {
                    b.HasBaseType("StreetTalk.Models.Post");

                    b.Property<bool>("closed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("reportCount")
                        .HasColumnType("INTEGER");

                    b.HasIndex("photoId")
                        .HasDatabaseName("IX_posts_photoId1");

                    b.HasDiscriminator().HasValue("PublicPost");
                });

            modelBuilder.Entity("StreetTalk.Models.Comment", b =>
                {
                    b.HasOne("StreetTalk.Models.PublicPost", "post")
                        .WithMany("comments")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreetTalk.Models.User", "author")
                        .WithMany("comments")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("post");
                });

            modelBuilder.Entity("StreetTalk.Models.Like", b =>
                {
                    b.HasOne("StreetTalk.Models.PublicPost", "post")
                        .WithMany("likes")
                        .HasForeignKey("postId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StreetTalk.Models.User", "user")
                        .WithMany("likes")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("post");

                    b.Navigation("user");
                });

            modelBuilder.Entity("StreetTalk.Models.Profile", b =>
                {
                    b.HasOne("StreetTalk.Models.User", "user")
                        .WithOne("profile")
                        .HasForeignKey("StreetTalk.Models.Profile", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("StreetTalk.Models.AnonymousPost", b =>
                {
                    b.HasOne("StreetTalk.Models.Photo", "photo")
                        .WithOne("post")
                        .HasForeignKey("StreetTalk.Models.AnonymousPost", "photoId");

                    b.Navigation("photo");
                });

            modelBuilder.Entity("StreetTalk.Models.PublicPost", b =>
                {
                    b.HasOne("StreetTalk.Models.Photo", "photo")
                        .WithMany()
                        .HasForeignKey("photoId")
                        .HasConstraintName("FK_posts_Photo_photoId1");

                    b.Navigation("photo");
                });

            modelBuilder.Entity("StreetTalk.Models.Photo", b =>
                {
                    b.Navigation("post");
                });

            modelBuilder.Entity("StreetTalk.Models.User", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("likes");

                    b.Navigation("profile");
                });

            modelBuilder.Entity("StreetTalk.Models.PublicPost", b =>
                {
                    b.Navigation("comments");

                    b.Navigation("likes");
                });
#pragma warning restore 612, 618
        }
    }
}
