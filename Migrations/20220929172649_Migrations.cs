using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerDuo.Migrations
{
    public partial class Migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    isPlayer = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportTypeId = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedUserId = table.Column<int>(type: "int", nullable: true),
                    ReportedUserId = table.Column<int>(type: "int", nullable: true),
                    IsChecked = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_ReportTypes_ReportTypeId",
                        column: x => x.ReportTypeId,
                        principalTable: "ReportTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skills_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageReports_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: true),
                    OrderedUserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true, defaultValue: 1),
                    Quality = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Users_OrderedUserId",
                        column: x => x.OrderedUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "ImageUrl" },
                values: new object[,]
                {
                    { 1, "Liên Quân Mobile", "/storage/AOV.png" },
                    { 2, "Liên Minh Huyền Thoại", "/storage/LOL.png" },
                    { 3, "PUBG Mobile", "/storage/PUBG.png" },
                    { 4, "Free Fire", "/storage/freefire.png" },
                    { 5, "Liên Minh: Tốc Chiến", "/storage/LOL_Mobile.png" },
                    { 6, "Mobile Legends", "/storage/MBLG.png" },
                    { 7, "Play Together", "/storage/playtogether.png" },
                    { 8, "AU Mobile", "/storage/AOV.png" },
                    { 9, "AU Mobile", "/storage/AOV.png" },
                    { 10, "Đấu Trường Chân Lý", "/storage/AOV.png" },
                    { 11, "Valorant", "/storage/valorant.png" },
                    { 12, "Minecraft", "/storage/AOV.png" },
                    { 13, "CSGO", "/storage/AOV.png" }
                });

            migrationBuilder.InsertData(
                table: "ReportTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Quấy rối tình dục" },
                    { 2, "Ngôn ngữ thù ghét hoặc bắt nạt" },
                    { 3, "Lừa đảo hoặc gian lận" },
                    { 4, "Quảng cáo hoặc spam" },
                    { 5, "Bạo lực và chia rẽ" },
                    { 6, "Giá sai" },
                    { 7, "Dịch vụ không khớp với mô tả" },
                    { 8, "Tuổi hoặc giới tính giả" },
                    { 9, "Chất lượng dịch vụ kém" },
                    { 10, "Chuyển đơn hàng cho người khác" },
                    { 11, "Nhận đơn hàng vượt quá công suất" },
                    { 12, "Khác" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Player" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AudioUrl", "AvatarUrl", "Description", "Email", "IsEnabled", "NickName", "PasswordHash", "PasswordSalt", "Phone", "Status", "Username", "isPlayer" },
                values: new object[,]
                {
                    { 1, null, null, null, "tuandang29042000@gmail.com", true, "admin", new byte[] { 23, 168, 114, 191, 185, 196, 127, 224, 75, 188, 177, 206, 86, 176, 195, 205, 51, 87, 18, 32, 151, 116, 253, 148, 64, 237, 103, 161, 149, 185, 26, 115, 103, 134, 140, 148, 182, 15, 196, 93, 46, 209, 14, 189, 85, 94, 46, 179, 145, 23, 113, 230, 119, 48, 71, 249, 172, 137, 114, 64, 122, 212, 219, 174 }, new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 }, "0921231220", false, "admin", false },
                    { 2, null, "/storage/tuan.jpg", null, "tuandang29042000@gmail.com", true, "boi_cudon", new byte[] { 74, 4, 209, 219, 172, 64, 137, 171, 151, 102, 70, 225, 35, 130, 61, 180, 67, 99, 93, 151, 234, 63, 239, 239, 62, 147, 241, 155, 60, 221, 89, 152, 75, 180, 162, 74, 17, 117, 215, 91, 233, 176, 155, 1, 4, 104, 23, 223, 41, 208, 251, 197, 85, 123, 124, 119, 20, 132, 200, 250, 11, 153, 156, 107 }, new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 }, "0921231220", false, "quoctuan", false },
                    { 3, null, "/storage/dat.jpg", null, "ngoluuquocdat@gmail.com", true, "sadboiz", new byte[] { 65, 154, 57, 197, 132, 15, 77, 38, 139, 85, 104, 188, 110, 116, 82, 163, 19, 136, 246, 249, 5, 73, 63, 217, 59, 171, 8, 102, 61, 103, 166, 6, 22, 181, 106, 206, 140, 128, 18, 21, 144, 212, 202, 222, 219, 82, 59, 234, 67, 66, 83, 214, 76, 190, 233, 192, 237, 31, 150, 27, 22, 228, 183, 50 }, new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 }, "0905553859", false, "quocdat", false },
                    { 4, null, "/storage/bao.jpg", null, "quanbao203@gmail.com", true, "mt-15", new byte[] { 40, 228, 177, 247, 15, 150, 222, 123, 108, 211, 180, 209, 24, 205, 21, 151, 200, 204, 79, 197, 152, 196, 112, 13, 11, 176, 65, 142, 140, 35, 89, 138, 158, 91, 181, 142, 91, 175, 228, 56, 172, 152, 158, 109, 149, 154, 65, 178, 199, 218, 129, 76, 43, 180, 224, 151, 21, 179, 88, 97, 158, 81, 72, 46 }, new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 }, "0905553859", false, "quangbao", false },
                    { 5, null, "/storage/tai.jpg", null, "brad@gmail.com", true, "gaylord", new byte[] { 57, 60, 99, 227, 51, 51, 63, 158, 92, 52, 34, 9, 50, 82, 173, 126, 42, 242, 117, 21, 205, 55, 193, 232, 77, 78, 145, 201, 138, 9, 39, 30, 195, 153, 70, 90, 48, 223, 180, 8, 33, 65, 89, 123, 40, 241, 101, 228, 233, 31, 152, 46, 95, 147, 136, 182, 45, 50, 128, 152, 46, 232, 13, 59 }, new byte[] { 72, 86, 41, 135, 27, 184, 107, 235, 132, 57, 18, 197, 187, 1, 154, 221, 116, 158, 94, 227, 49, 42, 146, 8, 138, 67, 12, 42, 173, 38, 49, 27, 105, 4, 109, 69, 122, 212, 15, 142, 212, 168, 18, 243, 10, 239, 82, 62, 90, 121, 184, 118, 184, 103, 12, 170, 45, 212, 128, 124, 15, 31, 11, 235, 13, 60, 133, 87, 172, 81, 25, 64, 232, 46, 132, 184, 101, 197, 70, 110, 23, 76, 9, 71, 120, 94, 130, 5, 66, 202, 165, 154, 97, 171, 93, 86, 218, 136, 113, 55, 137, 104, 88, 7, 0, 32, 243, 156, 30, 169, 46, 125, 102, 250, 109, 30, 24, 207, 28, 147, 139, 246, 241, 79, 239, 196, 37, 16 }, "0905553859", false, "congtai", false }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "AudioUrl", "CategoryId", "Description", "IsEnabled", "Price", "UserId" },
                values: new object[] { 1, "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3", 1, "Chiến tướng là chuyện nhỏ", true, 100.0, 2 });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "AudioUrl", "CategoryId", "Description", "Price", "UserId" },
                values: new object[] { 2, "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3", 2, "Tầu hài là chính", 100.0, 2 });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "AudioUrl", "CategoryId", "Description", "IsEnabled", "Price", "UserId" },
                values: new object[,]
                {
                    { 3, "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3", 2, "Cùng nhau leo rank nào.", true, 100.0, 3 },
                    { 4, "/audio/6a8f7469-c581-4f00-be5d-f9383c47c0b1.mp3", 11, "Valorant thì nạp mua skin xịn", true, 100.0, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 2, 2 },
                    { 3, 2 },
                    { 2, 3 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Comment", "CreatedAt", "OrderedUserId", "Price", "Quality", "Rating", "SkillId", "Status", "UpdatedAt" },
                values: new object[] { 1, null, new DateTime(2022, 9, 30, 0, 26, 48, 828, DateTimeKind.Local).AddTicks(2362), 2, 100.0, 2, null, 3, 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_ImageReports_ReportId",
                table: "ImageReports",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderedUserId",
                table: "Orders",
                column: "OrderedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SkillId",
                table: "Orders",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReportTypeId",
                table: "Reports",
                column: "ReportTypeId",
                unique: true,
                filter: "[ReportTypeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CategoryId",
                table: "Skills",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_UserId",
                table: "Skills",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageReports");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ReportTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
