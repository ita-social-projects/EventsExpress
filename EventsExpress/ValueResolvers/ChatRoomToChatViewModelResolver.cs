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
    public class ChatRoomToChatViewModelResolver : IValueResolver<ChatRoom, ChatViewModel, IEnumerable<UserPreviewViewModel>>
    {
        private IPhotoService photoService;

        public ChatRoomToChatViewModelResolver(IPhotoService photoService)
        {
            this.photoService = photoService;
        }

        public IEnumerable<UserPreviewViewModel> Resolve(ChatRoom dto, ChatViewModel viewModel, IEnumerable<UserPreviewViewModel> dest, ResolutionContext context)
        {
            var res = new List<UserPreviewViewModel>();

            foreach (var u in dto.Users)
            {
                res.Add(new UserPreviewViewModel
                {
                    Id = u.UserId,
                    Birthday = u.User.Birthday,
                    Username = u.User.Name ?? u.User.Email.Substring(0, u.User.Email.IndexOf("@")),
                    PhotoUrl = photoService.GetPhotoFromAzureBlob($"users/{u.UserId}/photo.png").Result,
                });
            }

            return res;
        }
    }
}
