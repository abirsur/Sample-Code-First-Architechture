namespace APPS08082016.Core.Response
{
    public class OperationObjectResponse<T> where T : class
    {
        public T Object { get; set; }
        public OperationResponse OperationResponse { get; set; }
    }
}