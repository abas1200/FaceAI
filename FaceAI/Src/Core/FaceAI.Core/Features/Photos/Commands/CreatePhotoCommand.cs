using FaceAI.Application.Features.Photos.Requests;
using FaceAI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAI.Application.Features.Photos.Commands
{

    public class CreatePhotoCommand : IRequest<List<Guid>>
    {
        public CreatePhotoCommand(string userName, List<PhotoRequest> requests)
        {
            UserName = userName;
            Items = requests;

            foreach (var item in Items) {
                var errors = item.Validate().ToArray();
                throw new Exception($"request is not valid {string.Join(",", errors).Trim(',')}");

            } 

        }
        public string UserName { get; set; }
        public List<PhotoRequest> Items { get; set; }
    }
   
}
