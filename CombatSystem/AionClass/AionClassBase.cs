using AionBotnet.AionGame;
using AionBotnet.AionGame.UnknownFramework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass
{
    public abstract class AionClassBase
    {
        public bool WaitAutoAttack { get; set; }
        public eAionClass CurrentAionClass { get; set; }
        public abstract bool Attack(Entity entity, float range);
        public abstract bool Heal();
        public abstract bool Pause();
        public abstract bool PartyCheck();
        public abstract bool NoGravityAntiStuck();

        public AionClassBase()
        {
            WaitAutoAttack = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="skills"></param>
        /// <returns>Return a tuple where first value indica se la skill è stata eseguita oppure no e il Second value contain the skill name</returns>
        protected Tuple<bool, string> ExecuteSkillFromList(Entity entity, string[] skills)
        {
            foreach (var skill in skills)
            {
                if (HelperFunction.CheckAvailable(skill))
                {
                    System.Diagnostics.Trace.WriteLine("Skill " + skill);
                    var result = HelperFunction.CheckExecute(skill, entity);
                    return new Tuple<bool, string>(false, skill);
                }
            }
            return new Tuple<bool, string>(true, "");
        }

        protected bool CheckAndExecuteSkills(Entity entity, string[] skillsName, int maxLevel = 10)
        {
            string[] level = new string[] { "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };

            for (int skillCount = 0; skillCount < skillsName.Length; skillCount++)
            {
                for (int i = level.Length - maxLevel; i < level.Length; i++)
                {
                    string tempSkillName =skillsName[skillCount] + " " + level[i];
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(tempSkillName))
                    {
                        Game.WriteDebugMessage("execute skill: " + tempSkillName);
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(tempSkillName, entity);
                        return true;
                    }
                }
            }


            return false;
        }

        protected bool CheckAndExecuteSkills(Entity entity, string skillsName, int maxLevel = 10)
        {
            return CheckAndExecuteSkills(entity, new string[] { skillsName }, maxLevel);
        }

        protected bool CheckAndExecuteSkills(Entity entity, Dictionary<string, int> skills)
        {
            string[] level = new string[] { "X", "IX", "VIII", "VII", "VI", "V", "IV", "III", "II", "I" };

            foreach (var skill in skills)
            {
                for (int i = level.Length - skill.Value; i < level.Length; i++)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(skill.Key + " " + level[i]))
                    {
                        Game.WriteDebugMessage("execute skill: " + skill.Key + " " + level[i]);
                        AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(skill.Key + " " + level[i], entity);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Controlla se il timer passato come primo parametro è maggiore del secondo
        /// </summary>
        /// <param name="dt1"></param>
        /// <param name="dt2"></param>
        /// <returns></returns>
        protected bool TimerIsUp(DateTime dt1, DateTime dt2)
        {
            if ((dt1 - dt2).TotalMilliseconds < 0)
                return true;

            return false;
        }

        public static int CountMods(float maxDistance)
        {
            int result = 0;

            var targetEntity = Game.EntityList.GetEntity(Game.Player.TargetId);

            if (targetEntity == null)
                return 0;

            // Check if our target is in range
            if (targetEntity != null && Game.Player.Position.Distance(targetEntity.Position) > maxDistance)
                return 0;
            

            foreach (var entity in Game.EntityList.GetList())
            {
                var e = entity.Value;

                if (e.Id != Game.Player.Id && e.IsFriendly == false && e.IsPlayer == false && e.IsPet == false && e.IsMinion == false && Game.Player.Position.Distance(e.Position) <= maxDistance)
                {
                 //   Game.WriteMessage(e.Name + " d:" + Game.Player.Position.Distance(e.Position));
                    result++;
                }
            }
            return result;
        }

        protected void SetWaitingAttack(bool wait)
        {
            WaitAutoAttack = wait;
        }

        protected bool WaitingAutoAttack(float cancellingTimePercentage = 0.5f)
        {
            if (WaitAutoAttack)
            {
                Game.Player.UpdateAutoAttackData();
                Game.WriteDebugMessage("Wait auto attack");
                //     Game.WriteMessage(Game.Player.AttackStatus);
                if (Game.Player.IsAutoAttacking && Game.Player.CurrentAnimationTime < (Game.Player.MaxAnimationTimer * cancellingTimePercentage))
                {
                    SetWaitingAttack(false);
                }
                Game.PlayerInput.Console(Game.SkillCommand + Game.AttackChatCommand);
                //AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(Game.AttackChatCommand);
                return true;
            }

            return false;
        }
    }
}
