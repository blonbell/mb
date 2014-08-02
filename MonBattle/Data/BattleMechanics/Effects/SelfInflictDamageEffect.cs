using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects{
    //Damages both the opponent and self (50% of the damage?)
    public class SelfInflictDamageEffect : Effect {
        int effectValue;

        public SelfInflictDamageEffect(int number) {
            effectValue = number;
        }

        /*
         * Does numeric damage to opponent and half to self
         */
        public string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string hurtMsg = null;
            if (character.charId != ownerId) {
                character.flatDamage += effectValue;
                hurtMsg = character.Name + " gets hit by " + moveName;
            } else {
                character.flatDamage += (effectValue / 2);
                hurtMsg = character.Name + " receives partial damage using " + moveName;
            }

            return hurtMsg;
        }
    }
}