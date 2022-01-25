﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pelicula.Models.DB
{
    public partial class PeliculaContext : DbContext
    {
        public PeliculaContext()
        {
        }

        public PeliculaContext(DbContextOptions<PeliculaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Genero> Generos { get; set; } = null!;
        public virtual DbSet<PeliculaGenero> PeliculaGeneros { get; set; } = null!;
        public virtual DbSet<PeliculaRepository> PeliculaRepositories { get; set; } = null!;
        public virtual DbSet<Puntuacion> Puntuacions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost; Database=Pelicula; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("PK_Comentario_IdComentario");

                entity.ToTable("Comentario");

                entity.Property(e => e.Comentario1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Comentario");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdUsuario);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PK_Genero_IdGenero");

                entity.ToTable("Genero");

                entity.Property(e => e.NombreGenero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.Generos)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK_Genero_Pelicula");
            });

            modelBuilder.Entity<PeliculaGenero>(entity =>
            {
                entity.HasKey(e => e.IdPeliculaGenero)
                    .HasName("PK_PeliculaGenero_IdPeliculaGenero");

                entity.ToTable("PeliculaGenero");

                entity.HasOne(d => d.IdGeneroNavigation)
                    .WithMany(p => p.PeliculaGeneros)
                    .HasForeignKey(d => d.IdGenero)
                    .HasConstraintName("FK_PeliculaGenero_Genero_IdUsuario");

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.PeliculaGeneros)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK_PeliculaGenero_Pelicula_IdPelicula");
            });

            modelBuilder.Entity<PeliculaRepository>(entity =>
            {
                entity.HasKey(e => e.IdPelicula)
                    .HasName("PK_Pelicula_IdPelicula");

                entity.ToTable("PeliculaRepository");

                entity.Property(e => e.Actores)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Director)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LinkImagen)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LinkPelicula)
                    .HasMaxLength(450)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Puntuacion>(entity =>
            {
                entity.HasKey(e => e.IdPuntuacion)
                    .HasName("PK_Puntuacion_IdPuntuacion");

                entity.ToTable("Puntuacion");

                entity.Property(e => e.IdUsuario).HasMaxLength(450);

                entity.HasOne(d => d.IdPeliculaNavigation)
                    .WithMany(p => p.Puntuacions)
                    .HasForeignKey(d => d.IdPelicula)
                    .HasConstraintName("FK_Puntuacion_Pelicula_IdPelicula");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Puntuacions)
                    .HasForeignKey(d => d.IdUsuario);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}