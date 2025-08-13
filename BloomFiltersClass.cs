using System.Collections;

namespace BloomFilters
{
    public class BloomFiltersClass
    {
        private readonly BitArray _bittArray;
        private readonly int _size;
        private readonly int _hashCount;

        public BloomFiltersClass(int s , int h)
        {
            _size = s;
            _hashCount = h;
            _bittArray = new BitArray(_size , false);
        }

        private IEnumerable<int> HashIndex(string username)
        {
            int hash = username.GetHashCode();

            for (int i = 0; i < _hashCount; i++)
            {
                int salt = ("code is hi." + i).GetHashCode();
                int combined = hash ^ salt; 
                combined = (int)((combined * 0x9e3779b1) & 0x7fffffff); 
                yield return combined % _size;
            }
        }


        public void Add(string username)
        {
            foreach (int i in HashIndex(username))
                _bittArray[i] = true;
        }

        public bool CheckUser(string username)
        {
            foreach (int i in HashIndex(username))
            {
                Console.WriteLine(i);
                if (!_bittArray[i])
                    return false;

            }

            return true;
        }
    }
}
