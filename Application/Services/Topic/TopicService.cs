using Infrastructure.Ef;

namespace Application.Services.Topic;

public class TopicService : ITopicService
{
    private readonly ITopicRepository _topicRepository;

    public TopicService(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }
    
    public Domain.Topic FetchPostsByUrlTopic(string urlTopic)
    {
        var dbTopic = _topicRepository.FetchByUrl(urlTopic);
        return Mapper.GetInstance().Map<Domain.Topic>(dbTopic);
    }
}