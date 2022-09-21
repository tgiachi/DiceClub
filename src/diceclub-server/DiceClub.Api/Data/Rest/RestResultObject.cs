using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceClub.Api.Data.Rest
{
    public class RestResultObject<TData>
    {
        public TData Result { get; set; }

        public string? Error { get; set; }

        public bool HaveError { get; set; }
    }

    public class RestResultObjectBuilder<TData>
    {
        private RestResultObject<TData> _resultObject = new();

        public static RestResultObjectBuilder<TData> Create()
        {
            return new RestResultObjectBuilder<TData>();
        }

        public RestResultObjectBuilder<TData> Data(TData data)
        {
            _resultObject.Result = data;
            return this;
        }

        public RestResultObjectBuilder<TData> Error(Exception ex)
        {
            _resultObject.Error = ex.Message;
            _resultObject.HaveError = true;
            return this;
        }

        public RestResultObject<TData> Build()
        {
            return _resultObject;
        }
    }
}