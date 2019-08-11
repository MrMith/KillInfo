namespace KillInfo.Managers
{
	class ConfigOptions
	{
		public string KillInfo_Dir { get; set; }
		public string AccuracyLine { get; set; }
		public string KillLine { get; set; }
		public string KDRLine { get; set; }
		public string EndOfRoundLine { get; set; }
		public string CallCommandName { get; set; }
		public bool CheckForFalseRoundEnd { get; set; }

		public void SetUp(Smod2.Plugin plugin)
		{
			KillInfo_Dir = plugin.GetConfigString("ki_playerinfodir");
			AccuracyLine = plugin.GetConfigString("ki_accuracyline");
			KillLine = plugin.GetConfigString("ki_killline");
			KDRLine = plugin.GetConfigString("ki_kdrline");
			CallCommandName = plugin.GetConfigString("ki_callcommandname");
			EndOfRoundLine = plugin.GetConfigString("ki_endofroundline");
			CheckForFalseRoundEnd = true;
		}
	}
}
