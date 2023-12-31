using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiApi.Context;
using MiApi.Models;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly MyDbContext _context;

        public BlogController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            return await _context.Blogs.ToListAsync();
        }

        // GET: api/Blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }

        // PUT: api/Blog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
          if (_context.Blogs == null)
          {
              return Problem("Entity set 'MyDbContext.Blogs'  is null.");
          }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        // DELETE: api/Blog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
