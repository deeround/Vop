using System.Collections.Generic;

namespace Vop.Api.FluentException
{
    public class ErrorCodeOptions
    {
        public ErrorCodeOptions()
        {
            ErrorCodes = new List<KeyValuePair<int?, string>>();
        }

        public List<KeyValuePair<int?, string>> ErrorCodes { get; set; }

        public void Add(int? errCode, string errMsg)
        {
            ErrorCodes.Add(new KeyValuePair<int?, string>(errCode, errMsg));
        }
    }
}