using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Users;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Implementation.UseCases.Commands.EF.Users
{
    public class EfDeleteUser : EfUseCase, IDeleteUser
    {
        public EfDeleteUser(AppDbContext dbContext)
            : base(dbContext)
        {

        }
        public int Id => 32;

        public string Name => "Delete user";

        public string Description => "Soft delete user";

        public void Execute(int request)
        {
            var user = this.DbContext.Users.Where(x => x.IsActive).FirstOrDefault(x => x.Id == request);
            if (user == null)
                throw new EntityNotFoundException();

            user.IsActive = false;
            this.DbContext.SaveChanges();
        }
    }
}
