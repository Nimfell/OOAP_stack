using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAP
{
    /*
    public class ATD_NativeDictionary<T>
    {
        // конструктор
        public ATD_NativeDictionary(int sz); 

        // команды:
        // предусловие: есть место
        // постусловие: значение добавлено
        public void put(string key, T value);

        // предусловие: есть значение
        // постусловие: значение удалено        
        public void delete;

        // запросы: 
        // предусловие: значение присутствует в словаре        
        public T get(string key);

        public bool is_key(string key); // возвращает true если ключ имеется, иначе false
        public bool put_status();
        public bool get_status();
        public bool delete_status();
    }
    */

    public class NativeDictionary<T>
    {
        private string[] slots;
        private T[] values;

        private int size;
        private int step;
        private bool put_STATUS;
        private bool get_STATUS;
        private bool delete_STATUS;

        public NativeDictionary(int Size)
        {
          step = 3;
          size = Size;
          slots = new string[size];
          values = new T[size];
        }

        public void put(string key, T value)
        {
            int index = find(key);
            if (index != -1)
            {
                values[index] = value;
                put_STATUS = true; 
                return;
            } 
            index = seekSlot(key);
            if (index == -1)
            {
                put_STATUS = false;
                return;
            }
            values[index] = value;
            slots[index] = key;
            put_STATUS = true;            
        }

        public void delete(string Key)
        {
            int index = find(Key);
            if (index == -1)           
                delete_STATUS = false;
            else
            {
                slots[index] = null;
                values[index] = default(T);
                delete_STATUS = true;
            }
        }

        //запросы:===============
        public bool is_key(string Key)
        {
            if (find(Key) != -1)
                return true;
            return false;          
        }

        T get(string key)
        {
            int index = find(key);
            if (index != -1)
            {
                get_STATUS = true;
                return values[index];
            }
            get_STATUS = false;
            return default(T);
        }

        public bool put_status() { return put_STATUS; }

        public bool get_status() { return get_STATUS; }

        public bool delete_status() { return delete_STATUS; }       

        //======================================================
        private int seekSlot(string key)
        {
            int index = hashFun(key);
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

        private int find(string key)
        {
            int index = 0;
            if (key != null)
                index = hashFun(key);
            if (index == -1)
                return -1;
            for (int i = 0; i < 3; i++)
            {
                while (index < size)
                {
                    if (slots[index] == key)
                        return index;
                    index += step;
                }
                index = index - size;
            }
            for (int i = 0; i < size; i++)
                if (slots[i] == key)
                    return i;
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

        public static int test_NativeDictionary()
        {
            NativeDictionary<int> item = new NativeDictionary<int>(6);
            int test = 0;
            item.put("key 1", 1);
            if (item.hashFun("key 1") != 5) test++;
            item.put("key 2", 2);
            if (item.is_key("key 8")) test++;
            if (item.get("key 8") != 0) test++;


            item.put("key 4", 4);
            item.put("key 5", 5);


            if (!(item.is_key("key 4"))) test++;
            if (item.get("key 4") != 4) test++;
            item.put("key 4", 104);
            if (item.get("key 4") != 104) test++;

            if (!(item.is_key("key 5"))) test++;
            if (item.get("key 5") != 5) test++;
            item.put("key 5", 105);
            if (item.get("key 5") != 105) test++;

            item.put("key 3", 3);
            item.put("key 6", 6);
            item.put("key 7", 7);
            item.put("key 8", 8);

            if (item.is_key("key 8")) test++;
            if (item.get("key 8") != 0) test++;

            return test;
        }
    } 
}


