﻿var s=File.ReadAllText("i");void f(int m)=>Console.WriteLine(s.Select((_,i)=>i).First(i=>s[i..(i+m)].Distinct().Count()==m)+m);f(4);f(14);