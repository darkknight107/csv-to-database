using core;
using Microsoft.AspNetCore.Mvc;

namespace api
{
    
    [Route("file")]
    public abstract class FileController : ControllerBase
    {
        protected ICqrsDispatcher CqrsDispatcher { get; set; }

        public FileController(ICqrsDispatcher cqrsDispatcher)
        {
            CqrsDispatcher = cqrsDispatcher;
        }
    }
}