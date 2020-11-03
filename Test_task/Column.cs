using System;
using System.Collections.Generic;
using System.Text;

namespace Test_task
{
    class Column
    {
        public Column(List<int> numbers, int index)
        {
            Numbers = numbers;
            Index = index;
        }
        public int this[int index] //индексатор
        {
            get
            {
                return Numbers[index];
            }
        }
        public List<int> Numbers { get; set; } 
        public int Index { get; set; } //Индекс столбца из матрицы
        public int Count { get => Numbers.Count; }
    }
}
