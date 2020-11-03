using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Test_task
{
    class TextConvert
    {
        public static void StrToState(string str, out int S, out int count)
        {
            Regex regex = new Regex(@"(\d+)"); 
            MatchCollection matches = regex.Matches(str);
            if(matches.Count == 3)
            {
                S = Convert.ToInt32(matches[0].Value);
                count = Convert.ToInt32(matches[2].Value) - Convert.ToInt32(matches[1].Value) + 1;
            }
            else
            {
                S = -1; count = -1;
            }
        }
        public static List<int> strToListState(string str) //Получение числового вектора из набора состояний для ресурса из строки
        {
            Regex regex = new Regex(@"S\d*{\d*,\d*}"); //Регулярное выражение для состояний
            MatchCollection matches = regex.Matches(str);
            List<int> resource = new List<int>();
            foreach(Match m in matches)
            {
                int S, count;
                StrToState(m.Value, out S, out count); //Преобразование из записи вида Sn{a,b} в вектор состояний
                for(int i = 0; i < count; i++)
                {
                    resource.Add(S);
                }
            }
            return resource;
        }
        public static string listStateToStr(List<int> resultStates) //Анализ выходного вектора для получения списка общих состояний в виде Sn(a,b)
        {
            string result = "", temp = "";
            int pState = 0;
            int s = 1, f = 1;
            for (int i = 0; i < resultStates.Count; i++) //Проход по вектору для сбора идущих подряд состояний для последовательных моментов времени в вид Sn(a,b)
            {
                if (resultStates[i] != 0 && pState == resultStates[i] && i != resultStates.Count - 1)
                {
                    f++;
                    pState = resultStates[i];
                }
                if (resultStates[i] != 0 && pState == resultStates[i] && i == resultStates.Count - 1)
                {
                    temp = "S" + pState + "{" + s + "," + (f + 1).ToString() + "} ";
                    result += temp;
                }
                if (pState != resultStates[i] && resultStates[i] != 0)
                {
                    temp = "S" + pState + "{" + s + "," + f + "} ";
                    result += temp;
                    s = i + 1; f = i + 1; pState = resultStates[i];
                }
                if (pState != resultStates[i] && resultStates[i] == 0)
                {
                    temp = "S" + pState + "{" + s + "," + f + "} ";
                    result += temp;
                    pState = 0;
                }
                if (resultStates[i] != 0 && pState == 0)
                {
                    s = i + 1; f = i + 1; pState = resultStates[i];
                }
            }
            Regex regex = new Regex(@"S0{\d+,\d+} "); //Регулярные выражения для нулевых состояний, означающие отстутвие общего состояния на данный момент времени
            MatchCollection matches = regex.Matches(result);
            foreach(Match a in matches)
            {
                result = result.Replace(a.Value, ""); //Удаление нулевых состояний
            }
            return result;
        }
    }
}
