using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public class Program
    {
        static void Main(string[] args)
        {
            string inputString = string.Empty;
            while (!inputString.Equals("\n"))
            {
                    Console.WriteLine("Ingrese la cadena a analizar: ");
                    inputString = Console.ReadLine();
                    byte[] bytes = Encoding.ASCII.GetBytes(inputString.Replace(" ", ""));
                    Queue<byte> inputQ = new Queue<byte>(bytes);
                    bool error = false;
                    int state = 0;
                    while (inputQ.Count() != 0 && error != true)
                    {
                        switch(state){
case 0:

if( inputQ.Peek() >= 48 && inputQ.Peek() <= 57){
state=1;
inputQ.Dequeue();
}
else if( inputQ.Peek() == 34){
state=2;
inputQ.Dequeue();
}
else if( inputQ.Peek() == 39){
state=3;
inputQ.Dequeue();
}
else if( inputQ.Peek() == 61){
state=4;
inputQ.Dequeue();
}
else if( inputQ.Peek() >= 65 && inputQ.Peek() <= 90 || inputQ.Peek() >= 97 && inputQ.Peek() <= 122 || inputQ.Peek() == 95){
state=5;
inputQ.Dequeue();
}
else{
state=0;
}
break;
case 1:

if( inputQ.Peek() >= 48 && inputQ.Peek() <= 57){
state=1;
inputQ.Dequeue();
}
else{
state=0;
}
break;
case 2:

if( inputQ.Peek() >= 32 && inputQ.Peek() <= 254){
state=6;
inputQ.Dequeue();
}
else{
error=true;
}
break;
case 3:

if( inputQ.Peek() >= 32 && inputQ.Peek() <= 254){
state=7;
inputQ.Dequeue();
}
else{
error=true;
}
break;
case 4:
state=0;
break;
case 5:

if( inputQ.Peek() >= 48 && inputQ.Peek() <= 57){
state=5;
inputQ.Dequeue();
}
else if( inputQ.Peek() == 39){
state=4;
inputQ.Dequeue();
}
else if( inputQ.Peek() >= 65 && inputQ.Peek() <= 90 || inputQ.Peek() >= 97 && inputQ.Peek() <= 122 || inputQ.Peek() == 95){
state=5;
inputQ.Dequeue();
}
else{
error=true;
}
break;
case 6:

if( inputQ.Peek() == 34){
state=4;
inputQ.Dequeue();
}
else{
error=true;
}
break;
case 7:

if( inputQ.Peek() == 39){
state=4;
inputQ.Dequeue();
}
else{
error=true;
}
break;}
                    }

                    if (error == false)
                    {
                        Console.WriteLine("La cadena fue aceptada.");
                        Tools tools = new Tools();
                        Dictionary<int, string> existingTokensDic = new Dictionary<int, string>();
existingTokensDic.Add(1,"DIGITO|DIGITO*");

existingTokensDic.Add(2,"\"CHARSET\"|'CHARSET'");

existingTokensDic.Add(4,"=");

existingTokensDic.Add(3,"LETRA(LETRA|DIGITO)*'");

existingTokensDic.Add(18,"PROGRAM");

existingTokensDic.Add(19,"INCLUDE");

existingTokensDic.Add(20,"CONST");

existingTokensDic.Add(21,"TYPE");

existingTokensDic.Add(22,"VAR");

existingTokensDic.Add(23,"RECORD");

existingTokensDic.Add(24,"ARRAY");

existingTokensDic.Add(25,"OF");

existingTokensDic.Add(26,"PROCEDURE");

existingTokensDic.Add(27,"FUNCTION");

existingTokensDic.Add(28,"IF");

existingTokensDic.Add(29,"THEN");

existingTokensDic.Add(30,"ELSE");

existingTokensDic.Add(31,"FOR");

existingTokensDic.Add(32,"TO");

existingTokensDic.Add(33,"WHILE");

existingTokensDic.Add(34,"DO");

existingTokensDic.Add(35,"EXIT");

existingTokensDic.Add(36,"END");

existingTokensDic.Add(37,"CASE");

existingTokensDic.Add(38,"BREAK");

existingTokensDic.Add(39,"DOWNTO");
Queue<string> tokensQ = tools.TokenizeText(inputString, existingTokensDic);
                        Dictionary<string, List<string>> setsRangesDic = new Dictionary<string, List<string>>();
setsRangesDic.Add("LETRA", new List<string>(new string[] {"65-90","97-122","95"}));

setsRangesDic.Add("DIGITO", new List<string>(new string[] {"48-57"}));

setsRangesDic.Add("CHARSET", new List<string>(new string[] {"32-254"}));

setsRangesDic.Add("\"", new List<string>(new string[] {"34"}));

setsRangesDic.Add("'", new List<string>(new string[] {"39"}));

setsRangesDic.Add("=", new List<string>(new string[] {"61"}));

List<string> finalTokenList = tools.TokenListToPrint(tokensQ, existingTokensDic, setsRangesDic);

                    foreach (var finalToken in finalTokenList)
                    {
                        Console.WriteLine(finalToken);
                    }
                
            }
                    else
                    {
                        Console.WriteLine("La cadena no fue aceptada.");
                    }

                    Console.ReadKey();
                    Console.Clear();
                }

            }
        }
    }