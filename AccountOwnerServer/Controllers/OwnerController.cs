using Contracts;
using Entities.Extensions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AccountOwnerServer.Controllers
{
    [Route("api/owner")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repository;

        public OwnerController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
            try
            {
                var owners = _repository.Owner.GetAllOwners();
                _logger.LogInfo("Returned all owners from database");

                return Ok(owners);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong - ex: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerById(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id {id} does not exist in DB.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");
                    return Ok(owner);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ex: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Owner owner)
        {
            try
            {
                if (owner.IsObjectNull())
                {
                    _logger.LogError("Owner object from client is null");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object from client");
                    return BadRequest("Invalid model object");
                }

                _repository.Owner.CreateOwner(owner);
                _repository.Save();

                return CreatedAtRoute("OwnerById", new { id = owner.Id }, owner);
                /*
                 CreatedAtRoute will return a status code 201, which stands for Created as explained in 
                 our post: The HTTP Reference. Also, it will populate the body of the response with the 
                 new owner object as well as the Location attribute within the response header with the 
                 address to retrieve that owner. You need to provide the name of the action, where you 
                 can retrieve the created entity.
                 */

            }
            catch (Exception ex)
            {
                _logger.LogError($"Err: {ex.Message}");
                return StatusCode(500, "internal server error");
            }
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
            try
            {
                var owner = _repository.Owner.GetOwnerWithDetails(id);

                if (owner.IsEmptyObject())
                {
                    _logger.LogError($"Owner with id {id} does not exist");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with details for id: {id}");
                    return Ok(owner);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ex: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
