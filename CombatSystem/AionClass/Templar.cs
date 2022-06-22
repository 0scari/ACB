using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class TemplarSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowWeaving", "Allow Weaving")]
        public bool AllowWeaving { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_StopWeavingBelowHPPercentage", "Stop Weaving if HP below %")]
        public int StopWeavingBelowHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Templar_UseEmpyreanArmorBelowHPPercentage", "Use Empyrean Armor if HP below %")]
        public int UseEmpyreanArmorBelowHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Templar_TankMode", "Use All Taunts")]
        public bool TankMode { get; set; }

        public TemplarSettings() : base()
        {
            TargetSearchDistance = 10;
            AllowWeaving = false;
            StopWeavingBelowHPPercentage = 20;
            UseEmpyreanArmorBelowHPPercentage = 75;
            TankMode = true;
        }

        public TemplarSettings(TemplarSettings classSettings) : base(classSettings)
        {
            AllowWeaving = classSettings.AllowWeaving;
            StopWeavingBelowHPPercentage = classSettings.StopWeavingBelowHPPercentage;
            UseEmpyreanArmorBelowHPPercentage = classSettings.UseEmpyreanArmorBelowHPPercentage;
            TankMode = classSettings.TankMode;
        }
    }

    public class Templar : AionClassBase
    {
        private TemplarSettings Settings { get; set; }
        private uint AttackStartedID { get; set; }
        private ulong _tauntLastTimeUsed;

        public Templar() : this(new TemplarSettings())
        {
        }

        public Templar(TemplarSettings settings)
        {
            Settings = settings;
            CurrentAionClass = eAionClass.Templar;
            AttackStartedID = 0;
            _tauntLastTimeUsed = 0;
        }




        private static string[] dmg_Skill = new string[]
        {
            // 10s CD
            "177", // "Ferocious Strike V",
            "172", // "Ferocious Strike IV",
            "171", // "Ferocious Strike III",
            "170", // "Ferocious Strike II",
            "169", // "Ferocious Strike I",

            "Dazing Severe Blow III",
            "Dazing Severe Blow II",
            "Dazing Severe Blow I",

            "Blunting Severe Blow II",
            "Blunting Severe Blow I",

            "Weakening Severe Blow V",
            "Weakening Severe Blow IV",
            "Weakening Severe Blow III",
            "Weakening Severe Blow II",
            "Weakening Severe Blow I",

            "Provoking Severe Blow IV",
            "Provoking Severe Blow III",
            "Provoking Severe Blow II",
            "Provoking Severe Blow I",

            "Shining Slash IV",
            "Shining Slash III",
            "Shining Slash II",
            "Shining Slash I",
        };


        private static string[] chain_skills = new string[]
        {
            // Stumble target
            "Break Power IV",
            "Break Power III",
            "Break Power II",
            "Break Power I",

            "Pitiless Blow I",


            // Block
            "Face Smash III",
            "Face Smash II",
            "Face Smash I",

            

            // 
            "Judgment II",
            "Judgment I",

            "Divine Blow III",
            "Divine Blow II",
            "Divine Blow I",

            // Ferocious Strike chain
            "Magic Smash IV",
            "Magic Smash III",
            "Magic Smash II",
            "Magic Smash I",

            "Wrath Strike IV",
            "Wrath Strike III",
            "Wrath Strike II",
            "Wrath Strike I",

            "Robust Blow V",
            "Robust Blow IV",
            "Robust Blow III",
            "Robust Blow II",
            "Robust Blow I",

            "Slash Artery II",
            "Slash Artery I",
            //
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

            if (Game.Player.HealthPercentage < 80 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Refresh Spirit I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Refresh Spirit I");
                return false;
            }
            else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ensnaring Blow I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ensnaring Blow I");
                return false;
            }

            // Recovery HP
            if (Game.Player.HealthPercentage < 80)
            {
                // Asmmodian
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Righteous Punishment I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Righteous Punishment I");
                    return false;
                }
                // Elyos
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Holy Punishment I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Holy Punishment I");
                    return false;
                }


                // Asmmodian
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ancestral Righteous Punishment I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ancestral Righteous Punishment I");
                    return false;
                }
                // Elyos
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ancestral Holy Punishment I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ancestral Holy Punishment I");
                    return false;
                }

                // Asmmodian
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment of Darkness I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment of Darkness I");
                    return false;
                }
                // Elyos
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment of Light I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment of Light I");
                    return false;
                }
            }

            // Recovery HP
            if (Game.Player.HealthPercentage < Settings.UseEmpyreanArmorBelowHPPercentage && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Empyrean Armor I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Empyrean Armor I");
                return false;
            }

            // Def buff
            if (Game.Player.HealthPercentage < 40 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Iron Skin I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Iron Skin I");
                return false;
            }

            bool isStunned = entity.StateList.GetList().Where(s => s.Value.IsStun).Any();
            var entityDistance = Game.Player.Position.Distance(entity.Position);
            bool isGreatswordEquipped = IsGreatswordEquipped();
            bool isShieldEquipped = IsShieldEquipped();

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


            if (isShieldEquipped)
            {
                if (!isStunned)
                {
                    // 
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Counter IV"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Counter IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Counter III"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Counter III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Counter II"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Counter II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Counter I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Counter I");
                        return false;
                    }

                    // Shieldburst I - Great stigms
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shieldburst I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shieldburst I");
                        return false;
                    }

                    // Provoking Shield Counter
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter IV"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter III"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter II"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter I");
                        return false;
                    }
                }

                // Avenging Blow
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Avenging Blow III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Avenging Blow III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Avenging Blow II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Avenging Blow II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Avenging Blow I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Avenging Blow I");
                    return false;
                }

                // Provoking Shield Counter I-IV
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter IV"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Provoking Shield Counter I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Provoking Shield Counter I");
                    return false;
                }
            }

            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            // If Taunting Enabled
            if (Settings.TankMode && entity.TargetId != 0 && entity.TargetId != Game.Player.Id && Game.Time() > _tauntLastTimeUsed + 1000)
            {
                // Taunt
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(176))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(176);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(160))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(160);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(158))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(158);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(158))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(158);
                    return false;
                }

                // Cry Of Ridicule
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2270))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2270);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2269))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2269);
                    return false;
                }

                // Provoking Roar
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(541))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(541);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(530))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(530);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(451))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(451);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(432))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(432);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(431))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(431);
                    return false;
                }

                // Incite Rage
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(408))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(408);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(407))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(407);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(406))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(406);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(402))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(402);
                    return false;
                }
            }

            // Buff damage - 30sec - 1.30m CD
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Empyrean Fury I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Empyrean Fury I");
                return false;
            }

            // Buff damage - 30sec - 3m CD
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Divine Fury I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Divine Fury I");
                return false;
            }

            if (!isStunned)
            {
                if (isShieldEquipped)
                {
                    // 
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Bash IV"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Bash IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Bash III"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Bash III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Bash II"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Bash II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Shield Bash I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Shield Bash I");
                        return false;
                    }
                }

                if (entityDistance < 6)
                {
                    // Divine Justice - Great stigna
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Divine Justice IV"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Divine Justice IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Divine Justice III"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Divine Justice III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Divine Justice II"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Divine Justice II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Divine Justice I"))
                    {
                        WaitAutoAttack = true;
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Divine Justice I");
                        return false;
                    }
                }
            }

            // Doom Lure
            if (entityDistance >= 5 && entityDistance < 17 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Doom Lure I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Doom Lure I");
                return false;
            }


            // Greatswork skill
            if (isGreatswordEquipped)
            {
                // Punishment
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment V"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment IV"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Punishment I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Punishment I");
                    return false;
                }
            }

            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_Skill).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            // Initial Attack 1: Automatic Attack
            if (true)
            {
                HelperFunction.CheckExecute(Game.AttackChatCommand);
            }

            return true;
        }

        public override bool PartyCheck()
        {
            // Provoking Roar - Taunt skill
            if (Game.PartyMemberList.PartyMemberCount > 1)
            {
                var ourGroupPlayerId = Game.PartyMemberList.GetList()
                    .Where(w => w.Value.Id != Game.Player.Id) // Exclude our ID
                    .Select(s => s.Value.Id);

                // If  monster have in target a group member
                if (Game.EntityList.GetList().Where(w => ourGroupPlayerId.Contains(w.Value.TargetId) && w.Value.Position.DistanceToPosition(Game.Player.Position) < 10).Any())
                {
                    if (CheckAndExecuteSkills(null, "Provoking Roar", 6))
                    {
                        return false;
                    }

                    // Cry of Ridicule
                    if (ExecuteSkillFromList(null, new string[] { "2269", "2270" }).Item1 == false)
                    {
                        return false;
                    }
                }
            }


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

            // Recovery HP
            if (Game.Player.HealthPercentage < 50)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Prayer of Resilience IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Prayer of Resilience IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Prayer of Resilience III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Prayer of Resilience III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Prayer of Resilience II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Prayer of Resilience II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Prayer of Resilience I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Prayer of Resilience I");
                    return false;
                }

            }

            return true;
        }

        public bool IsGreatswordEquipped()
        {
            bool result = Game.InventoryList.GetList()
                .Where(i => i.ItemType == AionGame.Enums.eInventoryType.Weapon2H && i.SlotType == AionGame.Enums.eInventorySlotType.MainHand2H_Equipped)
                .Any();

            return result;
        }

        public bool IsShieldEquipped()
        {
            bool result = Game.InventoryList.GetList()
                .Where(i => i.ItemType == AionGame.Enums.eInventoryType.Shield && i.SlotType == AionGame.Enums.eInventorySlotType.OffHand_Equipped)
                .Any();

            return result;
        }
    }
}
