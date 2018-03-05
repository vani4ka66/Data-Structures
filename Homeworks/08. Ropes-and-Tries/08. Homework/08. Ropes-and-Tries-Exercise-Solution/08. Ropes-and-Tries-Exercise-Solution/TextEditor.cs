using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace _08.Ropes_and_Tries_Exercise_Solution
{
    public class TextEditor : ITextEditor
    {
        private Trie<BigList<char> > usersStrings;
        private Trie<Stack<string>> usersStack;

        public TextEditor()
        {
            this.usersStrings = new Trie<BigList<char>>();
            this.usersStack = new Trie<Stack<string>>();
        }

        public void Login(string username)
        {
            this.usersStrings.Insert(username, new BigList<char>());
            this.usersStack.Insert(username, new Stack<string>());
        }

        public void Logout(string username)
        {
            this.usersStrings.Delete(username);
            this.usersStack.Delete(username);
        }

        public void Prepend(string username, string str)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            this.usersStack.GetValue(username).Push(string.Join("", this.usersStrings.GetValue(username)));
            this.usersStrings.GetValue(username).AddRangeToFront(str);
        }

        public void Insert(string username, int index, string str)
        {
            this.usersStack.GetValue(username).Push(string.Join("", this.usersStrings.GetValue(username)));

            this.usersStrings.GetValue(username).InsertRange(index, str);
        }

        public void Substring(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            string replace = string.Join("", this.usersStrings.GetValue(username).GetRange(startIndex, length));
            this.usersStrings.GetValue(username).Clear();
            this.usersStrings.GetValue(username).InsertRange(0, replace);
        }

        public void Delete(string username, int startIndex, int length)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            var userHistory = this.usersStack.GetValue(username);
            userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            this.usersStrings.GetValue(username).RemoveRange(startIndex, length);
        }

        public void Clear(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            //this.usersStack.GetValue(username).Push(string.Join("", this.usersStrings.GetValue(username)));

            this.usersStrings.GetValue(username).Clear();
            this.usersStack.GetValue(username).Clear();

            //this.usersStrings.Insert(username, new BigList<char>());
        }

        public int Length(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return 0;
            }

            //var userHistory = this.usersStack.GetValue(username);
            //userHistory.Push(string.Join("", this.usersStrings.GetValue(username)));

            return this.usersStrings.GetValue(username).Count;
        }

        public string Print(string username)
        {
            if (!usersStrings.Contains(username))
            {
                return "";
            }

            return string.Join("", this.usersStrings.GetValue(username));
        }

        //??
        public void Undo(string username)
        {
            if (!this.usersStrings.Contains(username))
            {
                return;
            }

            if (this.usersStack.GetValue(username).Count == 0)
            {
                return;
            }

            var userString = this.usersStrings.GetValue(username);

            var lastUserHistory = this.usersStack.GetValue(username).Pop();
            this.usersStack.GetValue(username).Push(string.Join("", userString));

            this.usersStrings.Insert(username, new BigList<char>(lastUserHistory));
        }

        public IEnumerable<string> Users(string prefix = "")
        {
            return this.usersStrings.GetByPrefix(prefix);
        }
    }
}
