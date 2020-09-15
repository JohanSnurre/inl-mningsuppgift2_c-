using System;
using System.IO;


//Author Johan Larsson, Johlax-8
namespace inlämningsuppgift2
{
    class Program
    {

        //internal class for the sellers
        public class Seller
        {
            private uint articles, pNum;//a personal number can't be a negative integer, lets make space for a lot of people.
                                        //since you can't sell a negative number of articles, lets make it possible
                                        //to register a LARGE amount of articles sold. Maybe the type for articles should be a long,
                                        //imagine a hardcore seller or retailer such as amazon/ebay.
            private string name, district;

            //constructor assigning the neccesary values
            public Seller(string name, string district, uint articles, uint pNum)
            {
                // a seller has a name, district, an amount of articles sold and a personal number
                this.name = name;
                this.district = district;
                this.articles = articles;
                this.pNum = pNum;//personal number

            }
            public uint getArticles()//returns the amount of articles of the seller
            {
                return this.articles;
            }
            public string getDistrict()//return the district of the seller
            {
                return this.district;
            }
            public string getName()//returns the name of the seller
            { 
                return this.name;
            }
            public uint getPNum()// returns the sellers personal number
            {
                return this.pNum;
            }
            public override string ToString()//returns the information of the class as a string
            {
                return (this.getName() + " " + this.getPNum() + " " + this.getDistrict() + " " + this.getArticles());
            }


        }
        static void Main(string[] args)
        {
            procedure();
           
        }


        private static void procedure()
        {
            
            uint amount = askAmount();//you can't have a negative amount of sellers, so lets make space for a lot of them

            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //using synchronous stream to write to a textfile in the documents folder

            StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "result.txt"));//creates a new textfile in the documents folder to write to. The file is called "result.txt"

            
            
            Seller[] sellers = new Seller[amount];
            Console.WriteLine("\n\n");

            for (int j = 0; j < amount; j++)
            {
                Console.Write("Vad heter säljaren? ");
                string fullName = Console.ReadLine();

                Console.Write("Vilket distrikt arbetar säljaren i? ");
                string district = Console.ReadLine();

                
                uint pNum = askPNum(sellers); ;

                uint articles = askArticles();
                

                sellers[j] = new Seller(fullName, district, articles, pNum);
                

                Console.WriteLine("\n");

            }


            inSort(sellers);//sorting the sellers by articles sold, increasing order.
            uint[] levels = {50, 99, 199 };
            int iter = 0;
            uint currentLevel = levels[iter];

            
           
            
            
            
            Console.WriteLine("NAMN | PERSONNUMMER | DISTRIKT | ARTIKLAR");
            outputFile.WriteLine("NAMN | PERSONNUMMER | DISTRIKT | ARTIKLAR");
            int sellerCount = 0;

            for (int i = 0; i < sellers.Length; i++)//loop thourh the sellers array, count the amount of sellers on each level and print them out
            {

                if (sellers[i].getArticles() < currentLevel) sellerCount++;
                else
                {
                    Console.WriteLine(sellerCount + " säljare har uppnått nivå " + iter + "\n");
                    outputFile.WriteLine(sellerCount + " säljare har uppnått nivå " + iter + "\n");//writing the same information to the textfile in /documents
                    sellerCount = 1;
                }    
                while (sellers[i].getArticles() > currentLevel)
                {
                    iter++;
                    if (iter >= levels.Length) break; //if the level is currently the max level, we can't increase it further
                    currentLevel = levels[iter];
                }

                Console.WriteLine(sellers[i].ToString());

            }

            Console.WriteLine(sellerCount + " säljare har uppnått nivå " + iter + "\n");//this is to print the last level 
            outputFile.WriteLine(sellerCount + " säljare har uppnått nivå " + iter + "\n");//writing the same information to the textfile in /documents
            Console.WriteLine("\n\nNivå 0: Färre än 50 artiklar sålda\n" +
                              "Nivå 1: Mellan 50 och 99 artiklar sålda\n" +
                              "Nivå 2: Mellan 100 och 199 artiklar sålda\n" +
                              "Nivå 3: Fler än 199 artiklar sålda");//Just some information about the levels printed out
            outputFile.WriteLine("\n\nNivå 0: Färre än 50 artiklar sålda\n" +
                              "Nivå 1: Mellan 50 och 99 artiklar sålda\n" +
                              "Nivå 2: Mellan 100 och 199 artiklar sålda\n" +
                              "Nivå 3: Fler än 199 artiklar sålda"); //writing the same information to the textfile in /documents

            outputFile.Close();//closes the textfile with the information inside

        }

        private static uint askAmount()
        {
            Console.Write("Hur många säljare? ");
            string strAmount = Console.ReadLine();
            uint amount;
            try
            {
                amount = UInt32.Parse(strAmount);

            }
            catch (Exception)
            {
                Console.WriteLine("Ogiltigt antal säljare, försäk igen");
                return askAmount();
            }
            return amount;

        }


        private static uint askPNum(Seller[] sellers)
        {
            Console.Write("Vad är säljarens personnummer? ");
            string strPNum = Console.ReadLine();
            uint pNum;
            try
            {
                pNum = UInt32.Parse(strPNum);
                foreach(Seller x in sellers)
                {
                    try
                    {
                        if (x.getPNum() == pNum)
                        {
                            throw new Exception();
                        }
                    }
                    catch (NullReferenceException)
                    {
                        continue;
                    }
                            
                }
            }
            catch (Exception)
            {
               
                Console.WriteLine("Ogiltigt personnummer, försök igen\n");
                return askPNum(sellers);
            }
            

            return pNum;
        }

        private static uint askArticles()
        {
            Console.Write("Hur många artiklar har säljaren sålt? ");
            string strArticles = Console.ReadLine();
            uint articles;
            try
            {
                articles = UInt32.Parse(strArticles);
            }
            catch (Exception)
            {
                Console.WriteLine("Ogiltig summa sålda artiklar, försök igen");
                return askArticles();
            }

            return articles;
        }

        private static void inSort(Seller[] sellers)//implementing insertion sort on the Seller array
        {
            int i = 1;
            int j;
            while (i < sellers.Length)
            {
                Seller a = sellers[i];
                j = i - 1;
                while (j>=0 && sellers[j].getArticles() > a.getArticles())
                {
                    sellers[j + 1] = sellers[j];
                    j = j - 1;
                }
                sellers[j + 1] = a;
                i = i + 1;

            }

        }

    }
}
