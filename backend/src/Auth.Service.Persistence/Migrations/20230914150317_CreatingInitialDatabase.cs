using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Service.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatingInitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authproviders",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authproviders", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2023, 9, 14, 15, 3, 17, 474, DateTimeKind.Utc).AddTicks(1099)),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    passwordhash = table.Column<string>(type: "text", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2023, 9, 14, 15, 3, 17, 475, DateTimeKind.Utc).AddTicks(8663)),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customeraddress",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    deliveryaddressid = table.Column<Guid>(type: "uuid", nullable: false),
                    receivername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    isprimaryaddress = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2023, 9, 14, 15, 3, 17, 472, DateTimeKind.Utc).AddTicks(8576)),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customeraddress", x => x.id);
                    table.ForeignKey(
                        name: "FK_customeraddress_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    roleid = table.Column<Guid>(type: "uuid", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2023, 9, 14, 15, 3, 17, 473, DateTimeKind.Utc).AddTicks(6046)),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_employee_role_roleid",
                        column: x => x.roleid,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userauthproviders",
                columns: table => new
                {
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    authproviderid = table.Column<Guid>(type: "uuid", nullable: false),
                    externaluserid = table.Column<string>(type: "text", nullable: false),
                    userid1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userauthproviders", x => new { x.userid, x.authproviderid });
                    table.ForeignKey(
                        name: "FK_userauthproviders_authproviders_authproviderid",
                        column: x => x.authproviderid,
                        principalTable: "authproviders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userauthproviders_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userauthproviders_user_userid1",
                        column: x => x.userid1,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "usertokens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    token = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(type: "uuid", nullable: false),
                    expiresat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usertokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_usertokens_user_userid",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    customeraddressid = table.Column<Guid>(type: "uuid", nullable: false),
                    zipcode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    city = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    state = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    country = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    streetnumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    complement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValue: new DateTime(2023, 9, 14, 15, 3, 17, 471, DateTimeKind.Utc).AddTicks(4262)),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address", x => x.id);
                    table.ForeignKey(
                        name: "FK_address_customeraddress_customeraddressid",
                        column: x => x.customeraddressid,
                        principalTable: "customeraddress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_customeraddressid",
                table: "address",
                column: "customeraddressid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_customeraddress_userid",
                table: "customeraddress",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_employee_roleid",
                table: "employee",
                column: "roleid");

            migrationBuilder.CreateIndex(
                name: "IX_employee_userid",
                table: "employee",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userauthproviders_authproviderid",
                table: "userauthproviders",
                column: "authproviderid");

            migrationBuilder.CreateIndex(
                name: "IX_userauthproviders_userid1",
                table: "userauthproviders",
                column: "userid1");

            migrationBuilder.CreateIndex(
                name: "IX_usertokens_userid",
                table: "usertokens",
                column: "userid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "userauthproviders");

            migrationBuilder.DropTable(
                name: "usertokens");

            migrationBuilder.DropTable(
                name: "customeraddress");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "authproviders");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
