﻿using System;
using System.Collections.Generic;
using TCC.Data;
using TCC.Data.Databases;
using TCC.ViewModels;
namespace TCC.Parsing
{
    public static class SystemMessagesProcessor
    {
        private static readonly List<string> ExclusionList = new List<string>
        {
            "SMT_ITEM_ROULETTE_VALUE",
            "SMT_BATTLE_START",
            "SMT_BATTLE_END",
            "SMT_DROPDMG_DAMAGE",
            "SMT_ABANDON_DIVIDE_DICE_PARTYPLAYER",
            "SMT_JOIN_DIVIDE_DICE_PATYPLAYER",
            "SMT_ABANDON_DIVIDE_DICE",
            "SMT_JOIN_DIVIDE_DICE"
        };

        private static bool Filter(string opcodeName)
        {
            return !ExclusionList.Contains(opcodeName);
        }
        internal static void AnalyzeMessage(string srvMsg, SystemMessage sysMsg, string opcodeName)
        {
            if (!Filter(opcodeName)) return;

            if (!Process(srvMsg, sysMsg, opcodeName))
            {
                ChatWindowManager.Instance.AddChatMessage(new ChatMessage(srvMsg, sysMsg, (ChatChannel)sysMsg.ChatChannel));
            }
        }
        private static void HandleMaxEnchantSucceed(string x)
        {
            var sysMsg = ChatMessage.BuildEnchantSystemMessage(x);
            ChatWindowManager.Instance.AddChatMessage(sysMsg);
        }
        private static void HandleFriendLogin(string friendName, SystemMessage sysMsg)
        {
            var sysmsg = "@0\vUserName\v" + friendName;
            var msg = new ChatMessage(sysmsg, sysMsg, ChatChannel.Friend);
            msg.Author = friendName;
            ChatWindowManager.Instance.AddChatMessage(msg);
        }

        #region Factory

        private static readonly Dictionary<string, Delegate> Processor = new Dictionary<string, Delegate>
        {
            { "SMT_MAX_ENCHANT_SUCCEED", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleMaxEnchantSucceed(srvMsg)) },
            { "SMT_FRIEND_IS_CONNECTED", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleFriendLogin(srvMsg, sysMsg)) },
            { "SMT_FRIEND_WALK_INTO_SAME_AREA", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleFriendInAreaMessage(srvMsg, sysMsg)) },
            { "SMT_CHAT_LINKTEXT_DISCONNECT", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleInvalidLink(srvMsg, sysMsg)) },

            { "SMT_BATTLE_PARTY_DIE", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleDeathMessage(srvMsg, sysMsg)) },
            { "SMT_BATTLE_PARTY_RESURRECT", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleRessMessage(srvMsg, sysMsg)) },
            { "SMT_BATTLE_YOU_DIE", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleDeathMessage(srvMsg, sysMsg)) },
            { "SMT_BATTLE_RESURRECT", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleRessMessage(srvMsg, sysMsg)) },

            { "SMT_ACCEPT_QUEST", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_CANT_START_QUEST", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_COMPLATE_GUILD_QUEST", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_COMPLETE_MISSION", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_COMPLETE_QUEST", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_FAILED_QUEST_COMPENSATION", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_FAILED_QUEST", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_FAILED_QUEST_CANCLE", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_FAILED_GET_FLAG_PARTY_LEVEL", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_ITEM_DELETED", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_RESET_MESSAGE", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_UPDATE_QUEST_TASK", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_SHARE_MESSAGE2", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_USE_SKILL", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_QUEST_USE_ITEM", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleQuestMessage(srvMsg, sysMsg)) },
            { "SMT_GRANT_DUNGEON_COOLTIME_AND_COUNT", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleDungeonEngagedMessage(srvMsg, sysMsg)) },
            { "SMT_GQUEST_URGENT_NOTIFY", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleGuilBamSpawn(srvMsg, sysMsg)) },

            { "SMT_PARTY_LOOT_ITEM_PARTYPLAYER", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleGroupMemberLoot(srvMsg, sysMsg)) },

            { "SMT_ACCOMPLISH_ACHIEVEMENT_GRADE_ALL", new Action<string, SystemMessage>((srvMsg, sysMsg) => HandleLaurelMessage(srvMsg, sysMsg)) },
            { "SMT_ACCOMPLISH_ACHIEVEMENT_GRADE_GUILD", new Action<string, SystemMessage>((srvMsg, sysMsg) => DoNothing(srvMsg, sysMsg)) },
            { "SMT_ACCOMPLISH_ACHIEVEMENT_GRADE_PARTY", new Action<string, SystemMessage>((srvMsg, sysMsg) => DoNothing(srvMsg, sysMsg)) },

        };

        private static void DoNothing(string srvMsg, SystemMessage sysMsg)
        {
            
        }

        private static void HandleLaurelMessage(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Laurel);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }

        private static void HandleGroupMemberLoot(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Loot);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleGuilBamSpawn(string srvMsg, SystemMessage sysMsg)
        {
            TimeManager.Instance.UploadGuildBamTimestamp();
            TimeManager.Instance.SetGuildBamTime(true);
            TimeManager.Instance.SendWebhookMessageOld();
            var msg = new ChatMessage(srvMsg, sysMsg, (ChatChannel)sysMsg.ChatChannel);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleDungeonEngagedMessage(string srvMsg, SystemMessage sysMsg)
        {
            const string s = "dungeon:";
            var dgId = Convert.ToUInt32(srvMsg.Substring(srvMsg.IndexOf(s, StringComparison.Ordinal) + s.Length));
            InfoWindowViewModel.Instance.EngageDungeon(dgId);

            var msg = new ChatMessage(srvMsg, sysMsg, (ChatChannel)sysMsg.ChatChannel);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleFriendInAreaMessage(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Friend);
            var start = srvMsg.IndexOf("UserName\v") + "UserName\v".Length;
            var end = srvMsg.IndexOf("\v", start);
            var friendName = srvMsg.Substring(start, end- start);
            msg.Author = friendName;
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleQuestMessage(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Quest);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleRessMessage(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Ress);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleDeathMessage(string srvMsg, SystemMessage sysMsg)
        {
            var msg = new ChatMessage(srvMsg, sysMsg, ChatChannel.Death);
            ChatWindowManager.Instance.AddChatMessage(msg);
        }
        private static void HandleInvalidLink(string srvMsg, SystemMessage sysMsg)
        {
            ChatWindowManager.Instance.AddChatMessage(new ChatMessage(srvMsg, sysMsg, (ChatChannel)sysMsg.ChatChannel));
            ChatWindowManager.Instance.RemoveDeadLfg();
            if(SettingsManager.LfgEnabled) WindowManager.LfgListWindow.VM.RemoveDeadLfg();
        }

        private static bool Process(string serverMsg, SystemMessage sysMsg, string opcodeName)
        {
            Delegate type;
            Processor.TryGetValue(opcodeName, out type);
            if (type == null) return false;
            type.DynamicInvoke(serverMsg, sysMsg);
            return true;
        }
        #endregion
    }
}