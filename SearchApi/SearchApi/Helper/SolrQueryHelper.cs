using System;
using System.Collections.Generic;
using System.Text;
using RandevuNokta.Search.Api.Enums;
using RandevuNokta.Search.Api.Models;

namespace RandevuNokta.Search.Api.Helper
{
    public static class SolrQueryHelper
    {
        public static StringBuilder BuildQuery(SearchParameters parameters)
        {
            var solrQuery = new List<string>();
            var solrExcludeQuery = new List<string>();

            var searchQuery = new StringBuilder();

            foreach (var searchType in parameters.SearchFor)
            {
                solrQuery.Add($"{searchType.Key}:\"{searchType.Value}\"");
            }

            if (solrQuery.Count > 0)
            {
                searchQuery.Append("q=");
                searchQuery.Append(string.Join(" OR ", solrQuery));
            }

            foreach (var searchType in parameters.Exclude)
            {
                solrExcludeQuery.Add($"{searchType.Key}:\"{searchType.Value}\"");
            }

            if (solrExcludeQuery.Count > 0)
            {
                searchQuery = searchQuery.Length > 0 ? searchQuery : searchQuery.Append("q=*:*");
                searchQuery.Append("-");
                searchQuery.Append(string.Join(" OR ", solrExcludeQuery));
            }

            if (string.IsNullOrEmpty(searchQuery.ToString()))
            {
                searchQuery.Append("q=*:*");
            }
            return searchQuery;
        }

        public static StringBuilder BuildFilterQueries(SearchParameters parameters)
        {
            var solrFilterQuery = new List<string>();

            var searchQuery = new StringBuilder();

            foreach (var filterType in parameters.FilterBy)
            {
                solrFilterQuery.Add($"{filterType.Key}:{filterType.Value}");
            }

            if (solrFilterQuery.Count > 0)
            {
                searchQuery.Append("&fq=");
                searchQuery.Append(string.Join(" AND ", solrFilterQuery));
            }

            return searchQuery;
        }
        
        public static StringBuilder GetFieldList(SearchParameters parameters)
        {
            var searchQuery = new StringBuilder();

            if (!string.IsNullOrEmpty(parameters.FieldList))
            {
                searchQuery.Append($"&fl={parameters.FieldList}");
            }

            return searchQuery;
        }

        public static StringBuilder GetSelectedSort(SearchParameters parameters)
        {
            var sortQueries = new List<string>();

            var searchQuery = new StringBuilder();

            foreach (var sortBy in parameters.SortBy)
            {
                sortQueries.Add(sortBy.order.Equals(SortOrder.Ascending)
                    ? $"{sortBy.FieldName} ASC"
                    : $"{sortBy.FieldName} DESC");
            }
            
            if (sortQueries.Count > 0)
            {
                searchQuery.Append("&sort=");
                searchQuery.Append(string.Join(" , ", sortQueries));
            }

            return searchQuery;
        }
        
        public static StringBuilder GetPagination(SearchParameters parameters)
        {
            var searchQuery = new StringBuilder();

            if (parameters.PageIndex > 0)
            {
                searchQuery.Append($"&start={(parameters.PageIndex - 1) * parameters.PageSize}");
            }

            if (parameters.PageSize > 0)

            {
                searchQuery.Append($"&rows={parameters.PageSize}");
            }

            return searchQuery;
        }
    }
}