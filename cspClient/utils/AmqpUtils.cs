using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SM.utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using static cspClient.utils.SqlHelper;

namespace cspClient.utils
{
    public class AmqpUtils
    {
        public static string amqpHost = XmlConfigHelper.GetValueByKey("AmqpHost");

        public static readonly AmqpUtils instance = new AmqpUtils();

        public ConnectionFactory factory;
        public IConnection connection;
        public IModel channel;
        public EventingBasicConsumer consumer_r;
        public EventingBasicConsumer consumer_p;
        private AmqpUtils() {

            try {
                initAmqp();
            } catch (Exception e) {
                Log.WriteLogLine("初始化amqp连接异常："+e.Message);
            }

        }
        // 初始化设置amqp
        public void initAmqp() {
            try {
                //factory = new ConnectionFactory() { HostName = "10.24.10.173" };
                if (string.IsNullOrEmpty(amqpHost))
                {
                    amqpHost = string.IsNullOrEmpty(XmlConfigHelper.GetValueByKey("AmqpHost")) ? "10.24.10.174" : XmlConfigHelper.GetValueByKey("AmqpHost");
                }
                factory = new ConnectionFactory() { HostName = amqpHost };
                //factory.Port = 5672;
                factory.Port = 54402;
                factory.Protocol = Protocols.AMQP_0_9_1;
                Dictionary<String, Object> clientProps = new Dictionary<string, object>();
                clientProps.Add("clientId", Utils.getMachineUUID());
                factory.ClientProperties = clientProps;
                string user = string.IsNullOrEmpty(XmlConfigHelper.GetValueByKey("AmqpUser")) ? "guest" : XmlConfigHelper.GetValueByKey("AmqpUser");
                string pasd = string.IsNullOrEmpty(XmlConfigHelper.GetValueByKey("AmqpPasd")) ? "guest" : XmlConfigHelper.GetValueByKey("AmqpPasd");
                factory.UserName = user.Trim();
                factory.Password = pasd.Trim();
                connection = factory.CreateConnection();

                connection.ConnectionShutdown += Connection_ConnectionShutdownAsync;
                channel = connection.CreateModel();
                
                string tId = Utils.getMachineUUID();

                channel.ExchangeDeclare(exchange: "csp", type: "topic",durable:true);

                channel.QueueDeclare(queue: "csp/register/pc_"+tId, durable: false, exclusive: false, autoDelete: false, arguments: null);               
                consumer_r = new EventingBasicConsumer(channel);
                consumer_r.Received += Consumer_Received_R;

                channel.QueueDeclare(queue: "csp/person/pc_"+tId, durable: false, exclusive: false, autoDelete: false, arguments: null);
                consumer_p = new EventingBasicConsumer(channel);
                consumer_p.Received += Consumer_Received_P;

                channel.QueueBind("csp/register/pc_" + tId , "csp", "csp.register.json.#", null);
                channel.QueueBind("csp/person/pc_" + tId, "csp", "csp.person.json.#", null);
                channel.BasicConsume(queue: "csp/register/pc_" + tId, autoAck: true, consumer: consumer_r);
                channel.BasicConsume(queue: "csp/person/pc_" + tId, autoAck: true, consumer: consumer_p);

            }
            catch (Exception e) {
                Log.WriteLogLine("初始化amqp失败:"+e.Message);
            }
        }

        public static AmqpUtils GetInstance()
        {
            if (instance == null)
            {
                return new AmqpUtils();
            }
            else {
                return instance;
            }
            
        }

