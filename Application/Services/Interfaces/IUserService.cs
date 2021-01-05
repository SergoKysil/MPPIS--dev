using Application.Dto;
using Domain.RDBMS.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);

        Task<AddUserDto> AddUser(AddUserDto addUserDto);

        Task<User> Login(LoginDto loginDto);

        Task RemoveUser(int userId);

    }
}
