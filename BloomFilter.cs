using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OOAP
{/*
    class ATD_BloomFilter
    {
        // конструктор?
        public ATD_BloomFilter(int size);

        // команда:
        // предусловия - нет
        // постусловие: в нулевые поля забиты единицы по 2м адресам
        public void add(string str1);

        // запрос:
        public bool is_value(string str1); // есть или нет значение
    }
  * */

    public class BloomFilter
    {
        private int  filter_len;
        private long filter;
      
        //конструктор:===================
        public BloomFilter(int f_len)
        {
            filter_len = f_len;   // filter size - m
            filter = 0;           // filter
        }

        //команда:======================
        void add(string str1)
        {
            filter |= hash1(str1);
            filter |= hash2(str1);
        }

        // запрос:=====================
        bool is_value(string str1)
        {
            long mask = 0;
            mask |= hash1(str1);     
            mask |= hash2(str1);
            if ( (filter & mask) == mask )
                return true;
            return false;
        }

        //============================
        private long hash1(string str1)
        {
            int number = 17;
            int iteration = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                int code = str1[i];
                iteration = (iteration * number + code) % filter_len;
                if (iteration < 0)
                    iteration *= -1;
            }
            long mask = 1 << iteration;
            return mask;
        }

        private long hash2(string str1)
        {
            int number = 223;
            int iteration = 0;
            for (int i = 0; i < str1.Length; i++)
            {
                int code = str1[i];
                iteration = (iteration * number + code) % filter_len;
                if (iteration < 0)
                    iteration *= -1;
            }
            long mask = 1 << iteration;
            return mask;
        }

        public static int Bloom_test()
        {
          int test = 0;
          BloomFilter Filter = new BloomFilter(32);
          Filter.add("Filter");
          Filter.add("Bloom");

          if (!Filter.is_value("Bloom"))      test++;
          if (!Filter.is_value("Filter"))     test++;
          if (Filter.is_value("0231589674"))  test++;
      
          return test;
        }
    };
}
