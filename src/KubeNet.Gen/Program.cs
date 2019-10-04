using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Configuration;

namespace KubeNet.Gen
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
                                    port = 8081
                                    hostname = 0.0.0.0
                                    public-hostname = localhost
                                }
                            }
                        }");
            using (var system = ActorSystem.Create("GenServ", config))
            {
                system.ActorOf(Props.Create(() => new GenServer()), "GenServer");
                Console.WriteLine("Press [Enter] to close");
                var key =Console.ReadKey();
                if(key.Key == ConsoleKey.Enter) {
                    system.Terminate().Wait();
                }
            }
        }
    }

    class GenServer : ReceiveActor, ILogReceive
    {
        public GenServer()
        {            
            Receive<string>(message =>
            {
                Sender.Tell("You have reached Gen", Self);
            });
        }

        
    }
}
