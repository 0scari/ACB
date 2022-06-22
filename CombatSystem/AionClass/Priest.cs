using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public class PriestSettings : AionClassBaseSetting
    {
        /// <summary>
        /// Indicates whether or not party resurrection are allowed
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowPartyResurrection", "Allow party resurrection")]
        public bool AllowPartyResurrection { get; set; }


        /// <summary>
        /// Indicates whether or not party healing are allowed
        /// </summary>
        [DisplayNameDynamic(ScriptDirectory, "AionClassBaseSetting_AllowPartyHealing", "Allow party healing")]
        public bool AllowPartyHealing { get; set; }

        public PriestSettings() : base()
        {
            TargetSearchDistance = 10;
            AllowPartyResurrection = true;
            AllowPartyHealing = true;
        }
    }

    public class Priest : AionClassBase
    {
        private PriestSettings Settings { get; set; }

        public Priest()
        {
        }

        public Priest(PriestSettings settings)
        {
            this.Settings = settings;
            CurrentAionClass = eAionClass.Priest;
        }

        public override bool Attack(Entity entity, float range)
        {
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

            // Hallowed Strike
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Hallowed Strike I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Hallowed Strike I");
                return false;
            }

            // Infernal Blaze
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Infernal Blaze I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Infernal Blaze I");
                return false;
            }

            // Crush
            if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Smite I"))
            {
                WaitAutoAttack = true;
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Smite I");
                return false;
            }

            HelperFunction.CheckExecute(Game.AttackChatCommand);

            return true;
        }

        public override bool PartyCheck()
        {
            // Copy member list to new list
            List<Entity> memberList = new List<Entity>(Game.PartyMemberList.GetList().Where(w => w.Value.GetEntity() != null).Select(s => s.Value.GetEntity()));

            List<Entity> candidateMembers = new List<Entity>();
            int healingGroupBenefitCount = 0;

            // Loop tra i membri del gruppo e ordinali in base agli HP mancanti. Controlla se il membro del gruppo può essere avvantaggiato da una cura AoE
            foreach (var force in memberList)
            {
                if (force != null && force.IsDead == false && force.Position.DistanceToPosition(Game.Player.Position) < 20)
                {
                    candidateMembers.Add(force);

                    if ((force.HealthMaximum - force.HealthCurrent) > 2000)
                    {
                        healingGroupBenefitCount++;
                    }
                }

            }

            if (Settings.AllowPartyHealing)
            {
                // Order list by HP (less HP have priority)
                var hpOrder = candidateMembers.OrderBy(o => o.HealthPercentage).ToList();

                // Loop della list e inizia a curare i membri
                foreach (var item in hpOrder)
                {
                    if (CheckHeal(item) == false)
                        return false;
                }
            }

            // Buff force members
            foreach (var tempMember in candidateMembers)
            {
                if (tempMember.Position.DistanceToPosition(Game.Player.Position) < 20 && TargetBuff(tempMember) == false)
                    return false;
            }


            // Ress party member
            if (Settings.AllowPartyResurrection)
            {
                foreach (var tempMember in memberList)
                {
                    var memberEntity = Game.EntityList.GetEntity(tempMember.Id);
                    // If is death
                    if (memberEntity != null && memberEntity.Id != Game.Player.Id && memberEntity.IsDead && memberEntity.Position.DistanceToPosition(Game.Player.Position) < 20)
                    {
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Resurrection I"))
                        {
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Resurrection I", memberEntity);
                            return false;
                        }
                    }
                }
            }


            return true;
        }

        public override bool Heal()
        {
            if (!CheckHeal(Game.Player))
                return false;

            return true;
        }



        public override bool NoGravityAntiStuck()
        {
            return true;
        }

        public override bool Pause()
        {
            if (!TargetBuff(Game.Player))
                return false;

            return true;
        }


        private bool CheckHeal(Entity entity)
        {
            if (entity.HealthPercentage < 60 && entity.StateList.GetState(958) == null && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Light of Renewal I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Light of Renewal I", entity);
                return false;
            }

            if (entity.HealthPercentage < 60 && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable("Healing Light I"))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute("Healing Light I", entity);
                return false;
            }

            return true;
        }

        private bool TargetBuff(Entity entity)
        {
            // Blessing of Health I
            if (entity.StateList.GetState(951) == null && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(951))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(951, entity);
                return false;
            }

            // Blessing of Rock I
            if (entity.StateList.GetState(955) == null && AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(955))
            {
                AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(955, entity);
                return false;
            }

            return true;
        }
    }
}
