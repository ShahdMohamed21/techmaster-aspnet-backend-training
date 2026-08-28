using AutoMapper;
using BookStoreApi.DTOs.Authors;
using BookStoreApi.DTOs.Books;
using BookStoreApi.DTOs.Categories;
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

            CreateMap<Category, CategoryResponse>();
            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>()
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore()); // يعني ملوش دعوه بال id عشان انا الي هحطه

            CreateMap<CreateBookRequest, Book>();
            CreateMap<UpdateBookRequest, Book>()
             .ForMember(dest => dest.BookId, opt => opt.Ignore());
            CreateMap<Book, BookResponse>();
                

        }
    }
}
