using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Commandline.Parameter;
using WindowsAdvancedFirewallApi.Commandline.Parameter.FirewallRule;

namespace WindowsAdvancedFirewallApi.Commandline.Objects
{
	public class FirewallRule
	{
		public NameParameter Name { get; }
		public DirectionParameter Direction { get; }
		public ActionParameter Action { get; }

		public ProgramParameter Program;
		public ServiceParameter Service;
		public DescriptionParameter Description;

		private EnableParameter _enable;
		public EnableParameter Enable
		{
			get
			{
				if(_enable == null)
				{
					_enable = new EnableParameter(EnableParameter.Value.Default);
				}
				return _enable;
			}
			set
			{
				if (_enable != value)
				{
					_enable = value;
				}
			}
		}

		private List<ProfileParameter> _profiles;
		public void Profiles(params ProfileParameter[] value)
		{
			if (value != null)
			{
				foreach (var profile in value)
				{
					if (!profile.UsableInRule) throw new ArgumentException("The profile " + profile.Value + " can't used in rules.");
				}
			}

			_profiles = new List<ProfileParameter>(value);
		}
		public ProfileParameter[] Profiles()
		{
			if (_profiles == null || _profiles.Count == 0)
			{
				_profiles = new List<ProfileParameter> { ProfileParameter.DefaultInRule };
			}
			return _profiles.ToArray();
		}

		private LocalIpAddressParameter _localIp;
		public LocalIpAddressParameter LocalIp
		{
			get
			{
				if (_localIp == null)
				{
					_localIp = new LocalIpAddressParameter(LocalIpAddressParameter.Value.Default);
				}

				return _localIp;
			}
			set
			{
				if (_localIp != value)
				{
					_localIp = value;
				}
			}
		}

		private RemoteIpAddressParameter _remoteIp;
		public RemoteIpAddressParameter RemoteIp
		{
			get
			{
				if (_remoteIp == null)
				{
					_remoteIp = new RemoteIpAddressParameter(RemoteIpAddressParameter.Value.Default);
				}

				return _remoteIp;
			}
			set
			{
				if (_remoteIp != value)
				{
					_remoteIp = value;
				}
			}
		}

		private LocalPortParameter _localPort;
		public LocalPortParameter LocalPort
		{
			get
			{
				if (_localPort == null)
				{
					_localPort = new LocalPortParameter(LocalPortParameter.Value.Default);
				}

				return _localPort;
			}
			set
			{
				if (_localPort != value)
				{
					_localPort = value;
				}
			}
		}

		private RemotePortParameter _remotePort;
		public RemotePortParameter RemotePort
		{
			get
			{
				if (_remotePort == null)
				{
					_remotePort = new RemotePortParameter(RemotePortParameter.Value.Default);
				}

				return _remotePort;
			}
			set
			{
				if (_remotePort != value)
				{
					_remotePort = value;
				}
			}
		}

		private ProtocolParameter _protocol;
		public ProtocolParameter Protocol
		{
			get
			{
				if (_protocol == null)
				{
					_protocol = new ProtocolParameter(ProtocolParameter.Value.Default);
				}

				return _protocol;
			}
			set
			{
				if (_protocol != value)
				{
					_protocol = value;
				}
			}
		}

		private InterfaceTypeParameter _interfaceType;
		public InterfaceTypeParameter InterfaceType
		{
			get
			{
				if (_interfaceType == null)
				{
					_interfaceType = new InterfaceTypeParameter(InterfaceTypeParameter.Value.Default);
				}

				return _interfaceType;
			}
			set
			{
				if (_interfaceType != value)
				{
					_interfaceType = value;
				}
			}
		}

		public RemoteComputerGroupParameter RemoteComputerGroup;
		public RemoteUserGroupParameter RemoteUserGroup;
		public EdgeParameter Edge;
		public SecurityParameter Security;

		public FirewallRule(NameParameter name, DirectionParameter direction, ActionParameter action)
		{
			if (name == null) throw new ArgumentNullException(nameof(name));
			if (direction == null) throw new ArgumentNullException(nameof(direction));
			if (action == null) throw new ArgumentNullException(nameof(action));

			Name = name;
			Direction = direction;
			Action = action;
		}

