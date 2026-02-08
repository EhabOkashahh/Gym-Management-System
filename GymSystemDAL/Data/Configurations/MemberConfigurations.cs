using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Data.Configurations;
using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemDAL.Data.Configurations
{
    public class MemberConfigurations : GymUserConfigurations<Member>, IEntityTypeConfiguration<Member>
    {
        public new void Configure(EntityTypeBuilder<Member> builder)
        {
            base.Configure(builder);    
            builder.Property(M => M.CreatedAt).HasColumnName("JoinDate").HasDefaultValueSql("GETDATE()");

            builder.HasOne(m => m.MemberShip).WithMany(ms => ms.Members).HasForeignKey(m => m.MemberShipID).OnDelete(DeleteBehavior.Restrict);
        }
    }
}