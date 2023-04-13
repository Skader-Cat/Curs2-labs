using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava1_Transport
{
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Table
    {
        List<Cell> cells = new List<Cell>();
        List<int> aColumn, bRow;
        List<int> vColumn, uRow;
        int n = 4;
        int m = 5;
        struct Cell
        {
            public int value;
            public int mainCoast;
            public int addictCoast;
        }

        private Tuple<List<int>, List<int>> fillCellersAndBuyers(int n, int m)
        {
            List<int> aColumn= new List<int>();
            List<int> bRow= new List<int>();

            Console.WriteLine("Введите столбец поставщиков a: ");
            for(int i = 0; i < n; i++)
            {
                Console.Write(">>");
                aColumn.Add(Convert.ToInt32(Console.ReadLine()));
            }

            Console.WriteLine("Введите строку потребителей b: ");
            for(int j = 0; j < m; j++)
            {
                Console.Write(">>");
                bRow.Add(Convert.ToInt32(Console.ReadLine()));
            }

            return Tuple.Create(aColumn, bRow);
        }

        private List<Cell> fillCells(int n, int m)
        {
            Console.WriteLine("Заполните ячейки с ценой доставки: ");
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    Console.WriteLine(">>");
                    Cell cell;
                    cell.value = Convert.ToInt32(Console.ReadLine());
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

        }

        Table()
        {
            Console.WriteLine("Создание таблицы");
            (this.aColumn, this.bRow) = fillCellersAndBuyers(n, m);
            this.cells
        }

    }
}
