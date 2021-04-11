using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API
{
	public static class ConfigurationInfo
	{
		public static string[] GetValue(this IConfiguration config, string path)
		{
			IConfigurationSection section;

			string[] values = null;

			try
			{
				section = config.GetSection(path);

				values = (section.Value == null) ?
							section.Get<string[]>() :
							new string[] { section.Value };

				if (values.Length == 0)
				{
					throw null;
				}
			}
			catch
			{
				throw (new Exception($"Some problem occurred while trying to get the '{path}' Configuration"));
			}

			return values;
		}
	}
}
