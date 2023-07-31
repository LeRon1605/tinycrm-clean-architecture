using AutoMapper;
using Lab2.API.Dtos;
using Lab2.Domain.Entities;

namespace Lab2.API.Mapper;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<PagedResultDto<User>, PagedResultDto<UserDto>>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}