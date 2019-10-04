using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Akka.Actor;
using Akka.Configuration;
using Microsoft.Extensions.Logging;

namespace KubeNet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenController : ControllerBase
    {
        private Config config = ConfigurationFactory.ParseString(@"
                        akka {  
                            actor {
                                provider = remote
                            }
                            remote {
                                dot-netty.tcp {
                                    port = 0
                                    hostname = localhost
                                }
                            }
                        }");
        private ActorSystem system;
        private ActorSelection genServ;

            
        private readonly ILogger<WeatherForecastController> _logger;

        public GenController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            system = ActorSystem.Create("GenClient", config);
            genServ = system.ActorSelection("akka.tcp://GenServ@localhost:8081/user/GenServer");
        }

        [HttpGet]
        public string Get()
        {           
            return genServ.Ask("").Result.ToString();
        }
    }
}
