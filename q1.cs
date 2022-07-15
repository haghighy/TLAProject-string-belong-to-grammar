using System;
using System.Collections.Generic;
using System.Linq;

namespace q1
{
    class Program
    {
        public class Production
        {
            public string firstSide;
            public List<string> Variable;
            public string lastSide;
            public string allPro;
            public Production(string pro)
            {
                firstSide = "";
                Variable = new List<string>();
                lastSide = "";
                allPro = pro;
                int i = 0;
                if (pro != "#")
                {
                    
                    while (i<pro.Length && pro[i] != '<')
                    {
                        firstSide += pro[i];
                        i++;
                    }
                    if (i<pro.Length && pro[i]== '<')
                    {
                        while (i<pro.Length)
                        {
                            string tmp = "";
                            while (i<pro.Length && pro[i] != '>')
                            {
                                tmp += pro[i];
                                i ++;
                            }
                            if (i<pro.Length && pro[i] == '>')
                            {
                                tmp += pro[i];
                                Variable.Add(tmp);
                                i++;
                            }
                        }
                    }
                    while (i<pro.Length)
                    {
                        lastSide += pro[i];
                        i++;
                    }
                }
                else
                    firstSide += "#";
            }
        }

        public static string TestStringOnGrammar (string test, Dictionary<string, List<Production>> AllProductions)
        {
            string currVariable = AllProductions.First().Key;
            // List<string> AllStrs = new List<string>();
            List<Production> AllStrs = new List<Production>();
            foreach (Production pro in AllProductions[currVariable])
            {
                AllStrs.Add(pro);
            }
            int k = 0;
            while (k<100 && AllStrs.Count != 0)
            {
                Production newpro = AllStrs[0];
                if (newpro.Variable.Count != 0)
                {
                    currVariable = newpro.Variable[0];
                    foreach (Production currp in AllProductions[currVariable])
                    {
                        string newVar = "";
                        for (int j = 0; j < currp.Variable.Count; j++)
                        {
                            newVar += currp.Variable[j];
                        }
                        Production add = new Production("");
                        if (currp.firstSide != "#")
                            add = new Production(newpro.firstSide + currp.firstSide + newVar + currp.lastSide + newpro.lastSide);
                        else 
                            add = new Production(newpro.firstSide + newpro.lastSide);
                        if (add.Variable.Count != 0)
                            AllStrs.Add(add);
                        else
                        {
                            if (add.allPro == test)
                                return "Accepted";
                        }
                    }
                }
                AllStrs.Remove(newpro);
                // newpro.Variable[0] = AllProductions[newpro.Variable[0]];
                k++;
            }
            return "Rejected";
        }
        static void Main(string[] args)
        {
            long n = long.Parse(Console.ReadLine());
            string[] Grammar = new string[n];
            for (int i = 0; i < n; i++)
            {
                Grammar[i] = Console.ReadLine();
            }
            string TestStr =  Console.ReadLine();
            Dictionary<string, List<Production>> AllProductions = new Dictionary<string, List<Production>>();
            for (int i = 0; i < n; i++)
            {   
                // string[] g =Grammar[i].Split('-');
                // g[1] = g[1].TrimStart(' ','>');
                // AllGrammars[g[0]] = g[1].Split('|');
                string[] gg = Grammar[i].Split(' ');
                List<string> g = gg.ToList();
                while (g.Contains("|"))
                {
                    g.Remove("|");
                }
                g.Remove("->");
                AllProductions[g[0]] = new List<Production>();
                for (int j = 1; j < g.Count; j++)
                {
                    AllProductions[g[0]].Add(new Production(g[j]));
                }
            }
            System.Console.WriteLine(TestStringOnGrammar(TestStr, AllProductions));
            // string x = Console.ReadLine();
            // System.Console.WriteLine(CheckBrackets(x));
        }
    }
}
