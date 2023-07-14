using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Database
{
	public class InMemoryDatabase<T> : IDbWrapper<T> where T : DataEntity
	{
		private static Dictionary<Tuple<string, string>, DataEntity> DatabaseInstance;

		static InMemoryDatabase()
		{
			DatabaseInstance = new Dictionary<Tuple<string, string>, DataEntity>();
		}

		public bool Insert(T data)
		{
			try
			{
				if(data.EmployeeCode != null)
				{
					DatabaseInstance.Add(Tuple.Create(data.EmployeeCode, data.CompanyCode), data);
				}
				else
				{
					DatabaseInstance.Add(Tuple.Create(data.SiteId, data.CompanyCode), data);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool Update(T data)
		{
			try
			{
				if (data.EmployeeCode != null)
				{
					if (DatabaseInstance.ContainsKey(Tuple.Create(data.EmployeeCode, data.CompanyCode)))
					{
						DatabaseInstance.Remove(Tuple.Create(data.EmployeeCode, data.CompanyCode));
						Insert(data);
						return true;
					}

					return false;
				}
				else
				{
					if (DatabaseInstance.ContainsKey(Tuple.Create(data.SiteId, data.CompanyCode)))
					{
						DatabaseInstance.Remove(Tuple.Create(data.SiteId, data.CompanyCode));
						Insert(data);
						return true;
					}

					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				return entities.Where(expression.Compile());
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public IEnumerable<T> FindAll()
		{
			try
			{
				return DatabaseInstance.Values.OfType<T>();
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
		}

		public bool Delete(Expression<Func<T, bool>> expression)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(expression.Compile()).ToList();
				foreach (var dataEntity in entity)
				{
					if(dataEntity.EmployeeCode != null)
					{
						DatabaseInstance.Remove(Tuple.Create(dataEntity.EmployeeCode, dataEntity.CompanyCode));
					}
					else
					{
						DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					}
				}
				
				return true;
			}
			catch (Exception error)
			{
				return false;
			}
		}

		public bool DeleteAll()
		{
			try
			{
				DatabaseInstance.Clear();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool UpdateAll(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			try
			{
				var entities = FindAll();
				var entity = entities.Where(filter.Compile());
				foreach (var dataEntity in entity)
				{
					var newEntity = UpdateProperty(dataEntity, fieldToUpdate, newValue);
					if(dataEntity.EmployeeCode != null)
					{
						DatabaseInstance.Remove(Tuple.Create(dataEntity.EmployeeCode, dataEntity.CompanyCode));
					}
					else
					{
						DatabaseInstance.Remove(Tuple.Create(dataEntity.SiteId, dataEntity.CompanyCode));
					}
					Insert(newEntity);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		private T UpdateProperty(T dataEntity, string key, object value)
		{
			Type t = typeof(T);
			var prop = t.GetProperty(key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

			if (prop == null)
			{
				throw new Exception("Property not found");
			}

			prop.SetValue(dataEntity, value, null);
			return dataEntity;
		}

		public Task<bool> InsertAsync(T data)
		{
			return Task.FromResult(Insert(data));
		}

		public Task<bool> UpdateAsync(T data)
		{
			return Task.FromResult(Update(data));
		}

		public Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Find(expression));
		}

		public Task<IEnumerable<T>> FindAllAsync()
		{
			return Task.FromResult(FindAll());
		}

		public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
		{
			return Task.FromResult(Delete(expression));
		}

		public Task<bool> DeleteAllAsync()
		{
			return Task.FromResult(DeleteAll());
		}

		public Task<bool> UpdateAllAsync(Expression<Func<T, bool>> filter, string fieldToUpdate, object newValue)
		{
			return Task.FromResult(UpdateAll(filter, fieldToUpdate, newValue));
		}

	
	}
}
