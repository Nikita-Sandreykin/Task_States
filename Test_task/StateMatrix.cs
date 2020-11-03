using System;
using System.Collections.Generic;
using System.Text;

namespace Test_task
{
    class StateMatrix
    {
        private List<List<int>> resources = new List<List<int>>(); //Матрица представлена набором векторов в виде List<int>
        public int lsize { get; set; } //Размерность матрицы по горизонтали представлена наименьшей длинной среди всех векторов
        private void calcLSize() //Вычисление наименьшей длины для векторов
        {
            int size = int.MaxValue;
            foreach (List<int> temp in resources)
            {
                if (temp.Count < size) size = temp.Count;
            }
            lsize = size;
        }
        public void addResource(List<int> temp) //Добавление вектора
        {
            resources.Add(temp);
            calcLSize();
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
        public Column getColumn(int index) //Получение столбца в виде объекта Column черещ индексатор
        {
            Column temp = new Column(this[index], index);
            return temp;
        }
    }
    
}
