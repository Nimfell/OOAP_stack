using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* 
public class ParentList<T> where T : IComparable<T>
{
    public const int HEAD_STATUS = 0;
    public const int TAIL_STATUS = 1;
    public const int RIGHT_STATUS = 2;
    public const int LEFT_STATUS = 3;
    public const int PUT_RIGHT_STATUS = 4;
    public const int PUT_LEFT_STATUS = 5;
    public const int REMOVE_STATUS = 6;
    public const int ADD_TAIL_STATUS = 7;
    public const int REPLACE_STATUS = 8;
    public const int FIND_STATUS = 9;
    public const int REMOVE_ALL_STATUS = 10;
    public const int GET_COMMAND_STATUS = 11;

    public const int NIL = 0; // команда не вызывалась
    public const int OK = 1;  // команда отработала нормально
    public const int ERR = 2; // ошибка при выполнении команды 

    // конструктор:
    public ParentList() // постусловие: создан новый пустой лист

    // команды:

    // предусловие: лист не пустой
    // постусловие: курсор установлен на первый узел
    public void head()

    // предусловие: лист не пустой
    // постусловие: курсор установлен на последний узел
    public void tail()

    // предусловие: справа есть узел
    // постусловие: курсор установлен на один узел вправо 
    public void right()

    // предусловие: курсор установлен
    // постусловие: вставлен новый узел следом за текущим узлом с заданным значением;
    public void put_right(T val)

    // предусловие: курсор установлен
    // постусловие: вставлен новый узел перед за текущим узлом с заданным значением;
    public void put_left(T val)

    // предусловие: курсор установлен
    // постусловие: удален текущий элемент, 
    //              курсор переставлен на левый или правый узел или убран, если лист пуст.
    public void remove()

    // постусловие: лист пуст, все значения выставлены в первоначальные
    public void clear()

    //--------------------
    // постусловие: в хвост добавлен новый узел с заданным значением
    public void add_tail(T val)

    // предусловие: курсор установлен
    // постусловие: значение текущего узла заменено на заданное;   
    public void replace(T val)

    // предусловие: курсор установлен
    // постусловие: курсор установлен на следующий узел  с искомым значением (по отношению к текущему узлу)
    public void find(T val)

    // предусловие: в листе есть хотя бы один узел с заданным значением
    // постусловия: удалены все узлы с заданным значением, 
    //              если на удаленном узле стоял курсор, то он смещается по правилу remove()
    public void remove_all(T val)

    // напечатан список
    public void print_list()

    // ================================================ 
    // запросы: 

    // предусловие: курсор установлен
    public T get() //-- получить значение текущего узла;

    public int size() //-- посчитать количество узлов в списке.

    //--------------------
    public bool is_head()  //-- находится ли курсор в начале списка?

    public bool is_tail()  //-- находится ли курсор в конце списка? 

    public bool is_value() //-- установлен ли курсор на какой-либо узел в списке (по сути, непустой ли список)

    // предусловие: запрашиваемый статус команды есть в перечислении
    public int get_command_status(int COMMAND) // возвращает статус выполнения запрашиваемой команды.

    public bool last_command_is_success() // возвращает статус выполнения запрашиваемой команды.    
}

public class LinkedList<T> : ParentList<T> where T : IComparable<T>
{
    public LinkedList()
}

public class TwoWayList<T> : ParentList<T> where T : IComparable<T>
{
    public TwoWayList()

    // предусловие: слева есть узел
    // постусловие: курсор установлен на один узел вправо 
    public void left()
}
 */

namespace LinkedLists
{
    public class ParentList<T> where T : IComparable<T>
    {
        protected class Node<T>
        {
            public T value;
            public Node<T> next;
            public Node<T> prev;

            public Node(T val)
            {
                value = val;
                next = null;
                prev = null;
            }
        };
        protected Node<T> Head;
        protected Node<T> Tail;
        protected Node<T> Cursor;

