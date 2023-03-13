using AutoMapper;
using SocialNetwork.Data.Model;
using System.Reflection;

namespace SocialNetwork.Backend.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            RunAllMaps();
        }

        private void RunAllMaps()
        {
            var methods = GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(item => item.Name.EndsWith("Models"));

            foreach (var method in methods)
                method.Invoke(this, new Object[0]);
        }

        private void PostModels()
        {
            CreateMap<Post, SocialNetwork.Backend.Model.Posts.ViewModel>()
                .ForMember(p => p.VotesSum, o => o.Ignore());

            CreateMap<SocialNetwork.Backend.Model.Posts.FormModel, Post>()
                .ForMember(p => p.SenderId, o => o.Ignore())
                .ForMember(p => p.PostId, o => o.Ignore())
                .ForMember(p => p.DataTime, o => o.Ignore())
                .ForMember(p => p.Messagescol, o => o.Ignore())
                .ForMember(p => p.Group, o => o.Ignore())
                .ForMember(p => p.Sender, o => o.Ignore())
                .ForMember(p => p.Comments, o => o.Ignore())
                .ForMember(p => p.PostViews, o => o.Ignore())
                .ForMember(p => p.PostVotes, o => o.Ignore());
        }

        private void CommentModels()
        {
            CreateMap<Comment, SocialNetwork.Backend.Model.Comments.ViewModel>()
                .ForMember(p => p.VotesSum, o => o.Ignore())
                .ForMember(p => p.CommentatorName, o => o.Ignore())
                .ForMember(p => p.CommentsCount, o => o.Ignore())
                .ForMember(p => p.VotesSum, o => o.Ignore());

            CreateMap<SocialNetwork.Backend.Model.Comments.FormModel, Comment>()
                .ForMember(p => p.SenderId, o => o.Ignore())
                .ForMember(p => p.Sender, o => o.Ignore())
                .ForMember(p => p.CommentVotes, o => o.Ignore())
                .ForMember(p => p.DateTime, o => o.Ignore())
                .ForMember(p => p.CommentId, o => o.Ignore())
                .ForMember(p => p.WasRemoved, o => o.Ignore())
                .ForMember(p => p.Post, o => o.Ignore());
        }

        private void GroupModels()
        {
            CreateMap<Group, SocialNetwork.Backend.Model.Groups.ViewModel>()
               .ForMember(p => p.CreatorId, o => o.Ignore());


            CreateMap<SocialNetwork.Backend.Model.Groups.FormModel, Group>()
                .ForMember(p => p.Name, o => o.Ignore())
                .ForMember(p => p.Description, o => o.Ignore())
                .ForMember(p => p.ConversationId, o => o.Ignore())
                .ForMember(p => p.Creator, o => o.Ignore())
                .ForMember(p => p.Posts, o => o.Ignore());

        }
        private void UserModels()
        {
            CreateMap<User, SocialNetwork.Backend.Model.Users.ViewModel>();
        }
    }
}
