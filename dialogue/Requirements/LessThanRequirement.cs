﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LegendDialogue
{
    public class LessThanRequirement : Requirement
    {
        String flagName;
        NumericalFlag value;
        public LessThanRequirement(String flagName, NumericalFlag value)
        {
            this.flagName = flagName;
            this.value = value;
        }

        public override bool Validate(Dictionary<String, Flag> flags)
        {
            if (flags.ContainsKey(flagName))
            {
                if (flags[flagName] is NumericalFlag && (NumericalFlag)flags[flagName] < value)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
