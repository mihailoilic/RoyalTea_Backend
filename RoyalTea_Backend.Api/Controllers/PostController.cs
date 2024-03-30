using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalTea_Backend.Application;
using RoyalTea_Backend.Application.Exceptions;
using RoyalTea_Backend.Application.UseCases.Commands.Posts;
using RoyalTea_Backend.Application.UseCases.DTO.Posts;
using RoyalTea_Backend.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RoyalTea_Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private AppDbContext DbContext { get; set; }
        private IUseCaseHandler Handler { get; set; }
        public PostController(AppDbContext dbContext, IUseCaseHandler handler)
        {
            this.DbContext = dbContext;
            this.Handler = handler;
        }


        // GET: api/<PostController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            var posts = this.DbContext.Posts.Include(x => x.Image).Where(x => x.IsActive).ToList();
            var postDtos = posts.Select(x =>
            {
                var postDto = Mapper.Map<PostDto>(x);
                postDto.Date = x.CreatedAt;
                postDto.Image = x.Image.Path;
                return postDto;
            });

            return Ok(new { items = postDtos});
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(int id)
        {
            var post = this.DbContext.Posts.Include(x => x.Image).FirstOrDefault(x => x.IsActive && x.Id == id);
            if (post == null)
                throw new EntityNotFoundException();

            var postDto = Mapper.Map<PostDto>(post);
            postDto.Date = post.CreatedAt;
            postDto.Image = post.Image.Path;

            post.Views += 1;
            this.DbContext.SaveChanges();

            return Ok(postDto);
        }

        // POST api/<PostController>
        [HttpPost]
        public IActionResult Post([FromForm] CreatePostDto request, [FromServices] ICreatePost command)
        {
            this.Handler.Handle(command, request);

            return StatusCode((int)HttpStatusCode.Created);
        }


        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public IActionResult  Delete(int id, [FromServices] IDeletePost command)
        {
            this.Handler.Handle(command, id);

            return StatusCode((int)HttpStatusCode.NoContent);
        }
    }
}
