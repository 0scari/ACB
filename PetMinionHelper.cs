using AionBotnet.AionGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AionBotnet.ScriptLibrary.AionClassic.Include
{
    public static class PetMinionHelper
    {
        /// <summary>
        /// Timer to check minion or pet status
        /// </summary>
        private static ulong PetMinionCheckTimer { get; set; }


        private static ulong PetMinionGiftCheckTimer { get; set; }

        /// <summary>
        /// Dictonary contains Minion with respctive activable skill
        /// </summary>
        private static readonly Dictionary<uint, uint> MinionActivableSkillId = new Dictionary<uint, uint>() {
            { 980010, 4909},    // Kerubar - Sleeping Dragon Kick
            { 980011, 4908 },   // Kerubian - Sleeping Dragon Kick
            { 980012, 4907 },   // Kerubiel - Sleeping Dragon Kick
            { 980013, 4906 },   // Arch Kerubiel - Sleeping Dragon Kick

            { 980022, 4904 },   // Seiren - Summon Waterdrop
            { 980023, 4905 },   // Seiren - Summon Durable Waterdrop

            { 980032, 4900 },   // Steel Rose - Projectile of Mind Division
            { 980033, 4901 },   // Steel Rose - Durable Projectile of Mind Division

            { 980042, 4896 },   // Abija - Improved Stamina
            { 980043, 4897 },   // Abija - Durable Improved Stamina

            { 980052, 4892 },   // Hamerun - Hamerun's Wrath
            { 980053, 4893 },   // Hamerun - Hamerun's Durable Wrath

            { 980062, 4888 },   // Grendal - Ragman
            { 980063, 4889 },   // Grendal - Durable Ragman

            { 980076, 4969 },   // Karemiwen - Deep Wound
            { 980077, 4970 },   // Karemiwen - Boosted Deep Wound

            { 980080, 4973 },   // Saendukal - Blind Rage
            { 980081, 4974 },   // Saendukal - Boosted Blind Rage

            { 980084, 4957 },   // Weda - Enchantment: Old Friend
            { 980085, 4958 },   // Weda - Upgraded Enchantment: Old Friend

            { 980088, 4961 },   // Kromede - Miserable Struggle
            { 980089, 4962 },   // Kromede - Boosted Miserable Struggle
            
            { 980092, 4965 },   // Hyperion - Id Aetheric Field
            { 980093, 4966 },   // Hyperion - Boosted Id Aetheric Field

            { 981010, 4909},    // Kerubar - Sleeping Dragon Kick
            { 981011, 4908 },   // Kerubian - Sleeping Dragon Kick
            { 981012, 4907 },   // Kerubiel - Sleeping Dragon Kick
            { 981013, 4906 },   // Arch Kerubiel - Sleeping Dragon Kick

            { 981022, 4904 },   // Seiren - Summon Waterdrop
            { 981023, 4905 },   // Seiren - Summon Durable Waterdrop

            { 981032, 4900 },   // Steel Rose - Projectile of Mind Division
            { 981033, 4901 },   // Steel Rose - Durable Projectile of Mind Division

            { 981042, 4896 },   // Abija - Improved Stamina
            { 981043, 4897 },   // Abija - Durable Improved Stamina

            { 981052, 4892 },   // Hamerun - Hamerun's Wrath
            { 981053, 4893 },   // Hamerun - Hamerun's Durable Wrath

            { 981062, 4888 },   // Grendal - Ragman
            { 981063, 4889 },   // Grendal - Durable Ragman

            { 981072, 4884 },   // Sita - Horror Beast
            { 981073, 4885 },   // Sita - Durable Horror Beast
            
            { 981076, 4969 },   // Karemiwen - Deep Wound
            { 981077, 4970 },   // Karemiwen - Boosted Deep Wound

            { 981080, 4973 },   // Saendukal - Blind Rage
            { 981081, 4974 },   // Saendukal - Boosted Blind Rage

            { 981084, 4957 },   // Weda - Enchantment: Old Friend
            { 981085, 4958 },   // Weda - Upgraded Enchantment: Old Friend

            { 981088, 4961 },   // Kromede - Miserable Struggle
            { 981089, 4962 },   // Kromede - Boosted Miserable Struggle
            
            { 981092, 4965 },   // Hyperion - Id Aetheric Field
            { 981093, 4966 },   // Hyperion - Boosted Id Aetheric Field
        };


        private static  string _petFoodSkill_PraisePet;
        static PetMinionHelper()
        {
            PetMinionCheckTimer = 0;
            PetMinionGiftCheckTimer = 0;
            _petFoodSkill_PraisePet = "/PraisePet";

            if (Game.ClientRegion == AionGame.Enums.eAionClientRegion.Europe|| Game.ClientRegion == AionGame.Enums.eAionClientRegion.NorthAmerica)
            {
                _petFoodSkill_PraisePet = "/PraisePet";
            }
            else if (Game.ClientRegion== AionGame.Enums.eAionClientRegion.Taiwan)
            {
                _petFoodSkill_PraisePet = "/稱讚寵物";
            }
            else if (Game.ClientRegion == AionGame.Enums.eAionClientRegion.Korean)
            {
                _petFoodSkill_PraisePet = "";
            }
            else if (Game.ClientRegion== AionGame.Enums.eAionClientRegion.Japanese)
            {
                _petFoodSkill_PraisePet = "/ペットを褒める";
            }

       //     Game.WriteMessage("PetMinionHelper set skill \"Prise Pet\" name: " + _petFoodSkill_PraisePet);
        }

        /// <summary>
        /// </summary>
        /// <example>
        /// // Summon Pet
        /// if (PetMinionHelper.UseMinionPet(1))
        ///     return false;
        /// </example>
        /// <param name="quickbarPetSlot">Slot where minion icon is</param>
        /// <param name="activeAutoLoot">Active auto loot pet function</param>
        /// <param name="activeBuffMaster">Active buff master pet function</param>
        /// <param name="activeMinionSkill">Active minion skill</param>
        /// <returns></returns>
        public static bool UseMinionPet(int quickbarPetSlot, bool activeAutoLoot = true, bool activeBuffMaster = true, bool activeMinionSkill = true)
        {
            if (CanCheck())
            {
                var quickBar = Game.DialogList.GetDialog("quickbar_dialog/ctn_shortcut/shortcut" + quickbarPetSlot);
                uint tempPetTypeId = Game.Process.GetUnsignedInteger(quickBar.GetAddress() + Game.Offsets.DialogOffsetList.SkillId);

                bool petRangeIdRespected = (tempPetTypeId >= 900000 && tempPetTypeId <= 900234) || (tempPetTypeId >= 970000 && tempPetTypeId <= 970018);
                bool minionRangeIdRespected = (tempPetTypeId >= 980010 && tempPetTypeId <= 980100);
                bool goldMinionRangeIdRespected = (tempPetTypeId >= 981130 && tempPetTypeId <= 981169);
                bool goldMinion2RangeIdRespected = (tempPetTypeId >= 981301 && tempPetTypeId <= 981310);

                if (petRangeIdRespected || petRangeIdRespected || goldMinionRangeIdRespected || goldMinion2RangeIdRespected)
                {
                    var pets = Game.EntityList.GetList().Where(ent => (ent.Value.IsPet || ent.Value.IsMinion) && ent.Value.OwnerID == Game.Player.Id).FirstOrDefault();

                    // Summon pet
                    if (pets.Value == null || (pets.Value != null && tempPetTypeId != pets.Value.TypeId))
                    {
                        quickBar.Click();
                        return true;
                    }

                    if (activeAutoLoot && pets.Value != null && Game.IsPetAutolootActive() == false)
                    {
                        Game.PlayerInput.Ability("Auto-Loot");
                        return true;
                    }


                    if (activeBuffMaster && pets.Value != null && Game.IsPetBuffMasterActive() == false)
                    {
                        Game.PlayerInput.Ability("Buff Master");
                        return true;
                    }

                    if (activeMinionSkill && pets.Value != null && MinionActivableSkillId.ContainsKey(tempPetTypeId))
                    {
                        var petAbility = Game.AbilityList.GetAbility(MinionActivableSkillId[tempPetTypeId]);

                        if (petAbility != null && petAbility.State == 0)
                        {
                            Game.PlayerInput.Ability(MinionActivableSkillId[tempPetTypeId]);
                            return true;
                        }
                    }
                }
            }

            // No action has been executed
            return false;
        }


        public static bool FeedPet()
        {
            return false;
        }

        public static bool PetGift()
        {
            // Wait for delay
            if (Game.Time() < PetMinionGiftCheckTimer + 5000)
                return false;

            // If pet name is empty skip
            string petName = Game.Process.GetString(Game.GameDllBaseAddress + Game.Offsets.GlobalOffsetList.PetName);
            if (string.IsNullOrWhiteSpace(petName))
                return false;

            bool isActionDone = false;

            // Check if we need to use pet skill to feed mood. Value under 9000
            int petMood = Game.Process.GetInteger(Game.GameDllBaseAddress + Game.Offsets.GlobalOffsetList.PetMood);
            if (petMood < 9000)
            {
                // Check if "Pet Management" skill is available
                var petManagementSkill = Game.AbilityList.GetAbility(50042);
                if (petManagementSkill.GetCooldown() == 0)
                {
                    StopMove();

                    // Select pet
                    Game.PlayerInput.Console(Game.SelectCommand + petName);
                    Thread.Sleep(200);

                    // Use skill
                    Game.PlayerInput.Console(Game.SkillCommand + petManagementSkill.Name);
                    Thread.Sleep(3000);

                    // Feed mood (/PraisePet - /GroomPet - /TrainPet)
                    Game.PlayerInput.Console(_petFoodSkill_PraisePet);
                    Thread.Sleep(5000);


                    Game.PlayerInput.SendKey(System.Windows.Forms.Keys.Escape);
                    Thread.Sleep(100);
                    Game.PlayerInput.SendKey(System.Windows.Forms.Keys.Escape);
                    Thread.Sleep(100);
                    Game.PlayerInput.SendKey(System.Windows.Forms.Keys.Escape);
                    Thread.Sleep(100);
                    Game.PlayerInput.SendKey(System.Windows.Forms.Keys.Escape);
                    Thread.Sleep(100);

                    PetMinionGiftCheckTimer = Game.Time();
                    isActionDone = true;
                }
            }

            // Check if have at least 1 slot to get pet gift
            if (Game.InventoryList.MaximumSlot - Game.InventoryList.UsedSlot > 1)
            {
                // Check if pet mood is full
                petMood = Game.Process.GetInteger(Game.GameDllBaseAddress + Game.Offsets.GlobalOffsetList.PetMood);
                if (petMood >= 9000)
                {
                    // Check if "Pet Gift" skill is available
                    var petGiftkill = Game.AbilityList.GetAbility(50043);
                    if (petGiftkill.GetCooldown() == 0)
                    {
                        // Select pet
                        //    Game.PlayerInput.Console(Game.SelectCommand + petName);
                        //    Thread.Sleep(200);

                        // Use skill
                        Game.PlayerInput.Console(Game.SkillCommand + petGiftkill.Name);
                        Thread.Sleep(3000);

                        PetMinionGiftCheckTimer = Game.Time();
                        isActionDone = true;
                    }
                }
            }

            // Check at least 1 free inventory slot
            if (Game.InventoryList.MaximumSlot - Game.InventoryList.UsedSlot > 1)
            {
                // Open "Coin Bundle"
                var inventoryItem = Game.InventoryList.GetItemByTypeId(188051379);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }


                // Open "Coin Bundle"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051162);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

                // Open "[Event] Elemental Stone Pouch II"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051380);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

                // Open "[Event] Balaur Materials Pouch"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051381);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

                // Open "[Event] Refining Stone Pouch"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051382);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

                // Open "[Event] Jewel Pouch II"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051383);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

                // Open "Pet's Gift Bundle"
                inventoryItem = Game.InventoryList.GetItemByTypeId(188051378);
                if (inventoryItem != null)
                {
                    StopMove();
                    Game.PlayerInput.Console(Game.UseCommand + inventoryItem.Name);
                    Thread.Sleep(4000);
                    PetMinionGiftCheckTimer = Game.Time();
                    return true;
                }

            }

            if (isActionDone)
                return true;

            return false;
        }

        private static void StopMove()
        {
            Game.Player.SetMove(null);
            Thread.Sleep(100);
            Game.Player.SetMove(null);
            Thread.Sleep(100);
            Game.Player.SetMove(null);
            Thread.Sleep(100);
            Game.Player.SetMove(null);
            Thread.Sleep(100);
        }

        private static bool CanCheck()
        {
            if (Game.Time() - PetMinionCheckTimer > 5000)
            {
                PetMinionCheckTimer = Game.Time();
                return true;
            }

            return false;
        }
    }
}
