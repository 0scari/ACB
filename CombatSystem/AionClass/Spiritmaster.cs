using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.Enums;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public enum SpiritType
    {
        EarthSpirit,
        FireSpirit,
        WaterSpirit,
        WindSpirit,
    }


    public class SpiritmasterSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_SpiritMaster_SpiritToSummoning", "Spirit to summoning")]
        public SpiritType SpiritToSummoning { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_SpiritMaster_AllowSandblaster", "Use Sandblaster")]
        public bool AllowSandblaster { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_SpiritMaster_AbsorbVitalityLowHP", "Absorb Vitality HP < %")]
        public uint AbsorbVitalityLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_SpiritMaster_SkillIDToDispell", "Skill ID to dispell")]
        public string SkillToDebuf { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_SpiritMaster_SpiritSubLowHP", "Spirit Substitution HP < %")]
        public uint SpiritSubLowHP { get; set; }

        public SpiritmasterSettings() : base()
        {
            TargetSearchDistance = 15;
            SpiritToSummoning = SpiritType.FireSpirit;
            AllowSandblaster = false;
            AbsorbVitalityLowHP = 70;
            SpiritSubLowHP = 0;
        }

        public SpiritmasterSettings(SpiritmasterSettings classSettings) : base(classSettings)
        {
            SpiritToSummoning = classSettings.SpiritToSummoning;
            AllowSandblaster = classSettings.AllowSandblaster;
            AbsorbVitalityLowHP = classSettings.AbsorbVitalityLowHP;
            SpiritSubLowHP = classSettings.SpiritSubLowHP;
        }
    }

    public class Spiritmaster : AionClassBase
    {
        private static readonly string[] dmg_skills = new string[] {
            // Summon Fire Energy
            "1792", // Summon Fire Energy IV - Asmodian
            "1749", // Summon Fire Energy III - Asmodian
            "1748", // Summon Fire Energy II - Asmodian
            "1747", // Summon Fire Energy I - Asmodian

            "1791", // Summon Fire Energy IV - Elyos
            "1752", // Summon Fire Energy III - Elyos
            "1751", // Summon Fire Energy II - Elyos
            "1750", // Summon Fire Energy I - Elyos

            // Summon Cyclone Servant
            "2235", // Summon Cyclone Servant IV - Asmodian
            "2221", // Summon Cyclone Servant III - Asmodian
            "2215", // Summon Cyclone Servant II - Asmodian
            "1642", // Summon Cyclone Servant I - Asmodian

            "2234", // Summon Cyclone Servant IV - Elyos
            "2220", // Summon Cyclone Servant III - Elyos
            "2214", // Summon Cyclone Servant II - Elyos
            "1641", // Summon Cyclone Servant I - Elyos


            // Summon Stone Energy
            "1797", // Summon Stone Energy III - Asmodian
            "1610", // Summon Stone Energy II - Asmodian
            "1609", // Summon Stone Energy I - Asmodian

            "1796", // Summon Stone Energy III - Elyos
            "1644", // Summon Stone Energy II - Elyos
            "1643", // Summon Stone Energy I - Elyos


            // Summon Water Energy
            "2240", // Summon Water Energy III - Asmodian
            "1784", // Summon Water Energy II - Asmodian
            "1753", // Summon Water Energy I - Asmodian

            "2239", // Summon Water Energy III - Elyos
            "1783", // Summon Water Energy II - Elyos
            "1754", // Summon Water Energy I - Elyos

            // Summon Wind Servant
            "1707", // Summon Wind Servant IV - Asmodian
            "1613", // Summon Wind Servant III - Asmodian
            "1612", // Summon Wind Servant II - Asmodian
            "1611", // Summon Wind Servant I - Asmodian

            "1706", // Summon Wind Servant IV - Elyos
            "1647", // Summon Wind Servant III - Elyos
            "1646", // Summon Wind Servant II - Elyos
            "1645", // Summon Wind Servant I - Elyos

            "Weaken Spirit IV",
            "Weaken Spirit III",
            "Weaken Spirit II",
            "Weaken Spirit I",

            "Infernal Pain II",
            "Infernal Pain I",

            "Vacuum Choke III",
            "Vacuum Choke II",
            "Vacuum Choke I",
        };

        private static readonly string[] chain_skills = new string[]{
            "Stone Shock III",
            "Stone Shock II",
            "Stone Shock I",
        };

        private static readonly string[] spirit_skills = new string[] {
            "Spirit Thunderbolt Claw I",
            "Spirit Disturbance I",
            "Spirit Detonation Claw I",
            "Spirit Ruinous Offensive I"
        };

        private static readonly string[] spiritChain_skills = new string[] {

        };

        private uint LastEntityId { get; set; }
        private DateTime SpiritAutoAttackTime { get; set; }
        private SpiritmasterSettings Settings { get; set; }
        private bool isEnergyCombo { get; set; }


        List<uint> _skillIdToDebuffList;

        public Spiritmaster() : this(new SpiritmasterSettings())
        {
            LastEntityId = 0;
            SpiritAutoAttackTime = DateTime.MinValue;
        }

        public Spiritmaster(SpiritmasterSettings settings)
        {
            LastEntityId = 0;
            SpiritAutoAttackTime = DateTime.MinValue;
            Settings = settings;
            CurrentAionClass = eAionClass.Spiritmaster;

            _skillIdToDebuffList = new List<uint>();
            if (string.IsNullOrWhiteSpace(Settings.SkillToDebuf) == false)
            {
                var sellItemArray = Settings.SkillToDebuf.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tempName in sellItemArray)
                {
                    if (string.IsNullOrWhiteSpace(tempName) == false)
                    {
                        int tempEntityId = 0;
                        string trimmedName = tempName.Trim();

                        if (Int32.TryParse(trimmedName, out tempEntityId))
                        {
                            _skillIdToDebuffList.Add((uint)tempEntityId);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Perform the attack routine on the selected target.
        /// </summary>
        /// <param name="entity">Contains the entity we have targeted.</param>
        /// <param name="range">Contains the distance to the target</param>
        /// <returns></returns>
        public override bool Attack(Entity entity, float range)
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Remove Shock I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Remove Shock I");
                return false;
            }
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Vengeful Backdraft I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Vengeful Backdraft I");
                return false;
            }

            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            var entitySpirit = FindSpirit(0);
            DateTime currentDateTime = DateTime.Now;


            // Initial pet aggro
            if (entitySpirit != null)
            {
                // Taunt
                if (entity.TargetId != entitySpirit.Id && HelperFunction.CheckAvailable("Spirit Threat I"))
                {
                    HelperFunction.CheckExecute("Spirit Threat I");
                    return false;
                }

                if ((currentDateTime > SpiritAutoAttackTime && entitySpirit.TargetId != entity.Id))// || (entity.HealthPercentage >= 100 && entity.TargetId != entitySpirit.Id))
                {
                    Dialog PetDialog = Game.DialogList.GetDialog("pet_dialog");

                    // If this dialog does not exist, there is a huge issue! > _ <
                    if (PetDialog == null)
                    {
                        Game.WriteMessage("ERROR: The pet command dialog could not be found!");
                        return false;
                    }

                    // HUD Bottom
                    var petCmd1_HudBottom = PetDialog.GetDialog("pet_cmd1");
                    if (petCmd1_HudBottom != null && petCmd1_HudBottom.IsVisible())
                    {
                        petCmd1_HudBottom.Click();
                    }
                    else
                    {
                        // HUD Top
                        var petCmd1_HudTop = PetDialog.GetDialog("mercenary_dialog/pet_cmd1");
                        if (petCmd1_HudTop != null && petCmd1_HudTop.IsVisible())
                        {
                            petCmd1_HudTop.Click();
                        }
                    }


                    //And make sure we are not spamming this clicking action.
                    SpiritAutoAttackTime = currentDateTime.Add(new TimeSpan(0, 0, 2));

                    return false;
                }
            }

            // HP recovery
            if (entity.HealthPercentage < 100) {
              if (Game.Player.HealthPercentage < Settings.AbsorbVitalityLowHP)
              {
                  string absorbVitalitySkillName = "Absorb Vitality";
                  if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " VII"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " VII");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " VI"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " VI");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " V"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " V");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " IV"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " IV");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " III"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " III");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " II"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " II");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbVitalitySkillName + " I"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbVitalitySkillName + " I");
                      return false;
                  }
              }

              if (Game.Player.HealthPercentage < Settings.AbsorbVitalityLowHP || Game.Player.ManaPercentage < Settings.AbsorbVitalityLowHP) {
                  if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Backdraft I"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Backdraft I");
                      return false;
                  }
              }
            }

            // Pat heals + Sub
            if (entitySpirit != null)
            {
                // Heal spirit
                if (entitySpirit.HealthPercentage < 25 && HelperFunction.CheckAvailable("Spirit Recovery I"))
                {
                    HelperFunction.CheckExecute("Spirit Recovery I");
                    return false;
                }

                // Heal spirit
                if (entitySpirit.HealthPercentage < 15 && HelperFunction.CheckAvailable("Healing Spirit I"))
                {
                    HelperFunction.CheckExecute("Healing Spirit I");
                    return false;
                }

                // Replenish Element IV - loss HP to recovery spirit HP
                if (Game.Player.HealthPercentage > 80)
                {
                    string replinishElementName = "Replenish Element";
                    uint spiritMissingHP = entitySpirit.HealthMaximum - entitySpirit.HealthCurrent;
                    if (spiritMissingHP > 2104 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(replinishElementName + " IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(replinishElementName + " IV");
                        return false;
                    }
                    else if (spiritMissingHP > 1472 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(replinishElementName + " III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(replinishElementName + " III");
                        return false;
                    }
                    else if (spiritMissingHP > 896 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(replinishElementName + " II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(replinishElementName + " II");
                        return false;
                    }
                    else if (spiritMissingHP > 724 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(replinishElementName + " I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(replinishElementName + " I");
                        return false;
                    }
                }

                // Spirit Substitution
                if (entitySpirit.HealthPercentage > 10 && Game.Player.HealthPercentage < Settings.SpiritSubLowHP)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Spirit Substitution I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Spirit Substitution I");
                        return false;
                    }
                }
            }

            // Main pet skills
            if (entitySpirit != null)
            {
                // Recovery player MP
                if (entitySpirit.HealthPercentage > 50 && Game.Player.ManaPercentage < 70)
                {
                    string spiritAbsorptionSkillName = "Spirit Absorption";
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritAbsorptionSkillName + " II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritAbsorptionSkillName + " II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritAbsorptionSkillName + " I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritAbsorptionSkillName + " I");
                        return false;
                    }
                }

                // Recovery player MP
                // TODO candidate for deletion
                if (Game.Player.HealthPercentage > 50 && Game.Player.ManaMaximum - Game.Player.ManaCurrent > 3500)
                {
                    string transparenceSkillName = "Transference";
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(transparenceSkillName + " II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(transparenceSkillName + " II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(transparenceSkillName + " I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(transparenceSkillName + " I");
                        return false;
                    }
                }

                // DP Buff
                if (Game.Player.DP > 2000 && entitySpirit.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Spirit Armor of Light") >= 0).Any() == false && entitySpirit.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Spirit Armor of Darkness") >= 0).Any() == false)
                {
                    string spiritArmorDpSkillName = "Spirit Armor of Light";
                    if (Game.Player.IsAsmodian)
                    {
                        spiritArmorDpSkillName = "Spirit Armor of Darkness";
                    }

                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritArmorDpSkillName + " IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritArmorDpSkillName + " IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritArmorDpSkillName + " III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritArmorDpSkillName + " III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritArmorDpSkillName + " II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritArmorDpSkillName + " II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritArmorDpSkillName + " I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritArmorDpSkillName + " I");
                        return false;
                    }
                }

                // Spirit Buff : Armor Spirit
                if (entitySpirit.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Armor Spirit") >= 0).Any() == false && HelperFunction.CheckAvailable("Armor Spirit I"))
                {
                    HelperFunction.CheckExecute("Armor Spirit I");
                    return false;
                }

                // Spirit Buff : Spirit Wrath Position I
                if (HelperFunction.CheckAvailable("Spirit Wrath Position I"))
                {
                    HelperFunction.CheckExecute("Spirit Wrath Position I");
                    return false;
                }

                // Dmg dot
                //if (entity.HealthPercentage > 50 && HelperFunction.CheckAvailable("Spirit Erosion I"))
                //{
                //    HelperFunction.CheckExecute("Spirit Erosion I");
                //    return false;
                //}

                // Damage skill
                if (ExecuteSkillFromList(entity, spirit_skills).Item1 == false)
                {
                    return false;
                }
            }


            // Wind Energy + Cyclone Combo
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summoning Alacrity I") && entity.HealthCurrent > 8500) {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summon Cyclone Servant II") || AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summon Cyclone Servant IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summoning Alacrity I");
                    return false;
                }
            }

            // Summon Cyclone Servant
            if (entity.HealthCurrent > 8500)
            {
                if (Game.Player.IsElyos)
                {
                    if (HelperFunction.CheckAvailable(2234))
                    {
                        HelperFunction.CheckExecute(2234);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(2220))
                    {
                        HelperFunction.CheckExecute(2220);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(2214))
                    {
                        HelperFunction.CheckExecute(2214);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1641))
                    {
                        HelperFunction.CheckExecute(1641);
                        return false;
                    }
                }
                else
                {
                    // Asmo Fire Energy
                    if (HelperFunction.CheckAvailable(2235))
                    {
                        HelperFunction.CheckExecute(2235);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(2221))
                    {
                        HelperFunction.CheckExecute(2221);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(2215))
                    {
                        HelperFunction.CheckExecute(2215);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1642))
                    {
                        HelperFunction.CheckExecute(1642);
                        return false;
                    }
                }
            }

            // Wind/fire Energy
            if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Summoning Alacrity", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == true)
            {
                if (Game.Player.IsElyos)
                {
                    if (HelperFunction.CheckAvailable(1706))
                    {
                        HelperFunction.CheckExecute(1706);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1647))
                    {
                        HelperFunction.CheckExecute(1647);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1646))
                    {
                        HelperFunction.CheckExecute(1646);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1645))
                    {
                        HelperFunction.CheckExecute(1645);
                        return false;
                    }
                }
                else
                {
                    // Asmo Fire Energy
                    if (HelperFunction.CheckAvailable(1792))
                    {
                        HelperFunction.CheckExecute(1792);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1749))
                    {
                        HelperFunction.CheckExecute(1749);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1748))
                    {
                        HelperFunction.CheckExecute(1748);
                        return false;
                    }
                    else if (HelperFunction.CheckAvailable(1747))
                    {
                        HelperFunction.CheckExecute(1747);
                        return false;
                    }
                }
            }

            var entityDistance = Game.Player.Position.Distance(entity.Position);


            // Blessing Of Fire -I - IV
            if (entityDistance < 20)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing Of Fire IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing Of Fire IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing Of Fire III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing Of Fire III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing Of Fire II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing Of Fire II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing Of Fire I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing Of Fire I");
                    return false;
                }
            }


            // Ignite Aether // Dispel Magic I
            if (entity.StateList.GetList().Where(s => _skillIdToDebuffList.Contains(s.Value.Id)).Count() >= 1)
            {
                if (HelperFunction.CheckAvailable("Ignite Aether V"))
                {
                    HelperFunction.CheckExecute("Ignite Aether V");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Ignite Aether IV"))
                {
                    HelperFunction.CheckExecute("Ignite Aether IV");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Ignite Aether III"))
                {
                    HelperFunction.CheckExecute("Ignite Aether III");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Ignite Aether II"))
                {
                    HelperFunction.CheckExecute("Ignite Aether II");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Ignite Aether I"))
                {
                    HelperFunction.CheckExecute("Ignite Aether I");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Dispel Magic I"))
                {
                    HelperFunction.CheckExecute("Dispel Magic I");
                    return false;
                }
            }

            // Erosion
            if (entity.StateList.GetList().Where(st => st.Value.Name_Eu.IndexOf("Erosion") >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Erosion I"))
            {
                string erosionSkillName = "Erosion";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(erosionSkillName + " V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(erosionSkillName + " V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(erosionSkillName + " IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(erosionSkillName + " IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(erosionSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(erosionSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(erosionSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(erosionSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(erosionSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(erosionSkillName + " I");
                    return false;
                }
            }

            if (HelperFunction.CheckAvailable("Cyclone of Wrath I") && entity.HealthCurrent > 8500) {
                HelperFunction.CheckExecute("Cyclone of Wrath I");
                return false;
            }

           // Slow enemy
            string chainOfEarthSkillName = "Chain of Earth";
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(chainOfEarthSkillName + " IV"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(chainOfEarthSkillName + " IV");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(chainOfEarthSkillName + " III"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(chainOfEarthSkillName + " III");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(chainOfEarthSkillName + " II"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(chainOfEarthSkillName + " II");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(chainOfEarthSkillName + " I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(chainOfEarthSkillName + " I");
                return false;
            }

            // Sandblaster
            if (Settings.AllowSandblaster)
            {
                string sandblasterName = "Sandblaster";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(sandblasterName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(sandblasterName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(sandblasterName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(sandblasterName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(sandblasterName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(sandblasterName + " I");
                    return false;
                }
            }

            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_skills).Item1 == false)
            {
                //  WaitAutoAttack = true;
                return false;
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Heal()
        {
            return true;
        }

        /// <summary>
        /// Perform the required pause checks.
        /// </summary>
        /// <returns></returns>
        public override bool Pause()
        {
            if (Game.Player.ManaMaximum - Game.Player.ManaCurrent > 230)
            {
                string gainManaSkillName = "Absorb Energy";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(gainManaSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(gainManaSkillName + " I");
                    return false;
                }

            }
            if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Stone Skin", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                string stoneSkinSkillName = "Stone Skin";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " I");
                    return false;
                }
            }

            var entitySpirit = FindSpirit(0);

            // If spirit is not summoned
            if (entitySpirit == null && !Game.Player.IsGliding && !Game.Player.IsFlying)
            {
                /*  string spiritNameToSummon = SpiritNameFromType(Settings.SpiritToSummoning);
                  if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritNameToSummon + " IV"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritNameToSummon + " IV");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritNameToSummon + " III"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritNameToSummon + " III");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritNameToSummon + " II"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritNameToSummon + " II");
                      return false;
                  }
                  else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spiritNameToSummon + " I"))
                  {
                      AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spiritNameToSummon + " I");
                      return false;
                  }*/


                var spiritSkillIDToSummon = SpiritSkillIdFromType(Settings.SpiritToSummoning);
                foreach (var spirtSkillId in spiritSkillIDToSummon)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(spirtSkillId.ToString()))
                    {
                        // Try to use "Summoning Alacrity I" to instant summon spirit
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summoning Alacrity I"))
                        {
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summoning Alacrity I");
                            return false;
                        }

                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(spirtSkillId.ToString());
                        return false;
                    }
                }
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        private string SpiritNameFromType(SpiritType spiritToSummoning)
        {
            switch (spiritToSummoning)
            {
                case SpiritType.EarthSpirit:
                    return "Summon Earth Spirit";
                case SpiritType.FireSpirit:
                    return "Summon Fire Spirit";
                case SpiritType.WaterSpirit:
                    return "Summon Water Spirit";
                case SpiritType.WindSpirit:
                    return "Summon Wind Spirit";
                default:
                    return "Summon Fire Spirit";
            }
        }

        /// <summary>
        /// Return the skill id based on spirt to summon and race
        /// </summary>
        /// <param name="spiritToSummoning"></param>
        /// <returns></returns>
        private uint[] SpiritSkillIdFromType(SpiritType spiritToSummoning)
        {
            switch (spiritToSummoning)
            {
                case SpiritType.EarthSpirit:
                    return AionGame.Game.Player.IsAsmodian ? new uint[] { 1714, 1616, 1615, 1614 } : new uint[] { 1713, 1650, 1649, 1648 };
                case SpiritType.FireSpirit:
                    return AionGame.Game.Player.IsAsmodian ? new uint[] { 1696, 1619, 1618, 1617 } : new uint[] { 1695, 1653, 1652, 1651 };
                case SpiritType.WaterSpirit:
                    return AionGame.Game.Player.IsAsmodian ? new uint[] { 2228, 1689, 1621, 1620 } : new uint[] { 2227, 1688, 1655, 1654 };
                case SpiritType.WindSpirit:
                    return AionGame.Game.Player.IsAsmodian ? new uint[] { 1703, 1624, 1623, 1622 } : new uint[] { 1702, 1658, 1657, 1656 };
                default:
                    return AionGame.Game.Player.IsAsmodian ? new uint[] { 1696, 1619, 1618, 1617 } : new uint[] { 1695, 1653, 1652, 1651 };
            }
        }

        /// <summary>
        /// Perform the required force checks.
        /// </summary>
        /// <returns></returns>
        public override bool PartyCheck()
        {
            return true;
        }

        /// <summary>
        /// Checks the healing requirements for the provided entity.
        /// </summary>
        /// <param name="entity">Contains the entity to perform healing on.</param>
        /// <returns></returns>
        private bool CheckHeal(Entity entity)
        {
            if (entity.ManaMaximum - entityMana.ManaCurrent >= 4000
                && Game.Player.HealthPercentage >= 50) {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Transference II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Transference II", entity);
                    return false;
                }
            }
            return true;
        }


        public bool GetBuff()
        {
            // Loop through the state of the target entity
            for (uint i = 0; i < Game.Player.StateList.GetStateSize(); i++)
            {
                // Retrieve the state from the EntityState.
                var stateIndex = Game.Player.StateList.GetStateIndex(i);

                // Check if the state is correct and check if it is a debuff.
                if (stateIndex != null && stateIndex.IsDebuff)
                {
                    return true;
                }
            }

            return false;
        }

        private bool TargetBuff(Entity entity)
        {
            var entityState = entity.StateList;

            if (entityState != null)
            {
            }
            return true;
        }

        /// <summary>
        /// Search Spiritmaster spirit
        /// </summary>
        /// <param name="spiritId"></param>
        /// <returns></returns>
        private Entity FindSpirit(uint spiritId)
        {
            // Check the provided identifier to look for, otherwise use my own identifier.
            if (spiritId == 0)
            {
                spiritId = Game.Player.Id;
            }

            // Loop through the available entities to find the spirit.
            foreach (var tempEntity in Game.EntityList.GetList())
            {
                // Check if this monster is a spirit and belongs to me!
                if (tempEntity.Value.OwnerID == spiritId && (tempEntity.Value.Name.IndexOf("Spirit") >= 0
                    || tempEntity.Value.Name.IndexOf("精灵") >= 0 // China
                    || tempEntity.Value.Name.IndexOf("精靈") >= 0 // Taiwan
                    || tempEntity.Value.Name.IndexOf("정령") >= 0) // Korean
                    || tempEntity.Value.Name.IndexOf("スピリット") >= 0 // Japan
                    || tempEntity.Value.Name.IndexOf("Элементаль") >= 0)
                {
                    return tempEntity.Value;
                }
            }

            // Spirit has not been found
            return null;
        }

        public override bool NoGravityAntiStuck()
        {
            return true;
        }
    }
}