        private void Consumer_Received_R(object model, BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                if (string.IsNullOrEmpty(message))
                {
                    return;
                }
                else
                {
                    try
                    {
                        
                        Dictionary<string, string> dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
                        if (("PC_" + Utils.getMachineUUID()).Equals(dic["terminalId"])) { return; }
                        //dic["confNo"].ToString();
                        //dic["terminalId"].ToString();
                        SqlHelper.CspConfRegister po = JsonConvert.DeserializeObject<SqlHelper.CspConfRegister>(message);
                        //string sql = SqlHelper.cspConfrRegister(po);
                        //DBsqliteHelper.ExecuteNonQuery(sql);
                        AmqpDataHandler.HandlerRegisterInfo(po);
                    }
                    catch (Exception e)
                    {
                        Log.WriteLogLine("登记消息处理失败:" + e.Message + "; 消息内容：" + message);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLogLine("消息接收处理异常：" + e.Message);
            }

        }
        private void Consumer_Received_P(object model, BasicDeliverEventArgs ea)
        {
            try
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                if ( string.IsNullOrEmpty(message) ) {
                    return;
                }else
                {
                    try {
                        Dictionary<string,string> dic = JsonConvert.DeserializeObject<Dictionary<string,string>>(message);
                        //dic["confNo"].ToString();
                        if (("PC_" + Utils.getMachineUUID()).Equals(dic["terminalId"].ToString())) { return; }
                        SqlHelper.CspMainPerson po = JsonConvert.DeserializeObject<SqlHelper.CspMainPerson>(message);
                        //string sql = SqlHelper.cspMainPerson(po);
                        //DBsqliteHelper.ExecuteNonQuery(sql);
                        AmqpDataHandler.HandlerMainPersonInfo(po);
                    }
                    catch (Exception e) {
                        Log.WriteLogLine("人员消息处理失败:"+e.Message+"; 消息内容："+message);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLogLine("消息接收处理异常：" + e.Message);
            }

        }

        

        public bool PushMsg(String p_msg,String p_queneName)
        {
            if ( channel == null ) {
                try
                {
                    initAmqp();
                }
                catch (Exception e)
                {
                    Log.WriteLogLine("发送消息(初始化失败)：" + e.Message);
                    return false;
                }
                return false;
            }
            try
            {
                //channel.ExchangeDeclare(exchange: "csp/register", type: "topic", durable: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: p_queneName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                byte[] msg = Encoding.UTF8.GetBytes(p_msg);
                channel.BasicPublish("", p_queneName, null, msg);
                return true;
            }
            catch (Exception e)
            {
                Log.WriteLogLine("消息推送异常：" + e.Message);
                return false;
            }

        }

        public bool PushPersonMsg(String p_msg) {
            if (channel == null )
            {
                try
                {
                    initAmqp();
                }
                catch (Exception e)
                {
                    Log.WriteLogLine("发送消息(初始化失败)：" + e.Message);
                    return false;
                }
                return false;
            }
            try
            {
                byte[] msg = Encoding.UTF8.GetBytes(p_msg);
                channel.BasicPublish(exchange:"csp", routingKey: "csp.person.json.pc", body: msg);
                return true;
            }
            catch (Exception e)
            {
                Log.WriteLogLine("人员消息推送异常：" + e.Message);
                return false;
            }
        }

        public bool PushRegisterMsg(String p_msg)
        {
            if (channel == null )
            {
                try
                {
                    initAmqp();
                }
                catch (Exception e)
                {
                    Log.WriteLogLine("发送消息(初始化失败)：" + e.Message);
                    return false;
                }
                return false;
            }
            try
            {
                byte[] msg = Encoding.UTF8.GetBytes(p_msg);
                channel.BasicPublish(exchange: "csp", routingKey: "csp.register.json.pc", body: msg);
                return true;
            }
            catch (Exception e)
            {
                Log.WriteLogLine("登记消息推送异常：" + e.Message);
                return false;
            }
        }

        private async void Connection_ConnectionShutdownAsync(object sender, ShutdownEventArgs e)
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }


        public void Close() {
            try {
                if (channel != null || channel.IsOpen)
                {
                    channel.Close();
                }
                if ( connection != null || connection.IsOpen ) {
                    connection.Close();
                }
            }
            catch (Exception e) {
                Log.WriteLogLine("amqp关闭异常:"+e.Message);
            }
        }

    }
}
