using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab01
{
    partial class BankAccount	{
        partial void AnonymiseName(ref string x)        {
            x = new string('-', x.Length);
        }
	}
}
