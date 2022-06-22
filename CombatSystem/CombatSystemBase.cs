using AionBotnet.AionGame.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AionBotnet.AionGame.UnknownFramework.Helper;
using AionBotnet.AionGame;
using AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem.AionClass;

namespace AionBotnet.ScriptLibrary.AionClassic.Include.CombatSystem
{
    public enum GrinderBaseResult
    {
        Fighting,
        Looting,
        Nothing,
        Party,
        Resting
    }
    public class CombatSystemBase
    {

        protected AionClassBase ClassController;
        protected bool needLoot = false;
        protected bool needResting = false;
        protected bool needRestingFly = false;

        // Loot variable
        protected int NumberOfLoot = 20;
        protected DateTime needLootTimer = DateTime.MinValue;
        protected bool iWasLooting = false;
        protected ulong _waitDelayLootLastTimeSkill = 0;

        protected ulong _lootLastTime = 0;
        protected Vector3D _lootLastPosition = null;

        // A method to try to unstuck the rare bug where you can be stucked when loot mob
        public bool UseAntistuckLootBug { get; set; }

        /// <summary>
        /// Tempo di attesa prima di ricontrollare nuovamente lo stato del resting
        /// </summary>
        protected ulong RestingTimerDelay { get; set; }

        /// <summary>
        /// Wait SetMove command is completed
        /// </summary>
        public bool WaitSetMoveCommand { get; set; }

        public bool AllowPartyCheck { get; set; }
        public uint RestingHpUpTo { get; set; }
        public uint RestingMpUpTo { get; set; }
        public uint RestingFlyUpTo { get; set; }

        // Totally ignore this entity
        private List<uint> TotallyEntityTypeIdToIgnore;
        private List<string> TotallyEntityNameToIgnore;

        // Exclude from seatch
        private List<uint> EntityTypeIdToIgnore;
        private List<string> EntityNameToIgnore;

        private Vector3D positionBeforeFight = null;
        private Stack<Vector3D> stackPositionBeforeFight = new Stack<Vector3D>();

        // Valid target variables. <entity id, dequeue timer>
        private Queue<KeyValuePair<uint, DateTime>> queueInvalidEntityId = new Queue<KeyValuePair<uint, DateTime>>();
        private Queue<KeyValuePair<string, DateTime>> queueInvalidEntityName = new Queue<KeyValuePair<string, DateTime>>();
        private DateTime validTargetTime = DateTime.MinValue;
        private uint validTargetId = 0;
        private DateTime selectEntityTime = DateTime.MinValue;

        /// <summary>
        /// Skip target if can't attack him for a while
        /// </summary>
        public uint SkipTargetAfterSeconds { get; set; }

        /// <summary>
        /// Execute the method "Pause" only when attack mob
        /// </summary>
        public bool ExecutePauseActionOnlyInFight { get; set; }
        public ulong UsePauseHealDelayAfterLoot { get; set; }

        // 
        public CombatSystemSettingBase Settings { get; set; }
        public AionClassBaseSetting ClassSettings { get; set; }
        private AionClassFactory AionClassCreator { get; set; }

        public CombatSystemBase(CombatSystemSettingBase settings) : this(settings, new AionClassCreatorDefaultFactory())
        {

        }

        public CombatSystemBase(CombatSystemSettingBase settings, AionClassFactory classFactory)
        {
            Settings = settings;
            AionClassCreator = classFactory;
            WaitSetMoveCommand = false;
            AllowPartyCheck = true;
            SkipTargetAfterSeconds = 10;

            ExecutePauseActionOnlyInFight = false;
            UsePauseHealDelayAfterLoot = 0;
            UseAntistuckLootBug = false;


            RestingTimerDelay = 0;
            RestingHpUpTo = 100;
            RestingMpUpTo = 100;
            RestingFlyUpTo = 100;

            //
            TotallyEntityTypeIdToIgnore = new List<uint>();
            TotallyEntityNameToIgnore = new List<string>();

            //
            EntityTypeIdToIgnore = new List<uint>();
            EntityNameToIgnore = new List<string>();

            // Extract name
            if (String.IsNullOrWhiteSpace(settings.IgnoreEnitityName) == false)
            {
                string[] splitsResult = settings.IgnoreEnitityName.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tempName in splitsResult)
                {
                    int tempEntityId = 0;
                    string tempentityName = tempName.Trim();
                    if (Int32.TryParse(tempentityName, out tempEntityId))
                    {
                        EntityTypeIdToIgnore.Add((uint)tempEntityId);
                    }
                    else
                    {
                        EntityNameToIgnore.Add(tempentityName);
                    }
                }
            }

