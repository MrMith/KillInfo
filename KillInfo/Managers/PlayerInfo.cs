using System;
using System.Collections.Generic;
using Smod2.API;

namespace KillInfo.Managers
{
	class PlayerInfo
	{
		public Dictionary<Smod2.API.DamageType, int> KillCounter = new Dictionary<Smod2.API.DamageType, int>();
		public Dictionary<Smod2.API.DamageType, int> KillsThisRoundCounter = new Dictionary<Smod2.API.DamageType, int>();
		public Dictionary<Smod2.API.DamageType, int> DeathCounter = new Dictionary<Smod2.API.DamageType, int>();

		public int ShotsFired = 0,
			ShotsHit = 0;

		/// <summary>
		/// Add one kill by damagetype to kill list
		/// </summary>
		/// <param name="damageType"></param>
		public void AddKill(Smod2.API.DamageType damageType)
		{
			if (KillCounter.ContainsKey(damageType))
			{
				KillCounter[damageType]++;
			}
			else
			{
				KillCounter[damageType] = 1;
			}
			if (KillsThisRoundCounter.ContainsKey(damageType))
			{
				KillsThisRoundCounter[damageType]++;
			}
			else
			{
				KillsThisRoundCounter[damageType] = 1;
			}
		}

		/// <summary>
		/// Add one death by damagetype to counter
		/// </summary>
		/// <param name="damageType"></param>
		public void AddDeath(Smod2.API.DamageType damageType)
		{
			if (DeathCounter.ContainsKey(damageType))
			{
				DeathCounter[damageType]++;
			}
			else
			{
				DeathCounter[damageType] = 1;
			}
		}

		/// <summary>
		/// Force sets amount of kills by damagetype
		/// </summary>
		/// <param name="damageType">damagetype</param>
		/// <param name="kills">Amount of kills</param>
		public void SetKill(Smod2.API.DamageType damageType, int kills)
		{
			KillCounter[damageType] = kills;
		}

		/// <summary>
		/// Force sets amount of deaths by damagetype
		/// </summary>
		/// <param name="damageType"></param>
		/// <param name="deaths"></param>
		public void SetDeath(Smod2.API.DamageType damageType, int deaths)
		{
			DeathCounter[damageType] = deaths;
		}

		/// <summary>
		/// Returns total amount of deaths.
		/// </summary>
		/// <returns></returns>
		public int GetAmountOfDeaths(bool CheckForZero = false)
		{
			int Deaths = 0;
			foreach (DamageType dmgtype in (DamageType[])Enum.GetValues(typeof(DamageType)))
			{
				if (DeathCounter.ContainsKey(dmgtype))
				{
					Deaths += DeathCounter[dmgtype];
				}
			}
			if (CheckForZero)
			{
				if (Deaths <= 0)
				{
					return 1;
				}
			}
			return Deaths;
		}

		/// <summary>
		/// Returns total amount of kills this person has gotten.
		/// </summary>
		/// <returns></returns>
		public int GetAmountOfKills()
		{
			int Kills = 0;
			foreach (DamageType dmgtype in (DamageType[])Enum.GetValues(typeof(DamageType)))
			{
				if (KillCounter.ContainsKey(dmgtype))
				{
					Kills += KillCounter[dmgtype];
				}
			}
			return Kills;
		}

		/// <summary>
		/// Returns amount of kills gotten by damagetype.
		/// </summary>
		/// <param name="damageType"></param>
		/// <returns></returns>
		public int GetKillByDamageType(Smod2.API.DamageType damageType)
		{
			if (KillCounter.ContainsKey(damageType))
			{
				return KillCounter[damageType];
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Returns amount of kills gotten during the round by damagetype.
		/// </summary>
		/// <returns>int</returns>
		public int GetCurrentKillsByDamageType(Smod2.API.DamageType damageType)
		{
			if (KillsThisRoundCounter.ContainsKey(damageType))
			{
				return KillsThisRoundCounter[damageType];
			}
			else
			{
				return 0;
			}
		}

		/// <summary>
		/// Gets information about how many shots they've fired, hit or their accuracy.
		/// </summary>
		/// <param name="FiredHitOrBoth">0 = shots fired, 1 = shots hit, 2 = accuracy</param>
		/// <returns></returns>
		public float GetShotInfo(int FiredHitOrBoth)
		{
			switch (FiredHitOrBoth)
			{
				case 0:
					return ShotsFired;
				case 1:
					return ShotsHit;
				case 2:
					return ((float)ShotsHit / (float)ShotsFired)*100;
				default:
					return 0;
			}
		}

		/// <summary>
		/// Add one shot and bool if they've hit the shot or not.
		/// </summary>
		/// <param name="Hit"></param>
		public void AddShot(bool Hit)
		{
			if (Hit)
			{
				ShotsFired++;
				ShotsHit++;
				return;
			}
			ShotsFired++;
			return;
		}
	}
}
