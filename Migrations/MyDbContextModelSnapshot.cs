﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlayerDuo.Database;

#nullable disable

namespace PlayerDuo.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PlayerDuo.Database.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Liên Quân Mobile",
                            ImageUrl = "/storage/AOV.png"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Liên Minh Huyền Thoại",
                            ImageUrl = "/storage/LOL.png"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "PUBG Mobile",
                            ImageUrl = "/storage/PUBG.png"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Free Fire",
                            ImageUrl = "/storage/freefire.png"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Liên Minh: Tốc Chiến",
                            ImageUrl = "/storage/LOL_Mobile.png"
                        },
                        new
                        {
                            Id = 6,
                            CategoryName = "Mobile Legends",
                            ImageUrl = "/storage/MBLG.png"
                        },
                        new
                        {
                            Id = 7,
                            CategoryName = "Play Together",
                            ImageUrl = "/storage/playtogether.png"
                        },
                        new
                        {
                            Id = 8,
                            CategoryName = "AU Mobile",
                            ImageUrl = "/storage/AOV.png"
                        },
                        new
                        {
                            Id = 9,
                            CategoryName = "AU Mobile",
                            ImageUrl = "/storage/AOV.png"
                        },
                        new
                        {
                            Id = 10,
                            CategoryName = "Đấu Trường Chân Lý",
                            ImageUrl = "/storage/AOV.png"
                        },
                        new
                        {
                            Id = 11,
                            CategoryName = "Valorant",
                            ImageUrl = "/storage/valorant.png"
                        },
                        new
                        {
                            Id = 12,
                            CategoryName = "Minecraft",
                            ImageUrl = "/storage/AOV.png"
                        },
                        new
                        {
                            Id = 13,
                            CategoryName = "CSGO",
                            ImageUrl = "/storage/AOV.png"
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.ImageReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReportId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ImageReports", (string)null);
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SenderId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Messages", (string)null);
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderedUserId")
                        .HasColumnType("int");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float");

                    b.Property<int?>("Quality")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<double?>("Rating")
                        .HasColumnType("float");

                    b.Property<int?>("SkillId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OrderedUserId");

                    b.HasIndex("SkillId");

                    b.ToTable("Orders", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2022, 9, 30, 0, 26, 48, 828, DateTimeKind.Local).AddTicks(2362),
                            OrderedUserId = 2,
                            Price = 100.0,
                            Quality = 2,
                            SkillId = 3,
                            Status = 1
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsChecked")
                        .HasColumnType("bit");

                    b.Property<int?>("ReportTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("ReportedUserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportTypeId")
                        .IsUnique()
                        .HasFilter("[ReportTypeId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Reports", (string)null);
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.ReportType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReportTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Quấy rối tình dục"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ngôn ngữ thù ghét hoặc bắt nạt"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lừa đảo hoặc gian lận"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Quảng cáo hoặc spam"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Bạo lực và chia rẽ"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Giá sai"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Dịch vụ không khớp với mô tả"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Tuổi hoặc giới tính giả"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Chất lượng dịch vụ kém"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Chuyển đơn hàng cho người khác"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Nhận đơn hàng vượt quá công suất"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Khác"
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleName = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleName = "Player"
                        },
                        new
                        {
                            Id = 3,
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AudioUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Skills", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                            CategoryId = 1,
                            Description = "Chiến tướng là chuyện nhỏ",
                            IsEnabled = true,
                            Price = 100.0,
                            UserId = 2
                        },
                        new
                        {
                            Id = 2,
                            AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                            CategoryId = 2,
                            Description = "Tầu hài là chính",
                            IsEnabled = false,
                            Price = 100.0,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                            CategoryId = 2,
                            Description = "Cùng nhau leo rank nào.",
                            IsEnabled = true,
                            Price = 100.0,
                            UserId = 3
                        },
                        new
                        {
                            Id = 4,
                            AudioUrl = "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3",
                            CategoryId = 11,
                            Description = "Valorant thì nạp mua skin xịn",
                            IsEnabled = true,
                            Price = 100.0,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AudioUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AvatarUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(62)
                        .HasColumnType("nvarchar(62)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("isPlayer")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "tuandang29042000@gmail.com",
                            IsEnabled = true,
                            NickName = "admin",
                            PasswordHash = new byte[] { 23, 168, 114, 191, 185, 196, 127, 224, 75, 188, 177, 206, 86, 176, 195, 205, 51, 87, 18, 32, 151, 116, 253, 148, 64, 237, 103, 161, 149, 185, 26, 115, 103, 134, 140, 148, 182, 15, 196, 93, 46, 209, 14, 189, 85, 94, 46, 179, 145, 23, 113, 230, 119, 48, 71, 249, 172, 137, 114, 64, 122, 212, 219, 174 },
                            PasswordSalt = new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 },
                            Phone = "0921231220",
                            Status = false,
                            Username = "admin",
                            isPlayer = false
                        },
                        new
                        {
                            Id = 2,
                            AvatarUrl = "/storage/tuan.jpg",
                            Email = "tuandang29042000@gmail.com",
                            IsEnabled = true,
                            NickName = "boi_cudon",
                            PasswordHash = new byte[] { 74, 4, 209, 219, 172, 64, 137, 171, 151, 102, 70, 225, 35, 130, 61, 180, 67, 99, 93, 151, 234, 63, 239, 239, 62, 147, 241, 155, 60, 221, 89, 152, 75, 180, 162, 74, 17, 117, 215, 91, 233, 176, 155, 1, 4, 104, 23, 223, 41, 208, 251, 197, 85, 123, 124, 119, 20, 132, 200, 250, 11, 153, 156, 107 },
                            PasswordSalt = new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 },
                            Phone = "0921231220",
                            Status = false,
                            Username = "quoctuan",
                            isPlayer = false
                        },
                        new
                        {
                            Id = 3,
                            AvatarUrl = "/storage/dat.jpg",
                            Email = "ngoluuquocdat@gmail.com",
                            IsEnabled = true,
                            NickName = "sadboiz",
                            PasswordHash = new byte[] { 65, 154, 57, 197, 132, 15, 77, 38, 139, 85, 104, 188, 110, 116, 82, 163, 19, 136, 246, 249, 5, 73, 63, 217, 59, 171, 8, 102, 61, 103, 166, 6, 22, 181, 106, 206, 140, 128, 18, 21, 144, 212, 202, 222, 219, 82, 59, 234, 67, 66, 83, 214, 76, 190, 233, 192, 237, 31, 150, 27, 22, 228, 183, 50 },
                            PasswordSalt = new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 },
                            Phone = "0905553859",
                            Status = false,
                            Username = "quocdat",
                            isPlayer = false
                        },
                        new
                        {
                            Id = 4,
                            AvatarUrl = "/storage/bao.jpg",
                            Email = "quanbao203@gmail.com",
                            IsEnabled = true,
                            NickName = "mt-15",
                            PasswordHash = new byte[] { 40, 228, 177, 247, 15, 150, 222, 123, 108, 211, 180, 209, 24, 205, 21, 151, 200, 204, 79, 197, 152, 196, 112, 13, 11, 176, 65, 142, 140, 35, 89, 138, 158, 91, 181, 142, 91, 175, 228, 56, 172, 152, 158, 109, 149, 154, 65, 178, 199, 218, 129, 76, 43, 180, 224, 151, 21, 179, 88, 97, 158, 81, 72, 46 },
                            PasswordSalt = new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 },
                            Phone = "0905553859",
                            Status = false,
                            Username = "quangbao",
                            isPlayer = false
                        },
                        new
                        {
                            Id = 5,
                            AvatarUrl = "/storage/tai.jpg",
                            Email = "brad@gmail.com",
                            IsEnabled = true,
                            NickName = "gaylord",
                            PasswordHash = new byte[] { 57, 60, 99, 227, 51, 51, 63, 158, 92, 52, 34, 9, 50, 82, 173, 126, 42, 242, 117, 21, 205, 55, 193, 232, 77, 78, 145, 201, 138, 9, 39, 30, 195, 153, 70, 90, 48, 223, 180, 8, 33, 65, 89, 123, 40, 241, 101, 228, 233, 31, 152, 46, 95, 147, 136, 182, 45, 50, 128, 152, 46, 232, 13, 59 },
                            PasswordSalt = new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 },
                            Phone = "0905553859",
                            Status = false,
                            Username = "congtai",
                            isPlayer = false
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            RoleId = 1
                        },
                        new
                        {
                            UserId = 1,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 1,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 2,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 2
                        },
                        new
                        {
                            UserId = 3,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 4,
                            RoleId = 3
                        },
                        new
                        {
                            UserId = 5,
                            RoleId = 3
                        });
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.ImageReport", b =>
                {
                    b.HasOne("PlayerDuo.Database.Entities.Report", "Report")
                        .WithMany("ImageReports")
                        .HasForeignKey("ReportId");

                    b.Navigation("Report");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Order", b =>
                {
                    b.HasOne("PlayerDuo.Database.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("OrderedUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PlayerDuo.Database.Entities.Skill", "Skill")
                        .WithMany("Orders")
                        .HasForeignKey("SkillId");

                    b.Navigation("Skill");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Report", b =>
                {
                    b.HasOne("PlayerDuo.Database.Entities.ReportType", "ReportType")
                        .WithOne("Report")
                        .HasForeignKey("PlayerDuo.Database.Entities.Report", "ReportTypeId");

                    b.HasOne("PlayerDuo.Database.Entities.User", null)
                        .WithMany("Reports")
                        .HasForeignKey("UserId");

                    b.Navigation("ReportType");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Skill", b =>
                {
                    b.HasOne("PlayerDuo.Database.Entities.Category", "Category")
                        .WithMany("Skills")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlayerDuo.Database.Entities.User", "User")
                        .WithMany("Skills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.UserRole", b =>
                {
                    b.HasOne("PlayerDuo.Database.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PlayerDuo.Database.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Category", b =>
                {
                    b.Navigation("Skills");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Report", b =>
                {
                    b.Navigation("ImageReports");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.ReportType", b =>
                {
                    b.Navigation("Report");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.Skill", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PlayerDuo.Database.Entities.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Reports");

                    b.Navigation("Skills");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
