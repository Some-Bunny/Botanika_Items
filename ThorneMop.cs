using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using System.Collections;
using Gungeon;
using MonoMod.RuntimeDetour;
using MonoMod;


namespace BotanikaItems
{
	public class ThorneMop : AdvancedGunBehavior
	{
		public static void Add()
		{
			Gun gun = ETGMod.Databases.Items.NewGun("Thorn Borne Mop", "thornemop");
			Game.Items.Rename("outdated_gun_mods:thorn_borne_mop", "btk:thorn_borne_mop");
			gun.gameObject.AddComponent<ThorneMop>();
			gun.SetShortDescription("Thorn-est Work");
			gun.SetLongDescription("A thorny mop carried by the hands of a Gungeoneer.\n\nSurely the goops found around the Gungeon could be mopped up to some benefit?");
			gun.SetupSprite(null, "thornemop_idle_001", 11);
			GunExt.SetAnimationFPS(gun, gun.shootAnimation, 15);
			GunExt.SetAnimationFPS(gun, gun.reloadAnimation, 10);
			GunExt.SetAnimationFPS(gun, gun.idleAnimation, 2);
			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(33) as Gun).gunSwitchGroup;
			for (int i = 0; i < 1; i++)
			{
				gun.AddProjectileModuleFrom(PickupObjectDatabase.GetById(33) as Gun, true, false);
			}
			gun.Volley.projectiles[0].ammoCost = 1;
			gun.Volley.projectiles[0].shootStyle = ProjectileModule.ShootStyle.Automatic;
			gun.Volley.projectiles[0].sequenceStyle = ProjectileModule.ProjectileSequenceStyle.Random;
			gun.Volley.projectiles[0].cooldownTime = 0.5f;
			gun.Volley.projectiles[0].angleVariance = 7f;
			gun.Volley.projectiles[0].numberOfShotsInClip = 25;

			gun.gunSwitchGroup = (PickupObjectDatabase.GetById(574) as Gun).gunSwitchGroup;

			Projectile projectile = UnityEngine.Object.Instantiate<Projectile>(gun.Volley.projectiles[0].projectiles[0]);
			projectile.gameObject.SetActive(false);
			gun.Volley.projectiles[0].projectiles[0] = projectile;
			projectile.baseData.damage = 11f;
			projectile.AdditionalScaleMultiplier *= 2f;
			projectile.baseData.speed *= 1.25f;
			projectile.gameObject.AddComponent<MopProjectile>();
			FakePrefab.MarkAsFakePrefab(projectile.gameObject);
			UnityEngine.Object.DontDestroyOnLoad(projectile);
			gun.DefaultModule.projectiles[0] = projectile;
			bool flag = gun.Volley.projectiles[0] != gun.DefaultModule;
			if (flag)
			{
				gun.Volley.projectiles[0].ammoCost = 0;
			}


			projectile.transform.parent = gun.barrelOffset;

		
			gun.barrelOffset.transform.localPosition = new Vector3(0.5f, 0.5f, 0f);
			gun.reloadTime = 2.5f;
			gun.SetBaseMaxAmmo(25);
			gun.muzzleFlashEffects = (PickupObjectDatabase.GetById(417) as Gun).muzzleFlashEffects;
			gun.CanReloadNoMatterAmmo = true;


			gun.quality = PickupObject.ItemQuality.SPECIAL;
			gun.InfiniteAmmo = true;

			gun.encounterTrackable.EncounterGuid = "da thorne mop";
			ETGMod.Databases.Items.Add(gun, false, "ANY");




			tk2dSpriteAnimationClip fireClip = gun.sprite.spriteAnimator.GetClipByName(gun.shootAnimation);
			float[] offsetsX = new float[] { 1.25f, -1f, -0.875f, -0.6875f, -0.375f, -0.125f, };
			float[] offsetsY = new float[] { 0.5f, -1.25f, -1f, -0.75f, -0.5f, -0.25f };
			for (int i = 0; i < offsetsX.Length && i < offsetsY.Length && i < fireClip.frames.Length; i++)
			{
				int id = fireClip.frames[i].spriteId;
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX[i];
				fireClip.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY[i];
			}

