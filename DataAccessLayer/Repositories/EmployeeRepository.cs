using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Repositories
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly IDbWrapper<Employee> _employeeDbWrapper;

		public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
		{
			_employeeDbWrapper = employeeDbWrapper;
		}

		public async Task<IEnumerable<Employee>> GetAll()
		{
			return await _employeeDbWrapper.FindAllAsync();
		}

		public async Task<Employee> GetByCode(string employeeCode)
		{
			IEnumerable<Employee> employees = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
			return employees.FirstOrDefault();
		}

		public async Task<bool> SaveEmployee(Employee employee)
		{
			var itemRepo = _employeeDbWrapper.Find(t =>
				t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
			if (itemRepo !=null)
			{
				itemRepo.EmployeeCode = employee.EmployeeCode;
				itemRepo.EmployeeName = employee.EmployeeName;
				itemRepo.Occupation = employee.Occupation;
				itemRepo.EmployeeStatus = employee.EmployeeStatus;
				itemRepo.EmailAddress = employee.EmailAddress;
				itemRepo.Phone = employee.Phone;
				itemRepo.LastModified = employee.LastModified;
				return _employeeDbWrapper.Update(employee);
			}

			return await _employeeDbWrapper.InsertAsync(employee);
		}

		public async Task<bool> DeleteEmployee(int employeeCode)
		{
			return await _employeeDbWrapper.DeleteAsync(t => t.EmployeeCode == employeeCode.ToString());
		}
	}
}
