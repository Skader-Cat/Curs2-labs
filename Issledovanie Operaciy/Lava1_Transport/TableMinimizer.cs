using Laba1_Transport;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Laba1_Transport.TransportTable;
using static Lava1_Transport.Graph;

namespace Lava1_Transport
{
    internal class TableMinimizer
    {
        public static void initionalResursDistribution(TransportTable transportTable)
        {
            var resourceNeedsConsumers = new List<int>(transportTable.consumers);
            for (int i = 0; i < transportTable.tableCosts.Rows.Count; i++)
            {
                var row = transportTable.tableCosts.Rows[i];
                var resourceHaveSupplier = (transportTable.suppliers[i]);
                while ( resourceHaveSupplier > 0)
                {
                    var indexOfMinCost = findIndexOfMinCostInRow(row, resourceNeedsConsumers);
                    var minCell = row.Field<Cell>(indexOfMinCost);
                    var resourseNeedsConsumer = resourceNeedsConsumers[indexOfMinCost];

                    minCell.resurs = getResursQuantuty(resourseNeedsConsumer, resourceHaveSupplier);

                    resourceHaveSupplier -= minCell.resurs;
                    resourceNeedsConsumers[indexOfMinCost] -= minCell.resurs;
                }
                //transportTable.suppliers[i] = resourceHaveSupplier;
            }
        }

        public static void setCoefs(TransportTable transportTable)
        {
            var coords = findCoordsOfMaxCostInTable(transportTable.tableCosts, transportTable.consumers);
            var rows = transportTable.tableCosts.Rows;
            var u = transportTable.u;
            var v = transportTable.v;
            var maxCostInRow = (rows[coords.Item1][coords.Item2] as Cell);
            v[coords.Item1] = maxCostInRow.defaultCost;

            var startRow = rows[coords.Item1];
            for(int i = 0; i < startRow.ItemArray.Count(); i++)
            {
                var currentCell = (startRow[i] as Cell);
                if (currentCell.resurs != 0) {
                    u[i] = currentCell.defaultCost - v[coords.Item1];
                }
            }

            for (int i = 0; i < rows.Count; i++) {
                {
                    var currentRow = rows[i];
                    if (currentRow != startRow)
                    {
                        List<int> weights = new List<int>();
                        for (int j = 0; j < currentRow.ItemArray.Count(); j++)
                        {
                            var currentCell = currentRow[j] as Cell;
                            if (currentCell.resurs > 0)
                            {
                                weights.Add(currentCell.defaultCost - u[j]);
                            }
                            else
                            {
                                weights.Add(0);
                            }
                        }
                        v[i] = weights.Max();

                        for (int j = 0; j < weights.Count(); j++)
                        {
                            var currentCell = currentRow[j] as Cell;
                            if (currentCell.resurs > 0)
                            {
                                u[j] = currentCell.defaultCost - v[i];
                            }
                        }
                    }
                }
            }
        }

        public static void fillDeltaCost(TransportTable table)
        {
            var rows = table.tableCosts.Rows;
            for (int i = 0; i < rows.Count; i++)
            {
                var currentRow = table.tableCosts.Rows[i];
                for (int j = 0; j < currentRow.ItemArray.Count(); j++)
                {
                    var currentCell = (rows[i][j] as Cell);
                    if (currentCell.resurs == 0)
                    {
                        currentCell.deltaCost = table.u[j] + table.v[i];
                    }
                    else
                    {
                        currentCell.deltaCost = 0;
                    }
                }
            }
        }

        public static Node findCorrectNode(TransportTable table, List<Node> nodes)
        {
            for(int i = 0; i < table.tableCosts.Rows.Count; i++)
            {
                var currentRow = table.tableCosts.Rows[i];
                for (int j = 0; j < currentRow.ItemArray.Count(); j++)
                {
                    var cell = (currentRow.ItemArray[j]as Cell);
                    if (cell.deltaCost > cell.defaultCost)
                    {
                        return nodes.Where(x => x.coords == (i, j)).First();
                    }
                }
            }
            return null;
        }

        public static int findMinResursInCycle(List<Node> cycle)
        {
            int minResurs = int.MaxValue;
            for(int i = 0; i < cycle.Count; i++)
            {
                if(i % 2 != 0 && cycle[i].node.resurs < minResurs)
                {
                    minResurs = cycle[i].node.resurs;
                }
            }
            return minResurs;
        }

        public static void optimizeTable(TransportTable table)
        {
            var indexOfMaxDeltaCost = findCoordsOfMaxCostInTable(table.tableCosts, table.consumers, CostType.deltaCost);
            var currentRow = table.tableCosts.Rows[indexOfMaxDeltaCost.Item1];
            var currentColumn = table.tableCosts.Columns[indexOfMaxDeltaCost.Item2];
            List<int> potentionalColumnsIndex = new List<int>();
            for(int i = 0; i < currentRow.ItemArray.Count(); i++)
            {
                var currentCell = currentRow.ItemArray[i] as Cell;
                
                if (currentCell.resurs > 0)
                {
                    //нужно добавить поиск минимального элемента в столбце, удовлетворяющее условию, что онстреиится к единице и ресурс != 0;
                }
            }
        }