			tk2dSpriteAnimationClip fireClip2 = gun.sprite.spriteAnimator.GetClipByName(gun.reloadAnimation);
			float[] offsetsX2 = new float[] { 0.25f, 0.375f, 0.875f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1.25f, 1, 0.625f, 0.25f };
			float[] offsetsY2 = new float[] { 0f, -0.5f, -0.75f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.875f, -0.6825f, -0.4375f, 0 };
			for (int i = 0; i < offsetsX2.Length && i < offsetsY2.Length && i < fireClip2.frames.Length; i++)
			{
				int id = fireClip2.frames[i].spriteId;
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.x += offsetsX2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position0.y += offsetsY2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.x += offsetsX2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position1.y += offsetsY2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.x += offsetsX2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position2.y += offsetsY2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.x += offsetsX2[i];
				fireClip2.frames[i].spriteCollection.spriteDefinitions[id].position3.y += offsetsY2[i];
			}
		}
		public static int MopID;
		public override void OnPostFired(PlayerController player, Gun bruhgun)
		{
			gun.PreventNormalFireAudio = true;
		}
		private bool HasReloaded;

		protected override void Update()
		{
			base.Update();
			if (gun.CurrentOwner)
			{

				if (!gun.PreventNormalFireAudio)
				{
					this.gun.PreventNormalFireAudio = true;
				}
				if (!gun.IsReloading && !HasReloaded)
				{
					this.HasReloaded = true;
				}
			}

		}
		protected override void OnPickup(PlayerController player)
		{
			base.OnPickup(player);
		}

		protected override void OnPostDrop(PlayerController player)
		{
			base.OnPostDrop(player);
		}

		public override void OnReloadPressed(PlayerController player, Gun bruhgun, bool bSOMETHING)
		{
			if (gun.IsReloading && this.HasReloaded)
			{
				AkSoundEngine.PostEvent("Stop_WPN_All", base.gameObject);
				HasReloaded = false;
				base.OnReloadPressed(player, gun, bSOMETHING);
			}
			GoopDefinition currentGoop = player.CurrentGoop;
			string Val = "Unknown";
			if (currentGoop != null && currentGoop.name != null)
			{
				string Name = currentGoop.name;
				DebuffKeys.TryGetValue(Name.ToLower(), out Val);
				if (!DebuffKeys.ContainsValue(Val))
				{
					//Debug.Log("Unrecognized Goop, attempting to breakdown");
					CurrentGoopKey = currentGoop;
					CurrentGoopStringKey = "Unknown";
				}
				else
				{
					CurrentGoopStringKey = Val;
				}
				//gun.GainAmmo(Mathf.Max(0, gun.ClipCapacity - gun.ClipShotsRemaining));
			}
			else
			{
				CurrentGoopStringKey = "none";
			}
			DeadlyDeadlyGoopManager.DelayedClearGoopsInRadius(player.CenterPosition, 2f);
		}



		public string CurrentGoopStringKey;
		public GoopDefinition CurrentGoopKey;


		public static Dictionary<string, string> DebuffKeys = new Dictionary<string, string>()
		{
			//Fire Goops
			{EasyGoopDefinitions.FireDef2.name.ToLower(), "fire" },
			{EasyGoopDefinitions.FireDef.name.ToLower(), "fire" },
			{"helicopternapalmgoop", "fire" },
			{"napalm goop", "fire" },
			{"napalmgoopshortlife", "fire" },
			{"bulletkingwinegoop", "fire" },
			{"devilgoop", "fire" },
			{"flamelinegoop", "fire" },
			{"demonwallgoop", "fire" },

			//Green Fire Goops
			{"greennapalmgoopthatworks", "hellfire" },

			//Blob Goops 
			{EasyGoopDefinitions.BlobulonGoopDef.name.ToLower(), "blob" },
			{"blobulordgoop", "blob" },

			//Oil Goops 
			{EasyGoopDefinitions.OilDef.name, "oil" },

			//Cheese Goops 
			{EasyGoopDefinitions.CheeseDef.name, "cheese" },

			//Water Goops 
			{EasyGoopDefinitions.WaterGoop.name.ToLower(), "water" },
			{"mimicspitgoop", "water" },

			//Charm Goops 
			{EasyGoopDefinitions.CharmGoopDef.name.ToLower(), "charm" },

			//Poison Goops 
			{EasyGoopDefinitions.PoisonDef.name.ToLower(), "poison" },
			{"resourcefulratpoisongoop", "poison" },
			{"meduzipoisongoop", "poison" },

			//Web Goops
			{EasyGoopDefinitions.WebGoop.name.ToLower(), "web" },

			//Blood Goops 
			{"permanentbloodgoop", "blood" },
			{"bloodgoop", "blood" },
			{"bloodbulongoop", "blood" },

			//Poop Goops
			{"poopulongoop", "poop" },
		};
	}
}


