using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Posts;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Posts
{
    public class EfDeletePost : EfUseCase, IDeletePost
    {
        public EfDeletePost(AppDbContext dbContext)
            :base(dbContext)
        {

        }
        public int Id => 35;

        public string Name => "Delete Post";

        public string Description => "Delete Post";

        public void Execute(int request)
        {
            var post = this.DbContext.Posts.Include(x => x.Image).FirstOrDefault(x => x.IsActive && x.Id == request);
            if (post == null)
                throw new EntityNotFoundException();

            post.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
