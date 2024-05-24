using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection=factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     autoDelete: false,
                     exclusive: false,
                     arguments: null);
Console.WriteLine(" [*] Aguardando mensagens.");

var consumer=new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message=Encoding.UTF8.GetString(body);  

    Console.WriteLine($" [x] Recebido: {message}"); 
};

channel.BasicConsume(queue:"hello", autoAck: true,consumer:consumer);

Console.WriteLine("Aperte [enter] para sair.");
Console.ReadLine();

class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
}