namespace BotanikaItems
{
	internal class MopProjectile : MonoBehaviour
	{
		public MopProjectile()
		{
			RandomEffects.BulletSlowModifier OwnSlowModifier = new RandomEffects.BulletSlowModifier();
			OwnSlowModifier.chanceToslow = 0.2f;
			OwnSlowModifier.SpeedMultiplier = 0.2f;
			OwnSlowModifier.slowLength = 5;
			OwnSlowModifier.Color = new Color32(213, 77, 77, 255);
			OwnSlowModifier.doTint = true;
			componentsToAddToProejctile.Add("blob", OwnSlowModifier);

			componentsToAddToProejctile.Add("oil", new RandomEffects.CoveredInOil());

			RandomEffects.BulletSlowModifier webslow = new RandomEffects.BulletSlowModifier();
			webslow.chanceToslow = 1;
			webslow.SpeedMultiplier = 0.166f;
			webslow.slowLength = 10;
			componentsToAddToProejctile.Add("web", webslow);


			componentsToAddToProejctile.Add("blood", new RandomEffects.ApplyEnrage());
			componentsToAddToProejctile.Add("poop", new RandomEffects.ApplyFear());

		}

		private Projectile projectile;

		public Dictionary<string, Color32> debuffColorationKeys = new Dictionary<string, Color32>()
		{
			{"fire", new Color32(255, 102, 0, 255)},//DONE
            {"hellfire", new Color32(211, 229, 73, 255)},//DONE
            {"blob", new Color32(213, 77, 77, 255)},//DONE
            {"oil", new Color32(10, 6, 18, 255)},//DONE
            {"cheese", new Color32(255, 102, 0, 255)},
			{"charm", new Color32(252, 72, 241, 255)},//DONE
            {"poison", new Color32(145, 227, 120, 255)},//DONE
            {"web", new Color32(184, 181, 147, 255)},//DONE
            {"blood", new Color32(136, 8, 8, 255)},//DONE
            {"poop", new Color32(123, 92, 0, 255)},//DONE
        };

		public Dictionary<string, Component> componentsToAddToProejctile = new Dictionary<string, Component>()
		{

		};

		public Dictionary<string, GameActorEffect> effectsToInflict = new Dictionary<string, GameActorEffect>()
		{
			{"fire", DebuffStatics.hotLeadEffect},
			{"hellfire", DebuffStatics.greenFireEffect},
			{"cheese", DebuffStatics.cheeseeffect},
			{"charm", DebuffStatics.charmingRoundsEffect},
			{"poison", DebuffStatics.irradiatedLeadEffect},
		};



		public void Start()
		{
			this.projectile = base.GetComponent<Projectile>();
			ThorneMop mopComp = this.projectile.PossibleSourceGun.GetComponent<ThorneMop>();
			if (this.projectile != null && mopComp != null)
			{
				if (mopComp.CurrentGoopStringKey != null && mopComp.CurrentGoopStringKey != "none")
				{
					if (mopComp.CurrentGoopStringKey == "Unknown")
					{
						GoopDefinition goopToBreakDown = mopComp.CurrentGoopKey;
						this.projectile.AdjustPlayerProjectileTint(goopToBreakDown.baseColor32, 0, 0f);
						List<GameActorEffect> effects = new List<GameActorEffect>();
						if (goopToBreakDown.CharmModifierEffect != null) { effects.Add(goopToBreakDown.CharmModifierEffect); }
						if (goopToBreakDown.fireEffect != null) { effects.Add(goopToBreakDown.fireEffect); }
						if (goopToBreakDown.HealthModifierEffect != null) { effects.Add(goopToBreakDown.HealthModifierEffect); }
						if (goopToBreakDown.CheeseModifierEffect != null) { effects.Add(goopToBreakDown.CheeseModifierEffect); }
						if (goopToBreakDown.SpeedModifierEffect != null) { effects.Add(goopToBreakDown.SpeedModifierEffect); }
						for (int i = 0; i < effects.Count; i++)
						{
							projectile.statusEffectsToApply.Add(effects[i]);
						}
					}
					else
					{
						Color32 color = new Color32(0, 0, 0, 0);
						debuffColorationKeys.TryGetValue(mopComp.CurrentGoopStringKey, out color);
						this.projectile.AdjustPlayerProjectileTint(color, 0, 0f);

						GameActorEffect effect = null;
						effectsToInflict.TryGetValue(mopComp.CurrentGoopStringKey, out effect);
						if (effect != null) { projectile.statusEffectsToApply.Add(effect); }

						Component component = null;
						componentsToAddToProejctile.TryGetValue(mopComp.CurrentGoopStringKey, out component);
						if (component != null) { projectile.gameObject.AddComponent(component); }

						if (mopComp.CurrentGoopStringKey.ToLower() == "water")
						{
							projectile.baseData.damage *= 0.75f;
						}
					}
				}
			}
		}
	}
}
