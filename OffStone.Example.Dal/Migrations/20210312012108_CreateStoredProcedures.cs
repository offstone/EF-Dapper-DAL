using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.IO;

namespace OffStone.Example.Dal.Migrations
{
    public partial class CreateStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string searchOrders = Path.Combine(AppContext.BaseDirectory, "SQL\\usp_SearchOrders.sql");
            string searchOrdersMulti = Path.Combine(AppContext.BaseDirectory, "SQL\\usp_SearchOrdersMulti.sql");

            migrationBuilder.Sql(File.ReadAllText(searchOrders));
            migrationBuilder.Sql(File.ReadAllText(searchOrdersMulti));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sql = Path.Combine(AppContext.BaseDirectory, "SQL\\Drop_Procedures.sql");
            migrationBuilder.Sql(File.ReadAllText(sql));
        }
    }
}
