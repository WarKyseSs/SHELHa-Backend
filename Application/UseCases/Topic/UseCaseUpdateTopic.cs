using Application.UseCases.Topic.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;
using Infrastructure.Utils;

namespace Application.UseCases.Topic;

public class UseCaseUpdateTopic: IUseCaseWriter<bool, DtoInputTopic>
{  
    private readonly ITopicRepository _topicRepository;
    private readonly SlugUrlProvider _slugUrlProvider;

    public UseCaseUpdateTopic(ITopicRepository topicRepository, SlugUrlProvider slugUrlProvider)
    {
        _topicRepository = topicRepository;
        _slugUrlProvider = slugUrlProvider;

    }

    public bool Execute(DtoInputTopic input)
    {
        var dbTopic = Mapper.GetInstance().Map<DbTopic>(input);
        dbTopic.urlTopic = _slugUrlProvider.ToUrlSlug(dbTopic.nameCat);
        return _topicRepository.UpdateTopic(dbTopic);
    }
}