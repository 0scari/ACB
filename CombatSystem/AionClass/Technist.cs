using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class TechnistSettings : AionClassBaseSetting
    {
        public TechnistSettings() : base()
        {
            TargetSearchDistance = 10;
        }
    }

    public class Technist : AionClassBase
    {
        private TechnistSettings Settings { get; set; }

        private static string[] dmg_Skill = new string[]
        {
            "",
        };
        public Technist():this(new TechnistSettings())
        {
        }
        public Technist(TechnistSettings settings)
        {
            Settings = settings;
            CurrentAionClass = eAionClass.Technist;
        }

        public override bool Attack(Entity entity, float range)
        {
            // 
            if (HelperFunction.CheckAvailable("Rapidfire I"))
            {
                HelperFunction.CheckExecute("Rapidfire I");
                return false;
            }
            
            if (HelperFunction.CheckAvailable("Precision Shot I"))
            {
                HelperFunction.CheckExecute("Precision Shot I");
                return false;
            }
            
            // 
            if (HelperFunction.CheckAvailable("Packed Bomb I"))
            {
                HelperFunction.CheckExecute("Packed Bomb I");
                return false;
            }

            // 
            if (HelperFunction.CheckAvailable("Gunshot I"))
            {
                HelperFunction.CheckExecute("Gunshot I");
                return false;
            }

            // 
            if (HelperFunction.CheckAvailable("Bullet Resistance I") && Game.Player.HealthPercentage < 50)
            {
                HelperFunction.CheckExecute("Bullet Resistance I");
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
