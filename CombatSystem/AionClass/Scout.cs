using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class ScoutSettings : AionClassBaseSetting
    {
        public ScoutSettings() : base()
        {
            TargetSearchDistance = 10;
        }
    }

    public class Scout : AionClassBase
    {
        private ScoutSettings Settings { get; set; }

        public Scout():this(new ScoutSettings())
        {

        }

        public Scout(ScoutSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Scout;
        }

        public override bool Attack(Entity entity, float range)
        {
            // 
            if (HelperFunction.CheckAvailable("Focused Evasion I") && Game.Player.HealthPercentage<50)
            {
                HelperFunction.CheckExecute("Focused Evasion I");
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
                HelperFunction.CheckExecute(Game.AttackChatCommand);
                return false;
            }
            // 
            if (HelperFunction.CheckAvailable("Soul Slash I"))
            {
                WaitAutoAttack = true;
                HelperFunction.CheckExecute("Soul Slash I");
                return false;
            }
            else if (HelperFunction.CheckAvailable("Swift Edge I"))
            {
                WaitAutoAttack = true;
                HelperFunction.CheckExecute("Swift Edge I");
                return false;
            }

            // 
            if (HelperFunction.CheckAvailable("Counterattack I"))
            {
                WaitAutoAttack = true;
                HelperFunction.CheckExecute("Counterattack I");
                return false;
            }

            // 
            if (HelperFunction.CheckAvailable("Surprise Attack I"))
            {
                WaitAutoAttack = true;
                HelperFunction.CheckExecute("Surprise Attack I");
                return false;
            }


            HelperFunction.CheckExecute(Game.AttackChatCommand);
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
    }
}
