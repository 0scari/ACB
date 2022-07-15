using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include
{
    public class PotionItemInfo
    {
        public string Name_EU { get; set; }
        public string Name_RU { get; set; }
        public string Name_KR { get; set; }
        public uint TypeId { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int FlyTime { get; set; }

        public PotionItemInfo()
        {

        }
        public PotionItemInfo(string name_EU, string name_RU, string name_KR, uint typeId, int level, int hp, int mp)
        {
            Name_EU = name_EU;
            Name_RU = name_RU;
            Name_KR = name_KR;
            TypeId = typeId;
            Level = level;
            HP = hp;
            MP = mp;
        }
    }

    public class WindSerumItemInfo
    {
        public string Name_EU { get; set; }
        public string Name_RU { get; set; }
        public string Name_KR { get; set; }
        public uint TypeId { get; set; }
        public int Level { get; set; }
        public uint FlyTime { get; set; }

        public WindSerumItemInfo()
        {

        }
        public WindSerumItemInfo(string name_EU, string name_RU, string name_KR, uint typeId, int level, uint flyTime)
        {
            Name_EU = name_EU;
            Name_RU = name_RU;
            Name_KR = name_KR;
            TypeId = typeId;
            Level = level;
            FlyTime = flyTime;
        }
    }


    public class TreatmentItemInfo
    {
        public string Name_EU { get; set; }
        public string Name_RU { get; set; }
        public string Name_KR { get; set; }
        public int Level { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public uint OdellaTypeId { get; set; }

        public TreatmentItemInfo()
        {

        }
        public TreatmentItemInfo(string name_EU, string name_RU, string name_KR, int level, int hp, int mp, uint odellaTypeId)
        {
            Name_EU = name_EU;
            Name_RU = name_RU;
            Name_KR = name_KR;
            Level = level;
            HP = hp;
            MP = mp;
            OdellaTypeId = odellaTypeId;
        }
    }

    public class PotionSerumItemCooldownInfo
    {
        public uint TypeId { get; set; }
        public uint Cooldown { get; set; }
        public uint RecoveryFlyTimeAmount { get; set; }

        public PotionSerumItemCooldownInfo()
        {

        }
    }

    public static class PotionHelper
    {
        // Recover hp/mp
        public static List<PotionItemInfo> RecoveryPotions { get; set; }
        public static List<PotionItemInfo> RecoverySerum { get; set; }


        public static List<PotionItemInfo> LifePotions { get; set; }
        public static List<PotionItemInfo> ManaPotions { get; set; }
        public static List<PotionItemInfo> LifeElixirPotions { get; set; }
        public static List<PotionItemInfo> ManaElixirPotions { get; set; }


        public static List<PotionItemInfo> LifeSerum { get; set; }
        public static List<PotionItemInfo> ManaSerum { get; set; }
        public static List<PotionItemInfo> PanaceaOfLife { get; set; }
        public static List<PotionItemInfo> PanaceaOfMana { get; set; }

        // Wind Serum
        public static List<WindSerumItemInfo> WindSerum { get; set; }


        // Mana/HP Treatment
        public static List<TreatmentItemInfo> HerbTreatment { get; set; }
        public static List<TreatmentItemInfo> ManaTreatment { get; set; }

        static ulong PotionReuseDelay { get; set; }

        public static List<uint> HP_MP_PotionTypeIdListToCheckCooldown { get; set; }

        /// <summary>
        /// Check if lost HP\MP are at least the potion recovery amount
        /// </summary>
        static bool CheckMinimumPotionHPMP { get; set; }

        static PotionHelper()
        {
            // Recover hp/mp
            RecoveryPotions = new List<PotionItemInfo>();
            RecoveryPotions.Add(new PotionItemInfo() { Name_EU = "Major Recovery Potion", Name_RU = "", Name_KR = "", TypeId = 162000045, Level = 50, HP = 1540, MP = 1600 });
            RecoveryPotions.Add(new PotionItemInfo() { Name_EU = "Greater Recovery Potion", Name_RU = "", Name_KR = "", TypeId = 162000044, Level = 40, HP = 1270, MP = 1480 });
            RecoveryPotions.Add(new PotionItemInfo() { Name_EU = "Recovery Potion", Name_RU = "", Name_KR = "", TypeId = 162000043, Level = 30, HP = 970, MP = 1280 });
            RecoveryPotions.Add(new PotionItemInfo() { Name_EU = "Lesser Recovery Potion", Name_RU = "", Name_KR = "", TypeId = 162000042, Level = 20, HP = 670, MP = 980 });
            RecoveryPotions.Add(new PotionItemInfo("Minor Recovery Potion", "", "", 162000041, 10, 370, 590));


            RecoverySerum = new List<PotionItemInfo>();
            RecoverySerum.Add(new PotionItemInfo() { Name_EU = "Major Recovery Serum", Name_RU = "", Name_KR = "", TypeId = 162000050, Level = 50, HP = 1410, MP = 1470 });
            RecoverySerum.Add(new PotionItemInfo() { Name_EU = "Greater Recovery Serum", Name_RU = "", Name_KR = "", TypeId = 162000049, Level = 40, HP = 1160, MP = 1360 });
            RecoverySerum.Add(new PotionItemInfo() { Name_EU = "Recovery Serum", Name_RU = "", Name_KR = "", TypeId = 162000048, Level = 30, HP = 890, MP = 1170 });
            RecoverySerum.Add(new PotionItemInfo() { Name_EU = "Lesser Recovery Serum", Name_RU = "", Name_KR = "", TypeId = 162000047, Level = 20, HP = 610, MP = 900 });
            RecoverySerum.Add(new PotionItemInfo("Minor Recovery Serum", "", "", 162000046, 10, 340, 540));


            // Potions
            LifePotions = new List<PotionItemInfo>();
            LifePotions.Add(new PotionItemInfo() { Name_EU = "Major Life Potion", Name_RU = "", Name_KR = "", TypeId = 162000006, Level = 50, HP = 1540, MP = 0 });
            LifePotions.Add(new PotionItemInfo() { Name_EU = "Greater Life Potion", Name_RU = "", Name_KR = "", TypeId = 162000005, Level = 40, HP = 1270, MP = 0 });
            LifePotions.Add(new PotionItemInfo() { Name_EU = "Life Potion", Name_RU = "", Name_KR = "", TypeId = 162000004, Level = 30, HP = 970, MP = 0 });
            LifePotions.Add(new PotionItemInfo() { Name_EU = "Lesser Life Potion", Name_RU = "", Name_KR = "", TypeId = 162000003, Level = 20, HP = 670, MP = 0 });
            LifePotions.Add(new PotionItemInfo("Minor Life Potion", "", "", 162000002, 10, 370, 0));


            LifeElixirPotions = new List<PotionItemInfo>();
            LifeElixirPotions.Add(new PotionItemInfo() { Name_EU = "Major Life Elixir", Name_RU = "", Name_KR = "", TypeId = 162000056, Level = 50, HP = 1280, MP = 0 });
            LifeElixirPotions.Add(new PotionItemInfo() { Name_EU = "Greater Life Elixir", Name_RU = "", Name_KR = "", TypeId = 162000055, Level = 40, HP = 1060, MP = 0 });
            LifeElixirPotions.Add(new PotionItemInfo() { Name_EU = "Regular Life Elixir", Name_RU = "", Name_KR = "", TypeId = 162000054, Level = 30, HP = 810, MP = 0 });
            LifeElixirPotions.Add(new PotionItemInfo() { Name_EU = "Lesser Life Elixir", Name_RU = "", Name_KR = "", TypeId = 162000053, Level = 20, HP = 560, MP = 0 });
            LifeElixirPotions.Add(new PotionItemInfo("Minor Life Elixir", "", "", 162000052, 10, 310, 0));




            ManaPotions = new List<PotionItemInfo>();
            ManaPotions.Add(new PotionItemInfo() { Name_EU = "Major Mana Potion", Name_RU = "", Name_KR = "", TypeId = 162000011, Level = 50, HP = 0, MP = 1600 });
            ManaPotions.Add(new PotionItemInfo() { Name_EU = "Greater Mana Potion", Name_RU = "", Name_KR = "", TypeId = 162000010, Level = 40, HP = 0, MP = 1480 });
            ManaPotions.Add(new PotionItemInfo() { Name_EU = "Mana Potion", Name_RU = "", Name_KR = "", TypeId = 162000009, Level = 30, HP = 0, MP = 1280 });
            ManaPotions.Add(new PotionItemInfo() { Name_EU = "Lesser Mana Potion", Name_RU = "", Name_KR = "", TypeId = 162000008, Level = 20, HP = 0, MP = 980 });
            ManaPotions.Add(new PotionItemInfo("Minor Mana Potion", "", "", 162000007, 10, 0, 590));


            ManaElixirPotions = new List<PotionItemInfo>();
            ManaElixirPotions.Add(new PotionItemInfo() { Name_EU = "Major Mana Elixir", Name_RU = "", Name_KR = "", TypeId = 162000061, Level = 50, HP = 0, MP = 1340 });
            ManaElixirPotions.Add(new PotionItemInfo() { Name_EU = "Greater Mana Elixir", Name_RU = "", Name_KR = "", TypeId = 162000060, Level = 40, HP = 0, MP = 1240 });
            ManaElixirPotions.Add(new PotionItemInfo() { Name_EU = "Regular Mana Elixir", Name_RU = "", Name_KR = "", TypeId = 162000059, Level = 30, HP = 0, MP = 1070 });
            ManaElixirPotions.Add(new PotionItemInfo() { Name_EU = "Lesser Mana Elixir", Name_RU = "", Name_KR = "", TypeId = 162000058, Level = 20, HP = 0, MP = 820 });
            ManaElixirPotions.Add(new PotionItemInfo("Minor Mana Elixir", "", "", 162000057, 10, 0, 500));


            // Serum
            LifeSerum = new List<PotionItemInfo>();
            LifeSerum.Add(new PotionItemInfo() { Name_EU = "Major Life Serum", Name_RU = "", Name_KR = "", TypeId = 162000016, Level = 50, HP = 1410, MP = 0 });
            LifeSerum.Add(new PotionItemInfo() { Name_EU = "Greater Life Serum", Name_RU = "", Name_KR = "", TypeId = 162000015, Level = 40, HP = 1160, MP = 0 });
            LifeSerum.Add(new PotionItemInfo() { Name_EU = "Life Serum", Name_RU = "", Name_KR = "", TypeId = 162000014, Level = 30, HP = 890, MP = 0 });
            LifeSerum.Add(new PotionItemInfo() { Name_EU = "Lesser Life Serum", Name_RU = "", Name_KR = "", TypeId = 162000013, Level = 20, HP = 610, MP = 0 });
            LifeSerum.Add(new PotionItemInfo("Minor Life Serum", "", "", 162000012, 10, 340, 0));


            ManaSerum = new List<PotionItemInfo>();
            ManaSerum.Add(new PotionItemInfo() { Name_EU = "Major Mana Serum", Name_RU = "", Name_KR = "", TypeId = 162000021, Level = 50, HP = 0, MP = 1470 });
            ManaSerum.Add(new PotionItemInfo() { Name_EU = "Greater Mana Serum", Name_RU = "", Name_KR = "", TypeId = 162000020, Level = 40, HP = 0, MP = 1360 });
            ManaSerum.Add(new PotionItemInfo() { Name_EU = "Mana Serum", Name_RU = "", Name_KR = "", TypeId = 162000019, Level = 30, HP = 0, MP = 1170 });
            ManaSerum.Add(new PotionItemInfo() { Name_EU = "Lesser Mana Serum", Name_RU = "", Name_KR = "", TypeId = 162000018, Level = 20, HP = 0, MP = 980 });
            ManaSerum.Add(new PotionItemInfo("Minor Mana Serum", "", "", 162000017, 10, 0, 590));
            
            // Divine Mana Serum
            DivineManaSerum = new List<PotionItemInfo>();
            DivineManaSerum.Add(new PotionItemInfo() { Name_EU = "Divine Mana Serum", Name_RU = "", Name_KR = "", TypeId = 162000021, Level = 50, HP = 0, MP = 1870 });
            

            // Panacea
            PanaceaOfLife = new List<PotionItemInfo>();
            PanaceaOfLife.Add(new PotionItemInfo() { Name_EU = "Major Panacea of Life", Name_RU = "", Name_KR = "", TypeId = 162000091, Level = 50, HP = 1130, MP = 0 });
            PanaceaOfLife.Add(new PotionItemInfo() { Name_EU = "Greater Panacea of Life", Name_RU = "", Name_KR = "", TypeId = 162000090, Level = 40, HP = 930, MP = 0 });
            PanaceaOfLife.Add(new PotionItemInfo() { Name_EU = "Panacea of Life", Name_RU = "", Name_KR = "", TypeId = 162000089, Level = 30, HP = 720, MP = 0 });
            PanaceaOfLife.Add(new PotionItemInfo() { Name_EU = "Lesser Panacea of Life", Name_RU = "", Name_KR = "", TypeId = 162000088, Level = 20, HP = 490, MP = 0 });
            PanaceaOfLife.Add(new PotionItemInfo("Minor Panacea of Life", "", "", 162000087, 10, 270, 0));

            PanaceaOfMana = new List<PotionItemInfo>();
            PanaceaOfMana.Add(new PotionItemInfo() { Name_EU = "Fine Panacea of Mana", Name_RU = "", Name_KR = "", TypeId = 162000098, Level = 50, HP = 0, MP = 1180 });
            PanaceaOfMana.Add(new PotionItemInfo() { Name_EU = "Major Panacea of Mana", Name_RU = "", Name_KR = "", TypeId = 162000097, Level = 50, HP = 0, MP = 1180 });
            PanaceaOfMana.Add(new PotionItemInfo() { Name_EU = "Greater Panacea of Mana", Name_RU = "", Name_KR = "", TypeId = 162000096, Level = 40, HP = 0, MP = 1090 });
            PanaceaOfMana.Add(new PotionItemInfo() { Name_EU = "Panacea of Mana", Name_RU = "", Name_KR = "", TypeId = 162000095, Level = 30, HP = 0, MP = 940 });
            PanaceaOfMana.Add(new PotionItemInfo() { Name_EU = "Lesser Panacea of Mana", Name_RU = "", Name_KR = "", TypeId = 162000094, Level = 20, HP = 0, MP = 720 });
            PanaceaOfMana.Add(new PotionItemInfo("Minor Panacea of Mana", "", "", 162000093, 10, 0, 440));

            // Wind serum
            WindSerum = new List<WindSerumItemInfo>();
            WindSerum.Add(new WindSerumItemInfo() { Name_EU = "Fine Wind Serum", Name_RU = "", Name_KR = "", TypeId = 162000028, FlyTime = 60000 });
            WindSerum.Add(new WindSerumItemInfo() { Name_EU = "Major Wind Serum", Name_RU = "", Name_KR = "", TypeId = 162000027, FlyTime = 48000 });
            WindSerum.Add(new WindSerumItemInfo() { Name_EU = "Greater Wind Serum", Name_RU = "", Name_KR = "", TypeId = 162000026, FlyTime = 36000 });
            WindSerum.Add(new WindSerumItemInfo() { Name_EU = "Wind Serum", Name_RU = "", Name_KR = "", TypeId = 162000025, FlyTime = 24000 });
            WindSerum.Add(new WindSerumItemInfo() { Name_EU = "Lesser Wind Serum", Name_RU = "", Name_KR = "", TypeId = 162000024, FlyTime = 12000 });

            // Treatment
            HerbTreatment = new List<TreatmentItemInfo>();
            HerbTreatment.Add(new TreatmentItemInfo() { Name_EU = "Herb Treatment IV", Name_RU = "", Name_KR = "", OdellaTypeId = 169300006, Level = 46, HP = 648, MP = 0 });
            HerbTreatment.Add(new TreatmentItemInfo() { Name_EU = "Herb Treatment III", Name_RU = "", Name_KR = "", OdellaTypeId = 169300005, Level = 34, HP = 535, MP = 0 });
            HerbTreatment.Add(new TreatmentItemInfo() { Name_EU = "Herb Treatment II", Name_RU = "", Name_KR = "", OdellaTypeId = 169300004, Level = 22, HP = 464, MP = 0 });
            HerbTreatment.Add(new TreatmentItemInfo() { Name_EU = "Herb Treatment I", Name_RU = "", Name_KR = "", OdellaTypeId = 169300003, Level = 10, HP = 298, MP = 0 });


            ManaTreatment = new List<TreatmentItemInfo>();
            ManaTreatment.Add(new TreatmentItemInfo() { Name_EU = "Mana Treatment IV", Name_RU = "", Name_KR = "", OdellaTypeId = 169300006, Level = 46, HP = 0, MP = 648 });
            ManaTreatment.Add(new TreatmentItemInfo() { Name_EU = "Mana Treatment III", Name_RU = "", Name_KR = "", OdellaTypeId = 169300005, Level = 34, HP = 0, MP = 535 });
            ManaTreatment.Add(new TreatmentItemInfo() { Name_EU = "Mana Treatment II", Name_RU = "", Name_KR = "", OdellaTypeId = 169300004, Level = 22, HP = 0, MP = 404 });
            ManaTreatment.Add(new TreatmentItemInfo() { Name_EU = "Mana Treatment I", Name_RU = "", Name_KR = "", OdellaTypeId = 169300003, Level = 10, HP = 0, MP = 298 });

            CheckMinimumPotionHPMP = false;


            HP_MP_PotionTypeIdListToCheckCooldown = new List<uint>();

            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(RecoveryPotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(RecoverySerum.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(LifePotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(LifeElixirPotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(ManaPotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(DivineManaPotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(ManaElixirPotions.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(LifeSerum.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(ManaSerum.Select(s => s.TypeId));
            HP_MP_PotionTypeIdListToCheckCooldown.AddRange(PanaceaOfMana.Select(s => s.TypeId));
        }


        public static bool CheckRecoveryPotion()
        {
            return CheckPotion(RecoveryPotions);
        }
        public static bool CheckRecoverySerum()
        {
            return CheckPotion(RecoverySerum);
        }

        public static bool CheckLifePotion()
        {
            return CheckPotion(LifePotions);
        }

        public static bool CheckManaPotion()
        {
            return CheckPotion(ManaPotions);
        }

        public static bool CheckLifeSerum()
        {
            return CheckPotion(LifeSerum);
        }

        public static bool CheckManaSerum()
        {
            return CheckPotion(ManaSerum);
        }

        public static bool CheckDivineManaSerum()
        {
            return CheckPotion(DivineManaSerum);
        }
        
        public static bool CheckLifeElixir()
        {
            return CheckPotion(LifeElixirPotions);
        }

        public static bool CheckManaElixir()
        {
            return CheckPotion(ManaElixirPotions);
        }


        public static bool CheckLifePanacea()
        {
            return CheckPotion(PanaceaOfLife);
        }

        public static bool CheckManaPanacea()
        {
            return CheckPotion(PanaceaOfMana);
        }

        /// <summary>
        /// Use a Wind serum potion
        /// </summary>
        /// <returns></returns>
        public static bool CheckWindSerum()
        {
            if (PotionReuseDelay >= AionGame.Game.Time())
            {
                return false;
            }

            var missingFlyTime = AionGame.Game.Player.FlightTimeMaximum - AionGame.Game.Player.FlightTimeCurrent;

            // We need to check all postion cooldown. if no potion are in cooldown then check if we need to use a potion
            if (IsInventoryPotionInCooldown(WindSerum.Select(s => s.TypeId).ToList()))
            {
                PotionReuseDelay = AionGame.Game.Time() + 1000;
                return false;
            }

            for (int i = 0; i < WindSerum.Count; i++)
            {
                if (missingFlyTime >= WindSerum[i].FlyTime)// && AionGame.Game.Player.Level >= potionsList[i].Level)
                {
                    var potionItem = AionGame.Game.InventoryList.GetItemByTypeId(WindSerum[i].TypeId);

                    if (potionItem != null && potionItem.GetCooldown() == 0)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(WindSerum[i].TypeId);

                        Thread.Sleep(200);
                        if (potionItem.GetCooldown() > 0)
                        {
                            PotionReuseDelay = AionGame.Game.Time() + potionItem.ReuseTime + 500;
                            return true;
                        }
                        else
                        {
                            PotionReuseDelay = AionGame.Game.Time() + 1000;
                        }
                    }
                }
            }

            return false;
        }

        private static bool CheckPotion(List<PotionItemInfo> potionsList)
        {
            if (PotionReuseDelay >= AionGame.Game.Time())
            {
                return false;
            }

            // We need to check all postion cooldown. if no potion are in cooldown then check if we need to cast a potion
            if (IsInventoryPotionInCooldown(HP_MP_PotionTypeIdListToCheckCooldown))
            {
                PotionReuseDelay = AionGame.Game.Time() + 1000;
                return false;
            }

            var missingHealth = AionGame.Game.Player.HealthMaximum - AionGame.Game.Player.HealthCurrent;
            var missingMana = AionGame.Game.Player.ManaMaximum - AionGame.Game.Player.ManaCurrent;

            for (int i = 0; i < potionsList.Count; i++)
            {
                if (CheckMinimumPotionHPMP == false || (missingHealth >= potionsList[i].HP && missingMana >= potionsList[i].MP))
                {
                    if (potionsList[i].TypeId == 162000098 && AionGame.Game.Player.Level < potionsList[i].Level) {
                        continue;
                    }
                    var potionItem = AionGame.Game.InventoryList.GetItemByTypeId(potionsList[i].TypeId);
                    if (potionItem != null && potionItem.GetCooldown() == 0)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(potionsList[i].TypeId);
                        Thread.Sleep(200);

                        if (potionItem.GetCooldown() > 0)
                        {
                            PotionReuseDelay = AionGame.Game.Time() + potionItem.ReuseTime + 500;
                            return true;
                        }
                        else
                        {
                            PotionReuseDelay = AionGame.Game.Time() + 1000;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check all inventory (with same type id of potion in potionList parameter) item cooldown. Even if only one item is in cd return true 
        /// </summary>
        /// <param name="potionsList"></param>
        /// <returns></returns>
        public static bool IsInventoryPotionInCooldown(List<uint> potionsList)
        {
            foreach (var inventoryItem in AionGame.Game.InventoryList.GetList())
            {
                if (potionsList.Contains(inventoryItem.TypeId))
                {
                    uint tempCurrentItemCooldown = inventoryItem.GetCooldown();
                    if (tempCurrentItemCooldown > 0)
                    {
                        //    AionGame.Game.WriteMessage("Potion "+inventoryItem.Name+ " is in CD - "+inventoryItem.GetCooldown());//
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check all inventory (with same type id of potion in potionList parameter) item cooldown. Even if only one item is in cd return true 
        /// </summary>
        /// <param name="potionsList"></param>
        /// <returns></returns>
        public static PotionSerumItemCooldownInfo GetInventoryPotionCooldownTime(List<WindSerumItemInfo> potionsList)
        {
            PotionSerumItemCooldownInfo tempPotionCooldownInfo = new PotionSerumItemCooldownInfo() { TypeId = 0, Cooldown = 0, RecoveryFlyTimeAmount = 0 };

            foreach (var inventoryItem in AionGame.Game.InventoryList.GetList())
            {
                var tempPotionFromListResult = potionsList.Where(item => item.TypeId == inventoryItem.TypeId).FirstOrDefault();
                if (tempPotionFromListResult != null)
                {
                    uint tempCurrentItemCooldown = inventoryItem.GetCooldown();
                    if (tempCurrentItemCooldown == 0 && tempPotionFromListResult.FlyTime > tempPotionCooldownInfo.RecoveryFlyTimeAmount)
                    {
                        tempPotionCooldownInfo.TypeId = inventoryItem.TypeId;
                        tempPotionCooldownInfo.RecoveryFlyTimeAmount = tempPotionFromListResult.FlyTime;
                        tempPotionCooldownInfo.Cooldown = tempCurrentItemCooldown;

                        //    AionGame.Game.WriteMessage("Potion "+inventoryItem.Name+ " is in CD - "+inventoryItem.GetCooldown());//
                    }
                    else if (tempCurrentItemCooldown > 0)
                    {
                        // There is at least a potion in CD, then all other potion of same type are in CD
                        return new PotionSerumItemCooldownInfo() { TypeId = 0, Cooldown = 0, RecoveryFlyTimeAmount = 0 };
                    }
                }
            }

            return tempPotionCooldownInfo;
        }

        public static bool CheckHerbTreatment()
        {
            var missingHealth = AionGame.Game.Player.HealthMaximum - AionGame.Game.Player.HealthCurrent;

            for (int i = 0; i < HerbTreatment.Count; i++)
            {
                if (CheckMinimumPotionHPMP == false || (missingHealth >= HerbTreatment[i].HP && AionGame.Game.Player.Level >= HerbTreatment[i].Level))
                {
                    var isOdellaItemAvailable = AionGame.Game.InventoryList.GetList()
                        .Where(item => item.TypeId == HerbTreatment[i].OdellaTypeId && item.Quantity >= 2)
                        .Any();

                    if (isOdellaItemAvailable)
                    {
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(HerbTreatment[i].Name_EU))
                        {
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(HerbTreatment[i].Name_EU);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool CheckManaTreatment()
        {
            var missingMana = AionGame.Game.Player.ManaMaximum - AionGame.Game.Player.ManaCurrent;

            for (int i = 0; i < ManaTreatment.Count; i++)
            {
                if (CheckMinimumPotionHPMP == false || (missingMana >= ManaTreatment[i].MP && AionGame.Game.Player.Level >= ManaTreatment[i].Level))
                {
                    var isOdellaItemAvailable = AionGame.Game.InventoryList.GetList()
                        .Where(item => item.TypeId == ManaTreatment[i].OdellaTypeId && item.Quantity >= 2)
                        .Any();

                    if (isOdellaItemAvailable)
                    {
                        if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(ManaTreatment[i].Name_EU))
                        {
                            AionGame.UnknownFramework.Helper.HelperFunction.CheckExecute(ManaTreatment[i].Name_EU);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public static bool CanUseHerbTreatment()
        {
            for (int i = 0; i < HerbTreatment.Count; i++)
            {
                var isOdellaItemAvailable = AionGame.Game.InventoryList.GetList()
                    .Where(item => item.TypeId == HerbTreatment[i].OdellaTypeId && item.Quantity >= 2)
                    .Any();

                if (isOdellaItemAvailable)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(HerbTreatment[i].Name_EU))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CanUseManaTreatment()
        {
            for (int i = 0; i < ManaTreatment.Count; i++)
            {
                var isOdellaItemAvailable = AionGame.Game.InventoryList.GetList()
                    .Where(item => item.TypeId == ManaTreatment[i].OdellaTypeId && item.Quantity >= 2)
                    .Any();

                if (isOdellaItemAvailable)
                {
                    if (AionGame.UnknownFramework.Helper.HelperFunction.CheckAvailable(ManaTreatment[i].Name_EU))
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}
