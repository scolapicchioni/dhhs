using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01
{
    partial class BankAccount
    {
        private string _owner;
        public uint AccountNumber { get; set; }
        public decimal Credit { get; set; }
        public string Owner
        {
            get
            {
                string o = _owner;
                AnonymiseName(ref o);
                return o;
            }
            set { _owner = value; }
        }

        partial void AnonymiseName(ref string x);
    }
}
