using AutoMapper;
using AutoMapper.QueryableExtensions;
using Forum_Management_System.Models.DTO;
using Forum_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Forum_Management_System.Models.View;
using Forum_Management_System.Helpers.Resolvers;

namespace Forum_Management_System.Helpers.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDTO>().ReverseMap();

            CreateMap<CreateUserDTO, User>();
            CreateMap<ICollection<User>, HashSet<GetUserDTO>>();

            CreateMap<Tag, TagViewModel>();
            CreateMap<Tag, TagDTO>();
            CreateMap<TagDTO, Tag>()
            .ForMember(dest => dest.PostTag, opt => opt.Ignore());

            CreateMap<Comment, GetCommentDTO>()
            .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ReverseMap();

            CreateMap<Post, GetPostDTO>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.Where(c => c.ParentID == null)));

            CreateMap<CreatePostDTO, Post>()
            .ForMember(dest => dest.Tags, opt => opt.Ignore())
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
                    .PreserveReferences();

            CreateMap<CreateCommentDTO, Comment>();

            CreateMap<UpdatePostDTO, Post>();

            CreateMap<UpdateCommentDTO, Comment>();

            CreateMap<ReplyDTO, Comment>();

            CreateMap<CreateGroupDTO, Group>();

            CreateMap<SignUpViewModel, CreateUserDTO>().ReverseMap();

            CreateMap<Post, PostViewModelMini>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dest => dest.HasUserLiked, opt => opt.MapFrom<UserLikesResolver>())
            .ForMember(dest => dest.HasUserDisliked, opt => opt.MapFrom<UserDislikesResolver>());

            CreateMap<Post, PostViewModel>()
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dest => dest.HasUserLiked, opt => opt.MapFrom<UserLikesResolver>())
            .ForMember(dest => dest.HasUserDisliked, opt => opt.MapFrom<UserDislikesResolver>());

            CreateMap<User, UserViewModelMini>()
            .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => Convert.ToBase64String(src.Photo)));

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.CommentCount, opt => opt.MapFrom<CommentCountResolver>())
                .ForMember(dest => dest.PostsCount, opt => opt.MapFrom<PostsCountResolver>())
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => Convert.ToBase64String(src.Photo)));

            CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.HasUserLiked, opt => opt.MapFrom<UserLikesResolver>())
            .ForMember(dest => dest.HasUserDisliked, opt => opt.MapFrom<UserDislikesResolver>());

            CreateMap<Group, GroupViewModelMini>();
            CreateMap<UserProfileViewModel, User>();
        }
    }
}
