using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonBattle.Data.BattleMechanics {
    interface Effect {
        string applyEffect(CharacterObject character, int ownerId, string moveName);
    }
}
