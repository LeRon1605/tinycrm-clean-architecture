using AutoMapper;

namespace Lab2.API.Services;

public class BaseService
{
    protected IMapper Mapper;
    public BaseService(IMapper mapper)
    {
        Mapper = mapper;
    }
}
