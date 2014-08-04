using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public class FlatResistanceEffect : FlatEffect {
        int effectValue;

        public FlatResistanceEffect(int number) {
            effectValue = number;
        }
        
        /*
         * Increase numeric damage resistance of self
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId == ownerId) {
                character.flatDamageResist += effectValue;
                msg = moveName + " activated. " + character.Name + " gains more damage resistance";
            }
            return msg;
        }
    }
}