        public  const int HEAD_STATUS = 0;
        public  const int TAIL_STATUS = 1;
        public  const int RIGHT_STATUS = 2;
        public  const int LEFT_STATUS = 3;
        public  const int PUT_RIGHT_STATUS = 4;
        public  const int PUT_LEFT_STATUS = 5;
        public  const int REMOVE_STATUS = 6;
        public  const int ADD_TAIL_STATUS = 7;
        public  const int REPLACE_STATUS = 8;
        public  const int FIND_STATUS = 9;
        public  const int REMOVE_ALL_STATUS = 10;
        public  const int GET_COMMAND_STATUS = 11;
        protected const int count_of_commands = 12;
        protected int[] status_table;
        protected int last_command;

        public const int NIL = 0; // команда не вызывалась
        public const int OK = 1;  // команда отработала нормально
        public const int ERR = 2; // ошибка при выполнении команды 

        // конструктор:
        public ParentList() // постусловие: создан новый пустой лист
        {
            Head = null;
            Tail = null;
            Cursor = null;
            status_table = new int[count_of_commands];
            for (int i = 0; i < count_of_commands; i++)
            {
                status_table[i] = NIL;
            }
        }       

        // команды:

        // предусловие: лист не пустой
        // постусловие: курсор установлен на первый узел
        public void head()
        {
            if (Head != null)
            {
                Cursor = Head;
                set_new_status(HEAD_STATUS, OK);
            }
            else
                set_new_status(HEAD_STATUS, ERR);
        }

        // предусловие: лист не пустой
        // постусловие: курсор установлен на последний узел
        public void tail()
        {
            if (Tail != null)
            {
                Cursor = Tail;
                set_new_status(TAIL_STATUS, OK);
            }
            else
                set_new_status(TAIL_STATUS, ERR);
        }

        // предусловие: справа есть узел
        // постусловие: курсор установлен на один узел вправо 
        public void right()
        {
            if (Cursor != null && Cursor.next != null)
            {
                Cursor = Cursor.next;
                set_new_status(RIGHT_STATUS, OK);
            }
            else
                set_new_status(RIGHT_STATUS, ERR);
        }

        // предусловие: курсор установлен
        // постусловие: вставлен новый узел следом за текущим узлом с заданным значением;
        public void put_right(T val)
        {
            if (Cursor != null)
            {
                if (Cursor == Tail)
                    add_tail(val);
                else
                {
                    Node<T> NODE = new Node<T>(val);
                    NODE.next = Cursor.next;
                    NODE.prev = Cursor;
                    Cursor.next = NODE;
                    NODE.next.prev = NODE;
                }
                set_new_status(PUT_RIGHT_STATUS, OK);
            }
            else
                set_new_status(PUT_RIGHT_STATUS, ERR);
        }

        // предусловие: курсор установлен
        // постусловие: вставлен новый узел перед за текущим узлом с заданным значением;
        public void put_left(T val)
        {
            if (Cursor != null)
            {
                Node<T> NODE = new Node<T>(val);
                if (Cursor == Head)
                    Head = NODE;
                else
                {
                    NODE.prev = Cursor.prev;
                    NODE.prev.next = NODE;
                }
                NODE.next = Cursor;
                Cursor.prev = NODE;
                set_new_status(PUT_LEFT_STATUS, OK);
            }
            else
                set_new_status(PUT_LEFT_STATUS, ERR);
        }

        // предусловие: курсор установлен
        // постусловие: удален текущий элемент, 
        //              курсор переставлен на левый или правый узел или убран, если лист пуст.
        public void remove()
        {
            if (Cursor != null)
            {
                if (Head == Tail)
                    clear();
                else
                {
                    if (Cursor == Head)
                    {
                        Head = Cursor.next;
                        Head.prev = null;
                        Cursor = Head;
                    }
                    else
                    {
                        if (Cursor == Tail)
                        {
                            Tail = Cursor.prev;
                            Tail.next = null;
                            Cursor = Tail;
                        }
                        else
                        {
                            Cursor.prev.next = Cursor.next;
                            Cursor.next.prev = Cursor.prev;
                            Cursor = Cursor.prev;
                        }
                    }
                }
                set_new_status(REMOVE_STATUS, OK);
            }
            else
                set_new_status(REMOVE_STATUS, ERR);
            // удалить узел на котором стоял курсор, если нужно, но не тут
        }

