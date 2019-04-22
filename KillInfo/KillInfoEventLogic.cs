using Smod2;
using Smod2.Events;
using Smod2.EventHandlers;
using Smod2.API;
using System;
using System.Text;
using System.Collections.Generic;
using KillInfo.Managers;

namespace KillInfo
{
	class KillInfoEventLogic : IEventHandlerPlayerDie,IEventHandlerRoundEnd,IEventHandlerShoot, IEventHandlerWaitingForPlayers, IEventHandlerCallCommand, IEventHandlerPlayerJoin
	{
		public Dictionary<string, PlayerInfo> CheckSteamIDForKillInfo = new Dictionary<string, PlayerInfo>();

		public KillFileReadAndWrite KillReadAndWrite = new KillFileReadAndWrite();

		public ConfigOptions configOptions = new ConfigOptions();

		readonly Plugin plugin;

		public KillInfoEventLogic(Plugin plugin)
		{
			this.plugin = plugin;
		}
		
		public void OnPlayerDie(PlayerDeathEvent ev)
		{
			if (!CheckSteamIDForKillInfo.ContainsKey(ev.Player.SteamId))
			{
				CheckSteamIDForKillInfo[ev.Player.SteamId] = KillReadAndWrite.ReadPlayerBySteamID(ev.Player.SteamId);
			}

			CheckSteamIDForKillInfo[ev.Player.SteamId].AddDeath(ev.DamageTypeVar);

			if (ev.Killer != null && ev.Player.SteamId != ev.Killer.SteamId)
			{
				if (!CheckSteamIDForKillInfo.ContainsKey(ev.Killer.SteamId))
				{
					CheckSteamIDForKillInfo[ev.Killer.SteamId] = KillReadAndWrite.ReadPlayerBySteamID(ev.Killer.SteamId);
				}

				CheckSteamIDForKillInfo[ev.Killer.SteamId].AddKill(ev.DamageTypeVar);
			}
		}

		public void OnRoundEnd(RoundEndEvent ev)
		{
			if(!configOptions.CheckForFalseRoundEnd)
			{
				return;
			}

			foreach (Player player in Smod2.PluginManager.Manager.Server.GetPlayers())
			{
				if (!CheckSteamIDForKillInfo.ContainsKey(player.SteamId))
				{
					CheckSteamIDForKillInfo[player.SteamId] = KillReadAndWrite.ReadPlayerBySteamID(player.SteamId);
				}

				PlayerInfo CurrentPlayerInfo = CheckSteamIDForKillInfo[player.SteamId];

				StringBuilder AccuracyMessage = new StringBuilder(configOptions.AccuracyLine);
				AccuracyMessage.Replace("ACCURACYPERCENT", CurrentPlayerInfo.GetShotInfo(2).ToString());
				AccuracyMessage.Replace("SHOTSFIRED", CurrentPlayerInfo.GetShotInfo(0).ToString());
				AccuracyMessage.Replace("SHOTSHIT", CurrentPlayerInfo.GetShotInfo(1).ToString());
				player.SendConsoleMessage(AccuracyMessage.ToString());

				foreach (DamageType dmgtype in (DamageType[])Enum.GetValues(typeof(DamageType)))
				{
					if (CurrentPlayerInfo.GetKillByDamageType(dmgtype) != 0)
					{
						StringBuilder KillLine = new StringBuilder(configOptions.KillLine);
						KillLine.Replace("ALLTIMEKILLS", CurrentPlayerInfo.GetKillByDamageType(dmgtype).ToString());
						KillLine.Replace("CURRENTKILLS", CurrentPlayerInfo.GetCurrentKillsByDamageType(dmgtype).ToString());
						KillLine.Replace("CURRENTDMGTYPE", dmgtype.ToString().Replace("_", "-"));
						player.SendConsoleMessage(KillLine.ToString());
					}
				}

				StringBuilder KDRLine = new StringBuilder(configOptions.KDRLine);
				KDRLine.Replace("KDRLINE", ((double)CurrentPlayerInfo.GetAmountOfKills(true) / (double)CurrentPlayerInfo.GetAmountOfDeaths(true)).ToString());
				KDRLine.Replace("DEATHS", CurrentPlayerInfo.GetAmountOfDeaths().ToString());
				KDRLine.Replace("KILLS", CurrentPlayerInfo.GetAmountOfKills().ToString());
				player.SendConsoleMessage(KDRLine.ToString());

				CurrentPlayerInfo.KillsThisRoundCounter.Clear();
			}
			KillReadAndWrite.SaveAllPlayers(CheckSteamIDForKillInfo);
			Smod2.PluginManager.Manager.Server.Map.Broadcast(5, configOptions.EndOfRoundLine, true);
			configOptions.CheckForFalseRoundEnd = false;
		}

