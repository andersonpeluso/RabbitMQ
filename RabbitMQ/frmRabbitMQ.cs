using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Windows.Forms;

namespace RabbitMQ
{
    public partial class frmRabbitMQ : Form
    {
        public frmRabbitMQ()
        {
            InitializeComponent();
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                string hostName = "192.168.8.194";
                string userName = "sys";
                string password = "123456";
                string queueName = "Message-Queue";

                //Cria a conexão com o RabbitMq
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                };

                //Cria a conexão
                IConnection connection = factory.CreateConnection();

                //cria a canal de comunicação com a rabbit mq
                IModel channel = connection.CreateModel();

                //Cria a fila caso não exista
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                String mensagem = txtEnvio.Text;
                byte[] body = Encoding.Default.GetBytes(mensagem);

                //Seta a mensagem como persistente
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                //Envia a mensagem para fila
                channel.BasicPublish(exchange: String.Empty, routingKey: queueName, basicProperties: properties, body: body);

                // Console.WriteLine("Mensagem enviadas para fila");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            try
            {
                string hostName = "192.168.8.194";
                string userName = "sys";
                string password = "123456";
                string queueName = "Message-Queue";

                //Cria a conexão com o RabbitMq
                var factory = new ConnectionFactory()
                {
                    HostName = hostName,
                    UserName = userName,
                    Password = password,
                };

                //Cria a conexão
                IConnection connection = factory.CreateConnection();

                //cria a canal de comunicação com a rabbit mq
                IModel channel = connection.CreateModel();

                //Cria a fila caso não exista
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                lock (channel)
                {
                    var consumer = new EventingBasicConsumer(channel)
                    {
                        ConsumerTag = Guid.NewGuid().ToString() // Tag de identificação do consumidor no RabbitMQ
                    };

                    consumer.Received += (remetente, ea) =>
                    {
                        var body = ea.Body;
                        var brokerMessage = Encoding.Default.GetString(ea.Body);

                        // Console.WriteLine($"Mensagem recebida com o valor: {brokerMessage}");

                        //Diz ao RabbitMQ que a mensagem foi lida com sucesso pelo consumidor
                        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: true);
                    };
                    // Registra os consumidor no RabbitMQ
                    // channel.BasicConsume(queueName, noAck: false, consumer: consumer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