        // постусловие: лист пуст, все значения выставлены в первоначальные
        public void clear()
        {
            Head = null;
            Tail = null;
            Cursor = null;
            status_table = new int[count_of_commands];
            for (int i = 0; i < count_of_commands; i++)
            {
                status_table[i] = NIL;
            }
        }

        //--------------------
        // постусловие: в хвост добавлен новый узел с заданным значением
        public void add_tail(T val)
        {
            Node<T> NODE = new Node<T>(val);
            if (Head == null)
            {
                Head = NODE;
                Head.next = null;
                Head.prev = null;
                Cursor = Head; // курсор ставим на единственный элемент
            }
            else
            {
                Tail.next = NODE;
                NODE.prev = Tail;
            }
            Tail = NODE;
        }

        // предусловие: курсор установлен
        // постусловие: значение текущего узла заменено на заданное;   
        public void replace(T val)
        {
            if (Cursor != null)
            {
                Cursor.value = val;
                set_new_status(REPLACE_STATUS, OK);
            }
            else
                set_new_status(REPLACE_STATUS, ERR);
        }

        // предусловие: курсор установлен
        // постусловие: курсор установлен на следующий узел  с искомым значением (по отношению к текущему узлу)
        public void find(T val)
        {
            Node<T> remembed = Cursor;
            if (Cursor != null)
            {
                while (Cursor != null)
                {
                    if (Cursor.value.CompareTo(val) == 0)
                    { // если значения равны
                        set_new_status(FIND_STATUS, OK);
                        return;
                    }
                    Cursor = Cursor.next;
                }
                set_new_status(FIND_STATUS, ERR); // от курсора до конца списка искомых значений не найдено
            }
            else
                set_new_status(FIND_STATUS, ERR); // курсор не выставлен - список пуст            
            Cursor = remembed;
        }

        // предусловие: в листе есть хотя бы один узел с заданным значением
        // постусловия: удалены все узлы с заданным значением, 
        //              если на удаленном узле стоял курсор, то он смещается по правилу remove()
        public void remove_all(T val)
        {
            set_new_status(REMOVE_STATUS, NIL);
            Cursor = Head;
            while (Cursor != Tail)
            {                
                find(val);
                if (get_command_status(FIND_STATUS) == ERR)
                    break;
                remove();
            }
            if (get_command_status(REMOVE_STATUS) == OK)
                set_new_status(REMOVE_ALL_STATUS, OK);
            else
                set_new_status(REMOVE_ALL_STATUS, ERR);
            
        }

        // ================================================ 
        // запросы: 

        // предусловие: курсор установлен
        public T get() //-- получить значение текущего узла;
        { return Cursor.value; }

        public int size() //-- посчитать количество узлов в списке.
        {
            Node<T> Temp = Head;
            int i = 0;
            while (Temp != Tail)
            {
                Temp = Temp.next;
                i++;
            }
            return i;
        }
        //--------------------
        public bool is_head()  //-- находится ли курсор в начале списка?
        { return (Cursor == Head); }

        public bool is_tail()  //-- находится ли курсор в конце списка?
        { return (Cursor == Tail); }

        public bool is_value() //-- установлен ли курсор на какой-либо узел в списке (по сути, непустой ли список).
        { return (Cursor != null); }

        // предусловие: запрашиваемый статус команды есть в перечислении
        public int get_command_status(int COMMAND) // возвращает статус выполнения запрашиваемой команды.
        {
            if (COMMAND > 0 && COMMAND < count_of_commands)
            {
                set_new_status(GET_COMMAND_STATUS, OK);             
            } else {
                set_new_status(GET_COMMAND_STATUS, ERR); 
                return NIL;
            }
            return status_table[COMMAND];
        }

        protected void set_new_status(int COMMAND, int STATUS)
        {
            last_command = COMMAND;
            status_table[COMMAND] = STATUS;
        }

