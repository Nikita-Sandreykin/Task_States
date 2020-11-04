using System;
using System.Collections.Generic;
using System.Text;

namespace TestTask
{
    class StateMatrix
    {
        private List<List<int>> resources = new List<List<int>>(); //Матрица представлена набором векторов в виде List<int>
        public int Lsize { get; set; } //Размерность матрицы по горизонтали представлена наименьшей длинной среди всех векторов
        private void CalcLSize() //Вычисление наименьшей длины для векторов
        {
            int size = int.MaxValue;
            foreach (List<int> temp in resources)
            {
                if (temp.Count < size) size = temp.Count;
            }
            Lsize = size;
        }
        public void AddResource(List<int> temp) //Добавление вектора
        {
            resources.Add(temp);
            CalcLSize();
        }
        public List<int> this[int index] //индексатор для получения столбцов как List<int>
        {
            get
            {
                List<int> temp = new List<int>();
                foreach (List<int> resource in resources)
                {
                    temp.Add(resource[index]);
                }
                return temp;
            }
        }
        public Column GetColumn(int index) //Получение столбца в виде объекта Column черещ индексатор
        {
            Column temp = new Column(this[index], index);
            return temp;
        }
    }

}
