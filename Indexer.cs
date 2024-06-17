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

	public void Add(int id, string documentText)
	{
		string pattern = @"\s|\?|\,|\.|\!|\:|\-|\n|\r";
        Regex regex = new Regex(pattern);
		List<string> words = regex.Split(documentText).ToList();
		foreach(string word in words)
		{
            List<int> wordsIndexesInText = new List<int>();
            FindWordIndexes(documentText, word, ref wordsIndexesInText, 0);
			_documents[word] = new Dictionary<int, List<int>>() { { id, wordsIndexesInText } };
		}
    }

	public void FindWordIndexes(string documentText, string wordToFind, ref List<int> indexes, int index)
	{
        index = documentText.IndexOf(wordToFind, index);
        if (index == -1 || index == documentText.Length)
        {
            return;
        }
        FindWordIndexes(documentText, wordToFind, ref indexes, index+1);
        indexes.Add(index);
    }

    public List<int> GetIds(string word)
	{
		return _documents.Where(x => x.Key == word).SelectMany(x => x.Value.Select(y => y.Key)).ToList();
	}

	public List<int> GetPositions(int id, string word)
	{
		return _documents.Where(document => document.Key == word).SelectMany(y => y.Value.Where( y=> y.Key == id)
			.SelectMany(y => y.Value.Select(y => y))).ToList();
	}

	public void Remove(int id)
	{
		//_documents.Remove(id);
	}
}