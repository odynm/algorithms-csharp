using Algorithms;
using System.Collections.Generic;

var list = new Item[] {
    new Item{ Id = 1, Name = "Um" },
    new Item{ Id = 5, Name = "Cinco" },
    new Item{ Id = 8, Name = "Oito" },
    new Item{ Id = 15, Name = "Quinze" },
    new Item{ Id = 20, Name = "Vinte" },
    new Item{ Id = 35, Name = "Trinta e cinco" },
    new Item{ Id = 98, Name = "Noventa e oito" },
    new Item{ Id = 105, Name = "Cento e cinco" },
    new Item{ Id = 401, Name = "Quatrocentos e um" }
};

var nodes = new string[]
{
    "England",   // 0
    "France",    // 1
    "Spain",     // 2 
    "Portugal",  // 3
    "Germany",   // 4
    "Italy",     // 5
    "Poland",    // 6
};

var cities = new string[]
{
    "S J Hortencio",  // 0
    "Ivoti",          // 1
    "Caí",            // 2 
    "Feliz",          // 3
    "L Nova",         // 4
    "P Lucena",       // 5
    "N Hamburgo",     // 6
    "B Principio",    // 7
    "Portão",         // 8
    "S Leopoldo",     // 9
    "P Café",         // 10
    "L Collor",       // 11
    "E Velha",        // 12
    "D Irmãos",       // 13
};

var weightenedEdges = new List<Tuple<Tuple<int, int>, int>>
{
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(0,2),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(0,3),5),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(0,4),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(0,5),3),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(0,11),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(1,5),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(1,6),3),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(1,12),2),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(1,11),3),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(1,13),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(2,7),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(2,8),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(3,7),2),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(3,4),2),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(4,10),3),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(5,10),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(6,9),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(8,9),4),
    new Tuple<Tuple<int,int>, int>(new Tuple<int,int>(11,12),3),
};

var adjListUndirected = new List<int>[]
{
    new List<int>{ 1 },
    new List<int>{ 0, 2, 4, 5 },
    new List<int>{ 1, 3 },
    new List<int>{ 2 },
    new List<int>{ 1, 5, 6 },
    new List<int>{ 4 },
    new List<int>{ 4 },
};

var adjListDirected = new List<int>[]
{
    new List<int>{ },
    new List<int>{ 0, 4, 5 },
    new List<int>{ 1 },
    new List<int>{ 2 },
    new List<int>{ 5, 6 },
    new List<int>{ },
    new List<int>{ },
};

var toSortArray = new int[] { 8, 6, 4, 2, 3, 1, 5, 7, 9 };

/*
 * * * * * * * 0 * * * * * * *
 * * * * * * / * \ * * * * * *
 * * * * * .2. * .3. * * * * *
 * * * * * / * * * \ * * * * *
 * * * * 1  - .2. -  2 * * * *
 * * * * | * * * * * | * * * *
 * * *  .3. * * * * .2.  * * * 
 * * * * |  * * * *  | * * * * 
 * * * * 3  - .4. -  4 * * * *
 * * * * \ * * * * * / * * * *
 * * * * .2. * * * .23.  * * *
 * * * * * \ * * * / * * * * *
 * * * * * *   5    *  * * * *
 */
var I = int.MaxValue;
var adjMatrixWeights = new int[,]
{
    /*0, 1, 2, 3, 4, 5*/
    { 0, 2, 3, I, I, I }, // 0
    { 2, 0, 2, 3, I, I }, // 1
    { 3, 2, 0, I, 2, I }, // 2
    { I, 3, I, 0, 4, 2 }, // 3
    { I, I, 2, 4, 0,23 }, // 4
    { I, I, I, 2,23, 0 }, // 5
};

var maze = new int[,] {
    { 1, 1, 1, 0, 1, 0},
    { 0, 0, 1, 0, 1, 1},
    { 0, 1, 1, 0, 0, 1},
    { 0, 1, 0, 1, 0, 1},
    { 0, 1, 1, 1, 1, 1},
    { 0, 1, 0, 0, 0, 1},
    { 0, 1, 1, 1, 1, 1},
};

