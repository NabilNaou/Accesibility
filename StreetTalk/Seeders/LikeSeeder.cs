﻿using System;
using System.Collections.Generic;
using System.Linq;
using StreetTalk.Data;
using StreetTalk.Models;

namespace StreetTalk.Seeders
{
    public class LikeSeeder : Seeder
    {
        public override bool ShouldSeed => !Context.Post.OfType<PublicPost>().Single(p => p.Id == 1).Likes.Any();

        public override void DoSeed(StreetTalkContext context, IServiceProvider services)
        {
            var firstPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 1);
            //The second and the third user like the first post
            firstPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Skip(1).First()
                    },
                    new Like
                    {
                        Post = firstPost,
                        User = Context.User.Skip(2).First()
                    }
                }
            );
            
            var secondPost = Context.Post.OfType<PublicPost>().Single(p => p.Id == 2);
            //The first user likes the second post
            secondPost.Likes.AddRange(new List<Like>
                {
                    new Like
                    {
                        Post = secondPost,
                        User = Context.User.Skip(0).First()
                    },
                }
            );
        }
    }
}