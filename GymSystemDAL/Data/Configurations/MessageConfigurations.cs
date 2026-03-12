using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystemDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymSystemDAL.Data.Configurations
{
    public class MessageConfigurations : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(m => m.Sender).WithMany().HasForeignKey(m => m.SenderId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(m => m.Receiver).WithMany().HasForeignKey(m => m.ReceiverId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}