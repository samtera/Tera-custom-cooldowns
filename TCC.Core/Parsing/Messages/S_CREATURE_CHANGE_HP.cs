﻿using TCC.TeraCommon.Game.Messages;
using TCC.TeraCommon.Game.Services;

namespace TCC.Parsing.Messages
{
    public class S_CREATURE_CHANGE_HP : ParsedMessage
    {
        private long currentHP, maxHP, diff;
        private uint type;
        private ulong target, source;
        private byte crit;

        public long CurrentHP { get => currentHP; }
        public long MaxHP { get => maxHP; }
        public long Diff { get => diff; }
        public ulong Target { get => target; }
        public ulong Source { get => source; }
        public byte Crit { get => crit; }

        public S_CREATURE_CHANGE_HP(TeraMessageReader reader) : base(reader)
        {
            //if (reader.Version < 321550 || reader.Version > 321600)
            //{
                currentHP = reader.ReadInt64();
                maxHP = reader.ReadInt64();
                diff = reader.ReadInt64();

            //}
            //else
            //{
            //    currentHP = reader.ReadInt32();
            //    maxHP = reader.ReadInt32();
            //    diff = reader.ReadInt32();
            //}
            type = reader.ReadUInt32();
            target = reader.ReadUInt64();
            source = reader.ReadUInt64();
            crit = reader.ReadByte();
        }
    }
}
