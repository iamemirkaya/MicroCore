using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroCore.Bus;
public class AwsBusOption
{
    public string Provider { get; set; } = "RabbitMq";
    public string Region { get; set; } = "eu-west-1";
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Address { get; set; } = "localhost";
    public int Port { get; set; } = 5672;
    public string UserName { get; set; }
    public string Password { get; set; }
}