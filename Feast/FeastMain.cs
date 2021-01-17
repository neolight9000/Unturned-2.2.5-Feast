using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Xml.Serialization;
using CommandHandler;
using Ini;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnturnedFeast
{
	public class FeastMain : MonoBehaviour
	{
		private System.Timers.Timer respawnTimer;

		private static bool enableEffects = true;
		private static bool announceWhilePlayerTook = true;
		private static int itemAmountForDrop = 10;
		private static int timeUntilNextDropSeconds = 1800;

		public static int[] items = new int[76]
		{
			2004,
			7008,
			12000,
			9004,
			10001,
			11003,
			18014,
			4017,
			5017,
			11,
			13000,
			14022,
			8015,
			8013,
			3002,
			14026,
			16007,
			16016,
			16017,
			5003,
			5004,
			18013,
			18012,
			18016,
			1,
			8,
			17,
			8019,
			7000,
			7001,
			7002,
			7003,
			7005,
			7006,
			7009,
			7011,
			7012,
			7013,
			7016,
			7018,
			13003,
			13004,
			13005,
			13006,
			13007,
			13010,
			13018,
			9000,
			9002,
			9003,
			9004,
			9009,
			9012,
			10000,
			10002,
			10004,
			10005,
			10006,
			10007,
			10009,
			10010,
			10011,
			10012,
			10013,
			11002,
			11003,
			11001,
			11000,
			11004,
			11005,
			12001,
			12002,
			25000,
			25002,
			21000,
			21001
		};

		public static void Airdrop()
		{
			int loc = Random.RandomRange(1, 13);
			string arg = string.Empty;
			string string_ = string.Empty;
			string str = string.Empty;
			Vector3 val = default(Vector3);
			val = new Vector3(0f, 0f, 0f);
			switch (loc)
			{
			case 1:
				arg = "Airdrop will fall to the Summerside Peninsula";
				string_ = "Airdop was fallen on Summerside Peninsula!";
				val = new Vector3 (790f, 24f, -450f);
				str = "Take airdrop on Summerside Peninsula";
				break;
			case 2:
				arg = "Airdrop will fall to the Burywood";
				string_ = "Airdrop was fallen on Burywood!";
					val = new Vector3(50f, 23f, 700f);
				str = "Take airdrop on BuryWood";
				break;
			case 3:
				arg = "Airdrop will fall to the Courtin Island";
				string_ = "Airdrop was fallen on Courtin Island!";
					val = new Vector3(890f, 22f, 500f);
				str = "Take airdrop on Courtin Island";
				break;
			case 4:
				arg = "Airdrop will fall to the Belfast Airport";
				string_ = "Airdrop was fallen on Belfast Airport!";
					val = new Vector3(600f, 25f, 468f);
				str = "Take airdrop on Belfast Airport";
				break;
			case 5:
				arg = "Airdrop will fall to the Holman Island";
				string_ = "Airdrop was fallen on Holman Island!";
					val = new Vector3(-770f, 29f, -760f);
				str = "Take airdrop on Holman Island";
				break;
			case 6:
				arg = "Airdrop will fall to the O'Leary Military Base";
				string_ = "Airdrop was fallen on O'Leary Military Base!";
					val = new Vector3(-440f, 26f, 607f);
				str = "Take airdrop on Military Base";
				break;
			case 7:
				arg = "Airdrop will fall to the Alberton";
				string_ = "Airdrop was fallen on Alberton!";
					val = new Vector3(-580f, 19f, 87f);
				str = "Take airdrop on Alberton";
				break;
			case 8:
				arg = "Airdrop will fall to the Charlottetown";
				string_ = "Airdrop was fallen on Charlottetown!";
					val = new Vector3(22f, 18f, -432f);
				str = "Take airdrop on Charlottetown";
				break;
			case 9:
				arg = "Airdrop will fall to the Montague";
				string_ = "Airdrop was fallen on Montague!";
					val = new Vector3(250f, 30f, -100f);
				str = "Take airdrop on Montague";
				break;
			case 10:
				arg = "Airdrop will fall to the Oultons Isle";
				string_ = "Airdrop was fallen on Oultons Isle!";
					val = new Vector3(200f, 28f, -825f);
				str = "Take airdrop on Oultons Isle";
				break;
			case 11:
				arg = "Airdrop will fall to the St. Peter's Island";
				string_ = "Airdrop was fallen on St. Peter's Island!";
					val = new Vector3(-245.4355f, 27.7f, 47.64797f);
				str = "Take airdrop on St. Peter's Island";
				break;
			case 12:
				arg = "Airdrop will fall to the Fernwood Farm";
				string_ = "Airdrop was fallen on Fernwood Farm!";
					val = new Vector3(-244.4423f, 25.5f, -376.0898f);
				str = "Take airdrop on Fernwood Farm";
				break;
			case 13:
				arg = "Airdrop will fall to the Wiltshire Farm";
				string_ = "Airdrop was fallen on Wiltshire Farm!";
					val = new Vector3(-451.6884f, 27.3f, -563.936f);
				str = "Take airdrop on Wiltishire Farm";
				break;
			}
			for (int i = 5; i >= 1; i--)
			{
				NetworkChat.sendAlert($"{arg} after {i} min");
				Thread.Sleep(60000);
			}
			NetworkChat.sendAlert(string_);
			for (int i = 0; i < itemAmountForDrop; i++)
			{
				int int_ = items[Random.Range(0, items.Length)];
				float num3 = Random.Range(0, 5);
				float num4 = Random.Range(0, 5);
				SpawnItems.spawnItem(int_, 1, new Vector3(val.x + num3, val.y, val.z + num4));
			}
			NetworkSounds.askSoundMax("Sounds/spooky/spooky_3", val, 500f, 1f, 700f, 3500f);
			for (int num5 = 1000; num5 > 0; num5--)
			{
				if (announceWhilePlayerTook)
				{
					for (int j = 0; j < UserList.users.Count; j++)
					{
						BetterNetworkUser val2 = UserList.users[j];
						float num6 = Mathf.Abs(Vector3.Distance(val, val2.position));
						if (num6 < 10.5f)
						{
							NetworkChat.sendAlert(val2.name + " " + str);
							NetworkSounds.askSoundMax("Sounds/spooky/spooky_0", val, 500f, 1f, 700f, 3500f);
							return;
						}
					}
				}
				if (enableEffects)
				{
					int num7 = 0;
					for (int k = 0; k < 25; k++)
					{
						NetworkEffects.askEffect("Effects/sparksRed", new Vector3(val.x, val.y + (float)num7, val.z), Quaternion.Euler(-90f, 0f, 0f), 100f);
						num7 += 2;
					}
					NetworkSounds.askSound("Sounds/projectiles/smoke", val, 500f, 1f, 700f);
				}
				Thread.Sleep(300);
			}
		}

		private static void Airdrop(object sender, ElapsedEventArgs e)
		{
			Thread thread = new Thread((ThreadStart)Airdrop);
			thread.Start();
		}

		public static void Airdrop(CommandArgs args)
		{
			Thread thread = new Thread((ThreadStart)Airdrop);
			thread.Start();
		}

		public void Start()
		{
			Command air = new Command(6, new CommandDelegate(Airdrop), new string[1]
			{
				"airdrop"
			});
			CommandList.add(air);
			if (!Directory.Exists("Unturned_Data/Managed/mods/Airdrop"))
			{
				Directory.CreateDirectory("Unturned_Data/Managed/mods/Airdrop");
			}

            try
            {
				if (!File.Exists("Unturned_Data/Managed/mods/Airdrop/airdrop.ini"))
				{
					IniFile airconf = new IniFile("Unturned_Data/Managed/mods/Airdrop/airdrop.ini");
					airconf.IniWriteValue("Config", "Delay between airdrop in seconds", "1800");
					airconf.IniWriteValue("Config", "Items Amount in the Drop", "10");
					airconf.IniWriteValue("Config", "Enable Effects", "true");
					airconf.IniWriteValue("Config", "Trigger on Player took Drop", "true");
				}
				string[] airconfs = File.ReadAllLines("Unturned_Data/Managed/mods/Airdrop/airdrop.ini");
				timeUntilNextDropSeconds = Convert.ToInt32(airconfs[1].Substring(33));
				itemAmountForDrop = Convert.ToInt32(airconfs[2].Substring(25));
				enableEffects = Convert.ToBoolean(airconfs[3].Substring(15));
				announceWhilePlayerTook = Convert.ToBoolean(airconfs[4].Substring(28));
			}
            catch
            {

            }
			
			respawnTimer = new System.Timers.Timer(timeUntilNextDropSeconds * 1000);
			respawnTimer.Elapsed += Airdrop;
			respawnTimer.Enabled = true;
			
			
		}
	}
}
