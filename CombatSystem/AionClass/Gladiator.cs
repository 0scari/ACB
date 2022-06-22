using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class GladiatorSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowAOE", "Allow AoE")]
        public bool AllowAoe { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowWeaving", "Allow Weaving")]
        public bool AllowWeaving { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Gladiator_UseDaevicFury", "Use Daevic Fury")]
        public bool UseDaevicFury { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Gladiator_UseBlessingOfNezekanOrZikelsThreat", "Use Blessing of Nezekan\\Zikel's Threat")]
        public bool UseBlessingOfNezekanOrZikelsThreat { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Gladiator_UseTaunt", "Use Taunt")]
        public bool UseTaunt { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Gladiator_UseDrainingBlowBelowHPPercentage", "Use Draining Blow if HP below %")]
        public int UseDrainingBlowBelowHPPercentage { get; set; }

        public GladiatorSettings() : base()
        {
            TargetSearchDistance = 10;
            AllowAoe = false;
            AllowWeaving = true;

            UseDaevicFury = true;
            UseBlessingOfNezekanOrZikelsThreat = true;
            UseTaunt = false;
            UseDrainingBlowBelowHPPercentage = 50;
        }

        public GladiatorSettings(GladiatorSettings classSettings) : base(classSettings)
        {
            AllowWeaving = classSettings.AllowWeaving;
            AllowAoe = classSettings.AllowAoe;
            UseDaevicFury = classSettings.UseDaevicFury;
            UseBlessingOfNezekanOrZikelsThreat = classSettings.UseBlessingOfNezekanOrZikelsThreat;
            UseTaunt = classSettings.UseTaunt;
            UseDrainingBlowBelowHPPercentage = classSettings.UseDrainingBlowBelowHPPercentage;
        }
    }

    public class Gladiator : AionClassBase
    {
        private uint AttackStartedID { get; set; }

        private static string[] chain_skills = new string[]{
            // Remove Shock
            "Ferocity I",
            "Wrathful Explosion I",


            "Vicious Blow II",
            "Vicious Blow I",

            // Ferocious Strike chain
            "Reckless Strike III",
            "Reckless Strike II",
            "Reckless Strike I",

            "Wrathful Strike IV",
            "Wrathful Strike III",
            "Wrathful Strike II",
            "Wrathful Strike I",

            "Rupture IV",
            "Rupture III",
            "Rupture II",
            "Rupture I",

            "Robust Blow V",
            "Robust Blow IV",
            "Robust Blow III",
            "Robust Blow II",
            "Robust Blow I",

            // Cleave
            "Righteous Cleave II",
            "Righteous Cleave I",

            "Great Cleave II",
            "Great Cleave I",

            "2034", // "Force Cleave II" - Asmodian
            "374", // "Force Cleave I" - Asmodian
            "2033", // "Force Cleave II" - Elyos
            "312", // "Force Cleave I" - Elyos

            // Stumbled Skills
            "Final Strike I",

            "Crashing Blow III",
            "Crashing Blow II",
            "Crashing Blow I",

            "Crippling Cut VI",
            "Crippling Cut V",
            "Crippling Cut IV",
            "Crippling Cut III",
            "Crippling Cut II",
            "Crippling Cut I",

            "Springing Slice III",
            "Springing Slice II",
            "Springing Slice I",

            // Parry
            "Spite Strike IV",
            "Spite Strike III",
            "Spite Strike II",
            "Spite Strike I",

            "Vengeful Strike V",
            "Vengeful Strike IV",
            "Vengeful Strike III",
            "Vengeful Strike II",
            "Vengeful Strike I",

            //
            "Seismic Billow III",
            "Seismic Billow II",
            "Seismic Billow I",

            "Pressure Wave IV",
            "Pressure Wave III",
            "Pressure Wave II",
            "Pressure Wave I",

            "Shock Wave III",
            "Shock Wave II",
            "Shock Wave I",
        };

        private static string[] dmg_skills = new string[] {
            "Precision Cut IV", // hight mana
            "Precision Cut III",
            "Precision Cut II",
            "Precision Cut I",

            "Sharp Strike IV",
            "Sharp Strike III",
            "Sharp Strike II",
            "Sharp Strike I",

            "Severe Weakening Blow VI",
            "Severe Weakening Blow V",
            "Severe Weakening Blow IV",
            "Severe Weakening Blow III",
            "Severe Weakening Blow II",
            "Severe Weakening Blow I",

            "Weakening Severe Blow V",
            "Weakening Severe Blow IV",
            "Weakening Severe Blow III",
            "Weakening Severe Blow II",
            "Weakening Severe Blow I",

            "Sure Strike I",

            "Whirling Strike II",
            "Whirling Strike I",

            "Tendor Slice II",
            "Tendor Slice I",

            // 10s CD
            "177", // "Ferocious Strike V",
            "172", // "Ferocious Strike IV",
            "171", // "Ferocious Strike III",
            "170", // "Ferocious Strike II",
            "169", // "Ferocious Strike I",


            "Body Smash III",
            "Body Smash II",
            "Body Smash I",
        };

        private static string[] dmgAoE_skills = new string[] {
            "",
        };

        private static string[] chainAoE_skills = new string[]{
            ""
        };



        //
        DateTime combatPreparationBuffTimer { get; set; }
        private ulong DrainingSwordTimer { get; set; }
        private GladiatorSettings Settings { get; set; }

        public Gladiator() : this(new GladiatorSettings())
        {
        }

        public Gladiator(GladiatorSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Gladiator;

            AttackStartedID = 0;
            combatPreparationBuffTimer = DateTime.MinValue;
        }

        public override bool Attack(Entity entity, float range)
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Remove Shock I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Remove Shock I");
                return false;
            }

            var missingHealth = Game.Player.HealthMaximum - Game.Player.HealthCurrent;
            var distance = entity.Position.Distance(Game.Player.Position);
            bool isDualWeapon = IsDualWeaponEquipped();

            // Cleave
            if (distance < 20 && entity.HealthPercentage >= 99)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Cleave IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Cleave IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Cleave III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Cleave III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Cleave II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Cleave II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Cleave I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Cleave I");
                    return false;
                }
            }

            if (WaitAutoAttack && Settings.AllowWeaving)
            {
                Game.Player.UpdateAutoAttackData();

                if (Game.Player.IsAutoAttacking && Game.Player.CurrentAnimationTime < (Game.Player.MaxAnimationTimer * 0.9))
                {
                    WaitAutoAttack = false;
                }
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(Game.AttackChatCommand);
                return false;
            }

            // Draining Blow
            if (Game.Player.HealthPercentage < Settings.UseDrainingBlowBelowHPPercentage)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2022))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2022);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(398))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(398);
                    return false;
                }
            }

            if (Settings.AllowAoe && distance < 6 && !isDualWeapon && missingHealth > 1500)
            {
                // Absorbing Fury
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("2268"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("2268");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("2267"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("2267");
                    return false;
                }
            }

            if (ExecuteSkillFromList(entity, chain_skills).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            // Wall of Steel
            if (entity.HealthPercentage > 80 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(250))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(250);
                return false;
            }

            // Daevic Fury
            if (Settings.UseDaevicFury && distance < 6 && Game.Player.DP >= 2000)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Daevic Fury I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Daevic Fury I");
                    return false;
                }
            }

            // Zikel's Threat I - Blessing of Nezekan I
            if (Settings.UseBlessingOfNezekanOrZikelsThreat && distance < 6 && Game.Player.DP >= 4000)
            {
                // Blessing of Nezekan I
                if (Game.Player.IsElyos && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(390))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(390);
                    return false;
                }

                // Zikel's Threat I
                if (Game.Player.IsAsmodian && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(391))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(391);
                    return false;
                }
            }

            // Lockdown
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown VI"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown VI");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown V"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown V");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown IV"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown IV");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown III"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown III");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown II"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown II");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Lockdown I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Lockdown I");
                return false;
            }

            // Taunt
            if (Settings.UseTaunt && entity.TargetId != 0 && entity.TargetId != Game.Player.Id)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Taunt IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Taunt IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Taunt III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Taunt III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Taunt II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Taunt II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Taunt I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Taunt I");

                }
            }

            if (Settings.AllowAoe && distance < 6)
            {
                var mobsAroundUs = CountMods(6);
                if (mobsAroundUs > 0)
                {
                    if (!isDualWeapon)
                    {
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seismic Wave V"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seismic Wave V");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seismic Wave IV"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seismic Wave IV");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seismic Wave III"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seismic Wave III");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seismic Wave II"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seismic Wave II");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seismic Wave I"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seismic Wave I");
                            return false;
                        }
                    }

                    // Earthquake Wave
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Earthquake Wave II"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Earthquake Wave II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Earthquake Wave I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Earthquake Wave I");
                        return false;
                    }





                    if (Game.Player.IsElyos)
                    {
                        // Shattering Wave
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(399))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(399);
                            return false;
                        }

                        // Ancestral Force Blast
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(396))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(396);
                            return false;
                        }

                        // Force Blast
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(320))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(320);
                            return false;
                        }
                    }
                    else
                    {
                        // Piercing Rupture
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(400))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(400);
                            return false;
                        }

                        // Ancestral Piercing Wave
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(397))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(397);
                            return false;
                        }


                        // Piercing Wave
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(321))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(321);
                            return false;
                        }
                    }


                    // Severe Precision Cut - Precision Cut
                    if (!isDualWeapon)
                    {
                        // Severe Precision Cut
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Severe Precision Cut II"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Severe Precision Cut II");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Severe Precision Cut I"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Severe Precision Cut I");
                            return false;
                        }

                        // Precision Cut
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Precision Cut IV"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Precision Cut IV");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Precision Cut III"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Precision Cut III");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Precision Cut II"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Precision Cut II");
                            return false;
                        }
                        else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Precision Cut I"))
                        {
                            WaitAutoAttack = true;
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Precision Cut I");
                            return false;
                        }
                    }
                }
            }


            // Aerial Lockdown
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Aerial Lockdown III"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Aerial Lockdown III");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Aerial Lockdown II"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Aerial Lockdown II");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Aerial Lockdown I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Aerial Lockdown I");
                return false;
            }

            if (ExecuteSkillFromList(entity, dmg_skills).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            // Initial Attack 1: Automatic Attack
            if (true)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(Game.AttackChatCommand);
            }

            return true;
        }

        public override bool PartyCheck()
        {
            return true;
        }

        public override bool Heal()
        {
            return true;
        }

        public override bool NoGravityAntiStuck()
        {
            return true;
        }

        public override bool Pause()
        {
            // Stamina recovery
            if (Game.Player.HealthMaximum - Game.Player.HealthCurrent > 1500 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stamina Recovery I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stamina Recovery I");
                return false;
            }

            // Second Wind
            if (Game.Player.HealthPercentage < 60 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Second Wind I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Second Wind I");
                return false;
            }
            // Improved stamina I
            else if (Game.Player.HealthPercentage < 80 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("269"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("269");
                return false;
            }

            DateTime tempTime = DateTime.Now;
            if (combatPreparationBuffTimer < tempTime)
            {
                var combatPreparationBuff = Game.AbilityList.GetAbility("Aion's Strength I");
                if (combatPreparationBuff != null && combatPreparationBuff.State == 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Aion's Strength I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Aion's Strength I");
                    combatPreparationBuffTimer = tempTime.AddSeconds(10);
                    return false;
                }
            }

            return true;
        }

        public bool IsDualWeaponEquipped()
        {
            bool mainHand1H = Game.InventoryList.GetList()
               .Where(i => i.ItemType == AionGame.Enums.eInventoryType.Weapon1H && i.SlotType == AionGame.Enums.eInventorySlotType.MainHand_Equipped)
               .Any();

            bool offHand1H = Game.InventoryList.GetList()
                .Where(i => i.ItemType == AionGame.Enums.eInventoryType.Weapon1H && i.SlotType == AionGame.Enums.eInventorySlotType.OffHand_Equipped)
                .Any();

            return mainHand1H && offHand1H;
        }
    }
}
