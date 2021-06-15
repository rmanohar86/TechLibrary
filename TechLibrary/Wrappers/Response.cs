using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechLibrary.Wrappers
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int PageNumber { get; set; }
        public int TotalRows { get; set; }
        public Response(T data, int pageNumber, int totalRows)
        {
            Data = data;
            PageNumber = pageNumber;
            TotalRows = totalRows;
        }
    }
}
