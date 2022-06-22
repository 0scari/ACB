using AionBotnet.AionGame;
using AionBotnet.AionGame.Enums;
using AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class CombatSystemDefaultSetting : CombatSystemSettingBase
    {
        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_ChanterSettings", "Chanter")]
        [ReadOnly(true)]
        public ChanterSettings ChanterSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_AethertechSettings", "Aethertech")]
        [ReadOnly(true)]
        [Browsable(false)]
        public AethertechSettings AethertechSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_AssassinSettings", "Assassin")]
        [ReadOnly(true)]
        public AssassinSettings AssassinSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_ClericSettings", "Cleric")]
        [ReadOnly(true)]
        public ClericSettings ClericSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_GladiatorSettings", "Gladiator")]
        [ReadOnly(true)]
        public GladiatorSettings GladiatorSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_GunslingerSettings", "Gunslinger")]
        [ReadOnly(true)]
        [Browsable(false)]
        public GunslingerSettings GunslingerSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_MageSettings", "Mage")]
        [ReadOnly(true)]
        public MageSettings MageSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_MuseSettings", "Muse")]
        [ReadOnly(true)]
        public MuseSettings MuseSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_PriestSettings", "Priest")]
        [ReadOnly(true)]
        public PriestSettings PriestSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_RangerSettings", "Ranger")]
        [ReadOnly(true)]
        public RangerSettings RangerSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_ScoutSettings", "Scout")]
        [ReadOnly(true)]
        public ScoutSettings ScoutSettings { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_SongweaverSettings", "Songweaver")]
        [ReadOnly(true)]
        [Browsable(false)]
        public SongweaverSettings SongweaverSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_PainterSettings", "Painter")]
        [ReadOnly(true)]
        [Browsable(false)]
        public PainterSettings PainterSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_SorcererSettings", "Sorcerer")]
        [ReadOnly(true)]
        public SorcererSettings SorcererSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_SpiritmasterSettings", "Spiritmaster")]
        [ReadOnly(true)]
        public SpiritmasterSettings SpiritmasterSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_TechnistSettings", "Technist")]
        [ReadOnly(true)]
        [Browsable(false)]
        public TechnistSettings TechnistSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_TemplarSettings", "Templar")]
        [ReadOnly(true)]
        public TemplarSettings TemplarSettings { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CategoryDynamicAttribute(ScriptDirectory, "ClassSettingsCategoryName", "Class Settings")]
        [DisplayNameDynamic(ScriptDirectory, "ClassSettings_WarriorSettings", "Warrior")]
        [ReadOnly(true)]
        public WarriorSettings WarriorSettings { get; set; }


        public CombatSystemDefaultSetting() : base()
        {
            ChanterSettings = new ChanterSettings();
            AethertechSettings = new AethertechSettings();
            AssassinSettings = new AssassinSettings();
            ClericSettings = new ClericSettings();
            GladiatorSettings = new GladiatorSettings();
            GunslingerSettings = new GunslingerSettings();
            MageSettings = new MageSettings();
            MuseSettings = new MuseSettings();
            PriestSettings = new PriestSettings();
            RangerSettings = new RangerSettings();
            ScoutSettings = new ScoutSettings();
            SongweaverSettings = new SongweaverSettings();
            PainterSettings = new PainterSettings();
            SorcererSettings = new SorcererSettings();
            SpiritmasterSettings = new SpiritmasterSettings();
            TechnistSettings = new TechnistSettings();
            TemplarSettings = new TemplarSettings();
            WarriorSettings = new WarriorSettings();
        }

        public CombatSystemDefaultSetting(CombatSystemDefaultSetting combatSystemSetting) : base(combatSystemSetting)
        {            
            ChanterSettings = new ChanterSettings(combatSystemSetting.ChanterSettings);
            ClericSettings = new ClericSettings(combatSystemSetting.ClericSettings);
            AssassinSettings = new AssassinSettings(combatSystemSetting.AssassinSettings);
            RangerSettings = new RangerSettings(combatSystemSetting.RangerSettings);
            GladiatorSettings = new GladiatorSettings(combatSystemSetting.GladiatorSettings);
            TemplarSettings = new TemplarSettings(combatSystemSetting.TemplarSettings);
            SorcererSettings = new SorcererSettings(combatSystemSetting.SorcererSettings);
            SpiritmasterSettings = new SpiritmasterSettings(combatSystemSetting.SpiritmasterSettings);


            MageSettings = new MageSettings();
            MuseSettings = new MuseSettings();
            PriestSettings = new PriestSettings();
            WarriorSettings = new WarriorSettings();
            ScoutSettings = new ScoutSettings();
            TechnistSettings = new TechnistSettings();


            SongweaverSettings = new SongweaverSettings();
            PainterSettings = new PainterSettings();
            AethertechSettings = new AethertechSettings();
            GunslingerSettings = new GunslingerSettings();
        }

        protected const string ScriptDirectory = "Script\\Source\\Include\\AionClassic\\Include\\CombatSystem";

        public override AionClass.AionClassBaseSetting GetClassSetting()
        {
            AionClassBaseSetting ClassSettings = null;

            switch (Game.Player.Class)
            {
                case eClass.Warrior:
                    ClassSettings = WarriorSettings;
                    break;
                case eClass.Gladiator:
                    ClassSettings = GladiatorSettings;
                    break;
                case eClass.Templar:
                    ClassSettings = TemplarSettings;
                    break;
                case eClass.Scout:
                    ClassSettings = ScoutSettings;
                    break;
                case eClass.Assassin:
                    ClassSettings = AssassinSettings;
                    break;
                case eClass.Ranger:
                    ClassSettings = RangerSettings;
                    break;
                case eClass.Mage:
                    ClassSettings = MageSettings;
                    break;
                case eClass.Sorcerer:
                    ClassSettings = SorcererSettings;
                    break;
                case eClass.Spiritmaster:
                    ClassSettings = SpiritmasterSettings;
                    break;
                case eClass.Priest:
                    ClassSettings = PriestSettings;
                    break;
                case eClass.Cleric:
                    ClassSettings = ClericSettings;
                    break;
                case eClass.Chanter:
                    ClassSettings = ChanterSettings;
                    break;
                case eClass.Technist:
                    ClassSettings = TechnistSettings;
                    break;
                case eClass.Aethertech:
                    ClassSettings = AethertechSettings;
                    break;
                case eClass.Gunslinger:
                    ClassSettings = GunslingerSettings;
                    break;
                case eClass.Muse:
                    ClassSettings = MuseSettings;
                    break;
                case eClass.Songweaver:
                    ClassSettings = SongweaverSettings;
                    break;
                case eClass.Painter:
                    ClassSettings = PainterSettings;
                    break;
                case eClass.Unknown_01:
                    break;
                default:
                    break;
            }

            return ClassSettings;
        }
    }
}
