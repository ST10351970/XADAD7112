using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Models
{
    /// <summary>
    /// A snapshot of what a resident seems interested in, built by SearchTracker from
    /// their recent searches and the events they opened. Each dictionary maps a value
    /// (a category, a keyword, a tag or a day of the week) to an interest weight.
    /// </summary>
    public class InterestProfile
    {
        private readonly Dictionary<string, double> _categoryWeights =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, double> _keywordWeights =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<string, double> _tagWeights =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

        private readonly Dictionary<DayOfWeek, double> _dayWeights = new Dictionary<DayOfWeek, double>();

        // Categories the resident chose in the filter or opened an event in, as
        // opposed to ones that only turned up in keyword results.
        private readonly HashSet<string> _explicitCategories =
            new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public IReadOnlyDictionary<string, double> KeywordWeights
        {
            get { return _keywordWeights; }
        }

        public IEnumerable<string> Tags
        {
            get { return _tagWeights.Keys; }
        }

        public bool IsEmpty
        {
            get { return _categoryWeights.Count == 0 && _keywordWeights.Count == 0 && _tagWeights.Count == 0; }
        }

        // ------------------------------------------------------------------
        // Building (used by SearchTracker)
        // ------------------------------------------------------------------

        public void AddCategory(string category, double weight, bool isExplicit)
        {
            if (string.IsNullOrWhiteSpace(category) || weight <= 0) return;

            Add(_categoryWeights, category, weight);
            if (isExplicit)
            {
                _explicitCategories.Add(category);
            }
        }

        public void AddKeyword(string term, double weight)
        {
            if (string.IsNullOrWhiteSpace(term) || weight <= 0) return;
            Add(_keywordWeights, term, weight);
        }

        public void AddTag(string tag, double weight)
        {
            if (string.IsNullOrWhiteSpace(tag) || weight <= 0) return;
            Add(_tagWeights, tag, weight);
        }

        public void AddDay(DayOfWeek day, double weight)
        {
            if (weight <= 0) return;
            Add(_dayWeights, day, weight);
        }

        // ------------------------------------------------------------------
        // Reading (used by RecommendationEngine)
        // ------------------------------------------------------------------

        public double GetCategoryWeight(string category)
        {
            return Get(_categoryWeights, category);
        }

        public double GetTagWeight(string tag)
        {
            return Get(_tagWeights, tag);
        }

        public double GetDayWeight(DayOfWeek day)
        {
            return _dayWeights.TryGetValue(day, out double weight) ? weight : 0;
        }

        public bool IsExplicitCategory(string category)
        {
            return _explicitCategories.Contains(category);
        }

        public double MaxCategoryWeight
        {
            get { return MaxOf(_categoryWeights.Values); }
        }

        public double MaxTagWeight
        {
            get { return MaxOf(_tagWeights.Values); }
        }

        public double MaxDayWeight
        {
            get { return MaxOf(_dayWeights.Values); }
        }

        public double TotalKeywordWeight
        {
            get { return SumOf(_keywordWeights.Values); }
        }

        public double TotalDayWeight
        {
            get { return SumOf(_dayWeights.Values); }
        }

        /// <summary>
        /// The categories with the highest weight, strongest first.
        /// </summary>
        public List<string> GetTopCategories(int count)
        {
            List<KeyValuePair<string, double>> entries = new List<KeyValuePair<string, double>>(_categoryWeights);

            entries.Sort((a, b) =>
            {
                int byWeight = b.Value.CompareTo(a.Value);
                return byWeight != 0 ? byWeight : string.Compare(a.Key, b.Key, StringComparison.OrdinalIgnoreCase);
            });

            List<string> top = new List<string>();
            for (int i = 0; i < entries.Count && i < count; i++)
            {
                top.Add(entries[i].Key);
            }

            return top;
        }

        // ------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------

        private static void Add<TKey>(Dictionary<TKey, double> weights, TKey key, double amount) where TKey : notnull
        {
            weights.TryGetValue(key, out double current);
            weights[key] = current + amount;
        }

        private static double Get(Dictionary<string, double> weights, string key)
        {
            if (string.IsNullOrEmpty(key)) return 0;
            return weights.TryGetValue(key, out double weight) ? weight : 0;
        }

        private static double MaxOf(IEnumerable<double> values)
        {
            double max = 0;
            foreach (double value in values)
            {
                if (value > max) max = value;
            }
            return max;
        }

        private static double SumOf(IEnumerable<double> values)
        {
            double sum = 0;
            foreach (double value in values)
            {
                sum += value;
            }
            return sum;
        }
    }
}