		public List<Exception> checkParameter()
		{
			var errors = new List<Exception>();

			if(LocalPort.ParameterValues.Contains(LocalPortParameter.Value.RPC)
				|| LocalPort.ParameterValues.Contains(LocalPortParameter.Value.RPC_EPMAP))
			{
				if(Protocol.ParameterValue != ProtocolParameter.Value.TCP
					|| Direction.ParameterValue != DirectionParameter.Value.Inbound)
				{
					errors.Add(new ArgumentException(
						nameof(LocalPort) + " or " + nameof(LocalPort),
						"The selected option value requires that option "
						+ nameof(Protocol) + "is TCP and option "
						+ nameof(Direction) + "is inbound. More information at "
						+ NetshConstants.TECHNET_NETSH_DOCU.AbsolutePath)
					);
				}
			}

			if(RemoteComputerGroup != null || RemoteUserGroup != null)
			{
				if(Security.ParameterValue != SecurityParameter.Value.Authenticate
					|| Security.ParameterValue != SecurityParameter.Value.AuthenticateEncryption)
				{
					errors.Add(new ArgumentException(
						nameof(RemoteComputerGroup) + " or " + nameof(RemoteComputerGroup),
						"The selected option value requires that option " + nameof(Security)
						+ "set to authenticate or authenicate encryption. More information at "
						+ NetshConstants.TECHNET_NETSH_DOCU.AbsolutePath)
					);
				}
			}

			if(Edge != null && Edge.ParameterValue != EdgeParameter.Value.No)
			{
				if (Direction.ParameterValue != DirectionParameter.Value.Inbound)
				{
					errors.Add(new ArgumentException(
						nameof(Edge) + " or " + nameof(RemoteComputerGroup),
						"The selected option value requires that option " + nameof(Security)
						+ "set to authenticate or authenicate encryption. More information at "
						+ NetshConstants.TECHNET_NETSH_DOCU.AbsolutePath)
					);
				}
			}

			return errors;
		}

		public override string ToString()
		{
			var strCommandline = new StringBuilder();

			strCommandline.Append(Name.ToStringAppendEmptyChar());
			strCommandline.Append(Direction.ToStringAppendEmptyChar());
			strCommandline.Append(Action);

			if (Program != null) strCommandline.Append(Program.ToStringAppendEmptyChar(true));
			if (Service != null) strCommandline.Append(Service.ToStringAppendEmptyChar(true));
			if (Description != null) strCommandline.Append(Description.ToStringAppendEmptyChar(true));

			strCommandline.Append(Enable.ToStringAppendEmptyChar(true, true));

			if(Profiles().Count() > 1)
			{
				var profileIterator = (IEnumerator<ProfileParameter>)Profiles().GetEnumerator();
				strCommandline.Append(profileIterator.Current.NameInRule + "=");

				do
				{
					var profile = profileIterator.Current;
					strCommandline.Append(profile.ValueInRule + ",");
				} while (profileIterator.MoveNext());

				strCommandline.Append(profileIterator.Current.ValueInRule);
			}
			else
			{
				var profile = Profiles().ElementAt(0);
				strCommandline.Append(profile.NameInRule + "=" + profile.ValueInRule);
			}

			strCommandline.Append(LocalIp.ToStringAppendEmptyChar(true));
			strCommandline.Append(RemoteIp.ToStringAppendEmptyChar(true));
			strCommandline.Append(LocalPort.ToStringAppendEmptyChar(true));
			strCommandline.Append(RemotePort.ToStringAppendEmptyChar(true));
			strCommandline.Append(Protocol.ToStringAppendEmptyChar(true));
			strCommandline.Append(InterfaceType.ToStringAppendEmptyChar(true));

			if (RemoteComputerGroup != null) strCommandline.Append(RemoteComputerGroup.ToStringAppendEmptyChar(true));
			if (RemoteUserGroup != null) strCommandline.Append(RemoteUserGroup.ToStringAppendEmptyChar(true));
			if (Edge != null) strCommandline.Append(Edge.ToStringAppendEmptyChar(true));
			if (Security != null) strCommandline.Append(Security.ToStringAppendEmptyChar(true));

			return strCommandline.ToString();
		}
	}
}
