using KruAll.Core.Models;
using KruAll.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KruAll.Core.Repositories
{
    public class KruAllEmployeeRepository : KruAllBaseRepository<Employee>
    {
        #region Constructor
        public KruAllEmployeeRepository() { }

        #endregion

        #region Methods

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<Employee> GetEmployees()
        {
            return base.GetAll().ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public Employee GetEmployeeById(int id)
        {
            return base.FindBy(e => e.ID == id).FirstOrDefault();
        }
        public Employee GetEmployeeByFirstName(string firstName)
        {
            return base.FindBy(e => e.FirstName == firstName).FirstOrDefault();
        }
        public Employee GetEmployeeByLastName(string lastName)
        {
            return base.FindBy(e => e.LastName == lastName).FirstOrDefault();
        }
        public Employee GetEmployeeByEmpNumber(int empNumber)
        {
            return base.FindBy(e => e.EmpNumber == empNumber).FirstOrDefault();
        }
        public Employee GetEmployeeByIdentification(int identification)
        {
            return base.FindBy(e => e.Identification == identification).FirstOrDefault();
        }
        public Employee GetEmployeeByPassport(string passport)
        {
            return base.FindBy(e => e.PassportID == passport).FirstOrDefault();
        }
        public List<Employee> GetEmployeesByCostCenterId(int costCenterId)
        {
            return base.FindBy(e => e.EmployeeInfo.CostCenterId == costCenterId).ToList();
        }
        public List<Employee> GetEmployeesByDepartmentId(int deptId)
        {
            return base.FindBy(e => e.EmployeeInfo.DepartmentId == deptId).ToList();
        }
        public List<Employee> GetEmployeesByLocationId(int locationId)
        {
            return base.FindBy(e => e.EmployeeInfo.LocationId == locationId).ToList();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void NewEmployee(Employee employee)
        {
            base.Add(employee);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void EditEmployee(Employee employee)
        {
            if (employee.ID == 0) return;
            base.Edit(employee);
            Save();
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DeleteEmployee(Employee employee)
        {
            if (employee.ID == 0) return;
            var currentEmployee = GetEmployeeById(employee.ID);
            Delete(currentEmployee);
            Save();
        }

        protected sealed override void Delete(Employee entity)
        {
            var employeeInfo = entity.EmployeeInfo;
            _contextPZE.Set<Employee>().Remove(entity);
            _contextPZE.Set<EmployeeInfo>().Remove(employeeInfo);
            Save();
        }

        #endregion

    }
}
