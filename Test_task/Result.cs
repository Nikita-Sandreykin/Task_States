using System;
using System.Collections.Generic;
using System.Text;

namespace Test_task
{
    class Result
    {
        public List<int> results { get; } //Выходной вектор
        public int this[int index]
        {
            get
            {
                return results[index];
            }
        }
        public void checkColumn(object a) //Проверка столбца на равенство всех его элементов
        {
            Column temp = (Column)a; //Распаковка из object
            bool check = true;
            int number = temp[0]; //Запись первого элемента для сравнения остальных с ним
            int i = 1;
            while(i < temp.Count && check) //Проверка на равенство
            {
                if (number != temp[i])
                {
                    check = false;
                    number = 0;
                }
                i++;
            }
            results[temp.Index] = number; //Запись в вектор состояния столбца под тем же индексом, под которым столбец находиться в матрице
        }
        public Result(int n)
        {
            this.results = new List<int>();
            for(int i = 0; i < n; i++)
            {
                this.results.Add(0);
            }
        }
    }
}
