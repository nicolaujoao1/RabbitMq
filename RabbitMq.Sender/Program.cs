using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "hello",
                     durable: false,
                     autoDelete: false,
                     exclusive: false,
                     arguments: null);
Console.WriteLine("Digite mensagem e aperte <ENTER>");

while (true)
{
    string message = Console.ReadLine();

    if (string.IsNullOrEmpty(message))
        break;

    var aluno = new Aluno { Id = 1, Nome = "Ilola Nicolau João" };

    message=JsonSerializer.Serialize(aluno);

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty, routingKey: "hello", basicProperties: null, body: body);

    Console.WriteLine($" [x] Envio {message}");
}


class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; }=string.Empty;  
}