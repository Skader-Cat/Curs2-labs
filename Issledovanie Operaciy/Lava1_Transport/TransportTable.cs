using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace Laba1_Transport
{
    internal class TransportTable
    {
        public DataTable tableCosts;
        public List<int> suppliers;
        public List<int> consumers;
        public List<int> u = new List<int>();
        public List<int> v = new List<int>();

        public enum CostType
        {
            defaultCost,
            deltaCost
        }

        public class Cell
        {
            public int resurs;
            public int defaultCost;
            public int deltaCost;

            public Cell(int resurs = 0, int defaultCost = 0, int deltaCost = 0)
            {
                this.resurs = resurs;
                this.defaultCost = defaultCost;
                this.deltaCost = deltaCost;
            }
        }


        public void setSuppliers(List<int> suppliers = null)
        {
            if (suppliers != null)
            {
                this.suppliers = suppliers;
            }
            else
            {
                Console.WriteLine("Введите количество ресурсов у поставщиков через пробел:");
                this.suppliers = getValuesFromLine();
            }
        }

        public void setConsumers(List<int> consumers = null)
        {
            if (consumers != null)
            {
                this.consumers = consumers;
            }
            else
            {
                Console.WriteLine("Введите количество ресурсов необходимых покупателям через пробел:");
                this.consumers = getValuesFromLine();
            }
        }

        public void setMinimizeTable(String[] strings = null)
        {
            if (strings != null)
            {
                Console.WriteLine("Заполненная таблица: ");
                this.tableCosts = makeMinimizeTable(strings);
            }
            else
            {
                Console.WriteLine("Построчно заполните таблицу. Символы в строке вводить через пробел:");
                this.tableCosts = makeMinimizeTable();
            }
        }

        private void setU()
        {
            for(int i = 0; i < suppliers.Count; i++)
            {
                v.Add(0);
            }
        }

        private void setV()
        {
            for (int i = 0; i < consumers.Count; i++)
            {
                u.Add(0);
            }
        }

        public TransportTable(List<int> suppliers = null, List<int> consumers = null, string[] strings = null)
        {
            setSuppliers(suppliers);
            setConsumers(consumers);
            setMinimizeTable(strings);
            setU();
            setV();
        }

        public void printTable(DataTable dataTable, List<int> consumers, List<int> suppliers, string mode = "")
        {
            var spectreTable = new Table();
            var columnNames = new List<string>();
            spectreTable.AddColumn(new TableColumn("Ai \\ Bj").Centered());
            for (int i = 0; i < consumers.Count; i++)
            {
                var columnName = $"{dataTable.Columns[i].ColumnName} = {consumers[i]}";
                spectreTable.AddColumn(new TableColumn(columnName).Centered());
            }
            spectreTable.AddColumn(new TableColumn("V").Centered());

            for (int i = 0; i < suppliers.Count; i++)
            {
                var rowValues = new List<IRenderable>();
                rowValues.Add(new Markup($"a{i + 1} = {suppliers[i]}"));
                for (int j = 0; j < consumers.Count; j++)
                {
                    var defaultCost = dataTable.Rows[i].Field<Cell>(j % consumers.Count).defaultCost.ToString();
                    var resurs = dataTable.Rows[i].Field<Cell>(j % consumers.Count).resurs.ToString();
                    var deltaCost = dataTable.Rows[i].Field<Cell>(j % consumers.Count).deltaCost.ToString();
                    if (mode != "multiply")
                    {
                        if (int.Parse(deltaCost) > int.Parse(defaultCost))
                        {
                            rowValues.Add(new Markup($"[italic red]{deltaCost}[/] [bold green]{resurs}[/] [italic black]{defaultCost}[/]", new Style(background: Color.GreenYellow)));
                        }
                        else
                        {
                            rowValues.Add(new Markup($"[italic red]{deltaCost}[/] [bold green]{resurs}[/] [italic yellow]{defaultCost}[/]"));
                        }
                    }
                    else
                    {
                        if (int.Parse(deltaCost) > int.Parse(defaultCost))
                        {
                            rowValues.Add(new Markup($"[bold green]{int.Parse(resurs) * int.Parse(defaultCost)}[/]"));
                        }
                        else
                        {
                            rowValues.Add(new Markup($"[bold green]{int.Parse(resurs) * int.Parse(defaultCost)}[/]"));
                        }
                    }
                }
                rowValues.Add(new Markup($"{v[i]}"));
                spectreTable.AddRow(new TableRow(rowValues));
            }

            var uRowValues = new List<IRenderable>();
            uRowValues.Add(new Markup("U"));
            for(int i = 0; i < u.Count; i++)
            {
                uRowValues.Add(new Markup($"{u[i]}"));
            }
            spectreTable.AddRow(uRowValues);
            spectreTable.Border = TableBorder.Heavy;
            AnsiConsole.Write(spectreTable);
        }

        private List<int> getValuesFromLine(String values = "")
        {
            if (values.Length == 0)
            {
                return Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToList();
            }
            else { return values.Split(' ').Select(x => int.Parse(x)).ToList(); }
        }

        private void fillColumnsNameByList(DataTable dataTable, List<int> ints, String filler)
        {
            for (int i = 0; i < consumers.Count; i++)
            {
                dataTable.Columns.Add(filler + (i + 1), typeof(Cell));
            }
        }

        private List<Cell> fillCells(List<int> costs, List<int> resurs = null) {
            List<Cell> filledCells = new List<Cell>();
            foreach (int cost in costs)
            {
                Cell cell = new Cell(defaultCost: cost);
                filledCells.Add(cell);
            }
            return filledCells;
        }

        private void addFlaw(DataTable dataTable)
        {
            var deltaResurs = Math.Abs(consumers.Sum() - suppliers.Sum());
            if(suppliers.Sum() < consumers.Sum())
            {
                suppliers.Add(deltaResurs);
                var nullSequence = string.Join(" ", Enumerable.Repeat('0', consumers.Count));
                dataTable.Rows.Add(fillCells(getValuesFromLine(nullSequence)).ToArray());
            }
            if(suppliers.Sum() > consumers.Sum())
            {
                consumers.Add(deltaResurs);
                var newColumnName = "b" + (consumers.Count);
                dataTable.Columns.Add(newColumnName, typeof(Cell));
                foreach(DataRow row in dataTable.Rows)
                {
                    row[newColumnName] = new Cell(0, 0, 0);
                }
            }
        }

        private DataTable makeMinimizeTable()
        {
            DataTable minimizeTable = new DataTable();
            fillColumnsNameByList(minimizeTable, consumers, "b");
            for (int i = 0; i < suppliers.Count; i++)
            {
                Console.Write(">>");
                minimizeTable.Rows.Add(fillCells(getValuesFromLine()).ToArray());
            }
            addFlaw(minimizeTable);

            return minimizeTable;
        }

        private DataTable makeMinimizeTable(String[] strings)
        {
            DataTable minimizeTable = new DataTable();
            fillColumnsNameByList(minimizeTable, consumers, "b");
            for (int i = 0; i < suppliers.Count; i++)
            {
                minimizeTable.Rows.Add(fillCells(getValuesFromLine(strings[i])).ToArray());
            }
            addFlaw(minimizeTable);

            return minimizeTable;
        }
    }
}
