﻿using System.Collections.Generic;
using Verse;

namespace BEWH
{
    public class RitualDefModExtension : DefModExtension
    {
        public List<GeneDef> requiredGenes;

        public List<GeneDef> forbiddenGenes;

        public List<GeneDef> givesGenes;

        public List<GeneDef> removesGenes;
    }

}   