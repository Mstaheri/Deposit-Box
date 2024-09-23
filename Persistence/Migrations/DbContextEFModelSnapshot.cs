﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DbContextEF))]
    partial class DbContextEFModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entity.BankAccount", b =>
                {
                    b.Property<string>("AccountNumber")
                        .HasMaxLength(16)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BankName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("AccountNumber");

                    b.HasIndex("UserName");

                    b.ToTable("BankAccounts", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.BankSafe", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SharePrice")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Name");

                    b.ToTable("BankSafes", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.BankSafeDocument", b =>
                {
                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal>("Deposit")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("NameBankSafe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situation")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Withdrawal")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Code");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("BankSafeDocuments", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.BankSafeTransaction", b =>
                {
                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(16)");

                    b.Property<decimal>("Deposit")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("NameBankSafe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Withdrawal")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Code");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("BankSafeTransactions", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.ChatRoom", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConnectionId")
                        .IsRequired()
                        .HasMaxLength(23)
                        .IsUnicode(false)
                        .HasColumnType("varchar(23)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConnectionId")
                        .IsUnique();

                    b.ToTable("ChatRooms", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.Loan", b =>
                {
                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NameBankSafe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfInstallments")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Wage")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.HasKey("Code");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("Loans", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.LoanDocument", b =>
                {
                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CodeLoan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DueDate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("NameBankSafe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RegistrationDate")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Situation")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Code");

                    b.HasIndex("CodeLoan");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("LoanDocuments", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.LoanTransactions", b =>
                {
                    b.Property<Guid>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("CodeLoan")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("NameBankSafe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumberOfInstallments")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Code");

                    b.HasIndex("CodeLoan");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("LoanTransactions", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("NationalIDNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserName");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.UserAndNumberOfShare", b =>
                {
                    b.Property<string>("UserName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NameBankSafe")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfShares")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("UserName", "NameBankSafe");

                    b.HasIndex("NameBankSafe");

                    b.ToTable("UserAndNumberOfShares", (string)null);
                });

            modelBuilder.Entity("Domain.Entity.BankAccount", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany("BankAccounts")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.BankSafeDocument", b =>
                {
                    b.HasOne("Domain.Entity.BankAccount", "BankAccount")
                        .WithMany("BankSafeDocuments")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("BankSafeDocuments")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("BankSafe");
                });

            modelBuilder.Entity("Domain.Entity.BankSafeTransaction", b =>
                {
                    b.HasOne("Domain.Entity.BankAccount", "BankAccount")
                        .WithMany("BankSafeTransactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("BankSafeTransactions")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("BankSafe");
                });

            modelBuilder.Entity("Domain.Entity.Loan", b =>
                {
                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("loans")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankSafe");
                });

            modelBuilder.Entity("Domain.Entity.LoanDocument", b =>
                {
                    b.HasOne("Domain.Entity.Loan", "Loan")
                        .WithMany("LoanDocuments")
                        .HasForeignKey("CodeLoan")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("LoanDocuments")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BankSafe");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Domain.Entity.LoanTransactions", b =>
                {
                    b.HasOne("Domain.Entity.Loan", "Loan")
                        .WithMany("LoanTransactions")
                        .HasForeignKey("CodeLoan")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("LoanTransactions")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("BankSafe");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Domain.Entity.UserAndNumberOfShare", b =>
                {
                    b.HasOne("Domain.Entity.BankSafe", "BankSafe")
                        .WithMany("UserAndNumberOfShares")
                        .HasForeignKey("NameBankSafe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany("UserAndNumberOfShares")
                        .HasForeignKey("UserName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankSafe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.BankAccount", b =>
                {
                    b.Navigation("BankSafeDocuments");

                    b.Navigation("BankSafeTransactions");
                });

            modelBuilder.Entity("Domain.Entity.BankSafe", b =>
                {
                    b.Navigation("BankSafeDocuments");

                    b.Navigation("BankSafeTransactions");

                    b.Navigation("LoanDocuments");

                    b.Navigation("LoanTransactions");

                    b.Navigation("UserAndNumberOfShares");

                    b.Navigation("loans");
                });

            modelBuilder.Entity("Domain.Entity.Loan", b =>
                {
                    b.Navigation("LoanDocuments");

                    b.Navigation("LoanTransactions");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Navigation("BankAccounts");

                    b.Navigation("UserAndNumberOfShares");
                });
#pragma warning restore 612, 618
        }
    }
}
