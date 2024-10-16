// See https://aka.ms/new-console-template for more information
using LINQTest;

Console.WriteLine("Hello, World!");
//var obj = new LinqInnerJoin();
//var obj = new LinqMultipleSourcesJoin();
//var obj = new LinqGroupJoin();
//var obj = new LeftRightJoin();
//var obj = new Select();
//var obj = new UnionTest();
var obj = new OuterJoin();
obj.QuerySyntax();
