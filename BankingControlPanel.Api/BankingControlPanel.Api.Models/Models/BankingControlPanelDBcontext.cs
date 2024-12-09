using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Api.Models.Models
{
    public class BankingControlPanelDBcontext : DbContext
    {
        public BankingControlPanelDBcontext(DbContextOptions<BankingControlPanelDBcontext> options): base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Search> Searches { get; set; }

    }
}
