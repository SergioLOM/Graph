using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Graph
{
    class Graph
    {
        string patternND = @"^[1-5]{1}\s*-\s*[1-5]{1}$";
        string patternND1 = @"^[1-4]{1}\s*-\s*[1-4]{1}$";
        string patternND2 = @"^[1-3]{1}\s*-\s*[1-3]{1}$";
        string patternND3 = @"^[1-2]{1}\s*-\s*[1-2]{1}$";
        string patternD = @"^[1-5]{1}\s*>\s*[1-5]{1}$";

        private string[,] matriz;
        string numNodes;
        string isDirected;
        string edge;
        static void Main(string[] args)
        {
            Graph matriz = new Graph();
            matriz.EnterNumberOfNodes();
            //matriz.PrintGraph();
        }

        public void EnterNumberOfNodes()
        {
            Console.WriteLine("How many nodes? (Max 5)");
            numNodes = Console.ReadLine();
            if (numNodes.Length>1 || string.IsNullOrEmpty(numNodes))
            {
                Console.WriteLine("Enter a number between 1 and 5.");
                EnterNumberOfNodes();
            }
            else
            {
                char numNodesC = Convert.ToChar(numNodes);
                if (!Char.IsNumber(numNodesC) || numNodesC == '0')
                {
                    Console.WriteLine("Enter a number between 1 and 5.");
                    EnterNumberOfNodes();
                }
                else
                {
                    EnterNodes(numNodesC);
                }
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void EnterNodes(char numNodes)
        {
            matriz = new string[10, 10];
            int num = 1;
            for (int i = 0; i < Char.GetNumericValue(numNodes); i++)
            {
                Console.WriteLine("Enter node " + (i + 1) + " (Ex: a,b with a and b between 0 and 9): ");
                string node = Console.ReadLine();
                string[] nodeM = node.Split(',').ToArray();
                if (nodeM.Length > 2 || string.IsNullOrEmpty(node))
                {
                    Console.WriteLine("The node must be entered as a,b with a and b between 0 and 9.");
                    EnterNodes(numNodes);
                }else if (nodeM.Length==1)
                {
                    Console.WriteLine("The node must be entered as a,b with a and b between 0 and 9.");
                    i--;
                }
                else
                {
                    bool st1 = IsNumeric(nodeM[0].Trim());
                    bool st2 = IsNumeric(nodeM[1].Trim());
                    if (st1 == false || st2 == false)
                    {
                        Console.WriteLine("Must be numbers from 0 to 9");
                        i--;
                    }
                    else
                    {
                        int f = Convert.ToInt32(nodeM[0].Trim());
                        int c = Convert.ToInt32(nodeM[1].Trim());
                        if (f > 9 || c > 9 || f < 0 || c < 0)
                        {
                            Console.WriteLine("Must be numbers from 0 to 9");
                            i--;
                        }
                        else if (matriz[f, c] != null)
                        {
                            Console.WriteLine("This node has already been entered!");
                            i--;
                        }
                        else
                        {
                            matriz[f, c] = "(" + num.ToString() + ")";
                            num++;
                        }
                    }
                }
            }
            GraphType();
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void GraphType()
        {
            Console.WriteLine("Is directed? y/n ");
            isDirected = Console.ReadLine();
            if (isDirected.Trim().Equals("y") || isDirected.Trim().Equals("n") || isDirected.Trim().Equals("Y") || isDirected.Trim().Equals("N"))
            {
                switch (isDirected)
                {
                    case "y": case "Y":
                        EnterEdgesGraph(matriz);
                        break;
                    case "n": case "N":
                        EnterEdgesGraph(matriz);
                        break;
                }
            }
            else
            {
                GraphType();
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void EnterEdgesGraph(string[,] matriz)
        {
            switch (numNodes)
            {
                case "5": case "4":
                    for (int i = 0; i < 2; i++)
                    {
                        GetEdges(i);
                    }
                    break;
                case "3": case "2":
                    for (int i = 0; i < 1; i++)
                    {
                        GetEdges(i);
                    }
                    break;
                default:
                    break;
            }
            PrintGraph(matriz);
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

       public int[] FindNode(string[,] matriz, string a, string b)
       {
            int[] nodes= new int[4];
            int n = 0;
            for (int f = 0; f < 10; f++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (matriz[f, c] != null)
                    {
                        if (matriz[f, c].Contains(a) || matriz[f, c].Contains(b))
                        {
                            nodes[n] = f;
                            nodes[n + 1] = c;
                            n = n + 2;
                        }
                    } 
                }
            }
            return nodes;
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public void VerifyIfDirected(string[,] matriz, int[] nodes, string isDirected)
        {
            if (isDirected.Equals("n") || isDirected.Equals("N")) {
                if (nodes[0] <= nodes[2] && nodes[1] <= nodes[3])
                {
                    matriz[nodes[0], nodes[1]] = matriz[nodes[0], nodes[1]] + "o";
                    matriz[nodes[2], nodes[3]] = "o" + matriz[nodes[2], nodes[3]];
                    PrintDotsOne(nodes, matriz);
                }
                else if (nodes[0] <= nodes[2] && nodes[1] >= nodes[3])
                {
                    matriz[nodes[0], nodes[1]] = "o" + matriz[nodes[0], nodes[1]];
                    matriz[nodes[2], nodes[3]] = matriz[nodes[2], nodes[3]] + "o";
                    PrintDotsTwo(nodes, matriz);
                }
            }
            else
            {
                string[] edgeSplited = edge.Split('-');
                if (matriz[nodes[0],nodes[1]].Contains(edgeSplited[0]))
                {
                    matriz[nodes[0], nodes[1]] = matriz[nodes[0], nodes[1]] + "o";
                    matriz[nodes[2], nodes[3]] = ">" + matriz[nodes[2], nodes[3]];
                    if (nodes[0] <= nodes[2] && nodes[1] <= nodes[3])
                    {
                        PrintDotsOne(nodes, matriz);
                    }
                    else if (nodes[0] <= nodes[2] && nodes[1] >= nodes[3])
                    {
                        PrintDotsTwo(nodes, matriz);
                    }
                }
                else
                {
                    matriz[nodes[0], nodes[1]] = matriz[nodes[0], nodes[1]] + "<";
                    matriz[nodes[2], nodes[3]] = "o" + matriz[nodes[2], nodes[3]];
                    if (nodes[0] <= nodes[2] && nodes[1] <= nodes[3])
                    {
                        PrintDotsOne(nodes, matriz);
                    }
                    else if (nodes[0] <= nodes[2] && nodes[1] >= nodes[3])
                    {
                        PrintDotsTwo(nodes, matriz);
                    }
                } 
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void PrintDotsOne(int[] nodes, string[,] matriz)
        {
            for (int f = nodes[0] + 1; f <= nodes[2]; f++)
            {
                matriz[f, nodes[1]] = matriz[f, nodes[1]] + ".";
            }
            for (int c = nodes[1] + 1; c < nodes[3]; c++)
            {
                matriz[nodes[2], c] = matriz[nodes[2], c] + ".";
            }
        }

        public void PrintDotsTwo(int[] nodes, string[,] matriz)
        {
            for (int f = nodes[0] + 1; f < nodes[2]; f++)
            {
                matriz[f, nodes[1]] = matriz[f, nodes[1]] + ".";
            }
            for (int c = nodes[1] - 1; c > nodes[3]; c--)
            {
                matriz[nodes[2], c] = matriz[nodes[2], c] + ".";
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void GetEdges(int i) {
            List<string> edgesTotal = new List<string>();
          
            Console.WriteLine("Enter edge. (Ex: a-b with a and b between 1 and 5))");
            edge = Console.ReadLine();
            bool validation = ValidateInput(edge.Trim(), numNodes);
            if (validation == true)
            {
                string[] edgeSplit = edge.Split('-');

                if (edgeSplit[0].Trim() == edgeSplit[1].Trim())
                {
                    Console.WriteLine("Autoloops are not valid.");
                    i--;
                    GetEdges(i);
                }
                else if (edgesTotal.Any(elementInEdgesTotal => elementInEdgesTotal == edgeSplit[0].Trim()) || edgesTotal.Any(elementInEdgesTotal => elementInEdgesTotal == edgeSplit[1].Trim()))
                {
                    Console.WriteLine("A node has one and just one edge.");
                    i--;
                    GetEdges(i);
                }
                else
                {
                    edgesTotal.Add(edgeSplit[0].Trim());
                    edgesTotal.Add(edgeSplit[1].Trim());
                    Console.WriteLine("Node " + edgeSplit[0].Trim() + " is conected with node " + edgeSplit[1].Trim());
                    int[] nodes = FindNode(matriz, "(" + edgeSplit[0].Trim() + ")", "(" + edgeSplit[1].Trim() + ")");
                    VerifyIfDirected(matriz, nodes, isDirected);
                }
            }
            else
            {
                if (numNodes == "5")
                {
                    Console.WriteLine("The edge must be between 1 and 5");
                }
                else if (numNodes == "4")
                {
                    Console.WriteLine("The edge must be between 1 and 4");
                }
                else if(numNodes == "3")
                {
                    Console.WriteLine("The edge must be between 1 and 3");
                }
                else
                {
                    Console.WriteLine("The edge must be between 1 and 2");
                }
                i--;
                GetEdges(i);
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void PrintGraph(string[,] matriz)
        {
            for (int f = 0; f < 10; f++)
            {
                for (int c = 0; c < 10; c++)
                {
                    if (matriz[f, c]==null)
                    {
                        matriz[f, c] = " ";
                        Console.Write(matriz[f, c] + "   ");
                    }
                    else
                    {
                        Console.Write(matriz[f, c]+ "   ");
                    } 
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public bool IsNumeric(string st)
        {
            int integer = 0;
            bool result = int.TryParse(st, out integer);
            return result;
        }

        public bool ValidateInput(string value, string numNodes)
        {
            bool answer;
            if (numNodes == "5")
            {
                answer = Regex.IsMatch(value, patternND);
            }
            else if(numNodes == "4")
            {
                answer = Regex.IsMatch(value, patternND1);
            }
            else if (numNodes == "3")
            {
                answer = Regex.IsMatch(value, patternND2);
            }
            else if (numNodes == "2")
            {
                answer = Regex.IsMatch(value, patternND3);
            }
            else
            {
                answer = true;
            }
            return answer;
        }
    }
}
