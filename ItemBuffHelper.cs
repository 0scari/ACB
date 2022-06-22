using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include
{
    public class RallySerum
    {
        public string Name_EU { get; set; }
        public string Name_RU { get; set; }
        public string Name_KR { get; set; }

        public uint TypeId { get; set; }
        public int Level { get; set; }
        public RallySerum()
        {

        }
    }

    public static class ItemBuffHelper
    {
        public static List<uint> RallySerumItemTypeId { get; set; }
        public static List<uint> FocusAgentItemTypeId { get; set; }
        public static List<uint> RunningScrollItemTypeId { get; set; }
        public static List<uint> AwakeningScrollItemTypeId { get; set; }
        public static List<uint> CourageScrollItemTypeId { get; set; }
        public static List<uint> CritStrikeScrollItemTypeId { get; set; }
        public static List<uint> CritSpellScrollItemTypeId { get; set; }


        // Custom food
        private static ulong _consumableLastTimeCheck;

        static ItemBuffHelper()
        {
            // 160003551 - Minor Rally Serum
            // 160003552 - Lesser Rally Serum
            // 160003553 - Rally Serum
            // 160003554 - Greater Rally Serum
            // 160003555 - Major Rally Serum
            RallySerumItemTypeId = new List<uint>() { 160003555, 160003554, 160003553, 160003552, 160003551 };

            // 160003557 - Minor Focus Agent
            // 160003558 - Lesser Focus Agent
            // 160003559 - Focus Agent
            // 160003560 - Greater Focus Agent
            // 160003561 - Major Focus Agent
            FocusAgentItemTypeId = new List<uint>() { 160003561, 160003560, 160003559, 160003558, 160003557 };


            // 
            // 
            AwakeningScrollItemTypeId = new List<uint>() { 164002237, 164000134, 164000334 };

            // 164002235 - Greater Running Sroll
            // 164000076 - Greater Running Scroll
            // 164000072 - Courage Scroll
            RunningScrollItemTypeId = new List<uint>() { 164002235, 164000076, 164000072 };

            // 164000073 - Greater Courage Scroll
            // 164000072 - Courage Scroll
            CourageScrollItemTypeId = new List<uint>() { 164000073, 164000072 };

            // 164000066 - Greater Crit Strike Scroll
            // 164000065 - Crit Strike Scroll
            CritStrikeScrollItemTypeId = new List<uint>() { 164000066, 164000065 };

            // 164000121 - Greater Crit Spell Scroll
            // 164000120 - Crit Spell Scroll
            CritSpellScrollItemTypeId = new List<uint>() { 164000121, 164000120 };

            // Custom food
            _consumableLastTimeCheck = 0;
        }


        public static bool CheckRallySerum()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Increase Natural Recovery Serum") >= 0).Any() == false)
            {
                for (int i = 0; i < RallySerumItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(RallySerumItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(RallySerumItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool CheckFocusAgent()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Increase Physical Attack and Magic Boost") >= 0).Any() == false)
            {
                for (int i = 0; i < FocusAgentItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(FocusAgentItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(FocusAgentItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Active Move speed scroll
        /// </summary>
        /// <returns></returns>
        public static bool CheckRunningScroll()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Increase Movement Speed") >= 0).Any() == false)
            {
                for (int i = 0; i < RunningScrollItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(RunningScrollItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(RunningScrollItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Active Attack speed scroll
        /// </summary>
        /// <returns></returns>
        public static bool CourageScrollIScroll()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Attack Speed Increase") >= 0).Any() == false)
            {
                for (int i = 0; i < CourageScrollItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(CourageScrollItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(CourageScrollItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Active Casting Speed scroll
        /// </summary>
        /// <returns></returns>
        public static bool CheckAwakeningScroll()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Casting Speed Increase") >= 0).Any() == false)
            {
                for (int i = 0; i < AwakeningScrollItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(AwakeningScrollItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(AwakeningScrollItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Active Critical Strike scroll
        /// </summary>
        /// <returns></returns>
        public static bool CheckCriticalStrikeScroll()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Crit Strike") >= 0).Any() == false)
            {
                for (int i = 0; i < CritStrikeScrollItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(CritStrikeScrollItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(CritStrikeScrollItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Active Critical Spell scroll
        /// </summary>
        /// <returns></returns>
        public static bool CheckCriticalSpellScroll()
        {
            if (AionGame.Game.Player.StateList.GetList().Where(s => s.Value.Name_Eu.IndexOf("Crit Spell") >= 0).Any() == false)
            {
                for (int i = 0; i < CritSpellScrollItemTypeId.Count; i++)
                {
                    var item = AionGame.Game.InventoryList.GetItemByTypeId(CritSpellScrollItemTypeId[i]);
                    if (item != null)
                    {
                        AionGame.Game.PlayerInput.UseInventoryItemByTypeId(CritSpellScrollItemTypeId[i]);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Use a custom consumable item
        /// </summary>
        /// <param name="customFoodString">A string that contains the item id or item name and the buff id associated with this consumable.
        /// The string format have to like this "ItemID or Item Name:BuffId;ItemID or Item Name:BuffId"</param>
        /// <returns></returns>
        public static bool CheckCustomConsumable(string customFoodString)
        {
            // Check if string is empty
            if (string.IsNullOrWhiteSpace(customFoodString))
                return false;

            // Check delay time
            if (AionGame.Game.Time() < _consumableLastTimeCheck + 5000)
                return false;

            _consumableLastTimeCheck = AionGame.Game.Time();

            var splitString = customFoodString.Trim().Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);

            // If at least 1 element
            if (splitString.Length > 0)
            {
                // Parse itemId\name and BuffId
                foreach (var singleConsumableString in splitString)
                {
                    var splitSingleItem = singleConsumableString.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if contain 2 element
                    if (splitSingleItem.Length >= 2)
                    {
                        string buffId = splitSingleItem[1];

                        var isBuffUp = AionGame.Game.Player.StateList.GetState(buffId.Trim());

                        // If buff is off
                        if (isBuffUp == null)
                        {
                            string itemIdOrNameString = splitSingleItem[0].Trim();

                            // Try parse item
                            var itemIdNumber = 0;
                            AionGame.InventoryItem itemToUse = null;
                            if (int.TryParse(itemIdOrNameString, out itemIdNumber))
                            {
                                itemToUse = AionGame.Game.InventoryList.GetItemByTypeId((uint)itemIdNumber);
                            }
                            else
                            {
                                itemToUse = AionGame.Game.InventoryList.GetInventory(itemIdOrNameString);
                            }

                            // If item exist use it
                            if (itemToUse != null)
                            {
                                AionGame.Game.PlayerInput.Console(AionGame.Game.UseCommand + itemToUse.Name);
                                AionGame.Game.WriteDebugMessage("item " + itemToUse.Name + " has used.");
                                return true;
                            }
                        }
                    }
                }
            }


            return false;
        }
    }
}
