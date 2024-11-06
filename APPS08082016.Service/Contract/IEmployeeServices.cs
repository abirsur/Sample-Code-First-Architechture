using APPS08082016.Core.DTO;
using APPS08082016.Core.Response;

namespace APPS08082016.Service.Contract
{
    public interface IEmployeeServices
    {
        OperationListResponse<EmployeeInfo> GetEmployeeInfo();
    }
}

#------------------------------------------
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Python: FastAPI (Poetry)",
            "type": "python",
            "request": "launch",
            "program": "${workspaceFolder}/path/to/your/main.py",
            "args": [
                "run",
                "--host",
                "127.0.0.1",
                "--port",
                "8000"
            ],
            "console": "integratedTerminal",
            "env": {
                "POETRY_ACTIVE": "1"
            },
            "envFile": "${workspaceFolder}/.env"
        }
    ]
}
