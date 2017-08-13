using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestris.ResourceLib;

namespace WindowsAdvancedFirewallApi.Resources
{
	public class SystemLocalizations
	{
		private static readonly object syncLock = new object();

		private static SystemLocalizations _singleton;

		public static SystemLocalizations Singleton
		{
			get
			{
				if (_singleton == null)
				{
					lock (syncLock)
					{
						if (_singleton == null)
						{
							_singleton = new SystemLocalizations();
						}
					}
				}

				return _singleton;
			}
		}
		public static SystemLocalizations Instance => Singleton;

		public IReadOnlyDictionary<uint, string> StringsFirewallAPI { get; private set; }
		public IReadOnlyDictionary<uint, string> StringsICSvc { get; private set; }

		private SystemLocalizations()
		{
			var FirewallAPI = new ResourceInfo();
			FirewallAPI.Load(Path.Combine(Environment.SystemDirectory, "FirewallAPI.dll"));
			StringsFirewallAPI = LoadStrings(FirewallAPI);
			FirewallAPI.Dispose();

			var ICSvc = new ResourceInfo();
			ICSvc.Load(Path.Combine(Environment.SystemDirectory, "icsvc.dll"));
			StringsICSvc = LoadStrings(ICSvc);
			ICSvc.Dispose();
		}

		private IReadOnlyDictionary<uint, string> LoadStrings(ResourceInfo resourceInfo)
		{
			var strings = new Dictionary<uint, string>();

			foreach (var resource in resourceInfo[Kernel32.ResourceTypes.RT_STRING])
			{
				if (!(resource is StringResource))
				{
					continue;
				}

				var stringResource = resource as StringResource;

				foreach (var @string in stringResource.Strings)
				{
					strings.Add(@string.Key, @string.Value);
				}
			}

			return strings;
		}
	}
}
