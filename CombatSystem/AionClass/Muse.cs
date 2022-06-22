using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class MuseSettings : AionClassBaseSetting
    {
        public MuseSettings() : base()
        {
            TargetSearchDistance = 10;
        }
    }
    public class Muse : AionClassBase
    {
        private MuseSettings Settings { get; set; }

        public Muse() : this(new MuseSettings())
        {

        }

        public Muse(MuseSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Muse;
        }

        public override bool Attack(Entity entity, float range)
        {

            return true;
        }

        public override bool PartyCheck()
        {
            return true;
        }

        public override bool Heal()
        {
          /*  if (Game.Player.HealthPercentage < 50 && Helper.HelperFunction.CheckAvailable("Gentle Echo"))
            {
                Helper.HelperFunction.CheckExecute("Gentle Echo", Game.Player);
                return false;
            }*/

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
