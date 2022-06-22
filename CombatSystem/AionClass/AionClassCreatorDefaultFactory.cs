using AionBotnet.AionGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class AionClassCreatorDefaultFactory : AionClassFactory
    {
        public override AionClassBase GetAionClass(eClass aionClass)
        {
            switch (aionClass)
            {
                case eClass.Warrior:
                    return new Warrior();
                case eClass.Gladiator:
                    return new Gladiator();
                case eClass.Templar:
                    return new Templar();
                case eClass.Scout:
                    return new Scout();
                case eClass.Assassin:
                    return new Assassin();
                case eClass.Ranger:
                    return new Ranger();
                case eClass.Mage:
                    return new Mage();
                case eClass.Sorcerer:
                    return new Sorcerer();
                case eClass.Spiritmaster:
                    return new Spiritmaster();
                case eClass.Priest:
                    return new Priest();
                case eClass.Cleric:
                    return new Cleric();
                case eClass.Chanter:
                    return new Chanter();
                case eClass.Technist:
                    return new Technist();
                case eClass.Aethertech:
                    return new Aethertech();
                case eClass.Gunslinger:
                    return new Gunslinger();
                case eClass.Muse:
                    return new Muse();
                case eClass.Songweaver:
                    return new Songweaver();
                case eClass.Unknown_01:
                    break;
                default:
                    throw new ArgumentException(aionClass.ToString());
               //     break;
            }
            throw new ArgumentException(aionClass.ToString());
        }

        public override AionClassBase GetAionClass(eClass aionClass, AionClassBaseSetting settings)
        {
            switch (aionClass)
            {
                case eClass.Warrior:
                    return new Warrior((WarriorSettings)settings);
                case eClass.Gladiator:
                    return new Gladiator((GladiatorSettings)settings);
                case eClass.Templar:
                    return new Templar((TemplarSettings)settings);
                case eClass.Scout:
                    return new Scout((ScoutSettings)settings);
                case eClass.Assassin:
                    return new Assassin((AssassinSettings)settings);
                case eClass.Ranger:
                    return new Ranger((RangerSettings)settings);
                case eClass.Mage:
                    return new Mage((MageSettings)settings);
                case eClass.Sorcerer:
                    return new Sorcerer((SorcererSettings)settings);
                case eClass.Spiritmaster:
                    return new Spiritmaster((SpiritmasterSettings)settings);
                case eClass.Priest:
                    return new Priest((PriestSettings)settings);
                case eClass.Cleric:
                    return new Cleric((ClericSettings)settings);
                case eClass.Chanter:
                    return new Chanter((ChanterSettings)settings);
                case eClass.Technist:
                    return new Technist((TechnistSettings)settings);
                case eClass.Aethertech:
                    return new Aethertech((AethertechSettings)settings);
                case eClass.Gunslinger:
                    return new Gunslinger((GunslingerSettings)settings);
                case eClass.Muse:
                    return new Muse((MuseSettings)settings);
                case eClass.Songweaver:
                    return new Songweaver((SongweaverSettings)settings);
                case eClass.Painter:
                    return new Painter((PainterSettings)settings);
                case eClass.Unknown_01:
                    break;
                default:
                    throw new ArgumentException(aionClass.ToString());
                    //     break;
            }
            throw new ArgumentException(aionClass.ToString());
        }

        public override AionClassBase GetAionClass(eAionClass aionClass)
        {
            throw new NotImplementedException();
        }

        public override AionClassBaseSetting GetClassSetting(eClass aionClass)
        {
            throw new NotImplementedException();
        }
    }
}
