using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Apriori_Algorithm
{
    class Program
    {
        public static int counter = 0;
        public static List<string> Frequent_items = new List<string>();
        static void Main(string[] args)
        {
            int minsup;
            string Current;

            //string[] rows = System.IO.File.ReadAllLines(@"C:\Users\Waqas Rizwan\Downloads\Apriori_DataSet.txt");
            /*string[] data = new string[rows.Length];
             int counter = 0;
            
             foreach (var row in rows){
                 string[] columns = row.Split(',');   // Split each row to columns on the basis of comma
                 foreach (var col in columns){
                     data[counter] += col;  //Concatenate into a single value
                 }
                 counter++;
             } */
            string[] data = new string[] { "ABDE", "BCE", "ABDE", "ABCE", "ABCDE", "BCD" };
            List<char> unique = new List<char>();
            for (int i = 0; i < data.Length; i++)
            {    //This loop finds all the unique characters in the data, i-e A,B,C,D,E
                Current = data[i];
                for (int j = 0; j < Current.Length; j++)
                {
                    if (!unique.Contains(Current[j]))
                    {
                        unique.Add(Current[j]);
                    }
                }
            }
            string[] items2 = new string[unique.Count];
            Console.Write("Unique Characters In The Data Are:\n");
            for (int k = 0; k < unique.Count; k++)
            {
                Console.WriteLine(unique[k]);
                items2[k] = (unique[k]).ToString();
            }
            Console.Write("Enter Minimum Support: ");
            minsup = int.Parse(Console.ReadLine());
            
            for (int i = 0; i < items2.Length; i++)
            {
                Leafy("", i, items2, data, minsup);
            }
            Console.WriteLine("Most Freqent Items are:");
            for (int k = 0; k < Frequent_items.Count; k++)
                Console.WriteLine("" + Frequent_items[k]);
            for (int i = 0; i < Frequent_items.Count; i++)
                for (int j = i + 1; j < Frequent_items.Count; j++)
                    if (Is_Disjoint(Frequent_items[i], Frequent_items[j]))
                    {
                        
                        Calculate_Association(Frequent_items[i], Frequent_items[j], data);
                    }

            Console.ReadLine();

        }
        static void Leafy(string item, int start, string[] items, string[] data, int minsup)
        {
            item += items[start];
            if (CalculateSup(item, data) == minsup) //Min_Sup may be '>=' or '<='
             {
                counter++;
                Frequent_items.Add(item);
                Console.WriteLine("" + counter + "  item:    " + item + "    support: " + CalculateSup(item, data));
                for (int i = start + 1; i < items.Length; i++)
                {
                    Leafy(item, i, items, data, minsup);
                }
            }
        }
        static int CalculateSup(string item, string[] data)
        {
            int support = 0;
            for (int k = 0; k < data.Length; k++)
                support += CheckinItem(item, data[k]);
            return support;
        }
        static int CheckinItem(string ptr, string Str)
        {
            int Exist = 1;
            for (int i = 0; i < ptr.Length; i++)
                Exist = Exist * CheckFor(ptr[i], Str);
            return Exist;
        }
        static int CheckFor(char Ptri, string Str)
        {
            int Exists = 0;
            for (int i = 0; i < Str.Length; i++)
                if (Ptri == Str[i])
                    Exists = 1;
            return Exists;
        }
        static bool Is_Disjoint(string A, string B)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (B.Contains(A[i]))
                    return false;
            }
            return true;
        }
        static void Calculate_Association(string A, string B, string[] data)
        {
            Console.WriteLine(" Association Rules for: " + A + "--->" + B);
            string A_B = A + B;
            int sup_A = CalculateSup(A, data);
            int sup_B = CalculateSup(B, data);
            int sup_AB = CalculateSup(A_B, data);

            int D = data.Length;
            double rsup_A = 1.0 * CalculateSup(A, data) / D;
            double rsup_B = 1.0 * CalculateSup(B, data) / D;
            double rsup_AB = 1.0 * CalculateSup(A_B, data) / D;

            Console.WriteLine("");
            Console.WriteLine("Support (" + A + "--->" + B + ")\t" + CalculateSup(A_B, data));
            
            double Confidence = 1.0 * sup_AB / sup_A;
            Console.WriteLine("Confidence (" + A + "--->" + B + ")\t" + Confidence);
            
            double Lift = 1.0 * (Confidence) / rsup_B;
            Console.WriteLine("Lift (" + A + "--->" + B + ")\t\t" + Lift);
            
            double leverage = 1.0 * (rsup_AB) - (rsup_A * rsup_B);
            Console.WriteLine("Leverage (" + A + "--->" + B + ")\t" + leverage);
            
            double Jaccard = 1.0 * (rsup_AB) / ((rsup_A) + (rsup_B) - (rsup_AB));
            Console.WriteLine("Jaccard (" + A + "--->" + B + ")\t" + Jaccard);
            
            double inj = 1.0 * CalculateSup(A_B, data) / CalculateSup(A, data);
            Console.WriteLine(inj);
            Console.WriteLine("");
        }

    }
}
