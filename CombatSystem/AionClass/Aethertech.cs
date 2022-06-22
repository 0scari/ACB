using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class AethertechSettings : AionClassBaseSetting
    {
        public AethertechSettings() : base()
        {
            TargetSearchDistance = 10;
        }
    }
    public class Aethertech : AionClassBase
    {
        DateTime CallMechBuffTimer { get; set; }
        DateTime MysticShellBuffTimer { get; set; }
        private AethertechSettings Settings { get; set; }
        private static string[] dmg_Skill = new string[]
        {
            "Idium Strike",
            "Bludgeon",
            
            "Burning Cannon Shot",
            "Inciting Wind",
            "Wave",
            "Two-Handed Strike",
            "Cooling Wave",
            "Cleave Armour", // Stigma
            "Collapsing Smash",

        };

        private static string[] chain_skills = new string[]
        {
            "Light Attack",
            "Strong Attack",

            "Repeated Cannon Shot",
            "Electric Snare",
            "Weakening Sting",

            "Flame Jet",
            "Flame of Demolition",

            "Aerial Shot",
            "Wave of Destruction",
            // After remove shock
            "Cannon Shot Riposte",
            "Uppercut",
            "Sprint Strike",
            "Shot Riposte",
        };

        public Aethertech() : this(new AethertechSettings())
        {
        }

        public Aethertech(AethertechSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Aethertech;
            CallMechBuffTimer = DateTime.MinValue;
            MysticShellBuffTimer = DateTime.MinValue;
        }

        public override bool Attack(AionGame.Entity entity, float range)
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && HelperFunction.CheckAvailable(283))
            {
                HelperFunction.CheckExecute(283);
                return false;
            }

            if (WaitAutoAttack)
            {
                Game.Player.UpdateAutoAttackData();
                //     Game.WriteMessage(Game.Player.AttackStatus);
                if (Game.Player.IsAutoAttacking && Game.Player.CurrentAnimationTime < (Game.Player.MaxAnimationTimer * 0.9))
                {
                    WaitAutoAttack = false;
                }
                HelperFunction.CheckExecute("Attack/Chat");
                return false;
            }

            // Silence enemy
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Chain Shot"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Chain Shot");
                return false;
            }


            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                //         WaitAutoAttack = true;
                return false;
            }

            // Surge of glodry chain "Vampiric Wave" "Mana Absorption"
            if (Game.Player.HealthMaximum - Game.Player.HealthCurrent > 1000 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Vampiric Wave"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Vampiric Wave");
                return false;
            }

            // Silence enemy
            if (entity.CurrentSkillId > 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Silence Smash"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Silence Smash");
                return false;
            }

            // Silence enemy
            if (entity.CurrentSkillId > 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Electric Shock"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Electric Shock");
                return false;
            }


            // Resist magical attack
            if (entity.CurrentSkillId > 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Instant Defensive Shield"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Instant Defensive Shield");
                return false;
            }

            // Restore from 1000 to 2000 mana
            if (Game.Player.ManaMaximum - Game.Player.ManaCurrent > 5000 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Surge of Glory"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Surge of Glory");
                return false;
            }

            // Restore 6000mp 4000hp
            if ((Game.Player.ManaMaximum - Game.Player.ManaCurrent > 15000 || Game.Player.HealthMaximum - Game.Player.HealthCurrent > 8000) && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Rapid Recharge"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Rapid Recharge");
                return false;
            }
        

            // Mystic Shell
            DateTime tempTime = DateTime.Now;
            if (Game.Player.ManaPercentage > 50 && MysticShellBuffTimer < tempTime && Game.Player.StateList.GetState(2441) == null && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Mystic Shell"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Mystic Shell");
                MysticShellBuffTimer = tempTime.AddSeconds(3);
                return false;
            }




            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_Skill).Item1 == false)
            {
                //    WaitAutoAttack = true;
                return false;
            }

            // Initial Attack 1: Automatic Attack
            if (true)
            {
                HelperFunction.CheckExecute("Attack/Chat");
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
            DateTime tempTime = DateTime.Now;
            if (CallMechBuffTimer < tempTime)
            {
                var callMechBuff = Game.AbilityList.GetAbility("Call Mech");
                if (callMechBuff != null && callMechBuff.State == 0 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Call Mech"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Call Mech");
                    CallMechBuffTimer = tempTime.AddSeconds(10);
                    return false;
                }
            }

            return true;
        }
    }
}