            // Totally ignore entity. Extract name
            if (String.IsNullOrWhiteSpace(settings.TotallyIgnoreEnitityName) == false)
            {
                string[] splitsResult = settings.TotallyIgnoreEnitityName.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var tempName in splitsResult)
                {
                    int tempEntityId = 0;
                    string tempentityName = tempName.Trim();
                    if (Int32.TryParse(tempentityName, out tempEntityId))
                    {
                        TotallyEntityTypeIdToIgnore.Add((uint)tempEntityId);
                    }
                    else
                    {
                        TotallyEntityNameToIgnore.Add(tempentityName);
                    }
                }
            }

            OnLoad();
        }
        public void OnClose()
        {
            Game.Player.SetMove(null);
        }

        public void OnLoad()
        {
            // Attack controller
            if (AionClassCreator is AionClassCreatorDefaultFactory)
            {
                var tempSettings = Settings as CombatSystemDefaultSetting;
                switch (Game.Player.Class)
                {
                    case eClass.Warrior:
                        ClassSettings = tempSettings.WarriorSettings;
                        break;
                    case eClass.Gladiator:
                        ClassSettings = tempSettings.GladiatorSettings;
                        break;
                    case eClass.Templar:
                        ClassSettings = tempSettings.TemplarSettings;
                        break;
                    case eClass.Scout:
                        ClassSettings = tempSettings.ScoutSettings;
                        break;
                    case eClass.Assassin:
                        ClassSettings = tempSettings.AssassinSettings;
                        break;
                    case eClass.Ranger:
                        ClassSettings = tempSettings.RangerSettings;
                        break;
                    case eClass.Mage:
                        ClassSettings = tempSettings.MageSettings;
                        break;
                    case eClass.Sorcerer:
                        ClassSettings = tempSettings.SorcererSettings;
                        break;
                    case eClass.Spiritmaster:
                        ClassSettings = tempSettings.SpiritmasterSettings;
                        break;
                    case eClass.Priest:
                        ClassSettings = tempSettings.PriestSettings;
                        break;
                    case eClass.Cleric:
                        ClassSettings = tempSettings.ClericSettings;
                        break;
                    case eClass.Chanter:
                        ClassSettings = tempSettings.ChanterSettings;
                        break;
                    case eClass.Technist:
                        ClassSettings = tempSettings.TechnistSettings;
                        break;
                    case eClass.Aethertech:
                        ClassSettings = tempSettings.AethertechSettings;
                        break;
                    case eClass.Gunslinger:
                        ClassSettings = tempSettings.GunslingerSettings;
                        break;
                    case eClass.Muse:
                        ClassSettings = tempSettings.MuseSettings;
                        break;
                    case eClass.Songweaver:
                        ClassSettings = tempSettings.SongweaverSettings;
                        break;
                    case eClass.Painter:
                        ClassSettings = tempSettings.PainterSettings;
                        break;
                    case eClass.Unknown_01:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ClassSettings = AionClassCreator.GetClassSetting(Game.Player.Class);
            }


            ClassController = AionClassCreator.GetAionClass(Game.Player.Class, ClassSettings);

            Init();
        }

        private void Init()
        {
            queueInvalidEntityId = new Queue<KeyValuePair<uint, DateTime>>();
            queueInvalidEntityName = new Queue<KeyValuePair<string, DateTime>>();
            validTargetTime = DateTime.MinValue;
            validTargetId = 0;
            selectEntityTime = DateTime.MinValue;
        }

        public GrinderBaseResult ScriptRoutine()
        {
            // Actual entity we are targeting
            Entity ActualEntity = Game.EntityList.GetEntity(Game.Player.TargetId);


            // Last time we skill
            if (Game.Player.CurrentSkillId != 0)
            {
                Game.WriteDebugMessage("CombatSystemBase CurrentSkillId: " + Game.Player.CurrentSkillId);
                _waitDelayLootLastTimeSkill = Game.Time();
            }

            // Loot
            if (IsLootEnable() && ActualEntity != null && ActualEntity.IsDead && needLootTimer != DateTime.MinValue)
            {
                _lootLastTime = Game.Time();
                _lootLastPosition = Game.Player.Position;

                // If we skill some instant before
                if (_waitDelayLootLastTimeSkill + 800 > Game.Time())
                {
                    return GrinderBaseResult.Looting;
                }

                UsePauseHealDelayAfterLoot = Game.Time();

                iWasLooting = true;
                if (needLootTimer >= DateTime.Now)
                {
                    // Confirm loot
                    var confirmDlg = AionGame.UnknownFramework.Helper.HelperFunction.GetConfirmDialog();
                    if (confirmDlg != null && confirmDlg.IsVisible() && confirmDlg.IsEnabled())
                    {
                        confirmDlg.GetDialog("ok").Click();
                        return GrinderBaseResult.Looting;
                    }
                    // ActualEntity.SetPosition(Game.Player.GetPosition());
                    //                     if (Game.Offsets.AionClientServerPublisher == AionClientServerPublisherType.AionEmpire || Game.Offsets.AionVersion == AionVersion.V12 || Game.Offsets.AionVersion == AionVersion.V19 || Game.Offsets.AionVersion == AionVersion.V21)
                    //                     {
                    //                         // Try to use take_all button after a while
                    //                         if ((needLootTimer - DateTime.Now).TotalSeconds < 5)
                    //                         {
                    //                             var tempTakeAllButton = Game.DialogList.GetDialog("loot_dialog/takeall_button");
                    //                             if (tempTakeAllButton != null)
                    //                             {
                    //                                 Game.WriteDebugMessage("Click \"Take All\" button");
                    //                                 tempTakeAllButton = tempTakeAllButton.Update(true, true);
                    //                                 tempTakeAllButton.Click();
                    //                                 System.Threading.Thread.Sleep(400);
                    //                             }
                    //                         }
                    // 
                    //                         Game.PlayerInput.Ability("Loot");
                    //                         System.Threading.Thread.Sleep(1000);
                    //                     }
                    //  else
                    {
                        Game.PlayerInput.SendKey(System.Windows.Forms.Keys.C);
                    }
                    //   System.Diagnostics.Trace.WriteLine(" loot");


             //       System.Threading.Thread.Sleep(600);

                    NumberOfLoot = NumberOfLoot + 1;
                    return GrinderBaseResult.Looting;
                }
                else if (needLootTimer < DateTime.Now)
                {
                    needLoot = false;
                    needLootTimer = DateTime.MinValue;
                    NumberOfLoot = 0;

                    Game.Player.SetMove(null);
                    System.Threading.Thread.Sleep(100);
                    Game.Player.SetMove(null);
                    System.Threading.Thread.Sleep(100);

                    Game.Player.SetTarget(Game.Player);
                    System.Threading.Thread.Sleep(100);

                    // Unselec mob
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(200);
                    Game.PlayerInput.Escape();
                    Game.Player.SetTarget(Game.Player);
                    System.Threading.Thread.Sleep(200);
                    Game.PlayerInput.Escape();

                    //                     while (!AionGame.UnknownFramework.Helper.HelperFunction.CloseUnnecessaryDialog())
                    //                     {
                    //                         Game.WriteDebugMessage("Stop loot. Close unnecessary windows");
                    //                     }
                }

                return GrinderBaseResult.Looting;
            }
            else if (IsLootEnable() == false)
            {
                var tempLootDlg = Game.DialogList.GetDialog("loot_dialog").Update(false, false);
                if (tempLootDlg != null && tempLootDlg.IsVisible())
                {
                    _lootLastTime = Game.Time();
                    _lootLastPosition = Game.Player.Position;
                    iWasLooting = true;

                    Game.Player.SetMove(null);
                    System.Threading.Thread.Sleep(100);
                    Game.Player.SetMove(null);
                    System.Threading.Thread.Sleep(100);

                    Game.Player.SetTarget(Game.Player);

                    // Unselec mob
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    System.Threading.Thread.Sleep(100);
                    Game.PlayerInput.Escape();
                    Game.WriteDebugMessage("Loot is disabled. Dialog loot detected. Stop loot.");
                }
            }

            if (UseAntistuckLootBug && iWasLooting)
            {
                // If resting
                if (Game.Player.IsResting)
                    iWasLooting = false;


                float distToLootCorpse = Game.Player.Position.Distance(_lootLastPosition);
                if (distToLootCorpse < 0.5f)
                {
                    // 
                    if (Game.Player.CurrentSkillId != 0)
                    {
                        Game.WriteDebugMessage("I was Looting. We are using skill, skip.");
                        iWasLooting = false;
                    }

                    Game.WriteDebugMessage("I was Looting.");
                    if (Game.Time() > _lootLastTime + 4000 && Game.Time() > _waitDelayLootLastTimeSkill + 4000)
                    {
                        Game.Process.SetUnsignedInteger(Game.Player.GetAddress() + 0xaa8, 0);
                        Game.Process.SetUnsignedInteger(Game.Player.GetAddress() + 0xaac, 0);

                        Game.Player.SetAction(eAction.MoveForward);
                        System.Threading.Thread.Sleep(100);

                        Game.Player.SetAction(eAction.None);
                        System.Threading.Thread.Sleep(100);

                        Game.Player.SetAction(eAction.MoveForward);
                        System.Threading.Thread.Sleep(100);

                        Game.Player.SetMove(null);
                        System.Threading.Thread.Sleep(100);

                        Game.Player.SetMove(null);
                        System.Threading.Thread.Sleep(100);

                        Game.Player.SetMove(null);
                        System.Threading.Thread.Sleep(100);


                        Game.WriteDebugMessage("\"I was Looting\" is active. Try to move forward.");
                        //  iWasLooting = false;
                        //    return GrinderBaseResult.Looting;
                    }



                    Game.WriteDebugMessage("\"I was Looting is active\". Dista: " + distToLootCorpse + ".Wait time elapsed");
                    //    iWasLooting = false;
                    //    return GrinderBaseResult.Looting;
                }
                else
                {
                    iWasLooting = false;
                }
            }

            #region Potion and Scroll section

            // Wind serum
            if (ClassSettings.UseWindSerum && Game.Player.IsFlying)
            {
                Include.PotionHelper.CheckWindSerum();
            }

            // "Recovery Potion" and "Recovery Serum"
            if (ClassSettings.PotionSerumBelowPercentage > 0 && (Game.Player.HealthPercentage < ClassSettings.PotionSerumBelowPercentage || Game.Player.ManaPercentage < ClassSettings.PotionSerumBelowPercentage))
            {
                Include.PotionHelper.CheckRecoverySerum();
            }

            if (ClassSettings.PotionRecoveryBelowPercentage > 0 && (Game.Player.HealthPercentage < ClassSettings.PotionRecoveryBelowPercentage || Game.Player.ManaPercentage < ClassSettings.PotionRecoveryBelowPercentage))
            {
                Include.PotionHelper.CheckRecoveryPotion();
            }

            // "Life Potion" and "Mana Potion"
            if (Game.Player.HealthPercentage < ClassSettings.LifePotionBelowPercentage)
            {
                Include.PotionHelper.CheckLifePotion();
            }

            if (Game.Player.ManaPercentage < ClassSettings.ManaPotionBelowPercentage)
            {
                Include.PotionHelper.CheckManaPotion();
            }

            // "Life Serum" and "Mana Serum"
            if (Game.Player.HealthPercentage < ClassSettings.LifeSerumPotionBelowPercentage)
            {
                Include.PotionHelper.CheckLifeSerum();
            }

            if (Game.Player.ManaPercentage < ClassSettings.ManaSerumPotionBelowPercentage)
            {
                Include.PotionHelper.CheckManaSerum();
            }

            // Elixir
            if (Game.Player.HealthPercentage < ClassSettings.LifeElixirPotionBelowPercentage)
            {
                Include.PotionHelper.CheckLifeElixir();
            }

            if (Game.Player.ManaPercentage < ClassSettings.ManaElixirPotionBelowPercentage)
            {
                Include.PotionHelper.CheckManaElixir();
            }

            // Panacea
            if (Game.Player.HealthPercentage < ClassSettings.LifePanaceaPotionBelowPercentage)
            {
                Include.PotionHelper.CheckLifePanacea();
            }

            if (Game.Player.ManaPercentage < ClassSettings.ManaPanaceaPotionBelowPercentage)
            {
                Include.PotionHelper.CheckManaPanacea();
            }

            // Rally Serum
            if (ClassSettings.UseRallySerum)
            {
                Include.ItemBuffHelper.CheckRallySerum();
            }

            // Rally Serum
            if (ClassSettings.UseFocusAgent)
            {
                Include.ItemBuffHelper.CheckFocusAgent();
            }


            // Scroll
            if (ClassSettings.UseScrollAttackSpeed)
            {
                Include.ItemBuffHelper.CourageScrollIScroll();
            }

            if (ClassSettings.UseScrollCastingSpeed)
            {
                Include.ItemBuffHelper.CheckAwakeningScroll();
            }

            if (ClassSettings.UseScrollMovementSpeed)
            {
                Include.ItemBuffHelper.CheckRunningScroll();
            }

            if (ClassSettings.UseScrollCriticalStrike)
            {
                Include.ItemBuffHelper.CheckCriticalStrikeScroll();
            }

            if (ClassSettings.UseScrollCriticalSpell)
            {
                Include.ItemBuffHelper.CheckCriticalSpellScroll();
            }
            #endregion


            if (Game.Time() > UsePauseHealDelayAfterLoot + 1000 && ExecutePauseActionOnlyInFight == false && Game.Player.IsMoving() == false && Game.Player.IsResting == false)
            {
                if (ClassController.Heal() == false)
                {
                    _waitDelayLootLastTimeSkill = Game.Time();
                    return GrinderBaseResult.Party;
                }

                if (ClassController.Pause() == false)
                {
                    _waitDelayLootLastTimeSkill = Game.Time();
                    return GrinderBaseResult.Party;
                }

                if (AllowPartyCheck && ClassController.PartyCheck() == false)
                {
                    _waitDelayLootLastTimeSkill = Game.Time();
                    return GrinderBaseResult.Party;
                }
            }




            if (Game.Player.IsMoving() && WaitSetMoveCommand)
            {
                var MoveTarget = Game.Player.GetMove();
                //   Game.Write("Game.Player.IsMoving()" + fff++);
                if (MoveTarget == null || Game.Player.Position.DistanceToPosition(MoveTarget) > 2)
                {
                    var sMoveTarget = MoveTarget == null ? "MoveTarget == null" : "Dist > 1";
                    //  if(isMoveWrite)
                    //   Game.Write("return" + mmm++);
                    Game.WriteDebugMessage("dist " + Game.Player.Position.DistanceToPosition(MoveTarget));
                    return GrinderBaseResult.Nothing;
                }
            }

            // If someone focus us then stop character movement
            var tempOurTargetEntityStopMove = Game.EntityList.GetEntity(Game.Player.TargetId);
            var tempEntityFocusUsStopMove = FindTarget(true, 20);// ClassSettings.TargetSearchDistance
            if ((tempOurTargetEntityStopMove != null && tempOurTargetEntityStopMove.Id != Game.Player.Id && tempOurTargetEntityStopMove.IsHostile && tempOurTargetEntityStopMove.IsDead == false) || (tempEntityFocusUsStopMove != null && tempEntityFocusUsStopMove.IsFriendly == false && tempEntityFocusUsStopMove.IsDead == false))
            {
                Game.Player.SetMove(null);
            }


            Entity entity = null;
            if (ActualEntity != null && ActualEntity.IsObject == false && ActualEntity.IsDead == false && !ActualEntity.IsPet && ActualEntity.IsFriendly == false && ActualEntity.Attitude != eAttitude.Utility && (IsEntityToTotallyIgnoreTypeId(ActualEntity.TypeId) == false && IsEntityToTotallyIgnoreName(ActualEntity.Name) == false))
            {
                entity = ActualEntity;
            }
            else if (ActualEntity == null || (ActualEntity != null && ActualEntity.IsDead && ActualEntity.IsObject == false) || (ActualEntity != null && ActualEntity.IsObject == false && (ActualEntity.IsPlayer || ActualEntity.IsPet || ActualEntity.IsMinion || ActualEntity.IsSpirit) || (ActualEntity != null && (IsEntityToTotallyIgnoreTypeId(ActualEntity.TypeId) || IsEntityToTotallyIgnoreName(ActualEntity.Name)))))
            {
                entity = FindTarget(true, ClassSettings.TargetSearchDistance);

                // Search our target in aggressive way (do not do this if need to rest)
                if (entity == null && IsAggressiveSearchEnable() && needResting == false && needRestingFly == false)
                {
                    // Avoid to use aggressive seach if need to use Treatment
                    bool canUseManaTreatment = Game.Player.ManaPercentage <= ClassSettings.ManaTreatmentBelowPercentage && PotionHelper.CanUseManaTreatment();
                    bool canUseHerbTreatment = Game.Player.HealthPercentage <= ClassSettings.HerbTreatmentBelowPercentage && PotionHelper.CanUseHerbTreatment();

                    if (!canUseManaTreatment && !canUseHerbTreatment)
                    {
                        entity = FindTarget(false, ClassSettings.TargetSearchDistance);
                    }
                }
            }


            // Check if need to resting HP or MP
            if (needResting == false)
            {
                needResting = false;
                bool restConditionHP = Game.Player.HealthPercentage < ClassSettings.RestHealth;
                bool restConditionMP = Game.Player.ManaPercentage < ClassSettings.RestMana;
                if ((restConditionHP || restConditionMP) && ClassController.CurrentAionClass != eAionClass.Songweaver)
                {
                    //  Game.WriteMessage("Resting active. HP cond:"+restConditionHP+ " - PM cond:"+restConditionMP);
                    needResting = true;
                }
            }


            // Fight
            if (entity != null && IsEntityToTotallyIgnoreTypeId(entity.TypeId) == false && IsEntityToTotallyIgnoreName(entity.Name) == false)
            {
                // Select entity
                if (Game.Player.TargetId != entity.Id)
                {
                    var tempSetMove = Game.Player.SetMove(null);
                    Game.Player.UpdateMove();
                    //     if (tempSetMove == false)
                    {
                        //     Game.Player.SetAction(eAction.None);
                        Game.Process.SetByte(Game.Player.GetAddress() + Game.Offsets.EntityOffsetList.Action, (byte)eAction.None);
                        //      return GrinderBaseResult.Fighting;
                    }

                    positionBeforeFight = Game.Player.Position;
                    Game.Player.SetTarget(entity);

                    // Set maximum timer to select target
                    if (selectEntityTime == DateTime.MinValue)
                    {
                        selectEntityTime = DateTime.Now.AddSeconds(3);
                    }
                    else if (selectEntityTime < DateTime.Now) // Abbiamo impiegato troppo tempo a selezionare l'entity. Ignoriamola
                    {
                        var dequeueTime = DateTime.Now.AddSeconds(10);
                        AddInvalidEntityToQueue(entity);
                        selectEntityTime = DateTime.MinValue;
                        //   Game.WriteMessage("Unselectable entity to queue" + " EntityName:" + entity.Name);
                    }
                    return GrinderBaseResult.Fighting;
                }
                else
                {
                    var action = Game.Process.GetByte(Game.Player.GetAddress(0) + Game.Offsets.EntityOffsetList.Action);
                    var tempGetMove = Game.Player.GetMove();
                    var tempIsMove = Game.Player.IsMoving();
                    if (tempGetMove != null || tempIsMove || action == 7 || action == 8 || action == 9 || action == 10)
                    {
                        string sGetMove = tempGetMove != null ? "GetMove no null - " : "";
                        string sIsMove = tempIsMove == true ? "IsMoving trur - " : "";


                        Game.WriteDebugMessage(sGetMove + sIsMove + "We are moving in fight action:" + action);

                        Game.Player.SetMove(null);
                        return GrinderBaseResult.Fighting;
                    }
                    if (tempGetMove == null && tempIsMove == false && (action == 7 || action == 8 || action == 9 || action == 10))
                    {
                        Game.Player.SetAction(eAction.None);
                    }
                    //                    Game.Player.SetMove(null);
                    bool isValidTarget = false;
                    selectEntityTime = DateTime.MinValue;

                    if (entity.TargetId == 0)
                    {
                        if (validTargetTime != DateTime.MinValue && validTargetId == entity.Id)
                        {
                            var playerSkillId = Game.Player.CurrentSkillId;

                            var skillInfo = Game.SkillList.GetSkill(playerSkillId);

                            if (skillInfo != null && playerSkillId != 0 && (skillInfo.IsAttack || (Game.Player.Class == eClass.Spiritmaster && skillInfo.IsPassive)))
                            {
                                isValidTarget = true;
                                validTargetTime = DateTime.Now.AddSeconds(SkipTargetAfterSeconds);
                            }

                        }
                        else // Abbiamo appena selezionato il target. Impostiamo i valori iniziali
                        {
                            isValidTarget = true;
                            validTargetId = entity.Id;
                            validTargetTime = DateTime.Now.AddSeconds(SkipTargetAfterSeconds);
                        }


                        if (isValidTarget == false)
                        {
                            // Time expired. Exclude entity
                            if (validTargetTime < DateTime.Now)
                            {
#if DEBUG
                                //        Game.WriteMessage("Add invalid entity to queue - " + entity.Name + " TypeID: " + entity.TypeId);
#endif
                                var dequeueTime = DateTime.Now.AddSeconds(10);
                                AddInvalidEntityToQueue(entity);
                                Game.Player.SetTarget(Game.Player);
                                validTargetId = 0;
                            }
                        }
                        else // Renew timer if we use a skill
                        {
                            validTargetTime = DateTime.Now.AddSeconds(SkipTargetAfterSeconds);
                        }
                    }
                    else
                    {
                        // Nel caso in cui il mob taghetti noi prima ancora che noi targhettiamo lui, imposta il ValidTargetID
                        if (validTargetId != entity.Id)
                        {
                            isValidTarget = true;
                            validTargetId = entity.Id;
                            validTargetTime = DateTime.Now.AddSeconds(SkipTargetAfterSeconds);
#if DEBUG
                            Game.WriteMessage("entity target!=0 Set valid target " + entity.Id);
#endif
                        }

                        var playerSkillId = Game.Player.CurrentSkillId;
                        var skillInfo = Game.SkillList.GetSkill(playerSkillId);

                        if ((skillInfo != null && playerSkillId != 0) || AionGame.UnknownFramework.Helper.SkillEffectList.WeAreUnderControl())// 
                        {
                            isValidTarget = true;
                            validTargetTime = DateTime.Now.AddSeconds(SkipTargetAfterSeconds);
                        }

                        if (validTargetTime != DateTime.MinValue && isValidTarget == false)
                        {
                            // Time expired. Exclude entity
                            if (validTargetTime < DateTime.Now)
                            {
#if DEBUG
                                //           Game.WriteMessage("Target !=0 - Add invalid entity to queue - " + entity.Name + " TypeID: " + entity.TypeId);
#endif
                                AddInvalidEntityToQueue(entity);
                                Game.Player.SetTarget(Game.Player);
                                validTargetId = 0;
                            }
                        }
                        else
                        {
                        }
                    }

                    // Check Reflect
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckReflect(entity))
                    {
                        Game.Player.SetTarget(Game.Player);
                        return GrinderBaseResult.Fighting;
                    }
                    needLootTimer = DateTime.Now.AddSeconds(15);

                    if (ClassController.Heal() == false)
                    {
                        _waitDelayLootLastTimeSkill = Game.Time();
                        return GrinderBaseResult.Party;
                    }

                    if (ClassController.Pause() == false)
                    {
                        _waitDelayLootLastTimeSkill = Game.Time();
                        return GrinderBaseResult.Party;
                    }

                    if (AllowPartyCheck && ClassController.PartyCheck() == false)
                    {
                        _waitDelayLootLastTimeSkill = Game.Time();
                        return GrinderBaseResult.Party;
                    }


                    //     Game.WriteMessage("attack");
                    ClassController.Attack(entity, (float)Game.Player.Position.DistanceToPosition(entity.Position));
                    _waitDelayLootLastTimeSkill = Game.Time();

                    if (entity != null)
                        Game.WriteDebugMessage("Attack entity: " + entity.Name);
                    NumberOfLoot = 0;
                }

                return GrinderBaseResult.Fighting;
            }

            // Treatment
            if (needResting == false && needRestingFly == false)
            {
                // Herb Treatment
                if (Game.Player.HealthPercentage <= ClassSettings.HerbTreatmentBelowPercentage)
                {
                    if (PotionHelper.CheckHerbTreatment())
                        return GrinderBaseResult.Fighting;
                }

                // Mana Treatment
                if (Game.Player.ManaPercentage <= ClassSettings.ManaTreatmentBelowPercentage)
                {
                    if (PotionHelper.CheckManaTreatment())
                        return GrinderBaseResult.Fighting;
                }
            }


            // Execute resting
            if ((needResting || needRestingFly) && Game.Player.IsFlying == false)
            {
                //
                if (Game.Time() - RestingTimerDelay <= 2000)
                {
                    return GrinderBaseResult.Resting;
                }

                // Start resting
                if (Game.Player.IsResting == false && (Game.Player.HealthPercentage < RestingHpUpTo || Game.Player.ManaPercentage < RestingMpUpTo || (needRestingFly && Game.Player.FlightTimePercentage < RestingFlyUpTo)))
                {
                    Game.Player.SetMove(null);
                    System.Diagnostics.Trace.WriteLine("combast system active resting");
                    Game.PlayerInput.Ability("Toggle Rest");
                    RestingTimerDelay = Game.Time();
                }

                // Stop resting
                else if (Game.Player.HealthPercentage >= RestingHpUpTo && Game.Player.ManaPercentage >= RestingMpUpTo && ((needRestingFly && Game.Player.FlightTimePercentage >= RestingFlyUpTo) || needRestingFly == false))
                {
                    if (Game.Player.IsResting)
                    {
                        Game.PlayerInput.Ability("Toggle Rest");
                        RestingTimerDelay = Game.Time();
                    }
                    else if (Game.Player.IsResting == false)
                    {
                        needResting = false;
                        needRestingFly = false;
                    }
                }

                return GrinderBaseResult.Resting;
            }


            // Check if need resting flytime
            needRestingFly = false;
            bool restConditionFlyTime = Game.Player.FlightTimePercentage < ClassSettings.RestFlight && Game.Player.IsFlying;
            if (restConditionFlyTime && ClassController.CurrentAionClass != eAionClass.Songweaver)
            {
                //    Game.WriteMessage("Resting active. Fly cond:" + restConditionFlyTime);
                needRestingFly = true;
            }

            return GrinderBaseResult.Nothing;
        }

        public void ForceResting()
        {
            needResting = true;
        }

        protected virtual bool IsAggressiveSearchEnable()
        {
            return Settings.AggressiveTargetSearch;
        }

        protected virtual bool IsLootEnable()
        {
            return Settings.AllowLoot;
        }

        /// <summary>
        /// This method return a list of entity type id to ignore.
        /// </summary>
        /// <returns></returns>
        protected virtual List<uint> DynamicEntityListToIgnoreTypeId()
        {
            return new List<uint>();
        }


        protected virtual List<string> DynamicEntityListToIgnoreName()
        {
            return new List<string>();
        }

        private Entity FindTarget(bool onlyAggressive, float maxDistance)
        {
            bool checkNext = true;
            while (checkNext && queueInvalidEntityId.Count > 0)
            {
                var current = queueInvalidEntityId.Peek();

                if (current.Value < DateTime.Now)
                {
                    queueInvalidEntityId.Dequeue();
                    checkNext = true;
                }
                else
                    checkNext = false;
            }

            checkNext = true;

            Entity result = null;
            double bestDistance = maxDistance;

            // Set 15meter to be sure to check entity focus us
            if (onlyAggressive)
                bestDistance = 15;

            // Loop through the available entities
            foreach (var entityKey in Game.EntityList.GetList())
            {
                Entity entity = entityKey.Value;

                // Se la differenza di quota è maggiore di un certo valore allora ignora compleamente il mob
                if (Math.Abs(entity.Position.Z - Game.Player.Position.Z) > 10)
                    continue;

                // Ignore pet
                if (entity.IsPet)
                    continue;

                double distance = Game.Player.Position.DistanceToPosition(entity.Position);

                Entity spiritEntity = null;

                if (Game.Player.Class == eClass.Spiritmaster)
                    spiritEntity = HelperFunction.FindSpiritmasterSpirit();


                // Check if this entity has a level higher than one or we are in Ishalgen / Poeta.
                bool entityLevelCondition = entity.Level > 1 || Game.Player.WorldId == 220010000 || Game.Player.WorldId == 210010000;
                bool mobTypeCondition = entity.IsFriendly == false && entity.IsObject == false && entity.IsMonster;
                bool petMinionCondition = entity.IsPet == false && entity.IsMinion == false;
                bool hideCondition = entity.IsHiddenStateActive == false || entity.Level < 10;

                if (entity.IsDead == false && mobTypeCondition && petMinionCondition && hideCondition && entity.Attitude != eAttitude.Utility && distance < bestDistance && entityLevelCondition)
                {
                    // Is an invalid entity
                    if (queueInvalidEntityId.Any(k => k.Key == entity.Id))
                        continue;

                    // Cerca tutte le entity
                    if (onlyAggressive)
                    {
                        if (IsEntityToTotallyIgnoreTypeId(entity.TypeId) || IsEntityToTotallyIgnoreName(entity.Name))
                            continue;

                        // Check if this monster has targeted me
                        if (entity.TargetId == Game.Player.Id || (spiritEntity != null && entity.TargetId == spiritEntity.Id))
                        {
                            result = entity;
                            bestDistance = distance;
                        }
                    }
                    else
                    {
                        if (IsEntityToIgnoreTypeId(entity.TypeId) || IsEntityToIgnoreName(entity.Name) || IsEntityToTotallyIgnoreTypeId(entity.TypeId) || IsEntityToTotallyIgnoreName(entity.Name))
                            continue;

                        var targetID = entity.TargetId;
                        if (targetID == 0 || targetID == entity.Id || targetID == Game.Player.Id || Game.PartyMemberList.GetMember(targetID) != null || (spiritEntity != null && targetID == spiritEntity.Id))
                        {
                            result = entity;
                            bestDistance = distance;
                        }
                    }
                }
            }

            return result;
        }

        public void AddEntityToTotallyIgnoreSeach(string entityName)
        {
            if (TotallyEntityNameToIgnore.Contains(entityName) == false)
                TotallyEntityNameToIgnore.Add(entityName);
        }

        public void AddEntityToTotallyIgnoreSeachTypeId(uint typeID)
        {
            if (TotallyEntityTypeIdToIgnore.Contains(typeID) == false)
                TotallyEntityTypeIdToIgnore.Add(typeID);
        }

        public void AddEntityToIgnoreSeach(string entityName)
        {
            if (EntityNameToIgnore.Contains(entityName) == false)
                EntityNameToIgnore.Add(entityName);
        }

        public void AddEntityToIgnoreSeachTypeId(uint typeID)
        {
            if (EntityTypeIdToIgnore.Contains(typeID) == false)
                EntityTypeIdToIgnore.Add(typeID);
        }

        private bool IsEntityToTotallyIgnoreTypeId(uint TypeID)
        {
            if (TotallyEntityTypeIdToIgnore.Contains(TypeID))
            {
                return true;
            }

            return false;
        }


        private bool IsEntityToTotallyIgnoreName(string entityName)
        {
            if (TotallyEntityNameToIgnore.Contains(entityName, StringComparer.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private bool IsEntityToIgnoreTypeId(uint TypeID)
        {
            if (EntityTypeIdToIgnore.Contains(TypeID))
            {
                return true;
            }

            // Dynamic entity to ignore
            if (DynamicEntityListToIgnoreTypeId().Contains(TypeID))
            {
                return true;
            }

            return false;
        }


        private bool IsEntityToIgnoreName(string entityName)
        {
            if (EntityNameToIgnore.Contains(entityName, StringComparer.InvariantCultureIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private bool AddInvalidEntityToQueue(Entity entity)
        {
            // Controlla che l'id di questa entity non sia già in lista
            if (queueInvalidEntityId.Where(tempItem => tempItem.Key == entity.Id).Count() == 0)
            {
                var dequeueTime = DateTime.Now.AddSeconds(10);
                queueInvalidEntityId.Enqueue(new KeyValuePair<uint, DateTime>(entity.Id, dequeueTime));
                //  queueInvalidEntityName.Enqueue(new KeyValuePair<string, DateTime>(entity.Name.ToLower(), dequeueTime));

                return true;
            }
            return false;
        }
    }
}
