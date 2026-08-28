using AutoMapper;
using BookStoreApi.DTOs.Authors;
using BookStoreApi.Models;

namespace BookStoreApi.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IMapper _mapper;
        private readonly List<Author> authors = new List<Author>
        {
            new Author()
            {
                AuthorId = 1,
                FullName = "Shahd Mohamed",
                Country="Egypt",
                BirthDate=new DateOnly(2005, 9, 20),
                CreatedAt=DateTime.Now,
            }
            ,
            new Author()
            {
                AuthorId = 2,
                FullName = "Ahmed Mostafa",
                Country="Saudi Arabia",
                BirthDate=new DateOnly(2000, 11, 21),
                CreatedAt=DateTime.Now,
            },
            new Author()
            {
                AuthorId = 3,
                FullName = "Mona Yasser",
                Country="Jordan",
                BirthDate=new DateOnly(1999, 12, 14),
                CreatedAt=DateTime.Now,
            }
        };  // seed data
        private int NextId = 4;

        public AuthorService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public AuthorResponse CreateAuthor(CreateAuthorRequest request)
        {
            if(string.IsNullOrWhiteSpace(request.FullName))
            {
                throw new ArgumentException("Author Name Is Required");
            }
            var author = _mapper.Map<Author>(request);
            author.AuthorId = NextId++;
            author.CreatedAt= DateTime.Now;
            authors.Add(author);
            var AuthorResponse=_mapper.Map<AuthorResponse>(author);
            return AuthorResponse;

        }

        public List<AuthorResponse> GetAllAuthors()
        {

            var authorsresponse = _mapper.Map<List<AuthorResponse>>(authors);
            return authorsresponse;
        }

       public AuthorResponse? GetAuthorById(int id)
        {
            var author = authors.FirstOrDefault(x => x.AuthorId == id);
            if(author == null)
            {
                return null;
            }
            var authorresponse=_mapper.Map<AuthorResponse>(author);
            return authorresponse;
        }

        public AuthorResponse? UpdateAuthor(int id, UpdateAuthorRequest request)
        {
            var author = authors.FirstOrDefault(x => x.AuthorId == id);

            if (author == null)
            {
                return null;
            }

            _mapper.Map(request, author);

            return _mapper.Map<AuthorResponse>(author);
        }

        AuthorResponse? IAuthorService.DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }
    }
}
