using System.Text.RegularExpressions;

namespace OverwatchStats.Web.OverwatchApi.Validators
{
    public class BattleTagValidator
    {
        private static readonly Regex Validator = new Regex("^[a-zA-Z]+#[0-9]+$", RegexOptions.Compiled);
    
        public  static bool IsInvalid(string outcode)
            => !Validator.IsMatch(outcode);
    }
}
