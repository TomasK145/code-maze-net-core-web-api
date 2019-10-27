using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repositoryWrapper;

        public ValuesController(ILoggerManager logger, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            _repositoryWrapper = repositoryWrapper;
        }

        //GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_logger.LogInfo("Here is info from our value controller");
            //_logger.LogDebug("Here is debug from our value controller");
            //_logger.LogWarn("Here is warn from our value controller");
            //_logger.LogError("Here is error from our value controller");

            var domesticAccount = _repositoryWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repositoryWrapper.Owner.FindAll();

            return new string[] { "value1", "value2" };
        }
    }
}
