using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAP
{
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
        public bool contained(T Value); // проверка входимости
        public bool is_put();           // проверка статуса PUT
        public bool is_delete();        // проверка статуса DELETE
    }

    class ATD_PowerSet<T> : ATD_HashTable<T>
    {
        //конструктор:============
        public ATD_PowerSet(int size);

        // команды:===============
        // предусловие: есть место для нового значения
        // постусловие: добавлен новый элемент
        public void put(T Value);

        // запросы:===============
        // постусловие: создает и возращает пересечение текущего множества и set2
        public ATD_PowerSet<T> Intersection(ATD_PowerSet<T> set2);

        // постусловие: создает и возращает объединение текущего множества и set2
        public ATD_PowerSet<T> Union(ATD_PowerSet<T> set2);

        // постусловие: создает и возращает разницу текущего множества и set2
        public ATD_PowerSet<T> Difference(ATD_PowerSet<T> set2);        
 
        public bool IsSubset(ATD_PowerSet<T> set2); // возвращает true, если set2 есть подмножество текущего множества, иначе false
    }
    */

    public class HashTable
    {
        protected int size;
        protected int step;
        protected string[] slots;
        protected bool put_STATUS;
        protected bool delete_STATUS;

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

        public bool is_put() { return put_STATUS; }

        public bool is_delete() { return delete_STATUS; }

        //================================================
        protected int seekSlot(string value)
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

        protected int find(string Value)
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

        protected int hashFun(string value)
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
    }

    public class PowerSet : HashTable
    {
        //конструктор:============
        public PowerSet(int new_size) : base(new_size) {   }

        // команды:===============
        public void put(string Value)
        {
            int index = find(Value);
            if (index != -1)
            {
                slots[index] = Value;
                put_STATUS = true;
                return;
            }
            index = seekSlot(Value);
            if (index == -1)
            {
                put_STATUS = false;
                return;
            }
            slots[index] = Value;
            put_STATUS = true;
        }

        // запросы: 
        public bool is_subset(PowerSet set2)
        {
            for (int i = 0; i < set2.size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (this.slots[j] == set2.slots[i])
                        break;
                    if (j == size - 1) // дошло до конца set2 и совпадений не найдено       
                        return false;
                }
            }
            return true;
        }

        // постусловие: создает и возращает пересечение текущего множества и set2 (новое множество)
        public PowerSet Intersection(PowerSet set2)
        {
            PowerSet new_set = new PowerSet(size);
            for (int j = 0; j < size; j++)
                for (int i = 0; i < set2.size; i++)
                    if (this.slots[j] == set2.slots[i])
                        new_set.put(slots[j]);
            return new_set;
        }

        // постусловие: создает и возращает объединение текущего множества и set2 (новое множество)
        public PowerSet Union(PowerSet set2)
        {
            PowerSet new_set = new PowerSet(size*2);
            for (int i = 0; i < size; i++)        //copy set to new_set            
                new_set.put(slots[i]);

            for (int j = 0; j < set2.size; j++)  //copy set2 to new_set 
                new_set.put(set2.slots[j]);

            return new_set;
        }

        // постусловие: создает и возращает разницу текущего множества и set2 (новое множество)
        public PowerSet Difference(PowerSet set2)
        {
            PowerSet new_set = new PowerSet(size);
            for (int i = 0; i < set2.size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (this.slots[i] == set2.slots[j])
                        break;
                    if (j == size - 1) // дошло до конца set2 и совпадений не найдено    
                        new_set.put(slots[i]);
                }
            }
            return new_set;
        }

        public static int PowerSet_test()
        {
	        PowerSet Table = new PowerSet(5);
            PowerSet Table2 = new PowerSet(5);
            int test = 0;	        
	        for (int i = 49; i <= 100; i++) // fill the entire array 
	        {                 
                string h = "light " + (char)i;
                Table.put( h );
	        }
            string h1 = "light 0";
            if (Table.contained("light 1") != true) test++;
            Table.delete("light 1");
	        if (Table.is_delete() != true ) test++;
            Table.delete("light 1");
	        if (Table.is_delete() != false ) test++;
            if (Table.contained("light 1") != false) test++;
            if (Table.contained("dark") != false) test++;
  
	        PowerSet Table6 = Table2.Union(Table);
	        for (int i = 48; i <= 59; i++) // fill the entire array 
	        {
                string h = "light " + (char)i;   
		        Table2.put( h );
	        }
            PowerSet Table3 = Table.Intersection(Table2);
            if (Table3.contained("light 0") != false ) test++;
            if (Table3.contained("light 2") != true  ) test++;
            if (Table3.contained("light F") != false ) test++;

            PowerSet Table4 = Table.Union(Table2);
            if (Table4.contained("light 2") != true ) test++;
            if (Table4.contained("light F") != false) test++;
            if (Table4.contained("light 0") != true ) test++;
            if (Table.is_subset(Table3) != true ) test++; 
            if (Table.is_subset(Table2) != false ) test++; 

	        PowerSet set2 = new PowerSet(5);
            string str = "l ";
	        for (int i = 48; i <= 59; i++) // fill the entire array 
	        { 
		        string str1 = str + (char)i;    
		        set2.put( str1 );
	        }
          PowerSet set1 = new PowerSet(5);
	        for (int i = 48; i <= 59; i++) // fill the entire array 
	        {
                string str1 = str + (char)i;    
		        set1.put( str1 );
	        }
	        PowerSet set7 = set1.Intersection(set2);
	        PowerSet set5 = set2.Intersection(set1);

            string str2 = "m 0";
	        for (int i = 48; i <= 59; i++) // fill the entire array 
	        {
                string str1 = str2 + (char)i;     
		        set1.put( str1 );
	        }
          PowerSet set6 = set2.Intersection(set1);
          PowerSet set8 = set1.Intersection(set2);
          PowerSet set3 = set1.Difference(set2);  
          PowerSet set4 = set2.Difference(set1);   
          return test;
        }
  }
}
