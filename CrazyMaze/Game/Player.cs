using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyMaze.Game
{
    public class Player
    {
        public Stack<int> Cards = new Stack<int>();

        public void AddCard(int amount)
        {
            for (int i = 0; i < amount; i++)
                Cards.Push(_GetUniqueID(amount));
            
        }

        private int _GetUniqueID(int max)
        {
            if (GameManager.UsedCards.Count >= max)
                GameManager.UsedCards.Clear();

            int num = GameManager.RANDOM.Next(0, max);
            if (GameManager.UsedCards.Contains(num))
                return _GetUniqueID(max);

            GameManager.UsedCards.Add(num);
            return num;
        }
    }
}
