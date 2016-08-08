using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}