namespace Application.Services.Topic;

public interface ITopicService
{
    Domain.Topic FetchPostsByUrlTopic(string urlTopic);
}