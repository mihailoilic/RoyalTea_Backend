using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoyalTea_Backend.Application.UseCases.DTO.Posts
{
    public class PostDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int Views { get; set; } = 0;

        public DateTime Date { get; set; }
    }

    public class CreatePostDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
