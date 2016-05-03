using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.IPCommunication.Event
{
	public class Server
	{
		public const string CommunicationPipeName = "WindowsFirewallDashboardProcessCommunication";

		private NamedPipeServer<CommunicationObject> pipeServer;

		public Server()
		{
			pipeServer = new NamedPipeServer<CommunicationObject>(CommunicationPipeName);
			pipeServer.ClientConnected += delegate (NamedPipeConnection<CommunicationObject, CommunicationObject> connection)
			{
				Console.WriteLine($"Client {connection.Id} is now connected!");
				connection.PushMessage(new CommunicationObject { Message = "Welcome!" });
			};

			pipeServer.ClientMessage += delegate (NamedPipeConnection<CommunicationObject, CommunicationObject> connection, CommunicationObject message)
			{
				Console.WriteLine($"Client {connection.Id} says: {message.Message}");
				connection.PushMessage(new CommunicationObject { Status = CommunicationObject.CommunicationStatus.MessageRecieved });
			};
		}

		public void Start()
		{
			pipeServer.Start();
		}

		public void Stop()
		{
			pipeServer.Stop();
		}
	}
}
