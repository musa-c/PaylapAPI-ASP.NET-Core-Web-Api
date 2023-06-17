using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Paylap.DataAccess.Abstract;
using Paylap.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Paylap.DataAccess.Concrete
{
    public class PostRepository : IPostRepository
    {
        public async Task<Post> CreatePost(Post post, int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var user = await paylapDbContext.Users.FindAsync(id);
                Post _post = new() { UserId = id, Text = post.Text, TimeStamp= DateTime.Now};
                await paylapDbContext.AddAsync(_post);
                await paylapDbContext.SaveChangesAsync();
                Console.Write("aaaaaa");
                return _post;
            }
        }

        public async Task Delete(int postId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var post = paylapDbContext.Posts
                                               .Include(p => p.Likes)
                                               .Include(p => p.Dislikes)
                                               .Include(p => p.BookMarks)
                                               .Include(p => p.Comments)
    .SingleOrDefault(p => p.Id == postId);
                if (post != null)
                {
                    paylapDbContext.Likes.RemoveRange(post.Likes);
                    paylapDbContext.Comments.RemoveRange(post.Comments);
                    paylapDbContext.Dislikes.RemoveRange(post.Dislikes);
                    paylapDbContext.BookMarks.RemoveRange(post.BookMarks);

                    paylapDbContext.Posts.Remove(post);
                    await paylapDbContext.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Post>> GetAllPost()
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                        return await paylapDbContext.Posts
                            .Select(p => new Post
                            {
                                Id = p.Id,
                                UserId = p.UserId,
                                LikeCount = p.Likes.Count,
                                Likes = p.Likes,
                                BookMarks = p.BookMarks,
                                Dislikes = p.Dislikes,
                                DislikeCount = p.Dislikes.Count,
                                CommentCount = p.Comments.Count,
                                BookMarkCount = p.BookMarks.Count,
                                TimeStamp = p.TimeStamp,
                                Text = p.Text,
                                User = new User
                                {
                                    Id = p.User.Id,
                                    UserName = p.User.UserName,
                                    Avatar = p.User.Avatar ?? new byte[0],
                                    FirstName = p.User.FirstName ?? "",
                                    LastName = p.User.LastName ?? "",
                                }
                            }).ToListAsync();

                        //var jsonSettings = new JsonSerializerSettings
                        //{
                        //    NullValueHandling = NullValueHandling.Ignore
                        //};

                        //var json = JsonConvert.SerializeObject(posts, Newtonsoft.Json.Formatting.Indented, jsonSettings);
                        //return json;
            }
        }

        public async Task<Post> GetPostById(int id)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {

                var post = await paylapDbContext.Posts
                    .Where(p => p.Id == id)
                            .Select(p => new Post
                            {
                                Id = p.Id,
                                LikeCount = p.Likes.Count,
                                DislikeCount = p.Dislikes.Count,
                                CommentCount = p.Comments.Count,
                                BookMarkCount = p.BookMarks.Count,
                                TimeStamp = p.TimeStamp,
                                Text = p.Text,
                                User = new User
                                {
                                    Id = p.User.Id,
                                    UserName = p.User.UserName,
                                    Avatar = p.User.Avatar,
                                    FirstName = p.User.FirstName,
                                    LastName = p.User.LastName,
                                }
                            })
                            .SingleOrDefaultAsync();


                return post;
                //return await paylapDbContext.Posts.FindAsync(id);
            }
        }

        public async Task<List<Post>> GetPostByUserId(int userId)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var post = await paylapDbContext.Posts
                  .Where(p => p.UserId == userId)
                          .Select(p => new Post
                          {
                              UserId = p.UserId,
                              Id = p.Id,
                              LikeCount = p.Likes.Count,
                              DislikeCount = p.Dislikes.Count,
                              CommentCount = p.Comments.Count,
                              BookMarkCount = p.BookMarks.Count,
                              TimeStamp = p.TimeStamp,
                              Text = p.Text,
                          }).ToListAsync();

                return post;
            }
        }

        public async Task<Post> UpdatePost(int id, string text)
        {
            using (var paylapDbContext = new PaylapDbContext())
            {
                var updatePost = await paylapDbContext.Posts.FirstOrDefaultAsync(a => a.Id == id);
                if(updatePost != null)
                {
                    updatePost.Text = text;
                    paylapDbContext.Posts.Update(updatePost);
                    await paylapDbContext.SaveChangesAsync();
                    return updatePost;
                }
                return null;
                    
            }
        }
    }
}
