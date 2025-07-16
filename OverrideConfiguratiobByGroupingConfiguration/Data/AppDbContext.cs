﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OverrideConfiguratiobByGroupingConfiguration.Data.Config;
using OverrideConfiguratiobByGroupingConfiguration.Entities;

namespace OverrideConfiguratiobByGroupingConfiguration.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tweet> Tweets { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserConfiguration().Configure(modelBuilder.Entity<User>());
            new CommentConfiguration().Configure(modelBuilder.Entity<Comment>());
            new TweetConfiguration().Configure(modelBuilder.Entity<Tweet>());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetSection("constr").Value;

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
