# KillInfo
This displays information using personal broadcasts or the player's console about kill with each damagetype, overall accuracy and KDR (Kill death ratio).

[Demo of end of round information](https://gfycat.com/defensivetangiblebelugawhale)
### This plugin is in BETA. Please report any errors/bugs as a issue or PM on discord.

## Install Instructions.
Put KillInfo.dll under the release tab into sm_plugins folder.


## Config Options.
| Config Option              | Value Type      | Default Value | Description |
|   :---:                    |     :---:       |    :---:      |    :---:    |
| ki_playerinfodir           | String          | config        | Path to store information for KillInfo plugin. Default is where config is. %appdata%\SCP Secret Laboratory\ for windows and /home/USER/.config/SCP Secret Laboratory/ for linux. |
| ki_disable                 | Boolean         | false         | Disables the entire KillInfo plugin. |
| ki_accuracyline            | String          | See this page | Shows player their accuracy in end of round console, when they use the client command or in ki_getinfo. |
| ki_killline                | String          | See this page | Shows player their kills in end of round console, when they use the client command or in ki_getinfo.  |
| ki_kdrline                 | String          | See this page | Shows player their kill death ratio in end of round console, when they use the client command or in ki_getinfo.  |
| ki_endofroundline          | String          | See this page | Message that is broadcasted to everyone on round end.  |
| ki_callcommandname         | String          | killinfo      | Name for command used so player's can see their own stats. This is entered in the console you press ~ to open. |


## Commands

| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| ki_version                 | N/A             | Get the version of this plugin           |
| ki_disable                 | N/A             | Disables the entire KillInfo plugin.     |
| ki_getinfo                 | String (SteamID64)   | Gets player's information and works on offline players. Example: ki_getinfo 76561198103930293     |

## Client Commands 
### This is used by typing .killinfo in client console (One you open with `)
| Command(s)                 | Value Type      | Description                              |
|   :---:                    |     :---:       |    :---:                                 |
| killinfo                   | N/A             |  Returns information about kills. |
