﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using QuizyZunaAPI.Persistence;

#nullable disable

namespace QuizyZunaAPI.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240726073824_EraToYearAndLastModifiedAt")]
    partial class EraToYearAndLastModifiedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "difficulty", new[] { "beginner", "novice", "intermediate", "difficult", "expert" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "topic", new[] { "geography", "history", "sciences", "literature", "cinema", "video_games", "music", "visual_arts", "technology", "living_beings", "mythology", "television", "sport", "gastronomy", "cartoons", "architecture", "mangas" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("QuizyZunaAPI.Domain.Questions.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("QuizyZunaAPI.Domain.Questions.Question", b =>
                {
                    b.OwnsOne("QuizyZunaAPI.Domain.Questions.ValueObjects.Answers", "Answers", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("CorrectAnswer")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("QuestionId");

                            b1.ToTable("Questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");

                            b1.OwnsOne("QuizyZunaAPI.Domain.Questions.ValueObjects.WrongAnswers", "WrongAnswers", b2 =>
                                {
                                    b2.Property<Guid>("AnswersQuestionId")
                                        .HasColumnType("uuid");

                                    b2.HasKey("AnswersQuestionId");

                                    b2.ToTable("Questions");

                                    b2.WithOwner()
                                        .HasForeignKey("AnswersQuestionId");

                                    b2.OwnsMany("QuizyZunaAPI.Domain.Questions.Entities.WrongAnswer", "Value", b3 =>
                                        {
                                            b3.Property<Guid>("QuestionId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("integer");

                                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b3.Property<int>("Id"));

                                            b3.Property<string>("Value")
                                                .IsRequired()
                                                .HasColumnType("text");

                                            b3.HasKey("QuestionId", "Id");

                                            b3.ToTable("WrongAnswer");

                                            b3.WithOwner()
                                                .HasForeignKey("QuestionId");
                                        });

                                    b2.Navigation("Value");
                                });

                            b1.Navigation("WrongAnswers")
                                .IsRequired();
                        });

                    b.OwnsOne("QuizyZunaAPI.Domain.Questions.ValueObjects.QuestionTags", "Tags", b1 =>
                        {
                            b1.Property<Guid>("QuestionId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Difficulty")
                                .HasColumnType("integer");

                            b1.Property<string>("Year")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("QuestionId");

                            b1.ToTable("Questions");

                            b1.WithOwner()
                                .HasForeignKey("QuestionId");

                            b1.OwnsOne("QuizyZunaAPI.Domain.Questions.ValueObjects.Themes", "Themes", b2 =>
                                {
                                    b2.Property<Guid>("QuestionTagsQuestionId")
                                        .HasColumnType("uuid");

                                    b2.HasKey("QuestionTagsQuestionId");

                                    b2.ToTable("Questions");

                                    b2.WithOwner()
                                        .HasForeignKey("QuestionTagsQuestionId");

                                    b2.OwnsMany("QuizyZunaAPI.Domain.Questions.Entities.Theme", "Value", b3 =>
                                        {
                                            b3.Property<Guid>("QuestionId")
                                                .HasColumnType("uuid");

                                            b3.Property<int>("Id")
                                                .ValueGeneratedOnAdd()
                                                .HasColumnType("integer");

                                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b3.Property<int>("Id"));

                                            b3.Property<int>("Value")
                                                .HasColumnType("integer");

                                            b3.HasKey("QuestionId", "Id");

                                            b3.ToTable("Theme");

                                            b3.WithOwner()
                                                .HasForeignKey("QuestionId");
                                        });

                                    b2.Navigation("Value");
                                });

                            b1.Navigation("Themes")
                                .IsRequired();
                        });

                    b.Navigation("Answers")
                        .IsRequired();

                    b.Navigation("Tags")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}