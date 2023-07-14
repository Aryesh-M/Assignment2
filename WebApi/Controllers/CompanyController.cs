using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        [Route("api/company/getall")]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        [Route("api/company/{companyCode}")]
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        [Route("api/company/savecompany")]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto value)
        {
            var item = await _companyService.SaveCompany(_mapper.Map<CompanyInfo>(value));
            return Ok(item);
        }

        // PUT api/<controller>/5
        [Route("api/company/{id}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]CompanyDto value)
        {
            var item = await _companyService.SaveCompany(_mapper.Map<CompanyInfo>(value));
            return Ok(item);
        }

        // DELETE api/<controller>/5
        [Route("api/company/{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _companyService.DeleteCompany(id);
        }
    }
}