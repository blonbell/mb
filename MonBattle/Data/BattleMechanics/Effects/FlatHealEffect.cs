using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public class FlatHealEffect : FlatEffect {
        int effectValue;

        public FlatHealEffect(int number) {
            effectValue = number;
        }

        /*
         * Does numeric heal to self
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId == ownerId) {
                character.flatHeal += effectValue;
                msg = character.Name + " replenishes health from " + moveName;
            }
            return msg;
        }
    }
}