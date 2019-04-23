using System;
using System.Collections.Generic;
using System.IO;

namespace Patterns.SOLID
{
    public class Journal
    {
        private readonly IList<string> _entries = new List<string>();
        private int _count = 0;

        public int AddEntry(string text)
        {
            _entries.Add($"{++_count}: {text}");
            return _count;
        }

        public bool RemoveEntry(int index)
        {
            if (index >= 0 && index < _count)
            {
                _entries.RemoveAt(index);
                _count--;

                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
            else
            {
                // ...
            }
        }
    }
}
