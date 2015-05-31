using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFirewallLibrary.Communication;

namespace WindowsFirewallEventListener
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new Client();

			//if (!client.IsServerOpen()) return;

			client.Start();

			if (args != null && args.Length > 0)
			{
				var eventData = args[0];
				Console.WriteLine(eventData);
				client.Send(new CommunicationObject() { Message = eventData});
			}

			//Console.ReadLine();

			client.Stop();
		}
	}
}