var draw = new int[,] {
    { 1, 1, 1, 0, 1, 0},
    { 0, 0, 0, 0, 1, 1},
    { 0, 1, 1, 0, 0, 1},
    { 0, 1, 0, 1, 0, 1},
    { 0, 1, 1, 1, 1, 1},
    { 0, 1, 0, 0, 0, 1},
    { 0, 1, 1, 1, 1, 1},
};

var linkedListNode1 = new FloydsCycleDetection.Node(null, 1);
var linkedListNode2 = new FloydsCycleDetection.Node(linkedListNode1, 2);
var linkedListNode3 = new FloydsCycleDetection.Node(linkedListNode2, 3);
var linkedListNode4 = new FloydsCycleDetection.Node(linkedListNode3, 4);
var linkedListNode5 = new FloydsCycleDetection.Node(linkedListNode4, 5);
var linkedListNode6 = new FloydsCycleDetection.Node(linkedListNode5, 6);
var linkedListNode7 = new FloydsCycleDetection.Node(linkedListNode6, 7);
// Add cycle
linkedListNode2.Next = linkedListNode4;

int[] universe = { 1, 2, 3, 4, 5 };

var edges = new Tuple<int, int>[]
{
    Tuple.Create(0,6),
    Tuple.Create(1,2),
    Tuple.Create(1,4),
    Tuple.Create(1,6),
    Tuple.Create(3,0),
    Tuple.Create(3,4),
    Tuple.Create(5,1),
    Tuple.Create(7,0),
    Tuple.Create(7,1),
};

var arr = new int[] { -2, 1, 3, 4, 5, 8, 9 };
var majority = new int[] { 1, 8, 7, 4, 1, 2, 2, 2, 2, 2, 2 };
var countingSortArr = new int[] { 4, 2, 10, 10, 1, 4, 2, 1, 10 };

//BreadthFirstSearch.FindPath(adjListDirected, 3, 5)?.ForEach(x => Console.WriteLine(nodes[x]));
//DepthFirstSearch.FindPath(adjListUndirected, 3, 5)?.ForEach(x => Console.WriteLine(nodes[x]));
//MergeSort.Sort(toSortArray);
//QuickSort.Sort(toSortArray);
//var mst = Kruskal.Run(weightenedEdges, cities.Length);
//var result = FloydWarshall.Run(adjMatrixWeights);
//var result = Dijkstra.ShortestPath(adjMatrixWeights, 0);
//var result = BellmanFord.ShortestPath(adjMatrixWeights, 0);
//Kadane.Run(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 });
//var result = LeeAlgorithm.ShortestPath(maze, 0, 0, 5, 5);
//FloodFill.Fill(draw, new Tuple<int, int>(5, 5), 2);
//FloydsCycleDetection.Detect(linkedListNode7);
//UnionFind.MakeSet(universe);
//UnionFind.Union(4, 3);
//var sorted = KahnsTopologicalSort.Sort(edges, 8);
//KMP.FindMatch("aabcaacaababaaabaaacaacba", "aabaaac");
//InsertionSort.Sort(arr);
//SelectionSort.Sort(arr);
// CountingSort.UnstabledSort(countingSortArr, 10); // Range is 10 (0 to 10)
//HeapSort.Sort(arr);
//arr.ToList().ForEach(x => Console.Write(x + " "));
//var codes = HuffmanCodingCompression.BuildHuffmanTree(text);
//var encoded = HuffmanCodingCompression.Encode(text, codes);
//HuffmanCodingCompression.Decode(encoded, codes);
//Console.WriteLine(Quickselect.Select(arr, 0, arr.Length - 1, 1));
//Console.WriteLine(BoyerMooreMajorityVote.Majority(majority));
Console.WriteLine(Euclids.GCD(30, 50));

Console.WriteLine();
Console.ReadLine();
