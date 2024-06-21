using Avalonia.Markup.Xaml.Templates;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private Dictionary<string, Dictionary<int, List<int>>> _documents = new();

    //Bug here in that method
    public void Add(int id, string documentText)
    {
        string pattern = @"(\s)|(\?)|(\,)|(\.)|(\!)|(\:)|(\-)|(\n)|(\r)";
        Regex regex = new Regex(pattern);
        int position = -1;
        foreach (string word in regex.Split(documentText))
        {
            if (!_documents.ContainsKey(word))
                _documents.Add(word, new Dictionary<int, List<int>>());
            if (!_documents[word].ContainsKey(id))
                _documents[word].Add(id, new List<int>());
            _documents[word][id].Add(position + 1);
            position += word.Length;
        }
    }

    public List<int> GetIds(string word) => _documents[word].Keys.ToList();

    //Refactor this
    public List<int> GetPositions(int id, string word)=> _documents[word][id];

    public void Remove(int id)
    {
        foreach (var key in _documents.Keys)
        {
            _documents[key].Remove(id);
        }
    }
}