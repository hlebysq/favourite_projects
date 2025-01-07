namespace CustomClasses
{
    /// <summary>
    /// Класс, в котором хранятся метод и перечисление ддя работы с выводом первых/последних n записей.
    /// </summary>
    public static class TopOrBottomEls
    {
        /// <summary>
        /// Перечисление двух опций вывода данных.
        /// </summary>
        public enum TopOrBottom
        {
            Bottom,
            Top
        }
        /// <summary>
        /// Метод, выводящий на экран первые или последние n элементов.
        /// </summary>
        /// <param name="table"></param>
        public static void Select(Zags[] table)
        {
            TopOrBottom choose = CustomInput.TopOrBottomInput();
            List<Zags> list = new List<Zags>();
            Console.Clear();
            int n = CustomInput.IntPut(1, table.Length);
            for (int i = 0; i < n; i++)
            {
                switch (choose) // Можно было и не через enum, но этого требует ТЗ.
                {
                    case TopOrBottom.Top:
                        list.Add(table[i]);
                        break;
                    case TopOrBottom.Bottom:
                        list.Add(table[table.Length-i-1]);
                        break;
                }
            }
            Methods.CoolOutput(list.ToArray());
            Menu.ReturnToMenu(table);
        }
    }
}
