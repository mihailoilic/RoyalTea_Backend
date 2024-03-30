using AutoMapper;
using RoyalTea_Backend.Implementation.Core;
using RoyalTea_Backend.Application.UseCases.Commands.Posts;
using RoyalTea_Backend.Application.UseCases.DTO.Posts;
using RoyalTea_Backend.DataAccess;
using RoyalTea_Backend.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoyalTea_Backend.Application.Exceptions;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Posts
{
    public class EfCreatePost : EfUseCase, ICreatePost
    {
        public EfCreatePost(AppDbContext dbContext)
            :base(dbContext)
        {

        }

        public int Id => 34;

        public string Name => "Create Post";

        public string Description => "Create Post";

        public void Execute(CreatePostDto request)
        {

            if (request.ImageFile == null)
            {
                throw new UseCaseConflictException("Image not provided.");
            }

            var newPost = Mapper.Map<Post>(request);

            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(request.ImageFile.FileName);
            if (!AppConstants.AllowedImageExtensions.Contains(extension.ToLower()))
            {
                throw new UseCaseConflictException("Unsupported file type.");
            }
            var fileName = guid + extension;
            var filePath = Path.Combine("wwwroot", "Images", "Posts", fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            request.ImageFile.CopyTo(stream);

            newPost.Image = new Image { Path = fileName };

            this.DbContext.Posts.Add(newPost);
            this.DbContext.SaveChanges();
        }
    }
}
