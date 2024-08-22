using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rhinobill.core.Models.Common;

namespace rhinobill.sql.Converters
{
    public class EmailPropertyConverter : ValueConverter<Email, string>
    {
        public EmailPropertyConverter() 
            : base(
            v => v.EmailAddress,
            v => Email.ParseOrDefault(v))
        {
            
        }
    }
}
