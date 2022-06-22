using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class WarriorSettings : AionClassBaseSetting
    {
        public WarriorSettings() : base()
        {
            TargetSearchDistance = 5;
        }
    }

    public class Warrior : AionClassBase
    {
        private WarriorSettings Settings { get; set; }

        public Warrior(WarriorSettings settings)
        {
            Settings = settings;
            CurrentAionClass = eAionClass.Warrior;
        }

        public Warrior() : this(new WarriorSettings())
        {
        }

        public override bool Attack(Entity entity, float range)
        {
            // Rage
            if (Game.Player.HealthPercentage < 95 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(161))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(161);
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

            // Weakening Severe Blow I
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(151))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(151);
                return false;
            }

            // Ferocious Strike
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(169))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(169);
                return false;
            }

            // Robust Blow I
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(165))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(165);
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
