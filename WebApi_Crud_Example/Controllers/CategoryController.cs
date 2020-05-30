using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_Crud_Example.Data;



namespace WebApi_Crud_Example.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDbContext _context;

        public CategoryController(CategoryDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Category")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories.Select(c => new
            {
                c.Id,
                c.CategoryName
            }));
        }

        [HttpGet]
        [Route("Category/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                return Ok(new
                {
                    category.Id,
                    category.CategoryName
                });
            }

            return NotFound();
        }

        [HttpPost]
        [Route("Category")]
        public async Task<IActionResult> Post([FromBody] CategoryDTO model)
        {
            var category = new Categories
            {
                CategoryName = model.CategoryName
            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                category.Id,
                category.CategoryName
            });
        }

        [HttpPut]
        [Route("Category/{id}")]
        public async Task<IActionResult> Put (int id, CategoryDTO model)
        {

            var category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (category != null)
                {
                    category.CategoryName = model.CategoryName;

                    await _context.SaveChangesAsync();

                    return NoContent();
                }
          
            return NotFound();
        }

        [HttpDelete]
        [Route("Category/{id}")]
        public async Task<IActionResult> Delete(int id)
        {


            if (id > 0)
            {

                var category = await _context.Categories.Where(c => c.Id == id).FirstOrDefaultAsync();

                if (category != null)
                {
                    _context.Categories.Remove(category);


                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return NotFound();
        }
    }

    public class CategoryDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }


}