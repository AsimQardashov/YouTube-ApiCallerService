using Microsoft.EntityFrameworkCore;

namespace YouTubeApi.Models;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<YtChannel> YtChannels { get; set; }

    public virtual DbSet<YtChannelNews> YtChannelNews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("connection string");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresExtension("btree_gin")
            .HasPostgresExtension("pg_stat_statements")
            .HasPostgresExtension("pg_trgm")
            .HasPostgresExtension("unaccent");

        modelBuilder.Entity<YtChannel>(entity =>
        {
            entity.HasKey(e => e.ChannelId).HasName("yt_channel_pkey");

            entity.ToTable("yt_channel", "youtube");

            entity.Property(e => e.ChannelId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("channel_id");
            entity.Property(e => e.ChannelCreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("channel_created_at");
            entity.Property(e => e.ChannelStatus)
                .HasDefaultValueSql("1")
                .HasColumnName("channel_status");
            entity.Property(e => e.ChannelSubcribersCount).HasColumnName("channel_subcribers_count");
            entity.Property(e => e.ChannelTitle)
                .HasColumnType("character varying")
                .HasColumnName("channel_title");
            entity.Property(e => e.ChannelUpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("channel_updated_at");
            entity.Property(e => e.ChannelUsername)
                .HasColumnType("character varying")
                .HasColumnName("channel_username");
            entity.Property(e => e.ChannelYtId)
                .HasColumnType("character varying")
                .HasColumnName("channel_yt_id");
        });

        modelBuilder.Entity<YtChannelNews>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("yt_channel_news", "youtube");

            entity.HasIndex(e => e.NewsYtId, "uq_channel_yt_id").IsUnique();

            entity.Property(e => e.ChannelId).HasColumnName("channel_id");
            entity.Property(e => e.NewsAddDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("news_add_date");
            entity.Property(e => e.NewsId)
                .ValueGeneratedOnAdd()
                .UseIdentityAlwaysColumn()
                .HasColumnName("news_id");
            entity.Property(e => e.NewsLikeCount).HasColumnName("news_like_count");
            entity.Property(e => e.NewsPublishDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("news_publish_date");
            entity.Property(e => e.NewsTitle)
                .HasColumnType("character varying")
                .HasColumnName("news_title");
            entity.Property(e => e.NewsUrl)
                .HasColumnType("character varying")
                .HasColumnName("news_url");
            entity.Property(e => e.NewsViewCount).HasColumnName("news_views_count");
            entity.Property(e => e.NewsYtId)
                .HasColumnType("character varying")
                .HasColumnName("news_yt_id");

            entity.HasOne(d => d.Channel).WithMany()
                .HasForeignKey(d => d.ChannelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_channel_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
