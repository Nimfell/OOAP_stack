using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
class BoundedStack<T>
{
    public const int POP_NIL = 0; // pop() ещё не вызывалась
    public const int POP_OK = 1;  // последняя pop() отработала нормально
    public const int POP_ERR = 2; // стек пуст

    public const int PEEK_NIL = 0; // peek() ещё не вызывалась
    public const int PEEK_OK = 1;  // последняя peek() вернула корректное значение 
    public const int PEEK_ERR = 2; // стек пуст

    public const int PUSH_NIL = 0; // push() ещё не вызывалась
    public const int PUSH_OK = 1;  // последняя push() отработала нормально
    public const int PUSH_ERR = 2; // стек переполнен

    // конструкторы
    public BoundedStack<T> BoundedStack(); // постусловие: создан новый пустой стек с макимальным размером по умолчанию
    public BoundedStack<T> BoundedStack(int size); // постусловие: создан новый пустой стек с заданным максимальным размером

    // команды:
    // предусловие: в стеке есть свободное место
    // постусловие: в стек добавлено новое значение
    public void push(T value); 

    // предусловие: стек не пустой; 
    // постусловие: из стека удалён верхний элемент
    public void pop(); 

    // постусловие: из стека удалятся все значения
    public void clear();

    // запросы:    
    public T peek();  // предусловие: стек не пустой
    public int size();

    // дополнительные запросы:
    public int get_pop_status();  // возвращает значение POP_*
    public int get_peek_status(); // возвращает значение PEEK_*
    public int get_push_status(); // возвращает значение PUSH_*
}
 */

namespace Stack
{
  class BoundedStack <T>
  {
      // константы
      private const int MAX_SIZE = 32;  // размер по умолчанию

      // скрытые поля
      private List<T> stack;   
      private int peek_status; 
      private int pop_status;  
      private int push_status;          // статус команды add() 
      private int stack_size;           // ограничение размера стека      

      // интерфейс класса, реализующий АТД Stack  
      public const int POP_NIL = 0;
      public const int POP_OK = 1;
      public const int POP_ERR = 2;
      public const int PEEK_NIL = 0;
      public const int PEEK_OK = 1;
      public const int PEEK_ERR = 2;
      public const int PUSH_NIL = 0; // без статуса 
      public const int PUSH_OK = 1;  // добавление выполнено
      public const int PUSH_ERR = 2; // стек переполнен

      public BoundedStack()  // конструктор
      {   
          clear();
          stack_size = MAX_SIZE;
      }

      public BoundedStack(int size)  // конструктор
      {
          clear();
          stack_size = size;
      }

      public void push(T value)
      {
        if (size() < stack_size) // если стек не заполнен
        { stack.Add(value);
          push_status = PUSH_OK;
        }
        else         
          push_status = PUSH_ERR;        
      }

      public void pop()
      {
        if (size() > 0)
        {
          stack.RemoveAt( size()-1 ); // удаление с хвоста
          pop_status = POP_OK;
        }
        else
          pop_status = POP_ERR;
      }

      public void clear()
      { 
        stack = new List<T>(stack_size); // пустой список/стек
        // начальные статусы для предусловий peek(), pop() и push()
        peek_status = PEEK_NIL;
        pop_status  = POP_NIL;
        push_status = PUSH_NIL;
      }

      public T peek()
      {
        T result = stack.ElementAtOrDefault( size()-1 ); // возвращаем хвост либо значение по умолчанию для типа T
        if (size() > 0)         
          peek_status = PEEK_OK;        
        else         
          peek_status = PEEK_ERR;        
        return result;
      }

      public int size()
      { return stack.Count;
      }

      // запросы статусов
      public int get_pop_status() 
      { return pop_status;
      }

      public int get_peek_status()
      { return peek_status;
      }

      public int get_push_status()
      { return push_status;
      }
    }

  /*    class Program
      {
          static void Main(string[] args)
          {
              int test = 0;
              BoundedStack<int> stack1 = new BoundedStack<int>(3);

              // Заполнение
              stack1.push(1);
              if (stack1.peek() == 1 && stack1.get_push_status() == BoundedStack<int>.PUSH_OK)            
                  test++;            
              stack1.push(2);
              if (stack1.peek() == 2 && stack1.get_push_status() == BoundedStack<int>.PUSH_OK)
                  test++;
              stack1.push(3);
              if (stack1.peek() == 3 && stack1.get_push_status() == BoundedStack<int>.PUSH_OK)
                  test++;
              stack1.push(4); // переполнен
              if (stack1.peek() == 3 && stack1.get_push_status() == BoundedStack<int>.PUSH_ERR)
                  test++;
                    
              // Опустошение
              stack1.pop(); // удалить 3
              if ( stack1.peek() == 2 &&
                   stack1.get_peek_status() == BoundedStack<int>.PEEK_OK &&
                   stack1.get_pop_status()  == BoundedStack<int>.POP_OK)
                  test++;

              stack1.pop(); // удалить 2
              if ( stack1.peek() == 1 &&
                   stack1.get_peek_status() == BoundedStack<int>.PEEK_OK &&
                   stack1.get_pop_status() == BoundedStack<int>.POP_OK)
                  test++;

              stack1.pop(); // удалить 1
              if ( stack1.peek() == 0 && 
                   stack1.get_peek_status() == BoundedStack<int>.PEEK_ERR &&
                   stack1.get_pop_status() == BoundedStack<int>.POP_OK)
                  test++;

              stack1.pop(); // стек пуст
              if ( stack1.peek() == 0 &&
                   stack1.get_peek_status() == BoundedStack<int>.PEEK_ERR &&
                   stack1.get_pop_status() == BoundedStack<int>.POP_ERR)
                  test++;

              if (test == 8)
                  Console.WriteLine("Тест прошел успешно");               
          }
      }*/
}
  

