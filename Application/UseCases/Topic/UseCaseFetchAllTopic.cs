using Application.UseCases.Topic.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;
using Infrastructure.Ef.DbEntities;

namespace Application.UseCases.Topic;

public class UseCaseFetchAllTopic : IUseCaseQuery<IEnumerable<DtoOutputTopic>>
{

        private readonly ITopicRepository _topicRepository;

        public UseCaseFetchAllTopic(ITopicRepository topicRepository)
        {
                _topicRepository = topicRepository;
        }

        public IEnumerable<DtoOutputTopic> Execute()
        {
                var topics = _topicRepository.GetAllTopics();
                return Mapper.GetInstance().Map<IEnumerable<DtoOutputTopic>>(topics);        
        }
}