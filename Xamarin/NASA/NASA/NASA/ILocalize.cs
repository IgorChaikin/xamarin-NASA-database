using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace NASA
{
    public interface ILocalize
    {
        CultureInfo GetCurrentCultureInfo();
    }
}
