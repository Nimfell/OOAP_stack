/*=======  ATD:  =========
public class DynArray<T>
{
    // конструктор
    public DynArray();

    // команды:
    // постусловие: список обнулен
       public void reset();

    // постусновие: добавлен новый элемент в конец массива, 
    //              возможно увеличение емкости массава
    public void append(int value);

    // предусловие: индекс элемента входит в размерность массива count. включительно 
    // постусновие: добавлен новый элемент в позицию index, 
    //              элементы справа сдвинуты на один вправо
    //              возможно увеличение емкости массава
    public void insert(int value, int index);

    // предусловие: индекс элемента входит в размерность массива count. включительно 
    // постусновие: удален объект из позиции index, 
    //              возможно сжатие буфера capacity.
    public void remove(int index);

    //запросы

    // предусловие: индекс элемента входит в размерность массива count. включительно 
    public int get_item(int index);
    public int get_count();
    public int capacity();

    public bool get_insert_status();
    public bool get_remove_status();
    public bool get_item_status();
}*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynArray
{
    public class DynArray<T>
    {
        private int count;
        private int capacity;
        private int[] array;   //link to the array
        private bool GET_ITEM_STATUS;
        private bool INSERT_STATUS;
        private bool REMOVE_STATUS;

        // конструктор: -----------------------------
        public DynArray()
        {
            count = 0;
            make_array(16);
            GET_ITEM_STATUS = false;
            INSERT_STATUS = false;
            REMOVE_STATUS = false;
        }

        // команды: ---------------------------------        
        public void reset()
        {
            count = 0;
            make_array(16);
            GET_ITEM_STATUS = false;
            INSERT_STATUS = false;
            REMOVE_STATUS = false;
        }

        private void make_array(int new_capacity)
        {
            if (new_capacity < 16)
                new_capacity = 16;
            int[] new_array = new int[new_capacity];
            for (int i = 0; i < count; i++) // copy array into new array to the 'count'
                new_array[i] = array[i];
            capacity = new_capacity;
            array = new_array;
        }

        public void append(int value)
        {
            if (count == capacity)
                make_array(capacity * 2);
            array[count] = value;
            count++;
        }

        public void insert(int value, int index)
        {
            if (index > count)
                INSERT_STATUS = false; //item was not found  
            else
            {
                if (index == count)
                    append(value);
                else
                {
                    if (count == capacity)
                        make_array(capacity * 2);
                    for (int i = count; i > index; i--) //copy to the right            
                        array[i] = array[i - 1];
                    array[index] = value;
                    count++;
                }
                INSERT_STATUS = true;
            }
        }

        public void remove(int index)
        {
            if (index >= count)
                INSERT_STATUS = false; //item was not found
            else
            {
                for (int i = index; i < count; i++) //copy to the left            
                    array[i] = array[i + 1];
                if (count <= capacity / 2 && capacity != 16) //if less than 50% 
                    make_array(capacity * 2 / 3);
                count--;
                INSERT_STATUS = true;
            }
        }

        //запросы: ----------------------------------    
        public int get_item(int index)
        {
            if (index >= count)
            {
                GET_ITEM_STATUS = false;
                return -1;
            }
            GET_ITEM_STATUS = true;
            return array[index];
        }

        public int get_count()
        { return count; }

        public int get_capacity()
        { return capacity; }

        public bool get_append_status()
        { return GET_ITEM_STATUS; }

        public bool get_insert_status()
        { return INSERT_STATUS; }

        public bool get_remove_ststus()
        { return REMOVE_STATUS; }
    }
}

