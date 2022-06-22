using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class PainterSettings : AionClassBaseSetting
    {
        public bool AllowWeaving { get; set; }

        [System.ComponentModel.DisplayName("Life Binding HP < %")]
        public int LifeBindingLowHP { get; set; }

        [System.ComponentModel.DisplayName("Colour Grenade MP < %")]
        public int ColourGrenadeLowMP { get; set; }

        [System.ComponentModel.DisplayName("Allow Surprise Attack")]
        public bool AllowSurpriseAttack { get; set; }


        [System.ComponentModel.DisplayName("Allow Target Locked")]
        public bool AllowTargetLocked { get; set; }

        public PainterSettings() : base()
        {
            AllowWeaving = false;
            AllowSurpriseAttack = false;
            AllowTargetLocked = true;
            LifeBindingLowHP = 40;
            ColourGrenadeLowMP = 50;
            TargetSearchDistance = 10;
        }
    }

    public class Painter : AionClassBase
    {
        private bool CheckLaserSkill { get; set; }
        private ulong CheckLaserSkillTimer { get; set; }
        private PainterSettings Settings { get; set; }
        private static string[] dmg_skills = new string[]
        {
            "Colour Fist", // Long cast
            "Flash Portrait",
            "Colour Monster", // 3x Stage
            "Colour Fight", // 4x Multicast

            "Colour Explosion",
            "Sustained Colour Immersion",
            "Colour Immersion",


            "Colour Grenade",
        };
        private static string[] dmg_skills2 = new string[]
        {
            "Work Destruction",
            "Colour Rocket",
        //    "Time Holding",

        };
        private static string[] chain_skills = new string[]
        {
            // Colour Grenade
            "Sour Dye",
            "Powerful Shot",

            // Surprise attack
            "Blow",
            "Retreat",



            // Aether's hold
            "Punishment Strap",

            // Remove shock
            "Instant Petrification",
        };

        public Painter() : this(new PainterSettings())
        {
        }

        public Painter(PainterSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Painter;

            CheckLaserSkill = false;
            CheckLaserSkillTimer = 0;
        }

        public override bool Attack(Entity entity, float range)
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && HelperFunction.CheckAvailable(283))
            {
                HelperFunction.CheckExecute(283);
                return false;
            }

            if (CheckLaserSkill)
            {
                // Delay
                if (CheckLaserSkillTimer + 500>=Game.Time())
                {
                    return false;
                }
                else if (Game.Player.CurrentSkillId!=0)
                {
                    // Renew timer
                    CheckLaserSkillTimer = Game.Time();
                    return false;
                }
                else
                {
                    CheckLaserSkill = false;
                }
            }

            if (WaitAutoAttack && Settings.AllowWeaving)
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


            

            range = (float)Game.Player.Position.DistanceToPosition(entity.Position);


            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                return false;
            }

            // 10% damage
            if (Settings.AllowTargetLocked && entity.HealthCurrent > 50000)
            {
                if (HelperFunction.CheckAvailable("Into the Black"))
                {
                    HelperFunction.CheckExecute("Into the Black");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Target Locked"))
                {
                    HelperFunction.CheckExecute("Target Locked");
                    return false;
                }
            }

            // DP Buff
            if (Game.Player.DP >= 2000 && HelperFunction.CheckAvailable("Colour Boost"))
            {
                HelperFunction.CheckExecute("Colour Boost");
                return false;
            }

            // Heal skill
            if (Game.Player.HealthPercentage<Settings.LifeBindingLowHP && ((Game.Player.Level>=80 &&entity.HealthPercentage > 60) || Game.Player.Level<80) &&HelperFunction.CheckAvailable("Life Binding"))
            {
                HelperFunction.CheckExecute("Life Binding");
                CheckLaserSkill = true;
                CheckLaserSkillTimer = Game.Time();
                return false;
            }

            if (Game.Player.ManaPercentage<Settings.ColourGrenadeLowMP&& HelperFunction.CheckAvailable("Colour Grenade"))
            {
                HelperFunction.CheckExecute("Colour Grenade");
                return false;
            }

            // Surprise Attack
            if (Settings.AllowSurpriseAttack)
            {
                if (HelperFunction.CheckAvailable("Sudden Smash"))
                {
                    HelperFunction.CheckExecute("Sudden Smash");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Shocking Blast"))
                {
                    HelperFunction.CheckExecute("Shocking Blast");
                    return false;
                }
                else if (HelperFunction.CheckAvailable("Surprise Attack"))
                {
                    HelperFunction.CheckExecute("Surprise Attack");
                    return false;
                }
            }

            if (ExecuteSkillFromList(entity, dmg_skills).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            if (HelperFunction.CheckAvailable("Band of Rage"))
            {
                HelperFunction.CheckExecute("Band of Rage");
                CheckLaserSkill = true;
                CheckLaserSkillTimer = Game.Time();
                return false;
            }

            if (ExecuteSkillFromList(entity, dmg_skills2).Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            // Initial Attack 1: Automatic Attack
            if (true)
            {
                HelperFunction.CheckExecute("Attack/Chat");
            }

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Heal()
        {
            return true;
        }

        public override bool Pause()
        {
            // 
            uint ColourOfTranscendence = 5592;
            uint ColourOfResistance = 5593;
            uint ColourOfAgility = 5594;
            if (Game.Player.Level>10&& Game.Player.StateList.GetState(ColourOfAgility) ==null && Game.Player.StateList.GetState(ColourOfResistance) == null && Game.Player.StateList.GetState(ColourOfTranscendence) == null
                && Game.Player.StateList.GetList().Where(tempState=>tempState.Key>= 5658&& tempState.Key <= 5664).Count()==0 && Game.Player.StateList.GetState(5569) == null && Game.Player.StateList.GetState(5570) == null && Game.Player.StateList.GetState(5571) == null
                && HelperFunction.CheckAvailable("Portrait of Resurrection"))
            {
                HelperFunction.CheckExecute("Portrait of Resurrection");
                return false;
            }

            return true;
        }

        public override bool PartyCheck()
        {
            return true;
        }

        public override bool NoGravityAntiStuck()
        {
            return true;
        }
    }
}
