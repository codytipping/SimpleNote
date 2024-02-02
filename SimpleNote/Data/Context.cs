using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleNote.Models;

namespace SimpleNote.Data;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}