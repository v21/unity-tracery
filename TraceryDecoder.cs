using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using LitJson;

public class TraceryGrammar {

    public string source;
    Dictionary<string,string[]> grammar;

    Regex rgx = new Regex(@"\#(?<expand>[^\#]*?)\#", RegexOptions.IgnoreCase);

    [ThreadStatic] private static System.Random random;

    public string Generate(int? randomSeed = null)
    {
        return GenerateFromNode("origin", randomSeed);
    }

    public string GenerateFromNode(string token, int? randomSeed = null)
    {
        if (randomSeed.HasValue)
        {
            random = new System.Random(randomSeed.Value);
        }
        if (grammar.ContainsKey(token))
        {
            var ret = Shuffle(grammar[token]).First();
            return rgx.Replace(ret, m => GenerateFromNode(m.Groups[1].Value));
        }
        else
        {
            return "[" + token + "]";
        }
    }


    public static List<T> Shuffle<T>(IEnumerable<T> list)
    {
        var rand = random ?? (random = new System.Random(unchecked(Environment.TickCount * 31 + System.Threading.Thread.CurrentThread.ManagedThreadId)));

        var output = new List<T>(list);
        int n = output.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            T value = output[k];
            output[k] = output[n];
            output[n] = value;
        }
        return output;
    }


    Dictionary<string,string[]> Decode(string source)
    {
        Dictionary<string,string[]> traceryStruct = new Dictionary<string, string[]>();
        var map = JsonToMapper(source);
        foreach (var key in map.Keys) {
            if (map[key].IsArray)
            {
                string[] entries = new string[map[key].Count];
                for (int i = 0; i < map[key].Count; i++) {
                    var entry = map[key][i];
                    entries[i] = (string)entry;
                }
                traceryStruct.Add(key, entries);
            }
            else if (map[key].IsString)
            {
                string[] entries = {map[key].ToString()};
                traceryStruct.Add(key, entries);
            }
            
        }
        return traceryStruct;
    }

    public static JsonData JsonToMapper(string tracery)
    {
        var traceryStructure = JsonMapper.ToObject(tracery);
        return traceryStructure;
    }

    public TraceryGrammar(string source)
    {
        this.source = source;
        this.grammar = Decode(source);
    }
}


