using ItemAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BotanikaItems
{

	public class DebuffStatics
	{
		//---------------------------------------BASEGAME STATUS EFFECTS
		//Fires
		public static GameActorFireEffect hotLeadEffect = PickupObjectDatabase.GetById(295).GetComponent<BulletStatusEffectItem>().FireModifierEffect;
		public static GameActorFireEffect greenFireEffect = PickupObjectDatabase.GetById(706).GetComponent<Gun>().DefaultModule.projectiles[0].fireEffect;


		//Freezes
		public static GameActorFreezeEffect frostBulletsEffect = PickupObjectDatabase.GetById(278).GetComponent<BulletStatusEffectItem>().FreezeModifierEffect;
		public static GameActorFreezeEffect chaosBulletsFreeze = PickupObjectDatabase.GetById(569).GetComponent<ChaosBulletsItem>().FreezeModifierEffect;

		//Poisons
		public static GameActorHealthEffect irradiatedLeadEffect = PickupObjectDatabase.GetById(204).GetComponent<BulletStatusEffectItem>().HealthModifierEffect;

		//Charms
		public static GameActorCharmEffect charmingRoundsEffect = PickupObjectDatabase.GetById(527).GetComponent<BulletStatusEffectItem>().CharmModifierEffect;

		//Cheeses
		public static GameActorCheeseEffect cheeseeffect = (PickupObjectDatabase.GetById(626) as Gun).DefaultModule.projectiles[0].cheeseEffect;
		//Speed Changes
		public static GameActorSpeedEffect tripleCrossbowSlowEffect = (PickupObjectDatabase.GetById(381) as Gun).DefaultModule.projectiles[0].speedEffect;
	}

	internal class EasyGoopDefinitions
	{
		public static void DefineDefaultGoops()
		{
			AssetBundle assetBundle = ResourceManager.LoadAssetBundle("shared_auto_001");
			EasyGoopDefinitions.goopDefs = new List<GoopDefinition>();
			foreach (string text in EasyGoopDefinitions.goops)
			{
				GoopDefinition goopDefinition;
				try
				{
					GameObject gameObject = assetBundle.LoadAsset(text) as GameObject;
					goopDefinition = gameObject.GetComponent<GoopDefinition>();
				}
				catch
				{
					goopDefinition = (assetBundle.LoadAsset(text) as GoopDefinition);
				}
				goopDefinition.name = text.Replace("assets/data/goops/", "").Replace(".asset", "");
				EasyGoopDefinitions.goopDefs.Add(goopDefinition);
			}
			List<GoopDefinition> list = EasyGoopDefinitions.goopDefs;
			EasyGoopDefinitions.FireDef = EasyGoopDefinitions.goopDefs[0];
			EasyGoopDefinitions.OilDef = EasyGoopDefinitions.goopDefs[1];
			EasyGoopDefinitions.PoisonDef = EasyGoopDefinitions.goopDefs[2];
			EasyGoopDefinitions.BlobulonGoopDef = EasyGoopDefinitions.goopDefs[3];
			EasyGoopDefinitions.WebGoop = EasyGoopDefinitions.goopDefs[4];
			EasyGoopDefinitions.WaterGoop = EasyGoopDefinitions.goopDefs[5];
			EasyGoopDefinitions.FireDef2 = EasyGoopDefinitions.goopDefs[6];

		}
		static EasyGoopDefinitions()
		{
			PickupObject byId = PickupObjectDatabase.GetById(310);
			GoopDefinition charmGoopDef;
			if (byId == null)
			{
				charmGoopDef = null;
			}
			else
			{
				WingsItem component = byId.GetComponent<WingsItem>();
				charmGoopDef = ((component != null) ? component.RollGoop : null);
			}
			EasyGoopDefinitions.CharmGoopDef = charmGoopDef;
			EasyGoopDefinitions.GreenFireDef = (PickupObjectDatabase.GetById(698) as Gun).DefaultModule.projectiles[0].GetComponent<GoopModifier>().goopDefinition;
			EasyGoopDefinitions.CheeseDef = (PickupObjectDatabase.GetById(808) as Gun).DefaultModule.projectiles[0].GetComponent<GoopModifier>().goopDefinition;
			EasyGoopDefinitions.TripleCrossbow = (ETGMod.Databases.Items["triple_crossbow"] as Gun);
			EasyGoopDefinitions.TripleCrossbowEffect = EasyGoopDefinitions.TripleCrossbow.DefaultModule.projectiles[0].speedEffect;

		}

		public static string[] goops = new string[]
		{
			"assets/data/goops/napalmgoopthatworks.asset",
			"assets/data/goops/oil goop.asset",
			"assets/data/goops/poison goop.asset",
			"assets/data/goops/blobulongoop.asset",
			"assets/data/goops/phasewebgoop.asset",
			"assets/data/goops/water goop.asset",
			"assets/data/goops/napalmgoopquickignite.asset"
		};

		private static List<GoopDefinition> goopDefs;

		public static GoopDefinition FireDef;
		public static GoopDefinition FireDef2;

		public static GoopDefinition OilDef;

		public static GoopDefinition PoisonDef;

		public static GoopDefinition BlobulonGoopDef;

		public static GoopDefinition WebGoop;

		public static GoopDefinition WaterGoop;

		public static GoopDefinition CharmGoopDef;

		public static GoopDefinition GreenFireDef;

		public static GoopDefinition CheeseDef;

		private static Gun TripleCrossbow;
		private static GameActorSpeedEffect TripleCrossbowEffect;

	}
}
