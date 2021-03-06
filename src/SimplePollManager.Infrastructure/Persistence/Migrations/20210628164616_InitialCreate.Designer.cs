// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimplePollManager.Infrastructure.Persistence;

namespace SimplePollManager.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(PollDbContext))]
    [Migration("20210628164616_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimplePollManager.Domain.Entities.Poll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatorKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsOpen")
                        .HasColumnType("bit");

                    b.Property<int>("PollType")
                        .HasColumnType("int");

                    b.Property<string>("Question")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Polls");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PollId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollAnswers");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollAnswerVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PollAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PollVoteId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PollAnswerId");

                    b.HasIndex("PollVoteId");

                    b.ToTable("PollAnswerVotes");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PollId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.ToTable("PollVotes");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollAnswer", b =>
                {
                    b.HasOne("SimplePollManager.Domain.Entities.Poll", "Poll")
                        .WithMany("Answers")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollAnswerVote", b =>
                {
                    b.HasOne("SimplePollManager.Domain.Entities.PollAnswer", "PollAnswer")
                        .WithMany("PollAnswerVotes")
                        .HasForeignKey("PollAnswerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("SimplePollManager.Domain.Entities.PollVote", "PollVote")
                        .WithMany("PollAnswerVotes")
                        .HasForeignKey("PollVoteId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("PollAnswer");

                    b.Navigation("PollVote");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollVote", b =>
                {
                    b.HasOne("SimplePollManager.Domain.Entities.Poll", "Poll")
                        .WithMany("Votes")
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poll");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.Poll", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollAnswer", b =>
                {
                    b.Navigation("PollAnswerVotes");
                });

            modelBuilder.Entity("SimplePollManager.Domain.Entities.PollVote", b =>
                {
                    b.Navigation("PollAnswerVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
