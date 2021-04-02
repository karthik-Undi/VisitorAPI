﻿using Microsoft.AspNetCore.Http;
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
            _log4net.Info("Get All Visitors was Called !!");
            return _context.GetAllVisitors();
        }
        [HttpGet("{id}")]
        public IActionResult GetVisitorById(int id)
        {
            _log4net.Info("Get Visitor By ID is Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Visitor = _context.GetVisitorById(id);
            _log4net.Info("Visitor of Id " + id + " is called");
            if (Visitor == null)
            {
                return NotFound();
            }
            return Ok(Visitor);
        }

        [HttpPost]
        public async Task<IActionResult> PostVisitors(Visitors item)
        {
            _log4net.Info("Post Visitors is called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var addVisitor = await _context.PostVisitor(item);
            return Ok(addVisitor);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdateVisitor(int id, Visitors item)
        {
            _log4net.Info("Update Visitors Was Called !!");
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updateVisitor = await _context.UpdateVisitor(id,item);
                _log4net.Info("Update Visitor By Id" + id + "Was Called !!");
                return Ok(updateVisitor);
            }
            catch(Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("RemoveVisitor/{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            _log4net.Info("Delete Visitors Was Called !!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var deleteVisitor = await _context.DeleteVisitor(id);
                _log4net.Info("Delete Visitor By Id" + id + "Was Called !!");
                return Ok(deleteVisitor);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }


    }
}
