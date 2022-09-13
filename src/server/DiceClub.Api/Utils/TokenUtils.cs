using System.Text.RegularExpressions;

namespace DiceClub.Api.Utils
{
    public class TokenUtils
    {

        public static int ExtractManaToken(string value)
        {
            try
            {
                var result = 0;
                var regEx = new Regex(@"\{([^}]*)\}");
                foreach (Match match in regEx.Matches(value))
                {
                    var val = match.Value.Replace("{", "").Replace("}", "");
                    if (val.All(char.IsDigit))
                    {
                        var number = int.Parse(val);
                        result += number;
                    }
                    else
                    {
                        result += 1;
                    }
                }

                return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}
