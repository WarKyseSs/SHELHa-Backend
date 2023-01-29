using Application.UseCases.Topic.Dtos;
using Application.UseCases.Comment.Dtos;
using Application.UseCases.Post.Dtos;
using Application.UseCases.UserConnexion.Dtos;
using Application.UseCases.Articles.Dtos;
using Application.UseCases.Conversations.Dtos;
using Application.UseCases.Events.Dtos;
using Application.UseCases.Implantations.Dtos;
using Application.UseCases.Role.Dtos;
using Application.UseCases.Message.Dtos;
using Application.UseCases.Users.Dtos;
using AutoMapper;
using Domain;
using Infrastructure.Ef.DbEntities;

namespace Application;

public static class Mapper
{
    private static AutoMapper.Mapper _instance;

    public static AutoMapper.Mapper GetInstance()
    {
        return _instance ??= CreateMapper();
    }

    private static AutoMapper.Mapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<DbTopic, DtoOutputTopic>();
            cfg.CreateMap<DtoInputCreateTopic, DbTopic>();
            cfg.CreateMap<DtoInputTopic, DbTopic>();
            cfg.CreateMap<DbTopic, Topic>();

            cfg.CreateMap<DbPost, DtoOutputPost>();
            cfg.CreateMap<DtoInputCreatePost, DbPost>();
            cfg.CreateMap<DtoInputPost, DbPost>();
            cfg.CreateMap<DbPost, Post>();

            cfg.CreateMap<DbComment, DtoOutputComment>();
            cfg.CreateMap<DtoInputCreateComment, DbComment>();
            cfg.CreateMap<DtoInputComment, DbComment>();

            cfg.CreateMap<User, DtoOutputUser>();
            cfg.CreateMap<DtoConnectionUser, DbUser>();

            cfg.CreateMap<DbRole, Role>();
            cfg.CreateMap<DbRole, DtoOutputRole>();

            cfg.CreateMap<Article, DtoOutputArticle>();
            cfg.CreateMap<DbArticle, DtoOutputArticle>();
            cfg.CreateMap<DtoInputCreateArticle, DbArticle>();
            cfg.CreateMap<DtoInputArticle, DbArticle>();

            cfg.CreateMap<DbEvent, DtoOutputEvent>();
            cfg.CreateMap<DtoInputCreateEvent, DbEvent>();
            cfg.CreateMap<DtoInputEvent, DbEvent>();
            

            cfg.CreateMap<User, DtoOutputUser>();
            cfg.CreateMap<DbUser, DtoOutputUser>();
            cfg.CreateMap<DbUser, User>();
            cfg.CreateMap<DtoInputCreateUser, DbUser>();
            cfg.CreateMap<DtoInputUpdateUser, DbUser>();
            
            cfg.CreateMap<DbImplantation, Implantation>();
            cfg.CreateMap<DbImplantation, DtoOutputImplantation>();
            cfg.CreateMap<DbRole, Role>();

            //messages
            cfg.CreateMap<DtoInputMessage, DbMessage>();
            cfg.CreateMap<DbMessage, DtoOutputMessage>();
            
            //conversations
            cfg.CreateMap<DtoInputConversation, DbConversation>();
            cfg.CreateMap<DbConversation, DtoOutputConversation>();
        });
    return new AutoMapper.Mapper(config);
    }
}