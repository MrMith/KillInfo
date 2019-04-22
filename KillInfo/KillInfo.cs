﻿using Smod2;
using Smod2.Attributes;
using Smod2.Config;
using KillInfo.Commands;

namespace KillInfo
{
	[PluginDetails(
	author = "Mith",
	name = "KillInfo",
	description = "Displays information about Player's kills through their console and personal broadcast.",
	id = "Mith.killinfo",
	version = "0.0.6",
	configPrefix = "ki",
	SmodMajor = 3,
	SmodMinor = 4,
	SmodRevision = 0
	)]
	class KillInfo : Plugin
	{
		public static KillInfo plugin;

		KillInfoEventLogic eventLogic;

		[ConfigOption]
		public readonly int hecktime = 30;

		[ConfigOption]
		public bool disable = false;

		[ConfigOption]
		public string playerinfodir = "config";

		[ConfigOption]
		public string accuracyline = "Your accuracy is ACCURACYPERCENT%. ( SHOTSFIRED / SHOTSHIT )";

		[ConfigOption]
		public string killline = "You've gotten ALLTIMEKILLS (+ CURRENTKILLS this round) kill(s) with CURRENTDMGTYPE.";

		[ConfigOption]
		public string kdrline = "Your KDR is KDRLINE. ( DEATHS / KILLS )";

		[ConfigOption]
		public string endofroundline = "Check your console for more detailed information about kills, death and accuracy! Press ~ to access!";

		[ConfigOption]
		public string callcommandname = "killinfo";

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
			this.AddEventHandlers(eventLogic = new KillInfoEventLogic(this));

			this.AddCommand("ki_version", new KillInfo_Version(this));
			this.AddCommand("ki_disable", new KillInfo_Disable(this));
			this.AddCommand("ki_getinfo", new KillInfo_GetInfo(this, eventLogic));
		}
	}
}