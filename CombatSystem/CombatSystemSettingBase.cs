using AionBotnet.AionGame.Enums;
using AionBotnet.AionGame.UnknownFramework.AionClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem
{
    /// <summary>
    /// Class to store all general settings
    /// </summary>
    public abstract class CombatSystemSettingBase
    {
        /*     // Indicates whether or not potions are allowed(does not use Serums, only Elixir and Potions).
             public bool Potion { get ;  set ; }

             // Contains the remaining amount of health required before potions are used(0 = disabled).
             public int PotionHealth { get; set; }

             // Contains the remaining amount of mana required before potions are used(0 = disabled).
             public int PotionMana { get; set; }

             // Contains the remaining amount of health and mana required before potions are used(0 = disabled).
             public int PotionRecovery { get; set; }

             // Contains the remaining amount of health required before resting(Requires AllowRest).
             public int RestHealth { get; set; }

             // Contains the remaining amount of mana required before resting(Requires AllowRest).
             public int RestMana { get; set; }

             // Contains the remaining amount of flight time required before resting(Requires AllowRest).
             public int RestFlight { get; set; }

             // Indicates whether or not the mana treatment skill is allowed to be used(Requires AllowAttack).
             public bool ManaTreatment { get; set; }

             // Contains the treshold before mana treatment is used(In percentages, or 0 to recharge an exact amount).
             public int ManaTreatmentTreshhold { get; set; }

             // Contains the maximum distance of the area in which to search for targets(Requires AllowTargetSearch).
             public int TargetSearchDistance { get; set; }
        */

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "GeneralSettingsCategoryName", "General Settings")] // [Category("General Settings")]
        [DisplayNameDynamic(ScriptDirectory, "GeneralSettings_AggressiveTargetSearch", "Search target aggressively")]
        public bool AggressiveTargetSearch { get; set; }

        /// <summary>
        /// Indicates whether or not to allow looting of targets
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "GeneralSettingsCategoryName", "General Settings")] // [Category("General Settings")]
        [DisplayNameDynamic(ScriptDirectory, "GeneralSettings_AllowLoot", "Allow loot")]
        public bool AllowLoot { get; set; }

        /// <summary>
        /// Indicates entity to exclude from aggressive search
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "GeneralSettingsCategoryName", "General Settings")] // [Category("General Settings")]
        [DisplayNameDynamic(ScriptDirectory, "GeneralSettings_IgnoreEnitityName", "Entity to ignore")]
        [Description("List name of entity to ignore")]
        public string IgnoreEnitityName { get; set; }

        /// <summary>
        /// Indicates entity to totally ignore
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "GeneralSettingsCategoryName", "General Settings")] // [Category("General Settings")]
        [DisplayNameDynamic(ScriptDirectory, "GeneralSettings_TotallyIgnoreEnitityName", "Entity to totally ignore")]
        [Description("List name of entity to totally ignore")]
        public string TotallyIgnoreEnitityName { get; set; }

        private const string ScriptDirectory = "Script\\Source\\Include\\AionClassic\\Include\\CombatSystem";

        public CombatSystemSettingBase()
        {
            
            /*    Potion = true;
                PotionHealth = 20;
                PotionMana = 20;
                PotionRecovery = 40;
                RestHealth = 50;
                RestMana = 40;
                RestFlight = 25;
                ManaTreatment = true;
                ManaTreatmentTreshhold = 70;
                TargetSearchDistance = 15;*/
            AggressiveTargetSearch = true;
            AllowLoot = true;

            TotallyIgnoreEnitityName = "";
            IgnoreEnitityName = "";
        }

        public CombatSystemSettingBase(CombatSystemSettingBase combatSystemSettingBase)
        {
            AggressiveTargetSearch = combatSystemSettingBase.AggressiveTargetSearch;
            AllowLoot = combatSystemSettingBase.AllowLoot;
            TotallyIgnoreEnitityName = combatSystemSettingBase.TotallyIgnoreEnitityName;
            IgnoreEnitityName = combatSystemSettingBase.IgnoreEnitityName;
        }

        public abstract CombatSystem.AionClass.AionClassBaseSetting GetClassSetting();
    }
}
