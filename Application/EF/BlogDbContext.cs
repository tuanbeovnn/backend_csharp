using Microsoft.EntityFrameworkCore;
using Models;
using Models.Accounts;

namespace Application.EF
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext()
        {
        } 
        

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }


        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer("Server=VUVIETTUNG\\MSSQLSERVER01;Database=BLOG;Trusted_Connection=True",
        //             options => { options.EnableRetryOnFailure(); });
        //     }
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Accounts
            modelBuilder.Entity<Account>(e =>
            {
                e.HasKey(m => m.Id);
                // table configuration
                e.Property(e => e.Name).IsRequired();
                e.Property(e => e.UserName).IsRequired();
                e.Property(e => e.Email).IsRequired();
                e.Property(e => e.Password).IsRequired();
                e.Property(e => e.Active).IsRequired();
                e.Property(e => e.Provider).IsRequired();

                // One-to-Many: Account -> Posts
                e.HasMany(e => e.Posts)
                    .WithOne(e => e.Account)
                    .HasForeignKey(e => e.AccountId)
                    .OnDelete(DeleteBehavior.Restrict);

                // One-to-Many: Account -> Comments
                e.HasMany(e => e.Comments)
                    .WithOne(e => e.Account)
                    .HasForeignKey(e => e.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                // One-to-One: Account -> Profile
                e.HasOne(e => e.Profile)
                    .WithOne(e => e.Account)
                    .HasForeignKey<Profile>()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Category
            modelBuilder.Entity<Category>(e =>
            {
                e.HasKey(m => m.Id);

                // table configuration
                e.Property(e => e.Name).IsRequired();
                e.Property(e => e.Slug).IsRequired();
                e.Property(e => e.Status).IsRequired();

                // One-to-Many: Account -> Comments
                e.HasMany(e => e.Posts)
                    .WithOne(e => e.Category)
                    .HasForeignKey(e => e.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // Post
            modelBuilder.Entity<Post>(e =>
            {
                e.HasKey(m => m.Id);

                // table configuration
                e.Property(e => e.Content).IsRequired();
                e.Property(e => e.Title).IsRequired();
                e.Property(e => e.Status).IsRequired();
                e.Property(e => e.Approved).IsRequired();
                e.Property(e => e.Favourite).IsRequired();
                e.Property(e => e.ShortDescription).IsRequired();
                e.Property(e => e.Thumbnails).IsRequired();


                // Many-to-Many: Account -> Comments
                e.HasMany(e => e.Comments)
                    .WithOne(e => e.Post)
                    .HasForeignKey(e => e.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Tags
            modelBuilder.Entity<Tag>(e =>
            {
                e.HasKey(m => m.Id);
                // Table Configuration
                e.Property(e => e.Type).IsRequired();
                e.Property(e => e.Name).IsRequired();
            });

            // PostTags
            modelBuilder.Entity<PostTag>(e =>
            {
                e.HasKey(sc => new { sc.PostId, sc.TagId });
                // Many to Many
                e.HasOne(sc => sc.Post)
                    .WithMany(s => s.PostTags)
                    .HasForeignKey(sc => sc.PostId);

                e.HasOne(sc => sc.Tag)
                    .WithMany(s => s.PostTags)
                    .HasForeignKey(sc => sc.TagId);
            });

            // Comment
            modelBuilder.Entity<Comment>(e =>
            {
                e.HasKey(m => m.Id);

                // Table Configuration
                e.Property(e => e.Content).IsRequired();
                e.Property(e => e.Status).IsRequired();
            });


            // Profiles
            modelBuilder.Entity<Profile>(e =>
            {
                e.HasKey(m => m.Id);

                // Table Configuration
                e.Property(e => e.Bio);
                e.Property(e => e.Website).IsRequired();
                e.Property(e => e.Location).IsRequired();
                //e.Property(e => e.Social.Twitter).IsRequired();
                //e.Property(e => e.Social.LinkedIn).IsRequired();
                //e.Property(e => e.Social.GitHub).IsRequired();
            });
            //modelBuilder.Entity<SocialLinks>(e =>
            //{
            //    e.ToTable("SocialLinks");
            //    e.HasKey(m => m.Id);

            //    // Table Configuration
            //    e.Property(e => e.Twitter).IsRequired();
            //    e.Property(e => e.Website).IsRequired();
            //    e.Property(e => e.Location).IsRequired();
            //    //e.Property(e => e.Website).IsRequired();
            //    //e.Property(e => e.Location).IsRequired();
            //    //e.Property(e => e.Social.Twitter).IsRequired();
            //    //e.Property(e => e.Social.LinkedIn).IsRequired();
            //    //e.Property(e => e.Social.GitHub).IsRequired();
            //});


            SeedData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            List<Account> accounts = new List<Account>()
            {
                new Account
                {
                    Id = 1,
                    Email = "tungvv@gmail.com",
                    Name = "Vu Viet Tung",
                    UserName = "tungvv",
                    Password = "123456",
                    Role = Role.ADMIN,
                }
            };

            modelBuilder.Entity<Account>(e => { e.HasData(accounts); });
        }
    }
}