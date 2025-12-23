using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SFWebservice.Modules;

namespace SFWebservice;

public partial class Sfdb01Context : DbContext
{
    public Sfdb01Context()
    {
    }

    public Sfdb01Context(DbContextOptions<Sfdb01Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Dodge> Dodges { get; set; }

    public virtual DbSet<GameSession> GameSessions { get; set; }

    public virtual DbSet<Hit> Hits { get; set; }

    public virtual DbSet<Objective> Objectives { get; set; }

    public virtual DbSet<ObjectiveCompleted> ObjectiveCompleteds { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sfdb02.database.windows.net;Authentication=Active Directory Default;Encrypt=True; Database=SFDB01");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dodge>(entity =>
        {
            entity.ToTable("Dodge");

            entity.Property(e => e.DodgeId).HasColumnName("DodgeID");
            entity.Property(e => e.GameSessionId).HasColumnName("GameSessionID");

            entity.HasOne(d => d.GameSession).WithMany(p => p.Dodges)
                .HasForeignKey(d => d.GameSessionId)
                .HasConstraintName("FK_Dodge_GameSession");
        });

        modelBuilder.Entity<GameSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("GameSession");

            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.FeedbackType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");

            entity.HasOne(d => d.Player).WithMany(p => p.GameSessions)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_GameSession_Player");
        });

        modelBuilder.Entity<Hit>(entity =>
        {
            entity.ToTable("Hit");

            entity.Property(e => e.HitId).HasColumnName("HitID");
            entity.Property(e => e.EntityHitId).HasColumnName("EntityHitID");
            entity.Property(e => e.GameSessionId).HasColumnName("GameSessionID");

            entity.HasOne(d => d.EntityHit).WithMany(p => p.Hits)
                .HasForeignKey(d => d.EntityHitId)
                .HasConstraintName("FK_Hit_Entity");

            entity.HasOne(d => d.GameSession).WithMany(p => p.Hits)
                .HasForeignKey(d => d.GameSessionId)
                .HasConstraintName("FK_Hit_GameSession");
        });

        modelBuilder.Entity<Objective>(entity =>
        {
            entity.ToTable("Objective");

            entity.Property(e => e.ObjectiveId).HasColumnName("ObjectiveID");
            entity.Property(e => e.ObjectiveName)
                .HasMaxLength(30)
                .IsFixedLength();
        });

        modelBuilder.Entity<ObjectiveCompleted>(entity =>
        {
            entity.ToTable("ObjectiveCompleted");

            entity.Property(e => e.ObjectiveCompletedId).HasColumnName("ObjectiveCompletedID");
            entity.Property(e => e.GameSessionId).HasColumnName("GameSessionID");

            entity.HasOne(d => d.GameSession).WithMany(p => p.ObjectiveCompleteds)
                .HasForeignKey(d => d.GameSessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ObjectiveCompleted_GameSession");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.Property(e => e.PlayerId).HasColumnName("PlayerID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
