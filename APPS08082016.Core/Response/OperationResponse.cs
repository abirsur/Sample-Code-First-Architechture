using System;

namespace APPS08082016.Core.Response
{
    public class OperationResponse
    {
        public OperationResponse(bool opStatus, int opMessage)
        {
            this.IsOperationSuccessful = opStatus;
            this.OperationStatusMessage = GetResponseMessage(opMessage);
        }

        public OperationResponse(Exception exMessage)
        {
            this.IsOperationSuccessful = false;
            this.OperationStatusMessage = @"Exception Found. Message: " + exMessage.Message;
        }
        private string GetResponseMessage(int choiceId)
        {
            switch (choiceId)
            {
                case 2:
                    return "Operation Successful";
                case -2:
                    return "No records found. or object null";
                case -3:
                    return "Invalid Parameters";
                case -4:
                    return "Duplicate entry not allowed";
                default:
                    return "Unknown operation result";
            }
        }
        public bool IsOperationSuccessful { get; set; }
        public string OperationStatusMessage { get; set; }
        public string OperationDateTime => DateTime.UtcNow.ToString("ddMMyyyyHHmmss");
       
    }
}