#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sandwichAPI.Models;


namespace sandwichAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class sandwichesController : ControllerBase
    {
        private readonly sandwichAPIDBContext _context;

        public sandwichesController(sandwichAPIDBContext context)
        {
            _context = context;
        }

        // GET: api/sandwiches
        [HttpGet]
      public async Task<ActionResult<Response>> Getsandwich()
        {
            var response = new Response();
            try
            {
                
                response.statusCode = 200;
                response.statusDescription = "List of sandwdiches found";
                response.sandwichList = await _context.sandwich.ToListAsync();
                foreach(var sand in response.sandwichList)
                {
                   var ingredients =   _context.sandwichIngredients.Where(x => x.sandwichID == sand.sandwichID).ToList();
                    sand.sandwichIngredients = ingredients[0];
                }
                return response;
            }
            catch (Exception)
            {
               if( _context.sandwich.ToListAsync().IsFaulted)
                {
                    response.statusDescription = "There is a problem with the server.";
                    response.statusCode = 500;
                    return response;
                }
            }
            return response;
            
        }
        // GET: api/sandwiches/random
        [HttpGet("random")]
        public async Task<ActionResult<Response>> GetRadnomdsandwich()
        {   
            var response = new Response();
            var rand = new Random();
            var list = await _context.sandwich.ToListAsync();
            int index = rand.Next(list.Count);
            response.sandwich = list[index];
            if (response.sandwich == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Sandwich not found";
                return response;
            }
            
            
             var ingredients = _context.sandwichIngredients.Where(x => x.sandwichID == response.sandwich.sandwichID).ToList();
            response.sandwich.sandwichIngredients = ingredients[0];
            response.statusCode = 200;
            response.statusDescription = "Try this sandwich.";
            return response;
        }

        // GET: api/sandwiches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> Getsandwich(int id)
        {
            var sandwich = await _context.sandwich.FindAsync(id);
            var sandwichIngredients = await _context.sandwichIngredients.FindAsync(id);
            var response = new Response(); 

            if (sandwich == null)
            {
                response.statusCode = 404;
                response.statusDescription = "Sandwich not found";
                return response;
            }
            response.statusCode = 200;
            response.statusDescription = "Sandwich found";
            response.sandwich = sandwich;
             var ingredients = _context.sandwichIngredients.Where(x => x.sandwichID == id).ToList();
            sandwich.sandwichIngredients = ingredients[0];
            
            return response;
        }
        
       
        // PUT: api/sandwiches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Putsandwich(int id, sandwich sandwich)
        {
            var response = new Response();
             _context.Entry(sandwich).State = EntityState.Modified;
             _context.Entry(sandwich.sandwichIngredients).State = EntityState.Modified;
             
            

 
            var name = sandwich.sandwichname;
            var givenid = sandwich.sandwichID;
            var list = await _context.sandwich.ToListAsync();
            foreach (sandwich x in list)
            {
                if (x.sandwichID != givenid && x.sandwichname == name)
                {
                    response.statusCode = 500;
                    response.statusDescription = "Duplicate name.";
                    return response;
                }
            }
            
            var ingredients = await _context.sandwichIngredients.Where(x => x.sandwichID == id && x.sandwichIngredientsID == sandwich.sandwichIngredients.sandwichIngredientsID).ToListAsync();
            if (id != sandwich.sandwichID || ingredients[0].sandwichIngredientsID != sandwich.sandwichIngredients.sandwichIngredientsID)
            {
                response.statusCode = 400;
                response.statusDescription = "Wrong sandwichId or ingredientsID.";
                return response;
            }
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!sandwichExists(id))
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
           
            response.statusCode = 200;
            response.statusDescription = "Your sandwich was updated.";
            response.sandwich = sandwich;
           response.sandwich.sandwichIngredients = ingredients[0];
            return response;
        }
       
        // POST: api/sandwiches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Response>> Postsandwich(sandwich sandwich)
        {
            var response = new Response();
            
            try
            {
           
                _context.sandwich.Add(sandwich);
            await _context.SaveChangesAsync(); 
            response.statusCode = 200;
            response.statusDescription = "Your sandwich was usccessfully uploaded.";
            response.sandwich = sandwich;
            var sandwichIngredients = await _context.sandwichIngredients.FindAsync(new { id = sandwich.sandwichID });
            
            return response;
            }
            catch (Exception)
            {
                if (sandwich.sandwichID == 0)
                {
                    response.statusCode = 500;
                    response.statusDescription = "Your sandwich could not be uploaded. Try Using a different sandwich name.";
                    return response;
                }
                else {  return response; }
            }
            
            
        }

        // DELETE: api/sandwiches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletesandwich(int id)
        {
            var sandwich = await _context.sandwich.FindAsync(id);
            if (sandwich == null)
            {
                return NotFound();
            }

            _context.sandwich.Remove(sandwich);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool sandwichExists(int id)
        {
            return _context.sandwich.Any(e => e.sandwichID == id);
        }
    }
}
