using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemDAL.Data.Configurations
{
    public class PlanConfigurations : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(P => P.Name).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(P => P.Description).HasColumnType("varchar").HasMaxLength(200);
            builder.Property(P => P.Price).HasPrecision(10,2);
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("GymSystem_PlanDuration_Check","[DurationDays] between 1 and 365");
            });

            builder.HasMany(p => p.MemberShips).WithOne(ms => ms.Plan).HasForeignKey(ms => ms.PlanID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}