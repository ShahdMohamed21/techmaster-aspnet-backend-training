using BookStoreApi.DTOs.Authors;

namespace BookStoreApi.Services
{
    public interface IAuthorService
    {
        public AuthorResponse CreateAuthor(CreateAuthorRequest request);
        public List<AuthorResponse> GetAllAuthors();
        public AuthorResponse? UpdateAuthor(int id,UpdateAuthorRequest request);
        public AuthorResponse? DeleteAuthor(int id);
        public AuthorResponse? GetAuthorById(int id);
    }
}
