using Application.UseCases.Topic.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Topic;

public class UseCaseFetchTopicById : IUseCaseParameterizedQuery<DtoOutputTopic, int>
{
    private readonly ITopicRepository _topicRepository;


    public UseCaseFetchTopicById(ITopicRepository topicRepository)
    {
        _topicRepository = topicRepository;
    }

    public DtoOutputTopic Execute(int idTopic)
    {
        var topic = _topicRepository.FetchById(idTopic);
        return Mapper.GetInstance().Map<DtoOutputTopic>(topic);
    }
}