using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonBattle.Data.BattleMechanics {
    
    public class BattleSystem {
        public CharacterObject self, opponent;
        List<Move> activeMoves;
        public List<string> battleLog;

        public BattleSystem(CharacterObject self, CharacterObject opponent) {
            this.self = self;
            self.Health = self.MaxHealth;
            this.opponent = opponent;
            opponent.Health = opponent.MaxHealth;

            battleLog = new List<string>();
            activeMoves = new List<Move>();
        }

        public void processAttack() {
            Random r = new Random();
            double rN = Convert.ToDouble(r.Next(5));
            double calc = (rN - 2.0f) / 10.0f;
            double percentage = (1.0 + calc);
            int atkPower = Math.Max(0, Convert.ToInt32((self.Attack * percentage) + 0.5));

            string atkStr = String.Format("DMG{0:D3}", atkPower);
            Move plainAttack = new Move("normal attack", atkStr, self.charId);
            activeMoves.Add(plainAttack);
            executeTurn();
        }

        public void processInput(int index) {
            Move choice = self.chosenMoves[index];
            choice.ownerId = self.charId;
            if (self.Meter < choice.meterCost) {
                battleLog.Add("You don't have enough meter to perform " + choice.name);
                return;
            }
            self.Meter -= choice.meterCost;

            string performMove = self.Name + " has performed " + choice.name + "!";
            battleLog.Add(performMove);
            //check for duplicates
            activeMoves.Add(choice);

            executeTurn();
        }

        public void executeTurn() { 
            //opponent selects a move or attack
            Random rand = new Random();
            int r = (int) Math.Floor(rand.NextDouble() * opponent.moveset.Count);
            Move randomMove = opponent.moveset[r];
            randomMove.ownerId = opponent.charId;
            activeMoves.Add(randomMove);

            for (int i = activeMoves.Count -1 ; i >= 0; i--) {
                if(activeMoves[i].applyThisTurn(battleLog)) {
                    activeMoves[i].applyOnCharacter(self, battleLog);
                    activeMoves[i].applyOnCharacter(opponent, battleLog);
                    if (activeMoves[i].isExpired()) {
                        activeMoves.RemoveAt(i);
                    }
                }
            }

            self.resolveBattleCalc(battleLog);
            opponent.resolveBattleCalc(battleLog);
        }

        public bool playerIsWinner() {
            return (self.Health > 0 && opponent.Health <= 0);
        }

        /**
         * Battle Ends while health points are at zero.
         */
        public bool isBattleFinished() {
            return (self.Health <= 0 || opponent.Health <= 0);
        }

        public void clearLog() {
            battleLog.Clear();
        }
    }
}