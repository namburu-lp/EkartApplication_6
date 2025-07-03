using AutoMapper;
using ECommerceAPP.IRepository;
using ECommerceAPP.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerceAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        private readonly Zecommerce123Context _context;
        private readonly IValidator<Supplier> _validator;

        public SupplierController(
            ISupplierRepository supplierRepository,
            Zecommerce123Context context,
            IMapper mapper,
            IValidator<Supplier> validator)
        {
            _supplierRepository = supplierRepository;
            _context = context;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSupplier();
                if (suppliers == null || suppliers.Count == 0)
                    return NotFound("No suppliers found.");

                return Ok(new { message = "Suppliers retrieved successfully.", data = suppliers });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving suppliers: {ex.Message}");
            }
        }

        [HttpGet("country/{country}")]
        public async Task<IActionResult> GetSupplierByCountry(string country)
        {
            try
            {
                if (!_supplierRepository.CountryExists(country))
                    return NotFound("Country not found.");

                var result = await _supplierRepository.GetSupplierByCountry(country);
                return Ok(new { message = "Suppliers from the given country retrieved.", data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving suppliers by country: {ex.Message}");
            }
        }



            [HttpPost]
          public async Task<IActionResult> PostSupplier([FromBody] Supplier supplier)
            {
                try
                {
                    ValidationResult validationResult = await _validator.ValidateAsync(supplier);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(new
                        {
                            message = "Validation failed.",
                            errors = validationResult.Errors
                        });
                    }

                    var created = await _supplierRepository.CreateSupplier(supplier);
                    return Ok(new { message = "Supplier created successfully.", data = created });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error creating supplier: {ex.Message}");
                }
            }

            [HttpPut("edit/{id}")]
          public async Task<IActionResult> UpdateSupplier(int id, [FromBody] Supplier supplier)
            {
                if (id != supplier.SupplierId)
                    return BadRequest("Supplier ID mismatch.");

                try
                {
                    ValidationResult validationResult = await _validator.ValidateAsync(supplier);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(new
                        {
                            message = "Validation failed.",
                            errors = validationResult.Errors
                        });
                    }

                    if (!_supplierRepository.IdExists(id))
                        return NotFound("Supplier not found for update.");

                    var updated = await _supplierRepository.UpdateSupplier(supplier);
                    return Ok(new { message = "Supplier updated successfully.", data = updated });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error updating supplier: {ex.Message}");
                }
            }
        }
    }

    