        public bool last_command_is_success() // возвращает статус выполнения запрашиваемой команды.
        {
            if (last_command != -1)            
                return (status_table[last_command] == OK); 
            else
                return false;
        }

        public void print_list()
        {
            head();
            int Size = size();
            for (int i = 0; i <= Size; i++)
            {
                Console.WriteLine(get());
                right();
            }
            Console.WriteLine();
        }
    }

    public class LinkedList<T> : ParentList<T> where T : IComparable<T>
    {
        public LinkedList()
        {
            Head = null;
            Tail = null;
            Cursor = null;
            last_command = -1;
            status_table = new int[count_of_commands];
            for (int i = 0; i < count_of_commands; i++)
            {
                status_table[i] = NIL;
            }
        } 
    }

    public class TwoWayList<T> : ParentList<T> where T : IComparable<T>
    {
        public TwoWayList()
        {
            Head = null;
            Tail = null;
            Cursor = null;
            last_command = -1;
            status_table = new int[count_of_commands];
            for (int i = 0; i < count_of_commands; i++)
            {
                status_table[i] = NIL;
            }
        } 

        // предусловие: слева есть узел
        // постусловие: курсор установлен на один узел вправо 
        public void left()
        {
            if (Cursor != null && Cursor.prev != null)
            {
                Cursor = Cursor.prev;
                set_new_status(LEFT_STATUS, OK);               
            }
            else
                set_new_status(LEFT_STATUS, ERR);
        }
    }
    /*
    class Program
    {
        static void Main(string[] args)
        {            
            LinkedList<int> list = new LinkedList<int>();
            int test = 0;
            list.put_left(10);  if( list.get_command_status(LinkedList<int>.PUT_LEFT_STATUS) != LinkedList<int>.ERR ) test++;
            list.put_right(11); if( list.last_command_is_success() ) test++;
            list.remove();      if( list.last_command_is_success() ) test++;
            list.right();       if( list.last_command_is_success() ) test++; 
            list.replace(5);    if( list.last_command_is_success() ) test++;
            list.tail();        if( list.last_command_is_success() ) test++;
                                if (!list.is_tail())                 test++;
            list.head();        if( list.last_command_is_success() ) test++;
                                if( !list.is_head())                 test++;
            list.find(5);       if( list.last_command_is_success() ) test++;


            list.add_tail(1);   
            list.add_tail(4);   
            list.head();        if (!list.last_command_is_success()) test++;
                                if (!list.is_head())                  test++;
            list.put_right(2);  if (!list.last_command_is_success()) test++;
            list.head();        if (!list.last_command_is_success()) test++;
            list.put_left(0);   if (!list.last_command_is_success()) test++;
            list.tail();        if (!list.last_command_is_success()) test++;
                                if (!list.is_tail())                  test++;
            list.put_right(5);  if (!list.last_command_is_success()) test++;
            list.put_left(3);   if (!list.last_command_is_success()) test++;
            list.print_list();   
            //курсор в хвосте
            list.head();
            list.replace(4);   if (!list.last_command_is_success()) test++;
            list.put_left(4);  if (!list.last_command_is_success()) test++;
            list.put_right(4); if (!list.last_command_is_success()) test++;
            list.add_tail(4);
            list.print_list();
            list.head();
            list.remove_all(4); if (!list.last_command_is_success()) test++;
            list.print_list();
            list.remove_all(4); if (list.last_command_is_success()) test++;
            
            
            //==================================================
            TwoWayList<int> list2 = new TwoWayList<int>();

            list2.add_tail(1);
            list2.add_tail(4);
            list2.head();
            list2.put_right(2);
            list2.head();
            list2.put_left(0);
            list2.tail();
            list2.put_right(5);
            list2.put_left(3);            
            list2.tail();
            for (int i = 5; i >= 0; i--)
            {
                Console.WriteLine(list2.get());
                if (list2.get() != i) test++;
                list2.left();
            }
            Console.WriteLine(); 
        }
     
    }*/
}