        private static int getResursQuantuty (int resourseNeedConsumer, int resourceHaveSupplier) 
        { 
            if(resourseNeedConsumer >= resourceHaveSupplier)
            {
                return resourceHaveSupplier;
            }
            else
            {
                return resourseNeedConsumer;
            }
        }
        private static int findIndexOfMinCostInRow(DataRow row, List<int> consumers, CostType costType = CostType.defaultCost)
        {
            var minCost = int.MaxValue;
            var minCostIndex = 0;
            for (int i = 0; i < row.ItemArray.Count(); i++)
            {
                var currentCell = (row.ItemArray[i] as Cell);
                if (costType == CostType.defaultCost)
                {
                    if (currentCell.defaultCost < minCost && consumers[i] > 0)
                    {
                        minCost = currentCell.defaultCost;
                        minCostIndex = i;
                    }
                }
                else
                {
                    if (currentCell.deltaCost < minCost && currentCell.resurs == 0 && consumers[i] > 0)
                    {
                        minCost = currentCell.deltaCost;
                        minCostIndex = i;
                    }
                }
            }
            return minCostIndex;
        }

        private static int findIndexOfMaxCostInRow(DataRow row, List<int> consumers, CostType costType = CostType.defaultCost)
        {
            var maxCost = int.MinValue;
            var maxCostIndex = 0;
            for (int i = 0; i < row.ItemArray.Count(); i++)
            {
                var currentCell = (row.ItemArray[i] as Cell);
                if (costType == CostType.defaultCost)
                {
                    if (currentCell.defaultCost > maxCost && currentCell.resurs > 0 && consumers[i] > 0)
                    {
                        maxCost = currentCell.defaultCost;
                        maxCostIndex = i;
                    }
                }
                else
                {
                    if (currentCell.deltaCost > maxCost && currentCell.resurs == 0 && consumers[i] > 0)
                    {
                        maxCost = currentCell.deltaCost;
                        maxCostIndex = i;
                    }
                }
            }
            return maxCostIndex;
        }

        public static (int, int) findCoordsOfMinCostInTable(DataTable tableCosts, List<int> consumers, CostType costType = CostType.defaultCost)
        {
            var minCostInTable = int.MaxValue;
            var minCostIndexI = 0;
            var minCostIndexJ = 0;
            for(int i = 0; i < tableCosts.Rows.Count; i++)
            {
                var row = tableCosts.Rows[i];
                var indexOfMinCostInCurrentRow = findIndexOfMinCostInRow(row, consumers, costType);
                var minCell = row[indexOfMinCostInCurrentRow] as Cell;
                if (costType == CostType.defaultCost)
                {
                    if (minCell.defaultCost < minCostInTable && minCell.resurs > 0)
                    {
                        minCostInTable = minCell.defaultCost;
                        minCostIndexI = i;
                        minCostIndexJ = indexOfMinCostInCurrentRow;
                    }
                }
                else
                {
                    if (minCell.deltaCost < minCostInTable)
                    {
                        minCostInTable = minCell.deltaCost;
                        minCostIndexI = i;
                        minCostIndexJ = indexOfMinCostInCurrentRow;
                    }
                }
            }
            return (minCostIndexI, minCostIndexJ);
        }

      

        public static (int, int) findCoordsOfMaxCostInTable(DataTable tableCosts, List<int> consumers, CostType costType = CostType.defaultCost)
        {
            var maxCostInTable = int.MinValue;
            var maxCostIndexI = 0;
            var maxCostIndexJ = 0;
            for (int i = 0; i < tableCosts.Rows.Count; i++)
            {
                var row = tableCosts.Rows[i];
                var indexOfMaxCostInCurrentRow = findIndexOfMaxCostInRow(row, consumers, costType);
                var maxCell = row[indexOfMaxCostInCurrentRow] as Cell;
                if (costType == CostType.defaultCost)
                {
                    if (maxCell.defaultCost > maxCostInTable && maxCell.resurs > 0)
                    {
                        maxCostInTable = maxCell.defaultCost;
                        maxCostIndexI = i;
                        maxCostIndexJ = indexOfMaxCostInCurrentRow;
                    }
                }
                else
                {
                    if (maxCell.deltaCost > maxCostInTable)
                    {
                        maxCostInTable = maxCell.deltaCost;
                        maxCostIndexI = i;
                        maxCostIndexJ = indexOfMaxCostInCurrentRow;
                    }
                }
            }
            return (maxCostIndexI, maxCostIndexJ);
        }

        public static bool findCycles(TransportTable transportTable, Node startNode, Graph graph)
        {
            graph.nodes.Clear();
            graph.createNodes(transportTable);

            try
            {
                var result = graph.findPathByDFS(startNode);
                var table = transportTable.tableCosts;
                var plusOrMinus = 0;
                int maxResurs = findMinResursInCycle(result[0]);
                foreach (var item in result[0])
                {
                    Cell currentCell = table.Rows[item.coords.Item1][item.coords.Item2] as Cell;
                    if (plusOrMinus % 2 == 0)
                    {
                        currentCell.resurs += maxResurs;
                    }
                    else
                    {
                        currentCell.resurs -= maxResurs;
                    }
                    plusOrMinus++;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Решение оптимально");
                return false; // выход из функции
            }
        }
    }
}
