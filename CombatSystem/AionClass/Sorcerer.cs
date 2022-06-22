using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class SorcererSettings : AionClassBaseSetting
    {
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseRobeOfFlame", "Use Robe of Flame")]
        public bool UseRobeOfFlame { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseRobeOfEarth", "Use Robe of Earth")]
        public bool UseRobeOfEarth { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseRobeOfCold", "Use Robe of Cold")]
        public bool UseRobeOfCold { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseLumielsWisdomMonsterAboveHPPercentage", "Use Lumiel's Wisdom if mob HP>%")]
        public int UseLumielsWisdomMonsterAboveHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseVaizelsWisdomMonsterAboveHPPercentage", "Use Vaizel's Wisdom if mob HP>%")]
        public int UseVaizelsWisdomMonsterAboveHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseZikelsWisdomMonsterAboveHPPercentage", "Use Zikel's Wisdom if mob HP>%")]
        public int UseZikelsWisdomMonsterAboveHPPercentage { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseDelayedBlastMonsterAboveHPPercentage", "Use Delayed Blast if mob HP>%")]
        public int UseDelayedBlastMonsterAboveHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseInfernoMonsterAboveHPPercentage", "Use Inferno if mob HP>%")]
        public int UseInfernoMonsterAboveHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseGlacialShardMonsterAboveHPPercentage", "Use Glacial Shard if mob HP>%")]
        public int UseGlacialShardMonsterAboveHPPercentage { get; set; }

        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage", "Use Pandaemonium Focus \\Aetheric Spell if mob HP>%")]
        public int UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage { get; set; }


        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_Sorcerer_UseStaminaAbsorptionBelowHPPercentage", "Use Stamina Absorption if HP<%")]
        public int UseStaminaAbsorptionBelowHPPercentage { get; set; }

        public SorcererSettings() : base()
        {
            TargetSearchDistance = 10;


            UseRobeOfFlame = true;
            UseRobeOfEarth = false;
            UseRobeOfCold = false;
            UseLumielsWisdomMonsterAboveHPPercentage = 99;
            UseVaizelsWisdomMonsterAboveHPPercentage = 99;
            UseZikelsWisdomMonsterAboveHPPercentage = 99;

            UseDelayedBlastMonsterAboveHPPercentage = 99;
            UseInfernoMonsterAboveHPPercentage = 95;
            UseGlacialShardMonsterAboveHPPercentage = 95;
            UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage = 70;

            UseStaminaAbsorptionBelowHPPercentage = 70;
        }

        public SorcererSettings(SorcererSettings classSettings) : base(classSettings)
        {
            UseRobeOfFlame = classSettings.UseRobeOfFlame;
            UseRobeOfEarth = classSettings.UseRobeOfEarth;
            UseRobeOfCold = classSettings.UseRobeOfCold;

            UseLumielsWisdomMonsterAboveHPPercentage = classSettings.UseLumielsWisdomMonsterAboveHPPercentage;
            UseVaizelsWisdomMonsterAboveHPPercentage = classSettings.UseVaizelsWisdomMonsterAboveHPPercentage;
            UseZikelsWisdomMonsterAboveHPPercentage = classSettings.UseZikelsWisdomMonsterAboveHPPercentage;

            UseDelayedBlastMonsterAboveHPPercentage = classSettings.UseDelayedBlastMonsterAboveHPPercentage;
            UseInfernoMonsterAboveHPPercentage = classSettings.UseInfernoMonsterAboveHPPercentage;
            UseGlacialShardMonsterAboveHPPercentage = classSettings.UseGlacialShardMonsterAboveHPPercentage;
            UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage = classSettings.UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage;

            UseStaminaAbsorptionBelowHPPercentage = classSettings.UseStaminaAbsorptionBelowHPPercentage;
        }
    }

    public class Sorcerer : AionClassBase
    {
        private SorcererSettings Settings { get; set; }
        private static string[] dmg_skill = new string[]
        {
            "Wind Cut Down V",
            "Wind Cut Down IV",
            "Wind Cut Down III",
            "Wind Cut Down II",
            "Wind Cut Down I",

            "Frostbite II",
            "Frostbite I",

            "Flame Spray II",
            "Flame Spray I",

            "Flame Fusion II",
            "Flame Fusion I",

            "Flame Harpoon IV",
            "Flame Harpoon III",
            "Flame Harpoon II",
            "Flame Harpoon I",

            "Arcane Tunderbolt II",
            "Arcane Tunderbolt I",


            "Soul Freeze II", // Silence
            "Soul Freeze I",

            "Summon Rock IV",
            "Summon Rock III",
            "Summon Rock II",
            "Summon Rock I",

            "Ice Harpoon I",
            "Ice Harpoon II",

            "Aether's Hold III",
            "Aether's Hold II",
            "Aether's Hold I",

            "Flame Bolt V",
            "Flame Bolt IV",
            "Flame Bolt III",
            "Flame Bolt II",
            "Flame Bolt I",

        };

        private static string[] chain_skills = new string[]
        {
            // Ice Chain
            "Frozen Shock V",
            "Frozen Shock IV",
            "Frozen Shock III",
            "Frozen Shock II",
            "Frozen Shock I",

            // Aether hold
            "Aether Flame III",
            "Aether Flame II",
            "Aether Flame I",

            "Magic Fist II",
            "Magic Fist I",


            // Flame Bolt
            "Blaze V",
            "Blaze IV",
            "Blaze III",
            "Blaze II",
            "Blaze I",
        };

        public ulong BuffCheckTime { get; set; }
        private bool SpearOfWindIsCasting { get; set; }

        public Sorcerer() : this(new SorcererSettings())
        {
        }

        public Sorcerer(SorcererSettings settings)
        {
            BuffCheckTime = 0;
            Settings = settings;
            CurrentAionClass = eAionClass.Sorcerer;

            SpearOfWindIsCasting = false;
        }

        public override bool Attack(Entity entity, float range)
        {
            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Remove Shock I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Remove Shock I");
                return false;
            }

            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                WaitAutoAttack = true;
                return false;
            }

            var entityDistance = Game.Player.Position.Distance(entity.Position);

            // ------ Skill buff ------
            // Lumiel's Wisdom I
            if (entity.HealthPercentage >= Settings.UseLumielsWisdomMonsterAboveHPPercentage && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1556))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1556);
                return false;
            }

            // Vaizel's Wisdom I
            if (entity.HealthPercentage >= Settings.UseVaizelsWisdomMonsterAboveHPPercentage && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1554))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1554);
                return false;
            }

            // Zikel's Wisdom I
            if (entity.HealthPercentage >= Settings.UseZikelsWisdomMonsterAboveHPPercentage && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1555))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1555);
                return false;
            }

            // Magic Boost I
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1432))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1432);
                return false;
            }

            // Delayed Blast
            if (entity.HealthPercentage > Settings.UseDelayedBlastMonsterAboveHPPercentage)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2200))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2200);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1512))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1512);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1460))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1460);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1459))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1459);
                    return false;
                }
            }

            // Ice Chain
            if (entity.HealthPercentage > 98)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ice Chain V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ice Chain V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ice Chain IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ice Chain IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ice Chain III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ice Chain III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ice Chain II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ice Chain II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Ice Chain I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Ice Chain I");
                    return false;
                }
            }

            // Stamina Absorption 			
            if (Game.Player.HealthPercentage < Settings.UseStaminaAbsorptionBelowHPPercentage)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2290))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2290);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2289))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2289);
                    return false;
                }
            }


            // DP skill. 			
            if (Game.Player.DP >= 2000 && entity.HealthPercentage < Settings.UsePandaemoniumFocusAethericSpellMonsterAboveHPPercentage)
            {
                // Pandaemonium Focus
                if (Game.Player.IsAsmodian)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1592))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1592);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1394))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1394);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1393))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1393);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1382))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1382);
                        return false;
                    }
                }

                // Aetheric Spell I
                if (Game.Player.IsElyos)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1591))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1591);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1392))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1392);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1391))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1391);
                        return false;
                    }
                    else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1381))
                    {
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1381);
                        return false;
                    }
                }
            }

            // Recovery mana skill
            if (Game.Player.ManaPercentage < 20 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Refracting Shard I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Refracting Shard I");
                return false;
            }

            // Inferno
            if (entity.HealthPercentage > Settings.UseInfernoMonsterAboveHPPercentage)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(2201))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(2201);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1583))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1583);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1567))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1567);
                    return false;
                }
            }

            // Glacial Shard
            if (entity.HealthPercentage > Settings.UseGlacialShardMonsterAboveHPPercentage)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1434))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1434);
                    return false;
                }
            }

            // Overtime danage
            if (entity.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Flame Cage", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                string flameCageSkillName = "Flame Cage";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(flameCageSkillName + " IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(flameCageSkillName + " IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(flameCageSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(flameCageSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(flameCageSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(flameCageSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(flameCageSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(flameCageSkillName + " I");
                    return false;
                }
            }


            // Recovery mana skill
            if (Game.Player.ManaPercentage < 30 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Refracting Shard I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Refracting Shard I");
                return false;
            }

            // Soul Absorption
            if (Game.Player.ManaPercentage < 30 && Game.Player.HealthPercentage > 50)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1451))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1451);
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(1572))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(1572);
                    return false;
                }
            }

            if (entityDistance < 3)
            {
                string freezingWindSkillName = "Freezing Wind";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(freezingWindSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(freezingWindSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(freezingWindSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(freezingWindSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(freezingWindSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(freezingWindSkillName + " I");
                    return false;
                }
            }

            // Area damage skill
            // Fire Burst III
            // Flaming Meteor II
            // Illusion Storm II
            // Lava Tempest I
            // Lava Tsunami I


            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_skill).Item1 == false)
            {
                WaitAutoAttack = true;
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
            var missingMana = Game.Player.ManaMaximum - Game.Player.ManaCurrent;

            // Mana
            string absorbEnergySkillName = "Absorb Energy";
            if (missingMana > 705 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbEnergySkillName + " V"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbEnergySkillName + " V");
                return false;
            }
            else if (missingMana > 595 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbEnergySkillName + " IV"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbEnergySkillName + " IV");
                return false;
            }
            else if (missingMana > 475 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbEnergySkillName + " III"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbEnergySkillName + " III");
                return false;
            }
            else if (missingMana > 332 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbEnergySkillName + " II"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbEnergySkillName + " II");
                return false;
            }
            else if (missingMana > 184 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(absorbEnergySkillName + " I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(absorbEnergySkillName + " I");
                return false;
            }

            if (Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Stone Skin", StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                string stoneSkinSkillName = "Stone Skin";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " V"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " V");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(stoneSkinSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(stoneSkinSkillName + " I");
                    return false;
                }
            }


            if (Game.Player.ManaMaximum - Game.Player.ManaCurrent > 1800 && CountMods(6) == 0)
            {
                string gainManaSkillName = "Gain Mana";
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(gainManaSkillName + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(gainManaSkillName + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(gainManaSkillName + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(gainManaSkillName + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(gainManaSkillName + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(gainManaSkillName + " I");
                    return false;
                }
            }



            // Robe buff
            string robeOf_Buff = "";
            if (Settings.UseRobeOfFlame)
                robeOf_Buff = "Robe of Flame";
            else if (Settings.UseRobeOfEarth)
                robeOf_Buff = "Robe of Earth";
            else if (Settings.UseRobeOfCold)
                robeOf_Buff = "Robe of Cold";


            if (string.IsNullOrWhiteSpace(robeOf_Buff) == false && Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf(robeOf_Buff, StringComparison.InvariantCultureIgnoreCase) >= 0).Any() == false)
            {
                if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(robeOf_Buff + " IV"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(robeOf_Buff + " IV");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(robeOf_Buff + " III"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(robeOf_Buff + " III");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(robeOf_Buff + " II"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(robeOf_Buff + " II");
                    return false;
                }
                else if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(robeOf_Buff + " I"))
                {
                    AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(robeOf_Buff + " I");
                    return false;
                }
            }

            return true;
        }
    }
}
