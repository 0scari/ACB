using AionBotnet.AionGame;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include
{
    public static class ShardHelper
    {
        /// <summary>
        /// Last check time
        /// </summary>
        private static ulong CheckTimer { get; set; }


        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class ShardSetting
        {
            public ShardSetting()
            {
                DisableShardBelowAmount = 1000;
                EnableShardAboveAmount = 5000;
            }

            public ShardSetting(int disableBelow, int enableAbove)
            {
                DisableShardBelowAmount = disableBelow;
                EnableShardAboveAmount = enableAbove;
            }
            
            [DisplayName("Disable shard below")]
            public int DisableShardBelowAmount { get; set; }

            [DisplayName("Enable shard above")]
            public int EnableShardAboveAmount { get; set; }

            public override string ToString()
            {
                return "";
            }
        }

        static ShardHelper()
        {
            CheckTimer = 0;
        }

        /// <summary>
        /// Automatically enable and disable shards
        /// </summary>
        /// <param name="settings">Shard settings</param>
        /// <returns>True if shard is enabled or disable else return false</returns>
        public static bool ManageShards(ShardSetting settings)
        {
            if (CanCheck())
            {
                var shardDialog = Game.DialogList.GetDialog("basic_status_dialog/boost_marker");

                if (shardDialog != null)
                {
                    var shardStatus = Game.Process.GetUnsignedInteger(shardDialog.GetAddress() + 0x28);
                    var shardInventorySlot = Game.InventoryList.GetList().Where(item => item.SlotType == AionGame.Enums.eInventorySlotType.Shard_R).FirstOrDefault();

                    // Shard currently disabled
                    if (shardStatus == 1)
                    {
                        // Disable shard if need
                        if (shardInventorySlot.Quantity >= (uint)settings.EnableShardAboveAmount)
                        {
                            Game.PlayerInput.SendKey(System.Windows.Forms.Keys.B);
                            return true;
                        }
                    }
                    // Shard currently enabled
                    else if (shardStatus == 2)
                    {
                        // Disable shard if need
                        if (shardInventorySlot.Quantity <= (uint)settings.DisableShardBelowAmount)
                        {
                            Game.PlayerInput.SendKey(System.Windows.Forms.Keys.B);
                            return true;
                        }
                    }
                    
                }
            }

            return false;
        }

        private static bool CanCheck()
        {
            if (Game.Time() - CheckTimer > 5000)
            {
                CheckTimer = Game.Time();
                return true;
            }

            return false;
        }
    }
}
