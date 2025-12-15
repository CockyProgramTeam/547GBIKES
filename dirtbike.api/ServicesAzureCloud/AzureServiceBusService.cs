using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace Services
{

  public class ServiceBusService
  {
    private readonly string _connectionString;
    private readonly string _queueName;
    private readonly ServiceBusClient _client;
    private readonly ServiceBusSender _sender;

    public ServiceBusService(IConfiguration configuration)
    {
      _connectionString = configuration["ServiceBus:ConnectionString"];
      _queueName = configuration["ServiceBus:QueueName"];
      _client = new ServiceBusClient(_connectionString);
      _sender = _client.CreateSender(_queueName);
    }

    public async Task SendMessageAsync(string message)
    {
      ServiceBusMessage busMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(message));
      await _sender.SendMessageAsync(busMessage);
    }
  }
}
