using AionBotnet.AionGame.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    /// <summary>
    /// Class to store all general settings
    /// </summary>
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class AionClassBaseSetting
    {
        // Contains the remaining amount of health before potions are used(0 = disabled).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_LifePotionBelowPercentage", "Use HP Life Potion below %")]
        public int LifePotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_LifeSerumPotionBelowPercentage", "Use HP Serum Potion below %")]
        public int LifeSerumPotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_LifeElixirPotionBelowPercentage", "Use HP Elixir below %")]
        public int LifeElixirPotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_LifePanaceaPotionBelowPercentage", "Use HP Panacea below %")]
        public int LifePanaceaPotionBelowPercentage { get; set; }



        // Contains the remaining amount of health and mana required before potions are used(0 = disabled).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_ManaPotionBelowPercentage", "Use MP potion below %")]
        public int ManaPotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_ManaSerumPotionBelowPercentage", "Use MP Serum Potion below %")]
        public int ManaSerumPotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_ManaElixirPotionBelowPercentage", "Use MP Elixir below %")]
        public int ManaElixirPotionBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_ManaPanaceaPotionBelowPercentage", "Use MP Panacea below %")]
        public int ManaPanaceaPotionBelowPercentage { get; set; }



        // Contains the remaining amount of health and mana required before potions are used(0 = disabled).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_PotionRecoveryBelowPercentage", "Use Recovery Potion below %")]
        public int PotionRecoveryBelowPercentage { get; set; }


        // Contains the remaining amount of health and mana required before potions are used(0 = disabled).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_PotionSerumBelowPercentage", "Use Recovery Serum below %")]
        public int PotionSerumBelowPercentage { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_ManaTreatmentBelowPercentage", "Use Treatment MP below %")]
        public int ManaTreatmentBelowPercentage { get; set; }
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_HerbTreatmentBelowPercentage", "Use Treatment HP below %")]
        public int HerbTreatmentBelowPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseWindSerum", "Use Wind Serum")]
        public bool UseWindSerum { get;  set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseRallySerum", "Use Rally Serum")]
        public bool UseRallySerum { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseFocusAgent", "Use Focus Agent")]
        public bool UseFocusAgent { get; set; }


        //---------------------------------------------------------------------------------------
        //                           SCROLL
        //---------------------------------------------------------------------------------------
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseScrollAttackSpeed", "Use Scroll Attack Speed")]
        public bool UseScrollAttackSpeed { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseScrollCastingSpeed", "Use Scroll Casting Speed")]
        public bool UseScrollCastingSpeed { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseScrollMovementSpeed", "Use Scroll Movement Speed")]
        public bool UseScrollMovementSpeed { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseScrollCriticalStrike", "Use Scroll Critical Strike")]
        public bool UseScrollCriticalStrike { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_UseScrollCriticalSpell", "Use Scroll Critical Spell")]
        public bool UseScrollCriticalSpell { get; set; }

        //---------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------

        // Contains the remaining amount of health required before resting(Requires AllowRest).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_RestHealth", "Resting if healt is below %")]
        public int RestHealth { get; set; }

        // Contains the remaining amount of mana required before resting(Requires AllowRest).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_RestMana", "Resting if mana is below %")]
        public int RestMana { get; set; }

        // Contains the remaining amount of flight time required before resting(Requires AllowRest).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_RestFlight", "Resting if flight time is below %")]
        public int RestFlight { get; set; }

        // Contains the maximum distance of the area in which to search for targets(Requires AllowTargetSearch).
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_TargetSearchDistance", "Target Search Distance")]
        public int TargetSearchDistance { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowRemoveShock", "Allow Remove Shock")]
        public bool AllowRemoveShock { get; set; }



        // ------------------------- Custom Food ----------------------------------------
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_CustomFoodSetting", "Custom food settings")]
        public string CustomFoodSetting { get; set; }


        protected const string ScriptDirectory = "Script\\Source\\Include\\AionClassic\\Include\\CombatSystem";

        public AionClassBaseSetting()
        {
            PotionRecoveryBelowPercentage = 40;
            PotionSerumBelowPercentage = 30;
            UseWindSerum = false;
            RestHealth = 50;
            RestMana = 40;
            RestFlight = 25;
            TargetSearchDistance = 15;
            AllowRemoveShock = true;

            UseScrollAttackSpeed = false;
            UseScrollCastingSpeed = false;
            UseScrollMovementSpeed = false;
            UseScrollCriticalStrike = false;
            UseScrollCriticalSpell = false;

            CustomFoodSetting = "";
        }

        public AionClassBaseSetting(AionClassBaseSetting aionClassBaseSetting)
        {
            LifePotionBelowPercentage = aionClassBaseSetting.LifePotionBelowPercentage;
            LifeSerumPotionBelowPercentage = aionClassBaseSetting.LifeSerumPotionBelowPercentage;
            LifeElixirPotionBelowPercentage = aionClassBaseSetting.LifeElixirPotionBelowPercentage;
            LifePanaceaPotionBelowPercentage = aionClassBaseSetting.LifePanaceaPotionBelowPercentage;


            ManaPotionBelowPercentage = aionClassBaseSetting.ManaPotionBelowPercentage;
            ManaSerumPotionBelowPercentage = aionClassBaseSetting.ManaSerumPotionBelowPercentage;
            ManaElixirPotionBelowPercentage = aionClassBaseSetting.ManaElixirPotionBelowPercentage;
            ManaPanaceaPotionBelowPercentage = aionClassBaseSetting.ManaPanaceaPotionBelowPercentage;


            PotionRecoveryBelowPercentage = aionClassBaseSetting.PotionRecoveryBelowPercentage;
            PotionSerumBelowPercentage = aionClassBaseSetting.PotionSerumBelowPercentage;

            ManaTreatmentBelowPercentage = aionClassBaseSetting.ManaTreatmentBelowPercentage;
            HerbTreatmentBelowPercentage = aionClassBaseSetting.HerbTreatmentBelowPercentage;

            UseWindSerum = aionClassBaseSetting.UseWindSerum;
            UseRallySerum = aionClassBaseSetting.UseRallySerum;
            UseFocusAgent = aionClassBaseSetting.UseFocusAgent;

            UseScrollAttackSpeed = aionClassBaseSetting.UseScrollAttackSpeed;
            UseScrollCastingSpeed = aionClassBaseSetting.UseScrollCastingSpeed;
            UseScrollMovementSpeed = aionClassBaseSetting.UseScrollMovementSpeed;
            UseScrollCriticalStrike = aionClassBaseSetting.UseScrollCriticalStrike;
            UseScrollCriticalSpell = aionClassBaseSetting.UseScrollCriticalSpell;

            RestHealth = aionClassBaseSetting.RestHealth;
            RestMana = aionClassBaseSetting.RestMana;
            RestFlight = aionClassBaseSetting.RestFlight;
            TargetSearchDistance = aionClassBaseSetting.TargetSearchDistance;
            AllowRemoveShock = aionClassBaseSetting.AllowRemoveShock;

            CustomFoodSetting = aionClassBaseSetting.CustomFoodSetting;
        }

        public override string ToString()
        {
            return "";
        }
    }
}
