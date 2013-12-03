using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    interface IConcurrencyProtection
    {
        [ConcurrencyCheck]
        [Timestamp]
        Byte[] Timestamp { get; set; }
    }
}
