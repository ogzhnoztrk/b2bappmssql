using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using B2BApp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace B2BApp.DataAccess.Context
{
    public class SqlDbContext : DbContext
    {

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {

        }

        public DbSet<Firma> Firmalar{ get; set; }
        public DbSet<Kategori> Kategoriler{ get; set; }
        public DbSet<Kullanici> Kullanicilar{ get; set; }
        public DbSet<Satis> Satislar{ get; set; }
        public DbSet<Siparis> Siparisler{ get; set; }
        public DbSet<Sube> Subeler{ get; set; }
        public DbSet<SubeStok> SubeStoklar{ get; set; }
        public DbSet<Tedarikci> Tedarikciler{ get; set; }
        public DbSet<Urun> Urunler{ get; set; }

        

    }
}
