﻿var r=File.OpenText("i");var s=r.ReadToEnd();void f(int m)=>Console.WriteLine(Enumerable.Range(0,s.Length-m+1).First(i=>s[i..(i+m)].Distinct().Count()==m)+m);f(4);f(14);