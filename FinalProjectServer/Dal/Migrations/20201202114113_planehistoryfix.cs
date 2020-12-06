using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dal.Migrations
{
    public partial class planehistoryfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ControlTower",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlTower", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightDirection = table.Column<int>(nullable: false),
                    PlaneId = table.Column<int>(nullable: false),
                    ScheduledTime = table.Column<DateTime>(nullable: false),
                    StationId = table.Column<int>(nullable: true),
                    ControlTowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_ControlTower_ControlTowerId",
                        column: x => x.ControlTowerId,
                        principalTable: "ControlTower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrentFlightId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stations_Flights_CurrentFlightId",
                        column: x => x.CurrentFlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ControlTowerStationRelation",
                columns: table => new
                {
                    StationToId = table.Column<int>(nullable: false),
                    ControlTowerId = table.Column<int>(nullable: false),
                    Direction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ControlTowerStationRelation", x => new { x.Direction, x.ControlTowerId, x.StationToId });
                    table.ForeignKey(
                        name: "FK_ControlTowerStationRelation_ControlTower_ControlTowerId",
                        column: x => x.ControlTowerId,
                        principalTable: "ControlTower",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ControlTowerStationRelation_Stations_StationToId",
                        column: x => x.StationToId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlaneStationHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<int>(nullable: true),
                    StationId = table.Column<int>(nullable: false),
                    EnterStationTime = table.Column<DateTime>(nullable: true),
                    ExitStationTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneStationHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaneStationHistory_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlaneStationHistory_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StationRelation",
                columns: table => new
                {
                    StationFromId = table.Column<int>(nullable: false),
                    StationToId = table.Column<int>(nullable: false),
                    Direction = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StationRelation", x => new { x.Direction, x.StationFromId, x.StationToId });
                    table.ForeignKey(
                        name: "FK_StationRelation_Stations_StationFromId",
                        column: x => x.StationFromId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StationRelation_Stations_StationToId",
                        column: x => x.StationToId,
                        principalTable: "Stations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ControlTower",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 1, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 2, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 3, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 4, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 5, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 6, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 7, null });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "Id", "CurrentFlightId" },
                values: new object[] { 8, null });

            migrationBuilder.InsertData(
                table: "ControlTowerStationRelation",
                columns: new[] { "Direction", "ControlTowerId", "StationToId" },
                values: new object[] { 0, 1, 1 });

            migrationBuilder.InsertData(
                table: "ControlTowerStationRelation",
                columns: new[] { "Direction", "ControlTowerId", "StationToId" },
                values: new object[] { 1, 1, 6 });

            migrationBuilder.InsertData(
                table: "ControlTowerStationRelation",
                columns: new[] { "Direction", "ControlTowerId", "StationToId" },
                values: new object[] { 1, 1, 7 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 1, 2 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 2, 3 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 3, 4 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 4, 5 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 5, 6 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 0, 5, 7 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 1, 6, 8 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 1, 7, 8 });

            migrationBuilder.InsertData(
                table: "StationRelation",
                columns: new[] { "Direction", "StationFromId", "StationToId" },
                values: new object[] { 1, 8, 4 });

            migrationBuilder.CreateIndex(
                name: "IX_ControlTowerStationRelation_ControlTowerId",
                table: "ControlTowerStationRelation",
                column: "ControlTowerId");

            migrationBuilder.CreateIndex(
                name: "IX_ControlTowerStationRelation_StationToId",
                table: "ControlTowerStationRelation",
                column: "StationToId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ControlTowerId",
                table: "Flights",
                column: "ControlTowerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneStationHistory_FlightId",
                table: "PlaneStationHistory",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaneStationHistory_StationId",
                table: "PlaneStationHistory",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_StationRelation_StationFromId",
                table: "StationRelation",
                column: "StationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_StationRelation_StationToId",
                table: "StationRelation",
                column: "StationToId");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_CurrentFlightId",
                table: "Stations",
                column: "CurrentFlightId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ControlTowerStationRelation");

            migrationBuilder.DropTable(
                name: "PlaneStationHistory");

            migrationBuilder.DropTable(
                name: "StationRelation");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "ControlTower");
        }
    }
}
