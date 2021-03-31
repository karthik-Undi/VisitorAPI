using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VisitorAPI.Models;
using VisitorAPI.Repositories;

namespace VisitorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(VisitorController));
        private readonly IVisitorRepo _context;
        public VisitorController(IVisitorRepo context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Visitors> GetAllVisitors()
        {
            _log4net.Info("Get All Visitors Was Called !!");
            return _context.GetAllVisitors();
        }
        [HttpGet("{id}")]
        public IActionResult GetVisitorById(int id)
        {
            _log4net.Info("Get Visitor By ID Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Visitor = _context.GetVisitorById(id);
                _log4net.Info("Visitor Of Id " + id + " Was Called");
                if (Visitor == null)
                {
                    return NotFound();
                }
                return Ok(Visitor);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitors(Visitors item)
        {
            _log4net.Info("Post Visitors Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var addVisitor = await _context.PostVisitor(item);
                return Ok(addVisitor);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }


    }
}
