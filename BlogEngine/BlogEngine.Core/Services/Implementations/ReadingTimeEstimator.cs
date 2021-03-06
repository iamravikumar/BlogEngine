﻿using System;
using System.Linq;
using System.Globalization;
using BlogEngine.Core.Services.Abstractions;
using BlogEngine.Shared.Helpers;
using BlogEngine.Core.Common.Extensions;

namespace BlogEngine.Core.Services.Implementations
{
    public class ReadingTimeEstimator : IReadingTimeEstimator
    {
        private readonly int _WPM; // Estimating Words per Minute

        public ReadingTimeEstimator(int wpm = 200) // 200 is average WPM
        {
            _WPM = wpm;
        }

        public int GetEstimatedReadingTime(string rawHtmlContent)
        {
            Preconditions.NotNullOrWhiteSpace(rawHtmlContent, nameof(rawHtmlContent));

            int totalWordCount = GetWordsCount(rawHtmlContent);
            decimal resultTime = (decimal)totalWordCount / _WPM;

            string resultTimeString = resultTime.ToString("0.00", CultureInfo.InvariantCulture);
            string[] timeParts = resultTimeString.Split('.');
            int minutes = int.Parse(timeParts[0]);
            int seconds = int.Parse(timeParts[1]);

            seconds = (int)Math.Round(seconds * 0.60);

            if (minutes > 0 && seconds < 30)
                return minutes;

            return minutes + 1;
        }

        private int GetWordsCount(string rawHtmlContent)
        {
            string content = rawHtmlContent.StripHtmlTagsWithRegex();

            var contentArray = content.Split(' ');

            var count = contentArray.Count(a => a.Length > 1);

            return count;
        }
    }
}