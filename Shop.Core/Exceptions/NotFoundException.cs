using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string name)
    {
        throw new ArgumentException($"Not found {name}.");
    }
}
