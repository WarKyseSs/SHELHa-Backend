namespace Application.Services.Post;

public interface IPostService
{
    Domain.Post FetchByUrlPost(string urlPost);
}