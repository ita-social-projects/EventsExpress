using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.ValueResolvers
{
    public class ChatRoomToUserChatViewModelResolver : IValueResolver<ChatRoom, UserChatViewModel, IEnumerable<UserPreviewViewModel>>
    {
        public IEnumerable<UserPreviewViewModel> Resolve(ChatRoom source, UserChatViewModel destination, IEnumerable<UserPreviewViewModel> destMember, ResolutionContext context)
        {
            var res = new List<UserPreviewViewModel>();

            foreach (var u in source.Users)
            {
                res.Add(new UserPreviewViewModel
                {
                    Id = u.UserId,
                    Birthday = u.User.Birthday,
                    Username = u.User.Name ?? u.User.Email.Substring(0, u.User.Email.IndexOf("@")),
                });
            }

            return res;
        }
    }
}
