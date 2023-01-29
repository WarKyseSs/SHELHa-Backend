using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Topic;

public class UseCaseDeleteTopic : IUseCaseWriter<bool, int>
{
    private readonly ITopicRepository _topicRepository;

    public UseCaseDeleteTopic(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public bool Execute(int idTopic)
    {
        var dbTopic = _topicRepository.DeleteTopic(idTopic);
        return dbTopic;
    }
}