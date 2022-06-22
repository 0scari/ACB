using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class RangerSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowWeaving", "Allow Weaving")]
        public bool AllowWeaving { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_StopWeavingBelowHPPercentage", "Stop Weaving if HP below %")]
        public int StopWeavingBelowHPPercentage { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowSharpenArrows", "Allow Sharpen Arrows")]
        public bool AllowSharpenArrows { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowFocusedShot", "Allow Focused Shot")]
        public bool AllowFocusedShot { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowArrowFlurry", "Allow Arrow Flurry")]
        public bool AllowArrowFlurry { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowBestialFury", "Allow Bestial Fury")]
        public bool AllowBestialFury { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_SeizureArrowManaLow", "Seizure Arrow MP < %")]
        public int SeizureArrowManaLow { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_SeizureArrowHPLow", "Seizure Arrow HP < %")]
        public int SeizureArrowHPLow { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowMauForm", "Allow Mau form")]
        public bool AllowMauForm { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowAoESkills", "Allow AoE Skills")]
        public bool AllowAoeSkills { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowDodging", "Allow Dodging")]
        public bool AllowDodging { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Ranger_AllowAiming", "Allow Aiming")]
        public bool AllowAiming { get; set; }

        public RangerSettings() : base()
        {
            TargetSearchDistance = 25;


            AllowWeaving = false;
            StopWeavingBelowHPPercentage = 20;
            AllowSharpenArrows = true;
            AllowFocusedShot = true;
            AllowArrowFlurry = true;
            AllowBestialFury = true;
            SeizureArrowManaLow = 80;
            SeizureArrowHPLow = 80;
            AllowMauForm = true;
            AllowAoeSkills = true;
            AllowDodging = false;
            AllowAiming = false;
        }

        public RangerSettings(RangerSettings classSettings) : base(classSettings)
        {
            AllowWeaving = classSettings.AllowWeaving;
            StopWeavingBelowHPPercentage = classSettings.StopWeavingBelowHPPercentage;
            AllowSharpenArrows = classSettings.AllowSharpenArrows;
            AllowFocusedShot = classSettings.AllowFocusedShot;
            AllowArrowFlurry = classSettings.AllowArrowFlurry;
            AllowBestialFury = classSettings.AllowBestialFury;
            SeizureArrowManaLow = classSettings.SeizureArrowManaLow;
            SeizureArrowHPLow = classSettings.SeizureArrowHPLow;
            AllowMauForm = classSettings.AllowMauForm;
            AllowAoeSkills = classSettings.AllowAoeSkills;
            AllowDodging = classSettings.AllowDodging;
            AllowAiming = classSettings.AllowAiming;
        }
    }

    public class Ranger : AionClassBase
    {
        private DateTime BreathOfNatureBuffTimer { get; set; }
        private ulong SharpenArrowBuffTimer { get; set; }

        private RangerSettings Settings { get; set; }


        public Ranger() : this(new RangerSettings())
        {

            BreathOfNatureBuffTimer = DateTime.MinValue;
            SharpenArrowBuffTimer = 0;
        }
        public Ranger(RangerSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Ranger;
        }


        private static string[] dmg_Skill19 = new string[]
        {
            "Stunning Shot III",
            "Stunning Shot II",
            "Stunning Shot I",

            "Gale Arrow IV",
            "Gale Arrow III",
            "Gale Arrow II",
            "Gale Arrow I",

            "Ancestral Darkwing Arrow I", // Asmo
            "Darkwing Arrow I",
            "Ancestral Brightwing Arrow I", // Elyos
            "Brightwing Arrow I",

            "Heart Shot II",
            "Heart Shot I",

            "Swift Shot IV",
            "Swift Shot III",
            "Swift Shot II",
            "Swift Shot I",

            "Shackle Arrow I",
            "Shock Arrow I",

            "Explosive Arrow II",
            "Explosive Arrow I",

            "Deadshot IV",
            "Deadshot III",
            "Deadshot II",
            "Deadshot I",
        };


        private static string[] chain_skills19 = new string[]
        {
            // Stunning shot chain
            "Rupture Arrow III",
            "Rupture Arrow II",
            "Rupture Arrow I",

            // Swift shot chain
            "Spiral Arrow II",
            "Spiral Arrow I",

            "Poison Arrow III",
            "Poison Arrow II",
            "Poison Arrow I",

            "Arrow Strike III",
            "Arrow Strike II",
            "Arrow Strike I",

            "Fleshcutter Arrow I",

            // Aether hold chain
            //"Aerial Wild Shot I",
        };


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

            var entityDistance = Game.Player.Position.Distance(entity.Position);

            // Dagger + Sword skill
            if (IsMeleeWeaponEquipped())
            {
                if (entityDistance < 6)
                {
                    // Devotion
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Devotion I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Devotion I");
                        return false;
                    }
                }

                // 
                if (HelperFunction.CheckAvailable("Focused Evasion I") && Game.Player.HealthPercentage < 50)
                {
                    HelperFunction.CheckExecute("Focused Evasion I");
                    return false;
                }

                // 
                if (HelperFunction.CheckAvailable("Soul Slash I"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Soul Slash I");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Swift Edge II"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Swift Edge II");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Swift Edge I"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Swift Edge I");
                    return false;
                }

                // 
                if (HelperFunction.CheckAvailable("Counterattack II"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Counterattack II");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Counterattack I"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Counterattack I");
                    return false;
                }

                // 
                if (HelperFunction.CheckAvailable("Surprise Attack II"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Surprise Attack II");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Surprise Attack I"))
                {
                    WaitAutoAttack = true;
                    HelperFunction.CheckExecute("Surprise Attack I");
                    return false;
                }

                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(Game.AttackChatCommand);

                return true;
            }

            if ((Game.Player.HealthPercentage < 50 || Game.Player.ManaPercentage < 50) && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Seizure Arrow I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Seizure Arrow I");
                return false;
            }


            if (WaitAutoAttack && Settings.AllowWeaving && Game.Player.HealthPercentage >= Settings.StopWeavingBelowHPPercentage)
            {
                Game.Player.UpdateAutoAttackData();
                //     Game.WriteMessage(Game.Player.AttackStatus);
                if (Game.Player.IsAutoAttacking && Game.Player.CurrentAnimationTime < (Game.Player.MaxAnimationTimer * 0.9))
                {
                    WaitAutoAttack = false;
                }
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(Game.AttackChatCommand);
                return false;
            }

            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills19);
            if (chain_SkillsResult.Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }


            // Buff recovery mana per attack - 1 minut
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Keen Cleverness I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Keen Cleverness I");
                return false;
            }

            // Buff +20% damage per 1 minut
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Bow of Blessing I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Bow of Blessing I");
                return false;
            }

            // Buff +5% damage per 1 minut
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Strong Shots I") && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Strong Shots", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Strong Shots I");
                return false;
            }

            // Buff Bestial Fury I
            if (Settings.AllowBestialFury && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Bestial Fury I") && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Bestial Fury", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Bestial Fury I");
                return false;
            }

            if (entity.TargetId == Game.Player.Id && entityDistance <= 10 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Focused Evasion I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Focused Evasion I");
                return false;
            }

            // Buff +40 damage per 5seconds
            if (entityDistance <= 20 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Devotion I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Devotion I");
                return false;
            }

            // Buff Speed of the Wind I
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Speed of the Wind I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Speed of the Wind I");
                return false;
            }

            // Buff Arrow Flurry I
            if (Settings.AllowArrowFlurry && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Arrow Flurry I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Arrow Flurry I");
                return false;
            }

            // Focused Shots I
            if (Settings.AllowFocusedShot && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Focused Shots I") && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Focused Shots", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Focused Shots I");
                return false;
            }

            // Mau form
            if (Settings.AllowMauForm && Game.Player.DP >= 2000 && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Mau Form", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Mau Form IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Mau Form IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Mau Form III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Mau Form III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Mau Form II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Mau Form II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Mau Form I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Mau Form I");
                    return false;
                }
            }
            if (entity.HealthPercentage >= 40 && entity.HealthPercentage < 80)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow VI"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow VI");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow V"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow IV"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Arrow I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Arrow I");
                    return false;
                }
            }

            // Buff +200 Evasion
            if (Settings.AllowDodging && !Settings.AllowAiming && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Dodging I") && Game.Player.StateList.GetState("Dodging I") == null)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Dodging I");
                return false;
            }

            // Buff +200 Accuracy
            if (Settings.AllowAiming && !Settings.AllowDodging && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Aiming I") && Game.Player.StateList.GetState("Aiming I") == null)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Aiming I");
                return false;
            }

            if (entityDistance > 10)
            {
                if (CheckAndExecuteSkills(entity, "Entangling Shot", 4))
                {
                    WaitAutoAttack = true;
                    return false;
                }
            }

            if (Settings.AllowAoeSkills)
            {
                if (CheckAndExecuteSkills(entity, "Arrow Storm", 1))
                {
                    WaitAutoAttack = true;
                    return false;
                }
            }

            if (Settings.AllowAoeSkills && entityDistance <= 15)
            {
                if (CheckAndExecuteSkills(entity, "Arrow Deluge", 7))
                {
                    WaitAutoAttack = true;
                    return false;
                }
            }

            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_Skill19).Item1 == false)
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
            // Buff hp/mp manage regen
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Breath of Nature III"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Breath of Nature III");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Breath of Nature II"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Breath of Nature II");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Breath of Nature I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Breath of Nature I");
                return false;
            }


            return true;
        }

        public bool IsMeleeWeaponEquipped()
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
