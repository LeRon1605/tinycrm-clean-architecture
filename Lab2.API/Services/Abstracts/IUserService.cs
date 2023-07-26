using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Services;

public interface IUserService : IService<User, string, UserDto, UserCreateDto, UserUpdateDto>
{
}