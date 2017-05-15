using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace isms.Services
{
    public class ReadHelper<T>: List<T>
    {

        public int TotalRow { get; private set; }

        public ReadHelper(int totalItem, List<T> items)
        {
            TotalRow = totalItem;
            AddRange(items);
        }


        public static async Task<ReadHelper<T>> listAsync(IQueryable<T> source, IQueryCollection query)
        {
            var paramsListData = ParamsHelper.GetParamsListData(query);
            source = paramsListData.SortOrder.Equals("asc") ?
                source.OrderBy(obj => obj.GetType().GetProperty(paramsListData.SortColumn).GetValue(obj)) :
                source.OrderByDescending(obj => obj.GetType().GetProperty(paramsListData.SortColumn).GetValue(obj));
            var count = await source.CountAsync();
            var items = await source.Skip(paramsListData.PaginationOffset).Take(paramsListData.PaginationMax).ToListAsync();
            return new ReadHelper<T>(count, items);
        }

    }
}