using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics.Effects {
    public abstract class PercentEffect : Effect {
        double effectValue;

        public void setEffectValue(int number){
            effectValue = Convert.ToDouble(number) / 100;
        }

        public double getEffectValue() {
            return effectValue;
        }

        public abstract string applyEffect(CharacterObject character, int ownerId, string moveName);
    }
}