using System.Collections.Generic;

namespace APPS08082016.Core.Response
{
    public class OperationListResponse<T> where T : class
    {
        public IList<T> ObjectList { get; set; }
        public OperationResponse OperationResponse { get; set; }
       
    }
}