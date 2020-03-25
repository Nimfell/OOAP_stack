
/***Задание 1.*********************************************************************************************
Напишите небольшой пример кода с комментариями, где применяются наследование, композиция и полиморфизм.

Композиция:  В классе Tribe использован класс Human, таким образом, что объекты используемого класса создаются при инициализации объектов класса Tribe и удаляются при его удалении.
Наследование: Есть базовый класс Human от которого наследуют классы: Woman, Man, Child.
Полиморфизм: Класс Tribe при использовании своих методов может одинаково обрабатывать объекты класса Human и всех его наследников(Woman, Man, Child).

***Задание 2.*********************************************************************************************
Напишите небольшой пример кода с комментариями, где в наследовании применяется как расширение класса-родителя, так и специализация класса-родителя.

Расширение класса-родителя:  Классы Woman и Man были созданы посредствам расширения базового класса Human.
Специализация класса-родителя: Класс TribeOfAmazons был создан введением строгой типизации класса Human.
Наследование нескольких родительских классов в С# - не поддерживается.

***Задание 3.*********************************************************************************************
Расскажите, как в выбранном вами языке программирования поддерживается концепция "класс как модуль".

Можно каждый класс хранить в отдельном файле, отдельном пространстве имен и т.д. 
А ещё есть такая штука как interface она может работать как синтаксическая единица и к модулю больше подходит, если 
его определение такогго: "Модуль по сути предоставляет некоторый абстрактный интерфейс -- когда мы используем сам модуль в целом, а не запрятанный в нём код."

***********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TribeSharp
{
    class Tribe // композиция 
    {
        private List<Human> People;

        public Tribe() { People = new List<Human>(); }

        public void add_resident(Human NewResident) { People.Add(NewResident); } // полиморфизм при использовании зависимых классов от Human
     
        public int get_population() {return People.Count; }

        //public object get_Human(string name, int age, string gender)
        //{ return People[People.IndexOf(new Human(name, age, gender))]; }
    }

    class TribeOfAmazons : Tribe
    {
        private override List<Woman> People; // урезаем возможности базового класса, исключая полиморфизм

        public TribeOfAmazons() { People = new List<Woman>(); }

        public override void add_resident(Woman NewResident) { People.Add(NewResident); } 
    }

    class Human // базовый класс
    {        
        private string Gender;
        private string Name;
        private int Age;

        public string get_name()   {return Name;}
        public string get_gender() {return Gender;}
        public int    get_age()    {return Age;}

        public Human(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }
    }

    class Woman : Human // класс - наследник
    {
        private string Profession;

        public Woman(string name, int age, string prof)
            : base(name, age, "Woman")
        { Profession = prof; }

        void set_prof(string prof) { Profession = prof;}
        string get_prof() { return Profession; }
    }

    class Man : Human
    {
        public string Profession;

        public Man(string name, int age, string prof)
            : base(name, age, "Man")
        { Profession = prof; }

        void set_prof(string prof) { Profession = prof; }
        string get_prof() { return Profession; }
    }

    class Child : Human   // класс - наследник
    {
        private string Mother;
        private string Father;

        public Child(string name, int age, string gender, string mom, string dad)
            : base(name, age, gender)
        {
            Mother = mom;
            Father = dad;
        }
    }

    class Program // класс - наследник
    {
        static void Main(string[] args)
        {
            const string M = "Man";
            const string W = "Woman";

            Tribe TribeOfHorn = new Tribe();          
            TribeOfHorn.add_resident(new Man("Mike", 34, "prof_1"));
            TribeOfHorn.add_resident(new Woman("Lisa", 25, "prof_2"));
            TribeOfHorn.add_resident(new Child("Alice", 8, W, "Mike", "Lisa"));              
        }
    }

}
