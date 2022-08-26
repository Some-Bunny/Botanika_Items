using System.Collections;
using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;
using Dungeonator;
using System.Reflection;
using Random = System.Random;
using FullSerializer;
using Gungeon;
using MonoMod.RuntimeDetour;
using MonoMod;
using System.Collections.ObjectModel;

using UnityEngine.Serialization;

namespace BotanikaItems
{
    public class RandomEffects
    {

		public class ApplyFear : MonoBehaviour
		{
			public ApplyFear()
			{
				this.chanceToslow = 1f;
			}

			private void Start()
			{
				this.m_projectile = base.GetComponent<Projectile>();
				bool flag = UnityEngine.Random.value <= this.chanceToslow;
				if (flag)
				{
					Projectile projectile = this.m_projectile;
					projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.ApplyStun));
				}
			}
			private static FleePlayerData fleeData;
			private void ApplyStun(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
			{
				bool r = enemy.behaviorSpeculator != null;
				if (r)
				{
					ApplyFear.fleeData = new FleePlayerData();
					ApplyFear.fleeData.Player = GameManager.Instance.PrimaryPlayer;
					ApplyFear.fleeData.StartDistance = 100f;
					enemy.behaviorSpeculator.FleePlayerData = ApplyFear.fleeData;
					FleePlayerData fleePlayerData = new FleePlayerData();
					GameManager.Instance.StartCoroutine(ApplyFear.scare(enemy.aiActor));
				}
			}
			private static IEnumerator scare(AIActor aiactor)
			{
				yield return new WaitForSeconds(5f);
				aiactor.behaviorSpeculator.FleePlayerData = null;
				yield break;
			}
			private Projectile m_projectile;
			public float chanceToslow;
			public float SpeedMultiplier;

			public bool doTint;
			public Color Color;
			public float slowLength;
		}


		public class ApplyEnrage : MonoBehaviour
		{
			public ApplyEnrage()
			{
				this.chanceToslow = 1f;
			}

