using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects
{
    public class PercentResistanceEffect : PercentEffect {

        public PercentResistanceEffect(int number) {
            setEffectValue(number);
        }

        /*
         * Increase percent damage resistance of self
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId == ownerId) {
                character.percentDamageResist += getEffectValue();
                msg = moveName + " activeted. " + character.Name + " gains more damage resistance";
            }
            return msg;
        }
    }
}