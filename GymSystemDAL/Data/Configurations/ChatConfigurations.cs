using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemDAL.Data.Configurations
{
    public class ChatConfigurations : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.HasMany(p => p.Messages).WithOne(p => p.Chat);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
        }
    }
}