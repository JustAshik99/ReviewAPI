using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewAPI.Data;
using ReviewAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Venue.Api.Authentication;

namespace ReviewAPI.Controllers
{
    [ApiKeyAuth]
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly Context _context;

        public ReviewController(Context context)
        {
            _context = context;
        }

        [HttpGet("{restaurant_id}")]
        public IActionResult GetReviews(string restaurant_id)
        {
            return Ok(_context.Reviews.Where(x => x.restaurantID == restaurant_id).ToList()); //linq query to search for reviews where restaurant id == parameter id
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            _context.Reviews.Add(review); //add review
            return Ok(review);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch]
        public IActionResult EditReviews(Review review)
        {
            var getReview = _context.Reviews.Where(x => x.id == review.id).FirstOrDefault(); //find review
            if (getReview == null)
            {
                var error = new Error //return if not found
                {
                    message = "no review found"
                };
                return BadRequest(error);
            }
            getReview = review; //replace review
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Where(x => x.id == id).FirstOrDefault(); //find review
            if (review == null)
            {
                var error = new Error //return if not found
                {
                    message = "no review found"
                };
                return BadRequest(error); 
            }
            _context.Reviews.Remove(review); //remove review
            return Ok(review);
        }
        public class Error
        {
            public string message { get; set; }
        }
    }
}
