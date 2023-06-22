
using Microsoft.AspNetCore.Mvc;
using Paylap.Business.Abstract;
using Paylap.Business.Concrete;
using Paylap.DataAccess.Abstract;
using Paylap.DataAccess.Concrete;

namespace PaylapAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            //builder.Services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(builder =>
            //    {
            //        builder.AllowAnyOrigin()
            //               .AllowAnyMethod()
            //               .AllowAnyHeader();
            //    });
            //});


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IBookMarkService, BookMarkService>();
            builder.Services.AddSingleton<IBookMarkRepository, BookMarkRepository>();
            builder.Services.AddSingleton<ICommentService, CommentService>();
            builder.Services.AddSingleton<ICommentRepository, CommentRepository>();
            builder.Services.AddSingleton<IDislikeService, DislikeService>();
            builder.Services.AddSingleton<IDislikeRepository, DislikeRepository>();
            builder.Services.AddSingleton<ILikeService, LikeService>();
            builder.Services.AddSingleton<ILikeRepository, LikeRepository>();
            builder.Services.AddSingleton<IPostService, PostService>();
            builder.Services.AddSingleton<IPostRepository, PostRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IUserRepository, UserRepository>();

          
            var app = builder.Build();
            
            app.UseSwagger();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            //app.UseCors(); 


            app.MapControllers();

            app.Run();
        }
    }
}