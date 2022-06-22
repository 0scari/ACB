using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class GunslingerSettings : AionClassBaseSetting
    {
        public GunslingerSettings() : base()
        {
            TargetSearchDistance = 25;
        }
    }

    public class Gunslinger : AionClassBase
    {

        private static Dictionary<string, int> chain_skills = new Dictionary<string, int>() {};

        private static Dictionary<string, int> dmg_skills = new Dictionary<string, int>() {};


        private GunslingerSettings Settings { get; set; }

        public Gunslinger() : this(new GunslingerSettings())
        {

        }
        public Gunslinger(GunslingerSettings settings)
        {
            this.Settings = settings;
        }

        public override bool Attack(Entity entity, float range)
        {
            return true;
        }

        public override bool PartyCheck()
        {
            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Heal()
        {
            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool Pause()
        {
            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool NoGravityAntiStuck()
        {
            throw new NotImplementedException();
        }
    }
}
