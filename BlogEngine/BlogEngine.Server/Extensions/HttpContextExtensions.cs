﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BlogEngine.Server.Extensions
{
    public static class HttpContextExtensions
    {
        public const string TotalAmountPagesHeaderKey = "totalAmountPages";

        public async static Task InsertPaginationParametersInResponseAsync<T>(
            this HttpContext httpContext, IEnumerable<T> enumerable, int recordsPerPage)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }
            if (recordsPerPage < 0)
            {
                throw new InvalidOperationException();
            }

            await Task.Run(() =>
            {
                ProcessInsertPaginationParametersInResponse(httpContext, enumerable, recordsPerPage);
            });
        }

        private static void ProcessInsertPaginationParametersInResponse<T>(
            HttpContext httpContext, IEnumerable<T> enumerable, int recordsPerPage)
        {
            double count = enumerable.Count();
            double totalAmountPages = Math.Ceiling(count / recordsPerPage);

            httpContext.Response.Headers.Add(TotalAmountPagesHeaderKey, totalAmountPages.ToString());
        }
    }
}