using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    public class Tools
    {

        //public Queue<string> TokenizeText(string text, Dictionary<int, string> existingTokens)
        //{
        //    List<string> characters = existingTokens.Values.ToList();
        //    Queue<string> tokens = new Queue<string>();
        //    string[] elements = text.Split(new[] { "\r\n", "\r", "\n", "\t", " " }, StringSplitOptions.None);
        //    foreach (string element in elements)
        //    {
        //        if (!string.IsNullOrWhiteSpace(element) && !string.IsNullOrEmpty(element))
        //        {
        //            List<string> find = characters.FindAll(x => element.Contains(x));
        //            if (find.Count() == 0)
        //            {
        //                tokens.Enqueue(element);
        //            }                    
        //            else
        //            {
        //                var greatestElement = find.OrderByDescending(x => x.Length).FirstOrDefault();                        
        //                int index = element.IndexOf(greatestElement);
        //                string newElement = string.Empty;
        //                if (index == 0)
        //                {
        //                    tokens.Enqueue(element.Substring(0, greatestElement.Length));
        //                    newElement = element.Remove(0, greatestElement.Length);
        //                }
        //                else
        //                {
        //                    tokens.Enqueue(element.Substring(0, index));
        //                    newElement = element.Remove(0, index);
        //                }
        //                while (newElement.Length > 0)
        //                {                            
        //                    find = characters.FindAll(x => newElement.Contains(x));
        //                    if (find.Count() == 0)
        //                    {
        //                        tokens.Enqueue(newElement);
        //                        newElement = string.Empty;
        //                    }
        //                    else
        //                    {
        //                        index = newElement.IndexOf(greatestElement);
        //                        if (index == 0)
        //                        {
        //                            tokens.Enqueue(newElement.Substring(0, greatestElement.Length));
        //                            newElement = newElement.Remove(0, greatestElement.Length);
        //                        }
        //                        else
        //                        {
        //                            tokens.Enqueue(newElement.Substring(0, index));
        //                            newElement = newElement.Remove(0, index);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return tokens;
        //}

        public Queue<string> TokenizeText(string text, Dictionary<int, string> existingTokens)
        {
            List<string> characters = existingTokens.Values.ToList();
            characters.Sort((a, b) => b.Length.CompareTo(a.Length));
            Queue<string> tokens = new Queue<string>();
            string[] elements = text.Split(new[] { "\r\n", "\r", "\n", "\t", " " }, StringSplitOptions.None);
            foreach (string element in elements)
            {
                if (!string.IsNullOrWhiteSpace(element) && !string.IsNullOrEmpty(element))
                {
                    string find = characters.Find(x => element.Contains(x));
                    if (string.IsNullOrEmpty(find))
                    {
                        tokens.Enqueue(element);
                    }
                    else
                    {
                        int index = element.IndexOf(find);
                        string newElement = string.Empty;
                        if (index == 0)
                        {
                            tokens.Enqueue(element.Substring(0, find.Length));
                            newElement = element.Remove(0, find.Length);
                        }
                        else
                        {
                            tokens.Enqueue(element.Substring(0, index));
                            newElement = element.Remove(0, index);
                        }
                        while (newElement.Length > 0)
                        {
                            find = characters.Find(x => newElement.Contains(x));
                            if (string.IsNullOrEmpty(find))
                            {
                                tokens.Enqueue(newElement);
                                newElement = string.Empty;
                            }
                            else
                            {
                                index = newElement.IndexOf(find);
                                if (index == 0)
                                {
                                    tokens.Enqueue(newElement.Substring(0, find.Length));
                                    newElement = newElement.Remove(0, find.Length);
                                }
                                else
                                {
                                    tokens.Enqueue(newElement.Substring(0, index));
                                    newElement = newElement.Remove(0, index);
                                }
                            }
                        }
                    }
                }
            }
            return tokens;
        }

        public List<string> TokenListToPrint(Queue<string> tokensQ, Dictionary<int, string> existingTokensDic, Dictionary<string, List<string>> setsRangesDic) 
        {
            List<string> finalTokenList = new List<string>();
            while (tokensQ.Count() != 0)
            {                
                var myKey = existingTokensDic.FirstOrDefault(x => x.Value.Equals(tokensQ.Peek())).Key;
                if (myKey > 0)
                {
                    finalTokenList.Add($"{ tokensQ.Dequeue()} = { myKey}");
                }
                else
                {                    
                    bool end = false;
                    foreach (var setRange in setsRangesDic)
                    {
                        foreach (var range in setRange.Value)
                        {
                            int actualTokenInt = 0;
                            if (range.Contains("-"))
                            {
                                var subRange = range.Split('-');
                                if (tokensQ.Peek().Any(x=>Convert.ToInt32(x) >= int.Parse(subRange[0]))  && tokensQ.Peek().Any(x => Convert.ToInt32(x) <= int.Parse(subRange[1])))
                                {
                                    actualTokenInt = existingTokensDic.FirstOrDefault(x => x.Value.Contains(setRange.Key)).Key;
                                    finalTokenList.Add($"{tokensQ.Dequeue()} = {actualTokenInt}");
                                    end = true;
                                    break;
                                }
                            }
                            else if (tokensQ.Peek().Any(x => Convert.ToInt32(x) == int.Parse(range)))
                            {
                                actualTokenInt = existingTokensDic.FirstOrDefault(x => x.Value.Contains(setRange.Key)).Key;
                                Console.WriteLine($"{tokensQ.Dequeue()} = {actualTokenInt}");
                                end = true;
                                break;
                            }                            
                        }
                        if (end)
                        {
                            break;
                        }
                    }
                                       
                    
                }
            }
            return finalTokenList;
        }
        
    }
}
