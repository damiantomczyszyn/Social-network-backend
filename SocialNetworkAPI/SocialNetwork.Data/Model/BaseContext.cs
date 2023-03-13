using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SocialNetwork.Data.Model
{
    public partial class BaseContext : DbContext
    {
        public BaseContext()
        {
        }

        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adress> Adresses { get; set; } = null!;
        public virtual DbSet<BlockedUser> BlockedUsers { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<CommentVote> CommentVotes { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupJoinRequest> GroupJoinRequests { get; set; } = null!;
        public virtual DbSet<GroupParticipant> GroupParticipants { get; set; } = null!;
        public virtual DbSet<Invite> Invites { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostView> PostViews { get; set; } = null!;
        public virtual DbSet<PostVote> PostVotes { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=15232;database=social-network;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Adress>(entity =>
            {
                entity.HasKey(e => e.UsersUserId)
                    .HasName("PRIMARY");

                entity.ToTable("adresses");

                entity.Property(e => e.UsersUserId)
                    .ValueGeneratedNever()
                    .HasColumnName("users_UserID");

                entity.Property(e => e.ApartmentNumber).HasMaxLength(45);

                entity.Property(e => e.City).HasMaxLength(35);

                entity.Property(e => e.Country).HasMaxLength(35);

                entity.Property(e => e.HouseNumber).HasMaxLength(45);

                entity.Property(e => e.Street).HasMaxLength(45);

                entity.HasOne(d => d.UsersUser)
                    .WithOne(p => p.Adress)
                    .HasForeignKey<Adress>(d => d.UsersUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timestamps_users1");
            });

            modelBuilder.Entity<BlockedUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("blocked users");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.BlockingUserId, e.BlockedUserId }, "ID constraint")
                    .IsUnique();

                entity.HasIndex(e => e.BlockingUserId, "User1ID_idx");

                entity.HasIndex(e => e.BlockedUserId, "User2ID_idx");

                entity.Property(e => e.BlockedUserId).HasColumnName("BlockedUserID");

                entity.Property(e => e.BlockingUserId).HasColumnName("BlockingUserID");

                entity.HasOne(d => d.BlockedUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.BlockedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User2ID0");

                entity.HasOne(d => d.BlockingUser)
                    .WithMany()
                    .HasForeignKey(d => d.BlockingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User1ID0");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comments");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CommentId, "CommentID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PostId, "fk_comments_posts1_idx");

                entity.HasIndex(e => e.SenderId, "fk_comments_users1_idx");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.Content).HasColumnType("mediumtext");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comments_posts1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comments_users1");
            });

            modelBuilder.Entity<CommentVote>(entity =>
            {
                entity.ToTable("comment votes");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CommentVoteId, "CommentVoteID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CommentId, "fk_comment votes_comments1");

                entity.HasIndex(e => e.VotingUserId, "fk_comment votes_users1_idx");

                entity.Property(e => e.CommentVoteId).HasColumnName("CommentVoteID");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.VotingUserId).HasColumnName("VotingUserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentVotes)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment votes_comments1");

                entity.HasOne(d => d.VotingUser)
                    .WithMany(p => p.CommentVotes)
                    .HasForeignKey(d => d.VotingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_comment votes_users1");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("conversations");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");
            });

            modelBuilder.Entity<ConversationParticipant>(entity =>
            {
                entity.HasKey(e => e.ParticipantId)
                    .HasName("PRIMARY");

                entity.ToTable("conversation participant");

                entity.HasIndex(e => e.ConversationId, "fk_conversation participant_users1_idx");

                entity.HasIndex(e => e.UserId, "fk_conversation participant_users2_idx");

                entity.Property(e => e.ParticipantId).HasColumnName("ParticipantID");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.LastViewDate).HasColumnType("timestamp");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.ConversationParticipantConversations)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conversation participant_users1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ConversationParticipantUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conversation participant_users2");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("friends");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.User1Id, e.User2Id }, "ID constraint")
                    .IsUnique();

                entity.HasIndex(e => e.User1Id, "User1ID_idx");

                entity.HasIndex(e => e.User2Id, "User2ID_idx");

                entity.Property(e => e.User1Id).HasColumnName("User1ID");

                entity.Property(e => e.User2Id).HasColumnName("User2ID");

                entity.HasOne(d => d.User1)
                    .WithMany()
                    .HasForeignKey(d => d.User1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User1ID");

                entity.HasOne(d => d.User2)
                    .WithMany()
                    .HasForeignKey(d => d.User2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User2ID");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("group");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.CreatorId, "fk_group_users1_idx");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.Description).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group_users1");
            });

            modelBuilder.Entity<GroupJoinRequest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group join request");

                entity.HasIndex(e => e.GroupId, "fk_group join request_group1_idx");

                entity.HasIndex(e => e.RequestingUserId, "fk_group join request_users1_idx");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.RequestingUserId).HasColumnName("RequestingUserID");

                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group join request_group1");

                entity.HasOne(d => d.RequestingUser)
                    .WithMany()
                    .HasForeignKey(d => d.RequestingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group join request_users1");
            });

            modelBuilder.Entity<GroupParticipant>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("group participant");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.UserId, e.GroupId }, "ID constraint")
                    .IsUnique();

                entity.HasIndex(e => e.GroupId, "fk_group participant_group1");

                entity.HasIndex(e => e.UserId, "fk_group participant_users1_idx");

                entity.Property(e => e.GroupId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("GroupID");

                entity.Property(e => e.Nick).HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group participant_group1");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_group participant_users1");
            });

            modelBuilder.Entity<Invite>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("invites");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => new { e.InvitingUserId, e.InvitedUserId }, "ID constraint")
                    .IsUnique();

                entity.HasIndex(e => e.InvitingUserId, "User1ID_idx");

                entity.HasIndex(e => e.InvitedUserId, "User2ID_idx");

                entity.Property(e => e.InvitedUserId).HasColumnName("InvitedUserID");

                entity.Property(e => e.InvitingUserId).HasColumnName("InvitingUserID");

                entity.HasOne(d => d.InvitedUser)
                    .WithMany()
                    .HasForeignKey(d => d.InvitedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User2ID00");

                entity.HasOne(d => d.InvitingUser)
                    .WithMany()
                    .HasForeignKey(d => d.InvitingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("User1ID00");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("messages");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.DateTime, "DateTime_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.ConversationParticipantId, "fk_messages_conversation participant2_idx");

                entity.HasIndex(e => e.ConversationId, "fk_messages_conversations2_idx");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Content).HasColumnType("mediumtext");

                entity.Property(e => e.ConversationId).HasColumnName("ConversationID");

                entity.Property(e => e.ConversationParticipantId).HasColumnName("ConversationParticipantID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.LastEditTime).HasColumnType("timestamp");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_messages_conversations2");

                entity.HasOne(d => d.ConversationParticipant)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_messages_conversation participant2");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("posts");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.GroupId, "fk_posts_group1_idx");

                entity.HasIndex(e => e.SenderId, "fk_posts_users_idx");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.Content).HasColumnType("mediumtext");

                entity.Property(e => e.DataTime).HasColumnType("datetime");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Messagescol)
                    .HasMaxLength(45)
                    .HasColumnName("messagescol");

                entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_posts_group1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_posts_users");
            });

            modelBuilder.Entity<PostView>(entity =>
            {
                entity.ToTable("post view");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.PostViewId, "PostViewID_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.PostId, "fk_post view_posts1");

                entity.HasIndex(e => e.UserId, "fk_senderid_idx");

                entity.Property(e => e.PostViewId).HasColumnName("PostViewID");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostViews)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post view_posts1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostViews)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_senderid");
            });

            modelBuilder.Entity<PostVote>(entity =>
            {
                entity.ToTable("post votes");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.PostId, "fk_post votes_posts1");

                entity.HasIndex(e => e.VotingUserId, "fk_post votes_users1_idx");

                entity.Property(e => e.PostVoteId).HasColumnName("PostVoteID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.VotingUserId).HasColumnName("VotingUserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostVotes)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post votes_posts1");

                entity.HasOne(d => d.VotingUser)
                    .WithMany(p => p.PostVotes)
                    .HasForeignKey(d => d.VotingUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_post votes_users1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.EmailAddress, "EmailAddress_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TelephoneNumber, "TelephoneNumber_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "UserID_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AllowInvites).HasDefaultValueSql("'1'");

                entity.Property(e => e.EmailAddress).HasMaxLength(35);

                entity.Property(e => e.LearningPlace).HasMaxLength(45);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.PasswordHash).HasMaxLength(50);

                entity.Property(e => e.Surname).HasMaxLength(20);

                entity.Property(e => e.TelephoneNumber).HasMaxLength(15);

                entity.Property(e => e.WorkingPlace).HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
