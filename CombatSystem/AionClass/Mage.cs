using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class MageSettings : AionClassBaseSetting
    {
        public MageSettings() : base()
        {
            TargetSearchDistance = 25;
        }
    }
    public class Mage : AionClassBase
    {

        private static string[] dmg_Skill;

        private MageSettings Settings { get; set; }

        public Mage() : this(new MageSettings())
        {

        }

        public Mage(MageSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Mage;

            dmg_Skill = new string[] { "Ice Chain I", "Flame Bolt I" };

        }

        public override bool Attack(Entity entity, float range)
        {
            // 
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Blaze I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Blaze I");
                return false;
            }

            // 
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Stone Skin I") && Game.Player.StateList.GetState("Stone Skin I") == null)
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Stone Skin I");
                return false;
            }

            // 
            if (Game.Player.ManaCurrent < 150 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Absorb Energy I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Absorb Energy I");
                return false;
            }

            if (ExecuteSkillFromList(entity, dmg_Skill).Item1 == false)
            {
                return false;
            }

            // 
            if (Game.Player.StateList.GetList().Where(st=>st.Value.Name_Eu.IndexOf("Erosion") >=0).Any() == false&& AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Erosion I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Erosion I");
                return false;
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
    }
}
