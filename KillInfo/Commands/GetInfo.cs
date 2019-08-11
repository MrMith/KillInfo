using Smod2;
using Smod2.API;
using Smod2.Commands;
using KillInfo.Managers;
using System;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace KillInfo.Commands
{
	class KillInfo_GetInfo : ICommandHandler
	{
		private readonly Plugin plugin;
		public KillInfoEventLogic eventLogic;
		public KillInfo_GetInfo(Plugin plugin,KillInfoEventLogic killInfoEvent)
		{
			this.plugin = plugin;
			eventLogic = killInfoEvent;
		}

		public string GetCommandDescription()
		{
			return "Gets information about player.";
		}

		public string GetUsage()
		{
			return "ki_getinfo SteamID64";
		}
		
		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if(!File.Exists(eventLogic.KillReadAndWrite.MakeSureDirExistAndGetDir() + args[0] + ".txt"))
			{
				return new string[] {"Player was not found."};
			}

			PlayerInfo playerInfo = eventLogic.KillReadAndWrite?.ReadPlayerBySteamID(args[0]);

			List<string> StringToReturn = new List<string>()
			{
				""
			};

			StringBuilder AccuracyMessage = new StringBuilder(eventLogic.configOptions.AccuracyLine);
			AccuracyMessage.Replace("ACCURACYPERCENT", playerInfo.GetShotInfo(2).ToString());
			AccuracyMessage.Replace("SHOTSFIRED", playerInfo.GetShotInfo(0).ToString());
			AccuracyMessage.Replace("SHOTSHIT", playerInfo.GetShotInfo(1).ToString());
			StringToReturn.Add(AccuracyMessage.ToString());

			foreach (DamageType dmgtype in (DamageType[])Enum.GetValues(typeof(DamageType)))
			{
				if (playerInfo.GetKillByDamageType(dmgtype) != 0)
				{
					StringBuilder KillLine = new StringBuilder(eventLogic.configOptions.KillLine);
					KillLine.Replace("ALLTIMEKILLS", playerInfo.GetKillByDamageType(dmgtype).ToString());
					KillLine.Replace("CURRENTKILLS", playerInfo.GetCurrentKillsByDamageType(dmgtype).ToString());
					KillLine.Replace("CURRENTDMGTYPE", dmgtype.ToString().Replace("_", "-"));
					StringToReturn.Add(KillLine.ToString());
				}
			}

			StringBuilder KDRLine = new StringBuilder(eventLogic.configOptions.KDRLine);
			KDRLine.Replace("KDRLINE", ((double)playerInfo.GetAmountOfKills() / (double)playerInfo.GetAmountOfDeaths(true)).ToString());
			KDRLine.Replace("DEATHS", playerInfo.GetAmountOfDeaths().ToString());
			KDRLine.Replace("KILLS", playerInfo.GetAmountOfKills().ToString());
			StringToReturn.Add(KDRLine.ToString());

			return StringToReturn.ToArray();
		}
	}
}
