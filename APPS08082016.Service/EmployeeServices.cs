using System;
using System.Collections.Generic;
using System.Linq;
using APPS08082016.Core.DTO;
using APPS08082016.Core.EntityModel;
using APPS08082016.Core.Response;
using APPS08082016.Data.Repository.Contract;
using APPS08082016.Service.Contract;

namespace APPS08082016.Service
{
    public class EmployeeServices : IEmployeeServices, IDisposable
    {
        #region Stored Procedure Definations
        private static string PROC_EMPLOYEE_GET { get; } = "proc_EmployeeInfo_Get";
        private static string PROC_EMPLOYEE_GET_BY_ID { get; } = "proc_EmployeeInfo_GetById";
        private static string PROC_EMPLOYEE_INFO_INSERT { get; } = "proc_EmployeeInfo_Insert";
        private static string PROC_EMPLOYEE_INFO_UPDATE { get; } = "proc_EmployeeInfo_UpdateById";

        #endregion

        #region Repositories
        private readonly IRepository<EmployeeInfoEntity> _employeeRepository;
        #endregion

       

        // Default Construtor
        public EmployeeServices(
            IRepository<EmployeeInfoEntity> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #region Private Methods

        private Func<IEnumerable<EmployeeInfoEntity>, IList<EmployeeInfo>> GetEmployeeInformations { get; } =
           employeeInfoEntities =>
           {
               IList<EmployeeInfo> employeeInformations = new List<EmployeeInfo>();
               foreach (EmployeeInfoEntity entity in employeeInfoEntities)
               {
                   employeeInformations.Add(new EmployeeInfo
                   {
                       FirstName = entity.FirstName,
                       MiddleName = entity.MiddleName,
                       LastName = entity.LastName,
                       Address = entity.Address,
                       Phone = entity.Phone,
                       Email = entity.Email,
                       DateofBirth = entity.DateofBirth,
                       JoinDate = entity.JoinDate,
                       Department = entity.Department,
                       NoticePeriod = entity.NoticePeriod,
                       ZipCode = entity.ZipCode
                   });
               }
               return employeeInformations;
           };

        #endregion

        #region Using Methods

        /// <summary>
        /// Get All Employees Informations
        /// </summary>
        /// <returns></returns>
        public OperationListResponse<EmployeeInfo> GetEmployeeInfo()
        {
            try
            {
                IEnumerable<EmployeeInfoEntity> employee = _employeeRepository.Table;
                if (employee == null || !employee.Any())
                    return new OperationListResponse<EmployeeInfo>
                    {
                        OperationResponse = new OperationResponse(true, -2),
                        ObjectList = null
                    };
                return new OperationListResponse<EmployeeInfo>
                {
                    OperationResponse = new OperationResponse(true, 2),
                    ObjectList = GetEmployeeInformations(employee)
                };
            }
            catch (Exception exception)
            {
                return new OperationListResponse<EmployeeInfo>
                {
                    OperationResponse = new OperationResponse(exception),
                    ObjectList = null
                };
            }
        }

        #endregion

        #region Using Stored Procedures

        #endregion

        public void Dispose()
        {
           this.Dispose();
        }
    }
}
