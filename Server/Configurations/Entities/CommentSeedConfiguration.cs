using GreenBook.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Server.Configurations.Entities
{
    public class CommentSeedConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(
                new Comment
                {
                    Id = 1,
                    Text = "Hello! We are travelling to Singapore.",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                },
                new Comment
                {
                    Id = 2,
                    Text = "Hello! We are eating in Thailand.",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                },
                new Comment
                {
                    Id = 3,
                    Text = "Hello! We are recommending buying this item from Swiss.",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                }
                );
        }
    }
}
