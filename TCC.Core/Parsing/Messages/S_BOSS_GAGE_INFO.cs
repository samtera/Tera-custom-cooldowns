﻿using TCC.TeraCommon.Game.Messages;
using TCC.TeraCommon.Game.Services;

namespace TCC.Parsing.Messages
{
    public class S_BOSS_GAGE_INFO : ParsedMessage
    {
        private ulong id, targetId;
        private int templateId, huntingZoneId;//, unk1;
        private float /*hpDiff,*/ currHp, maxHp;
        private byte enrage/*, unk3*/;

        public ulong EntityId { get => id; } 
        public int TemplateId { get => templateId; }
        public int HuntingZoneId { get => huntingZoneId; }
        public float CurrentHP { get => currHp; }
        public float MaxHP { get => maxHp; }
        public ulong Target { get => targetId; }

        public S_BOSS_GAGE_INFO(TeraMessageReader reader) : base(reader)
        {
            id = reader.ReadUInt64();
            huntingZoneId = reader.ReadInt32();
            templateId = reader.ReadInt32();
            targetId = reader.ReadUInt64();
            reader.Skip(4); //unk1 = reader.ReadInt32();
            //if (reader.Version < 321550 || reader.Version > 321600)
            //{
                enrage = reader.ReadByte();
                currHp = reader.ReadInt64();
                maxHp = reader.ReadInt64();

            //}
            //else
            //{

            //    hpDiff = reader.ReadSingle();
            //    enrage = reader.ReadByte();
            //    currHp = reader.ReadSingle();
            //    maxHp = reader.ReadSingle();
            //}

            //unk3 = reader.ReadByte();
        }
    }
}