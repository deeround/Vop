using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vop.Api
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
