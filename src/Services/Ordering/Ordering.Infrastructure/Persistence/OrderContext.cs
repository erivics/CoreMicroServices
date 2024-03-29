﻿using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> option) : base(option)
        {
        }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "Kola";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LasModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Kola";
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
