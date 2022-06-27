using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class ClericSettings : AionClassBaseSetting
    {
        /// <summary>
        /// Indicates whether or not party healing are allowed
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowPartyHealing", "Allow party healing")]
        public bool AllowPartyHealing { get; set; }

        /// <summary>
        /// Indicates whether or not party resurrection are allowed
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowPartyResurrection", "Allow party resurrection")]
        public bool AllowPartyResurrection { get; set; }

        /// <summary>
        /// Indicates whether or not using Hand of Reincarnation is allowed.
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowHandOfReincarnation", "Allow Hand Of Reincarnation")]
        public bool AllowHandOfReincarnation { get; set; }

        /// <summary>
        /// Indicates whether or not using Hand of Reincarnation is allowed.
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowRebirth", "Allow Rebirth")]
        public bool AllowRebirth { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowPenance", "Allow Penance")]
        public bool AllowPenance { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowSacrificalPower", "Allow Sacrifical Power")]
        public bool AllowSacrificalPower { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_SplendourOfRecoveryHPLow", "Splendour of Recovery HP < %")]
        public int SplendourOfRecoveryHPLow { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_SavingGraceLowHP", "Saving Grace HP < %")]
        public int SavingGraceLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_RadiantCureLowHP", "Radiant Cure HP < %")]
        public int RadiantCureLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_LightOfRecovery", "Light of Recovery HP < %")]
        public int LightOfRecovery { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_FlashOfRecovery", "Flash of Recovery HP < %")]
        public int FlashOfRecovery { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_SplendourOfRebirth", "Splendour of Rebirth HP < %")]
        public int SplendourOfRebirth { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_RippleOfPurification", "Ripple of Purification HP < %")]
        public int RippleOfPurification { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_HealingLightLowHP", "Healing Light HP < %")]
        public int HealingLightLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_HealingWindLowHP", "Healing Wind HP < %")]
        public int HealingWindLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_SplendorOfRecoveryLowHP", "Splendor Of Recovery HP < %")]
        public int SplendorOfRecoveryLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_SummonHealingServantLowHP", "Summon Healing Servant HP < %")]
        public int SummonHealingServantLowHP { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowSummerCircleBuff", "Allow Summer Circle Buff")]
        public bool AllowSummerCircleBuff { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowWinterCircleBuff", "Allow Winter Circle Buff")]
        public bool AllowWinterCircleBuff { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_AllowSalvation_PandaemoniumsProtection", "Allow Salvation - Pandaemonium's Protection")]
        public bool AllowSalvation_PandaemoniumsProtection { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_UseSalvation_PandaemoniumsProtectionLowMPPErcentage", "Use Salvation - Pandaemonium's Protection MP < %")]
        public int UseSalvation_PandaemoniumsProtectionLowMPPErcentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_UseSagesWisdomHPPercentage", "Use Sage's Wisdom HP < %")]
        public int UseSagesWisdomHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_UseThornySkin", "Use Thorny Skin")]
        public bool UseThornySkin { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_CureMindSkillId", "Use Cure Mind With skill ID")]
        public string CureMindSkillId { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_DispelSkillId", "Use Dispel With skill ID")]
        public string DispelSkillId { get; set; }

        [Browsable(false)]
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_MasterName", "Master Name")]
        public string MasterName { get; set; }

        public ClericSettings() : base()
        {
            AllowPartyHealing = true;
            AllowPartyResurrection = true;
            AllowHandOfReincarnation = true;
            AllowRebirth = true;
            AllowPenance = false;
            AllowSalvation_PandaemoniumsProtection = false;
            UseSalvation_PandaemoniumsProtectionLowMPPErcentage = 50;
            UseSagesWisdomHPPercentage = 50;
            UseThornySkin = true;


            SplendourOfRecoveryHPLow = 70;
            SavingGraceLowHP = 45;
            RadiantCureLowHP = 50;
            LightOfRecovery = 0;
            FlashOfRecovery = 50;
            SplendourOfRebirth = 50;
            HealingLightLowHP = 0;
            SummonHealingServantLowHP = 50;
            RippleOfPurification = 50;
            HealingWindLowHP = 50;
            SplendorOfRecoveryLowHP = 50;
            TargetSearchDistance = 25;

            CureMindSkillId = "";
            DispelSkillId = "";
            MasterName = "";
        }

        public ClericSettings(ClericSettings classSettings) : base(classSettings)
        {
            AllowPartyHealing = classSettings.AllowPartyHealing;
            AllowPartyResurrection = classSettings.AllowPartyResurrection;
            AllowHandOfReincarnation = classSettings.AllowHandOfReincarnation;
            AllowRebirth = classSettings.AllowRebirth;
            AllowPenance = classSettings.AllowPenance;
            AllowSacrificalPower = classSettings.AllowSacrificalPower;

            SplendourOfRecoveryHPLow = classSettings.SplendourOfRecoveryHPLow;
            SavingGraceLowHP = classSettings.SavingGraceLowHP;
            RadiantCureLowHP = classSettings.RadiantCureLowHP;
            LightOfRecovery = classSettings.LightOfRecovery;
            FlashOfRecovery = classSettings.FlashOfRecovery;
            SplendourOfRebirth = classSettings.SplendourOfRebirth;
            RippleOfPurification = classSettings.RippleOfPurification;
            HealingLightLowHP = classSettings.HealingLightLowHP;
            HealingWindLowHP = classSettings.HealingWindLowHP;
            SplendorOfRecoveryLowHP = classSettings.SplendorOfRecoveryLowHP;
            SummonHealingServantLowHP = classSettings.SummonHealingServantLowHP;

            AllowSummerCircleBuff = classSettings.AllowSummerCircleBuff;
            AllowWinterCircleBuff = classSettings.AllowWinterCircleBuff;

            AllowSalvation_PandaemoniumsProtection = classSettings.AllowSalvation_PandaemoniumsProtection;
            UseSalvation_PandaemoniumsProtectionLowMPPErcentage = classSettings.UseSalvation_PandaemoniumsProtectionLowMPPErcentage;
            UseSagesWisdomHPPercentage = classSettings.UseSagesWisdomHPPercentage;
            UseThornySkin = classSettings.UseThornySkin;

            CureMindSkillId = classSettings.CureMindSkillId;
            DispelSkillId = classSettings.DispelSkillId;
            MasterName = classSettings.MasterName;
        }
    }

    public class Cleric : AionClassBase
    {
        // ----------------------------------------------------------------------
        private DateTime StateDispelTime { get; set; }
        private Dictionary<uint, Dictionary<uint, int>> StateArray { get; set; }
        private Dictionary<uint, DateTime> StateUndispellable { get; set; }
        public bool PerformFlash { get; set; }
        DateTime sacrificialPowerBuffTimer { get; set; }
        private ClericSettings Settings { get; set; }
        private uint[] CureMindSkillId { get; set; }
        private uint[] DispelSkillId { get; set; }


        private string[] skillDamage = new string[]
        {
            "1043", // Summon Noble Energy I - Elyos
            "1044", // Summon Noble Energy I - Asmo
            "2144", // Summon Noble Energy II - Elyos
            "2145", // Summon Noble Energy II - Asmo

            "1131", // "Summon Holy Servant IV", - Asmo
            "1068", // "Summon Holy Servant III", - Asmo
            "1067", // "Summon Holy Servant II", - Asmo
            "1066", // "Summon Holy Servant I", - Asmo
            
            "1132", // "Summon Holy Servant IV", - Elyos
            "1085", // "Summon Holy Servant III", - Elyos
            "1084", // "Summon Holy Servant II", - Elyos
            "1083", // "Summon Holy Servant I", - Elyos




            "1181", // "Chastise II",
            "1170", // "Chastise I",

            "2140", // "Earth's Wrath V",
            "1037", // "Earth's Wrath IV",
            "1036", // "Earth's Wrath III",
            "1035", // "Earth's Wrath II",
            "1034", // "Earth's Wrath I",

            "1030", // "Punishing Earth I",

            "2147", // "Punishing Wind II",
            "1177", // "Punishing Wind I",
            "2148", // "Slashing Wind II",
            "1178", // "Slashing Wind I",

            "2284", // "Storm of Aion IV",
            "2283", // "Storm of Aion III",
            "2282", // "Storm of Aion II",
            "2281", // "Storm of Aion I",

            "988", // "Smite V",
            "978", // "Smite IV",
            "977", // "Smite III",
            "976", // "Smite II",
            "975", // "Smite I",
        };

        private string[] skillChainDamage = new string[]
        {
            "982", // "Infernal Blaze IV",
            "981", // "Infernal Blaze III",
            "980", // "Infernal Blaze II",
            "979", // "Infernal Blaze I",

            "1115", // "Divine Touch III",
            "1072", // "Divine Touch II",
            "1071", // "Divine Touch I",

            "1124", // "Divine Spark III",
            "1070", // "Divine Spark II",
            "1069", // "Divine Spark I",

            "1117", // "Thunderbolt IV",
            "1088", // "Thunderbolt III",
            "1087", // "Thunderbolt II",
            "1086", // "Thunderbolt I",
        };


        public Cleric() : this(new ClericSettings())
        {
        }

        public Cleric(ClericSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Cleric;

            // 
            StateDispelTime = DateTime.MinValue;
            StateArray = new Dictionary<uint, Dictionary<uint, int>>();
            StateUndispellable = new Dictionary<uint, DateTime>();
            PerformFlash = false;
            sacrificialPowerBuffTimer = DateTime.MinValue;

            CureMindSkillId = new uint[] { };
            DispelSkillId = new uint[] { };


            // Parse skill id where to use "Cure Mind"

            if (string.IsNullOrWhiteSpace(Settings.CureMindSkillId) == false)
            {
                var skillIds = Settings.CureMindSkillId.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<uint> tempSkillIds = new List<uint>();

                foreach (var tempName in skillIds)
                {
                    if (string.IsNullOrWhiteSpace(tempName) == false)
                    {
                        string trimmedName = tempName.Trim();
                        int tempSkillId = 0;
                        if (int.TryParse(trimmedName, out tempSkillId))
                        {
                            tempSkillIds.Add((uint)tempSkillId);
                        }
                    }
                }

                CureMindSkillId = tempSkillIds.ToArray();
            }

            // Parse skill id where to use "Dispel"
            if (string.IsNullOrWhiteSpace(Settings.DispelSkillId) == false)
            {
                var skillIds = Settings.DispelSkillId.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                List<uint> tempSkillIds = new List<uint>();

                foreach (var tempName in skillIds)
                {
                    if (string.IsNullOrWhiteSpace(tempName) == false)
                    {
                        string trimmedName = tempName.Trim();
                        int tempSkillId = 0;
                        if (int.TryParse(trimmedName, out tempSkillId))
                        {
                            tempSkillIds.Add((uint)tempSkillId);
                        }
                    }
                }

                DispelSkillId = tempSkillIds.ToArray();
            }
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

            var distance = entity.Position.Distance(Game.Player.Position);

            if (ExecuteSkillFromList(entity, skillChainDamage).Item1 == false)
            {
                return false;
            }

            // Player only
            //if (entity.HealthPercentage > 70)
            //{
            //    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Chain of Suffering IV"))
            //    {
            //        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Chain of Suffering IV");
            //        return false;
            //    }
            //    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Chain of Suffering III"))
            //    {
            //        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Chain of Suffering III");
            //        return false;
            //    }
            //    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Chain of Suffering II"))
            //    {
            //        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Chain of Suffering II");
            //        return false;
            //    }
            //    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Chain of Suffering I"))
            //    {
            //        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Chain of Suffering I");
            //        return false;
            //    }
            //}

            // Shield
            if (Game.Player.HealthPercentage < 40 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Immortal Shroud I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Immortal Shroud I");
                return false;
            }


            if (Game.Player.HealthPercentage < 75 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessed Shield I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessed Shield I");
                return false;
            }

            // Sage's Wisdom I
            if (Game.Player.HealthPercentage< Settings.UseSagesWisdomHPPercentage&& AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1102))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1102);
                return false;
            }

            // Thorny Skin I
            if (Settings.UseThornySkin&&Game.Player.HealthPercentage>20)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2133))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2133);
                    return false;
                }
                else if(AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1104))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1104);
                    return false;
                }
                else if(AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1103))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1103);
                    return false;
                }
            }
           

            if (entity.HealthPercentage > 60 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Call Lightning I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Call Lightning I");
                return false;
            }

            if (entity.HealthCurrent > 8500)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Enfeebling Burst II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Enfeebling Burst II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Enfeebling Burst I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Enfeebling Burst I");
                    return false;
                }
            }

            if (distance < 4)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike I");
                    return false;
                }
            }

            if (ExecuteSkillFromList(entity, skillDamage).Item1 == false)
            {
                return false;
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool PartyCheck()
        {
            List<Entity> candidateMembers = new List<Entity>();

            // Copy member list to new list
            List<Entity> memberList = new List<Entity>(Game.PartyMemberList.GetList().Where(w => w.Value.GetEntity() != null).Select(s => s.Value.GetEntity()));

            if (string.IsNullOrWhiteSpace(Settings.MasterName) == false)
            {
                var masterEntity = Game.EntityList.GetEntity(Settings.MasterName);
                if (masterEntity != null)
                {
                    memberList.Add(masterEntity);
                }
            }

            int healingGroupBenefitCount = 0;

            // Loop tra i membri del gruppo e ordinali in base agli HP mancanti. Controlla se il membro del gruppo può essere avvantaggiato da una cura AoE
            foreach (var tempMember in memberList)
            {
                if (tempMember != null && tempMember.Position.DistanceToPosition(Game.Player.Position) < 20)
                {
                    if (tempMember.IsDead == false)
                    {

                        candidateMembers.Add(tempMember);

                        if ((tempMember.HealthMaximum - tempMember.HealthCurrent) > 2000)
                        {
                            healingGroupBenefitCount++;
                        }
                    }
                }

            }

            if (Settings.AllowPartyHealing)
            {
                // Order list by HP (less HP have priority)
                var hpOrder = candidateMembers.OrderBy(o => o.HealthPercentage).ToList();

                // Healing Wind
                if (hpOrder.Where(w => w.HealthPercentage < Settings.HealingWindLowHP).Count() >= 2)
                {
                    if (CheckAndExecuteSkills(null, "Healing Wind", 4))
                    {
                        return false;
                    }
                }

                // Splendor of Recover
                if (hpOrder.Where(w => w.HealthPercentage < Settings.SplendorOfRecoveryLowHP).Count() >= 2)
                {
                    string splendorOfRecoveryName = "Splendor of Recovery";
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(splendorOfRecoveryName + " III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(splendorOfRecoveryName + " III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(splendorOfRecoveryName + " II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(splendorOfRecoveryName + " II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(splendorOfRecoveryName + " I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(splendorOfRecoveryName + " I");
                        return false;
                    }
                }


                // Loop della list e inizia a curare i membri
                foreach (var tempMember in hpOrder)
                {
                    if (CheckHeal(tempMember, healingGroupBenefitCount) == false)
                        return false;
                }
            }

            // Buff force members
            foreach (var tempMember in candidateMembers)
            {
                if (tempMember.Position.DistanceToPosition(Game.Player.Position) < 20 && TargetBuff(tempMember) == false)
                    return false;
            }

            // Ress party member
            if (Settings.AllowPartyResurrection)
            {
                foreach (var tempMember in memberList)
                {
                    var memberEntity = Game.EntityList.GetEntity(tempMember.Id);
                    // If is death
                    if (memberEntity != null && memberEntity.Id != Game.Player.Id && memberEntity.IsDead && memberEntity.Position.DistanceToPosition(Game.Player.Position) < 20)
                    {
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Resurrection I"))
                        {
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Resurrection I", memberEntity);
                            return false;
                        }
                    }
                }
            }

            // Dispel
            foreach (var tempMember in memberList)
            {
                var memberEntity = Game.EntityList.GetEntity(tempMember.Id);
                // If is death
                if (memberEntity != null && memberEntity.Id != Game.Player.Id && memberEntity.IsDead == false && memberEntity.Position.DistanceToPosition(Game.Player.Position) < 20)
                {
                    if (CheckDispel(memberEntity) == false)
                        return false;
                }
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Heal()
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Remove Shock I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Remove Shock I");
                return false;
            }

            if (CheckHeal(Game.Player) == false)
                return false;


            if (CheckDispel(Game.Player) == false)
                return false;

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Pause()
        {
            if (TargetBuff(Game.Player) == false)
            {
                return false;
            }

            // Istant
            if (Game.Player.HealthPercentage < 50)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery VI"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery VI", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery V", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery IV", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery III", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery II", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery I", Game.Player);
                    return false;
                }
            }

            // 
            if (Game.Player.HealthPercentage < 95 && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Light of Rejuvenation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                    && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Revival", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation IV", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation III", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation II", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation I", Game.Player);
                    return false;
                }
            }


            // Penance
            if (Settings.AllowPenance)
            {
                var missingMane = Game.Player.ManaMaximum - Game.Player.ManaCurrent;
                if (missingMane > 2000 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Penance IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Penance IV", Game.Player);
                    return false;
                }
                else if (missingMane > 1700 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Penance III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Penance III", Game.Player);
                    return false;
                }
                else if (missingMane > 1200 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Penance II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Penance II", Game.Player);
                    return false;
                }
                else if (missingMane > 800 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Penance I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Penance I", Game.Player);
                    return false;
                }
            }


            // Salvation I - Pandaemonium's Protection I
            if (Settings.AllowSalvation_PandaemoniumsProtection && Game.Player.DP >= 2000)
            {
                if (Game.Player.HealthPercentage <= 20 || Game.Player.ManaPercentage <= Settings.UseSalvation_PandaemoniumsProtectionLowMPPErcentage)
                {
                    // Salvation I
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1166))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1166);
                        return false;
                    }

                    // Pandaemonium's Protection I
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1171))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1171);
                        return false;
                    }
                }
            }


            // Promise of Wind
            if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Promise of Wind", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Promise of Wind I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Promise of Wind I");
                    return false;
                }
            }


            // Rebirth to yourslef
            if (Settings.AllowRebirth
                && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Rebirth", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Hand of Reincarnation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rebirth I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rebirth I", Game.Player);
                return false;
            }

            // Hand of Reincarnation I to yourslef
            if (Settings.AllowHandOfReincarnation
                && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Rebirth I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Hand of Reincarnation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hand of Reincarnation I"))
                {
                    AionGame.Game.Player.SetTarget(Game.Player);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hand of Reincarnation I", Game.Player);
                    return false;
                }
            }

            //Uses Splendor of Flight if Flight time is 30 seconds lower than maximum flight time. Change that value according to your needs.
            if (Game.Player.IsFlying && (Game.Player.FlightTimeMaximum - Game.Player.FlightTimeCurrent) >= 30000 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Splendor of Flight I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Splendor of Flight I");
                return false;
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        private bool CheckState(Entity entity)
        {
            /*    // Retrieve the range of the entity compared to my own character position.
                var Range = Game.Player.Position.DistanceToPosition(entity.Position);

                // Check if this routine is allowed to be ran under the current circumstances.
                if (entity.IsDead || (Settings.AllowApproach == false && Range > 23))
                {
                    return true;
                }

                // Retrieve the state for the current entity to inspect.
                var EntityState = entity.StateList;


                // Loop through the states only when we are available to dispel them. We still check for removed states!
                if (EntityState != null && StateDispelTime < DateTime.Now)
                {


                    // Create the state array for the current entity if it does not exist.
                    if (StateArray.ContainsKey(entity.Id) == false)
                    {
                        StateArray[entity.Id] = new Dictionary<uint, int>();
                    }

                    // Loop through the states to find which need to be removed.
                    foreach (var itemState in EntityState.GetList())
                    {
                        var skill = itemState.Value;

                        // Check if the current skill is valid and has not been marked and undispellable.
                        if (skill != null && skill.IsDebuff && (StateUndispellable.ContainsKey(skill.Id) == false || StateUndispellable[skill.Id] < DateTime.Now))
                        {
                            // Check if this entity had the current skill effect on him and hasn't been removed by either Cure Mind or Dispel.
                            if (StateArray[entity.Id].ContainsKey(skill.Id) && StateArray[entity.Id][skill.Id] == 2)
                            {
                                StateUndispellable[skill.Id] = DateTime.Now.AddSeconds(30);
                            }
                            // Remove the state from the entity.
                            else
                            {
                                // Retrieve the magical state the current skill.
                                var RemoveMagical = skill.IsMagical();

                                // Check if we are required to change the magical state for the current skill.
                                if (StateArray[entity.Id].ContainsKey(skill.Id))
                                {
                                    RemoveMagical = !RemoveMagical;
                                }

                                // Check if the dispel or cure mind can be executed correctly. The function might need to set the target first!
                                if ((RemoveMagical && Helper.HelperFunction.CheckExecute("Cure Mind", entity) == Helper.HelperFunction.eSkillExecuteResult.Executed) || (RemoveMagical == false && Helper.HelperFunction.CheckExecute("Dispel", entity) == Helper.HelperFunction.eSkillExecuteResult.Executed))
                                {
                                    // Change the state dispel timer to prevent dispel and cure mind from being used too quickly.
                                    StateDispelTime = DateTime.Now.AddMilliseconds(500);

                                    // Track the current state of the dispel and cure mind to find undispellable states.
                                    if (StateArray[entity.Id].ContainsKey(skill.Id) == false)
                                    {
                                        StateArray[entity.Id].Add(skill.Id, 1);
                                        return false;
                                    }
                                    else
                                    {
                                        StateArray[entity.Id].Add(skill.Id, 2);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (var pairs in StateArray[entity.Id])
                {
                    if (EntityState.GetState(pairs.Key) == null)
                    {
                        if (StateArray[entity.Id].ContainsKey(pairs.Key))
                            StateArray[entity.Id].Remove(pairs.Key);
                    }
                }*/

            // Return true to let the caller know this function completed.
            return true;
        }
        private bool CheckHeal(Entity entity, int memberToHealCount = 1)
        {
            if (entity == null)
                return true;

            // Retrieve the range of the entity compared to my own character position.
            var Range = Game.Player.Position.DistanceToPosition(entity.Position);
            if (Range > 23)
            {
                return true;
            }

            // Change the healing routine if I'm healing myself when allowed to attack.
            if (Range < 20)//(entity.Id == Game.Player.Id)
            {
                if (entity.HealthPercentage < Settings.FlashOfRecovery)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery VI"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery VI", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery V"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery V", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery IV", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery III", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery II", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flash of Recovery I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flash of Recovery I", entity);
                        return false;
                    }
                }

                if (entity.HealthPercentage < Settings.RadiantCureLowHP)
                {
                    if (CheckAndExecuteSkills(entity, "Radiant Cure", 5))
                    {
                        return false;
                    }
                }

                // Over time heal
                if (entity.HealthPercentage < 50 && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Splendor of Rebirth", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Splendor of Rebirth IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Splendor of Rebirth IV", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Splendor of Rebirth III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Splendor of Rebirth III", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Splendor of Rebirth II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Splendor of Rebirth II", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Splendor of Rebirth I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Splendor of Rebirth I", entity);
                        return false;
                    }
                }

                if (entity.HealthPercentage < 95 && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Light of Rejuvenation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                    && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Revival", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation IV", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation III", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation II", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Rejuvenation I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Rejuvenation I", entity);
                        return false;
                    }
                }

                if (entity.HealthPercentage < Settings.LightOfRecovery)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Recovery III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Recovery III", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Recovery II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Recovery II", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Recovery I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Recovery I", entity);
                        return false;
                    }
                }

                if (entity.HealthPercentage < Settings.HealingLightLowHP)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light V"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light V", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light IV", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light III", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light II", entity);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light I", entity);
                        return false;
                    }
                }

                if (entity.HealthPercentage < Settings.SummonHealingServantLowHP)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summon Healing Servant II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summon Healing Servant II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summon Healing Servant I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summon Healing Servant I");
                        return false;
                    }
                }
            }




            // Return true to let the caller know this function completed.
            return true;
        }

        private bool CheckDispel(Entity entity)
        {
            // Cure Mind
            if (entity.StateList.GetList().Where(s => CureMindSkillId.Contains(s.Key)).Any())
            {
                string cureMindName = "Cure Mind";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(cureMindName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(cureMindName + " II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(cureMindName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(cureMindName + " I", entity);
                    return false;
                }
            }

            // Dispel
            if (entity.StateList.GetList().Where(s => DispelSkillId.Contains(s.Key)).Any())
            {
                string dispelName = "Dispel";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(dispelName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(dispelName + " III", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(dispelName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(dispelName + " II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(dispelName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(dispelName + " I", entity);
                    return false;
                }
            }

            // Return true to let the caller know this function completed.
            return true;
        }

        private bool TargetBuff(Entity entity)
        {
            // Buff Blessing of Health
            if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Health", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Health I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Health I", entity);
                    return false;
                }
            }

            // Blessing of Stone
            if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Stone", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Rock", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Rock I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Rock I", entity);
                        return false;
                    }
                }
            }

            // Check if AllowSummerCircleBuff is set and if entity needs Summer Circle State
            if (Settings.AllowSummerCircleBuff && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Summer Circle", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summer Circle III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summer Circle III", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summer Circle II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summer Circle II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Summer Circle I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Summer Circle I", entity);
                    return false;
                }
            }

            // Check if AllowWinterCircleBuff is set and if entity needs Winter Circle State
            if (Settings.AllowSummerCircleBuff == false && Settings.AllowWinterCircleBuff && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Winter Circle", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Winter Circle III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Winter Circle III", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Winter Circle II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Winter Circle II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Winter Circle I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Winter Circle I", entity);
                    return false;
                }
            }

            // Hand of Reincarnation I for party members
            if (Settings.AllowHandOfReincarnation && entity.Id != Game.Player.Id
                && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Hand of Reincarnation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Rebirth I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hand of Reincarnation I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hand of Reincarnation I", entity);
                    return false;
                }
            }

            return true;
        }

        public override bool NoGravityAntiStuck()
        {
            return true;
        }
    }
}
