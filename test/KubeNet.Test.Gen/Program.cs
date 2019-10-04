using System;
using Akka.Actor;
using Akka.Configuration;

namespace KubeNet.Test.Gen
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationFactory.ParseString(@"
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
                        }
                        ");

            using (var system = ActorSystem.Create("GenClient", config))
            {
                var genClient = system.ActorSelection("akka.tcp://GenServ@localhost:8081/user/GenServer");
                var result = genClient.Ask("").Result;
                Console.WriteLine(result);
                system.Terminate().Wait();
            }
        }
    }
}