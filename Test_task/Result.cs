using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask
{
    class Result
    {
        public List<int> Results { get; } //Выходной вектор
        public int this[int index] => Results[index];
        public void CheckColumn(object column) //Проверка столбца на равенство всех его элементов
        {
            Column temp = (Column)column; //Распаковка из object
            bool check = true; //Флаг равенства всех элементов столбца
            int number = temp[0]; //Запись первого элемента для сравнения остальных с ним
            int i = 1;
            while (i < temp.Count && check) //Проверка на равенство
            {
                if (number != temp[i])
                {
                    check = false;
                    number = 0;
                }
                i++;
            }
            Results[temp.Index] = number; //Запись в вектор состояния столбца под тем же индексом, под которым столбец находиться в матрице
        }
        public Result(int n) //Иницилизация вектора нулями
        {
            this.Results = new List<int>();
            for (int i = 0; i < n; i++)
            {
                this.Results.Add(0);
            }
        }
    }
}
