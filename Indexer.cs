using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private Dictionary<int, List<string>> _documents = new();


	public void Add(int id, string documentText)
	{
		string pattern = @"\s|\?|\,|\.|\!|\:|\-|\n|\r";
        Regex regex = new Regex(pattern);
		List<string> words = regex.Split(documentText).Where(word => word != "").ToList();
		_documents.Add(id, words);
	}

	public List<int> GetIds(string word)
	{
		throw new NotImplementedException();
	}

	public List<int> GetPositions(int id, string word)
	{
		throw new NotImplementedException();
	}

	public void Remove(int id)
	{
		throw new NotImplementedException();
	}
}