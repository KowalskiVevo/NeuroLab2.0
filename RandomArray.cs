using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neuron
{
    public class RandomArray
    {
        public int[] data;
        Random rand = new Random();

        public RandomArray(int count)
        { 
            data = new int[count];    
        }

        public void Randomize()
        { 
            int current = 0;
            int currentRandom;
            bool needAdd = true;

            while (current < data.Length)
            {
                currentRandom = rand.Next(0 , data.Length);
                needAdd = true;

                for (int i = 0; i < current; i++)
                    if (currentRandom == data[i]) needAdd = false;

                if (needAdd) data[current++] = currentRandom;
            }
        }
    }
}
