﻿// <auto-generated />
using System;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutomationP.Migrations
{
    [DbContext(typeof(ProductContext))]
    [Migration("20190506153255_SalesR1")]
    partial class SalesR1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Library.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnterpriseId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Library.Models.Enterprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Enterprises");
                });

            modelBuilder.Entity("Library.Models.IncomingInvoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("StorageId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.HasIndex("UserId");

                    b.ToTable("IncomingInvoices");
                });

            modelBuilder.Entity("Library.Models.Invoice_Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("InvoiceId");

                    b.Property<int>("Prise");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("Invoice_Products");
                });

            modelBuilder.Entity("Library.Models.Money", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Coment");

                    b.Property<DateTime>("Data");

                    b.Property<int>("Price");

                    b.Property<int?>("SalesId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SalesId");

                    b.HasIndex("UserId");

                    b.ToTable("Money");
                });

            modelBuilder.Entity("Library.Models.Point_Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PointOfSaleId");

                    b.Property<int>("StorageId");

                    b.HasKey("Id");

                    b.HasIndex("PointOfSaleId");

                    b.HasIndex("StorageId");

                    b.ToTable("Point_Storages");
                });

            modelBuilder.Entity("Library.Models.PointOfSale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnterpriseId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("PointOfSales");
                });

            modelBuilder.Entity("Library.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BarCode");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ParCategoryId");

                    b.Property<int>("SellingPrice");

                    b.Property<string>("Units");

                    b.Property<string>("VendorCode");

                    b.HasKey("Id");

                    b.HasIndex("ParCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Library.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnterpriseId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Library.Models.Role_RoleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("RoleItemId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("RoleItemId");

                    b.ToTable("Role_RoleItems");
                });

            modelBuilder.Entity("Library.Models.RoleItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("RoleItems");
                });

            modelBuilder.Entity("Library.Models.Sales", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date");

                    b.Property<int>("PointOfSaleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PointOfSaleId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("Library.Models.Sales_Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count");

                    b.Property<int>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("SaleId");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SaleId");

                    b.HasIndex("UserId");

                    b.ToTable("Sales_Products");
                });

            modelBuilder.Entity("Library.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnterpriseId");

                    b.Property<int>("MaxWeight");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Library.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<int>("EnterpriseId");

                    b.Property<string>("LastName");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Patronymic");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Library.Models.User_Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PointId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PointId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Points");
                });

            modelBuilder.Entity("Library.Models.User_Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Roles");
                });

            modelBuilder.Entity("Library.Models.User_Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StorageId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.HasIndex("UserId");

                    b.ToTable("User_Storages");
                });

            modelBuilder.Entity("Library.Models.Category", b =>
                {
                    b.HasOne("Library.Models.Enterprise", "Enterprise")
                        .WithMany("Categories")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.Category", "ParentCategory")
                        .WithMany()
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.IncomingInvoice", b =>
                {
                    b.HasOne("Library.Models.Storage", "Storage")
                        .WithMany("IncomingInvoices")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Invoice_Product", b =>
                {
                    b.HasOne("Library.Models.IncomingInvoice", "Invoice")
                        .WithMany("Invoice_Products")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Money", b =>
                {
                    b.HasOne("Library.Models.Sales", "Sales")
                        .WithMany()
                        .HasForeignKey("SalesId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Point_Storage", b =>
                {
                    b.HasOne("Library.Models.PointOfSale", "PointOfSale")
                        .WithMany()
                        .HasForeignKey("PointOfSaleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.PointOfSale", b =>
                {
                    b.HasOne("Library.Models.Enterprise", "Enterprise")
                        .WithMany("PointOfSales")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Product", b =>
                {
                    b.HasOne("Library.Models.Category", "ParCategory")
                        .WithMany("Products")
                        .HasForeignKey("ParCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Role", b =>
                {
                    b.HasOne("Library.Models.Enterprise", "Enterprise")
                        .WithMany("Roles")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Role_RoleItem", b =>
                {
                    b.HasOne("Library.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.RoleItem", "RoleItem")
                        .WithMany()
                        .HasForeignKey("RoleItemId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Sales", b =>
                {
                    b.HasOne("Library.Models.PointOfSale", "PointOfSale")
                        .WithMany()
                        .HasForeignKey("PointOfSaleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Sales_Product", b =>
                {
                    b.HasOne("Library.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.Sales", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.Storage", b =>
                {
                    b.HasOne("Library.Models.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.User", b =>
                {
                    b.HasOne("Library.Models.Enterprise", "Enterprise")
                        .WithMany()
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.User_Point", b =>
                {
                    b.HasOne("Library.Models.PointOfSale", "Point")
                        .WithMany()
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.User_Role", b =>
                {
                    b.HasOne("Library.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Library.Models.User_Storage", b =>
                {
                    b.HasOne("Library.Models.Storage", "Storage")
                        .WithMany()
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Library.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
