using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class SongweaverSettings : AionClassBaseSetting
    {
        /// <summary>
        /// Indicates whether or not party mana healing are allowed
        /// </summary>
        public bool AllowPartyHealingMana { get; set; }

        /// <summary>
        /// Indicates whether or not party healing are allowed
        /// </summary>
        public bool AllowPartyHealing { get; set; }

        /// <summary>
        /// Indicates whether or not party resurrection are allowed
        /// </summary>
        public bool AllowPartyResurrection { get; set; }

        [System.ComponentModel.DisplayName("Gentle Echo HP < %")]
        public int GentleEchoHPLow { get; set; }

        [System.ComponentModel.DisplayName("Rejuvenation Melody HP < %")]
        public int RejuvenationMelodyHPLow { get; set; }

        [System.ComponentModel.DisplayName("Allow Symphony of Destruction")]
        public bool AllowSymphonyfDestruction { get; set; }

        [System.ComponentModel.DisplayName("Allow Attack Resonation")]
        public bool AllowAttackResonation { get; set; }

        [System.ComponentModel.DisplayName("Allow Mosky Requiem")]
        public bool AllowMoskyRequiem { get; set; }


        [System.ComponentModel.DisplayName("Allow Melody Of Hope")]
        public bool AllowMelodyOfHope { get; set; }


        [System.ComponentModel.DisplayName("Melody Of Hope HP < %")]
        public int MelodyOfHopeHPLow { get; set; }

        [System.ComponentModel.DisplayName("Magic Boost Mode Allowed")]
        public bool MagicBoostModeEnabled { get; set; }

        public SongweaverSettings() : base()
        {
            TargetSearchDistance = 25;
            AllowPartyHealing = false;
            AllowPartyHealingMana = true;
            AllowPartyResurrection = true;
            AllowRemoveShock = true;
            AllowSymphonyfDestruction = false;
            GentleEchoHPLow = 70;
            RejuvenationMelodyHPLow = 60;
            AllowAttackResonation = true;
            AllowMoskyRequiem = false;
            AllowMelodyOfHope = false;
            MelodyOfHopeHPLow = 70;
            MagicBoostModeEnabled = true;
        }
    }

    public class Songweaver : AionClassBase
    {
        public bool AllowApproach { get; set; }
        public bool AllowSleep { get; set; }
        public double ChargeSkillDistance { get; set; }

        DateTime StateDispelTime { get; set; }
        Entity _SleepTarget { get; set; }
        Entity _SleepTargetRestore { get; set; }

        //
        DateTime magicBoostModeBuffTimer { get; set; }
        private SongweaverSettings Settings { get; set; }

        private static string[] chain_skills = new string[]{
            // 
            "Flame Harmony",
            "Wind Harmony",
            "Earth Harmony",

            // 
            "Tsunami Requiem",
            "Harmony of Death",

            "Ironclad Tank Harmony",
            "Harmony of Vengeance",
            "Harmony of Destruction",

            "Acute Grating Sound",
        };

        private static string[] dmg_skills = new string[] {
           // "Chilling Harmony",
            "Soul Harmony",
            "Sound of the Breeze",


            "Storm Variation",
            "Boosted Storm Variation",
            "Storm Harmony",
            "Storm Requiem",
            "Gust Requiem",


            "Squall Variation",
            "Loud Bang",
            "Mosky Requiem",
            "March of the Bees"
        };


        public Songweaver() : this(new SongweaverSettings())
        {
        }

        public Songweaver(SongweaverSettings settings)
        {
            Settings = settings;
            CurrentAionClass = eAionClass.Songweaver;
            StateDispelTime = DateTime.MinValue;
            _SleepTarget = null;
            _SleepTargetRestore = null;
            AllowApproach = true;
            AllowSleep = false;
            ChargeSkillDistance = 0;

            magicBoostModeBuffTimer = DateTime.MinValue;
        }

        public override bool Attack(Entity entity, float range)
        {
   /*         if (entity == null)
                return true;

            // enemy position check
            var position = Game.Player.Position;

            double attackRange = 1;

            var entityState = entity.StateList;

            var playerState = Game.Player.StateList;


            // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && HelperFunction.CheckAvailable(283))
            {
                HelperFunction.CheckExecute(283);
                return false;
            }
            if (HelperFunction.CheckAvailable("Melody of Appreciation") && Game.Player.HealthPercentage < 50)
            {
                HelperFunction.CheckExecute("Melody of Appreciation");
                return false;
            }


            // Check if we are allowed to sleep attackers.
            if (AllowSleep && SleepMultipleAttacker(entity, attackRange) == false)
            {
                return false;
            }

            // -----------------------------------------
            // --------------Wind Harmony---------------
            // -----------------------------------------
            if (HelperFunction.CheckAvailable("Chilling Harmony") && HelperFunction.CheckAvailable("Wind Harmony", true))
            {
                HelperFunction.CheckExecute("Chilling Harmony");
                return false;
            }

            // DOT Attack : Attack Resonation
            if (Settings.AllowAttackResonation && entity.HealthPercentage >= 80 && HelperFunction.CheckAvailable("Attack Resonation") && entityState.GetState("Attack Resonation") == null)
            {
                HelperFunction.CheckExecute("Attack Resonation");
                return false;
            }


            // Chain skill
            var chain_SkillsResult = ExecuteSkillFromList(entity, chain_skills);
            if (chain_SkillsResult.Item1 == false)
            {
                //         WaitAutoAttack = true;
                return false;
            }

            // 2000DP Skill Symphony of Wrath , Symphony of Destruction
            if (Settings.AllowMelodyOfHope && Game.Player.HealthPercentage< Settings.RejuvenationMelodyHPLow && Game.Player.DP >= 4000 && HelperFunction.CheckAvailable("Melody of Hope"))
            {
                HelperFunction.CheckExecute("Melody of Hope");
                return false;
            }

            // Buff: Melody of Cheer (20% attack speed for 15 seconds)
            if (HelperFunction.CheckAvailable("Melody of Cheer") && entity.HealthPercentage > 90)
            {
                HelperFunction.CheckExecute("Melody of Cheer");
                return false;
            }

            // Attack : Mosky Requiem
            if (Settings.AllowMoskyRequiem && entity.HealthPercentage >= 25 && HelperFunction.CheckAvailable("Mosky Requiem"))
            {
                HelperFunction.CheckExecute("Mosky Requiem");
                return false;
            }

            // -----------------------------------------
            // --------------Primary Skills-------------
            // -----------------------------------------

            // Harmony of Silence
            uint entitySkillID = entity.CurrentSkillId;
            var entitySkill = Game.SkillList.GetSkill(entity.CurrentSkillId);
//               if (HelperFunction.CheckAvailable("Harmony of Silence") && entitySkillID != 0 && entitySkill != null && entitySkill.IsMagical() && entity.GetSkillTime() >= 500)
//               {
//                   HelperFunction.CheckExecute("Harmony of Silence");
//                   return false;
//               }

            // if Melody of Cheer is active skip charge skill
            if (playerState.GetState("Melody of Cheer") == null)
            {
                var distanceToMob = entity.Position.DistanceToPosition(Game.Player.Position);

                // Fantastic Variation
                if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Fantastic Variation") && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Fantastic Variation");
                    return false;
                }



                if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Illusion Ensemble") && HelperFunction.CheckAvailable("Tsunami Requiem", true) && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Illusion Ensemble");
                    return false;
                }
                else if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Illusion Symphony") && HelperFunction.CheckAvailable("Tsunami Requiem", true) && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Illusion Symphony");
                    return false;
                }
                else if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Illusion Variation") && HelperFunction.CheckAvailable("Tsunami Requiem", true) && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Illusion Variation");
                    return false;
                }

                // Battle Variation
                if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Battle Variation") && (HelperFunction.CheckAvailable("Tsunami Requiem", true) || Game.Player.Level < 54) && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Battle Variation");
                    return false;
                }

                // Ascended Soul Variation
                if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Ascended Soul Variation") && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Ascended Soul Variation");
                    return false;
                }

                // Sea Variation
                if (entity.HealthCurrent >= 10000 && HelperFunction.CheckAvailable("Sea Variation") && distanceToMob >= ChargeSkillDistance)
                {
                    HelperFunction.CheckExecute("Sea Variation");
                    return false;
                }
            }

            // 2000DP Skill Symphony of Wrath , Symphony of Destruction
            if (Settings.AllowSymphonyfDestruction && Game.Player.DP >= 2000 && HelperFunction.CheckAvailable("Symphony of Destruction"))
            {
                HelperFunction.CheckExecute("Symphony of Destruction");
                return false;
            }


            // Attack : Disharmony
            if (entity.HealthPercentage >= 25 && HelperFunction.CheckAvailable("Disharmony"))
            {
                HelperFunction.CheckExecute("Disharmony");
                return false;
            }

            // Damage skill
            if (ExecuteSkillFromList(entity, dmg_skills).Item1 == false)
            {
                //    WaitAutoAttack = true;
                return false;
            }
*/

            // Nothing was executed, continue with other functions.
            return true;
        }

        public override bool PartyCheck()
        {
        /*    List<Entity> candidateMembers = new List<Entity>();
            List<Entity> candidateRessMembers = new List<Entity>();
            int healingGroupBenefitCount = 0;

            // Loop tra i membri del gruppo e ordinali in base agli HP mancanti. Controlla se il membro del gruppo può essere avvantaggiato da una cura AoE
            foreach (var tempMember in Game.PartyMemberList.GetList())
            {
                var tempEntity = tempMember.Value.GetEntity();

                if (tempEntity != null && tempEntity.IsDead)
                {
                    candidateRessMembers.Add(tempEntity);
                }

                if (tempEntity != null && tempEntity.IsDead == false && tempEntity.Position.DistanceToPosition(Game.Player.Position) < 20)
                {
                    candidateMembers.Add(tempEntity);

                    if ((tempEntity.HealthMaximum - tempEntity.HealthCurrent) > 2000)
                    {
                        healingGroupBenefitCount++;
                    }
                }

            }

            // Heal
            if (Settings.AllowPartyHealing)
            {
                // Order list by HP (less HP have priority)
                var hpOrder = candidateMembers.OrderBy(o => o.HealthPercentage).ToList();

                // Loop della list e inizia a curare i membri
                foreach (var tempMember in hpOrder)
                {
                    if (CheckHeal(tempMember) == false)
                        return false;
                }
            }

            // Mana
            if (Settings.AllowPartyHealingMana)
            {
                // Order list by MP (less MP have priority)
                var mpOrder = candidateMembers.OrderBy(o => o.ManaPercentage).ToList();

                // Loop della list e inizia a curare i membri
                foreach (var tempMember in mpOrder)
                {
                    if (CheckMana(tempMember) == false)
                        return false;
                }
            }

            // Buff force members
            foreach (var tempMember in candidateMembers)
            {
                if (TargetBuff(tempMember) == false)
                    return false;
            }

            // Ress party member
            if (Settings.AllowPartyResurrection)
            {
                foreach (var item in candidateRessMembers)
                {
                    // If is death
                    if (item.IsDead)
                    {
                        if (Helper.HelperFunction.CheckAvailable("Heavenly Rhapsody"))
                        {
                            Helper.HelperFunction.CheckExecute("Heavenly Rhapsody");
                            return false;
                        }
                    }
                }
            }*/

            return true;
        }

        public override bool Heal()
        {
       /*     // -----------------------------------------
            // -------------------Shock-----------------
            // -----------------------------------------
            if (Settings.AllowRemoveShock && HelperFunction.CheckAvailable(283))
            {
                HelperFunction.CheckExecute(283);
                return false;
            }

            // Check if we are allowed to execute our healing routines, after checking the force we can check our own HP.
            if (CheckHeal(Game.Player) == false)
            {
                return false;
            }


            // Check if we are allowed to execute our healing routines, after checking the force we can check our own MP.
            if (CheckMana(Game.Player) == false)
            {
                return false;
            }

            if (TargetBuff(Game.Player) == false)
            {
                return false;
            }*/
            // Nothing was executed, continue with other functions.
            return true;
        }

        /// <summary>
        /// Perform the safety checks before moving to the next target.
        /// </summary>
        /// <returns></returns>
        public override bool Pause()
        {
       /*     // Shield Melody
            if (HelperFunction.CheckAvailable("Shield Melody") && Game.Player.StateList.GetState("Shield Melody") == null)
            {
                HelperFunction.CheckExecute("Shield Melody", Game.Player);
                return false;
            }

            // -----------------------------------------
            // -----------------Bufovani----------------
            // -----------------------------------------
            if (HelperFunction.CheckAvailable("Snowflower Melody") && Game.Player.HealthPercentage < 40)
            {
                HelperFunction.CheckExecute("Snowflower Melody");
                return false;
            }

            DateTime tempTime = DateTime.Now;
            if (Settings.MagicBoostModeEnabled && magicBoostModeBuffTimer < tempTime)
            {
                var magicBoostModeBuff = Game.AbilityList.GetAbility("Magic Boost Mode");
                if (magicBoostModeBuff != null && magicBoostModeBuff.State == 0 && Helper.HelperFunction.CheckAvailable("Magic Boost Mode"))
                {
                    Helper.HelperFunction.CheckExecute("Magic Boost Mode", Game.Player);
                    magicBoostModeBuffTimer = tempTime.AddSeconds(10);
                    return false;
                }
            }*/

            // Nothing was executed, continue with other functions.
            return true;
        }

        /// <summary>
        /// Checks the healing requirements for the provided entity.
        /// </summary>
        /// <param name="entity">Contains the entity to perform healing on.</param>
        /// <returns></returns>
        private bool CheckHeal(Entity entity)
        {
        /*    // Retrieve the range of the entity compared to my own character position.
            double range = Game.Player.Position.DistanceToPosition(entity.Position);

            // Check if this routine is allowed to be ran under the current circumstances.
            if (entity.IsDead || (AllowApproach == false && range > 23))
            {
                return true;
            }

            if (entity.HealthPercentage < 90 && Helper.HelperFunction.CheckAvailable("Melody of Joy"))
            {
                Helper.HelperFunction.CheckExecute("Melody of Joy", entity);
                return false;
            }
            else if (Helper.HelperFunction.CheckAvailable("Soft Echo"))
            {
                Helper.HelperFunction.CheckExecute("Soft Echo");
                return false;
            }
            else if (Helper.HelperFunction.CheckAvailable("Mild Echo"))
            {
                Helper.HelperFunction.CheckExecute("Mild Echo");
                return false;
            }



            // Change the healing routine if I'm healing myself when allowed to attack.
            if (entity.Id == Game.Player.Id)
            {
                if (entity.HealthPercentage < Settings.GentleEchoHPLow && Helper.HelperFunction.CheckAvailable("Gentle Echo"))
                {
                    Helper.HelperFunction.CheckExecute("Gentle Echo", entity);
                    return false;
                }
            }
            // Check if we should heal the provided entity.
            else if (entity.HealthPercentage < Settings.GentleEchoHPLow && Helper.HelperFunction.CheckAvailable("Gentle Echo"))
            {
                Helper.HelperFunction.CheckExecute("Gentle Echo", entity);
                return false;
            }


            // Heal: Rejuvenation Melody
            if (entity.HealthPercentage < Settings.RejuvenationMelodyHPLow && entity.StateList.GetState("Rejuvenation Melody") == null && Helper.HelperFunction.CheckAvailable("Rejuvenation Melody"))
            {
                Helper.HelperFunction.CheckExecute("Rejuvenation Melody", entity);
                return false;
            }
            */


            // Return true to let the caller know this function completed.
            return true;
        }

        /// <summary>
        /// Checks if the state of the provided entity.
        /// </summary>
        /// <param name="entity">Contains the entity to check.</param>
        /// <returns></returns>
        private bool CheckMana(Entity entity)
        {
            // Retrieve the range of the entity compared to my own character position.
       /*     double range = Game.Player.Position.DistanceToPosition(entity.Position);

            // Check if this routine is allowed to be ran under the current circumstances.
            if (entity.IsDead || (AllowApproach == false && range > 23))
            {
                return true;
            }

            // 
            var entityState = entity.StateList;

            // Complete the chain
            if (Game.Player.ManaPercentage < 90 && Helper.HelperFunction.CheckAvailable("Variation of Peace"))
            {
                Helper.HelperFunction.CheckExecute("Variation of Peace", entity);
                return false;
            }

            // Check if the entity requires healing and perform the correct mana healing routine.
            if (entity.ManaPercentage < 50 && (AllowApproach || range <= 23))
            {
                if (entity.ManaPercentage < 45 && Helper.HelperFunction.CheckAvailable("Echo of Clarity"))
                {
                    Helper.HelperFunction.CheckExecute("Echo of Clarity", entity);
                    return false;
                }
            }

            if (entity.ManaPercentage < 50 && Helper.HelperFunction.CheckAvailable("Melody of Reflection"))
            {
                Helper.HelperFunction.CheckExecute("Melody of Reflection", entity);
                return false;
            }*/

            // Return true to let the caller know this function completed.
            return true;
        }

        private bool CheckState(Entity entity)
        {
            // Retrieve the range of the entity compared to my own character position.
            double range = Game.Player.Position.DistanceToPosition(entity.Position);

            // Check if this routine is allowed to be ran under the current circumstances.
            if (entity.IsDead || (AllowApproach == false && range > 23))
            {
                return true;
            }

            // 
            var entityState = entity.StateList;

            // Loop through the states only when we are available to dispel them. We still check for removed states!
            if (entityState != null && StateDispelTime < DateTime.Now)
            {
            }
            // Return true to let the caller know this function completed.
            return true;
        }

        private bool SleepMultipleAttacker(Entity entityTarget, double attackRange)
        {
        /*    // Check if we have stored a target.
            if (_SleepTarget == null)
            {
                // Check if the current target is the stored target.
                if (_SleepTarget.Id == Game.Player.TargetId)
                {
                    // Check if Sleep Arrow is available.
                    if (Helper.HelperFunction.CheckAvailable("March of the Jester"))
                    {
                        // Shoot the Sleep Arrow.
                        Helper.HelperFunction.CheckExecute("March of the Jester");

                        // Indicate we cannot continue attacking.
                        return false;
                    }
                    else
                    {
                        // Set the target.
                        Game.Player.SetTarget(_SleepTargetRestore);

                        // Indicate we cannot continue attacking.
                        return false;
                    }
                }
                else if (Helper.HelperFunction.CheckAvailable("March of the Jester") == false && _SleepTargetRestore.Id == entityTarget.Id)
                {
                    // Clear the sleep target.
                    _SleepTarget = null;

                    // Indicate we cannot continue attacking.
                    return true;
                }
                else
                {
                    // Set the target.
                    Game.Player.SetTarget(_SleepTarget);

                    // Indicate we cannot continue attacking.
                    return false;
                }
            }

            // Check if Sleep Arrow is available.
            if (Helper.HelperFunction.CheckAvailable("March of the Jester"))
            {
                // Loop through the entities.
                foreach (var tempEntity in Game.EntityList.GetList())
                {
                    Entity e = tempEntity.Value;
                    // Check if this entity is a monster, is not friendly and decided to attack me (and obviously is not my current target).
                    if (e.IsDead == false && e.IsMonster && e.IsFriendly == false && e.TargetId == Game.Player.Id && e.Id != entityTarget.Id)
                    {
                        // Check if the entity that is attacking us is within range.
                        if (e.Position.DistanceToPosition(Game.Player.Position) <= attackRange)
                        {
                            // Store the sleep target.
                            _SleepTarget = e;

                            // Store the restore target.
                            _SleepTargetRestore = entityTarget;

                            // Set the target.
                            Game.Player.SetTarget(e);

                            // Indicate we cannot continue attacking.
                            return false;
                        }
                    }
                }
            }*/

            // Indicate we can continue attacking.
            return true;
        }

        private bool TargetBuff(Entity entity)
        {
        /*    var entityState = entity.StateList;

            if (entityState != null && entity.IsDead == false)
            {
                uint improvedBlessingOfStoneId = 6156; // (Improved) Blessing of Stone

                if (entityState.GetState("Melody of Life") == null
                    && entityState.GetState("Blessing of Rock") == null
                    && entityState.GetState("Blessing of Stone") == null
                    && entityState.GetState("Prayer of Protection") == null
                    && entityState.GetState(improvedBlessingOfStoneId) == null
                    && Helper.HelperFunction.CheckAvailable("Melody of Life"))
                {
                    Helper.HelperFunction.CheckExecute("Melody of Life", entity);
                    return false;
                }
            }*/
            return true;
        }

        public override bool NoGravityAntiStuck()
        {
            return true;
        }
    }
}
