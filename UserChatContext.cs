using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserDataLayer.Models;
namespace UserDataLayer
{
    public class UserChatContext : DbContext
    {
        public UserChatContext() : base() { }
        public UserChatContext(DbContextOptions<UserChatContext> opts) : base(opts)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Friendship> Frienships { get; set; }
        public DbSet<MessageGroupUser> MessageGroupUsers { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasMany(f => f.RequestedFrienships)
            .WithOne(f => f.RequesterFriend)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
            .HasMany(f => f.ExternalFrienships)
            .WithOne(f => f.RequestedFriend)
            .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<User>()
            //.HasMany(f => f.GroupsBridge)
            //.WithOne(o => o.Member)
            //.OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<GroupUser>()
            //.HasOne(f => f.Member)
            //.WithMany(mu => mu.GroupsBridge)
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<GroupUser>()
            //.HasOne(f => f.Group)
            //.WithMany(mu => mu.GroupMembers)
            //.OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Message>()
            //.HasMany(f => f.GroupUsers)
            //.WithOne(gu => gu.Message)
            //.OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
