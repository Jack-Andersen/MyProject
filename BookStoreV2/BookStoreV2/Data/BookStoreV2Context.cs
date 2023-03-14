using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookStoreV2.Models;

namespace BookStoreV2.Data
{
    public class BookStoreV2Context : DbContext
    {
        public BookStoreV2Context (DbContextOptions<BookStoreV2Context> options)
            : base(options)
        {
        }

        private static string _sQLConnectionString = String.Empty;

        public static string SQLConnectionString
        {
            get
            {
                return _sQLConnectionString;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _sQLConnectionString = value;
                }
            }
        }

        private readonly IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<ReadingHistory> ReadingHistories { get; set; }

        public BookStoreV2Context(DbContextOptions<BookStoreV2Context> options,
                               IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()
                .HasKey(cl => new
                {
                    cl.BookId,
                    cl.AuthorId
                });
            modelBuilder.Entity<ReadingHistory>()
                .HasKey(cl => new
                {
                    cl.BookId,
                    cl.CustomerId
                });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString;

#if ENABLED_FOR_LAZY_LOADING_USAGE
            if (!String.IsNullOrEmpty(_sQLConnectionString))
            {
                connectionString = _sQLConnectionString;
            }
            else
            {
                connectionString = this._configuration.GetConnectionString("BookStoreV2Context_ConnectionString");
            }
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(connectionString);
#endif
        }
    }
}
