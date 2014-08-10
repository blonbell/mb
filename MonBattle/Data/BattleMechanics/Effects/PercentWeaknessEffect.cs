using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    //Flat Weakness is the polar opposite of resistance. ie) makes you take more damage
    public class PercentWeaknessEffect : PercentEffect {
        public PercentWeaknessEffect(int number) {
            setEffectValue(number);
        }
        
        /*
         * Decrease percent damage resistance of opponent
         */
        public override string applyEffect(CharacterObject character, int ownerId, string moveName) {
            string msg = null;
            if (character.charId != ownerId) {
                character.percentDamageResist -= getEffectValue();
                msg = moveName + "activeted. " + character.Name + " is more vulnerable to damage";
            }
            return msg;
        }
    }
}