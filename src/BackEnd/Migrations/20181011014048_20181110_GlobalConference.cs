using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.Migrations
{
    public partial class _20181110_GlobalConference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Attendees_AttendeeID",
                table: "Sessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Tracks_TrackId",
                table: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Sessions_AttendeeID",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "TrackID",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "AttendeeID",
                table: "Sessions");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConferenceID",
                table: "Tracks",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Tracks",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConferenceID",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Speakers",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000,
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Speakers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Speakers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Speakers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TagLine",
                table: "Speakers",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "Speakers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "TagID",
                table: "SessionTag",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionID",
                table: "SessionTag",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SpeakerId",
                table: "SessionSpeaker",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "SessionId",
                table: "SessionSpeaker",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "TrackId",
                table: "Sessions",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ConferenceID",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Conferences",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Conferences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Conferences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Conferences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GlobalConferenceID",
                table: "Conferences",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                table: "Conferences",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                table: "Conferences",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Conferences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Registration",
                table: "Conferences",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Conferences",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Conferences",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<Guid>(
                name: "AttendeeID",
                table: "ConferenceAttendee",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "ConferenceID",
                table: "ConferenceAttendee",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<Guid>(
                name: "ID",
                table: "Attendees",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "ConferenceOrganisers",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Bio = table.Column<string>(maxLength: 10000, nullable: true),
                    TagLine = table.Column<string>(maxLength: 200, nullable: true),
                    Order = table.Column<int>(nullable: false),
                    WebSite = table.Column<string>(maxLength: 1000, nullable: true),
                    Twitter = table.Column<string>(maxLength: 100, nullable: true),
                    LinkedIn = table.Column<string>(maxLength: 100, nullable: true),
                    Picture = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 200, nullable: false),
                    ConferenceID1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConferenceOrganisers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConferenceOrganisers_Conferences_ConferenceID1",
                        column: x => x.ConferenceID1,
                        principalTable: "Conferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GlobalConferences",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Summary = table.Column<string>(nullable: false),
                    TagLine = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalConferences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SessionAttendee",
                columns: table => new
                {
                    SessionID = table.Column<Guid>(nullable: false),
                    AttendeeID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionAttendee", x => new { x.SessionID, x.AttendeeID });
                    table.ForeignKey(
                        name: "FK_SessionAttendee_Attendees_AttendeeID",
                        column: x => x.AttendeeID,
                        principalTable: "Attendees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SessionAttendee_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Picture = table.Column<string>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: false),
                    ConferenceID = table.Column<Guid>(nullable: true),
                    GlobalConferenceID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sponsors_Conferences_ConferenceID",
                        column: x => x.ConferenceID,
                        principalTable: "Conferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sponsors_GlobalConferences_GlobalConferenceID",
                        column: x => x.GlobalConferenceID,
                        principalTable: "GlobalConferences",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_GlobalConferenceID",
                table: "Conferences",
                column: "GlobalConferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_ConferenceOrganisers_ConferenceID1",
                table: "ConferenceOrganisers",
                column: "ConferenceID1");

            migrationBuilder.CreateIndex(
                name: "IX_SessionAttendee_AttendeeID",
                table: "SessionAttendee",
                column: "AttendeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_ConferenceID",
                table: "Sponsors",
                column: "ConferenceID");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsors_GlobalConferenceID",
                table: "Sponsors",
                column: "GlobalConferenceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Conferences_GlobalConferences_GlobalConferenceID",
                table: "Conferences",
                column: "GlobalConferenceID",
                principalTable: "GlobalConferences",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Tracks_TrackId",
                table: "Sessions",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conferences_GlobalConferences_GlobalConferenceID",
                table: "Conferences");

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Tracks_TrackId",
                table: "Sessions");

            migrationBuilder.DropTable(
                name: "ConferenceOrganisers");

            migrationBuilder.DropTable(
                name: "SessionAttendee");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "GlobalConferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_Conferences_GlobalConferenceID",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Tracks");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "TagLine",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "Speakers");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "GlobalConferenceID",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Registration",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Conferences");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Conferences");

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceID",
                table: "Tracks",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "TrackID",
                table: "Tracks",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Tags",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceID",
                table: "Speakers",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Speakers",
                maxLength: 4000,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Speakers",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "TagID",
                table: "SessionTag",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SessionID",
                table: "SessionTag",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SpeakerId",
                table: "SessionSpeaker",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "SessionId",
                table: "SessionSpeaker",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "TrackId",
                table: "Sessions",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceID",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Sessions",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "AttendeeID",
                table: "Sessions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Conferences",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "AttendeeID",
                table: "ConferenceAttendee",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ConferenceID",
                table: "ConferenceAttendee",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<int>(
                name: "ID",
                table: "Attendees",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tracks",
                table: "Tracks",
                column: "TrackID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_AttendeeID",
                table: "Sessions",
                column: "AttendeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Attendees_AttendeeID",
                table: "Sessions",
                column: "AttendeeID",
                principalTable: "Attendees",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Tracks_TrackId",
                table: "Sessions",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "TrackID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
