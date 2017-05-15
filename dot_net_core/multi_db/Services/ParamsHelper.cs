using System;
using Microsoft.AspNetCore.Http;


namespace isms.Services
{

    public class ParamsListData
    {
        public string SearchValue { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
        public int PaginationOffset { get; set; }
        public int PaginationMax { get; set; }
    }

    public class ParamsHelper
    {

        public static ParamsListData GetParamsListData(IQueryCollection query)
        {
            string sort = query["sort"];
            string sortCol = query["sortCol"];
            string offset = query["offset"];
            string max = query["max"];
            var paramsListData = new ParamsListData
            {
                PaginationMax = max != null ? int.Parse(max) : 10,
                PaginationOffset = offset != null ? int.Parse(offset) : 0,
                SearchValue = query["search"],
                SortOrder = sort ?? "desc",
                SortColumn = sortCol ?? "id"
            };
            return paramsListData;
        }

    }
}