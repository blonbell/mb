using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public class FlatDamageEffect : FlatEffect {

        public FlatDamageEffect(int number) {
            setEffectValue(number);
        }

        /*
         * Does numeric damage to opponent (ie, not self.Id)
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string hurtMsg = null;
            if (character.charId != ownerId) {
                character.flatDamage += getEffectValue();
                hurtMsg = character.Name + " gets hit by " + moveName;
            }
            return hurtMsg;
        }
    }
}