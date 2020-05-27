using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EP.EntityData.Context
{
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();

        DbSet<UserProfile> UserProfiles { get; set; }
        DbSet<Webpages_Roles> Webpages_Roles { get; set; }
        DbSet<Friend> Friends { get; set; }
        DbSet<RequestToFriend> RequestToFriends { get; set; }
        DbSet<News> News { get; set; }
        DbSet<NewsCommentary> NewsCommentaries { get; set; }
        DbSet<NewsLike> NewsLikes { get; set; }
        DbSet<Team> Teams { get; set; }
        DbSet<AssignTeam> AssignTeams { get; set; }
        DbSet<ActivationCode> ActivationCodes { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<ParticipantTeam> ParticipantTeams { get; set; }
        DbSet<ParticipantPlayer> ParticipantPlayers { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Tournament> Tournaments { get; set; }
        DbSet<AssignPlayer> AssignPlayers { get; set; }
        DbSet<Movie> Movies { get; set; }
        DbSet<Match> Matches { get; set; }
        DbSet<Goal> Goals { get; set; }
        DbSet<Card> Cards { get; set; }
    }

    public partial class DataContext : DbContext, IDataContext
    {
        public DataContext()
            : base("name=DataContext")
        {
            Database.SetInitializer<DataContext>(null);
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Webpages_Roles> Webpages_Roles { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<RequestToFriend> RequestToFriends { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsCommentary> NewsCommentaries { get; set; }
        public DbSet<NewsLike> NewsLikes { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<AssignTeam> AssignTeams { get; set; }
        public DbSet<ActivationCode> ActivationCodes { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ParticipantTeam> ParticipantTeams { get; set; }
        public DbSet<ParticipantPlayer> ParticipantPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<AssignPlayer> AssignPlayers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Card> Cards { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
               .HasMany(m => m.Roles)
               .WithRequired(m => m.User)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Webpages_Roles>()
                .HasMany(m => m.UsersInRoles)
                .WithRequired(m => m.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestToFriend>()
                .HasRequired(m => m.Who)
                .WithMany(m => m.RequestToFriends)
                .HasForeignKey(m => m.WhoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RequestToFriend>()
                .HasRequired(m => m.With)
                .WithMany(m => m.RequestFromFriends)
                .HasForeignKey(m => m.WithID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friend>()
                .HasRequired(m => m.Who)
                .WithMany(m => m.Friends)
                .HasForeignKey(m => m.WhoID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NewsCommentary>()
                .HasRequired(m => m.CreateBy)
                .WithMany(m => m.NewsCommentaries)
                .HasForeignKey(m => m.CreateById);

            modelBuilder.Entity<NewsCommentary>()
                .HasRequired(m => m.News)
                .WithMany(m => m.NewsCommentaries)
                .HasForeignKey(m => m.NewsId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<NewsLike>()
                .HasRequired(m => m.LikeBy)
                .WithMany(m => m.NewsLikes)
                .HasForeignKey(m => m.LikedById);

            modelBuilder.Entity<NewsLike>()
                .HasRequired(m => m.News)
                .WithMany(m => m.NewsLikes)
                .HasForeignKey(m => m.NewsId)
                .WillCascadeOnDelete(true);        

            modelBuilder.Entity<News>()
                .HasRequired(m => m.CreateBy)
                .WithMany()
                .HasForeignKey(m => m.CreateById)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasRequired(m => m.ModifiedBy)
                .WithMany()
                .HasForeignKey(m => m.ModifiedById)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Team>()
                .HasRequired(m => m.CreateBy)
                .WithMany(m => m.Teams)
                .HasForeignKey(m => m.CreateById);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Author)
                .WithMany()
                .HasForeignKey(m => m.AuthorId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasRequired(m => m.Recipient)
                .WithMany()
                .HasForeignKey(m => m.RecipientId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
               .HasRequired(m => m.LastSender)
               .WithMany()
               .HasForeignKey(m => m.LastSenderId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Player>()
                .HasOptional(m => m.UserProfile)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParticipantPlayer>()
               .HasRequired(m => m.Player)
               .WithMany(m => m.ParticipantPlayers)
               .HasForeignKey(m => m.PlayerId);

            modelBuilder.Entity<ParticipantPlayer>()
              .HasRequired(m => m.Team)
              .WithMany()
              .HasForeignKey(m => m.TeamId);

            modelBuilder.Entity<ParticipantPlayer>()
              .HasRequired(m => m.Tournament)
              .WithMany(m => m.ParticipantPlayers)
              .HasForeignKey(m => m.TournamentId);

            modelBuilder.Entity<ParticipantTeam>()
              .HasRequired(m => m.Tournament)
              .WithMany(m => m.ParticipantsTeams)
              .HasForeignKey(m => m.TournamentId);

            modelBuilder.Entity<ParticipantTeam>()
              .HasRequired(m => m.Team)
              .WithMany(m => m.ParticipantsTeams)
              .HasForeignKey(m => m.TeamId);

            modelBuilder.Entity<Friend>()
                .HasRequired(m => m.With)
                .WithMany()
                .HasForeignKey(m => m.WithID);

            modelBuilder.Entity<Goal>()
                .HasRequired(m => m.ParticipantPlayer)
                .WithMany(m => m.Goals)
                .HasForeignKey(m => m.ParticipantId);

            modelBuilder.Entity<Card>()
                .HasRequired(m => m.ParticipantPlayer)
                .WithMany(m => m.Cards)
                .HasForeignKey(m => m.ParticipantId);

            modelBuilder.Entity<Goal>()
                .HasRequired(m => m.Match)
                .WithMany(m => m.Goals)
                .HasForeignKey(m => m.MatchId);

            modelBuilder.Entity<Card>()
                .HasRequired(m => m.Match)
                .WithMany(m => m.Cards)
                .HasForeignKey(m => m.MatchId);

            modelBuilder.Entity<Match>()
                .HasRequired(m => m.Tournament)
                .WithMany(m => m.Matches)
                .HasForeignKey(m => m.TournamentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
               .HasRequired(m => m.FirstTeam)
               .WithMany()
               .HasForeignKey(m => m.FirstTeamId)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Match>()
              .HasRequired(m => m.SecondTeam)
              .WithMany()
              .HasForeignKey(m => m.SecondTeamId)
              .WillCascadeOnDelete(false);
        }
    }
}
