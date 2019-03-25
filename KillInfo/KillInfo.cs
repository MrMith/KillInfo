using Smod2;
using Smod2.Attributes;

namespace KillInfo
{
	[PluginDetails(
	author = "Mith",
	name = "KillInfo",
	description = "Displays information about Player's kills through their console and personal broadcast.",
	id = "Mith.killinfo",
	version = "0.0.1",
	SmodMajor = 3,
	SmodMinor = 3,
	SmodRevision = 0
	)]
	class KillInfo : Plugin
	{
		internal KillInfo plugin;
		
		public override void OnDisable()
		{
			this.Info($"KillInfo({this.Details.version}) has been Disabled.");
		}
		
		public override void OnEnable()
		{
			plugin = this;
			this.Info($"KillInfo({this.Details.version}) has been Enabled.");
		}

		public override void Register()
		{
			this.AddEventHandlers(new KillInfoEventLogic(this));

			this.AddConfig(new Smod2.Config.ConfigSetting("ki_playerinfodir", "appdata", Smod2.Config.SettingType.STRING, true, "Path to store information for KillInfo plugin."));
			this.AddConfig(new Smod2.Config.ConfigSetting("ki_disable", false, Smod2.Config.SettingType.BOOL, true, "Disable the entire KillInfo plugin?"));

			this.AddCommand("ki_version", new KillInfo_Version(this));
			this.AddCommand("ki_disable", new KillInfo_Disable(this));
		}
	}
}