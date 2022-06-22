using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System;
using System.ComponentModel;
using System.Linq;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class AssassinSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowWeaving", "Allow Weaving")]
        public bool AllowWeaving { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_StopWeavingBelowHPPercentage", "Stop Weaving if HP below %")]
        public int StopWeavingBelowHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Assassin_UseDivineStrikeBelowHPPercentage", "Use Divine Strike if HP below %")]
        public int UseDivineStrikeBelowHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Assassin_AllowAmbush", "Allow Ambush")]
        public bool AllowAmbush { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Assassin_ForceAttackFromBehind", "Force Attack From Behind")]
        public bool ForceAttackFromBehind { get; set; }


        public AssassinSettings() : base()
        {
            TargetSearchDistance = 10;
            AllowWeaving = false;
            StopWeavingBelowHPPercentage = 20;
            UseDivineStrikeBelowHPPercentage = 20;
            AllowAmbush = true;
            ForceAttackFromBehind = false;
        }

        public AssassinSettings(AssassinSettings classSettings) : base(classSettings)
        {
            AllowWeaving = classSettings.AllowWeaving;
            UseDivineStrikeBelowHPPercentage = classSettings.UseDivineStrikeBelowHPPercentage;
            StopWeavingBelowHPPercentage = classSettings.StopWeavingBelowHPPercentage;
            AllowAmbush = classSettings.AllowAmbush;
            ForceAttackFromBehind = classSettings.ForceAttackFromBehind;
        }
    }

    public class Assassin : AionClassBase
    {
        private uint AttackStartedID { get; set; }

        /// <summary>
        /// Ultima volta che è stata eseguita una skill per applicare le rune
        /// </summary>
        private DateTime LastRuneSkillTimer { get; set; }

        /// <summary>
        /// Last time we try to use Ambush skill. To avoid to be stucked when can't cast Ambush cause obstacle
        /// </summary>
        private DateTime LastAmbushUseSkillTimer { get; set; }


        private AssassinSettings Settings { get; set; }


        public Assassin() : this(new AssassinSettings())
        {
            LastRuneSkillTimer = DateTime.MinValue;
            LastAmbushUseSkillTimer = DateTime.MinValue;
        }

        public Assassin(AssassinSettings settings)
        {
            this.Settings = settings;
            LastRuneSkillTimer = DateTime.MinValue;
            CurrentAionClass = eAionClass.Assassin;
            AttackStartedID = 0;
        }


        private static string[] chain_skills12 = new string[] {

            // Rune carve chain
            "Sigil Strike VII",
            "Sigil Strike VI",
            "Sigil Strike V",
            "Sigil Strike IV",
            "Sigil Strike III",
            "Sigil Strike II",
            "Sigil Strike I",

            //
            "Beastly Scar IV",
            "Beastly Scar III",
            "Beastly Scar II",
            "Beastly Scar I",

            "Beast Swipe II",
            "Beast Swipe I",

            "Beast Kick III",
            "Beast Kick II",
            "Beast Kick I",

            // Remove Shock chain
            "Cyclone Slash I",
            "Bursting Flame Strike I",

            // Aether hold
            "Crashing Wind Strike II",
            "Crashing Wind Strike I",

         //   "Throw Shuriken II", // Fly only
       //     "Throw Shuriken I",

            // Swift Edge chain
            "Soul Slash V",
            "Soul Slash IV",

            "Soul Slash III",
            "Soul Slash II",
            "Soul Slash I",

            "Rune Slash III",
            "Rune Slash II",
            "Rune Slash I",

            // 
            "Agony Rune II",
            "Agony Rune I",

            "Agonizing Slash IV",
            "Agonizing Slash III",
            "Agonizing Slash II",
            "Agonizing Slash I",

            "Lightning Slash IV",
            "Lightning Slash III",
            "Lightning Slash II",
            "Lightning Slash I",
        };

        private static string[] dmg_skills12 = new string[] {

            "867", // "Rune Carve IV",
            "809", // "Rune Carve III",
            "808", // "Rune Carve II",
            "807", // "Rune Carve I",

            "Fang Strike IV",
            "Fang Strike III",
            "Fang Strike II",
            "Fang Strike I",

            "Swift Edge V",
            "Swift Edge IV",
            "Swift Edge III",
            "Swift Edge II",
            "Swift Edge I",

            "Dash and Slash I",
            "Dash and Slash II",
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


            // Is target stunned
            bool isStunned = entity.StateList != null ? entity.StateList.GetList().Where(s => s.Value.IsStun).Any() : false; ;
            bool isSpinState = entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Spin", StringComparison.InvariantCultureIgnoreCase) >= 0).Any();
            var entityDistance = Game.Player.Position.Distance(entity.Position);

            if (isStunned)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Assassination III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Assassination III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Assassination II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Assassination II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Assassination I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Assassination I");
                    return false;
                }
            }
            else if (!isSpinState && !isSpinState)
            {
                // Evasive Boost I - Buff
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Evasive Boost I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Evasive Boost I");
                    return false;
                }

                // Counterattack
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(581))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(581);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(558))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(558);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(557))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(557);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(556))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(556);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(555))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(555);
                    return false;
                }

                // Whirlwind Slash (put in Spin state)
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Whirlwind Slash III"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Whirlwind Slash III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Whirlwind Slash II"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Whirlwind Slash II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Whirlwind Slash I"))
                {
                    WaitAutoAttack = true;
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Whirlwind Slash I");
                    return false;
                }
            }


            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills12);
            if (chain_SkillsResult.Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            if (entity.TargetId == Game.Player.Id && entityDistance <= 10 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Focused Evasion I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Focused Evasion I");
                return false;
            }

            // Aethertwisting
            if (entity.TargetId == Game.Player.Id && entityDistance <= 10 && entity.CurrentSkillId != 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(920)
                && entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Focused Evasion", StringComparison.InvariantCultureIgnoreCase) >= 0).Any()==false)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(920);
                return false;
            }

            // Divine Strike (Healing skill)
            if (Game.Player.IsElyos && Game.Player.DP >= 2000 && Game.Player.HealthPercentage <= Settings.UseDivineStrikeBelowHPPercentage)
            {
                // Divine Strike IV
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("930"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("930");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("910"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("910");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("909"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("909");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("908"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("908");
                    return false;
                }
            }

            // Strike of Darkness (Healing skill)
            if (Game.Player.IsAsmodian && Game.Player.DP >= 2000 && entity.HealthPercentage > 20)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("931"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("931");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("907"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("907");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("906"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("906");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("905"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("905");
                    return false;
                }
            }

            // Buff
            // Apply Poison - "Scolopen Poison" item type id 169300010
            if (CheckScolopenPoison(4) && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Apply Poison", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Apply Poison IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Apply Poison IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Apply Poison III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Apply Poison III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Apply Poison II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Apply Poison II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Apply Poison I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Apply Poison I");
                    return false;
                }
            }

            // Apply Deadly Poison I
            if (CheckScolopenPoison(2) && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Apply Deadly Poison", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Apply Deadly Poison I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Apply Deadly Poison I");
                return false;
            }

            if (entityDistance < 6)
            {
                // Deadly Abandon
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Deadly Abandon I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Deadly Abandon I");
                    return false;
                }

                // Flurry
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Flurry I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Flurry I");
                    return false;
                }

                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Clear Focus I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Clear Focus I");
                    return false;
                }

                // Killer's Eye I
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Killer's Eye I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Killer's Eye I");
                    return false;
                }

                // Devotion
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Devotion I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Devotion I");
                    return false;
                }
            }

            // Explode Runes
            if (CheckRunes(entity) >= 5)
            {
                // Healing skill
                if (Game.Player.HealthPercentage < 60)
                {
                    // Rune Swipe
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Swipe I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Swipe I");
                        return false;
                    }

                    // Blood Rune
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blood Rune II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blood Rune II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blood Rune I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blood Rune I");
                        return false;
                    }

                    // Elyos and Asmo
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ancestral Radiant Rune I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ancestral Radiant Rune I");
                        return false;
                    }
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ancestral Darkness Rune I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ancestral Darkness Rune I");
                        return false;
                    }

                }

                // Stun effect skill
                if (isStunned == false)
                {
                    // Pain Rune
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Pain Rune IV"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Pain Rune IV");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Pain Rune III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Pain Rune III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Pain Rune II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Pain Rune II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Pain Rune I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Pain Rune I");
                        return false;
                    }

                    // Binding Rune - Put enemy in aether hold state
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Binding Rune III"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Binding Rune III");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Binding Rune II"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Binding Rune II");
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Binding Rune I"))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Binding Rune I");
                        return false;
                    }
                }

                // Needle Rune
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Needle Rune II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Needle Rune II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Needle Rune I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Needle Rune I");
                    return false;
                }

                // Rune Burst
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Burst V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Burst V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Burst IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Burst IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Burst III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Burst III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Burst II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Burst II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rune Burst I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rune Burst I");
                    return false;
                }
            }

            // Ambush
            if (Settings.AllowAmbush && DateTime.Now > LastAmbushUseSkillTimer.AddSeconds(5))
            {
                if (CheckAndExecuteSkills(entity, "Ambush", 6))
                {
                    LastAmbushUseSkillTimer = DateTime.Now;
                    return false;
                }
            }

            // Behind skill
            if (entityDistance <= 3)
            {
                var PosE = entity.Position;
                float dist = 1;
                var Angle = entity.Rotation;
                PosE.X = (float)(PosE.X - dist * Math.Sin(Angle * (Math.PI / 180)));
                PosE.Y = (float)(PosE.Y + dist * Math.Cos(Angle * (Math.PI / 180)));

                // 
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Venomous Strike I"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Venomous Strike I");
                    return false;
                }

                // 
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Weakening Blow II"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Weakening Blow II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Weakening Blow I"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Weakening Blow I");
                    return false;
                }

                // Surprise Attack
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surprise Attack V"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surprise Attack V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surprise Attack IV"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surprise Attack IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surprise Attack III"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surprise Attack III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surprise Attack II"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surprise Attack II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surprise Attack I"))
                {
                    SetPositionBehindEnemy(PosE);
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surprise Attack I");
                    return false;
                }
            }

            // Quickening Doom
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Quickening Doom I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Quickening Doom I");
                return false;
            }

            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_skills12).Item1 == false)
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
            return true;
        }

        private bool CheckPoisonState(Entity entity)
        {
            // Ambush assault effect id 3271
            // Apply Deadly Poison Poisoning Effect effect id 9100
            // Apply Deadly Poison Stun Effect - effect id 9089
            if (entity != null && (entity.StateList.GetState("Venomous Strike") != null
                || entity.StateList.GetState("Massacre") != null
                || entity.StateList.GetState("Ambush Assault") != null
                || entity.StateList.GetState("Apply Deadly Poison Poisoning Effect") != null
                || entity.StateList.GetState("Poison Slash") != null))
            {
                return true;
            }
            return false;
        }

        private uint CheckRunes(Entity entity)
        {
            if (entity.StateList.GetState(8307) != null)
            {
                return 5;
            }
            if (entity.StateList.GetState(8306) != null)
            {
                return 4;
            }
            if (entity.StateList.GetState(8305) != null)
            {
                return 3;
            }
            if (entity.StateList.GetState(8304) != null)
            {
                return 2;
            }
            if (entity.StateList.GetState(8303) != null)
            {
                return 1;
            }
            return 0;
        }

        private bool CheckScolopenPoison(uint quantity)
        {
            var scolopenPoisonItem = Game.InventoryList.GetItemByTypeId(169300010);

            if (scolopenPoisonItem != null && scolopenPoisonItem.Quantity >= quantity)
            {
                return true;
            }
            return false;
        }

        private void SetPositionBehindEnemy(Vector3D pos)
        {
            if (Settings.ForceAttackFromBehind)
                Game.Player.SetPosition(pos);
        }
    }
}
