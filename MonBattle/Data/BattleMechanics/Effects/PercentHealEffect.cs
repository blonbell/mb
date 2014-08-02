using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public class PercentHealEffect : PercentEffect {

        public PercentHealEffect(int number) {
            setEffectValue(number);
        }

        /*
         * Does percent heal to self
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId == ownerId) {
                character.percentHeal += getEffectValue();
                msg = character.Name + " replenishes health from " + moveName;
            }
            return msg;
        }
    }
}