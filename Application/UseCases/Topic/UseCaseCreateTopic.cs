using Application.UseCases.Topic.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Topic;

public class UseCaseCreateTopic : IUseCaseWriter<DtoOutputTopic, DtoInputCreateTopic>
{
    private readonly ITopicRepository _topicRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseCreateTopic(ITopicRepository topicRepository, SlugUrlProvider slugUrlProvider)
    {
        _topicRepository = topicRepository;
        _slugUrlProvider = slugUrlProvider;

    }

    public DtoOutputTopic Execute(DtoInputCreateTopic input)
    {
        var dbTopic = Mapper.GetInstance().Map<DbTopic>(input);
        dbTopic.urlTopic = _slugUrlProvider.ToUrlSlug(dbTopic.nameCat);
        dbTopic = _topicRepository.CreateTopic(dbTopic);
        return Mapper.GetInstance().Map<DtoOutputTopic>(dbTopic);
    }
}