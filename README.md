# blackvoidFix
Fixes glitch for SCP:SL start of round glitch for players.
This plugin looks if someone died at the start of the round then kicks them telling them to please reconnect.
When they rejoin they get the class they had.

[Demo of end of round information](https://gfycat.com/defensivetangiblebelugawhale)
### This plugin is in BETA. Please report any errors/bugs as a issue or PM on discord.

## Install Instructions.
Put blackvoidFix.dll under the release tab into sm_plugins folder.


## Config Options.
| Config Option              | Value Type      | Default Value | Description |
|   :---:                    |     :---:       |    :---:      |    :---:    |
| ki_playerinfodir           | String          | appdata       | Path to store information for KillInfo plugin. Default is %appdata%\SCP Secret Laboratory\KillInfo\PlayerInfo. (Sorry linux users) |
| ki_disable                 | Boolean         | false         | Disables the entire KillInfo plugin. |


## Commands

| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| ki_version                 | N/A             | Get the version of this plugin           |
| ki_disable                 | N/A             | Disables the entire KillInfo plugin.    |