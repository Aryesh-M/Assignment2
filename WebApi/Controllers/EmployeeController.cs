using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [Route("api/employee/getall")]
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployees();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/<controller>/5
        [Route("api/employee/{employeeCode}")]
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCode(employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/<controller>
        [Route("api/employee/saveemployee")]
        public async Task<IHttpActionResult> Post([FromBody]EmployeeDto value)
        {
            var item = await _employeeService.SaveEmployee(_mapper.Map<EmployeeInfo>(value));
            return Ok(item);
        }

        // PUT api/<controller>/5
        [Route("api/employee/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]EmployeeDto value)
        {
            var item = await _employeeService.SaveEmployee(_mapper.Map<EmployeeInfo>(value));
            return Ok(item);
        }

        // DELETE api/<controller>/5
        [Route("api/employee/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _employeeService.DeleteEmployee(id);
        }
    }
}