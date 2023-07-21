using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerCalAccountTotalSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER [dbo].[UpdateTotalSale]
				ON [dbo].[DealLines]
				AFTER INSERT, UPDATE, DELETE
				AS
				BEGIN
					DECLARE @CustomerId INT = (SELECT TOP 1 CUSTOMERID FROM 
														(SELECT * FROM INSERTED 
															UNION 
														 SELECT * FROM DELETED) AS DATA
												   INNER JOIN DEALS
												   ON DATA.DealId = DEALS.ID
												   INNER JOIN LEADS
												   ON DEALS.LEADID = LEADS.ID);
		
					UPDATE ACCOUNTS
					SET TOTALSALES = (SELECT SUM(DEALLINES.PricePerUnit * DealLines.Quantity) FROM LEADS
										INNER JOIN DEALS
										ON DEALS.LEADID = LEADS.ID
										INNER JOIN DEALLINES
										ON DEALLINES.DEALID = DEALS.ID
										WHERE LEADS.CUSTOMERID = @CUSTOMERID)
					WHERE ID = @CustomerId;
				END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP TRIGGER [dbo].[UpdateTotalSale]");
        }
    }
}
