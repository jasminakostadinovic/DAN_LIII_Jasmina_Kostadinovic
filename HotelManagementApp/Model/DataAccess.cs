using HotelManagementApp.DataValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace HotelManagementApp.Model
{
	class DataAccess
    {
		public bool IsCorrectUser(string userName, string password)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					var user = conn.tblUserDatas.FirstOrDefault(x => x.Username == userName);

					if (user != null)
					{
						var passwordFromDb = conn.tblUserDatas.First(x => x.Username == userName).Password;
						return SecurePasswordHasher.Verify(password, passwordFromDb);
					}
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal List<vwEmployee> LoadEmployees(string floor)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					if (conn.tblEmployees.Any(x => x.FloorNumber == floor))
						return conn.vwEmployees.Where(x => x.FloorNumber == floor).ToList();
					return new List<vwEmployee>();
				}
			}
			catch (Exception)
			{
				return new List<vwEmployee>();
			}
		}

		internal string GetTypeOfUser(int userDataId)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					if (conn.tblManagers.Any(x => x.UserDataID == userDataId))
						return nameof(tblManager);
					if (conn.tblEmployees.Any(x => x.UserDataID == userDataId))
						return nameof(tblEmployee);
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		internal tblManager LoadManagerById(int managerId)
		{

			using (var conn = new HotelManagementEntities())
			{

				return conn.tblManagers.FirstOrDefault(x => x.ManagerID == managerId);

			}
		}

		internal tblManager LoadManagerByUsername(string userName)
		{

			using (var conn = new HotelManagementEntities())
			{
				var user = conn.tblUserDatas.FirstOrDefault(x => x.Username == userName);
				if (user != null)
				{
					return conn.tblManagers.FirstOrDefault(x => x.UserDataID == user.UserDataID);
				}
				return null;
			}
		}

		internal tblEmployee LoadEmployeeByUsername(string userName)
		{

			using (var conn = new HotelManagementEntities())
			{
				var user = conn.tblUserDatas.FirstOrDefault(x => x.Username == userName);
				if (user != null)
				{
					return conn.tblEmployees.FirstOrDefault(x => x.UserDataID == user.UserDataID);
				}
				return null;
			}
		}


		internal bool IsUniqueUsername(string username)
		{
			using (var conn = new HotelManagementEntities())
			{
				return !conn.tblUserDatas.Any(x => x.Username == username);
			}
		}

		internal bool TryAddNewUserData(tblUserData userData)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					conn.tblUserDatas.Add(userData);
					conn.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal int GetUserDataId(string username)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					var user = conn.tblUserDatas.FirstOrDefault(x => x.Username == username);
					if (user != null)
						return user.UserDataID;
					return 0;
				}
			}
			catch (Exception)
			{
				return 0;
			}
		}

		internal bool TryUpdateEmloyeeSalary(int employeeId, string salary)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					var user = conn.tblEmployees.FirstOrDefault(x => x.EmployeeID == employeeId);
					if(user != null)
					{
						user.Salary = salary;
						conn.SaveChanges();
						return true;
					}
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal bool TryAddNewEmployee(tblEmployee employee)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					conn.tblEmployees.Add(employee);
					conn.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal string LoadEmployeeSex(int employeeId)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					var user = conn.tblEmployees.FirstOrDefault(x => x.EmployeeID == employeeId);
					if (user != null)
						return user.Sex;
					return null;
				}
			}
			catch (Exception)
			{
				return null;
			}
		}

		internal bool IsManagerOnTheFloor(string floorNumber)
		{
			using (var conn = new HotelManagementEntities())
			{
				return conn.tblManagers.Any(x => x.FloorNumber == floorNumber);
			}
		}

		internal bool TryRemoveUserData(int userId)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					var userToRemove = conn.tblUserDatas.FirstOrDefault(x => x.UserDataID == userId);

					if (userToRemove != null)
					{
						conn.tblUserDatas.Remove(userToRemove);
						conn.SaveChanges();
						return true;
					}
					return false;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}

		internal List<string> LoadFloorNumbers()
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					if (conn.tblManagers.Any())
					{
						return conn.tblManagers.Select(x => x.FloorNumber).ToList();
					}
					return new List<string>();
				}
			}
			catch (Exception)
			{
				return new List<string>();
			}
		}

		internal bool TryAddNewManager(tblManager manager)
		{
			try
			{
				using (var conn = new HotelManagementEntities())
				{
					conn.tblManagers.Add(manager);
					conn.SaveChanges();
					return true;
				}
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}
