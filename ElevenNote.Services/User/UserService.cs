using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElevenNote.Data;
using ElevenNote.Data.Entities;
using ElevenNote.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ElevenNote.Services.User
{
    public class UserService : IUserService
    {
        // constructor
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model) //process response, like reverse fetch
        {
        if (await GetUserByEmailAsync(model.Email) != null || await GetUserByUserNameAsync(model.UserName) != null)
        return false;

            var entity = new UserEntity
            {
                Email = model.Email,
                UserName = model.UserName,
                DateCreated = DateTime.Now
            };
            var passwordHasher = new PasswordHasher<UserEntity>();
            entity.Password = passwordHasher.HashPassword(entity, model.Password);
            
            _context.Users.Add(entity);
            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        private async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
        }
        private async Task<UserEntity?> GetUserByUserNameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());
            
        }



//         public Task<bool> Find = RegisterUserAsync (UserEntity.UserName) => != null :
//         { return true;}
// if (Find == true) {return UserName.value
//     } else
// {
//     create Username = new UserName
}
}
