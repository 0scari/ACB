using AionBotnet.AionGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public abstract class AionClassFactory
    {
        public abstract AionClassBase GetAionClass(eAionClass aionClass);
        public abstract AionClassBase GetAionClass(eClass aionClass);
        public abstract AionClassBase GetAionClass(eClass aionClass, AionClassBaseSetting settings);
        public abstract AionClassBaseSetting GetClassSetting(eClass aionClass);
    }
}