		public void OnShoot(PlayerShootEvent ev)
		{
			if (!CheckSteamIDForKillInfo.ContainsKey(ev.Player.SteamId))
			{
				CheckSteamIDForKillInfo[ev.Player.SteamId] = KillReadAndWrite.ReadPlayerBySteamID(ev.Player.SteamId);
			}

			if (ev.Target != null)
			{
				CheckSteamIDForKillInfo[ev.Player.SteamId].AddShot(true);
			}
			else
			{
				CheckSteamIDForKillInfo[ev.Player.SteamId].AddShot(false);
			}
		}

		public void OnCallCommand(PlayerCallCommandEvent ev)
		{
			if(ev.Command.ToLower() == configOptions.CallCommandName)
			{
				if (!CheckSteamIDForKillInfo.ContainsKey(ev.Player.SteamId))
				{
					CheckSteamIDForKillInfo[ev.Player.SteamId] = KillReadAndWrite?.ReadPlayerBySteamID(ev.Player.SteamId);
				}
				PlayerInfo CurrentPlayerInfo = CheckSteamIDForKillInfo[ev.Player.SteamId];

				StringBuilder AccuracyMessage = new StringBuilder(configOptions.AccuracyLine);
				AccuracyMessage.Replace("ACCURACYPERCENT", CurrentPlayerInfo.GetShotInfo(2).ToString());
				AccuracyMessage.Replace("SHOTSFIRED", CurrentPlayerInfo.GetShotInfo(0).ToString());
				AccuracyMessage.Replace("SHOTSHIT", CurrentPlayerInfo.GetShotInfo(1).ToString());
				ev.Player.SendConsoleMessage(AccuracyMessage.ToString());

				foreach (DamageType dmgtype in (DamageType[])Enum.GetValues(typeof(DamageType)))
				{
					if (CurrentPlayerInfo.GetKillByDamageType(dmgtype) != 0)
					{
						StringBuilder KillLine = new StringBuilder(configOptions.KillLine);
						KillLine.Replace("ALLTIMEKILLS", CurrentPlayerInfo.GetKillByDamageType(dmgtype).ToString());
						KillLine.Replace("CURRENTKILLS", CurrentPlayerInfo.GetCurrentKillsByDamageType(dmgtype).ToString());
						KillLine.Replace("CURRENTDMGTYPE", dmgtype.ToString().Replace("_", "-"));
						ev.Player.SendConsoleMessage(KillLine.ToString());
					}
				}

				StringBuilder KDRLine = new StringBuilder(configOptions.KDRLine);
				KDRLine.Replace("KDRLINE", ((double)CurrentPlayerInfo.GetAmountOfKills(true) / (double)CurrentPlayerInfo.GetAmountOfDeaths(true)).ToString());
				KDRLine.Replace("DEATHS", CurrentPlayerInfo.GetAmountOfDeaths().ToString());
				KDRLine.Replace("KILLS", CurrentPlayerInfo.GetAmountOfKills().ToString());
				ev.Player.SendConsoleMessage(KDRLine.ToString());

				ev.ReturnMessage = "Got data for " + ev.Player.Name;
			}
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			if (plugin.GetConfigBool("ki_disable"))
			{
				plugin.PluginManager.DisablePlugin(plugin);
			}

			configOptions.SetUp(plugin);
			KillReadAndWrite.SetUp(configOptions);
			CheckSteamIDForKillInfo = KillReadAndWrite.ReadAllPlayers(CheckSteamIDForKillInfo);

			if (KillReadAndWrite.MakeSureDirExistAndGetDir().Length == 0)
			{
				plugin.Error("ki_playerinfodir is not set correctly. Nothing is going to be saved.");
			}
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			if (!CheckSteamIDForKillInfo.ContainsKey(ev.Player.SteamId))
			{
				CheckSteamIDForKillInfo[ev.Player.SteamId] = KillReadAndWrite.ReadPlayerBySteamID(ev.Player.SteamId);
			}
		}
	}
}