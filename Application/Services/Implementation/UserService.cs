using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Application.Dto;
using System;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Services.Interfaces;
using Domain.RDBMS.Entities;
using Domain.RDBMS;
using Infrastructure;

namespace Application.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly AppDbContext _context;

        public UserService(IRepository<User> userRepository, IMapper mapper, AppDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
            _mapper = mapper;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<AddUserDto> AddUser(AddUserDto addUserDto)
        {
            if (await _userRepository.FindByCondition(u => u.Email == addUserDto.Email) == null)
            {
                var user = _mapper.Map<User>(addUserDto);              

                user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

                _userRepository.Add(user);
                await _userRepository.SaveChangesAsync();
                return _mapper.Map<AddUserDto>(user);
            }
            else
                return null;
        }

        public async Task<UserDto> FindByEmailAsync(string email)
        {
            var user = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == email);
                

            if (user == null || user.IsEmailConfirmed == false)
            {
                throw new ObjectNotFoundException($"There is no user with email = {email} in database");
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetById(Expression<Func<User, bool>> predicate)
        { 
            var user = await _userRepository.GetAll()
                .Include(p => p.Location)
                .Include(p => p.Role)
                .FirstOrDefaultAsync(predicate);

            if (user == null)
                return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task RemoveUser(int userId)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            var user = _userRepository.GetAll().FirstOrDefault(user => user.Id == userId);
            if (user == null)
            {
                throw new ObjectNotFoundException($"There is no user with id = {userId}");
            }
            user.IsDeleted = true;
            var affectedRows = await _userRepository.SaveChangesAsync();
            if (affectedRows == 0)
            {
                throw new DbUpdateException();
            }
            await transaction.CommitAsync();

        }
    }
}
