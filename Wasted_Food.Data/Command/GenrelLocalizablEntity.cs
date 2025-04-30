using System.Globalization;

namespace Wasted_Food.Data.Command
{
    public class GenrelLocalizablEntity
    {
        public string GetLocalize(string textAr, string textEn)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textAr;
            return textEn;
        }
    }
}
