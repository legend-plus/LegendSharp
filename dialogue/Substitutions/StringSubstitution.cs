﻿using LegendSharp;
using MiscUtil.Conversion;
using MongoDB.Bson;
using Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace LegendDialogue
{
    public class StringSubstitution : Substitution
    {
        const short SubId = 0;
        string text;
        public StringSubstitution(string text)
        {
            this.text = text;
        }

        public override byte[] Encode(BigEndianBitConverter converter)
        {
            byte[] idData = converter.GetBytes(SubId);
            DataString stringData = new DataString();
            byte[] textData = stringData.encode(text, converter);

            byte[] output = new byte[textData.Length + 2];

            System.Buffer.BlockCopy(idData, 0, output, 0, 2);
            System.Buffer.BlockCopy(textData, 0, output, 2, textData.Length);

            return output;
        }

        public override string Evaluate(Game game)
        {
            return text;
        }

        public override Substitution Simplify(Game game)
        {
            return this;
        }
    }
}
