using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}

[System.Environment]::SetEnvironmentVariable('POPPLER_PATH', 'C:\path\to\poppler\bin', [System.EnvironmentVariableTarget]::Machine)
[System.Environment]::SetEnvironmentVariable('TESSERACT_PATH', 'C:\Program Files\Tesseract-OCR', [System.EnvironmentVariableTarget]::Machine)


    setx POPPLER_PATH "C:\path\to\poppler\bin" /M
setx TESSERACT_PATH "C:\Program Files\Tesseract-OCR" /M
