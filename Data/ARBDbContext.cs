using ARB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARB.Data.Entities
{
    public class ARBDbContext : IdentityDbContext<UsuarioEntity>
    {
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<DestinatarioEntity> Destinatarios { get; set; }
        public DbSet<PedidoEntity> Pedidos { get; set; }
        public DbSet<RepartidorEntity> Repartidores { get; set; }
        public DbSet<SolicitudUbicacionEntity> SolicitudesUbicaciones { get; set; }
        public ARBDbContext(DbContextOptions<ARBDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsuarioEntity>().ToTable("Usuarios");
            modelBuilder.Entity<UsuarioEntity>().HasMany(u => u.Pedidos).WithOne(p => p.Usuario);
            modelBuilder.Entity<UsuarioEntity>().HasMany(u => u.Destinatarios).WithOne(d => d.Usuario);
            modelBuilder.Entity<UsuarioEntity>().Property(u => u.Id).ValueGeneratedOnAdd();


            modelBuilder.Entity<PedidoEntity>().ToTable("Pedidos");
            modelBuilder.Entity<PedidoEntity>().HasOne(p => p.Usuario).WithMany(u => u.Pedidos);
            modelBuilder.Entity<PedidoEntity>().HasOne(p => p.Repartidor).WithMany(r => r.Pedidos);
            modelBuilder.Entity<PedidoEntity>().HasOne(p => p.Destinatario).WithMany(d => d.Pedidos);
            modelBuilder.Entity<PedidoEntity>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<DestinatarioEntity>().ToTable("Destinatarios");
            modelBuilder.Entity<DestinatarioEntity>().HasMany(d => d.Pedidos).WithOne(p => p.Destinatario);
            modelBuilder.Entity<DestinatarioEntity>().HasMany(d => d.SolicitudesUbicacion).WithOne(s=>s.Destinatario);
            modelBuilder.Entity<DestinatarioEntity>().HasOne(d => d.Usuario).WithMany(u => u.Destinatarios);

            modelBuilder.Entity<DestinatarioEntity>().Property(d => d.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<RepartidorEntity>().ToTable("Repartidores");
            modelBuilder.Entity<RepartidorEntity>().HasMany(r => r.Pedidos).WithOne(p => p.Repartidor);
            modelBuilder.Entity<RepartidorEntity>().Property(r => r.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<SolicitudUbicacionEntity>().ToTable("SolicitudesUbicaciones");
            modelBuilder.Entity<SolicitudUbicacionEntity>().HasOne(s => s.Destinatario).WithMany(d => d.SolicitudesUbicacion);
        }

    }
}
