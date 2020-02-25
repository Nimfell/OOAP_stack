using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
class ATD_HashTable<T>
{
    //конструктор:============
    public ATD_HashTable(int size);

    // команды:===============
    // предусловие: есть место для нового значения
    // постусловие: добавлен новый элемент
    public void put(T Value);

    // предусловие: значение присутствует в таблице
    // постусловие: значение удалено
    public void delete(T Value);

    //запросы:===============
    public bool contained(T Value);
    public bool is_put();
    public bool is_delete();
}
*/

namespace OOAP
{
    class HashTable
    {
        private int size;
        private int step;
        private string[] slots;

        private bool put_STATUS;
        private bool delete_STATUS;
        
        //конструктор:============
        public HashTable(int new_size)
        {
            size = new_size;
            step = 3;
            slots = new string[size];
            put_STATUS = false;
            delete_STATUS = false;
        }

        // команды:===============
        public void put(string Value)
        {
            int index = seekSlot(Value);
            if (index == -1)
            {
                put_STATUS = false;
                return;
            }
            slots[index] = Value;
            put_STATUS = true;            
        }

        public void delete(string Value)
        {
            int index = find(Value);
            if (index == -1)           
                delete_STATUS = false;
            else
            {
                slots[index] = null;
                delete_STATUS = true;
            }
        }

        //запросы:===============
        public bool contained(string Value)
        {            
            if (find(Value) != -1)
                return true;
            return false;          
        }

        public bool is_put()    { return put_STATUS; }

        public bool is_delete() { return delete_STATUS; }

        //================================================
        private int seekSlot(string value)
        {
            int index = hashFun(value);
            if (index == -1)
                return -1;
            for (int i = 0; i < 3; i++)
            {
                while (index < size)
                {
                    if (slots[index] == null)                                           
                        return index;                    
                    index += step;
                }
                index = index - size;
            }
            for (int i = 0; i < size; i++)            
                if (slots[i] == null)                                    
                    return i; 
            return -1;
        }

        private int find(string Value)
        {
            int index = 0;
            if (Value != null) 
                index = hashFun(Value);
            if (index == -1)
                return -1;
            for (int i = 0; i < 3; i++)
            {
                while (index < size)
                {
                    if (slots[index] == Value)                   
                        return index;                   
                    index += step;
                }
                index = index - size;
            }
            for (int i = 0; i < size; i++)            
                if (slots[i] == Value)              
                    return index;
            return -1;          
        }

        private int hashFun(string value)
        {
            int index = 0;
            for (int i = 0; i < value.Length; i++) // adder of symbols          
                index += (int)value[i];            
            if (index < 0) 
                index *= -1;       // number modulus
            while (index >= size)  // if larger that the size            
                index /= 3;            
            return index;
        }
        //========================================================
        static public int test_hash_table()
        {
            HashTable Table = new HashTable(17);
            string value = "It’s better to light a candle than curse the darkness";
            string val_2 = "You can’t make bricks without straw";
            int Step = Table.step;
            int test = 0;
            // CONTAINED
            if (Table.contained(null) != true) test++; // checking the filling of the empty table
            if (Table.contained("I") != false) test++; // this value is not in the table - don't work
            // PUT
            int index = Table.hashFun(value);        // index = 6
            for (int i = 0; i < Table.size - 1; i++) // fill the entire array except for one cell
            {
                index = (index <= Table.size - 1) ? index : index - Table.size;
                Table.put(value);
                if (Table.put_STATUS != true) test++;
                index += Step;
            }
            // the last empty data cell
            index = (index <= Table.size - 1) ? index : index - Table.size;
            Table.put(val_2);
            if (Table.put_STATUS != true) test++;
            Table.put(val_2);
            if (Table.put_STATUS != false) test++;    //the table is full
            return test;
        }
    }
}
