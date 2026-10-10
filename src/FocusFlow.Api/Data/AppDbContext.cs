using Microsoft.EntityFrameworkCore;

namespace FocusFlow.Api.Data;

public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }
}
