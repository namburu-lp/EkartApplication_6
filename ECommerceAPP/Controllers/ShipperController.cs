using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceAPP.DTOS;
using ECommerceAPP.Repository;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

namespace ECommerceAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository;
        private readonly IMapper _mapper;
        private readonly Zecommerce123Context _zecommerce123Context;
        private readonly IValidator<Shipper> _validator;

        public ShipperController(
            IShipperRepository shipperRepository,
            Zecommerce123Context zecommerce,
            IMapper mapper,
            IValidator<Shipper> validator)
        {
            _shipperRepository = shipperRepository;
            _zecommerce123Context = zecommerce;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetShippers()
        {
            try
            {
                var shippers = await _shipperRepository.GetAllShipper();
                return Ok(new { message = "List of shippers.", data = shippers });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error retrieving shippers: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipperById([FromRoute] int id)
        {
            try
            {
                var shipper = await _shipperRepository.GetShipperById(id);
                if (shipper == null)
                    return NotFound("Shipper not found.");

                return Ok(new { message = "Shipper found.", data = shipper });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error retrieving shipper: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShipper([FromRoute] int id, [FromBody] ShipperDto shipperDto)
        {
            if (id != shipperDto.ShipperID)
                return BadRequest("Shipper ID mismatch.");

            try
            {
                var shipper = _mapper.Map<Shipper>(shipperDto);

                ValidationResult validationResult = await _validator.ValidateAsync(shipper);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = validationResult.Errors
                    });
                }

                var updated = await _shipperRepository.Updateshipper(shipper);
                if (updated == null)
                    return NotFound("Shipper not found for update.");

                return Ok(new { message = "Shipper updated successfully.", data = updated });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error updating shipper: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostShipper([FromBody] ShipperDto shipperDto)
        {
            try
            {
                var shipper = _mapper.Map<Shipper>(shipperDto);

                ValidationResult validationResult = await _validator.ValidateAsync(shipper);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Validation failed.",
                        errors = validationResult.Errors
                    });
                }

                var created = await _shipperRepository.CreateShipper(shipper);
                return Ok(new { message = "Shipper created.", data = created });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error creating shipper: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            try
            {
                var deleted = await _shipperRepository.DeleteShipperById(id);
                if (deleted != null)
                    return Ok(new { message = "Shipper deleted successfully.", data = deleted });

                return NotFound("Shipper not found for deletion.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error deleting shipper: {ex.Message}");
            }
        }
    }
}
