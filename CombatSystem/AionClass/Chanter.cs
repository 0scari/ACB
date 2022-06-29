using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System.ComponentModel;
using AionBotnet.AionGame;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public enum ChanterMantraType
    {
        CelerityMantra, // Move Speed
        ClementMindMantra, // Mp mantra
        HitMantra, // Crit Strike, Crit Spell, Strike Resist
        IntensityMantra, // Crit Strike
        InvincibilityMantra, // Mp, Attack, Phys Def, Mag Boost
        MagicMantra, // Mag Boost, Mag Acc
        ProtectionMantra, // Elemental Def
        RevivalMantra, // HP
        ShieldMantra, // Phys Def, Block, Evasion
        VictoryMantra, // Phys Attack

        None // No mantra
    }


    public class ChanterSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowWeaving", "Allow Weaving")]
        public bool AllowWeaving { get; set; }

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


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_AllowWoW", "Allow Word of Wind")]
        public bool AllowWoW { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseYustielsProtection_MarchutansProtection", "Use Yustiel's Protection\\Marchutan's Protection")]
        public bool UseYustielsProtection_MarchutansProtection { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseWordOfInspiration", "Use Word Of Inspiration")]
        public bool UseWordOfInspiration { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseWordOfProtection", "Use Word Of Protection")]
        public bool UseWordOfProtection { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseWordOfSpellstoppingAethericFieldLowHP", "Use Word of Spellstopping\\AethericField if HP < %")]
        public int UseWordOfSpellstoppingAethericFieldLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseWordOfQuicknessLowHP", "Use Word of Quickness if HP < %")]
        public int UseWordOfQuicknessLowHP { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UseRageSpell", "Use Rage Spell")]
        public bool UseRageSpell { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UsePromiseOfEarth", "Use Promise of Earth")]
        public bool UsePromiseOfEarth { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UsePromiseOfWind", "Use Promise of Wind")]
        public bool UsePromiseOfWind { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_UsePromiseOfAether", "Use Promise of Aether")]
        public bool UsePromiseOfAether { get; set; }

        // Healing Skill
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_RecoveryMagicLowMP", "Recovery Magic MP < %")]
        public int RecoveryMagicLowMP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_HealingLightLowHP", "Healing Light HP < %")]
        public int HealingLightLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_ProtectiveWardLowHP", "Protective Ward HP < %")]
        public int ProtectiveWardLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_HealingBurstLowHP", "Healing Burst HP < %")]
        public int HealingBurstLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_RecoverySpellLowHP", "Recovery Spell HP < %")]
        public int RecoverySpellLowHP { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_PerfectParryLowHP", "Perfect Parry if HP < %")]
        public int PerfectParryLowHP { get; set; }

        // Mantras
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_MantraFirst", "Mantra 1")]
        public ChanterMantraType MantraFirst { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_MantraSecond", "Mantra 2")]
        public ChanterMantraType MantraSecond { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_MantraThird", "Mantra 3")]
        public ChanterMantraType MantraThird { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Chanter_Test", "TESTo")]
        public ChanterMantraType Test { get; set; }

        [Browsable(false)]
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Cleric_MasterName", "Master Name")]
        public string MasterName { get; set; }

        public ChanterSettings() : base()
        {
            TargetSearchDistance = 10;

            AllowWeaving = false;
            AllowPartyHealing = true;
            AllowPartyResurrection = true;
            AllowWoW = false;
            UseYustielsProtection_MarchutansProtection = true;
            UseWordOfInspiration = false;
            UseWordOfProtection = false;
            UseWordOfSpellstoppingAethericFieldLowHP = 50;
            UseWordOfQuicknessLowHP = 50;
            UseRageSpell = false;
            UsePromiseOfEarth = false;
            UsePromiseOfWind = true;
            UsePromiseOfAether = false;
            RecoveryMagicLowMP = 70;
            HealingLightLowHP = 70;
            ProtectiveWardLowHP = 50;
            HealingBurstLowHP = 60;
            RecoverySpellLowHP = 60;
            PerfectParryLowHP = 50;

            MantraFirst = ChanterMantraType.CelerityMantra;
            MantraSecond = ChanterMantraType.InvincibilityMantra;
            MantraThird = ChanterMantraType.HitMantra;

            MasterName = "";
        }

        public ChanterSettings(ChanterSettings classSettings) : base(classSettings)
        {
            AllowWeaving = classSettings.AllowWeaving;
            AllowPartyHealing = classSettings.AllowPartyHealing;
            AllowPartyResurrection = classSettings.AllowPartyResurrection;
            AllowWoW = classSettings.AllowWoW;
            UseYustielsProtection_MarchutansProtection = classSettings.UseYustielsProtection_MarchutansProtection;
            UseWordOfInspiration = classSettings.UseWordOfInspiration;
            UseWordOfProtection = classSettings.UseWordOfProtection;
            UseWordOfSpellstoppingAethericFieldLowHP = classSettings.UseWordOfSpellstoppingAethericFieldLowHP;
            UseWordOfQuicknessLowHP = classSettings.UseWordOfQuicknessLowHP;
            UseRageSpell = classSettings.UseRageSpell;
            UsePromiseOfEarth = classSettings.UsePromiseOfEarth;
            UsePromiseOfWind = classSettings.UsePromiseOfWind;
            UsePromiseOfAether = classSettings.UsePromiseOfAether;
            RecoveryMagicLowMP = classSettings.RecoveryMagicLowMP;
            HealingLightLowHP = classSettings.HealingLightLowHP;
            ProtectiveWardLowHP = classSettings.ProtectiveWardLowHP;
            HealingBurstLowHP = classSettings.HealingBurstLowHP;
            RecoverySpellLowHP = classSettings.RecoverySpellLowHP;
            PerfectParryLowHP = classSettings.PerfectParryLowHP;

            MantraFirst = classSettings.MantraFirst;
            MantraSecond = classSettings.MantraSecond;
            MantraThird = classSettings.MantraThird;

            MasterName = classSettings.MasterName;
        }
    }

    public class Chanter : AionClassBase
    {
        private uint AttackStartedID { get; set; }

        private ulong CheckWorldOfWindTimer { get; set; }
        private ulong BuffCheckLastTimer { get; set; }
        private ChanterSettings Settings { get; set; }
        private DateTime PerfectParryTimeout { get; set; }

        public Chanter() : this(new ChanterSettings())
        {
            PerfectParryTimeout = DateTime.MinValue;
        }

        public Chanter(ChanterSettings settings)
        {
            Settings = settings;
            CurrentAionClass = eAionClass.Chanter;
            AttackStartedID = 0;
            CheckWorldOfWindTimer = 0;
            BuffCheckLastTimer = 0;
            PerfectParryTimeout = DateTime.MinValue;
        }



        private static string[] dmg_Skill12 = new string[]
        {
            "Mountain Crash II",
            "Mountain Crash I",

            "Disorienting Blow II",
            "Disorienting Blow I",

            "Numbing Blow I",

            "2287", // Soul Lock II
            "2286", // Soul Lock I

            "Inescapable Judgment II", // Elyos
            "Inescapable Judgment I",
            "Soul Strike II", // Asmo
            "Soul Strike I",

            "Hallowed Strike V",
            "Hallowed Strike IV",
            "Hallowed Strike III",
            "Hallowed Strike II",
            "Hallowed Strike I",

            "Meteor Strike IV",
            "Meteor Strike III",
            "Meteor Strike II",
            "Meteor Strike I",
        };

        private string[] chain_skills12 = new string[]{
            // Remove shock
            "Backshock I",
            "Retribution I",

            // Hallowed strike chain
            "Booming Assault III",
            "Booming Assault II",
            "Booming Assault I",

            "Booming Smash III",
            "Booming Smash II",
            "Booming Smash I",

            "Booming Strike IV",
            "Booming Strike III",
            "Booming Strike II",
            "Booming Strike I",

            "Infernal Blaze I",

            // Meteor Strike chain
            "Pentacle Shock III",
            "Pentacle Shock II",
            "Pentacle Shock I",

            "Incandescent Blow IV",
            "Incandescent Blow III",
            "Incandescent Blow II",
            "Incandescent Blow I",

            // Stumble target
            "Seismic Crash I",

            "Resonance Haze II",
            "Resonance Haze I",

            // Stunned target
            "Soul Crush II",
            "Soul Crush I",

            // Parry
            "Confident Defense I", // Def buff

            "Splash Swing III",
            "Splash Swing II",
            "Splash Swing I",

            "Parrying Strike III",
            "Parrying Strike II",
            "Parrying Strike I",
        };

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
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Remove Shock I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Remove Shock I");
                return false;
            }

            if (DateTime.Now < PerfectParryTimeout) {
                System.Threading.Thread.Sleep(1000);
                return false;
            }

			ActivateMantras(true);

            if (Settings.AllowWeaving && WaitingAutoAttack())
                return false;

            if (ExecuteSkillFromList(entity, chain_skills12).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            float entityDist = entity.Position.Distance(Game.Player.Position);

            // Word of Protection
            if (Game.Player.HealthPercentage <= 60 || Settings.UseWordOfProtection)
            {
                if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Protection", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Protection I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Protection I");
                    return false;
                }
            }

            // Protective Ward
            if (Game.Player.HealthPercentage < Settings.ProtectiveWardLowHP)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward VI"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward VI", null);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward V", null);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward IV", null);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward III", null);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward II", null);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protective Ward I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protective Ward I", null);
                    return false;
                }
            }

            if (entityDist < 6)
            {
                // Focused Parry
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Focused Parry I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Focused Parry I", null);
                    return false;
                }

                // Perfect Parry
                if (Game.Player.HealthPercentage < Settings.PerfectParryLowHP && entity.HealthCurrent > 4000)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2288))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2288, null);
                        PerfectParryTimeout = DateTime.Now.Add(new TimeSpan(0, 0, 3));
                        return false;
                    }
                }

            }

            // Word of Wind I
            if (Game.Time() > CheckWorldOfWindTimer + 15000 && Settings.AllowWoW && Game.Player.DP >= 2000)
            {
                CheckWorldOfWindTimer = Game.Time();

                if (Game.Player.StateList.GetList().Where(s => s.Value.Id == 1328).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("1328"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("1328");
                        return false;
                    }
                }
            }

            // Word of Quickness
            if (Game.Player.HealthPercentage < Settings.UseWordOfQuicknessLowHP)
            {
                if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Quickness I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1326))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1326);
                        return false;
                    }
                }
            }

            // Word of inspiration
            if (Settings.UseWordOfInspiration && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Inspiration", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Inspiration I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Inspiration I");
                    return false;
                }
            }

            // Word of Spellstopping \ Aetheric Field
            if (Game.Player.HealthPercentage < Settings.UseWordOfSpellstoppingAethericFieldLowHP)
            {
                if (Game.Player.IsElyos)
                {
                    // Divine Curtain I
                    if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Divine Curtain", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1197))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1197);
                        return false;
                    }
                    else if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Spellstopping", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1344))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1344);
                        return false;
                    }
                    else if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Ancestral Word of Spellstopping", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1199))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1199);
                        return false;
                    }
                }
                else
                {
                    // Curtain of Aether I
                    if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Curtain of Aether", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1198))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1198);
                        return false;
                    }
                    else if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Aetheric Field", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1345))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1345);
                        return false;
                    }
                    else if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Ancestral Aetheric Field", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1200))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1200);
                        return false;
                    }
                }
            }

            // Blessing of Wind
            if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Wind", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Wind IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Wind IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Wind III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Wind III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Wind II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Wind II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Wind I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Wind I");
                    return false;
                }
            }

            // Yustiel's Protection I - Marchutan's Protection I
            if (entityDist < 6 && Settings.UseYustielsProtection_MarchutansProtection && Game.Player.DP >= 2000)
            {
                if (Game.Player.IsElyos)
                {
                    // Yustiel's Protection I
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("1304"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("1304");
                        return false;
                    }
                }
                else
                {
                    // Marchutan's Protection I
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("1305"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("1305");
                        return false;
                    }
                }
            }

            if (ExecuteSkillFromList(entity, dmg_Skill12).Item1 == false)
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

            uint healthRecharge = Game.Player.HealthMaximum - Game.Player.HealthCurrent;

            // Stamina Restoration
            if (healthRecharge > 2700)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stamina Restoration IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stamina Restoration IV", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stamina Restoration III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stamina Restoration III", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stamina Restoration II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stamina Restoration II", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stamina Restoration I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stamina Restoration I", Game.Player);
                    return false;
                }
            }

            if (CheckHeal(Game.Player) == false)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Perform the required pause checks.
        /// </summary>
        /// <returns></returns>
        public override bool Pause()
        {
            // MP recovery
            if (Game.Player.ManaCurrent < Game.Player.ManaMaximum - 1500)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Recovery IV"))
                {
                    if (Game.Player.ManaCurrent < Game.Player.ManaMaximum - 1830)
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Recovery IV", Game.Player);
                        return false;
                    }
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Recovery III"))
                {
                    if (Game.Player.ManaCurrent < Game.Player.ManaMaximum - 1720)
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Recovery III", Game.Player);
                        return false;
                    }
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Recovery II"))
                {
                    if (Game.Player.ManaCurrent < Game.Player.ManaMaximum - 1610)
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Recovery II", Game.Player);
                        return false;
                    }
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Recovery I"))
                {
                    if (Game.Player.ManaCurrent < Game.Player.ManaMaximum - 1500)
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Recovery I", Game.Player);
                        return false;
                    }
                }
            }

            // HP regen
            if (Game.Player.HealthCurrent < Game.Player.HealthMaximum && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Light of Rejuvenation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                    && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Revival", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival IV", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival III", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival II", Game.Player);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival I", Game.Player);
                    return false;
                }
            }

            // Buff ourself
            if (TargetBuff(Game.Player) == false)
            {
                return false;
            }

            // Promise of Wind
            if (Settings.UsePromiseOfWind && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Promise of Wind", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (ExecuteSkillFromList(null, new string[] { "2181", "989", "971", "970", "969" }).Item1 == false)
                {
                    return false;
                }
            }

            // Promise of Earth
            if (Settings.UsePromiseOfEarth && !Settings.UsePromiseOfWind && !Settings.UsePromiseOfAether && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Promise of Earth", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (ExecuteSkillFromList(null, new string[] { "1267", "1222", "1221", "1220" }).Item1 == false)
                {
                    return false;
                }
            }

            // Promise of Aether
            if (Settings.UsePromiseOfAether && !Settings.UsePromiseOfEarth && !Settings.UsePromiseOfWind && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Promise of Aether", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (ExecuteSkillFromList(null, new string[] { "2183", "1342", "1319", "1318" }).Item1 == false)
                {
                    return false;
                }
            }

            // Rage Spell
            if (Settings.UseRageSpell && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Rage Spell I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rage Spell I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rage Spell I");
                    return false;
                }
            }

			ActivateMantras(false);
            return true;
        }

        /// <summary>
        /// Perform the required force checks.
        /// </summary>
        /// <returns></returns>
        public override bool PartyCheck()
        {
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

            List<Entity> candidateMembers = new List<Entity>();
            int healingGroupBenefitCount = 0;

            // Loop tra i membri del gruppo e ordinali in base agli HP mancanti. Controlla se il membro del gruppo può essere avvantaggiato da una cura AoE
            foreach (var force in memberList)
            {
                if (force != null && force.IsDead == false && force.Position.DistanceToPosition(Game.Player.Position) < 20)
                {
                    candidateMembers.Add(force);

                    if ((force.HealthMaximum - force.HealthCurrent) > 2000)
                    {
                        healingGroupBenefitCount++;
                    }
                }

            }

            if (healingGroupBenefitCount > 1)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Life IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Life IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Life III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Life III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Life II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Life II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Life I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Life I");
                    return false;
                }
            }

            if (Settings.AllowPartyHealing)
            {
                // Order list by HP (less HP have priority)
                var hpOrder = candidateMembers.OrderBy(o => o.HealthPercentage).ToList();

                // Loop della list e inizia a curare i membri
                foreach (var item in hpOrder)
                {
                    if (CheckHeal(item) == false)
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


            return true;
        }

        /// <summary>
        /// Checks the healing requirements for the provided entity.
        /// </summary>
        /// <param name="entity">Contains the entity to perform healing on.</param>
        /// <returns></returns>
        private bool CheckHeal(Entity entity)
        {
            // Retrieve the rechargeable health.
            var entityMissingHp = entity.HealthMaximum - entity.HealthCurrent;

            // Recovery Spell
            if (entity.HealthPercentage < Settings.RecoverySpellLowHP && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Recovery Spell II"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Recovery Spell II", entity);
                return false;
            }
            else if (entity.HealthPercentage < Settings.RecoverySpellLowHP && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Recovery Spell I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Recovery Spell I", entity);
                return false;
            }

            // HP regen
            if (entity.HealthPercentage < 95 && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Light of Rejuvenation", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                    && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Word of Revival", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival IV", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival III", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Word of Revival I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Word of Revival I", entity);
                    return false;
                }
            }

            // Healing Burst
            if (entity.HealthPercentage < Settings.HealingBurstLowHP)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Burst II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Burst II", entity);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Burst I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Burst I", entity);
                    return false;
                }
            }

            // Healing Light
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
            // Wait delay
            if (Game.Time() < BuffCheckLastTimer + 1000)
                return true;


            // Buff Blessing of Health
            if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Health II", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Health II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Health II", entity);
                    BuffCheckLastTimer = Game.Time();
                    return false;
                }
                else if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Health I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Health I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Health I", entity);
                        BuffCheckLastTimer = Game.Time();
                        return false;
                    }
                }
            }

            // Blessing of Rock\Blessing of Stone
            if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Stone I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Stone I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Stone I", entity);
                    BuffCheckLastTimer = Game.Time();
                    return false;
                }
                else if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Rock I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false
                    && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Blessing of Stone I", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blessing of Rock I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blessing of Rock I", entity);
                        BuffCheckLastTimer = Game.Time();
                        return false;
                    }
                }
            }

            return true;
        }

        private bool IsMantra(ChanterMantraType mantraType)
        {
            if (Settings.MantraFirst == mantraType || Settings.MantraSecond == mantraType || Settings.MantraThird == mantraType)
            {
                return true;
            }

            return false;
        }

        private bool ActivateMantras(bool isAttack)
        {
            // Celerity Mantra
            if (!isAttack || IsMantra(ChanterMantraType.CelerityMantra))
            {
                var celerityMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Celerity Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (celerityMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Celerity Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Celerity Mantra I");
                        return false;
                    }
                }
            }

            // Clement Mind Mantra
            if (!isAttack || IsMantra(ChanterMantraType.ClementMindMantra))
            {
                var clementMindMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Clement Mind Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (clementMindMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Clement Mind Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Clement Mind Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Clement Mind Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Clement Mind Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Clement Mind Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Clement Mind Mantra I");
                        return false;
                    }
                }
            }

            // Hit Mantra
            if (IsMantra(ChanterMantraType.HitMantra))
            {
                var hitMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Hit Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (hitMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hit Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hit Mantra I");
                        return false;
                    }
                }
            }

            // Intensity Mantra
            if (IsMantra(ChanterMantraType.IntensityMantra))
            {
                var mantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Intensity Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (mantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Intensity Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Intensity Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Intensity Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Intensity Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Intensity Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Intensity Mantra I");
                        return false;
                    }
                }
            }

            // Invincibility Mantra
            if (IsMantra(ChanterMantraType.InvincibilityMantra))
            {
                var invincibilityMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Invincibility Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (invincibilityMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Invincibility Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Invincibility Mantra I");
                        return false;
                    }
                }
            }

            // Protection Mantra
            if (IsMantra(ChanterMantraType.ProtectionMantra))
            {
                var ProtectionMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Protection Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (ProtectionMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protection Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protection Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Protection Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Protection Mantra I");
                        return false;
                    }
                }
            }

            // Magic Mantra
            if (IsMantra(ChanterMantraType.MagicMantra))
            {
                var mantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Magic Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (mantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Mantra IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Mantra IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Magic Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Magic Mantra I");
                        return false;
                    }
                }
            }

            // Revival Mantra
            if (IsMantra(ChanterMantraType.RevivalMantra))
            {
                var mantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Revival Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (mantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Revival Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Revival Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Revival Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Revival Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Revival Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Revival Mantra I");
                        return false;
                    }
                }
            }

            // Shield Mantra
            if (IsMantra(ChanterMantraType.ShieldMantra))
            {
                var mantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Shield Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (mantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Mantra IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Mantra IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Mantra I");
                        return false;
                    }
                }
            }

            //Victory Mantra
            if (IsMantra(ChanterMantraType.VictoryMantra))
            {
                var victoryMantraState = Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Victory Mantra", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
                if (victoryMantraState == false)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Victory Mantra IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Victory Mantra IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Victory Mantra III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Victory Mantra III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Victory Mantra II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Victory Mantra II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Victory Mantra I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Victory Mantra I");
                        return false;
                    }
                }
            }

        }

        public override bool NoGravityAntiStuck()
        {


            return true;
        }
    }
}
