using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;



namespace ConsoleRPG{
    public partial class PlayersContext : DbContext
    {
        public DbSet<Player> Players {get; set;}
        public string Dbpath;
        public PlayersContext(){
          Dbpath = $"{Environment.CurrentDirectory}/Players.db";
        }

        public PlayersContext(DbContextOptions<PlayersContext> options): base(options){
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={Dbpath}");
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.

    }
}