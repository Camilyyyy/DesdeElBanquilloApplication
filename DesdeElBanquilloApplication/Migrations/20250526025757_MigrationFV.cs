using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesdeElBanquilloApplication.Migrations
{
    /// <inheritdoc />
    public partial class MigrationFV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    IdAdministrator = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.IdAdministrator);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.IdCountry);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    IdPosition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.IdPosition);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "Federations",
                columns: table => new
                {
                    IdFederation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Acronym = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstablishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Federations", x => x.IdFederation);
                    table.ForeignKey(
                        name: "FK_Federations_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    IdLeague = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.IdLeague);
                    table.ForeignKey(
                        name: "FK_Leagues_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    IdSeason = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCurrent = table.Column<bool>(type: "bit", nullable: false),
                    TotalMatchdays = table.Column<int>(type: "int", nullable: false),
                    IdLeague = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.IdSeason);
                    table.ForeignKey(
                        name: "FK_Seasons_Leagues_IdLeague",
                        column: x => x.IdLeague,
                        principalTable: "Leagues",
                        principalColumn: "IdLeague",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    IdCompetition = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdSeason = table.Column<int>(type: "int", nullable: false),
                    IdFederation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.IdCompetition);
                    table.ForeignKey(
                        name: "FK_Competitions_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitions_Federations_IdFederation",
                        column: x => x.IdFederation,
                        principalTable: "Federations",
                        principalColumn: "IdFederation",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Competitions_Seasons_IdSeason",
                        column: x => x.IdSeason,
                        principalTable: "Seasons",
                        principalColumn: "IdSeason",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    IdTeam = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FoundedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCompetition = table.Column<int>(type: "int", nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false),
                    IdLeague = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.IdTeam);
                    table.ForeignKey(
                        name: "FK_Teams_Competitions_IdCompetition",
                        column: x => x.IdCompetition,
                        principalTable: "Competitions",
                        principalColumn: "IdCompetition",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Leagues_IdLeague",
                        column: x => x.IdLeague,
                        principalTable: "Leagues",
                        principalColumn: "IdLeague",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeagueTables",
                columns: table => new
                {
                    IdLeagueTable = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<int>(type: "int", nullable: false),
                    MatchesPlayed = table.Column<int>(type: "int", nullable: false),
                    MatchesWon = table.Column<int>(type: "int", nullable: false),
                    MatchesDrawn = table.Column<int>(type: "int", nullable: false),
                    MatchesLost = table.Column<int>(type: "int", nullable: false),
                    GoalsFor = table.Column<int>(type: "int", nullable: false),
                    GoalsAgainst = table.Column<int>(type: "int", nullable: false),
                    GoalDifference = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdTeam = table.Column<int>(type: "int", nullable: false),
                    IdSeason = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTables", x => x.IdLeagueTable);
                    table.ForeignKey(
                        name: "FK_LeagueTables_Seasons_IdSeason",
                        column: x => x.IdSeason,
                        principalTable: "Seasons",
                        principalColumn: "IdSeason",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueTables_Teams_IdTeam",
                        column: x => x.IdTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    IdPlayer = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    JerseyNumber = table.Column<int>(type: "int", nullable: false),
                    MarketValue = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    IdTeam = table.Column<int>(type: "int", nullable: false),
                    IdPosition = table.Column<int>(type: "int", nullable: false),
                    IdCountry = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.IdPlayer);
                    table.ForeignKey(
                        name: "FK_Players_Countries_IdCountry",
                        column: x => x.IdCountry,
                        principalTable: "Countries",
                        principalColumn: "IdCountry",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Positions_IdPosition",
                        column: x => x.IdPosition,
                        principalTable: "Positions",
                        principalColumn: "IdPosition",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Teams_IdTeam",
                        column: x => x.IdTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stadiums",
                columns: table => new
                {
                    IdStadium = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FoundedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    IdTeam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadiums", x => x.IdStadium);
                    table.ForeignKey(
                        name: "FK_Stadiums_Teams_IdTeam",
                        column: x => x.IdTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    IdMatch = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HomeGoals = table.Column<int>(type: "int", nullable: true),
                    AwayGoals = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Referee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IdHomeTeam = table.Column<int>(type: "int", nullable: false),
                    IdAwayTeam = table.Column<int>(type: "int", nullable: false),
                    IdCompetition = table.Column<int>(type: "int", nullable: false),
                    IdStadium = table.Column<int>(type: "int", nullable: false),
                    SeasonIdSeason = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.IdMatch);
                    table.ForeignKey(
                        name: "FK_Matches_Competitions_IdCompetition",
                        column: x => x.IdCompetition,
                        principalTable: "Competitions",
                        principalColumn: "IdCompetition",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Seasons_SeasonIdSeason",
                        column: x => x.SeasonIdSeason,
                        principalTable: "Seasons",
                        principalColumn: "IdSeason");
                    table.ForeignKey(
                        name: "FK_Matches_Stadiums_IdStadium",
                        column: x => x.IdStadium,
                        principalTable: "Stadiums",
                        principalColumn: "IdStadium",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_IdAwayTeam",
                        column: x => x.IdAwayTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Matches_Teams_IdHomeTeam",
                        column: x => x.IdHomeTeam,
                        principalTable: "Teams",
                        principalColumn: "IdTeam",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayers",
                columns: table => new
                {
                    IdMatchPlayers = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Goals = table.Column<int>(type: "int", nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    YellowCards = table.Column<int>(type: "int", nullable: false),
                    RedCards = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    IsStarter = table.Column<bool>(type: "bit", nullable: false),
                    SubstitutionMinute = table.Column<int>(type: "int", nullable: true),
                    IdMatch = table.Column<int>(type: "int", nullable: false),
                    IdPlayer = table.Column<int>(type: "int", nullable: false),
                    IdPosition = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayers", x => x.IdMatchPlayers);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Matches_IdMatch",
                        column: x => x.IdMatch,
                        principalTable: "Matches",
                        principalColumn: "IdMatch",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Players_IdPlayer",
                        column: x => x.IdPlayer,
                        principalTable: "Players",
                        principalColumn: "IdPlayer",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchPlayers_Positions_IdPosition",
                        column: x => x.IdPosition,
                        principalTable: "Positions",
                        principalColumn: "IdPosition",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_IdCountry",
                table: "Competitions",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_IdFederation",
                table: "Competitions",
                column: "IdFederation");

            migrationBuilder.CreateIndex(
                name: "IX_Competitions_IdSeason",
                table: "Competitions",
                column: "IdSeason");

            migrationBuilder.CreateIndex(
                name: "IX_Federations_IdCountry",
                table: "Federations",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Leagues_IdCountry",
                table: "Leagues",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTables_IdSeason",
                table: "LeagueTables",
                column: "IdSeason");

            migrationBuilder.CreateIndex(
                name: "IX_LeagueTables_IdTeam",
                table: "LeagueTables",
                column: "IdTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdAwayTeam",
                table: "Matches",
                column: "IdAwayTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdCompetition",
                table: "Matches",
                column: "IdCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdHomeTeam",
                table: "Matches",
                column: "IdHomeTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_IdStadium",
                table: "Matches",
                column: "IdStadium");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SeasonIdSeason",
                table: "Matches",
                column: "SeasonIdSeason");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_IdMatch",
                table: "MatchPlayers",
                column: "IdMatch");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_IdPlayer",
                table: "MatchPlayers",
                column: "IdPlayer");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayers_IdPosition",
                table: "MatchPlayers",
                column: "IdPosition");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IdCountry",
                table: "Players",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IdPosition",
                table: "Players",
                column: "IdPosition");

            migrationBuilder.CreateIndex(
                name: "IX_Players_IdTeam",
                table: "Players",
                column: "IdTeam");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_IdLeague",
                table: "Seasons",
                column: "IdLeague");

            migrationBuilder.CreateIndex(
                name: "IX_Stadiums_IdTeam",
                table: "Stadiums",
                column: "IdTeam",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_IdCompetition",
                table: "Teams",
                column: "IdCompetition");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_IdCountry",
                table: "Teams",
                column: "IdCountry");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_IdLeague",
                table: "Teams",
                column: "IdLeague");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "LeagueTables");

            migrationBuilder.DropTable(
                name: "MatchPlayers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Stadiums");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "Federations");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
