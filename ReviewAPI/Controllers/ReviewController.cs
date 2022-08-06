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
    //[ApiKeyAuth]
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
            return Ok(_context.Reviews.Where(x => x.restaurantID == restaurant_id).ToList());
        }

        [HttpPost]
        public IActionResult AddReview(Review review)
        {
            _context.Reviews.Add(review);
            return Ok(review);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPatch]
        public IActionResult EditReviews(Review review)
        {
            var getReview = _context.Reviews.Where(x => x.id == review.id).FirstOrDefault();
            if (getReview == null)
            {
                var error = new Error
                {
                    message = "no review found"
                };
                return BadRequest(error);
            }
            getReview = review;
            return Ok(review);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReview(int id)
        {
            var review = _context.Reviews.Where(x => x.id == id).FirstOrDefault();
            if (review == null)
            {
                var error = new Error
                {
                    message = "no review found"
                };
                return BadRequest(error);
            }
            _context.Reviews.Remove(review);
            return Ok(review);
        }
        public class Error
        {
            public string message { get; set; }
        }
    }
}