			private void Start()
			{
				this.m_projectile = base.GetComponent<Projectile>();
				bool flag = UnityEngine.Random.value <= this.chanceToslow;
				if (flag)
				{
					Projectile projectile = this.m_projectile;
					projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.ApplyStun));
				}
			}

			private void ApplyStun(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
			{
				if (enemy != null)
				{
					enemy.gameObject.GetOrAddComponent<Enrage>();
				}
			}
			private Projectile m_projectile;
			public float chanceToslow;
			public float SpeedMultiplier;

			public bool doTint;
			public Color Color;
			public float slowLength;
		}

		public class Enrage : BraveBehaviour
		{
			public void Start()
			{
				RagePassiveItem rageitem = PickupObjectDatabase.GetById(353).GetComponent<RagePassiveItem>();
				this.RageOverheadVFX = rageitem.OverheadVFX.gameObject;
				this.instanceVFX = base.aiActor.PlayEffectOnActor(this.RageOverheadVFX, new Vector3(0f, 1.375f, 0f), true, true, false);

				base.aiActor.behaviorSpeculator.AttackCooldown *= 0.80f;
				base.aiActor.RegisterOverrideColor(Rage, "rage");
				base.aiActor.healthHaver.AllDamageMultiplier *= 1.4f;
			}
			public void Update()
			{

			}

			public Color Rage = new Color(7f, 0f, 0f, 0.33f);
			private GameObject instanceVFX;
			public GameObject RageOverheadVFX;
		}



		public class CoveredInOil : MonoBehaviour
		{
			public CoveredInOil()
			{
				this.chanceToslow = 1f;
			}

			private void Start()
			{
				this.m_projectile = base.GetComponent<Projectile>();
				bool flag = UnityEngine.Random.value <= this.chanceToslow;
				if (flag)
				{
					Projectile projectile = this.m_projectile;
					projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.ApplyStun));
				}
			}

			private void ApplyStun(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
			{
				if (enemy != null)
				{
					enemy.gameObject.GetOrAddComponent<OilFlight>();
				}
			}


			private Projectile m_projectile;
			public float chanceToslow;
			public float SpeedMultiplier;

			public bool doTint;
			public Color Color;
			public float slowLength;
		}

		public class OilFlight : BraveBehaviour
		{
			public void Start()
			{
				base.aiActor.SetIsFlying(true, "wing");
				base.aiActor.RegisterOverrideColor(GhostColor, "oiled");

				float fard = UnityEngine.Random.Range(1, 25);
				if (fard == 1)
				{
					if (base.aiActor != null && !base.aiActor.healthHaver.IsBoss)
					{
						GameManager.Instance.Dungeon.StartCoroutine(this.HandleEnemySuck(base.aiActor));
						base.aiActor.EraseFromExistence(true);
					}
				}
			}
			public void Update()
			{

			}
			private IEnumerator HandleEnemySuck(AIActor target)
			{
				float oddset = UnityEngine.Random.Range(5, -5);
				Transform copySprite = this.CreateEmptySprite(target);
				Vector3 startPosition = copySprite.transform.position;
				target.RegisterOverrideColor(GhostColor, "oiled");

				float elapsed = 0f;
				float duration = UnityEngine.Random.Range(3, 8);
				while (elapsed < duration)
				{
					elapsed += BraveTime.DeltaTime;
					bool flag3 = copySprite;
					if (flag3)
					{
						Vector3 position = startPosition + new Vector3(oddset, 30);
						float t = elapsed / duration * (elapsed / duration);
						copySprite.position = Vector3.Lerp(startPosition, position, t);
						Vector3 pos = Vector3.Lerp(startPosition, position, t);
						float? startLifetime = new float?(UnityEngine.Random.Range(0.3f, 2f));


						position = default(Vector3);
					}
					yield return null;
				}
				bool flag4 = copySprite;
				if (flag4)
				{
					UnityEngine.Object.Destroy(copySprite.gameObject);
				}
				yield break;
			}
			private Transform CreateEmptySprite(AIActor target)
			{
				GameObject gameObject = new GameObject("suck image");
				gameObject.layer = target.gameObject.layer;
				tk2dSprite tk2dSprite = gameObject.AddComponent<tk2dSprite>();
				gameObject.transform.parent = SpawnManager.Instance.VFX;
				tk2dSprite.SetSprite(target.sprite.Collection, target.sprite.spriteId);
				tk2dSprite.transform.position = target.sprite.transform.position;
				GameObject gameObject2 = new GameObject("image parent");
				gameObject2.transform.position = tk2dSprite.WorldCenter;
				tk2dSprite.transform.parent = gameObject2.transform;
				bool flag = target.optionalPalette != null;
				if (flag)
				{
					tk2dSprite.renderer.material.SetTexture("_PaletteTex", target.optionalPalette);
				}
				return gameObject2.transform;
			}

			public Color GhostColor = new Color(6f, 1f, 2f, 0.25f);
			public float minimumHealth;
			public float CheatDeath = 20f;
		}


		public class BulletSlowModifier : MonoBehaviour
		{
			public BulletSlowModifier()
			{
				this.chanceToslow = 0f;
				this.slowLength = 1f;
				//this.doVFX = true;
				this.SpeedMultiplier = 1;
				this.Color = Color.white;
				this.doTint = false;
			}

			private void Start()
			{
				this.m_projectile = base.GetComponent<Projectile>();
				bool flag = UnityEngine.Random.value <= this.chanceToslow;
				if (flag)
				{
					Projectile projectile = this.m_projectile;
					projectile.OnHitEnemy = (Action<Projectile, SpeculativeRigidbody, bool>)Delegate.Combine(projectile.OnHitEnemy, new Action<Projectile, SpeculativeRigidbody, bool>(this.ApplyStun));
				}
			}

			private void ApplyStun(Projectile bullet, SpeculativeRigidbody enemy, bool fatal)
			{
				if (enemy != null)
				{
					EasyApplyDirectSlow.ApplyDirectSlow(enemy.gameActor, slowLength, SpeedMultiplier, Color, Color, EffectResistanceType.None, "Slonedown", doTint, doTint);
				}
			}
			private Projectile m_projectile;
			public float chanceToslow;
			public float SpeedMultiplier;

			public bool doTint;
			public Color Color;
			public float slowLength;
		}
		public class EasyApplyDirectSlow
		{
			// Token: 0x06000353 RID: 851 RVA: 0x0002058C File Offset: 0x0001E78C
			public static void ApplyDirectSlow(GameActor target, float duration, float speedMultiplier, Color tintColour, Color deathTintColour, EffectResistanceType resistanceType, string identifier, bool tintsEnemy, bool tintsCorpse)
			{
				Gun gun = ETGMod.Databases.Items["triple_crossbow"] as Gun;
				GameActorSpeedEffect speedEffect = gun.DefaultModule.projectiles[0].speedEffect;
				GameActorSpeedEffect effect = new GameActorSpeedEffect
				{
					duration = duration,
					TintColor = tintColour,
					DeathTintColor = deathTintColour,
					effectIdentifier = identifier,
					AppliesTint = tintsEnemy,
					AppliesDeathTint = tintsCorpse,
					resistanceType = resistanceType,
					SpeedMultiplier = speedMultiplier,
					OverheadVFX = speedEffect.OverheadVFX,
					AffectsEnemies = true,
					AffectsPlayers = false,
					AppliesOutlineTint = false,
					OutlineTintColor = tintColour,
					PlaysVFXOnActor = false
				};
				bool flag = target && target.aiActor && target.healthHaver && target.healthHaver.IsAlive;
				if (flag)
				{
					target.ApplyEffect(effect, 1f, null);
				}
			}
		}

	}
}