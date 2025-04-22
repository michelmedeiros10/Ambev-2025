using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			//---------
			//Users
			//---------
			migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					Username = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Status = table.Column<int>(type: "INT", nullable: false),
                    Role = table.Column<int>(type: "INT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    UpdatedAt = table.Column<DateTime?>(type: "DATETIME", nullable: true)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

			//----------
			//PersonName
			//----------
			migrationBuilder.CreateTable(
				name: "PersonNames",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
					FirstName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
					LastName = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PersonNames", x => x.Id);
					table.ForeignKey("FK_PersonNames_Users", x => x.UserId, "Users", principalColumn: "Id");
				});

			//-------------
			//PersonAddress
			//-------------
			migrationBuilder.CreateTable(
				name: "PersonAddress",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					UserId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
					City = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
					Street = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
					Number = table.Column<int>(type: "INT", nullable: false),
					Zipcode = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
					GeoLatitude = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
					GeoLongitude = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PersonAddress", x => x.Id);
					table.ForeignKey("FK_PersonAddress_Users", x => x.UserId, "Users", principalColumn: "Id");
				});


			//--------
			//UserRole
			//--------
			migrationBuilder.CreateTable(
				name: "UserRoles",
				columns: table => new
				{
					Id = table.Column<int>(type: "INT", nullable: false),
					Description = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserRoles", x => x.Id);
				});

			migrationBuilder.Sql("INSERT INTO UserRoles (id, Description) VALUES (0, 'None');", false);
			migrationBuilder.Sql("INSERT INTO UserRoles (id, Description) VALUES (1, 'Customer');", false);
			migrationBuilder.Sql("INSERT INTO UserRoles (id, Description) VALUES (2, 'Manager');", false);
			migrationBuilder.Sql("INSERT INTO UserRoles (id, Description) VALUES (3, 'Admin');", false);

			//Adding the ForeignKey to table Users
			migrationBuilder.AddForeignKey("FK_Users_UserRoles", "Users", "Role", "UserRoles", principalColumn: "Id");

			//----------
			//UserStatus
			//----------
			migrationBuilder.CreateTable(
				name: "UserStatus",
				columns: table => new
				{
					Id = table.Column<int>(type: "INT", nullable: false),
					Description = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserStatus", x => x.Id);
				});

			migrationBuilder.Sql("INSERT INTO UserStatus (id, Description) VALUES (0, 'Unknown');", false);
			migrationBuilder.Sql("INSERT INTO UserStatus (id, Description) VALUES (1, 'Active');", false);
			migrationBuilder.Sql("INSERT INTO UserStatus (id, Description) VALUES (2, 'Inactive');", false);
			migrationBuilder.Sql("INSERT INTO UserStatus (id, Description) VALUES (3, 'Suspended');", false);

			//Adding the ForeignKey to table Users
			migrationBuilder.AddForeignKey("FK_Users_UserStatus", "Users", "Status", "UserStatus", principalColumn: "Id");

			//--------
			//Products
			//--------
			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					Title = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
					Price = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
					Description = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
					Category = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: true),
					Image = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true),
					Rate = table.Column<decimal>(type: "DECIMAL(7,2)", nullable: true),
					Count = table.Column<int>(type: "INT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.Id);
				});

			migrationBuilder.Sql("INSERT INTO Products (Id, Title, Price, Description, Category, Image, Rate, Count) VALUES ('c0c58759-6c68-4a3a-e89a-08dd7e8829a8', 'Product Test 1', 100, 'Product Test 1', 'Category Test 1', 'http://img-link1', 1.1, 10);", false);
			migrationBuilder.Sql("INSERT INTO Products (Id, Title, Price, Description, Category, Image, Rate, Count) VALUES ('4bda22c9-a344-49bf-15df-08dd7eb73725', 'Product Test 2', 200, 'Product Test 2', 'Category Test 1', 'http://img-link2', 2.2, 20);", false);

			//----------
			//Customers
			//----------
			migrationBuilder.CreateTable(
				name: "Customers",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					Name = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Customers", x => x.Id);
				});

			migrationBuilder.Sql("INSERT INTO Customers (Id, Name) VALUES ('3e2525ec-dcf9-4b88-810b-43edf2ff3928','Customer Test');", false);


			//---------
			//Sales
			//---------
			migrationBuilder.CreateTable(
				name: "Sales",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					SaleNumber = table.Column<int>(type: "INT", nullable: false),
					SaleDate = table.Column<DateTime>(type: "DATETIME", nullable: false),
					CustomerId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
					Amount = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
					Branch = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Sales", x => x.Id);
					table.ForeignKey("FK_Sales_Customers", x => x.CustomerId, "Customers", principalColumn: "Id");
				});

			//------------
			//SaleProducts
			//------------
			migrationBuilder.CreateTable(
				name: "SaleProduct",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false, defaultValueSql: "NEWID()"),
					SaleId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
					ProductId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
					Price = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
					Quantity = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
					Discount = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false),
					TotalAmount = table.Column<decimal>(type: "DECIMAL(12,2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_SaleProduct", x => x.Id);
					table.ForeignKey("FK_SaleProduct_Products", x => x.ProductId, "Products", principalColumn: "Id");
					table.ForeignKey("FK_SaleProduct_Sales", x => x.SaleId, "Sales", principalColumn: "Id");
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
