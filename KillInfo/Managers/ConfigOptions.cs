namespace KillInfo.Managers
{
	class ConfigOptions
	{
		public string killinfo_dir;
		public bool CheckForFalseRoundEnd;

		public void SetUp(Smod2.Plugin plugin)
		{
			killinfo_dir = plugin.GetConfigString("ki_playerinfodir");
			CheckForFalseRoundEnd = true;
		}
	}
}
