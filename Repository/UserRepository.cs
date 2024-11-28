﻿using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalSystemAPI.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly CarRentalContext _context;

        public UserRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }


        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
