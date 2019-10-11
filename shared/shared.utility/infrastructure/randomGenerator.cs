using System;
using System.Collections.Generic;
using System.Text;

namespace shared.utility.infrastructure {
    public interface IRandomGenerator {
        int Create();
        int Create(string patern = "****");
        int Create(int end = int.MaxValue);
        int Create(int start = 0, int end = int.MaxValue);
    }
    public class RandomGenerator: IRandomGenerator {
        #region Constructor
        private readonly Random _random;
        public RandomGenerator() {
            _random = new Random();
        }
        #endregion
        public int Create() {
            return _random.Next();
        }
        public int Create(string patern) {
            var start = "0";
            var end = "9";
            if(patern.Length > 1) {
                start = "1";
                for(var index = 1; index < patern.Length; index++) {
                    start += "0";
                    end += "9";
                }
            }
            return _random.Next(int.Parse(start), int.Parse(end));
        }
        public int Create(int end) {
            return _random.Next(end);
        }
        public int Create(int start, int end) {
            return _random.Next(start, end);
        }
    }
}
