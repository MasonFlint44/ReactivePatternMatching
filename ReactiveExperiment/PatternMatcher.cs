using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveExperiment
{
    public class PatternMatcher<T>
    {
        private readonly IObservable<T> _originalStream;
        public IObservable<T> Matches { get; private set; }

        public PatternMatcher(IObservable<T> stream)
        {
            _originalStream = stream;
            Matches = _originalStream;
        }

        public PatternMatcher<List<T>> And(PatternMatcher<T> pattern)
        {
            return new PatternMatcher<List<T>>(Matches.Zip(pattern.Matches, (x, y) =>
            {
                if (x is List<T> xList)
                {
                    
                }

                return new List<T> {x, y};
            }));
        }

        public PatternMatcher<T> Or(T element)
        {
            return Or(x => x.Equals(element));
        }

        public PatternMatcher<T> Or(Func<T, bool> predicate)
        {
            var filteredStream = _originalStream.Where(predicate);

            // If stream hasn't been filtered yet, don't merge with it
            if (Matches == _originalStream)
            {
                Matches = filteredStream;
                return this;
            }

            Matches = Matches.Merge(filteredStream);
            return this;
        }

        // TODO
        public PatternMatcher<T> Then(PatternMatcher<T> pattern)
        {
            Matches.Subscribe();
        }
    }
}
