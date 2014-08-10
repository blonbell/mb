using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public class PercentDamageEffect : PercentEffect {
        public PercentDamageEffect(int number) {
            setEffectValue(number);
        }

        /*
         * Does percent damage to opponent (ie, not self.Id)
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId != ownerId) {
                character.percentDamage += getEffectValue();
                msg = character.Name + " gets hit by " + moveName;
            }
            return msg;
        }
    }
}