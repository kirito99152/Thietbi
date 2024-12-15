using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Thietbi.Models;

public partial class ThietBiContext : DbContext
{
    public ThietBiContext()
    {
    }

    public ThietBiContext(DbContextOptions<ThietBiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DmLoaiThietBi> DmLoaiThietBis { get; set; }

    public virtual DbSet<DmTrangThaiThietBi> DmTrangThaiThietBis { get; set; }

    public virtual DbSet<TbLichSuCapPhat> TbLichSuCapPhats { get; set; }

    public virtual DbSet<TbLichSuSuaChua> TbLichSuSuaChuas { get; set; }

    public virtual DbSet<TbQuanHeThietBi> TbQuanHeThietBis { get; set; }

    public virtual DbSet<TbThietBi> TbThietBis { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Thietbi;Trusted_Connection=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmLoaiThietBi>(entity =>
        {
            entity.HasKey(e => e.IdLoaiThietBi);

            entity.ToTable("DmLoaiThietBi", "DM");
        });

        modelBuilder.Entity<DmTrangThaiThietBi>(entity =>
        {
            entity.HasKey(e => e.IdTrangThaiThietBi);

            entity.ToTable("DmTrangThaiThietBi", "DM");
        });

        modelBuilder.Entity<TbLichSuCapPhat>(entity =>
        {
            entity.HasKey(e => e.IdLichSuCapPhat).HasName("PK_LichSuCapPhat");

            entity.ToTable("TbLichSuCapPhat", "TB");

            entity.Property(e => e.ThoiGianCapPhat).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianThuHoi).HasColumnType("datetime");

            entity.HasOne(d => d.IdThietBiNavigation).WithMany(p => p.TbLichSuCapPhats)
                .HasForeignKey(d => d.IdThietBi)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TbLichSuCapPhat_TbThietBi");
        });

        modelBuilder.Entity<TbLichSuSuaChua>(entity =>
        {
            entity.HasKey(e => e.IdLichSuSuaChua).HasName("PK_LichSuSuaChua");

            entity.ToTable("TbLichSuSuaChua", "TB");

            entity.Property(e => e.ThoiGianBatDau).HasColumnType("datetime");
            entity.Property(e => e.ThoiGianKetThuc).HasColumnType("datetime");

            entity.HasOne(d => d.IdThietBiNavigation).WithMany(p => p.TbLichSuSuaChuas)
                .HasForeignKey(d => d.IdThietBi)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TbLichSuSuaChua_TbThietBi");
        });

        modelBuilder.Entity<TbQuanHeThietBi>(entity =>
        {
            entity.HasKey(e => e.IdQuanHeThietBi).HasName("PK_TbThietBiCon");

            entity.ToTable("TbQuanHeThietBi", "TB");

            entity.HasOne(d => d.IdThietBiChaNavigation).WithMany(p => p.TbQuanHeThietBiIdThietBiChaNavigations)
                .HasForeignKey(d => d.IdThietBiCha)
                .HasConstraintName("FK_TbQuanHeThietBi_TbThietBiCha");

            entity.HasOne(d => d.IdThietBiConNavigation).WithMany(p => p.TbQuanHeThietBiIdThietBiConNavigations)
                .HasForeignKey(d => d.IdThietBiCon)
                .HasConstraintName("FK_TbQuanHeThietBi_TbThietBiCon");
        });

        modelBuilder.Entity<TbThietBi>(entity =>
        {
            entity.HasKey(e => e.IdThietBi).HasName("PK_ThietBi");

            entity.ToTable("TbThietBi", "TB");

            entity.Property(e => e.MaThietBiHv).HasColumnName("MaThietBiHV");
            entity.Property(e => e.MaThietBiNhaSx).HasColumnName("MaThietBiNhaSX");
            entity.Property(e => e.NgayThemThietBi).HasColumnType("datetime");

            entity.HasOne(d => d.IdLoaiThietBiNavigation).WithMany(p => p.TbThietBis)
                .HasForeignKey(d => d.IdLoaiThietBi)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TbThietBi_DmLoaiThietBi");

            entity.HasOne(d => d.IdTrangThaiThietBiNavigation).WithMany(p => p.TbThietBis)
                .HasForeignKey(d => d.IdTrangThaiThietBi)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_TbThietBi_DmTrangThaiThietBi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
