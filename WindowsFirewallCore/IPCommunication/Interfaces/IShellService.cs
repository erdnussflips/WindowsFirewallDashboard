	using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallCore.IPCommunication.Interfaces
{
	//[ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]

	[ServiceContract]
	public interface IShellService
	{
		[OperationContract]
		void GetApplicationStatus(string applicationFilePath);

		[OperationContract]
		void AddApplicationRule(string applicationFilePath);

		[OperationContract]
		void RemoveApplicationRule(string applicationFilePath);
	}
}
