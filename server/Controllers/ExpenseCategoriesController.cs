using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using WebApi.Dtos;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApi.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ExpenseCategoriesController : Controller
    {
        private IExpenseCategoryService _expenseCategoryService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ExpenseCategoriesController(
            IExpenseCategoryService expenseCategoryService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _expenseCategoryService = expenseCategoryService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var expenseCategories =  _expenseCategoryService.GetAll();
            var expenseCategoryDtos = _mapper.Map<IList<ExpenseCategoryDto>>(expenseCategories);
            return Ok(expenseCategoryDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var expenseCategory =  _expenseCategoryService.GetById(id);
            var expenseCategoryDto = _mapper.Map<ExpenseCategoryDto>(expenseCategory);
            return Ok(expenseCategoryDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ExpenseCategoryDto expenseCategoryDto)
        {
            // map dto to entity and set id
            var expenseCategory = _mapper.Map<ExpenseCategory>(expenseCategoryDto);
            expenseCategory.Id = id;

            try 
            {
                // save 
                _expenseCategoryService.Update(expenseCategory);
                return Ok();
            } 
            catch(AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _expenseCategoryService.Delete(id);
            return Ok();
        }
    }
}
