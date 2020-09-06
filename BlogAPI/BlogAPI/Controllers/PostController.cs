using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogAPI.DatabaseContext;
using BlogAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public PostController(DataContext dataContext )
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var allPosts = _dataContext.Posts.ToList();
            return Ok(allPosts);
        }

        [Route("{Id}")]
        [HttpGet]
        public IActionResult GetPost(int Id)
        {
            var post = _dataContext.Posts.Where(s => s.Id == Id).SingleOrDefault();

            if (post == null)
                return NotFound($"No post found for id {Id}");
            return Ok();
        }

        [HttpPost]
        public IActionResult CreatePost([FromBody]Post newPost)
        {
            _dataContext.Posts.Add(newPost);
            _dataContext.SaveChanges();
            return Ok(newPost);
        }

        [HttpPut]
        public IActionResult UpdatePost(int Id,[FromBody]Post updatedPost)
        {
            var post = _dataContext.Posts.Where(s => s.Id == Id).SingleOrDefault();

            if (post == null)
                return NotFound($"No post found for id {Id}");

            post.Content = updatedPost.Content;
            post.Title = updatedPost.Title;
            post.ImageUrl = post.ImageUrl;

            _dataContext.Posts.Update(post);
            _dataContext.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletePost(int id)
        {
            var post = _dataContext.Posts.Where(s => s.Id == id).SingleOrDefault();

            if (post == null)
                return NotFound($"No post found for id {id}");

            _dataContext.Posts.Remove(post);
            _dataContext.SaveChanges();

            return Ok();
        }

    }
}
