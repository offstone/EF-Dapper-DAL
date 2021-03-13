using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Diagnostics;
using System.IO;

namespace OffStone.Example.Dal.Migrations
{
    public partial class SeedTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (Debugger.IsAttached == false)
            {
                //Debugger.Launch();
            }

            string northwindSeed = Path.Combine(AppContext.BaseDirectory, "SQL\\Insert_NorthwindCopy_SeedData.sql");
            migrationBuilder.Sql(File.ReadAllText(northwindSeed));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string northwindtruncate = Path.Combine(AppContext.BaseDirectory, "SQL\\Truncate_NorthwindCopy_Data.sql");
            migrationBuilder.Sql(File.ReadAllText(northwindtruncate));
        }
    }
}
