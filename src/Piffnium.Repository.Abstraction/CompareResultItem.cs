﻿namespace Piffnium.Repository.Abstraction
{
    public class CompareResultItem
    {
        public int ProcessId { get; set; }

        public string Key { get; set; }

        public bool HasExpect { get; set; }

        public bool HasActual { get; set; }

        public bool HasDiff { get; set; }
    }
}