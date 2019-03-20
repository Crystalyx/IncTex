using System;
using System.Collections.Generic;
using static IncTex.Category;

namespace IncTex
{
    public class TexState
    {
        public int[] IntRegistry = new int[256];
        public int[] LengthRegistry = new int[256];
        public SortedDictionary<Token, Delegate> Primitives = new SortedDictionary<Token, Delegate>();

        private readonly Category[] _categories = new Category[128];
        public Stack<object> PageStack = new Stack<object>();
        //todo constructor

        public TexState()
        {
            FillCategories();
        }

        private void FillCategories() //call in constructor
        {
            for (int i = 0; i < 128; i++)
            {
                _categories[i] = Other;
            }
            _categories['\\'] = Special; // special character
            _categories['{'] = GStart; // group start
            _categories['}'] = GEnd; // group end
            _categories['$'] = Maths; // math mode
            _categories['&'] = Tabulation; // tabulation
            _categories['\n'] = NewLine; // end of the line
            _categories['#'] = Parameter; // parameter number
            _categories['^'] = UpperIndex; // upper indexes
            _categories['_'] = LowerIndex; // lower indexes
            _categories['\0'] = IgnoreChar; // ignored character
            _categories[' '] = Space;
            for (char i = 'A'; i < 'Z'; i++)
                _categories[i] = Letter;
            for (char i = 'a'; i < 'z'; i++)
                _categories[i] = Letter;
            for (char i = '0'; i < '9'; i++)
                _categories[i] = Letter;
            //Categories['A-Za-z']= 11; // any word
            //Categories['<>']= 12; // other symbol
            _categories['~'] = Active; // active character
            _categories['%'] = Comment; // comment character
        }

        public Category GetCategory(char s)
        {
           return _categories[s];
        }
        
    }
}