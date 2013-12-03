using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Model.Complex
{
    public class LocalizedString
    {
        public LocalizedString()
        {
            French = null;
            English = null;
        }

        public string French { get; set; }
        public string English { get; set; }

        public string Current
        {
            get { return (string) LanguageProperty().GetValue(this); }
            set { LanguageProperty().SetValue(this, value); }
        }

        public override string ToString()
        {
            return Current;
        }

        private PropertyInfo LanguageProperty()
        {
            string currentLanguage = Thread.CurrentThread.CurrentUICulture.DisplayName.Split(' ').First();
            return GetType().GetProperty(currentLanguage);
        }

        public static implicit operator LocalizedString(string toConvert)
        {
            return new LocalizedString {Current = toConvert};
        }

        public static implicit operator string(LocalizedString toConvert)
        {
            return toConvert.Current;
        }
    }
}