//Вставку, замену элемнтов и т.д. не организовывала т.к. предполагаю что этого не предполагает концепция очереди

/*  
class ATD_Queue<T>
{ 
    //=== Конструктор: ============================
     
    public Queue();

    //=== Команды: ================================
     
    // Предусловие: Очерендь не пуста
    // Постусловие: Удаляет объект из начала очереди Queue<T> и возвращает его.
    public void Dequeue();

    // Добавляет объект в конец коллекции Queue<T>.
    public void Enqueue(T value);

    // Постусловие: Удаляет все объекты из Queue<T>.
    public void Clear();

    //=== Запросы: ===============================
     
    public bool Contains(T value);

    public int size();

    // Предусловие: Очерендь не пуста        
    public T Peek(); // Возвращает объект, находящийся в начале очереди Queue<T>, но не удаляет его.
    public bool is_dequeue();
    public bool is_peek();  
} 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAP
{
    class Queue<T>
    {
        private List<T> queue;
        private bool Dequeue_STATUS;
        private bool Peek_STATUS;       
        
        //=== Конструктор: ============================
        public Queue()
        {
            queue = new List<T>();            
            Peek_STATUS = false;
            Dequeue_STATUS = false;
        }

        //=== Команды: ================================
        // Предусловие: Очерендь не пуста
        // Постусловие: Удаляет объект из начала очереди Queue<T> и возвращает его.
        public void Dequeue()
        {
            if (queue.Count() != 0)
            {
                queue.RemoveRange(0,1);
                Dequeue_STATUS = true;
            }
            else
                Dequeue_STATUS = false;
        }

        // Добавляет объект в конец коллекции Queue<T>.
        public void Enqueue(T value)
        {
            queue.Add(value);
        }

        // Постусловие: Удаляет все объекты из Queue<T>.
        public void Clear()
        {
            queue.Clear();
        }

        //=== Запросы: ===============================
        public bool Contains(T value)
        {            
            return queue.Contains(value);
        }

        public int size()
        {
            return queue.Count();
        }

        // Предусловие: Очерендь не пуста        
        public T Peek() // Возвращает объект, находящийся в начале очереди Queue<T>, но не удаляет его.	  
        {
            if (queue.Count() != 0)
            {                
                Peek_STATUS = true;
                return queue.ElementAt(0);
            }
            else
                Peek_STATUS = false;
            return default(T);
        }   

        public bool is_dequeue()
        {
            return Dequeue_STATUS;
        }

        public bool is_peek()
        {
            return Peek_STATUS;
        }

        static public int Queue_Test()
        {
            int test = 0;
            Queue<int> Q = new Queue<int>();
            if (Q.size() != 0) test++;
            Q.Dequeue();  if (Q.is_dequeue()) test++;
            Q.Enqueue(1); if (Q.size() != 1) test++;
            Q.Enqueue(2); if (Q.size() != 2) test++;
            Q.Enqueue(3); if (Q.size() != 3) test++;
            Q.Enqueue(4); if (Q.size() != 4) test++;
            int t;
            Q.Dequeue(); if (!Q.is_dequeue()) test++;
            Q.Dequeue(); if (!Q.is_dequeue()) test++;
            Q.Dequeue(); if (!Q.is_dequeue()) test++;
            t = Q.Peek();  if (!Q.is_peek()) test++; if (t != 4) test++;          
            Q.Dequeue(); if (!Q.is_dequeue()) test++;
            if (Q.size() != 0) test++; 
            t = Q.Peek(); if (Q.is_peek()) test++; if (t != 0) test++;
            return test;
        }
    }
}
