using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public abstract class FlatEffect : Effect {
        int effectValue;

        public void setEffectValue(int number){
            effectValue = number;
        }

        public int getEffectValue() {
            return effectValue;
        }

        public abstract string applyEffect(CharacterObject character, int ownerId, string moveName);
    }
}