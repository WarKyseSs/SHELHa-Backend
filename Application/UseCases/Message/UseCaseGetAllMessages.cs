using Application.UseCases.Message.Dtos;
using Application.UseCases.Utils;
using Infrastructure.Ef;

namespace Application.UseCases.Message;

public class UseCaseGetAllMessages : IUseCaseQuery<IEnumerable<DtoOutputMessage>>
{
    private IMessageRepository _messageRepository;

    public UseCaseGetAllMessages(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public IEnumerable<DtoOutputMessage> Execute()
    {
        var dbMessages = _messageRepository.FetchAll();
        return Mapper.GetInstance().Map<IEnumerable<DtoOutputMessage>>(dbMessages);
    }
}