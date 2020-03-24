using System;
using System.Collections.Generic;
using System.Linq;

namespace WarOfCoin.Scripts.CoreGame.LevelData {

    public class RandomGenerator {

        private readonly Random _random;

        public RandomGenerator() {
            _random = new Random();
        }

        public RandomGenerator(int randomSeed) {
            _random = new Random(randomSeed);
        }

        public int GetRandomNum(int min, int max) {
            return _random.Next(min, max);
        }

        public int GetRandomNumExcept(int min, int max, params int[] exceptNums) {
            var exclude = new HashSet<int>();
            exclude.UnionWith(exceptNums);
            var range = Enumerable.Range(min, max - 1).Where(i => !exclude.Contains(i));

            int index = GetRandomNum(0, max - exclude.Count - min);
            return range.ElementAt(index);
        }

    }

}
