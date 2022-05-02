#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sandwichAPI.Models;
//dnt use this just testing 
namespace sandwichAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sandwichIngredientsController : ControllerBase
    {
        private readonly sandwichAPIDBContext _context;

        public sandwichIngredientsController(sandwichAPIDBContext context)
        {
            _context = context;
        }
/*
        // GET: api/sandwichIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<sandwichIngredients>>> GetsandwichIngredients()
        {
            return await _context.sandwichIngredients.ToListAsync();
        }

        // GET: api/sandwichIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<sandwichIngredients>> GetsandwichIngredients(int id)
        {
            var sandwichIngredients = await _context.sandwichIngredients.FindAsync(id);

            if (sandwichIngredients == null)
            {
                return NotFound();
            }

            return sandwichIngredients;
        }
*/      //nope
        // PUT: api/sandwichIngredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Response> PutsandwichIngredients(int id, sandwichIngredients sandwichIngredients)
        {
            
            var response = new Response();
            if (id != sandwichIngredients.sandwichIngredientsID)
            {
                response.statusCode = 400;
                response.statusDescription = "Wrong Id.";
                return response;
            }

            _context.Entry(sandwichIngredients).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sandwichIngredientsExists(id))
                {
                    response.statusCode = 404;
                    response.statusDescription = "Your sandwich could not be found";
                    return response;
                }
                else
                {
                    throw;
                }
            }
            var sandwich = await _context.sandwich.FindAsync(id);
            response.statusCode = 200;
            response.statusDescription = "Your sandwich was updated.";
            response.sandwich = sandwich;
            response.sandwich.sandwichIngredients = await _context.sandwichIngredients.FindAsync(id);
            return response;
        }
        
        // POST: api/sandwichIngredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<sandwichIngredients>> PostsandwichIngredients(sandwichIngredients sandwichIngredients)
        {
            _context.sandwichIngredients.Add(sandwichIngredients);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetsandwichIngredients", new { id = sandwichIngredients.sandwichIngredientsID }, sandwichIngredients);
        }

        // DELETE: api/sandwichIngredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletesandwichIngredients(int id)
        {
            var sandwichIngredients = await _context.sandwichIngredients.FindAsync(id);
            if (sandwichIngredients == null)
            {
                return NotFound();
            }

            _context.sandwichIngredients.Remove(sandwichIngredients);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool sandwichIngredientsExists(int id)
        {
            return _context.sandwichIngredients.Any(e => e.sandwichIngredientsID == id);
        }
    }
}
