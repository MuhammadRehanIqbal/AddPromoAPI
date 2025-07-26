using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstraction.Service
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
