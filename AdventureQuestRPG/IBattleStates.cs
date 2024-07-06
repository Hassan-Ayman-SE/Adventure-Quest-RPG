using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureQuestRPG
{
    internal interface IBattleStates
    {
        //props
        string Name { get; set; }
        int Health { get; set; }
        int AttackPower { get; set; }
        int Defense { get; set; }
    }
}
