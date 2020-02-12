//============   I задание: АТД Deque и его реализация   ==============
/*  
class ATD_Deque<T>
{ 
    //=== Конструктор: ============================
     
    public Deque();

    //=== Команды: ================================
     
    // Предусловие: Очерендь не пуста
    // Постусловие: Удаляет объект из начала очереди 
    public void remove_head();

    // Добавляет объект в конец очереди 
    public void add_tail(T value);
 
    // Предусловие: Очерендь не пуста
    // Постусловие: Удаляет объект с конца очереди
    public void remove_tail();

    // Добавляет объект в начало очереди.
    public void add_head(T value);

    // Постусловие: Удаляет все объекты из Queue<T>.
    public void clear();

    //=== Запросы: ===============================
 
    // Предусловие: Очерендь не пуста        
    public T get_head(); 
    // Предусловие: Очерендь не пуста  
    public T get_tail();
 
    public int size();
    public bool is_remove_head();
    public bool is_remove_tail();
    public bool is_get_head();
    public bool is_get_tail();
} 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAP
{
    class Deque<T>
    {
        private List<T> deque;
        private bool get_head_STATUS;
        private bool get_tail_STATUS;
        private bool remove_head_STATUS;
        private bool remove_tail_STATUS;

        //=== Конструктор: ============================
        public Deque()
        {
            deque = new List<T>();
            get_head_STATUS = false;
            get_tail_STATUS = false; 
            remove_head_STATUS = false;
            remove_tail_STATUS = false;  
        }

        //=== Команды: ================================
        public void add_tail(T value) { deque.Add(value); }

        public void add_head(T value) { deque.Insert(0, value); }

        public void clear() { deque.Clear(); }

        public void remove_head()
        {
            if (deque.Count() != 0) {
                deque.RemoveRange(0, 1);
                remove_head_STATUS = true;
            } else
                remove_head_STATUS = false;
        }

        public void remove_tail()
        {
            if (deque.Count() != 0) {
                deque.RemoveRange( deque.Count() - 1, 1);
                remove_tail_STATUS = true;
            } else
                remove_tail_STATUS = false;
        }

        //=== Запросы: ===============================
        public T get_head() 
        {
            if (deque.Count() != 0) {
                get_head_STATUS = true;
                return deque.ElementAt(0);
            } else
                get_head_STATUS = false;
            return default(T);
        }

        public T get_tail()
        {
            if (deque.Count() != 0) {
                get_tail_STATUS = true;
                return deque.ElementAt(deque.Count() - 1);
            } else
                get_tail_STATUS = false;
            return default(T);
        }

        public int size() { return deque.Count(); }

        public bool is_remove_head() { return remove_head_STATUS; }

        public bool is_remove_tail() { return remove_tail_STATUS; }

        public bool is_get_head()    { return get_head_STATUS; }       

        public bool is_get_tail()    { return get_tail_STATUS; }

/*      static public int Deque_Test()
        {
            int test = 0;
            Deque<int> D = new Deque<int>();
            if (D.size() != 0) test++;
            D.remove_head(); if (D.is_remove_head()) test++;
            D.add_tail(1); if (D.size() != 1) test++;
            D.add_tail(2); if (D.size() != 2) test++;
            D.add_tail(3); if (D.size() != 3) test++;
            D.add_tail(4); if (D.size() != 4) test++;
            int t;
            D.remove_head(); if (!D.is_remove_head()) test++;
            D.remove_head(); if (!D.is_remove_head()) test++;
            D.remove_head(); if (!D.is_remove_head()) test++;
            t = D.get_head(); if (!D.is_get_head()) test++; if (t != 4) test++;
            D.remove_head(); if (!D.is_remove_head()) test++;
            if (D.size() != 0) test++;
            t = D.get_head(); if (D.is_get_head()) test++; if (t != 0) test++;
            
            D.remove_tail(); if (D.is_remove_tail()) test++;
            D.add_head(1); if (D.size() != 1) test++;
            D.add_head(2); if (D.size() != 2) test++;
            D.add_head(3); if (D.size() != 3) test++;
            D.add_head(4); if (D.size() != 4) test++;            
            D.remove_tail(); if (!D.is_remove_tail()) test++;
            D.remove_tail(); if (!D.is_remove_tail()) test++;
            D.remove_tail(); if (!D.is_remove_tail()) test++;
            t = D.get_tail(); if (!D.is_get_tail()) test++; if (t != 4) test++;
            D.remove_tail(); if (!D.is_remove_tail()) test++;
            if (D.size() != 0) test++;
            t = D.get_tail(); if (D.is_get_tail()) test++; if (t != 0) test++;
            return test;
        } */
    }
}


//============   II задание: Создание иерархии классов Deque и Queue   ==============
/*
class ParentQueue<T>
{ 
    //=== Конструктор: ============================

    public ParentQueue();

    //=== Команды: ================================
     
    // Предусловие: Очерендь не пуста
    // Постусловие: Удаляет объект из начала очереди 
    public void remove_head();

    // Добавляет объект в конец очереди 
    public void add_tail(T value);

    // Постусловие: Удаляет все объекты из Queue<T>.
    public void clear();

    //=== Запросы: ===============================
 
    // Предусловие: Очерендь не пуста        
    public T get_head(); 
 
    public int size();
    public bool is_remove_head();
    public bool is_get_head();
}

class ATD_Queue<T> : ParentQueue<T>
{
    //=== Конструктор: ============================
    public ATD_Queue();
}

class ATD_Deque<T> : ParentQueue<T>
{
    //=== Конструктор: ============================

    public ATD_Deque();

    //=== Команды: ================================

    // Предусловие: Очерендь не пуста
    // Постусловие: Удаляет объект с конца очереди
    public void remove_tail();

    // Добавляет объект в начало очереди.
    public void add_head(T value);

    //=== Запросы: ===============================

    // Предусловие: Очерендь не пуста  
    public T get_tail();

    public bool is_remove_tail();
    public bool is_get_tail();
}
*/
