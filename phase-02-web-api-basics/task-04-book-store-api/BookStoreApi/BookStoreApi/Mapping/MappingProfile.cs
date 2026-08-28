using AutoMapper;
using BookStoreApi.DTOs.Authors;
using BookStoreApi.Models;
namespace BookStoreApi.Mapping
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorResponse>();
            CreateMap<CreateAuthorRequest, Author>();
            CreateMap<UpdateAuthorRequest, Author>();

        }
    }
}
