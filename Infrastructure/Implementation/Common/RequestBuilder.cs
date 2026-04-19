using Application.Common.Dtos;
using Application.Common.Helpers;
using Application.Common.Interfaces.Common;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Implementation.Common
{
    public class RequestBuilder : IRequestBuilder
    {
        private ServerRowsRequest _serverRowsRequest;
        private List<SortModel> _sortModels;
        private dynamic _filterModels;
        public RequestBuilder() { }
        public RequestBuilder(ServerRowsRequest request)
        {
            AssignRequest(request);
        }
        public IRequestBuilder GetRequestBuilder(ServerRowsRequest request)
        {
            if (request == null)
                return new RequestBuilder(_serverRowsRequest);
            else

                return new RequestBuilder(request);
        }
        public void AssignRequest(ServerRowsRequest request)
        {
            _serverRowsRequest = request;
            _filterModels = request.FilterModel;
            _sortModels = request.SortModel;
        }
        public string GetSorts()
        {
            if (_sortModels.Count <= 0)
                return string.Empty;

            StringBuilder sorts = new StringBuilder(" ORDER BY ");
            foreach (SortModel sort in _sortModels)
            {
                sorts.Append($" {sort.ColId} ").Append($"{sort.Sort},");
            }
            return sorts.ToStringEx().TrimLastCharacter();
        }
        public int GetPageIndex()
        {
            return _serverRowsRequest.StartRow / (_serverRowsRequest.EndRow - _serverRowsRequest.StartRow) + 1;
        }
        public int GetPageSize()
        {
            return (_serverRowsRequest.EndRow - _serverRowsRequest.StartRow);
        }
        public string GetFilters()
        {
            dynamic filters = JsonConvert.DeserializeObject<dynamic>(_filterModels.ToString());
            StringBuilder whereClause = new StringBuilder(" 1=1 ");
            if (filters != null)
            {

                foreach (var column in filters)
                {
                    string columnName = column.Name;
                    string filterType = string.Empty;
                    string operatorType = string.Empty;
                    string filter = string.Empty;
                    string filterTo = string.Empty;
                    string type = string.Empty;
                    string dateFrom = string.Empty;
                    string dateTo = string.Empty;
                    dynamic columnFilters = column.Value;

                    dynamic filterCondition = columnFilters.filterType;
                    if (filterCondition != "set")
                    {

                        foreach (var dataType in columnFilters)
                        {
                            string name = dataType.Name;
                            if (name == "filterType")
                            {
                                filterType = dataType.Value;
                            }
                            else if (name == "operator")
                            {
                                operatorType = dataType.Value;
                            }
                            else if (name == "type")
                            {
                                type = dataType.Value;
                            }
                            else if (name == "filter")
                            {
                                filter = GetFilterValue(dataType);
                            }
                            else if (name == "filterTo")
                            {
                                filterTo = GetFilterValue(dataType);
                            }
                            else if (name == "dateFrom")
                            {
                                dateFrom = GetFilterValue(dataType);
                            }
                            else if (name == "dateTo")
                            {
                                dateTo = GetFilterValue(dataType);
                            }
                            if (name.Contains("condition"))
                            {
                                GetMultipleConditions(whereClause, columnName, filterType, operatorType, dataType, name);
                                type = filter = filterTo = string.Empty;
                            }
                            GetSingleCondition(whereClause, filterType, columnName, filter, filterTo, type, dateFrom, dateTo);
                        }
                    }
                    else
                    {
                        type = "Set";
                        filter = string.Join(",", columnFilters.values);
                        if (string.IsNullOrEmpty(filter))
                        {
                            filter = " NUll ";
                        }
                        GetSingleCondition(whereClause, filterType, columnName, filter, filterTo, type, dateFrom, dateTo);
                    }
                }
                //string test = whereClause.ToString();

            }
            return whereClause.ToStringEx();

        }
        private void GetMultipleConditions(StringBuilder whereClause, string columnName, string filterType, string operatorType, dynamic dataType, string name)
        {
            string filter = string.Empty;
            string filterTo = string.Empty;
            string type = string.Empty;
            string dateFrom = string.Empty;
            string dateTo = string.Empty;
            dynamic conditionFilters = dataType.Value;
            foreach (var condition in conditionFilters)
            {
                string conditionName = condition.Name;
                if (conditionName == "type")
                {
                    type = condition.Value;
                }
                else if (conditionName == "filter")
                {
                    filter = GetFilterValue(condition);
                }
                else if (conditionName == "filterTo")
                {
                    filterTo = GetFilterValue(condition);
                }
                else if (conditionName == "dateFrom")
                {
                    dateFrom = GetFilterValue(condition);
                }
                else if (conditionName == "dateTo")
                {
                    dateTo = GetFilterValue(condition);
                }
            }
            if (name == "condition1")
            {
                GetFirstCondition(whereClause, filterType, columnName, filter, filterTo, type, dateFrom, dateTo);
            }
            else if (name == "condition2")
            {
                GetSecondCondition(whereClause, filterType, columnName, operatorType, filter, filterTo, type, dateFrom, dateTo);
            }
        }
        private static void GetSecondCondition(StringBuilder whereClause, string filterType, string columnName, string operatorType, string filter, string filterTo, string type, string dateFrom, string dateTo)
        {
            if (type == "equals")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} = '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} = {filter}) ");
                }
            }
            else if (type == "notEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} != '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} != {filter}) ");
                }
            }
            else if (type == "greaterThan")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} > '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} > {filter}) ");
                }
            }
            else if (type == "greaterThanOrEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} >= '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} >= {filter}) ");
                }
            }
            else if (type == "lessThan")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} < '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} < {filter}) ");
                }
            }
            else if (type == "lessThanOrEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" {operatorType} {columnName} <= '{dateFrom}') ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" {operatorType} {columnName} <= {filter}) ");
                }
            }
            else if (type == "contains" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" {operatorType} {columnName} LIKE '%{filter}%' ) ");
            }
            else if (type == "lessThanOrEqual" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" {operatorType} {columnName} NOT LIKE '%{filter}%' ) ");
            }
            else if (type == "startsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" {operatorType} {columnName} LIKE '{filter}%' ) ");
            }
            else if (type == "endsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" {operatorType} {columnName} LIKE '%{filter}' ) ");
            }
            else if (type == "inRange")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    whereClause.Append($" {operatorType} {columnName} BETWEEN '{dateFrom}' AND '{dateTo}' ) ");
                }
                else if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterTo))
                {
                    whereClause.Append($" {operatorType} {columnName} BETWEEN {filter} AND {filterTo} ) ");
                }
            }
        }
        private static void GetFirstCondition(StringBuilder whereClause, string filterType, string columnName, string filter, string filterTo, string type, string dateFrom, string dateTo)
        {
            if (type == "equals")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} = '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} = {filter} ");
                }
            }
            else if (type == "notEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} != '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} != {filter} ");
                }
            }
            else if (type == "greaterThan")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} > '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} > {filter} ");
                }
            }
            else if (type == "greaterThanOrEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} >= '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} >= {filter} ");
                }
            }
            else if (type == "lessThan")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} < '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} < {filter} ");
                }
            }
            else if (type == "lessThanOrEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND ({columnName} <= '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND ({columnName} <= {filter} ");
                }
            }
            else if (type == "contains" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND ({columnName} LIKE '%{filter}%' ");
            }
            else if (type == "notContains" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND ({columnName} NOT LIKE '%{filter}%' ");
            }
            else if (type == "startsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND ({columnName} LIKE '{filter}%' ");
            }
            else if (type == "endsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND ({columnName} LIKE '%{filter}' ");
            }
            else if (type == "inRange")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    whereClause.Append($" AND ({columnName} BETWEEN '{dateFrom}' AND '{dateTo}' ");
                }
                else if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterTo))
                {
                    whereClause.Append($" AND ({columnName} BETWEEN {filter} AND {filterTo} ");
                }
            }

        }
        private static void GetSingleCondition(StringBuilder whereClause, string filterType, string columnName, string filter, string filterTo, string type, string dateFrom, string dateTo)
        {
            if (type == "equals")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND {columnName} = '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    if (columnName == "todaysDriver")
                    {
                        // Remove existing "todaysDriver" condition
                        string pattern = $@"\bAND\s+{columnName}\s*(=|IS)\s*(NULL|'[^']*'|\d+)\b";
                        string updatedClause = Regex.Replace(whereClause.ToString(), pattern, "", RegexOptions.IgnoreCase).Trim();

                        // Clear existing StringBuilder and update it
                        whereClause.Clear().Append(updatedClause);

                        // Append new "todaysDriver" condition
                        whereClause.Append($" AND {columnName} = {filter}");
                    }
                    else
                    {
                        filter = filterType == "text" ? $"'{filter}'" : filter;
                        whereClause.Append($" AND {columnName} = {filter} ");
                    }
                }
                else if (string.IsNullOrEmpty(filter) && columnName == "todaysDriver")
                {
                    // Remove existing "todaysDriver" condition
                    string pattern = $@"\bAND\s+{columnName}\s*(=|IS)\s*(NULL|'[^']*'|\d+)\b";
                    string updatedClause = Regex.Replace(whereClause.ToString(), pattern, "", RegexOptions.IgnoreCase).Trim();

                    // Clear and update StringBuilder
                    whereClause.Clear().Append(updatedClause);

                    // Append "IS NULL"
                    whereClause.Append($" AND {columnName} IS NULL");
                }
                

            }
        
            else if (type == "blank")
            {
                
                string pattern = $@"\bAND\s+{columnName}\s*(=|IS)\s*(NULL|'[^']*'|\d+)\b";
                string updatedClause = Regex.Replace(whereClause.ToString(), pattern, "", RegexOptions.IgnoreCase).Trim();

                
                whereClause.Clear().Append(updatedClause);

                whereClause.Append($" AND {columnName} IS NULL");
            }
            else if (type == "notEqual")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
                {
                    whereClause.Append($" AND {columnName} != '{dateFrom}' ");
                }
                else if (!string.IsNullOrEmpty(filter))
                {
                    filter = filterType == "text" ? $"'{filter}'" : filter;
                    whereClause.Append($" AND {columnName} != {filter} ");
                }
            }
            //else if (type == "greaterThan")
            //{
            //    if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
            //    {
            //        whereClause.Append($" AND {columnName} > '{dateFrom}' ");
            //    }
            //    else if (!string.IsNullOrEmpty(filter))
            //    {
            //        filter = filterType == "text" ? $"'{filter}'" : filter;
            //        whereClause.Append($" AND {columnName} > {filter} ");
            //    }
            //}
            //else if (type == "greaterThanOrEqual")
            //{
            //    if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
            //    {
            //        whereClause.Append($" AND {columnName} >= '{dateFrom}' ");
            //    }
            //    else if (!string.IsNullOrEmpty(filter))
            //    {
            //        filter = filterType == "text" ? $"'{filter}'" : filter;
            //        whereClause.Append($" AND {columnName} >= {filter} ");
            //    }
            //}
            //else if (type == "lessThan")
            //{
            //    if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
            //    {
            //        whereClause.Append($" AND {columnName} < '{dateFrom}' ");
            //    }
            //    else if (!string.IsNullOrEmpty(filter))
            //    {
            //        filter = filterType == "text" ? $"'{filter}'" : filter;
            //        whereClause.Append($" AND {columnName} < {filter} ");
            //    }
            //}
            //else if (type == "lessThanOrEqual")
            //{
            //    if (filterType == "date" && !string.IsNullOrEmpty(dateFrom))
            //    {
            //        whereClause.Append($" AND {columnName} <= '{dateFrom}' ");
            //    }
            //    else if (!string.IsNullOrEmpty(filter))
            //    {
            //        filter = filterType == "text" ? $"'{filter}'" : filter;
            //        whereClause.Append($" AND {columnName} <= {filter} ");
            //    }
            //}
            else if (type == "contains" && !string.IsNullOrEmpty(filter) && type != "time")
            {
                whereClause.Append($" AND {columnName} LIKE '%{filter}%' ");
            }
            else if (type == "time" && !string.IsNullOrEmpty(filter))
            {
                // Remove any AND clause that mentions 'time' in any form
                string pattern = @"\s+AND\s+[^A\s]*time[^A\s]*\s+(=|IS|LIKE)\s+('.*?'|NULL|%[^%]*%)";
                string updatedClause = Regex.Replace(whereClause.ToString(), pattern, "", RegexOptions.IgnoreCase).Trim();

                whereClause.Clear().Append(updatedClause);

                // Append formatted filter (corrected single quotes)
                whereClause.Append($" AND FORMAT(CAST({columnName} AS DATETIME), 'hh:mm tt') LIKE '%{filter}%'");
            }




            else if (type == "notContains" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND {columnName} NOT LIKE '%{filter}%' ");
            }
            else if (type == "startsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND {columnName} LIKE '{filter}%' ");
            }
            else if (type == "endsWith" && !string.IsNullOrEmpty(filter))
            {
                whereClause.Append($" AND {columnName} LIKE '%{filter}' ");
            }
            else if (type == "inRange")
            {
                if (filterType == "date" && !string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
                {
                    whereClause.Append($" AND {columnName} BETWEEN '{dateFrom}' AND '{dateTo}' ");
                }
                else if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterTo))
                {
                    whereClause.Append($" AND {columnName} BETWEEN {filter} AND {filterTo} ");
                }
            }
            //else if (type == "Set" && !string.IsNullOrEmpty(filter))
            //{
            //    int count = 1;
            //    int totalCount = filter.Split(",").Count();
            //    foreach (var val in filter.Split(","))
            //    {
            //        if (count == 1)
            //        {
            //            whereClause.Append($" AND ( {columnName} LIKE '%{val}%' ");
            //        }
            //        else
            //        {
            //            whereClause.Append($" OR {columnName} LIKE '%{val}%' ");
            //        }
            //        if (count == totalCount)
            //        {
            //            whereClause.Append(")");
            //        }
            //        count = count + 1;
            //    }
            //}
        }
        private static string GetFilterValue(dynamic condition)
        {
            return condition.Value.ToString();
        }
    }
}
