using GreenBook.Client.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenBook.Server.Configurations.Entities
{
    public class PostSeedConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post
                {
                    Id = 1,
                    Title = "Adventure Time",
                    Text = "Hello! We are travelling to Singapore.",
                    Location = "Singapore",
                    PicUrl = "singapore/picture",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                },
                new Post
                {
                    Id = 2,
                    Title = "Food Time",
                    Text = "Hello! We are eating in Thailand.",
                    Location = "Thailand",
                    PicUrl = "thailand/picture",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                },
                new Post
                {
                    Id = 3,
                    Title = "Recommendation Time",
                    Text = "Hello! We are recommending buying this item from Swiss.",
                    Location = "Switzerland",
                    PicUrl = "switzerland/picture",
                    DateCreate = DateTime.Now,
                    DateUpdate = DateTime.Now,
                    CreateBy = "System",
                    UpdateBy = "System"
                }
                );
        }
    }
}
