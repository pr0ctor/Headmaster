using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Headmaster.Helper
{
    /*
     *This code was open source on the internet and I in no way claim it as my work, all credit goes to Li Chen 
     */
    // Modify this enum to add number of levels. It will picked up automatically
    enum QueuePriorityEnum
    {
        Low = 0,
        Medium=1,
        High = 2
    }

    class PriorityQueue<T>
    {
        Queue<T>[] _queues;

        public PriorityQueue()
        {
            int levels = Enum.GetValues(typeof(QueuePriorityEnum)).Length;
            _queues = new Queue<T>[levels];
            for (int i = 0; i < levels; i++)
            {
                _queues[i] = new Queue<T>();
            }
        }

        public void Enqueue(QueuePriorityEnum priority, T item)
        {
            _queues[(int)priority].Enqueue(item);
        }

        public int Count
        {
            get
            {
                return _queues.Sum(q => q.Count);
            }
        }

        public T Dequeue()
        {
            int levels = Enum.GetValues(typeof(QueuePriorityEnum)).Length;
            for (int i = levels - 1; i > -1; i--)
            {
                if (_queues[i].Count > 0)
                {
                    return _queues[i].Dequeue();
                }
            }
            throw new InvalidOperationException("The Queue is empty. ");
        }
    }
}