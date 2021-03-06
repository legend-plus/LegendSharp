﻿using LegendItems;
using LegendSharp;
using MiscUtil.Conversion;
using MongoDB.Bson;
using Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegendDialogue
{
    public abstract class Substitution
    {
        const short SubId = 0;
        public abstract string Evaluate(Game game);
        public abstract byte[] Encode(BigEndianBitConverter converter);
        public abstract Substitution Simplify(Game game);

        public static Substitution DecodeSubstitution(byte[] data, BigEndianBitConverter converter, int offset = 0)
        {
            short subId = converter.ToInt16(data, offset);
            if (subId == 0)
            {
                DataString dataString = new DataString();
                string text = (string) dataString.decode(data, converter, offset + 2);
                return new StringSubstitution(text);
            }
            else if (subId == 2)
            {
                DataItem dataItem = new DataItem();
                Item item = (Item)dataItem.decode(data, converter, offset + 2);
                return new ItemSubstitution(item);
            }
            else
            {
                return new StringSubstitution("");
            }
        }
    }
}
